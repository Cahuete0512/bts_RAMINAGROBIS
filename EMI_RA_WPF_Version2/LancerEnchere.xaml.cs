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

        public LancerEnchere()
        {
            InitializeComponent();
        }
        #region Button_Click
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime periodeDebut = OuvertureEnchere.SelectedDates[0];
            DateTime periodeFin = OuvertureEnchere.SelectedDates[OuvertureEnchere.SelectedDates.Count-1];

            int jourDeSemaineDebut = (int)periodeDebut.DayOfWeek;
            int jourDeSemaineFin = (int)periodeFin.DayOfWeek;

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;

            int weekOfYearDebut = cal.GetWeekOfYear(periodeDebut, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int weekOfYearNow = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            periodeFin = periodeFin.AddDays(1);

            if ((periodeFin - periodeDebut).TotalDays <= 6
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
                MessageBox.Show($"la période d'enchère est non-valide, vous devez sélectionner six jours maximum");
            }
        }
        #endregion

        #region Calendar_SelectedDatesChanged
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as System.Windows.Controls.Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                SelectedDateTextBox.Text = calendar.SelectedDate.ToString();
              
            }
        }
        #endregion
    }
}
