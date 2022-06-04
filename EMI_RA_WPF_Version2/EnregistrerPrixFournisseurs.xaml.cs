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
    /// Logique d'interaction pour EnregistrerPrixFournisseurs.xaml
    /// </summary>
    public partial class EnregistrerPrixFournisseurs : Page
    {
        Fournisseurs fournisseur;

        #region EnregistrerPrixFournisseurs
        public EnregistrerPrixFournisseurs(EMI_RA.API.Client.Fournisseurs unfournisseur)
        {
            InitializeComponent();
            fournisseur = unfournisseur;
        }
        #endregion

        #region Button_Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Panier";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Text documents (.csv)|*.csv";
            string fileText = "reference ;quantite ;prix unitaire HT";
            Nullable<bool> result = dlg.ShowDialog();


            var clientApi = new Client("https://localhost:5001/", new HttpClient());
            var lefounisseur = clientApi.PanierAllAsync(fournisseur.IdFournisseurs);


            var list = lefounisseur.Result;
            string liste2 = "reference ;quantite ;prix unitaire HT ;\n";
            for (int i = 0; i < list.Count(); i++)
            {
                liste2 = liste2 + list.ElementAt(i);
            }

            if (result == true)
            {
                File.WriteAllText(dlg.FileName, liste2);
            }
        }
        #endregion

        #region Button_Click_1
        private void Button_Click_1(object sender, RoutedEventArgs e)
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
            var commande = clientApi.Offres2Async(fournisseur.IdFournisseurs, fichier);
        }
        #endregion
    }
}
