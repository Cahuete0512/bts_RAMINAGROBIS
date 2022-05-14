using EMI_RA.API.Client;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour ModifierFournisseur.xaml
    /// </summary>
    public partial class ModifierFournisseur : Page
    {
        #region ModifierFournisseur
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
            actif.IsChecked = fournisseur.Actif;
        }
        #endregion

        #region Button_Click
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
                Adresse = adresse.Text,
                Actif = (bool)actif.IsChecked,
            };

            var fournisseur = await clientApi.FournisseursPUTAsync(fournisseurDTO);
            MessageBox.Show("Le fournisseur a été modifié");
            if (actif.IsChecked == false)
            {
                MessageBox.Show("Le fournisseur a été désactivé");
            }
            else
            {
                MessageBox.Show("Le fournisseur est actif");
            }
        }
        #endregion
    }
}
