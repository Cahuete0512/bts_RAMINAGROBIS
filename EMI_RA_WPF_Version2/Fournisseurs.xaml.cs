using EMI_RA.API.Client;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace EMI_RA.WPF
{
    public partial class Fournisseurs : Page
    {
        #region Fournisseurs
        public Fournisseurs()
        {
            InitializeComponent();
        }
        #endregion

        #region Window_Loaded
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            var fournisseurs = await clientApi.FournisseursAllAsync();

            liste.ItemsSource = fournisseurs;
        }
        #endregion
    }
}
