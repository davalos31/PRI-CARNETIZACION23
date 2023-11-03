using PetPass.Model;
using PetPass.Service;

namespace PetPass.View.Report;

public partial class TotalPet : ContentPage
{
    private readonly ReportService _reportService = new ReportService(); // Aseg�rate de tener una instancia de tu servicio aqu�.
    string _token;
    public TotalPet(string token)
	{
		InitializeComponent();
        _token = token;
        LoadPatrolsAsync();

    }

    private async void LoadPatrolsAsync()
    {
        try
        {
            // Obt�n los datos de patrullas desde tu servicio
            var dataFromService = await _reportService.GetZoneAndTotalDogs(_token);

            // Crea una lista de objetos PatrolViewModel
            var dataList = dataFromService.Select(item => new BrigadierData
            {
                ZoneName = item.ZoneName,
                TotalDogs = item.TotalDogs
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