using Microsoft.Maui.Controls;

namespace PetPass.View.Menu
{
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void OnShowButtonsClicked(object sender, EventArgs e)
        {
            // Mostrar el StackLayout con los botones adicionales
            additionalButtonsStackLayout.IsVisible = true;
        }

        private void OnButton1Clicked(object sender, EventArgs e)
        {
            // L�gica para el Bot�n 1
        }

        private void OnButton2Clicked(object sender, EventArgs e)
        {
            // L�gica para el Bot�n 2
        }

        private void OnButton3Clicked(object sender, EventArgs e)
        {
            // L�gica para el Bot�n 3
        }
    }
}