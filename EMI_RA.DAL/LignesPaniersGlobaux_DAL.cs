using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace EMI_RA.DAL
{
    public class LignesPaniersGlobaux_DAL
    {
        public int IDProduits { get;  set; }
        public int Quantite { get;  set; }
        public int IDPaniersGlobaux { get; set; }
        public int IDAdherents { get; set; }
        public int ID { get; set; }
        public Produits_DAL produit { get; set; }

        public LignesPaniersGlobaux_DAL(int idProduits, int quantites, int idPaniersGlobaux, int idAdherents) => 
            (IDProduits, Quantite, IDPaniersGlobaux, IDAdherents) = (idProduits, quantites, idPaniersGlobaux, idAdherents);

        public LignesPaniersGlobaux_DAL(int idLignesPaniersGlobaux, int idProduits, int quantites, int idPaniersGlobaux, int idAdherents)
                => (ID, IDProduits, Quantite, IDPaniersGlobaux, IDAdherents) = (idLignesPaniersGlobaux, idProduits, quantites, idPaniersGlobaux, idAdherents);

        public LignesPaniersGlobaux_DAL(int idLignesPaniersGlobaux)
        {
            ID = idLignesPaniersGlobaux;
        }
        public LignesPaniersGlobaux_DAL()
        {

        }
    }
}
