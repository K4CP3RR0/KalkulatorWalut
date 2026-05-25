using KalkulatorWalut2.Models;
using KalkulatorWalut2.Helpers;
using System.Linq;
namespace KalkulatorWalut2
{
    public partial class MainPage : ContentPage
    {
        
        List<PozycjaTabA> kursyWalut = new();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, EventArgs e)
        {
            
            try
            {
                var klient = new HttpClient();
                var dane = await klient.GetStringAsync(new Uri(ApiNBP.daneTabA));

                var tabA = XmlManager.DeserializujXML<TabelaKursow>(dane);
                kursyWalut = tabA.pozycje;

                kursyWalut.Insert(0, new PozycjaTabA()
                {
                    KodWaluty = "PLN",
                    NazwaWaluty = "Polski Złoty",
                    Przelicznik = "1",
                    KursSredni = "1,000"
                });
                CVWalutaWejscie.ItemsSource = kursyWalut;
                CVWalutaWyjscie.ItemsSource = kursyWalut;

                //Zadanie 2
                string lastWalutaWejscie = Preferences.Default.Get("LastWalutaWejscie","PLN");
                string lastWalutaWyjscie = Preferences.Default.Get("LastWalutaWyjscie","PLN");
                CVWalutaWejscie.SelectedItem = kursyWalut.FirstOrDefault(x => x.KodWaluty == lastWalutaWejscie);
                CVWalutaWyjscie.SelectedItem = kursyWalut.FirstOrDefault(x => x.KodWaluty == lastWalutaWyjscie);

                //Zadanie 6
                
                string obecnaData = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                string sciezkaPliku = Path.Combine(FileSystem.AppDataDirectory, "data_aktualizacji.txt");
                File.WriteAllText(sciezkaPliku, obecnaData);
                lblDataAktualizacji.Text = $"Ostatnia aktualizacja (lokalnie): {obecnaData}";
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się pobrać danych NBP\n\nSpróbuj ponownie później", "OK");
                
            }
        }
        void Przelicz()
        {
            if (CVWalutaWejscie.SelectedItem == null || CVWalutaWyjscie.SelectedItem == null || entryKwota.Text == "") return;
            
            var zWaluty = CVWalutaWejscie.SelectedItem as PozycjaTabA;
            var naWalute = CVWalutaWyjscie.SelectedItem as PozycjaTabA;
            decimal kwota;
                if (decimal.TryParse(entryKwota.Text, out kwota))
                {
                    decimal naPLN = kwota * zWaluty.Kurs;
                    decimal wynik = naPLN / naWalute.Kurs;
                    lblKwota.Text = string.Format($"{wynik:f2} {naWalute.KodWaluty}");
                }
                else
                {
                    entryKwota.Text = "";
                }
            }
        
        private void entryKwota_TextChanged(object sender, TextChangedEventArgs e)
        {
            Przelicz();
        }

        private void CVWalutaWejscie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Przelicz();
            
            //Zadanie 2
            var zWaluty = CVWalutaWejscie.SelectedItem as PozycjaTabA;
            var naWalute = CVWalutaWyjscie.SelectedItem as PozycjaTabA;
            if (zWaluty != null)
                Preferences.Default.Set("LastWalutaWejscie", zWaluty.KodWaluty);
            if (naWalute != null)
                Preferences.Default.Set("LastWalutaWyjscie", naWalute.KodWaluty);
        }

        private void ButtonOProgramie_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OProgramie());

        }

        private void ButtonPomoc_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pomoc());
        }
    }
   
}
