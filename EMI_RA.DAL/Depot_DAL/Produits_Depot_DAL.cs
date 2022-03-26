using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMI_RA.DAL
{
    public class Produits_Depot_DAL : Depot_DAL<Produits_DAL>
    {
        public Produits_Depot_DAL()
            : base()
        {

        }

        /**
         * Recupère tous les produits disponibles (flag "disponible" = 1)
         */
        public override List<Produits_DAL> GetAll()
        {
            CreerConnexionEtCommande();
            
            commande.CommandText = "select idProduits, libelle, marque, reference, disponible from produits where disponible=1";
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDeProduits = new List<Produits_DAL>();

            while (reader.Read())
            {
                //dans reader.GetInt32 on met la colonne que l'on souhaite récupérer ici 0 = ID, 1 = Societe...
                var produits = new Produits_DAL(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4));

                listeDeProduits.Add(produits);
            }

            DetruireConnexionEtCommande();

            return listeDeProduits;
        }

        public override Produits_DAL GetByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idProduits, libelle, marque, reference, disponible from produits where idProduits = @idProduits";
            commande.Parameters.Add(new SqlParameter("@idProduits", ID));
            var reader = commande.ExecuteReader();

            var depotFournisseur = new Fournisseurs_Depot_DAL();

            var listeDeProduits = new List<Produits_DAL>();

            Produits_DAL produit;
            if (reader.Read())
            {
                produit = new Produits_DAL(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4));


                var fournisseurs = depotFournisseur.GetByProduitID(produit.ID);

                produit.fournisseurListe = fournisseurs;

            }
            else
                throw new Exception($"Pas de produit dans la BDD avec l'ID {ID}");

            DetruireConnexionEtCommande();

            return produit;
        }

        public override Produits_DAL Insert(Produits_DAL produits)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into produits (libelle, marque, reference, disponible) values (@libelle, @marque, @reference, @disponible) ; SELECT SCOPE_IDENTITY()";
            commande.Parameters.Add(new SqlParameter("@libelle", produits.Libelle));
            commande.Parameters.Add(new SqlParameter("@marque", produits.Marque));
            commande.Parameters.Add(new SqlParameter("@reference", produits.Reference));
            commande.Parameters.Add(new SqlParameter("@disponible", produits.Disponible));
            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            produits.ID = ID;

            DetruireConnexionEtCommande();

            return produits;
        }

        public Produits_DAL GetByRef(string reference)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idProduits, libelle, marque, reference from produits where reference = @reference";
            commande.Parameters.Add(new SqlParameter("@reference", reference));
            var reader = commande.ExecuteReader();

            var listeDeProduits = new List<Produits_DAL>();

            Produits_DAL produits = null;
            if (reader.Read())
            {
                produits = new Produits_DAL(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
            }

            DetruireConnexionEtCommande();

            return produits;
        }

        public List<Produits_DAL> GetByIdFournisseur(int idFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select p.idProduits, p.libelle, p.marque, p.reference from produits p " +
                                    "inner join assoProduitsFournisseurs apf on p.idProduits = apf.idProduits " +
                                    "where apf.idFournisseurs = @idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", idFournisseurs));
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDeProduits = new List<Produits_DAL>();

            while (reader.Read())
            {
                //dans reader.GetInt32 on met la colonne que l'on souhaite récupérer ici 0 = ID, 1 = Societe...
                var produits = new Produits_DAL(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

                listeDeProduits.Add(produits);
            }

            DetruireConnexionEtCommande();

            return listeDeProduits;
        }

        public override Produits_DAL Update(Produits_DAL produits)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update produits set libelle = @libelle, marque = @marque, reference = @reference, disponible = @disponible where idProduits=@idProduits";
            commande.Parameters.Add(new SqlParameter("@idProduits", produits.ID));
            commande.Parameters.Add(new SqlParameter("@libelle", produits.Libelle));
            commande.Parameters.Add(new SqlParameter("@marque", produits.Marque));
            commande.Parameters.Add(new SqlParameter("@reference", produits.Reference));
            commande.Parameters.Add(new SqlParameter("@disponible", produits.Disponible));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour le produit avec l'ID  {produits.ID}");
            }

            DetruireConnexionEtCommande();

            return produits;
        }

        public override void Delete(Produits_DAL produits)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from produits where idProduits = @idProduits";
            commande.Parameters.Add(new SqlParameter("@idProduits", produits.ID));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer le produit avec l'ID {produits.ID}");
            }

            DetruireConnexionEtCommande();
        }
    }
}
