using PetPass.Model;
using PetPass.Service;
using PetPass.ViewModel;

namespace PetPass.View.Report;


public partial class ReportZone : ContentPage
{
    private readonly PatrolService _patrolService = new PatrolService(); // Asegúrate de tener una instancia de tu servicio aquí.

    public ReportZone()
    {
        InitializeComponent();
        LoadPatrolsAsync();
    }

    private async void LoadPatrolsAsync()
    {
        try
        {
            // Obtén los datos de patrullas desde tu servicio
            var dataFromService = await _patrolService.GetPatrolsByZoneAsync();

            // Crea una lista de objetos PatrolViewModel
            var dataList = dataFromService.Select(item => new Reports
            {
                ZoneName = item.ZoneName,
                PeopleCount = item.PeopleCount
            }).ToList();

            // Asigna la lista de objetos PatrolViewModel a la ListView
            patrolListView.ItemsSource = dataList;

            // Oculta el ActivityIndicator y muestra la ListView
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            patrolListView.IsVisible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar los datos: {ex.Message}");
        }
    }
}

