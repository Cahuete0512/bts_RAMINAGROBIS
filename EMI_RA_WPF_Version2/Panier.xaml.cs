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
        public Panier()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Ca serait mieux de mettre l'URL dans un fichier de config plutôt qu'en dur ici
            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            //le async et le await c'est de la programmation asynchrone en C#
            var panier = await clientApi.PaniersGlobauxAsync();

            liste.ItemsSource = panier;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void liste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
