using EMI_RA.DAL;
using System.Collections.Generic;
using System.Linq;

namespace EMI_RA
{
    public class OffresService : IOffresService
    {
        private Offres_Depot_DAL depot = new Offres_Depot_DAL();

        public List<Offres> GetAllOffres()
        {
            var offre = depot.GetAll()
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

        public List<Offres> GetMeilleursOffres(int idPaniersGlobaux)
        {
            var offre = depot.GetGagneByIDPaniers(idPaniersGlobaux)
                .Select(offre => new Offres(offre.NomFournisseur,
                                            offre.NomProduit,
                                            offre.Quantite,
                                            offre.Prix))
                .ToList();

            return offre;
        }
    }
}
