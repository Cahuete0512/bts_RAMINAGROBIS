using System.Collections.Generic;

namespace EMI_RA
{
    public interface IOffresService
    {
        public Offres Insert(Offres offres);
        public List<Offres> GetAllOffres();
        public List<Offres> GetOffreByIDPaniers(int idPaniersGlobaux);
        public Offres GetOffreByIDFournisseur(int idFournisseur);
        public void Update(Offres offres);
        public List<Offres> GetMeilleursOffres(int idPaniersGlobaux);
    }
}
