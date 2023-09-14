using Android.App;
using Android.Runtime;

namespace PetPass
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    
        //Hola desde mi maquina Nahu ---- te saludo fercho
    
    }
}