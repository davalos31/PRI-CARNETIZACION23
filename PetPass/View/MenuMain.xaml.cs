using Microsoft.Maui.Controls;
using PetPass.View.Campaign;
using PetPass.View.Patrol;
using PetPass.View.Person;
using PetPass.View.Report;
using System;

namespace PetPass.View
{
    public partial class MenuMain : ContentPage
    {
        private bool areReportsVisible = false;
        private bool areJefeBrigadaVisible = false;
        private bool areCampanasVisible = false;
        private bool arePatrullajeVisible = false;
        int _userId;
        string _token;

        public MenuMain(int userId, string token)
        {
            InitializeComponent();
            _userId = userId;
            _token = token;
        }

        private void OnContentViewTapped(object sender, EventArgs e)
        {
            // Oculta todos los botones adicionales
            showReportsButtonsStackLayout.IsVisible = false;
            jefeBrigadaButtonsStackLayout.IsVisible = false;
            crearCampanasButtonsStackLayout.IsVisible = false;
            crearPatrullajeButtonsStackLayout.IsVisible = false;

            // Muestra el botón "Mostrar Reportes", "Por Jefe Brigada", "Crear Campañas" y "Crear Patrullaje"
            showReportsButton.IsVisible = true;
            jefeBrigadaButton.IsVisible = true;
            crearCampanasButton.IsVisible = true;
            crearPatrullajeButton.IsVisible = true;

            areReportsVisible = false;
            areJefeBrigadaVisible = false;
            areCampanasVisible = false;
            arePatrullajeVisible = false;
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
                // Si no están visibles, muéstralos y oculta los botones de "Por Jefe Brigada", "Crear Campañas" y "Crear Patrullaje"
                showReportsButtonsStackLayout.IsVisible = true;
                jefeBrigadaButtonsStackLayout.IsVisible = false;
                crearCampanasButtonsStackLayout.IsVisible = false;
                crearPatrullajeButtonsStackLayout.IsVisible = false;
            }

            // Invierte el estado de visibilidad
            areReportsVisible = !areReportsVisible;
        }

        private void OnReporte1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReportPet());
        }

        private void OnReporte2Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReportZone());
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
                // Si no están visibles, muéstralos y oculta los botones de reportes, "Crear Campañas" y "Crear Patrullaje"
                jefeBrigadaButtonsStackLayout.IsVisible = true;
                showReportsButtonsStackLayout.IsVisible = false;
                crearCampanasButtonsStackLayout.IsVisible = false;
                crearPatrullajeButtonsStackLayout.IsVisible = false;
            }

            // Invierte el estado de visibilidad
            areJefeBrigadaVisible = !areJefeBrigadaVisible;
        }

        private void OnJefeBrigada1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePerson(_userId, _token));
        }

        private void OnJefeBrigada2Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailPerson(_token, _userId));
        }

        private void OnCrearCampanasClicked(object sender, EventArgs e)
        {
            if (areCampanasVisible)
            {
                // Si los botones de "Crear Campañas" ya están visibles, ocúltalos
                crearCampanasButtonsStackLayout.IsVisible = false;
            }
            else
            {
                // Si no están visibles, muéstralos y oculta los botones de reportes, "Por Jefe Brigada" y "Crear Patrullaje"
                crearCampanasButtonsStackLayout.IsVisible = true;
                showReportsButtonsStackLayout.IsVisible = false;
                jefeBrigadaButtonsStackLayout.IsVisible = false;
                crearPatrullajeButtonsStackLayout.IsVisible = false;
            }

            // Invierte el estado de visibilidad
            areCampanasVisible = !areCampanasVisible;
        }

        private void OnCrearCampanas1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateCampaign( _token));
        }

        private void OnCrearCampanas2Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailCampaign(_token));
        }

        private void OnCrearCampanas3Clicked(object sender, EventArgs e)
        {
            // Lógica para crear campaña 3
        }

        private void OnCrearPatrullajeClicked(object sender, EventArgs e)
        {
            if (arePatrullajeVisible)
            {
                // Si los botones de "Crear Patrullaje" ya están visibles, ocúltalos
                crearPatrullajeButtonsStackLayout.IsVisible = false;
            }
            else
            {
                // Si no están visibles, muéstralos y oculta los botones de reportes, "Por Jefe Brigada" y "Crear Campañas"
                crearPatrullajeButtonsStackLayout.IsVisible = true;
                showReportsButtonsStackLayout.IsVisible = false;
                jefeBrigadaButtonsStackLayout.IsVisible = false;
                crearCampanasButtonsStackLayout.IsVisible = false;
            }

            // Invierte el estado de visibilidad
            arePatrullajeVisible = !arePatrullajeVisible;
        }

        private void OnCrearPatrullajeAClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePatrol(_userId, _token));
        }

        private void OnCrearPatrullajeBClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailPatrol( _token,_userId));
        }

        private void OnCrearPatrullajeCClicked(object sender, EventArgs e)
        {
            // Lógica para crear patrullaje C
        }
    }
}
