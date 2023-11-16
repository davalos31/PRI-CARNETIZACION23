using Camera.MAUI;
using PetPass.Model;
using PetPass.Model.Extras;
using PetPass.Service;

namespace PetPass;

public partial class UseCamera : ContentPage
{
	List<string> base64Images;

	int index = 0;
	private Pet pet1;
	readonly PetService pets;

	public UseCamera(Pet pet)
	{
		InitializeComponent();
		pet1 = pet;
		pets = new PetService();
		base64Images = new List<string>();
	}

	private void CameraView_CamerasLoaded(object sender, EventArgs e)
	{
		try
		{
			CameraView.Camera = CameraView.Cameras.First();

			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await CameraView.StopCameraAsync();
				await CameraView.StartCameraAsync();
			});

			/*if (DeviceInfo.Platform != DevicePlatform.Android)
			{
				CameraView.Camera = CameraView.Cameras.First();

				MainThread.BeginInvokeOnMainThread(async () =>
				{
					await CameraView.StopCameraAsync();
					await CameraView.StartCameraAsync();
				});
			}
			else
			{
				CameraView.IsVisible = false;
			}*/
		}
		catch
		{
			DisplayAlert("Sistema", "no se encontro una camara", "ok");
		}
	}

	private void btnPick_Clicked(object sender, EventArgs e)
	{
		generalCamera();
		

		//if (DeviceInfo.Platform == DevicePlatform.Android)
		//{
		//	cameraAndroid();
		//}
		//else
		//{
		//	generalCamera();
		//}
	}

	private async void btnTerminar_Clicked(object sender, EventArgs e)
	{
		// Mostrar la pantalla de carga
		loadingIndicator.IsRunning = true;
		loadingIndicator.IsVisible = true;
		btnTerminar.IsEnabled = false;

		try
		{
			PetCreated p = new(0, pet1.Name, pet1.Specie, pet1.Breed, pet1.Gender, pet1.BirthDate, pet1.SpecialFeature, 0, pet1.PersonId, session.AuthResponse.userID, base64Images);
			bool res = await pets.CreatePet(p);
			if (res)
			{
				await DisplayAlert("Sistema", "el registro se completo correctamente", "ok");

				await CameraView.StopCameraAsync();

				await Navigation.PushAsync(new MenuBrigadier());
			}
			else
			{
				await DisplayAlert("Sistema", "no se pudo completar el registro", "ok");
			}
		}
		finally
		{
			// Ocultar la pantalla de carga, incluso si hay una excepción
			loadingIndicator.IsRunning = false;
			loadingIndicator.IsVisible = false;
			btnTerminar.IsEnabled = true;
		}

	}


	private async void generalCamera()
	{
		ImageSource newImage = CameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);

		if (newImage is StreamImageSource streamImageSource)
		{
			var cancellationToken = new CancellationToken();
			Stream imageStream = await streamImageSource.Stream(cancellationToken);

			using (MemoryStream memoryStream = new MemoryStream())
			{
				await imageStream.CopyToAsync(memoryStream);

				byte[] imageBytes = memoryStream.ToArray();
				string base64Image = Convert.ToBase64String(imageBytes);

				bool savePhoto = await DisplayAlert("Guardar Foto", "¿Deseas guardar la foto?", "Sí", "No");

				if (savePhoto)
				{
					if (index < 4)
					{
						base64Images.Add(Convert.ToBase64String(imageBytes));
						if (index == 0) myImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
						else if (index == 1) myImage2.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
						else if (index == 2) myImage3.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
						else if (index == 3) myImage4.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));

						index++;

						if (index == 4)
						{
							btnTerminar.IsEnabled = true;
							btnPick.IsEnabled = false;
						}
					}
				}
			}
		}
		else
		{
			await DisplayAlert("Valor de la Imagen", "La imagen no se puede convertir en formato base64.", "OK");
		}
	}

	/// <summary>
	/// no se usa
	/// </summary>
	private async void cameraAndroid()
	{
		try
		{

			var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
			if (status != PermissionStatus.Granted)
			{
				status = await Permissions.RequestAsync<Permissions.Camera>();
			}

			if (status == PermissionStatus.Granted)
			{
				var photo = await MediaPicker.CapturePhotoAsync();

				if (photo != null)
				{
					using (Stream imageStream = await photo.OpenReadAsync())
					{
						using (MemoryStream ms = new MemoryStream())
						{
							await imageStream.CopyToAsync(ms);
							byte[] imageBytes = ms.ToArray();

							if (index < 4)
							{
								base64Images.Add(Convert.ToBase64String(imageBytes));  // este es el string para mandar
								if (index == 0) myImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)); // este es para mostrar en la aplicacion
								else if (index == 1) myImage2.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)); // este es para mostrar en la aplicacion
								else if (index == 2) myImage3.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)); // este es para mostrar en la aplicacion
								else if (index == 3) myImage4.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)); // este es para mostrar en la aplicacion

								index++;
							}
						}
					}
				}
				else
				{
					await DisplayAlert("Error", "no se pudo sacar la foto", "Aceptar");
				}
			}
			else
			{
				await DisplayAlert("Permiso denegado", "La aplicación no tiene permisos para acceder a la cámara.", "Aceptar");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", "Ha ocurrido un error al capturar la imagen: " + ex.Message, "Aceptar");
		}
	}

	private void volver()
	{
		// Obtén la instancia de NavigationPage
		var navigationPage = Application.Current.MainPage as NavigationPage;

		// Verifica que sea un NavigationPage y no sea nulo
		if (navigationPage != null)
		{
			// Obtén la página específica a la que quieres ir
			var paginaEspecifica = new MenuBrigadier();

			// Itera sobre el historial y utiliza PopAsync hasta llegar a la página específica
			while (navigationPage.Navigation.NavigationStack.LastOrDefault() != paginaEspecifica)
			{
				navigationPage.Navigation.PopAsync();
			}
		}
	}
}