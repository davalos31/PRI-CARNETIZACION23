using System;
using System.IO;
using System.Threading.Tasks;
using PetPass.Validation;
using Microsoft.Maui.Controls;
using Camera.MAUI;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;


namespace PetPass.View.Person
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePerson : ContentPage
    {
        private ImageSource capturedImage;
        private string capturedImagePath;

        public ImagePerson()
        {
            InitializeComponent();
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
            }
            catch
            {
                DisplayAlert("Sistema", "no se encontro una camara", "ok");
            }
        }

        private void btnPick_Clicked(object sender, EventArgs e)
        {
            
            _ = CaptureAndConvertAsync();
        }


        private async Task CaptureAndConvertAsync()
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
                       
                      Validations.SetCapturedImage(base64Image);
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await CameraView.StopCameraAsync();
                          
                        });
                        Navigation.PopAsync();
                    
                    }
                    else
                    {
                        //myImage.Source = null;
                    }
                }
            }
            else
            {
                await DisplayAlert("Valor de la Imagen", "La imagen no se puede convertir en formato base64.", "OK");
            }
        }

        private void btnVolver_Clicked(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await CameraView.StopCameraAsync();

            });

            Navigation.PopAsync();
        }

      


    }
}
