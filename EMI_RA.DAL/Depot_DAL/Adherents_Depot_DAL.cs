using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMI_RA.DAL
{
    public class Adherents_Depot_DAL : Depot_DAL<Adherents_DAL>
    {
        public Adherents_Depot_DAL()
            :base()
        {

        }

        public override List<Adherents_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idAdherents, societe, civiliteContact, nomContact, prenomContact, email, adresse from adherents";
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDeAdherents = new List<Adherents_DAL>();

            while (reader.Read())
            {
                //dans reader.GetInt32 on met la colonne que l'on souhaite récupérer ici 0 = ID, 1 = Societe...
                var adherent = new Adherents_DAL(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));

                listeDeAdherents.Add(adherent);
            }

            DetruireConnexionEtCommande();

            return listeDeAdherents;
        }

        public override Adherents_DAL GetByID(int ID)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idAdherents, societe, civiliteContact, nomContact, prenomContact, email, adresse from adherents"
            +" where idAdherents=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = commande.ExecuteReader();

            var listeDeAdherents = new List<Adherents_DAL>();

            Adherents_DAL adherent;
            if (reader.Read())
            {
                adherent = new Adherents_DAL(reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetString(4),
                                        reader.GetString(5),
                                        reader.GetString(6)
                                        );
            }
            else
                throw new Exception($"Pas de adherent dans la BDD avec l'ID {ID}");

            DetruireConnexionEtCommande();

            return adherent;
        }

        public override Adherents_DAL Insert(Adherents_DAL adherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into adherents (societe, civiliteContact, nomContact, prenomContact, email, adresse, dateAdhesion)"
                                    + " values (@societe, @civiliteContact, @nomContact, @prenomContact, @email, @adresse, @dateAdhesion); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@societe", adherent.Societe));
            commande.Parameters.Add(new SqlParameter("@civiliteContact", adherent.CiviliteContact));
            commande.Parameters.Add(new SqlParameter("@nomContact", adherent.NomContact));
            commande.Parameters.Add(new SqlParameter("@prenomContact", adherent.PrenomContact));
            commande.Parameters.Add(new SqlParameter("@email", adherent.Email));
            commande.Parameters.Add(new SqlParameter("@adresse", adherent.Adresse));
            commande.Parameters.Add(new SqlParameter("@dateAdhesion", DateTime.Now));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            adherent.ID = ID;

            DetruireConnexionEtCommande();

            return adherent;
        }

        public override Adherents_DAL Update(Adherents_DAL adherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update adherents set societe = @societe, civiliteContact = @civiliteContact, nomContact = @nomContact, prenomContact = @prenomContact, email = @email, adresse = @adresse "
                                    + " where idAdherents=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", adherent.ID));
            commande.Parameters.Add(new SqlParameter("@societe", adherent.Societe));
            commande.Parameters.Add(new SqlParameter("@civiliteContact", adherent.CiviliteContact));
            commande.Parameters.Add(new SqlParameter("@nomContact", adherent.NomContact));
            commande.Parameters.Add(new SqlParameter("@prenomContact", adherent.PrenomContact));
            commande.Parameters.Add(new SqlParameter("@email", adherent.Email));
            commande.Parameters.Add(new SqlParameter("@adresse", adherent.Adresse));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour l'adhérent avec l'ID  {adherent.ID}");
            }

            DetruireConnexionEtCommande();

            return adherent;
        }

        public override void Delete(Adherents_DAL adherent)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from adherents where idAdherents = @ID";
            commande.Parameters.Add(new SqlParameter("@ID", adherent.ID));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer l'adherent avec l'ID {adherent.ID}");
            }

            DetruireConnexionEtCommande();
        }
    }
}
