using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA.DAL
{
    public class Offres_Depot_DAL : Depot_DAL<Offres_DAL>
    {
        public Offres_Depot_DAL()
            : base()
        {

        }
        public override Offres_DAL Insert(Offres_DAL offre)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into offres (idFournisseurs, idPaniersGlobaux, idProduits, quantite, prix, gagne)"
                                    + " values (@idFournisseurs, @idPaniersGlobaux,@idProduits, @quantite, @prix, 0);  SELECT SCOPE_IDENTITY();";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", offre.IdFournisseurs));
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", offre.IdPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@idProduits", offre.IdProduits));
            commande.Parameters.Add(new SqlParameter("@quantite", offre.Quantite));
            commande.Parameters.Add(new SqlParameter("@prix", offre.Prix));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());
            offre.IdOffres = ID;

            DetruireConnexionEtCommande();

            return offre;
        }
        public override List<Offres_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idOffres, idFournisseurs, idPaniersGlobaux, idProduits, quantite, prix, gagne from offres";
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDOffres = new List<Offres_DAL>();

            while (reader.Read())
            {
                //dans reader.GetInt32 on met la colonne que l'on souhaite récupérer ici 0 = ID, 1 = Societe...
                var offre = new Offres_DAL(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetFloat(5), reader.GetBoolean(6));

                listeDOffres.Add(offre);
            }

            DetruireConnexionEtCommande();

            return listeDOffres;
        }

        public override Offres_DAL GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public Offres_DAL GetByIDFournisseur(int idFournisseurs)
        {
            CreerConnexionEtCommande();
            
            commande.CommandText = "select idOffres, idFournisseurs, idPaniersGlobaux, idProduits, quantite, prix, gagne from offres where idFournisseurs=@idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", idFournisseurs));
            var reader = commande.ExecuteReader();

            var listeOffre = new List<Offres_DAL>();

            Offres_DAL offre;
            if (reader.Read())
            {
                offre = new Offres_DAL(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetFloat(5), reader.GetBoolean(6));
            }
            else
                throw new Exception($"Pas d'offre dans la BDD avec l'idFournisseur {idFournisseurs}");

            DetruireConnexionEtCommande();

            return offre;
        }

        public List<Offres_DAL> GetByIDPaniers(int idPaniersGlobaux)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idOffres, idFournisseurs, idPaniersGlobaux, idProduits, quantite, prix, gagne from offres where idPaniersGlobaux=@idPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            var reader = commande.ExecuteReader();

            var listeOffre = new List<Offres_DAL>();

            while (reader.Read())
            {
                Offres_DAL offre = new Offres_DAL(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetFloat(5), reader.GetBoolean(6));
                listeOffre.Add(offre);
            }

            DetruireConnexionEtCommande();

            return listeOffre;
        }

        public override Offres_DAL Update(Offres_DAL offre)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update offres set idPaniersGlobaux = @idPaniersGlobaux, prix = @prix, quantite = @quantite, idProduits = @idProduits, gagne = @gagne, idFournisseurs = @idFournisseurs where idOffres=@idOffres";
            commande.Parameters.Add(new SqlParameter("@idOffres", offre.IdOffres));
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", offre.IdPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@prix", offre.Prix));
            commande.Parameters.Add(new SqlParameter("@quantite", offre.Quantite));
            commande.Parameters.Add(new SqlParameter("@idProduits", offre.IdProduits));
            commande.Parameters.Add(new SqlParameter("@gagne", offre.Gagne));
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", offre.IdFournisseurs));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour l'offre avec l'ID  {offre.IdOffres}");
            }

            DetruireConnexionEtCommande();

            return offre;
        }
        public override void Delete(Offres_DAL offre)
        {
            throw new NotImplementedException();
        }

        public List<Offres_DAL> GetGagneByIDPaniers(int idPaniersGlobaux)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select societe, libelle, quantite, prix from offres join produits on offres.idProduits = produits.idProduits join fournisseurs on offres.idFournisseurs = fournisseurs.idFournisseurs where idPaniersGlobaux = @idPaniersGlobaux and gagne = 1";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            var reader = commande.ExecuteReader();

            var listeOffre = new List<Offres_DAL>();

            while (reader.Read())
            {
                Offres_DAL offre = new Offres_DAL(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetFloat(3));
                listeOffre.Add(offre);
            }

            DetruireConnexionEtCommande();

            return listeOffre;
        }
    }
}
