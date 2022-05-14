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

        #region Depot_DAL
        public Depot_DAL()
        {
            // pour lire la config, on a besoin d'un objet, le "ConfigurationBuilder"
            var builder = new ConfigurationBuilder();
            var config = builder.AddJsonFile("appsettings.json", true, true).Build();

            ChaineDeConnexion = config.GetSection("ConnectionStrings:default").Value;

        }
        #endregion

        #region CreerConnexionEtCommande
        protected void CreerConnexionEtCommande()
        {
            connexion = new SqlConnection(ChaineDeConnexion);
            connexion.Open();
            commande = new SqlCommand();
            commande.Connection = connexion;
        }
        #endregion

        #region DetruireConnexionEtCommande
        protected void DetruireConnexionEtCommande()
        {
            // détruit la partie externe à la mémoire dotnet qui a été utilisée
            commande.Dispose(); 
            connexion.Close();
            connexion.Dispose();
        }
        #endregion

        #region Méthodes abstraites
        public abstract void Delete(Type_DAL item);

        public abstract List<Type_DAL> GetAll();

        public abstract Type_DAL GetByID(int ID);

        public abstract Type_DAL Insert(Type_DAL item);

        public abstract Type_DAL Update(Type_DAL item);
        #endregion
    }
}
