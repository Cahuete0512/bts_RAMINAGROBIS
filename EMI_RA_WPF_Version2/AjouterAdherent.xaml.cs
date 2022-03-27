using EMI_RA.API.Client;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

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
