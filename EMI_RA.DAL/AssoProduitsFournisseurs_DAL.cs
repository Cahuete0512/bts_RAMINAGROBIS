
namespace EMI_RA.DAL
{
    public class AssoProduitsFournisseurs_DAL
    {

        public int IdFournisseurs { get; set; }
        public int IdProduits { get; set; }
        public Produits_DAL produit { get; set; }
        public AssoProduitsFournisseurs_DAL(int idFournisseurs, int idProduits)
            => (IdFournisseurs, IdProduits) = (idFournisseurs, idProduits);
    }
}
