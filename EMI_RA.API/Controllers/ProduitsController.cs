using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMI_RA.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProduitsController : Controller
    {
        private IProduitsService service;

        public ProduitsController(IProduitsService srv)
        {
            service = srv;
        }

        #region Get
        [HttpGet]
        public IEnumerable<Produits> Get()
        {
            return service.GetAll();
        }
        #endregion

        #region GetProduitsById
        [HttpGet("{id}")]
        public Produits GetProduitsById(int id)
        {
            return service.GetProduitsByID(id);
        }
        #endregion
    }
}
