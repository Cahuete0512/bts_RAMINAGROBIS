using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMI_RA.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class FournisseursController : Controller
    {
        private IFournisseursService service;
        #region FournisseursController
        public FournisseursController(IFournisseursService srv)
        {
            service = srv;
        }
        #endregion

        #region GetAllFournisseurs
        [HttpGet]
        public IEnumerable<Fournisseurs> GetAllFournisseurs()
        {
            return service.GetAllFournisseurs().Select(f => new Fournisseurs(
                f.IdFournisseurs,
                f.Societe,
                f.CiviliteContact,
                f.NomContact,
                f.PrenomContact,
                f.Email,
                f.Adresse,
                f.DateAdhesion,
                f.Actif));
        }
        #endregion

        #region AlimenterCatalogue
        [HttpPost("catalogue/{IdFournisseurs}")]
        public void AlimenterCatalogue(int IdFournisseurs, IFormFile csvFile)
        {
            service.AlimenterCatalogue(IdFournisseurs, csvFile);
        }
        #endregion
        
        #region AlimenterCatalogueString
        [HttpPost("catalogueStringCSV/{IdFournisseurs}")]
        public void AlimenterCatalogueString(int IdFournisseurs, List<string> csvFile)
        {
            service.AlimenterCatalogueStringCSV(IdFournisseurs, csvFile);
        }
        #endregion

        #region InsertFournisseur
        [HttpPost]
        public Fournisseurs Insert(Fournisseurs f)
        {
            var f_metier = service.Insert(f);

            return f_metier;
        }
        #endregion

        #region UpdateFournisseur
        [HttpPut]
        public Fournisseurs Update(Fournisseurs f)
        {
            var f_metier = service.Update(f);

            return f_metier;
        }
        #endregion

        #region DesactiverFournisseur
        [HttpPost("desactiver")]
        public void Desactiver(int IdFournisseurs)
        {
            service.Desactiver(IdFournisseurs);
        }
        #endregion

        #region DeleteFournisseur
        [HttpDelete("{IdFournisseurs}")]
        public void Delete([FromRoute] int IdFournisseurs)
        {
            service.Delete(new Fournisseurs(IdFournisseurs));
        }
        #endregion
    }
}
