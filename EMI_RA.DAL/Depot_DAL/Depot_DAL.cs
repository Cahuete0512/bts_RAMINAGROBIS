using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMI_RA.DAL
{
    public abstract class Depot_DAL<Type_DAL> : IDepot_DAL<Type_DAL>
    {
        public string ChaineDeConnexion { get; set; }

        protected SqlConnection connexion;
        protected SqlCommand commande;

        public Depot_DAL()
        {
            // pour lire la config, on a besoin d'un objet, le "ConfigurationBuilder"
            var builder = new ConfigurationBuilder();
            var config = builder.AddJsonFile("appsettings.json", true, true).Build();

            ChaineDeConnexion = config.GetSection("ConnectionStrings:default").Value;

        }

        protected void CreerConnexionEtCommande()
        {
            connexion = new SqlConnection(ChaineDeConnexion);
            connexion.Open();
            commande = new SqlCommand();
            commande.Connection = connexion;
        }

        protected void DetruireConnexionEtCommande()
        {
            commande.Dispose(); // détruit la partie externe à la mémoire dotnet qui a été utilisée
            connexion.Close();
            connexion.Dispose();
        }

        #region Méthodes abstraites
        public abstract void Delete(Type_DAL item);

        public abstract List<Type_DAL> GetAll();

        public abstract Type_DAL GetByID(int ID);

        public abstract Type_DAL Insert(Type_DAL item);

        public abstract Type_DAL Update(Type_DAL item);
        #endregion
    }
}
