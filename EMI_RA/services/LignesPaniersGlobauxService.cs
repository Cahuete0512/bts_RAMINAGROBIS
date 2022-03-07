using EMI_RA.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_RA
{
    public class LignesPaniersGlobauxService : ILignesPaniersGlobauxService
    {
        private LignesPaniersGlobaux_Depot_DAL depot = new LignesPaniersGlobaux_Depot_DAL();

        public List<LignesPaniersGlobaux> GetAllLignesPaniersGlobaux()
        {
            var lignePanierGlobaux = depot.GetAll()
                .Select(l => new LignesPaniersGlobaux(l.ID,
                                                      l.IDProduits,
                                                      l.Quantite,
                                                      l.IDPaniersGlobaux,
                                                      l.IDAdherents))
                .ToList();

            return lignePanierGlobaux;
        }

        public List<LignesPaniersGlobaux> GetLignesPaniersGlobauxByPanierGlobauxID(int idPaniersGlobaux)
        {
            var liste = depot.GetByPanierGlobauxID(idPaniersGlobaux)
                .Select(l => new LignesPaniersGlobaux(l.ID,
                                                      l.IDProduits,
                                                      l.Quantite,
                                                      l.IDPaniersGlobaux,
                                                      l.IDAdherents))
                .ToList();

            return liste;
        }
        public List<LignesPaniersGlobaux> GetLignesPaniersGlobauxByPanierGlobauxIDAndFournisseurID(int idPaniersGlobaux, int idFournisseurs)
        {
            var liste = depot.GetByPanierGlobauxIDAndFournisseurID(idPaniersGlobaux, idFournisseurs)
                .Select(l => new LignesPaniersGlobaux(l.ID,
                                                      l.IDProduits,
                                                      l.Quantite,
                                                      l.IDPaniersGlobaux,
                                                      l.IDAdherents))
                .ToList();

            return liste;
        }

        public LignesPaniersGlobaux Insert(LignesPaniersGlobaux l)
        {
            var lignePaniersGlobaux = new LignesPaniersGlobaux_DAL(
                                                      l.IDProduits,
                                                      l.Quantite,
                                                      l.IDPaniersGlobaux,
                                                      l.IDAdherents);
            depot.Insert(lignePaniersGlobaux);
            l.ID = lignePaniersGlobaux.ID;

            return l;
        }
    }
}
