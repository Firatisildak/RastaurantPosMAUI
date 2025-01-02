using CommunityToolkit.Maui.Views;

namespace RastaurantPosMAUI.Controls;

public partial class HelpPopup : Popup
{
    public const string Email = "abc@gmail.com";
    public const string Phone = "1218734723";
	public HelpPopup()
	{
        InitializeComponent();
	}

    private async void CloseLabel_Tapped(object sender, TappedEventArgs e)=>
		await this.CloseAsync();

    private async void Footer_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.Default.OpenAsync("https://firatisildak.netlify.app");
    }

    private async void CopyEmail_Tapped(object sender, TappedEventArgs e)
    {
        await Clipboard.Default.SetTextAsync(Email);
        copyEmailLable.Text = "Copied";
        await Task.Delay(2000);
        copyEmailLable.Text = "Copy to Clipboard";
    }

    private async void CopyPhone_Tapped(object sender, TappedEventArgs e)
    {
        await Clipboard.Default.SetTextAsync(Phone);
        copyPhoneLable.Text = "Copied";
        await Task.Delay(2000);
        copyPhoneLable.Text = "Copy to Clipboard";
    }
        
}