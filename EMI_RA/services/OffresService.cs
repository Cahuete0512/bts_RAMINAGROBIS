using EMI_RA.DAL;
using System.Collections.Generic;
using System.Linq;

namespace EMI_RA
{
    public class OffresService : IOffresService
    {
        private Offres_Depot_DAL depot = new Offres_Depot_DAL();
        #region GetAllOffres
        public List<Offres> GetAllOffres()
        {
            var offre = depot.GetAll()
                .Select(offre => new Offres(offre.IdOffres,
                                            offre.IdFournisseurs,
                                            offre.NomContact,
                                            offre.IdPaniersGlobaux,
                                            offre.Libelle,
                                            offre.IdProduits,
                                            offre.Quantite,
                                            offre.Prix,
                                            offre.Gagne))
                .ToList();

            return offre;
        }
        #endregion
        #region GetOffreByIDFournisseur
        public Offres GetOffreByIDFournisseur(int idFournisseur)
        {
            var offre = depot.GetByIDFournisseur(idFournisseur);

            return new Offres(offre.IdOffres,
                                            offre.IdFournisseurs,
                                            offre.IdPaniersGlobaux,
                                            offre.IdProduits,
                                            offre.Quantite,
                                            offre.Prix,
                                            offre.Gagne);

        }
        #endregion
        #region GetOffreByIDPaniers
        public List<Offres> GetOffreByIDPaniers(int idPaniersGlobaux)
        {
            var offre = depot.GetByIDPaniers(idPaniersGlobaux)
                .Select(offre => new Offres(offre.IdOffres,
                                            offre.IdFournisseurs,
                                            offre.IdPaniersGlobaux,
                                            offre.IdProduits,
                                            offre.Quantite,
                                            offre.Prix,
                                            offre.Gagne))
                .ToList();

            return offre;

        }
        #endregion
        #region Insert
        public Offres Insert(Offres offres)
        {
            var offre = new Offres_DAL(offres.IdFournisseurs,
                                       offres.IdPaniersGlobaux,
                                       offres.IdProduits,
                                       offres.Quantite,
                                       offres.Prix);
            depot.Insert(offre);
            offre.IdFournisseurs = offre.IdFournisseurs;

            return offres;
        }
        #endregion
        #region Update
        public void Update(Offres offres)
        {
            var offre = new Offres_DAL(offres.IdOffres,
                                        offres.IdFournisseurs,
                                        offres.IdPaniersGlobaux,
                                        offres.IdProduits,
                                        offres.Quantite,
                                        offres.Prix,
                                        offres.Gagne);
            depot.Update(offre);
        }
        #endregion
        #region GetMeilleursOffres
        public List<Offres> GetMeilleursOffres(int idPaniersGlobaux)
        {
            var offre = depot.GetGagneByIDPaniers(idPaniersGlobaux)
                .Select(offre => new Offres(offre.NomContact,
                                            offre.Libelle,
                                            offre.Quantite,
                                            offre.Prix))
                .ToList();

            return offre;
        }
        #endregion
    }
}
