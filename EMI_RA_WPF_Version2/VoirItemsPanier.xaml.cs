using EMI_RA.API.Client;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour Fournisseurs.xaml
    /// </summary>
    public partial class VoirItemsPanier: Page
    {
        PaniersGlobaux panier;
        public VoirItemsPanier(PaniersGlobaux lepanier)
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
