﻿using EMI_RA.DTO;
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
    public class FournisseursController : Controller
    {
        private IFournisseursService service;

        public FournisseursController(IFournisseursService srv)
        {
            service = srv;
        }

        // GET: api/<FournisseursController>
        [HttpGet]
        public IEnumerable<Fournisseurs_DTO> GetAllFournisseurs()
        {
            return service.GetAllFournisseurs().Select(f => new Fournisseurs_DTO()
            {
                IdFournisseurs = f.IdFournisseurs,
                Societe = f.Societe,
                CiviliteContact = f.CiviliteContact,
                NomContact = f.NomContact,
                PrenomContact = f.PrenomContact,
                Email = f.Email,
                Adresse = f.Adresse,
            });
        }

        [HttpPost]
        public Fournisseurs_DTO Insert(Fournisseurs_DTO f)
        {
            var f_metier = service.Insert(new Fournisseurs(f.IdFournisseurs,
                                                   f.Societe,
                                                   f.CiviliteContact,
                                                   f.NomContact,
                                                   f.PrenomContact,
                                                   f.Email,
                                                   f.Adresse));
            //Je récupère l'ID
            f.IdFournisseurs = f_metier.IdFournisseurs;
            //je renvoie l'objet DTO
            return f;
        }

        // GET api/<FournisseursController>/5
        //[HttpGet("{idFournisseurs}")]
        //public string Get(int idFournisseurs)
        //{
        //    return "value";
        //}

        //// POST api/<FournisseursController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<FournisseursController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<FournisseursController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}