namespace PetPass;

public partial class UseCamera : ContentPage
{
	List<ImageSource> images = new List<ImageSource>();
	int index = 0;
	public UseCamera()
	{
		InitializeComponent();
	}

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
		CameraView.Camera = CameraView.Cameras.First();

		MainThread.BeginInvokeOnMainThread(async () =>
		{
			await CameraView.StopCameraAsync();
			await CameraView.StartCameraAsync();
		});
    }

    private void btnPick_Clicked(object sender, EventArgs e)
    {
		switch (index)
		{
			case 0:
                images.Add(CameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG));
				myImage.Source = images[index];
                break;
            case 1:
                images.Add(CameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG));
                myImage2.Source = images[index];
                break;
            case 2:
                images.Add(CameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG));
                myImage3.Source = images[index];
                break;
            case 3:
                images.Add(CameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG));
                myImage4.Source = images[index];
                break;
        }
        index++;
    }
}