using EMI_RA.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EMI_RA
{
    public class FournisseursService : IFournisseursService
    {
        private Fournisseurs_Depot_DAL depot = new Fournisseurs_Depot_DAL();
        private ProduitsServices produitsService = new ProduitsServices();
        private AssoProduitsFournisseursServices assoProduitsFournisseursServices = new AssoProduitsFournisseursServices();

        #region GetAllFournisseurs
        public List<Fournisseurs> GetAllFournisseurs()
        {
            var fournisseurs = depot.GetAll()
                .Select(f => new Fournisseurs(f.IdFournisseurs,
                                              f.Societe,
                                              f.CiviliteContact,
                                              f.NomContact,
                                              f.PrenomContact,
                                              f.Email,
                                              f.Adresse,
                                              f.DateAdhesion,
                                              f.Actif))
                .ToList();
            
            return fournisseurs;
        }
        #endregion

        #region GetFournisseeursById
        public Fournisseurs GetFournisseursByID(int idFournisseurs)
        {
            var f = depot.GetByID(idFournisseurs);

            return new Fournisseurs(f.IdFournisseurs,
                                    f.Societe,
                                    f.CiviliteContact,
                                    f.NomContact,
                                    f.PrenomContact,
                                    f.Email,
                                    f.Adresse,
                                    f.DateAdhesion,
                                    f.Actif);
        }
        #endregion

        #region Insert
        public Fournisseurs Insert(Fournisseurs f)
        {
            var fournisseur = new Fournisseurs_DAL(f.IdFournisseurs,
                                                   f.Societe,
                                                   f.CiviliteContact,
                                                   f.NomContact,
                                                   f.PrenomContact,
                                                   f.Email,
                                                   f.Adresse,
                                                   DateTime.Now,
                                                   f.Actif);
            depot.Insert(fournisseur);

            f.IdFournisseurs = fournisseur.IdFournisseurs;

            return f;
        }
        #endregion

        #region Update
        public Fournisseurs Update(Fournisseurs f)
        {
            var fournisseur = new Fournisseurs_DAL(f.IdFournisseurs,
                                                   f.Societe,
                                                   f.CiviliteContact,
                                                   f.NomContact,
                                                   f.PrenomContact,
                                                   f.Email,
                                                   f.Adresse,
                                                   f.DateAdhesion,
                                                   f.Actif);
            depot.Update(fournisseur);

            return f;
        }
        #endregion

        #region Desactiver
        public void Desactiver(int idFournisseurs)
        {
            Fournisseurs fournisseurs = GetFournisseursByID(idFournisseurs);
            if (fournisseurs.Actif == true)
            {
                List<Produits> listeProduits = produitsService.GetByIdFournisseur(idFournisseurs);

                List<int> idProduits = listeProduits
                    .Select(produit => produit.ID).Distinct()
                    .ToList();


                UpdateFournisseurDesactive(fournisseurs);
            }
            else if (fournisseurs.Actif == false)
            {
                throw new Exception($"Le fournisseur avec l'identifiant : {fournisseurs.IdFournisseurs} est déjà désactivé.");
            }
        }
        #endregion

        #region UpdateFournisseurDesactive
        public Fournisseurs UpdateFournisseurDesactive(Fournisseurs f)
        {
            var fournisseur = new Fournisseurs_DAL(f.IdFournisseurs,
                                                   f.Societe,
                                                   f.CiviliteContact,
                                                   f.NomContact,
                                                   f.PrenomContact,
                                                   f.Email,
                                                   f.Adresse,
                                                   f.DateAdhesion,
                                                   f.Actif);
            depot.UpdateFournisseurDesactive(fournisseur);

            return f;
        }
        #endregion

        #region DeleteFournisseur
        public void Delete(Fournisseurs f)
        {
            var fournisseur = new Fournisseurs_DAL(f.IdFournisseurs,
                                                      f.Societe,
                                                      f.CiviliteContact,
                                                      f.NomContact,
                                                      f.PrenomContact,
                                                      f.Email,
                                                      f.Adresse,
                                                      f.DateAdhesion,
                                                      f.Actif);
            depot.Delete(fournisseur);

        }
        #endregion

        #region AliementerCatalogue
        // TODO : voir pour additionner 2ème méthode qui est la même à celle-ci (juste paramètre qui est modifié, ajouter : IEnumerable<String> csvFile
        public void AlimenterCatalogue(int idFournisseurs, IFormFile csvFile)
        {
            Fournisseurs fournisseurs = this.GetFournisseursByID(idFournisseurs);

            // récupérer les produits en lien avec le fournisseur
            List<Produits> produitsExistantsListe = produitsService.GetByIdFournisseur(idFournisseurs);

            // récupérer les produits du fichier csv
            List<Produits> produitsCsvListe = RecupProduitsCsv(csvFile, idFournisseurs);

            // pour les produits du csv qui n'existent pas en BDD -> création du produit en BDD et de la liaison
            foreach (var produitCsv in produitsCsvListe)
            {
                Produits produitsCorrespondant = null;

                foreach(var produitBdd in produitsExistantsListe)
                {
                    if (produitBdd.Reference.Equals(produitCsv.Reference)){
                        produitsCorrespondant = produitBdd;
                        break;
                    }
                }

                if (produitsCorrespondant == null)
                {
                    Produits produitALier = produitsService.GetByRef(produitCsv.Reference);
                    if (produitALier == null)
                    {
                        produitALier = produitsService.Insert(produitCsv);
                    } else if (!produitALier.Disponible)
                    {
                        // Si le produit n'était pas disponible, on le rend disponible
                        produitALier.Disponible = true;
                        produitsService.Update(produitALier);
                    }
                    // pour les produits qui sont dans le fichier et non en BDD -> création du lien
                    assoProduitsFournisseursServices.Insert(new AssoProduitsFournisseurs(idFournisseurs, produitALier.ID));
                }
            }
            // les produits qui sont dans la BDD et non dans le fichier -> suppression du lien BDD
            foreach (var produitExistant in produitsExistantsListe)
            {
                bool exists = false;
                foreach (var produitCsv in produitsCsvListe)
                {
                    if (produitCsv.Reference.Equals(produitExistant.Reference))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    assoProduitsFournisseursServices.Delete(produitExistant.ID, idFournisseurs);
                    List<AssoProduitsFournisseurs> assoProduitsFournisseursListe = assoProduitsFournisseursServices.GetByIdProduit(produitExistant.ID);
                    if(assoProduitsFournisseursListe.Count == 0)
                    {
                        produitExistant.Disponible = false;
                        produitsService.Update(produitExistant);
                    }
                }
            }
        }
        #endregion

        #region AlimenterCatalogueStringCSV
        public void AlimenterCatalogueStringCSV(int idFournisseurs, IEnumerable<String> csvFile)
        {
            Fournisseurs fournisseurs = this.GetFournisseursByID(idFournisseurs);

            // récupérer les produits en lien avec le fournisseur
            List<Produits> produitsExistantsListe = produitsService.GetByIdFournisseur(idFournisseurs);

            // récupérer les produits du fichier csv
            List<Produits> produitsCsvListe = RecupProduitsCsvString(csvFile, idFournisseurs);

            // pour les produits du csv qui n'existent pas en BDD -> création du produit en BDD et de la liaison
            foreach (var produitCsv in produitsCsvListe)
            {
                Produits produitsCorrespondant = null;

                foreach (var produitBdd in produitsExistantsListe)
                {
                    if (produitBdd.Reference.Equals(produitCsv.Reference))
                    {
                        produitsCorrespondant = produitBdd;
                        break;
                    }
                }

                if (produitsCorrespondant == null)
                {
                    Produits produitALier = produitsService.GetByRef(produitCsv.Reference);
                    if (produitALier == null)
                    {
                        produitALier = produitsService.Insert(produitCsv);
                    }
                    else if (!produitALier.Disponible)
                    {
                        // Si le produit n'était pas disponible, on le rend disponible
                        produitALier.Disponible = true;
                        produitsService.Update(produitALier);
                    }
                    // pour les produits qui sont dans le fichier et non en BDD -> création du lien
                    assoProduitsFournisseursServices.Insert(new AssoProduitsFournisseurs(idFournisseurs, produitALier.ID));
                }
            }
            // les produits qui sont dans la BDD et non dans le fichier -> suppression du lien BDD
            foreach (var produitExistant in produitsExistantsListe)
            {
                bool exists = false;

                foreach (var produitCsv in produitsCsvListe)
                {
                    if (produitCsv.Reference.Equals(produitExistant.Reference))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    assoProduitsFournisseursServices.Delete(produitExistant.ID, idFournisseurs);
                    List<AssoProduitsFournisseurs> assoProduitsFournisseursListe = assoProduitsFournisseursServices.GetByIdProduit(produitExistant.ID);
                    if (assoProduitsFournisseursListe.Count == 0)
                    {
                        produitExistant.Disponible = false;
                        produitsService.Update(produitExistant);
                    }
                }
            }
        }
        #endregion

        #region RecupProduitsCsv
        private List<Produits> RecupProduitsCsv(IFormFile csvFile, int idFournisseurs)
        {
            List<Produits> produitsListe = new List<Produits>();
            using (StreamReader reader = new StreamReader(csvFile.OpenReadStream()))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    string reference = values[0];
                    string libelle = values[1];
                    string marque = values[2];

                    Produits produits = new Produits(libelle, marque, reference, true);
                    produitsListe.Add(produits);
                }
            }

            return produitsListe;
        }
        #endregion 

        #region RecupProduitsCsvString
        private List<Produits> RecupProduitsCsvString(IEnumerable<String> csvFile, int idFournisseurs)
        {
            List<Produits> produitsListe = new List<Produits>();

            for (int i = 0; i < csvFile.Count(); i++)
            {
                var liste = csvFile.ElementAt(i).Split(';');
                string reference = liste[0];
                string libelle = liste[1];
                string marque = liste[2];

                Produits produits = new Produits(libelle, marque, reference, true);
                produitsListe.Add(produits);
            }

            return produitsListe;
        }
        #endregion
    }
}
