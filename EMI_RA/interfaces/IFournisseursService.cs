using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace EMI_RA
{
    public interface IFournisseursService
    {
        public List<Fournisseurs> GetAllFournisseurs();
        public void alimenterCatalogue(int IdFournisseurs, IFormFile csvFile);
        public void alimenterCatalogueVersion2(int IdFournisseurs, IEnumerable<String> csvFile);
        public Fournisseurs GetFournisseursByID(int IdFournisseurs);
        public Fournisseurs Insert(Fournisseurs f);
        public Fournisseurs Update(Fournisseurs f);
        public void Delete(Fournisseurs f);
    }
}
