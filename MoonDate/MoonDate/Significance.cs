namespace MoonDate;

public class Significance : ContentPage
{
	public Significance()
	{
        String history = String.Empty;
        String significance = String.Empty;

        HijriMonthInfo.GetHijriMonthInfo(MoonDate.App.HijriMonth, ref history, ref significance);
        Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = significance
				}
			}
		};
	}
}