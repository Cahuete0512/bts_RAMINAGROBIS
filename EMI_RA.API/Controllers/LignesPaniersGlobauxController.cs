using EMI_RA.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMI_RA.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class LignesPaniersGlobauxController : Controller
    {
        private ILignesPaniersGlobauxService service;

        public LignesPaniersGlobauxController(ILignesPaniersGlobauxService srv)
        {
            service = srv;
        }

        #region GetPaniersGlobauxByID
        [HttpGet]
        public IEnumerable<LignesPaniersGlobaux> GetPaniersGlobauxByID()
        {
            return service.GetAllLignesPaniersGlobaux();
        }
        #endregion

        #region Insert
        [HttpPost]
        public LignesPaniersGlobaux Insert(LignesPaniersGlobaux l)
        {
            var l_metier = service.Insert(l);
            
            return l_metier;
        }
        #endregion
    }
}
