using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMI_RA.DAL
{
    public class PaniersGlobaux_Depot_DAL : Depot_DAL<PaniersGlobaux_DAL>
    {
        public PaniersGlobaux_Depot_DAL()
           : base()
        {

        }

        public override List<PaniersGlobaux_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idPaniersGlobaux, numeroSemaine, annee, cloture from paniersGlobaux";
            //pour lire les lignes une par une
            var reader = commande.ExecuteReader();

            var listeDePaniersGlobaux = new List<PaniersGlobaux_DAL>();

            while (reader.Read())
            { 
                var listeDePanierGlobal = new PaniersGlobaux_DAL(reader.GetInt32(0),
                                                                 reader.GetInt32(1),
                                                                 reader.GetInt32(2),
                                                                 reader.GetBoolean(3));


                listeDePaniersGlobaux.Add(listeDePanierGlobal);
            }

            DetruireConnexionEtCommande();

            return listeDePaniersGlobaux;
        }

        public override PaniersGlobaux_DAL GetByID(int idPaniersGlobaux)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idPaniersGlobaux, numeroSemaine, annee, cloture from paniersGlobaux where idPaniersGlobaux = @idPaniersGlobaux ";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            var reader = commande.ExecuteReader();

            var listeDePaniersGlobaux = new List<PaniersGlobaux_DAL>();

            PaniersGlobaux_DAL panierGlobal;
            if (reader.Read())
            {
                panierGlobal = new PaniersGlobaux_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetBoolean(3));
            }
            else
                throw new Exception($"Pas de panier dans la BDD avec l'ID {idPaniersGlobaux}");

            DetruireConnexionEtCommande();

            return panierGlobal;
        }

        public override PaniersGlobaux_DAL Insert(PaniersGlobaux_DAL panierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into paniersGlobaux (numeroSemaine, annee, cloture)"
                                    + " values (@numeroSemaine, @annee, 0); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@numeroSemaine", panierGlobal.NumeroSemaine));
            commande.Parameters.Add(new SqlParameter("@annee", panierGlobal.Annee));
   

            var idPaniersGlobaux = Convert.ToInt32((decimal)commande.ExecuteScalar());

            panierGlobal.IDPaniersGlobaux = idPaniersGlobaux;

            DetruireConnexionEtCommande();

            return panierGlobal;
        }

        public override PaniersGlobaux_DAL Update(PaniersGlobaux_DAL panierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update paniersGlobaux set numeroSemaine = @numeroSemaine, annee = @annee";
                                   
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", panierGlobal.IDPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@numeroSemaine", panierGlobal.NumeroSemaine));
            commande.Parameters.Add(new SqlParameter("@annee", panierGlobal.Annee));
            
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour le pannier global avec l'ID  {panierGlobal.IDPaniersGlobaux}");
            }

            DetruireConnexionEtCommande();

            return panierGlobal;
        }

        public PaniersGlobaux_DAL UpdateCloture(PaniersGlobaux_DAL panierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update paniersGlobaux set cloture='true'"
                                    + " where idPaniersGlobaux=@idPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", panierGlobal.IDPaniersGlobaux));

            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour le pannier global avec l'ID  {panierGlobal.IDPaniersGlobaux}");
            }

            DetruireConnexionEtCommande();

            return panierGlobal;
        }

        public override void Delete(PaniersGlobaux_DAL panierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from paniersGlobaux where idpaniersGlobaux = @idpaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idpaniersGlobaux", panierGlobal.IDPaniersGlobaux));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer le panier global avec l'ID {panierGlobal.IDPaniersGlobaux}");
            }

            DetruireConnexionEtCommande();
        }

        public PaniersGlobaux_DAL GetByYearAndWeek(int annee, int semaine)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idPaniersGlobaux, numeroSemaine, annee, cloture from paniersGlobaux where annee = @annee and numeroSemaine = @semaine";
            commande.Parameters.Add(new SqlParameter("@annee", annee));
            commande.Parameters.Add(new SqlParameter("@semaine", semaine));
            var reader = commande.ExecuteReader();

            var listeDePaniersGlobaux = new List<PaniersGlobaux_DAL>();

            PaniersGlobaux_DAL panierGlobal = null;
            if (reader.Read())
            {
                panierGlobal = new PaniersGlobaux_DAL(reader.GetInt32(0),
                                                      reader.GetInt32(1),
                                                      reader.GetInt32(2), 
                                                      reader.GetBoolean(3));
            }
            DetruireConnexionEtCommande();

            return panierGlobal;
        }

    }
}
