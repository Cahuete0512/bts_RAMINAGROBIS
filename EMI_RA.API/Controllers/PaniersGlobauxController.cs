using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using EMI_RA.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMI_RA.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaniersGlobauxController : Controller
    {
        private IPaniersGlobauxService service;

        public PaniersGlobauxController(IPaniersGlobauxService srv)
        {
            service = srv;
        }

        #region GetAllPaniersGlobaux
        [HttpGet]
        public IEnumerable<PaniersGlobaux> GetAllPaniersGlobaux()
        {
            return service.GetAllPaniersGlobaux().Select(p => new PaniersGlobaux(
                p.ID,
                p.NumeroSemaine,
                p.Annee, 
                p.Cloture
            ));
        }
        #endregion

        #region getPanier
        [HttpGet("panier")]
        public FileStreamResult getPanier()
        {
            int annee = DateTime.Now.AddDays(-7).Year;
            int semaine = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(-7), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            String filename = "Panier_" + annee + "_S" + semaine + ".csv";

            Stream stream = service.GenererPanierStream(annee, semaine);

            return new FileStreamResult(stream, "application/csv")
            {
                FileDownloadName = filename
            };
        }
        #endregion

        #region getPanierById
        [HttpGet("panier/byIdPanier/{idPanier}")]
        public PaniersGlobaux getPanierById([FromRoute] int idPanier)
        {
            return service.GetPaniersGlobauxByID(idPanier);
        }
        #endregion

        #region getPanier
        [HttpGet("panier/{idFournisseur}")]
        public FileStreamResult getPanier([FromRoute] int idFournisseur)
        {
            int annee = DateTime.Now.AddDays(-7).Year;
            int semaine = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(-7), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            String filename = "Panier_" + annee + "_S" + semaine + ".csv";

            Stream stream = service.GenererPanierStream(annee, semaine, idFournisseur);

            return new FileStreamResult(stream, "application/csv")
            {
                FileDownloadName = filename
            };
        }
        #endregion

        #region getPanierVersionTest
        [HttpGet("panier/Version test {idFournisseur}")]
        public List<String> getPanierVersionTest([FromRoute] int idFournisseur)
        {
            int annee = DateTime.Now.AddDays(-7).Year;
            int semaine = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(1), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            String filename = "Panier_" + annee + "_S" + semaine + ".csv";

            List<String> Panier = service.GenererPanierString(annee, semaine, idFournisseur);
            var commande_DTO = new Commande_DTO()
            {
                FichierCsv = Panier
            };
            return Panier;
 
        }
        #endregion

        #region LancerEnchere
        [HttpPost("lancerEnchere")]
        public void LancerEnchere(DateTime debutPeriode, DateTime finPeriode)
        {
            service.LancerEnchere(debutPeriode, finPeriode);
        }
        #endregion

        #region Cloturer
        [HttpPost("cloturer")]
        public void Cloturer(int pgId)
        {
             service.Cloturer(pgId);
        }
        #endregion
    }
}
