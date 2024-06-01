using AndroidX.AppCompat.Widget;

namespace TravelBuddyApp.Platforms.Android
{
    public class EditorHandler : Microsoft.Maui.Handlers.EditorHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            platformView.Background = null;
            base.ConnectHandler(platformView);
        }
    }
}
