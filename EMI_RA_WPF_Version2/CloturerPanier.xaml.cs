using EMI_RA.API.Client;
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
