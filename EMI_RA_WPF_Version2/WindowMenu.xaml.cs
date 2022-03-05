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
using System.Windows.Shapes;

namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour WindowMenu.xaml
    /// </summary>
    public partial class WindowMenu : Window
    {
        public WindowMenu()
        {
            InitializeComponent();
            GestionnaireDeFenetres.PageParDefault = new EMI_RA_WPF.PageParDefault();
            Main.Content = GestionnaireDeFenetres.PageParDefault;
        }

        private void MenuItemVoirAdherents_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Adherents == null)
            {
                GestionnaireDeFenetres.Adherents = new Adherents();
            }
            Main.Navigate(GestionnaireDeFenetres.Adherents);
        }

        private void MenuItemAjouterAdherent_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.ajouterAdhérent == null)
            {
                GestionnaireDeFenetres.ajouterAdhérent = new AjouterAdhérent();
            }
            //w.Show();
            // MessageBox.Show("hello");
            Main.Navigate(GestionnaireDeFenetres.ajouterAdhérent);

        }

        private void MenuItemModifierAdherent_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Adherents == null || GestionnaireDeFenetres.Adherents.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un adhérent dans la liste");
            }
            else
            {
                if (GestionnaireDeFenetres.modifierAdherent == null)
                {
                    GestionnaireDeFenetres.modifierAdherent = new ModifierAdherent((API.Client.Adherents)GestionnaireDeFenetres.Adherents.liste.SelectedItem);
                }

                Main.Navigate(GestionnaireDeFenetres.modifierAdherent);
            }
        }

        private void MenuItemVoirFournisseurs_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null)
            {
                GestionnaireDeFenetres.Fournisseurs = new Fournisseurs();
            }
            //w.Show();
            // MessageBox.Show("hello");
            Main.Navigate(GestionnaireDeFenetres.Fournisseurs);
        }

        private void MenuItemAjouterFournisseurs_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.AjouterFournisseurs == null)
            {
                GestionnaireDeFenetres.AjouterFournisseurs = new AjouterFournisseurs();
            }

            Main.Navigate(GestionnaireDeFenetres.AjouterFournisseurs);
        }

        private void MenuItemModifierFournisseurs_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null || GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un fournisseur dans la liste");
            }
            else
            {
                if (GestionnaireDeFenetres.modifierFournisseur == null)
                {
                    GestionnaireDeFenetres.modifierFournisseur = new ModifierFournisseur((API.Client.Fournisseurs)GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem);
                }

                Main.Navigate(GestionnaireDeFenetres.modifierFournisseur);

            }
        }

        private void MenuItemVoirPanier_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Panier == null)
            {
                GestionnaireDeFenetres.Panier = new Panier();
            }
            Main.Navigate(GestionnaireDeFenetres.Panier);

        }

        private void MenuItemModifierCommande_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Adherents == null || GestionnaireDeFenetres.Adherents.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un adherent dans la liste");
            }
            else
            {
                if (GestionnaireDeFenetres.Commande == null)
                {
                    GestionnaireDeFenetres.Commande = new EMI_RA.WPF.Commande((API.Client.Adherents)GestionnaireDeFenetres.Adherents.liste.SelectedItem);
                }

                Main.Navigate(GestionnaireDeFenetres.Commande);
            }
           

        }
        
        private void MenuItemEnregistrerPrix_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null || GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un fournisseur dans la liste");
            }
            else
            {
                if (GestionnaireDeFenetres.EnregistrerPrixFournisseurs == null)
                {
                    GestionnaireDeFenetres.EnregistrerPrixFournisseurs = new EMI_RA_WPF.EnregistrerPrixFournisseurs((API.Client.Fournisseurs)GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem);
                }

                Main.Navigate(GestionnaireDeFenetres.EnregistrerPrixFournisseurs);
            }
           

        }
        private void MenuItemCatalogue_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null || GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un fournisseur dans la liste");
            }
            else
            {
                if (GestionnaireDeFenetres.Catalogue == null)
                {
                    GestionnaireDeFenetres.Catalogue = new EMI_RA_WPF.Catalogue((API.Client.Fournisseurs)GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem);
                }

                Main.Navigate(GestionnaireDeFenetres.Catalogue);
            }


        }
        private void MenuItemVoirPanierSelectionne_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Panier == null || GestionnaireDeFenetres.Panier.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un panier dans la liste");
            }
            else
            {
                if (GestionnaireDeFenetres.voirItemsPanier == null)
                {
                    GestionnaireDeFenetres.voirItemsPanier = new EMI_RA.WPF.VoirItemsPanier((API.Client.PaniersGlobaux)GestionnaireDeFenetres.Panier.liste.SelectedItem);
                }

                Main.Navigate(GestionnaireDeFenetres.voirItemsPanier);
            }


        }
        
        private void MenuItemCloturerPanierSelectionne_click(object sender, RoutedEventArgs e)
        {

            if (GestionnaireDeFenetres.Panier == null || GestionnaireDeFenetres.Panier.liste.SelectedItem == null)
            {
                MessageBox.Show("Voyez selectionner un panier dans la liste");
            }
            else
            {
                PaniersGlobaux paniers;
                paniers = (PaniersGlobaux)GestionnaireDeFenetres.Panier.liste.SelectedItem;

                var clientApi = new Client("https://localhost:5001/", new HttpClient());
                var cloturerPanier = clientApi.CloturerAsync(paniers.Id);

                MessageBox.Show("Le panier a été cloturé");

            }


        }

    }
}
