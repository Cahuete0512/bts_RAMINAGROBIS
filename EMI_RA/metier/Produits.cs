using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class Produits
    {
        public int ID { get; set; }
        public string Libelle { get; set; }
        public string Marque { get; set; }
        public string Reference { get; set; }
        public bool Disponible { get; set; }
        public List<Fournisseurs> fournisseurListe { get; set; }
        public Produits(string libelle, string marque, string reference, bool disponible)
           => (Libelle, Marque, Reference, Disponible) = (libelle, marque, reference, disponible);
        public Produits(int idProduits, string libelle, string marque, string reference, bool disponible)
             => (ID, Libelle, Marque, Reference, Disponible ) = (idProduits, libelle, marque, reference, disponible); 
        public Produits(int idProduits, string reference, string libelle, string marque)
              => (ID, Reference, Libelle, Marque) = (idProduits, reference, libelle, marque);
        public Produits (string reference, string libelle, string marque)
              => (Reference, Libelle, Marque) = (reference, libelle, marque);
    }
}
