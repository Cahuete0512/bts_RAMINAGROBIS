using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class Offres
    {
        public int IdOffres { get; set; }
        public int IdFournisseurs { get; set; }
        public string NomFournisseur { get; set; }
        public int IdPaniersGlobaux { get; set; }
        public string NomProduit { get; set; }
        public int IdProduits { get; set; }
        public int Quantite { get; set; }
        public float Prix { get; set; }
        public Boolean Gagne { get; set; }

        public Offres(int idFournisseurs, int idPaniersGlobaux, int idProduits, int quantite, float prix)
                    => (IdFournisseurs, IdPaniersGlobaux, IdProduits, Quantite, Prix) = (idFournisseurs, idPaniersGlobaux, idProduits, quantite, prix);
        public Offres(int idOffres, int idFournisseurs, int idPaniersGlobaux, int idProduits, int quantite, float prix, Boolean gagne)
                    => (IdOffres, IdFournisseurs, IdPaniersGlobaux, IdProduits, Quantite, Prix, Gagne) = (idOffres, idFournisseurs, idPaniersGlobaux, idProduits, quantite, prix, gagne);
        public Offres(string nomFournisseur, string nomProduit, int quantite, float prix)
                    => (NomFournisseur, NomProduit, Quantite, Prix) = (nomFournisseur, nomProduit, quantite, prix);
    }
}
