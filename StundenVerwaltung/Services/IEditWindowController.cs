using StundenVerwaltung.EventArgs;

namespace StundenVerwaltung.Services
{
    public interface IEditWindowController
    {
        bool? ShowDialog(OpenEditWindowArgs args);
    }
}