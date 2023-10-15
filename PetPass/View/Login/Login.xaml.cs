using PetPass.Service;
using PetPass.ViewModel;

namespace PetPass.View.Login;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();

        // Crea una instancia de UserService con tu cadena de conexión.
        var userService = new UserService("Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;");

        // Crea una instancia de LoginViewModel pasando el servicio como argumento.
        var loginViewModel = new LoginViewModel(userService);

        // Establece el contexto de enlace (BindingContext) de la página con el ViewModel.
        BindingContext = loginViewModel;
    }

    //    Establece tu cadena de conexión aquí
    //    private string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";

    //    public Login()
    //    {
    //        InitializeComponent();
    //    }

    //    private async void LoginButton_Clicked(object sender, EventArgs e)
    //    {
    //        // Obtener los valores de los campos de entrada
    //        string username = UsernameEntry.Text;
    //        string password = PasswordEntry.Text;

    //        // Realizar la lógica de validación de usuario aquí
    //        bool isValidUser = await ValidarUsuarioAsync(username, password);

    //        if (isValidUser)
    //        {
    //            // Usuario válido, obtén el rol
    //            string userRole = await GetUserRoleAsync(username, password);

    //            if (userRole == "B")
    //            {
    //                // Navegar a la página de administrador
    //                await Navigation.PushAsync(new MainPage());
    //            }
    //            else
    //            {
    //                // Otro rol, muestra un mensaje de error.
    //                ErrorMessageLabel.Text = "Rol no válido. Por favor, inténtalo de nuevo o contacta al administrador.";
    //                ErrorMessageLabel.IsVisible = true;
    //            }
    //        }
    //        else
    //        {
    //            // Usuario no válido, muestra un mensaje de error.
    //            ErrorMessageLabel.Text = "Nombre de usuario o contraseña incorrectos";
    //            ErrorMessageLabel.IsVisible = true;
    //        }
    //    }

    //    private async Task<string> GetUserRoleAsync(string userName, string password)
    //    {
    //        using (SqlConnection connection = new SqlConnection(_connectionString))
    //        {
    //            connection.Open();

    //            using (SqlCommand command = new SqlCommand(
    //                "SELECT [Rol] FROM [DbPetPass].[dbo].[User] WHERE [username] = @Username AND [userpassword] = @Password", connection))
    //            {
    //                command.Parameters.AddWithValue("@Username", userName);
    //                command.Parameters.AddWithValue("@Password", password);

    //                string role = await command.ExecuteScalarAsync() as string;
    //                return role;
    //            }
    //        }
    //    }

    //    private async Task<bool> ValidarUsuarioAsync(string username, string password)
    //    {
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(_connectionString))
    //            {
    //                await connection.OpenAsync();

    //                using (SqlCommand command = new SqlCommand(
    //                    "SELECT COUNT(*) FROM [User] WHERE [username] = @Username AND [userpassword] = @Password", connection))
    //                {
    //                    command.Parameters.AddWithValue("@Username", username);
    //                    command.Parameters.AddWithValue("@Password", password);

    //                    int count = (int)await command.ExecuteScalarAsync();

    //                    // Si count es mayor que 0, las credenciales son válidas.
    //                    return count > 0;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Manejo de excepciones, log, etc.
    //            Console.WriteLine($"Error al verificar usuario: {ex.Message}");
    //            return false;
    //        }
    //    }
}