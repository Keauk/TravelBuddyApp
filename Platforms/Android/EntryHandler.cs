using AndroidX.AppCompat.Widget;

namespace TravelBuddyApp.Platforms.Android
{
    public class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            platformView.Background = null;
            base.ConnectHandler(platformView);
        }
    }
}
