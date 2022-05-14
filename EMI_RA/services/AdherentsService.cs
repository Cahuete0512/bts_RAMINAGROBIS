using EMI_RA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class AdherentsService : IAdherentsService
    {
        private Adherents_Depot_DAL depotAdherents = new Adherents_Depot_DAL();

        #region GetAllAdherents
        public List<Adherents> GetAllAdherents()
        {
            var result = new List<Adherents>();

            foreach (var a in depotAdherents.GetAll())
            {
                Adherents adherent = new Adherents(a.ID,
                                                   a.Societe,
                                                   a.CiviliteContact,
                                                   a.NomContact,
                                                   a.PrenomContact,
                                                   a.Email,
                                                   a.Adresse);
                result.Add(adherent);
            }
            return result;
        }
        #endregion

        #region GetAdherentById
        public Adherents GetByID(int idAdherents)
        {
            var a = depotAdherents.GetByID(idAdherents);

            return new Adherents(a.ID,
                                 a.Societe,
                                 a.CiviliteContact,
                                 a.NomContact,
                                 a.PrenomContact,
                                 a.Email,
                                 a.Adresse);
        }
        #endregion

        #region InsertAdherent
        public Adherents Insert(Adherents a)
        {
            var adherents = new Adherents_DAL(a.ID,
                                              a.Societe,
                                              a.CiviliteContact,
                                              a.NomContact,
                                              a.PrenomContact,
                                              a.Email,
                                              a.Adresse);
            depotAdherents.Insert(adherents);

            a.ID = adherents.ID;

            return a;
        }
        #endregion

        #region UpdateAdherent
        public Adherents Update(Adherents a)
        {
            var adherents = new Adherents_DAL(a.ID,
                                              a.Societe,
                                              a.CiviliteContact,
                                              a.NomContact,
                                              a.PrenomContact,
                                              a.Email,
                                              a.Adresse);
            depotAdherents.Update(adherents);

            return a;
        }
        #endregion

        #region DeleteAdherent
        public void Delete(Adherents a)
        {
            var adherentDAL = new Adherents_DAL(a.ID,
                                              a.Societe,
                                              a.CiviliteContact,
                                              a.NomContact,
                                              a.PrenomContact,
                                              a.Email,
                                              a.Adresse);
            depotAdherents.Delete(adherentDAL);
        }
        #endregion
    }
}
