using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using EMI_RA.API.Client;
using System.Net.Http;


namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour Commande.xaml
    /// </summary>
    public partial class Commande : System.Windows.Controls.Page
    {
        EMI_RA.API.Client.Adherents adherent;

        #region Commande
        public Commande(EMI_RA.API.Client.Adherents unAdherent)
        {
            InitializeComponent();
            adherent = unAdherent;
        }
        #endregion

        #region Choisir_Click
        private void Choisir_Click(object sender, RoutedEventArgs e)
        {

            string ligne;
            int compteur = 0;

            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";
            opfd.ShowDialog();
            var liste = File.ReadAllText(opfd.FileName);
            var fichiercsv = File.ReadLines(opfd.FileName);
            List<string> fichier = fichiercsv.Skip(1).Take(fichiercsv.Count()-1).ToList();

            for (int i = 1; i < fichiercsv.Count(); i++)
            {
               fichier.ToList().Add(fichiercsv.ElementAt(i));
            }
            
            var clientApi = new Client("https://localhost:44313/", new HttpClient());
            var commande = clientApi.CommandeVersion2Async(adherent.Id, fichier);
        }
        #endregion
    }
}
