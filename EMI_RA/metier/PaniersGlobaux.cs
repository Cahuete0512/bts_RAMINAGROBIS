using System;
using System.Collections.Generic;

namespace EMI_RA
{
    public class PaniersGlobaux
    {
        public int ID { get; set; }
        public int NumeroSemaine { get; set; }
        public int Annee { get; set; }
        public Boolean Cloture { get; set; }

        public List<LignesPaniersGlobaux> lignesPaniersGlobauxList { get; set; } 
        public PaniersGlobaux(int numeroSemaine, int annee)
                    => (NumeroSemaine, Annee) = (numeroSemaine, annee);
        public PaniersGlobaux(int numeroSemaine, int annee, Boolean cloture)
                    => (NumeroSemaine, Annee, Cloture) = (numeroSemaine, annee, cloture);
        public PaniersGlobaux(int idPaniersGlobaux, int numeroSemaine, int annee, Boolean cloture)
                    => (ID, NumeroSemaine, Annee, Cloture) = (idPaniersGlobaux, numeroSemaine, annee, cloture);
        public PaniersGlobaux(int id)
        {
            ID = id;
        }
    }
}
