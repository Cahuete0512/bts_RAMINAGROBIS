using EMI_RA.API.Client;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace EMI_RA_WPF
{
    /// <summary>
    /// Logique d'interaction pour LancerEnchere.xaml
    /// </summary>
    public partial class LancerEnchere : System.Windows.Controls.Page
    {

        public LancerEnchere()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var periodeDebut = OuvertureEnchere.SelectedDates[0];
            var periodeFin = OuvertureEnchere.SelectedDates[OuvertureEnchere.SelectedDates.Count-1];

            if(OuvertureEnchere.SelectedDates.Count == 7) { 
            var clientapi = new Client("https://localhost:5001/", new HttpClient());
            await clientapi.LancerEnchereAsync(periodeDebut, periodeFin);
            MessageBox.Show($"la période d'enchère est validée, le mail est envoyé");
            }
            else
            {
                MessageBox.Show($"la période d'enchère est non-valide, vous devez sélectionner sept jours");
            }
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                DateTime date = calendar.SelectedDate.Value;
                this.Title = date.ToShortDateString();
            }
        }
    }
}
