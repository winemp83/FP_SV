using System;

namespace StundenVerwaltung.Services
{
    public interface IDialogService
    {
        bool Confirm(string message);
        void Exception(Exception ex);
        void ShowMessage(string message);
        void Warning(string message);
    }
}