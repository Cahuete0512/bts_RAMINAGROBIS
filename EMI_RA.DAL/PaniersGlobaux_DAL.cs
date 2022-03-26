using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA.DAL
{
    public class PaniersGlobaux_DAL
    {
        public int IDPaniersGlobaux { get; set; }

        public int NumeroSemaine { get; set; }
        public int Annee { get; set; }
        public Boolean Cloture { get; set; }

        public List<LignesPaniersGlobaux_DAL> lignesPaniersGlobauxListe { get; set; }

        public PaniersGlobaux_DAL(int numeroSemaine, int annee)
                    => (NumeroSemaine, Annee) = (numeroSemaine, annee);
        public PaniersGlobaux_DAL(int numeroSemaine, int annee, Boolean cloture)
                    => (NumeroSemaine, Annee, Cloture) = (numeroSemaine, annee, cloture);

        public PaniersGlobaux_DAL(int idPaniersGlobaux, int numeroSemaine, int annee, Boolean cloture)
                    => (IDPaniersGlobaux, NumeroSemaine, Annee, Cloture) = (idPaniersGlobaux, numeroSemaine, annee, cloture);
    }
}
