using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace EMI_RA.DAL
{
    public class LignesPaniersGlobaux_Depot_DAL : Depot_DAL<LignesPaniersGlobaux_DAL>
    {
        public override List<LignesPaniersGlobaux_DAL> GetAll()
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idLignesPaniersGlobaux, idProduits, quantite, idPaniersGlobaux, idAdherents from lignesPaniersGlobaux";
            var reader = commande.ExecuteReader();

            var listeDeLignesPaniersGlobaux = new List<LignesPaniersGlobaux_DAL>();

            while (reader.Read())
            {
                var paniersGlobaux = new LignesPaniersGlobaux_DAL(reader.GetInt32(0),
                                                                  reader.GetInt32(1),
                                                                  reader.GetInt32(2),
                                                                  reader.GetInt32(3),
                                                                  reader.GetInt32(4));

                listeDeLignesPaniersGlobaux.Add(paniersGlobaux);
            }

            DetruireConnexionEtCommande();

            return listeDeLignesPaniersGlobaux;
        }

        public List<LignesPaniersGlobaux_DAL> GetPaniersGlobauxByID(int idProduits, int idPaniersGlobaux, int idListesDAchats)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idLignesPaniersGlobaux, idProduits, quantite, idListesDAchats, idPaniersGlobaux from lignesPaniersGlobaux where idProduits=@idProduits";
            commande.Parameters.Add(new SqlParameter("@idProduits", idProduits));
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@idListesDAchats", idListesDAchats));

            var reader = commande.ExecuteReader();

            var listeDeLignesPaniersGlobaux = new List<LignesPaniersGlobaux_DAL>();

            while (reader.Read())
            {
                var paniersGlobaux = new LignesPaniersGlobaux_DAL(reader.GetInt32(0),
                                                                  reader.GetInt32(1),
                                                                  reader.GetInt32(2),
                                                                  reader.GetInt32(3),
                                                                  reader.GetInt32(4));

                listeDeLignesPaniersGlobaux.Add(paniersGlobaux);
            }

            DetruireConnexionEtCommande();

            return listeDeLignesPaniersGlobaux;
        }


        public override LignesPaniersGlobaux_DAL GetByID(int idLignesPaniersGlobaux)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idLignesPaniersGlobaux, idProduits, quantite, idListesDAchats, idPaniersGlobaux from lignesPaniersGlobaux where idPaniersGlobaux=@idPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idLignesPaniersGlobaux", idLignesPaniersGlobaux));
            var reader = commande.ExecuteReader();

            var listeDeLignesPaniersGlobaux = new List<LignesPaniersGlobaux_DAL>();

            LignesPaniersGlobaux_DAL paniersGlobaux;
            if (reader.Read())
            {
                paniersGlobaux = new LignesPaniersGlobaux_DAL(reader.GetInt32(0),
                                                              reader.GetInt32(1),
                                                              reader.GetInt32(2),
                                                              reader.GetInt32(3),
                                                              reader.GetInt32(4));
            }
            else
                throw new Exception($"Pas de paniers globaux dans la BDD avec l'ID {idLignesPaniersGlobaux}");

            DetruireConnexionEtCommande();

            return paniersGlobaux;
        }
        public List<LignesPaniersGlobaux_DAL> GetByPanierGlobauxID(int idPaniersGlobaux)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idLignesPaniersGlobaux, idProduits, quantite, idPaniersGlobaux, idAdherents from lignesPaniersGlobaux where idPaniersGlobaux=@idPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            var reader = commande.ExecuteReader();

            var produitDepot = new Produits_Depot_DAL();

            var listeDeLignesPaniersGlobaux = new List<LignesPaniersGlobaux_DAL>();

            while (reader.Read())
            {
                LignesPaniersGlobaux_DAL lignesPaniersGlobaux = new LignesPaniersGlobaux_DAL(reader.GetInt32(0),
                                                              reader.GetInt32(1),
                                                              reader.GetInt32(2),
                                                              reader.GetInt32(3),
                                                              reader.GetInt32(4));

                Produits_DAL produits = produitDepot.GetByID(lignesPaniersGlobaux.IDProduits);
                lignesPaniersGlobaux.produit = produits;

                listeDeLignesPaniersGlobaux.Add(lignesPaniersGlobaux);
            }

            DetruireConnexionEtCommande();

            return listeDeLignesPaniersGlobaux;
        }
        public List<LignesPaniersGlobaux_DAL> GetByPanierGlobauxIDWithRelations(int idPaniersGlobaux)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select idLignesPaniersGlobaux, idProduits, quantite, idPaniersGlobaux, idAdherents from lignesPaniersGlobaux where idPaniersGlobaux=@idPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            var reader = commande.ExecuteReader();

            var listeDeLignesPaniersGlobaux = new List<LignesPaniersGlobaux_DAL>();

            while (reader.Read())
            {
                LignesPaniersGlobaux_DAL lignesPaniersGlobaux = new LignesPaniersGlobaux_DAL(reader.GetInt32(0),
                                                              reader.GetInt32(1),
                                                              reader.GetInt32(2),
                                                              reader.GetInt32(3),
                                                              reader.GetInt32(4));
                listeDeLignesPaniersGlobaux.Add(lignesPaniersGlobaux);
            }

            DetruireConnexionEtCommande();

            return listeDeLignesPaniersGlobaux;
        }


        public List<LignesPaniersGlobaux_DAL> GetByPanierGlobauxIDAndFournisseurID(int idPaniersGlobaux, int idFournisseurs)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "select lpg.idLignesPaniersGlobaux, lpg.idProduits, lpg.quantite, lpg.idPaniersGlobaux, lpg.idAdherents " +
                "from lignesPaniersGlobaux lpg " +
                "inner join produits p on lpg.idProduits = p.idProduits " +
                "inner join assoProduitsFournisseurs a on p.idProduits = a.idProduits " +
                "where lpg.idPaniersGlobaux=@idPaniersGlobaux " +
                "and a.idFournisseurs=@idFournisseurs";
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", idPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@idFournisseurs", idFournisseurs));
            var reader = commande.ExecuteReader();

            var listeDeLignesPaniersGlobaux = new List<LignesPaniersGlobaux_DAL>();

            while (reader.Read())
            {
                LignesPaniersGlobaux_DAL lignesPaniersGlobaux = new LignesPaniersGlobaux_DAL(reader.GetInt32(0),
                                                                                             reader.GetInt32(1),
                                                                                             reader.GetInt32(2),
                                                                                             reader.GetInt32(3),
                                                                                             reader.GetInt32(4));
                listeDeLignesPaniersGlobaux.Add(lignesPaniersGlobaux);
            }

            DetruireConnexionEtCommande();

            return listeDeLignesPaniersGlobaux;
        }

        public override LignesPaniersGlobaux_DAL Insert(LignesPaniersGlobaux_DAL lignePanierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "insert into lignesPaniersGlobaux(idProduits, quantite, idPaniersGlobaux, idAdherents)"
                                    + " values (@idProduits, @quantite, @idPaniersGlobaux, @idAdherents); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@idProduits", lignePanierGlobal.IDProduits));
            commande.Parameters.Add(new SqlParameter("@quantite", lignePanierGlobal.Quantite));
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", lignePanierGlobal.IDPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@idAdherents", lignePanierGlobal.IDAdherents));

            var idLignesPaniersGlobaux = Convert.ToInt32((decimal)commande.ExecuteScalar());

            lignePanierGlobal.ID = idLignesPaniersGlobaux;

            DetruireConnexionEtCommande();

            return lignePanierGlobal;
        }

        public override LignesPaniersGlobaux_DAL Update(LignesPaniersGlobaux_DAL lignePanierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "update lignesPaniersGlobaux set idProduits=@idProduits, quantite=@quantite, idPaniersGlobaux=@idPaniersGlobaux, idAdherents=@idAdherents "
                                    + " where idLignesPaniersGlobaux=@idLignesPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idLignesPaniersGlobaux", lignePanierGlobal.ID));
            commande.Parameters.Add(new SqlParameter("@idProduits", lignePanierGlobal.IDProduits));
            commande.Parameters.Add(new SqlParameter("@quantite", lignePanierGlobal.Quantite));
            commande.Parameters.Add(new SqlParameter("@idPaniersGlobaux", lignePanierGlobal.IDPaniersGlobaux));
            commande.Parameters.Add(new SqlParameter("@idAdherents", lignePanierGlobal.IDAdherents));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour la ligne du panier global d'ID {lignePanierGlobal.ID}");
            }

            DetruireConnexionEtCommande();

            return lignePanierGlobal;
        }

        public override void Delete(LignesPaniersGlobaux_DAL lignePanierGlobal)
        {
            CreerConnexionEtCommande();

            commande.CommandText = "delete from LignesPaniersGlobaux where idLignesPaniersGlobaux=@idLignesPaniersGlobaux";
            commande.Parameters.Add(new SqlParameter("@idLignesPaniersGlobaux", lignePanierGlobal.ID));
            var nombreDeLignesAffectees = (int)commande.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer la ligne du panier global d'ID {lignePanierGlobal.ID}");
            }

            DetruireConnexionEtCommande();
        }
    }
}
