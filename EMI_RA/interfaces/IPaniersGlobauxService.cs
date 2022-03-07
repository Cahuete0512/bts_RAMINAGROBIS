using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace EMI_RA
{
    public interface IPaniersGlobauxService
    {
        public List<PaniersGlobaux> GetAllPaniersGlobaux();
        public PaniersGlobaux GetPaniersGlobauxByID(int IDPanierGlobaux);
        public PaniersGlobaux Insert(PaniersGlobaux p);
        public PaniersGlobaux Update(PaniersGlobaux p);
        public PaniersGlobaux UpdateCloture(PaniersGlobaux p);
        public void Delete(PaniersGlobaux p);
        public PaniersGlobaux getPanierGlobal();
        public Stream genererPanierStream(int annee, int semaine);
        public Stream genererPanierStream(int annee, int semaine, int idFournisseur);
        public void genererListeAchatString(int IdAdherent, IEnumerable<String> csvFile);
        public List<String> genererPanierString(int annee, int semaine, int idFournisseur);
        public void Cloturer(int pgId);
        public void genererListeAchat(int IdAdherent, IFormFile csvFile);
    }
}
