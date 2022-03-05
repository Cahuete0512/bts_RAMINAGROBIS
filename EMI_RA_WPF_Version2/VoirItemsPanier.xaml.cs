using EMI_RA.API.Client;
using System;
using System.Collections.Generic;
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

namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour Fournisseurs.xaml
    /// </summary>
    public partial class VoirItemsPanier: Page
    {
        PaniersGlobaux panier;
        public VoirItemsPanier(API.Client.PaniersGlobaux lepanier)
        {
            InitializeComponent();
            panier = lepanier;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            var items = await clientApi.MeilleursPrixAsync(panier.Id);

            liste.ItemsSource = items;

        }

    }
}
