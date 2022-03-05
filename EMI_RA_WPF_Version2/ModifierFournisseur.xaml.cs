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
    /// Logique d'interaction pour ModifierFournisseur.xaml
    /// </summary>
    public partial class ModifierFournisseur : Page
    {

        public ModifierFournisseur(API.Client.Fournisseurs fournisseur)
        {
            InitializeComponent();
            id.Text = fournisseur.IdFournisseurs.ToString();
            societe.Text = fournisseur.Societe;
            civilite.Text = fournisseur.CiviliteContact;
            nom.Text = fournisseur.NomContact;
            prenom.Text = fournisseur.PrenomContact;
            email.Text = fournisseur.Email;
            adresse.Text = fournisseur.Adresse;



        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            
            var clientApi = new Client("https://localhost:5001/", new HttpClient());

            var fournisseurDTO = new API.Client.Fournisseurs()
            {
                IdFournisseurs = Int32.Parse(id.Text),
                Societe = societe.Text,
                CiviliteContact = civilite.Text,
                NomContact = nom.Text,
                PrenomContact = prenom.Text,
                Email = email.Text,
                Adresse = adresse.Text

            };

            var fournisseur = await clientApi.FournisseursPUTAsync(fournisseurDTO);
            MessageBox.Show("Le fournisseur a été modifié");
        }
    }
}
