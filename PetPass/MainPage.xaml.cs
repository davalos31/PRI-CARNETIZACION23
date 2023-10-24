using PetPass.Login;
namespace PetPass
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

		private void btnLogin_Clicked(object sender, EventArgs e)
		{
            var page = new LoginPage();
            Navigation.PushAsync(page);
		}
	}
}