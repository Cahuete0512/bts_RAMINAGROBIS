using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMI_RA.DAL
{
    public class AssoProduitsFournisseurs_Depot_DAL : Depot_DAL<AssoProduitsFournisseurs_DAL>
    {
        public AssoProduitsFournisseurs_Depot_DAL()
            : base()
        {

        }

        #region GetAll
        public override List<AssoProduitsFournisseurs_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idProduits, idFournisseurs from assoProduitsFournisseurs";
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDeProduitsFournisseurs = new List<AssoProduitsFournisseurs_DAL>();

            while (reader.Read())
            {
                //dans reader.GetInt32 on met la colonne que l'on souhaite récupérer ici 0 = ID, 1 = Societe...
                var ProduitsFournisseurs = new AssoProduitsFournisseurs_DAL(reader.GetInt32(0), reader.GetInt32(1));

                listeDeProduitsFournisseurs.Add(ProduitsFournisseurs);
            }

            DetruireConnexionEtCommande();

            return listeDeProduitsFournisseurs;
        }
        #endregion

        #region Insert
        public override AssoProduitsFournisseurs_DAL Insert(AssoProduitsFournisseurs_DAL assoProduitsFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into assoProduitsFournisseurs (idFournisseurs, idProduits ) values (@idFournisseurs, @idProduits)";
            commande.Parameters.Add(new SqlParameter("@idProduits", assoProduitsFournisseurs.IdProduits));
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", assoProduitsFournisseurs.IdFournisseurs));
            commande.ExecuteNonQuery();

            DetruireConnexionEtCommande();

            return assoProduitsFournisseurs;
        }
        #endregion

        #region Delete
        public void Delete(int idProduits, int idFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from assoProduitsFournisseurs " +
                                   "where idFournisseurs=@idFournisseurs and idProduits=@idProduits";
            commande.Parameters.Add(new SqlParameter("@idProduits", idProduits));
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", idFournisseurs));
            commande.ExecuteNonQuery();

            DetruireConnexionEtCommande();
        }
        #endregion

        #region GetByID
        public override AssoProduitsFournisseurs_DAL GetByID(int ID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public override AssoProduitsFournisseurs_DAL Update(AssoProduitsFournisseurs_DAL produits)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetByIdProduit
        public List<AssoProduitsFournisseurs_DAL> GetByIdProduit(int idProduits)
        {

            CreerConnexionEtCommande();

            commande.CommandText = "select idProduits, idFournisseurs from assoProduitsFournisseurs " +
                                   "where idProduits=@idProduits ";
            commande.Parameters.Add(new SqlParameter("@idProduits", idProduits));
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeAssos = new List<AssoProduitsFournisseurs_DAL>();

            while (reader.Read())
            {
                var asso = new AssoProduitsFournisseurs_DAL(reader.GetInt32(0), 
                                                            reader.GetInt32(1));

                listeAssos.Add(asso);
            }

            DetruireConnexionEtCommande();

            return listeAssos;
        }
        #endregion

        #region GetByIdFournisseurs
        public AssoProduitsFournisseurs_DAL GetByIdFournisseurs(int idFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idProduits, idFournisseurs from assoProduitsFournisseurs where idFournisseurs=@idFournisseurs ";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", idFournisseurs));
            var reader = commande.ExecuteReader();

            var listeDeAssoProduitsFournisseurs = new List<AssoProduitsFournisseurs_DAL>();

            AssoProduitsFournisseurs_DAL AssoProduitsFournisseurs;
            if (reader.Read())
            {
                AssoProduitsFournisseurs = new AssoProduitsFournisseurs_DAL(reader.GetInt32(0), 
                                                                            reader.GetInt32(2));
            }
            else
                throw new Exception($"Pas de association produit fournisseur dans la BDD avec l'ID fournisseur {idFournisseurs}");

            DetruireConnexionEtCommande();

            return AssoProduitsFournisseurs;
        }
        #endregion

        #region Delete
        public override void Delete(AssoProduitsFournisseurs_DAL item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
