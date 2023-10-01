namespace PetPass
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            // creacion rama ricaldez
        }

        private void btnCreateOwner_Clicked(object sender, EventArgs e)
        {
            var form = new CreateOwner();
            Navigation.PushAsync(form);
        }

		private void btnAskRecovery_Clicked(object sender, EventArgs e)
		{
			var form = new AskRecovery();
			Navigation.PushAsync(form);
		}
	}
}