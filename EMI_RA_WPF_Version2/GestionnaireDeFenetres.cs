using EMI_RA_WPF;

namespace EMI_RA.WPF
{
    public static class GestionnaireDeFenetres
    {

        public static Fournisseurs? Fournisseurs { get; set; }
        public static Adherents? Adherents { get; set; }
        public static ajouterAdherent? ajouterAdherent { get; set; }
        public static ModifierAdherent? modifierAdherent { get; set; }
        public static ModifierFournisseur? modifierFournisseur { get; set; }
        public static AjouterFournisseurs? AjouterFournisseurs { get; set; }
        public static Commande? Commande { get; set; }
        public static EnregistrerPrixFournisseurs? EnregistrerPrixFournisseurs { get; set; }
        public static Panier? Panier { get; set; }
        public static CloturerPanier? CloturerPanier { get; set; }
        public static Catalogue? Catalogue { get; set; }
        public static VoirItemsPanier? voirItemsPanier { get; set; }
        public static PageParDefault? PageParDefault { get; set; }

    }
}
