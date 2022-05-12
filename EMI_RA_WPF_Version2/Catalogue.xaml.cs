using EMI_RA.API.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMI_RA_WPF
{
    /// <summary>
    /// Logique d'interaction pour Catalogue.xaml
    /// </summary>
    public partial class Catalogue : Page
    {
        Fournisseurs fournisseur;

        #region Catalogue
        public Catalogue(EMI_RA.API.Client.Fournisseurs unfournisseur)
        {
            InitializeComponent();
            fournisseur = unfournisseur;
        }
        #endregion

        #region Button_Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";
            opfd.ShowDialog();
            var liste = File.ReadAllText(opfd.FileName);

            var fichiercsv = File.ReadLines(opfd.FileName);
            List<string> fichier = fichiercsv.Skip(1).Take(fichiercsv.Count() - 1).ToList();

            for (int i = 1; i < fichiercsv.Count(); i++)
            {
                fichier.ToList().Add(fichiercsv.ElementAt(i));
            }
            var clientApi = new Client("https://localhost:5001/", new HttpClient());
            var commande = clientApi.CatalogueStringCSVAsync(fournisseur.IdFournisseurs, fichier);
        }
        #endregion
    }
}
