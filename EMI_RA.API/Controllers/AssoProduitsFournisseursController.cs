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
    public class AssoProduitsFournisseursController : Controller
    {
        private IAssoProduitsFournisseursServices service;

        public AssoProduitsFournisseursController(IAssoProduitsFournisseursServices srv)
        {
            service = srv;
        }

        [HttpGet]
        public IEnumerable<AssoProduitsFournisseurs_DTO> GetAllProduits()
        {
            return service.GetAll().Select(a => new AssoProduitsFournisseurs_DTO()
            {
                IdFournisseurs= a.IdFournisseurs,
                IdProduits = a.IdProduits
            });
        }

        [HttpPost]
        public AssoProduitsFournisseurs_DTO Insert(AssoProduitsFournisseurs_DTO a)
        {
            var a_metier = service.Insert(new AssoProduitsFournisseurs(a.IdFournisseurs,
                                                                       a.IdProduits));
           
            //je renvoie l'objet DTO
            return a;
        }

       
    }
}
