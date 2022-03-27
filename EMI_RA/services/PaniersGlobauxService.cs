using EMI_RA.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EMI_RA
{
    public class PaniersGlobauxService : IPaniersGlobauxService
    {
        private PaniersGlobaux_Depot_DAL depot = new PaniersGlobaux_Depot_DAL();
        private LignesPaniersGlobaux_Depot_DAL lignesPaniersGlobaux_depot = new LignesPaniersGlobaux_Depot_DAL();
        private LignesPaniersGlobauxService lignesPaniersGlobauxService = new LignesPaniersGlobauxService();
        private ProduitsServices produitsServices = new ProduitsServices();
        private OffresService offresService = new OffresService();
        private FournisseursService fournisseursService = new FournisseursService();

        public List<PaniersGlobaux> GetAllPaniersGlobaux()
        {
            var paniersGlobaux = depot.GetAll()
                .Select(p => new PaniersGlobaux(p.IDPaniersGlobaux,
                                                p.NumeroSemaine,
                                                p.Annee,
                                                p.Cloture))
                .ToList();

            return paniersGlobaux;
        }

        public PaniersGlobaux GetPaniersGlobauxByID(int idPaniersGlobaux)
        {
            var panierDAL = depot.GetByID(idPaniersGlobaux);

            var panier = new PaniersGlobaux(panierDAL.IDPaniersGlobaux,
                                              panierDAL.NumeroSemaine,
                                              panierDAL.Annee,
                                              panierDAL.Cloture);

            panier.lignesPaniersGlobauxList = new List<LignesPaniersGlobaux>();

            foreach(var lignesPaniersGlobauxDAL in panierDAL.lignesPaniersGlobauxListe)
            {
                LignesPaniersGlobaux lignesPaniersGlobaux = new LignesPaniersGlobaux(lignesPaniersGlobauxDAL.ID,
                                                                                      lignesPaniersGlobauxDAL.IDProduits,
                                                                                      lignesPaniersGlobauxDAL.Quantite,
                                                                                      lignesPaniersGlobauxDAL.IDPaniersGlobaux,
                                                                                      lignesPaniersGlobauxDAL.IDAdherents);

                panier.lignesPaniersGlobauxList.Add(lignesPaniersGlobaux);
                lignesPaniersGlobaux.produit = new Produits(lignesPaniersGlobauxDAL.produit.ID,
                                                            lignesPaniersGlobauxDAL.produit.Reference,
                                                            lignesPaniersGlobauxDAL.produit.Libelle,
                                                            lignesPaniersGlobauxDAL.produit.Marque);

                var fournisseurListe = new List<Fournisseurs>();

                foreach(var fournisseurDAL in lignesPaniersGlobauxDAL.produit.fournisseurListe)
                {
                    var fournisseur = new Fournisseurs(fournisseurDAL.IdFournisseurs, 
                                                       fournisseurDAL.Societe, 
                                                       fournisseurDAL.CiviliteContact, 
                                                       fournisseurDAL.NomContact, 
                                                       fournisseurDAL.PrenomContact, 
                                                       fournisseurDAL.Email, 
                                                       fournisseurDAL.Adresse);
                    fournisseurListe.Add(fournisseur);
                }

                lignesPaniersGlobaux.produit.fournisseurListe = fournisseurListe;
            }


            return panier;
        }

        public PaniersGlobaux GetPaniersGlobauxByID(int annee, int semaine)
        {
            var p = depot.GetByYearAndWeek(annee, semaine);

            return new PaniersGlobaux(p.IDPaniersGlobaux,
                                      p.NumeroSemaine,
                                      p.Annee,
                                      p.Cloture);
        }

        public PaniersGlobaux Insert(PaniersGlobaux p)
        {
            var paniersGlobaux = new PaniersGlobaux_DAL(p.ID,
                                                        p.NumeroSemaine,
                                                        p.Annee,
                                                        p.Cloture);
            depot.Insert(paniersGlobaux);
            p.ID = paniersGlobaux.IDPaniersGlobaux;

            return p;
        }

        public PaniersGlobaux GetPanierSemainePrecedente()
        {
            int annee = DateTime.Now.AddDays(-7).Year;
            int semaine = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(1), 
                                                                              CalendarWeekRule.FirstFullWeek, 
                                                                              DayOfWeek.Monday);

            return this.GetGlobal(annee, semaine);
        }

        public PaniersGlobaux Update(PaniersGlobaux p)
        {
            var paniersGlobaux = new PaniersGlobaux_DAL(p.ID,
                                                        p.NumeroSemaine,
                                                        p.Annee,
                                                        p.Cloture);
            depot.Update(paniersGlobaux);

            return p;
        }
        public PaniersGlobaux UpdateCloture(PaniersGlobaux p)
        {
            var paniersGlobaux = new PaniersGlobaux_DAL(p.ID,
                                                        p.NumeroSemaine,
                                                        p.Annee,
                                                        p.Cloture);
            depot.UpdateCloture(paniersGlobaux);

            return p;
        }

        public void Delete(PaniersGlobaux p)
        {
            var panierGlobaux = new PaniersGlobaux_DAL(p.ID,
                                                       p.NumeroSemaine,
                                                       p.Annee,
                                                       p.Cloture); ;
            depot.Delete(panierGlobaux);
        }
        public PaniersGlobaux GetPanierGlobal()
        {
            int annee = DateTime.Now.Year;
            int semaine = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, 
                                                                              CalendarWeekRule.FirstFullWeek, 
                                                                              DayOfWeek.Monday);

            return this.GetGlobal(annee, semaine);
        }
        public PaniersGlobaux GetGlobal(int annee, int semaine)
        {
            PaniersGlobaux_DAL paniersGlobauxDAL = depot.GetByYearAndWeek(annee, semaine);

            if (paniersGlobauxDAL == null)
            {
                paniersGlobauxDAL = new PaniersGlobaux_DAL(semaine, annee);
                depot.Insert(paniersGlobauxDAL);
            }
            return new PaniersGlobaux(paniersGlobauxDAL.IDPaniersGlobaux, paniersGlobauxDAL.NumeroSemaine, paniersGlobauxDAL.Annee, paniersGlobauxDAL.Cloture);
        }
        public Stream GenererPanierStream(int annee, int semaine)
        {
            StringBuilder contentBuilder = new StringBuilder("reference;quantite;prix unitaire HT\n");

            PaniersGlobaux paniersGlobaux = this.GetGlobal(annee, semaine);
            List<LignesPaniersGlobaux> lignesPaniersGlobaux = lignesPaniersGlobauxService.GetLignesPaniersGlobauxByPanierGlobauxID(paniersGlobaux.ID);

            List<LignesPaniersGlobaux> lignesAgregees = lignesPaniersGlobaux
                            .GroupBy(l => l.IDProduits)
                            .Select(ligneGroupe => new LignesPaniersGlobaux(ligneGroupe.Select(p => p.IDProduits).First(),
                                                                            ligneGroupe.Sum(p => p.Quantite)))
                            .ToList();

            foreach (var ligne in lignesAgregees)
            {
                Produits produits = produitsServices.GetProduitsByID(ligne.IDProduits);

                contentBuilder
                    .Append(produits.Reference)
                    .Append(";")
                    .Append(ligne.Quantite)
                    .Append(";0")
                    .Append("\n");
            }

            byte[] bytes = Encoding.ASCII.GetBytes(contentBuilder.ToString());
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }
        public Stream GenererPanierStream(int annee, int semaine, int idFournisseur)
        {
            StringBuilder contentBuilder = new StringBuilder("reference;quantite;prix unitaire HT\n");

            PaniersGlobaux paniersGlobaux = this.GetGlobal(annee, semaine);
            List<LignesPaniersGlobaux> lignesPaniersGlobaux = lignesPaniersGlobauxService.GetLignesPaniersGlobauxByPanierGlobauxIDAndFournisseurID(paniersGlobaux.ID, idFournisseur);

            List<LignesPaniersGlobaux> lignesAgregees = lignesPaniersGlobaux
                            .GroupBy(l => l.IDProduits)
                            .Select(ligneGroupe => new LignesPaniersGlobaux(ligneGroupe.Select(p => p.IDProduits).First(),
                                                                            ligneGroupe.Sum(p => p.Quantite)))
                            .ToList();

            foreach (var ligne in lignesAgregees)
            {
                Produits produits = produitsServices.GetProduitsByID(ligne.IDProduits);

                contentBuilder
                    .Append(produits.Reference)
                    .Append(";")
                    .Append(ligne.Quantite)
                    .Append(";0")
                    .Append("\n");
            }
            //convertit une chaîne C# en tableau d’octets au format Ascii et on place le tableau dans une variable
            byte[] bytes = Encoding.ASCII.GetBytes(contentBuilder.ToString());
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }
        public List<String> GenererPanierString(int annee, int semaine, int idFournisseur)
        {
            StringBuilder contentBuilder = new StringBuilder("reference;quantite;prix unitaire HT\n");

            PaniersGlobaux paniersGlobaux = this.GetGlobal(annee, semaine);
            List<LignesPaniersGlobaux> lignesPaniersGlobaux = lignesPaniersGlobauxService.GetLignesPaniersGlobauxByPanierGlobauxIDAndFournisseurID(paniersGlobaux.ID, idFournisseur);

            List<LignesPaniersGlobaux> lignesAgregees = lignesPaniersGlobaux
                            .GroupBy(l => l.IDProduits)
                            .Select(ligneGroupe => new LignesPaniersGlobaux(ligneGroupe.Select(p => p.IDProduits).First(),
                                                                            ligneGroupe.Sum(p => p.Quantite)))
                            .ToList();
            List<String> liste = new List<String>();
            
            foreach (var ligne in lignesAgregees)
            {
                Produits produits = produitsServices.GetProduitsByID(ligne.IDProduits);

                contentBuilder
                    .Append(produits.Reference)
                    .Append(";")    
                    .Append(ligne.Quantite)
                    .Append(";0")
                    .Append("\n");
                string uneLigne = produits.Reference + "; " + ligne.Quantite + "; 0" + "\n";
                liste.Add(uneLigne);
            }

            return liste;
        }
        public void Cloturer(int pgId)
        {
            PaniersGlobaux paniersGlobaux = GetPaniersGlobauxByID(pgId);
            if (paniersGlobaux.Cloture == false)
            {
                List<Offres> listeOffres = offresService.GetOffreByIDPaniers(pgId);

                List<int> idProduits = listeOffres
                    .Select(offre => offre.IdProduits).Distinct()
                    .ToList();

                foreach (int idProduit in idProduits)
                {
                    float prixMin = listeOffres.Where(offre => offre.IdProduits == idProduit).Select(offre => offre.Prix).Min();

                    Offres offreGagnante =
                        listeOffres
                        .Where(offre => offre.IdProduits == idProduit && offre.Prix == prixMin)
                        .OrderBy(offre => fournisseursService.GetFournisseursByID(offre.IdFournisseurs).DateAdhesion)
                        .First();

                    offreGagnante.Gagne = true;
                    offresService.Update(offreGagnante);
                }
                UpdateCloture(paniersGlobaux);
            }
            else if(paniersGlobaux.Cloture==true)
            {
                throw new Exception($"Le panier avec identifié : {paniersGlobaux.ID} est déjà clôturé.");
            }
        }
        public void GenererListeAchat(int IdAdherent, IFormFile csvFile)
        {

            ProduitsServices produitsServices = new ProduitsServices();

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date = DateTime.Now;
            Calendar cal = dfi.Calendar;

            // récupération du panier global
            PaniersGlobaux paniersGlobaux = this.GetPanierGlobal();

            using (StreamReader reader = new StreamReader(csvFile.OpenReadStream()))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    string reference = values[0];
                    string quantite = values[1];

                    Produits produits = produitsServices.GetByRef(reference);

                    var lignesPaniersGlobaux = new LignesPaniersGlobaux_DAL(produits.ID, 
                                                                            Int32.Parse(quantite), 
                                                                            paniersGlobaux.ID, 
                                                                            IdAdherent);
                    lignesPaniersGlobaux_depot.Insert(lignesPaniersGlobaux);
                }
            }
        }
        public void GenererListeAchatString(int IdAdherent, IEnumerable<String> csvFile)
        {

            ProduitsServices produitsServices = new ProduitsServices();

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date = DateTime.Now;
            Calendar cal = dfi.Calendar;
            PaniersGlobaux paniersGlobaux = this.GetPanierGlobal();

            for (int i = 0; i < csvFile.Count();i++ )
            {
                var liste = csvFile.ElementAt(i).Split(';');
                string reference = liste[0];
                string quantite = liste[1];

                Produits produits = produitsServices.GetByRef(reference);

                var lignesPaniersGlobaux = new LignesPaniersGlobaux_DAL(produits.ID, Int32.Parse(quantite), paniersGlobaux.ID, IdAdherent);
                lignesPaniersGlobaux_depot.Insert(lignesPaniersGlobaux);
            }
        }
        public void LancerEnchere(DateTime debutPeriode, DateTime finPeriode)
        {
            var url = "http://127.0.0.1:8000/lancerEnchere";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            request.ContentType = "application/json";

            var panier = GetPanierGlobal();

            var data = "{\"IdPanier\": " + panier.ID + ", \"debutPeriode\": \""+ debutPeriode + "\", \"finPeriode\": \"" + finPeriode + "\" }";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("La requête a échoué");
            }
        }
    }
}

