using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class AssoProduitsFournisseurs
    {
        public int IdFournisseurs { get; set; }
        public int IdProduits { get; set; }
        public AssoProduitsFournisseurs(int idProduits, int idFournisseurs)
            => (IdProduits, IdFournisseurs) = (idProduits, idFournisseurs);
    }
}
