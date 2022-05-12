using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class Fournisseurs
    {
        public int IdFournisseurs { get; set; }
        public String Societe { get; set; }
        public String CiviliteContact { get; set; }
        public String NomContact { get; set; }
        public String PrenomContact { get; set; }
        public String Email { get; set; }
        public String Adresse { get; set; }
        public DateTime DateAdhesion { get; set; }

        public Boolean Actif { get; set; }

        public Fournisseurs(String societe, String civiliteContact, String nomContact, String prenomContact, String email, String adresse)
                    => (Societe, CiviliteContact, NomContact, PrenomContact, Email, Adresse) = (societe, civiliteContact, nomContact, prenomContact, email, adresse);
        public Fournisseurs(int idFournisseurs, String societe, String civiliteContact, String nomContact, String prenomContact, String email, String adresse)
                    => (IdFournisseurs, Societe, CiviliteContact, NomContact, PrenomContact, Email, Adresse) = (idFournisseurs, societe, civiliteContact, nomContact, prenomContact, email, adresse);
        public Fournisseurs(int iDFournisseurs, String societe, String civiliteContact, String nomContact, String prenomContact, String email, String adresse, DateTime dateAdhesion, Boolean actif)
                   => (IdFournisseurs, Societe, CiviliteContact, NomContact, PrenomContact, Email, Adresse, DateAdhesion, Actif) = (iDFournisseurs, societe, civiliteContact, nomContact, prenomContact, email, adresse, dateAdhesion, actif);
        public Fournisseurs(int iDFournisseurs, String societe, String civiliteContact, String nomContact, String prenomContact, String email, String adresse, DateTime dateAdhesion)
            : this(societe, civiliteContact, nomContact, prenomContact, email, adresse)
        {
            IdFournisseurs = iDFournisseurs;
            DateAdhesion = dateAdhesion;
        }
        public Fournisseurs(int id)
        {
            IdFournisseurs = id;
        }
    }
}