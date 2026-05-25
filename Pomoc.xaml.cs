namespace KalkulatorWalut2;

public partial class Pomoc : ContentPage
{
	public Pomoc()
	{
		InitializeComponent();
	}

    private void Grid_Loaded(object sender, EventArgs e)
    {
        string kodWaluty = Preferences.Default.Get("LastWalutaWejscie", "PLN");

        
        komHej.Text = $"--Hej, {kodWaluty}";
    }
}