using Microsoft.Maui.Controls;
using System;

namespace PetPass.View
{
    public partial class MenuMain : ContentPage
    {
        private bool areReportsVisible = false;
        private bool areJefeBrigadaVisible = false;

        public MenuMain()
        {
            InitializeComponent();
        }

        private void OnContentViewTapped(object sender, EventArgs e)
        {
            // Oculta todos los botones adicionales
            showReportsButtonsStackLayout.IsVisible = false;
            jefeBrigadaButtonsStackLayout.IsVisible = false;

            // Muestra el botón "Mostrar Reportes" y "Por Jefe Brigada"
            showReportsButton.IsVisible = true;
            jefeBrigadaButton.IsVisible = true;

            areReportsVisible = false;
            areJefeBrigadaVisible = false;
        }

        private void OnShowReportsClicked(object sender, EventArgs e)
        {
            if (areReportsVisible)
            {
                // Si los botones de reportes ya están visibles, ocúltalos
                showReportsButtonsStackLayout.IsVisible = false;
            }
            else
            {
                // Si no están visibles, muéstralos y oculta los botones de "Por Jefe Brigada"
                showReportsButtonsStackLayout.IsVisible = true;
                jefeBrigadaButtonsStackLayout.IsVisible = false;
            }

            // Invierte el estado de visibilidad
            areReportsVisible = !areReportsVisible;
        }

        private void OnReporte1Clicked(object sender, EventArgs e)
        {
            // Lógica para el Reporte 1
        }

        private void OnReporte2Clicked(object sender, EventArgs e)
        {
            // Lógica para el Reporte 2
        }

        private void OnReporte3Clicked(object sender, EventArgs e)
        {
            // Lógica para el Reporte 3
        }

        private void OnJefeBrigadaClicked(object sender, EventArgs e)
        {
            if (areJefeBrigadaVisible)
            {
                // Si los botones de "Por Jefe Brigada" ya están visibles, ocúltalos
                jefeBrigadaButtonsStackLayout.IsVisible = false;
            }
            else
            {
                // Si no están visibles, muéstralos y oculta los botones de reportes
                jefeBrigadaButtonsStackLayout.IsVisible = true;
                showReportsButtonsStackLayout.IsVisible = false;
            }

            // Invierte el estado de visibilidad
            areJefeBrigadaVisible = !areJefeBrigadaVisible;
        }

        private void OnJefeBrigada1Clicked(object sender, EventArgs e)
        {
            // Lógica para el Jefe Brigada 1
        }

        private void OnJefeBrigada2Clicked(object sender, EventArgs e)
        {
            // Lógica para el Jefe Brigada 2
        }

        private void OnJefeBrigada3Clicked(object sender, EventArgs e)
        {
            // Lógica para el Jefe Brigada 3
        }
    }
}
