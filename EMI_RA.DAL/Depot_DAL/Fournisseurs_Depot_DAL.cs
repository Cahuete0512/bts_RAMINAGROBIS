using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMI_RA.DAL
{
    public class Fournisseurs_Depot_DAL : Depot_DAL<Fournisseurs_DAL>
    {
        public Fournisseurs_Depot_DAL()
            : base()
        {

        }

        public override List<Fournisseurs_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idFournisseurs, societe, civiliteContact, nomContact, prenomContact, email, adresse, dateAdhesion, actif from fournisseurs";
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDeFournisseurs = new List<Fournisseurs_DAL>();

            while (reader.Read())
            {
                //dans reader.GetInt32 on met la colonne que l'on souhaite récupérer ici 0 = idFournisseurs, 1 = societe...
                var fournisseur = new Fournisseurs_DAL(reader.GetInt32(0), 
                                                        reader.GetString(1), 
                                                        reader.GetString(2), 
                                                        reader.GetString(3), 
                                                        reader.GetString(4), 
                                                        reader.GetString(5),
                                                        reader.GetString(6),
                                                        reader.GetDateTime(7),
                                                        reader.GetBoolean(8));

                listeDeFournisseurs.Add(fournisseur);
            }

            DetruireConnexionEtCommande();

            return listeDeFournisseurs;
        }

        public override Fournisseurs_DAL GetByID(int idFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idFournisseurs, societe, civiliteContact, nomContact, prenomContact, email, adresse, dateAdhesion, actif from fournisseurs where idFournisseurs = @idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", idFournisseurs));
            var reader = commande.ExecuteReader();

            var listeDeFournisseurs = new List<Fournisseurs_DAL>();

            Fournisseurs_DAL fournisseur;
            if (reader.Read())
            {
                fournisseur = new Fournisseurs_DAL(reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetString(4),
                                        reader.GetString(5),
                                        reader.GetString(6),
                                        reader.GetDateTime(7),
                                        reader.GetBoolean(8)
                                        );
            }
            else
                throw new Exception($"Pas de fournisseur dans la BDD avec l'ID {idFournisseurs}");

            DetruireConnexionEtCommande();

            return fournisseur;
        }

        public override Fournisseurs_DAL Insert(Fournisseurs_DAL fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into fournisseurs (societe, civiliteContact, nomContact, prenomContact, email, adresse, dateAdhesion, actif)"
                                    + " values (@societe, @civiliteContact, @nomContact, @prenomContact, @email, @adresse, @dateAdhesion, @actif); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@societe", fournisseur.Societe));
            commande.Parameters.Add(new SqlParameter("@civiliteContact", fournisseur.CiviliteContact));
            commande.Parameters.Add(new SqlParameter("@nomContact", fournisseur.NomContact));
            commande.Parameters.Add(new SqlParameter("@prenomContact", fournisseur.PrenomContact));
            commande.Parameters.Add(new SqlParameter("@email", fournisseur.Email));
            commande.Parameters.Add(new SqlParameter("@adresse", fournisseur.Adresse));
            commande.Parameters.Add(new SqlParameter("@dateAdhesion", fournisseur.DateAdhesion));
            commande.Parameters.Add(new SqlParameter("@desactiver", fournisseur.Actif));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            fournisseur.IdFournisseurs = ID;

            DetruireConnexionEtCommande();

            return fournisseur;
        }

        public override Fournisseurs_DAL Update(Fournisseurs_DAL fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update fournisseurs set societe = @societe, civiliteContact = @civiliteContact, nomContact = @nomContact, prenomContact = @prenomContact, email = @email, adresse = @adresse, actif = @actif "
                                    + " where idFournisseurs=@idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", fournisseur.IdFournisseurs));
            commande.Parameters.Add(new SqlParameter("@societe", fournisseur.Societe));
            commande.Parameters.Add(new SqlParameter("@civiliteContact", fournisseur.CiviliteContact));
            commande.Parameters.Add(new SqlParameter("@nomContact", fournisseur.NomContact));
            commande.Parameters.Add(new SqlParameter("@prenomContact", fournisseur.PrenomContact));
            commande.Parameters.Add(new SqlParameter("@email", fournisseur.Email));
            commande.Parameters.Add(new SqlParameter("@adresse", fournisseur.Adresse));
            commande.Parameters.Add(new SqlParameter("@desactiver", fournisseur.Actif));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour le fournisseur avec l'ID  {fournisseur.IdFournisseurs}");
            }

            DetruireConnexionEtCommande();

            return fournisseur;
        }
        public  Fournisseurs_DAL UpdateDelete(Fournisseurs_DAL fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update fournisseurs set societe = @societe, civiliteContact = @civiliteContact, nomContact = @nomContact, prenomContact = @prenomContact, email = @email, adresse = @adresse, actif = @actif";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", fournisseur.IdFournisseurs));
            commande.Parameters.Add(new SqlParameter("@societe", fournisseur.Societe));
            commande.Parameters.Add(new SqlParameter("@civiliteContact", fournisseur.CiviliteContact));
            commande.Parameters.Add(new SqlParameter("@nomContact", fournisseur.NomContact));
            commande.Parameters.Add(new SqlParameter("@prenomContact", fournisseur.PrenomContact));
            commande.Parameters.Add(new SqlParameter("@email", fournisseur.Email));
            commande.Parameters.Add(new SqlParameter("@adresse", fournisseur.Adresse));
            commande.Parameters.Add(new SqlParameter("@actif", fournisseur.Actif));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour le fournisseur avec l'ID  {fournisseur.IdFournisseurs}");
            }

            DetruireConnexionEtCommande();

            return fournisseur;
        }
        public Fournisseurs_DAL UpdateFournisseurDesactive(Fournisseurs_DAL fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update fournisseurs set actif=0"
                                    + " where idFournisseurs=@idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", fournisseur.IdFournisseurs));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de désactiver le fournisseur avec l'ID  {fournisseur.IdFournisseurs}");
            }

            DetruireConnexionEtCommande();

            return fournisseur;
        }

        public override void Delete(Fournisseurs_DAL fournisseur)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from fournisseurs where idFournisseurs = @idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", fournisseur.IdFournisseurs));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer le fournisseur avec l'ID {fournisseur.IdFournisseurs}");
            }

            DetruireConnexionEtCommande();
        }
    }
}
