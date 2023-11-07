using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Microsoft.Maui.Controls;
using PetPass.Model;
using System.Collections.Generic;
using PetPass.Service;


namespace PetPass.View.Patrol
{
    public partial class DetailPatrol : ContentPage
    {
        private ObservableCollection<PatrolInfo> Patrols;
       
        private PatrolService _patrolService;
        string _tokenValue;
        int _idUser;

        public DetailPatrol(string _tokenvalue, int _iduser)
        {
            InitializeComponent();
            _tokenValue = _tokenvalue;
            _patrolService = new PatrolService();
            _idUser = _iduser;
            LoadDateApiAsync();
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            PatrolListView.ItemsSource = Patrols;
        }


        private async Task LoadDateApiAsync()
        {
            try
            {
                // Llama al método para obtener la lista de patrullas, pasando el token de autorización
                Patrols = new ObservableCollection<PatrolInfo>();
                string authToken = _tokenValue; // Reemplaza con tu token real
                List<PatrolInfo> patrolList = await _patrolService.GetPatrolAsyncApi(authToken);
               
                if (patrolList != null)
                {
                    // Los datos se cargaron con éxito, ahora puedes usar la lista `patrolList`.
                    foreach (var patrol in patrolList)
                    {
                        Patrols.Add(patrol);
                    }
                }
                else
                {
                    // Maneja el caso en el que no se pudieron obtener datos (error o lista vacía).
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, como problemas de red o errores en la solicitud HTTP.
                Console.WriteLine("Error al cargar datos: " + ex.Message);
            }
        }



    }
}
