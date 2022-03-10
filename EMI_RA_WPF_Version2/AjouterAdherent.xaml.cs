using EMI_RA.API.Client;
using EMI_RA.DTO;
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
    /// Logique d'interaction pour ajouterAdherent.xaml
    /// </summary>
    public partial class ajouterAdherent : Page
    {
        String Societe = "";
        String CiviliteContact = "";
        String NomContact = "";
        String PrenomContact = "";
        String Email = "";
        String Adresse = "";

        public ajouterAdherent()
        {
            InitializeComponent();
            

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Societe = societe.Text;
            CiviliteContact = civilite.Text;
            NomContact = nom.Text;
            PrenomContact = prenom.Text;
            Email = email.Text;
            Adresse = adresse.Text;


            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            var adherentDTO = new EMI_RA.API.Client.Adherents()
            {

                Societe = Societe,
                CiviliteContact = CiviliteContact,
                NomContact = NomContact,
                PrenomContact = PrenomContact,
                Email = Email,
                Adresse = Adresse

            };
            var adherent = await clientApi.AdherentsPOSTAsync(adherentDTO);
            MessageBox.Show("L'adhérent a été enregistré");


        }

      
    }
}
