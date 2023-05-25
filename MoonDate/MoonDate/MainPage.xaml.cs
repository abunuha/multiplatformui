using System.Globalization;

namespace MoonDate;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
        Loaded += MainPage_Loaded;
        Quit.Clicked += Quit_Clicked;
	}

    private void Quit_Clicked(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
		MoonPhaseImage.Source = MoonPhase.GetCurrentMoonPhaseImage();
        
		String hijriMonth = null, startDateStr = null;
        MoonPhase.GetHijriMonthStart(ref hijriMonth, ref startDateStr);

		//String[] items = startDateStr.Split("-");

		DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", CultureInfo.CurrentCulture);
		

		TimeSpan timeSpan = DateTime.Now - startDate;

		HijriDateLabel.Text = "Hijri Date : " + hijriMonth + " " + (timeSpan.Days + 1);

		DateTime lastNewMoonDate = MoonPhase.GetLastNewMoonTime();
		LastNewMoonLabel.Text = "Last New Moon : " + lastNewMoonDate.ToString();

		NextNewMoonLabel.Text = "Next New Moon : " + MoonPhase.GetNextNewMoonTime(lastNewMoonDate).ToString();
    }

    
}

