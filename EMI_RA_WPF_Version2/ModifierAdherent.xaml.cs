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
    /// Logique d'interaction pour ModifierAdherent.xaml
    /// </summary>
    public partial class ModifierAdherent : Page
    {

        public ModifierAdherent(API.Client.Adherents adherent)
        {
            InitializeComponent();
            id.Text = adherent.Id.ToString();
            societe.Text = adherent.Societe;
            civilite.Text = adherent.CiviliteContact;
            nom.Text = adherent.NomContact;
            prenom.Text = adherent.PrenomContact;
            email.Text = adherent.Email;
            adresse.Text = adherent.Adresse;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            var adherentDTO = new API.Client.Adherents()
            {
                Id = Int32.Parse(id.Text),
                Societe = societe.Text,
                CiviliteContact = civilite.Text,
                NomContact = nom.Text,
                PrenomContact = prenom.Text,
                Email = email.Text,
                Adresse = adresse.Text

            };
            var adherent = await clientApi.AdherentsPUTAsync(adherentDTO);
            MessageBox.Show("L'adhérent a été modifié");


        }
    }
}
