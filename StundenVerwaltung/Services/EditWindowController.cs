using StundenVerwaltung.EventArgs;
using StundenVerwaltung.Views;
using GalaSoft.MvvmLight.Ioc;

namespace StundenVerwaltung.Services
{
    public class EditWindowController : IEditWindowController
    {
        public bool? ShowDialog(OpenEditWindowArgs args)
        {
            if (SimpleIoc.Default.ContainsCreated<OpenEditWindowArgs>())
                SimpleIoc.Default.Unregister<OpenEditWindowArgs>();
            SimpleIoc.Default.Register(() => args);
            EditWindow editWindow = new EditWindow();
            return editWindow.ShowDialog();
        }
    }
}
