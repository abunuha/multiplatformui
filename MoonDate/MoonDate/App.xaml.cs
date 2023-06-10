namespace MoonDate;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		moonImage = MoonPhase.GetCurrentMoonPhaseImage();
    }

    public static string HijriMonth { get; internal set; }
    public static string moonImage { get; private set; }
}
