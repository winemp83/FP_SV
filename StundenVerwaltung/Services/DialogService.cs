using System;
using System.Windows;

namespace StundenVerwaltung.Services
{
    public class DialogService : IDialogService
    {
        public bool Confirm(string message)
        {
            var result = MessageBox.Show(message, "Betättige", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes ? true : false;
        }
        public void Exception(Exception ex)
        {
            string message = $@"Es ist ein Fehler aufgetretten: {ex.Message} 

{ex.ToString()}";
            MessageBox.Show(message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void Warning(string message)
        {
            MessageBox.Show(message, "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
