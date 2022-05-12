using EMI_RA.API.Client;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour Fournisseurs.xaml
    /// </summary>
    public partial class Panier : Page
    {
        #region
        public Panier()
        {
            InitializeComponent();
        }
        #endregion

        #region Window_Loaded
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Ca serait mieux de mettre l'URL dans un fichier de config plutôt qu'en dur ici
            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            //le async et le await = programmation asynchrone en C#
            var panier = await clientApi.PaniersGlobauxAsync();

            liste.ItemsSource = panier;
        }
        #endregion

    }
}
