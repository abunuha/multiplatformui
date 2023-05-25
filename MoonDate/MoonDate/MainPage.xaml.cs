using System.Globalization;

namespace MoonDate;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
        Loaded += MainPage_Loaded;
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

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

