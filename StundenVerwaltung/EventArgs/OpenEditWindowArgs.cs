using StundenVerwaltung.Model;

namespace StundenVerwaltung.EventArgs
{
    public enum ActionType { 
        Add,
        Edit
    }
    public class OpenEditWindowArgs
    {
        public Personal Personal { get; set; }
        public ActionType Type { get; set; }
    }
}
