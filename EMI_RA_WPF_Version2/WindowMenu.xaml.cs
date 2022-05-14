using EMI_RA.API.Client;
using EMI_RA_WPF;
using System.Net.Http;
using System.Windows;

namespace EMI_RA.WPF
{
    /// <summary>
    /// Logique d'interaction pour WindowMenu.xaml
    /// </summary>
    public partial class WindowMenu : Window
    {
        #region WindowMenu
        public WindowMenu()
        {
            InitializeComponent();
            GestionnaireDeFenetres.PageParDefault = new EMI_RA_WPF.PageParDefault();
            Main.Content = GestionnaireDeFenetres.PageParDefault;
        }
        #endregion

        #region MenuItemVoirAdherents_click
        private void MenuItemVoirAdherents_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Adherents == null)
            {
                GestionnaireDeFenetres.Adherents = new Adherents();
            }
            Main.Navigate(GestionnaireDeFenetres.Adherents);
        }
        #endregion

        #region MenuItemAjouterAdherent_click
        private void MenuItemAjouterAdherent_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.ajouterAdherent == null)
            {
                GestionnaireDeFenetres.ajouterAdherent = new ajouterAdherent();
            }

            Main.Navigate(GestionnaireDeFenetres.ajouterAdherent);

        }
        #endregion

        #region MenuItemModifierAdherent_click
        private void MenuItemModifierAdherent_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Adherents == null || GestionnaireDeFenetres.Adherents.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un adhérent dans la liste");
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
        #endregion

        #region  MenuItemVoirFournisseurs_click
        private void MenuItemVoirFournisseurs_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null)
            {
                GestionnaireDeFenetres.Fournisseurs = new Fournisseurs();
            }

            Main.Navigate(GestionnaireDeFenetres.Fournisseurs);
        }
        #endregion

        #region MenuItemAjouterFournisseurs_click
        private void MenuItemAjouterFournisseurs_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.AjouterFournisseurs == null)
            {
                GestionnaireDeFenetres.AjouterFournisseurs = new AjouterFournisseurs();
            }

            Main.Navigate(GestionnaireDeFenetres.AjouterFournisseurs);
        }
        #endregion

        #region MenuItemModifierFournisseurs_click
        private void MenuItemModifierFournisseurs_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null || GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un fournisseur dans la liste");
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
        #endregion

        #region MenuItemVoirPanier_click
        private void MenuItemVoirPanier_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Panier == null)
            {
                GestionnaireDeFenetres.Panier = new Panier();
            }
            Main.Navigate(GestionnaireDeFenetres.Panier);

        }
        #endregion

        #region MenuItemModifierCommande_click
        private void MenuItemModifierCommande_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Adherents == null || GestionnaireDeFenetres.Adherents.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un adherent dans la liste");
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
        #endregion

        #region MenuItemEnregistrerPrix_click
        private void MenuItemEnregistrerPrix_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null || GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un fournisseur dans la liste");
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
        #endregion

        #region MenuItemCatalogue_click
        private void MenuItemCatalogue_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Fournisseurs == null || GestionnaireDeFenetres.Fournisseurs.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un fournisseur dans la liste");
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
        #endregion

        #region MenuItemVoirPanierSelectionne_click
        private void MenuItemVoirPanierSelectionne_click(object sender, RoutedEventArgs e)
        {
            if (GestionnaireDeFenetres.Panier == null || GestionnaireDeFenetres.Panier.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un panier dans la liste");
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
        #endregion

        #region MenuItemLancerEnchereSelectionne_click
        private void MenuItemLancerEnchereSelectionne_click(object sender, RoutedEventArgs e)
        {

                GestionnaireDeFenetres.LancerEnchere = new LancerEnchere();

                Main.Navigate(GestionnaireDeFenetres.LancerEnchere);

        }
        #endregion

        #region MenuItemCloturerPanierSelectionne_click
        private void MenuItemCloturerPanierSelectionne_click(object sender, RoutedEventArgs e)
        {

            if (GestionnaireDeFenetres.Panier == null || GestionnaireDeFenetres.Panier.liste.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un panier dans la liste");
            }
            else
            {
                PaniersGlobaux paniers;
                paniers = (PaniersGlobaux)GestionnaireDeFenetres.Panier.liste.SelectedItem;
                if (paniers.Cloture)
                {
                    MessageBox.Show("Ce panier est déjà clôturé");
                }
                else if (!paniers.Cloture)
                {
                    var clientApi = new Client("https://localhost:5001/", new HttpClient());
                    var cloturerPanier = clientApi.CloturerAsync(paniers.Id);

                    MessageBox.Show("Le panier a été cloturé");
                }
            }
        }
        #endregion
    }
}
