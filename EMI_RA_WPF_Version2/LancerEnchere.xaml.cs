using EMI_RA.API.Client;
using System;
using System.Globalization;
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
        #region LancerEnchere
        public LancerEnchere()
        {
            InitializeComponent();
        }
        #endregion

        #region Button_Click
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime periodeDebut = OuvertureEnchere.SelectedDates[0];
            DateTime periodeFin = OuvertureEnchere.SelectedDates[OuvertureEnchere.SelectedDates.Count-1];

            int jourDeSemaineDebut = (int)periodeDebut.DayOfWeek;
            int jourDeSemaineFin = (int)periodeFin.DayOfWeek;
            periodeFin = periodeFin.AddDays(1);

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;

            int weekOfYearDebut = cal.GetWeekOfYear(periodeDebut, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int weekOfYearNow = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            if ((periodeFin - periodeDebut).TotalDays <= 7
                && jourDeSemaineFin >= jourDeSemaineDebut
                && periodeDebut.Date >= DateTime.Now.Date
                && weekOfYearDebut == weekOfYearNow
                && jourDeSemaineFin != 0)
            {
                var clientapi = new Client("https://localhost:5001/", new HttpClient());

                await clientapi.LancerEnchereAsync(periodeDebut, periodeFin);

                MessageBox.Show($"la période d'enchère est validée, le mail est envoyé");
            }
            else
            {
                MessageBox.Show($"la période d'enchère est non-valide, il faut que votre sélection soit comprise dans la semaine en cours, ne doit pas inclure le dimanche et ne doit pas dépasser 6 jours");
            }
        }
        #endregion

        #region Calendar_SelectedDatesChanged
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as System.Windows.Controls.Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                DateTime date = calendar.SelectedDate.Value;
                this.Title = date.ToShortDateString();
            }
        }
        #endregion
    }
}
