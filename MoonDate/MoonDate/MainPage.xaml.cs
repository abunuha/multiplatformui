using System.Globalization;

namespace MoonDate;

public partial class MainPage : ContentPage
{
	int count = 0;

	public String hijriMonth;

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
		//Location loc = null;


		bool isDebugMode = System.Diagnostics.Debugger.IsAttached;




		//MoonPhaseImage.Source = MoonPhase.GetCurrentMoonPhaseImage();

		Boolean useWebView = true;

		String mainPageHtml = GitHubPages.GetMainPageHtml();

		MoonPhaseImage.Source = MoonDate.App.moonImage;

		mainPageHtml = mainPageHtml.Replace("MOON_IMAGE", MoonDate.App.moonImage);

        
		String startDateStr = null;
		String lastNewMoonStr = null;
        MoonPhase.GetHijriMonthStart(ref hijriMonth, ref startDateStr, ref lastNewMoonStr);

        //String[] items = startDateStr.Split("-");

        //DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", CultureInfo.CurrentCulture);
        DateTime startDate = DateTime.ParseExact(startDateStr, "MM dd yyyy HH mm", CultureInfo.CurrentCulture);
		DateTime lastNewMoonDate = DateTime.ParseExact(lastNewMoonStr, "MM dd yyyy HH mm", CultureInfo.CurrentCulture);

        TimeSpan timeSpan = DateTime.Now - startDate;

		if(!useWebView) HijriDateLabel.Text = "Hijri Date : " + hijriMonth + " " + (timeSpan.Days + 1);
		else mainPageHtml = mainPageHtml.Replace("HIJRI_DATE", hijriMonth + " " + (timeSpan.Days + 1));

        //DateTime lastNewMoonDate = MoonPhase.GetLastNewMoonTime();
        if(!useWebView) LastNewMoonLabel.Text = "Last New Moon : " + lastNewMoonDate.ToString();
		else mainPageHtml = mainPageHtml.Replace("LAST_NEW_MOON", lastNewMoonDate.ToString());

        DateTime nextNewMoonDate = MoonPhase.GetNextNewMoonTime(lastNewMoonDate);


        if(!useWebView) NextNewMoonLabel.Text = "Next New Moon : " + nextNewMoonDate.ToString();
        else mainPageHtml = mainPageHtml.Replace("NEXT_NEW_MOON", nextNewMoonDate.ToString());

        Boolean useLocation = false;
		Location loc = null;


        if (useLocation)
		{

            CurrentLocation currLoc = new CurrentLocation();
            loc = currLoc.GetLocation();
            if (loc == null)
			{
				DebugLabel.Text = "Could not find location";
			}
			else
			{
#if false
			String address = currLoc.GetAddress(loc.Latitude, loc.Longitude);
			DebugLabel.Text = loc.ToString();
#else
				DebugLabel.Text = "Latitude : " + loc.Latitude + " Longitude : " + loc.Longitude;
#endif
			}
		}

		// Compute 29 days from startDate. Add 28 to avoid 1 off error
		DateTime twentyNinthDay = startDate.AddDays(28);

		DateTime sunsetTime = GetSunsetTime(loc, twentyNinthDay);

		// Where is the sunsetTime on the 29th day relative to the next new moon date
		TimeSpan newMoonDelta = sunsetTime.Subtract(nextNewMoonDate);
		//DebugLabel.Text = "New moon delta " + newMoonDelta.ToString();

		DateTime nextHijriMonthStart;
		// Number of hours of moon age when it is possible to see it by the naked eye
		int oldEnoughToBeSeen = 17;
		int numDays;
		// If the new moon is more than oldEnoughToBeSeen hours old at the time of the sunset on the 29th day, assume the month starts the next day
		if (newMoonDelta.Hours > oldEnoughToBeSeen)
		{
			nextHijriMonthStart = twentyNinthDay.AddDays(1);
			numDays = 29;
		}
		else
		{
			nextHijriMonthStart = twentyNinthDay.AddDays(2);
			numDays = 30;
		}

		String nextHijriMonth = HijriMonth.GetNextMonth(hijriMonth);

		String nextMonthText = String.Format("{0} is expected to have {1} days. {2} starts on {3}",
            hijriMonth, numDays, nextHijriMonth, nextHijriMonthStart.ToString("MMMM dd"));

		if(!useWebView) NewHijriMonthLabel.Text = nextMonthText;
		else mainPageHtml = mainPageHtml.Replace("DAYS_EXPECTED", String.Format("{0} is expected to have {1} days", hijriMonth, numDays));

		if (!useWebView) mainPageHtml = mainPageHtml.Replace("NEXT_MONTH_START", String.Format("{0} starts on {1}", nextHijriMonth, nextHijriMonthStart.ToString("MMMM dd")));

		MainPageWebView.Source = new HtmlWebViewSource { Html = mainPageHtml };


        
        DebugLabel.IsVisible = isDebugMode;

		MoonDate.App.HijriMonth = hijriMonth;

    }

   

    private DateTime GetSunsetTime(Location loc, DateTime twentyNinthDay)
    {
		// For now, return 9 pm
		// TBD - Refine
		return twentyNinthDay.AddHours(21);
    }
}

