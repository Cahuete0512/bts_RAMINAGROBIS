using EMI_RA.API.Client;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace EMI_RA_WPF
{
    public partial class CloturerPanier : Page
    {
        PaniersGlobaux panier;
        public CloturerPanier(PaniersGlobaux unPanier)
        {
            InitializeComponent();
            panier = unPanier;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var clientApi = new Client("https://localhost:5001/", new HttpClient());
            var cloturerPanier = clientApi.CloturerAsync(panier.Id);
        }
    }
}
