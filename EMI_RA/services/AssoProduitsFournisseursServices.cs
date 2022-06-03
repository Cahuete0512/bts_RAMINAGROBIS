using EMI_RA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class AssoProduitsFournisseursServices : IAssoProduitsFournisseursServices
    {
        private AssoProduitsFournisseurs_Depot_DAL depot = new AssoProduitsFournisseurs_Depot_DAL();
        #region GetAll
        public List<AssoProduitsFournisseurs> GetAll()
        {
            var assoProduits = depot.GetAll()
                .Select(a => new AssoProduitsFournisseurs(a.IdProduits,
                                                          a.IdFournisseurs))
                .ToList();

            return assoProduits;
        }
        #endregion

        #region GetByIdProduit
        public List<AssoProduitsFournisseurs> GetByIdProduit(int idProduits)
        {

            var result = new List<AssoProduitsFournisseurs>();

            foreach (var a in depot.GetByIdProduit(idProduits))
            {
                AssoProduitsFournisseurs asso = new AssoProduitsFournisseurs(a.IdProduits, 
                                                                             a.IdFournisseurs);
                result.Add(asso);
            }
            return result;
        }
        #endregion

        #region GetByIdFournisseurs
        public AssoProduitsFournisseurs GetByIdFournisseurs(int idFournisseurs)
        {
            var a = depot.GetByIdFournisseurs(idFournisseurs);

            return new AssoProduitsFournisseurs(a.IdProduits,
                                                a.IdFournisseurs);
        }
        #endregion

        #region Insert
        public AssoProduitsFournisseurs Insert(AssoProduitsFournisseurs a)
        {
            var assoProduits = new AssoProduitsFournisseurs_DAL(a.IdProduits,
                                                                a.IdFournisseurs);
            depot.Insert(assoProduits);

            return a;
        }
        #endregion

        #region Delete
        public void Delete(int idFournisseurs, int idProduits)
        {
            depot.Delete(idFournisseurs, idProduits);
        }
        #endregion
    }
}
