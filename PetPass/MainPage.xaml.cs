namespace PetPass;
using PetPass.View;
using PetPass.View.Login;

public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new CreatePerson());
            await Application.Current.MainPage.Navigation.PushAsync(new MenuMain());
        }
    }
