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
            //HttpClient httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("https://localhost:5001/");


            //HttpContent content = null;
            //HttpResponseMessage httpResponse = httpClient.PostAsync("lancerEnchere", content).Result;

            var periodeDebut = OuvertureEnchere.SelectedDates[0];
            var periodeFin = OuvertureEnchere.SelectedDates[OuvertureEnchere.SelectedDates.Count-1];

            var clientapi = new Client("https://localhost:5001/", new HttpClient());
            await clientapi.LancerEnchereAsync(periodeDebut, periodeFin);
            MessageBox.Show($"la période d'enchère est validée, le mail est envoyé");

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
