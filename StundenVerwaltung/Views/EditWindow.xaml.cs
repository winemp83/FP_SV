using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using StundenVerwaltung.EventArgs;

namespace StundenVerwaltung.Views
{
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            Unloaded += EditWindow_Unloaded;
            Messenger.Default.Register<CloseWindowEventArgs>(this, CloseWindow);
        }

        private void CloseWindow(CloseWindowEventArgs obj) {
            this.DialogResult = true;
            this.Close();
        }

        private void EditWindow_Unloaded(object sender, RoutedEventArgs e) {
            Messenger.Default.Unregister<CloseWindowEventArgs>(this);
        }
    }
}
