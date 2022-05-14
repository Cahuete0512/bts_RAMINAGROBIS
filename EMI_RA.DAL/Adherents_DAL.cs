using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EMI_RA.DAL
{
    public class Adherents_DAL
    {
        public int ID { get; set; }

        public List<Adherents_DAL> Adherents { get; set; }
        public String Societe { get; set; }
        public String CiviliteContact { get; set; }
        public String NomContact { get; set; }
        public String PrenomContact { get; set; }
        public String Email { get; set; }
        public String Adresse { get; set; }

        public Adherents_DAL(String societe, String civiliteContact, String nomContact, String prenomContact, String email, String adresse)
            =>(Societe, CiviliteContact, NomContact, PrenomContact, Email, Adresse) 
            = (societe, civiliteContact, nomContact, prenomContact, email, adresse);
        public Adherents_DAL(int idAdherents, String societe, String civiliteContact, String nomContact, String prenomContact, String email, String adresse)
            => (ID, Societe, CiviliteContact, NomContact, PrenomContact, Email, Adresse) 
            = (idAdherents, societe, civiliteContact, nomContact, prenomContact, email, adresse);
    }
}
