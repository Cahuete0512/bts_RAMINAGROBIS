using EMI_RA.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMI_RA.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdherentsController : Controller
    {
        private IAdherentsService service;
        private IPaniersGlobauxService paniersGlobauxService;

        public AdherentsController(IAdherentsService srv, IPaniersGlobauxService paniersGlobauxService)
        {
            service = srv;
            this.paniersGlobauxService = paniersGlobauxService;
        }

        #region GetAllAdherents
        [HttpGet]
        public IEnumerable<Adherents> GetAllAdherents()
        {
            return service.GetAllAdherents().Select(a => new Adherents(
            
                a.ID,
                a.Societe,
                a.CiviliteContact,
                a.NomContact,
                a.PrenomContact,
                a.Email,
                a.Adresse
            ));
        }
        #endregion

        #region GenererListeAchat
        [HttpPost("commande")]
        public void GenererListeAchat(int IdAdherent, IFormFile csvFile)
        {
            paniersGlobauxService.GenererListeAchat(IdAdherent, csvFile);
        }
        #endregion

        #region GenererListeAchatString
        // récup les infos du CSV passée par le WPF
        [HttpPost("commandeVersion2")]
        public void GenererListeAchatString(int IdAdherent, IEnumerable<string> csvFile)
        {
            paniersGlobauxService.GenererListeAchatString(IdAdherent, csvFile);
        }
        #endregion

        #region Insert
        [HttpPost]
        public Adherents Insert(Adherents a)
        {
            var a_metier = service.Insert(a);
            
            return a_metier;
        }
        #endregion

        #region Update
        [HttpPut]
        public Adherents Update(Adherents a)
        {
            var a_metier = service.Update(a);
            
            return a_metier;
        }
        #endregion

        #region Delete
        [HttpDelete ("{id}")]
        public void Delete([FromRoute] int id)
        {
            service.Delete(new Adherents(id));
        }
        #endregion
    }
}
