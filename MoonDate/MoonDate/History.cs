namespace MoonDate;

public class History : ContentPage
{
	public History()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center,
					Text = "Welcome to .NET MAUI! " + MoonDate.App.HijriMonth
				}
			}
		};
	}
}