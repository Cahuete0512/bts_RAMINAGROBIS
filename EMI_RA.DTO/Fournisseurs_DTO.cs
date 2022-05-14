using System;

namespace EMI_RA.DTO
{
    public class Fournisseurs_DTO
    {
        public int IdFournisseurs { get; set; }
        public String Societe { get; set; }
        public String CiviliteContact { get; set; }
        public String NomContact { get; set; }
        public String PrenomContact { get; set; }
        public String Email { get; set; }
        public String Adresse { get; set; }
        public DateTime DateAdhesion { get; set; }
        public bool Actif { get; set; }
    }
}
