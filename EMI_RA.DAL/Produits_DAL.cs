using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA.DAL
{
    public class Produits_DAL
    {
        public int ID { get; set; }
        public List<Produits_DAL> Produits { get; set; }
        public string Libelle { get; set; }
        public string Marque { get; set; }
        public string Reference { get; set; }
        public List<AssoProduitsFournisseurs_DAL> ProduitsFournisseurs { get; set; }
        public bool Disponible { get; set; }

        public Produits_DAL(int idProduits, string libelle, string marque, string reference, bool disponible)
            => (ID, Libelle, Marque, Reference, Disponible) = (idProduits, libelle, marque, reference, disponible);

        public Produits_DAL(string libelle, string marque, string reference, bool disponible)
            => (Libelle, Marque, Reference, Disponible) = (libelle, marque, reference, disponible);
        public Produits_DAL(int idProduits, string libelle, string marque, string reference)
            => (ID, Libelle, Marque, Reference) = (idProduits, libelle, marque, reference);
        public Produits_DAL(string libelle, string marque, string reference)
           => (Libelle, Marque, Reference) = (libelle, marque, reference);
    }
}
