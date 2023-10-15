using PetPass.Service;
using PetPass.Model;
namespace PetPass.View.Report;

public partial class ReportPet : ContentPage
{
    private readonly PatrolService _patrolService = new PatrolService(); // Aseg�rate de tener una instancia de tu servicio aqu�.
    public ReportPet()
    {
        InitializeComponent();
        LoadPatrolsAsync();

    }
    private async void LoadPatrolsAsync()
    {
        try
        {
            // Obt�n los datos de patrullas desde tu servicio
            var dataFromService = await _patrolService.GetReportByPet();

            // Crea una lista de objetos PatrolViewModel
            var dataList = dataFromService.Select(item => new Reports
            {
                PersonName = item.PersonName,
                PetCount = item.PetCount
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