using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class LignesPaniersGlobaux
    {
        public int ID { get; set; }
        public int IDProduits { get; set; }
        public int Quantite { get; set; }
        public int IDPaniersGlobaux { get; set; }
        public int IDAdherents { get; set; }
        public Produits produit { get; set; }
        public LignesPaniersGlobaux(int idProduits, int quantites) 
            => (IDProduits, Quantite) = (idProduits, quantites);
        public LignesPaniersGlobaux(int idProduits, int quantites, int idPaniersGlobaux, int idAdherents) =>
            (IDProduits, Quantite, IDPaniersGlobaux, IDAdherents) = (idProduits, quantites, idPaniersGlobaux, idAdherents);

        public LignesPaniersGlobaux(int id, int idProduits, int quantites, int idPaniersGlobaux, int idAdherents) =>
            (ID, IDProduits, Quantite, IDPaniersGlobaux, IDAdherents) = (id, idProduits, quantites, idPaniersGlobaux, idAdherents);


    }
}
