using StundenVerwaltung.EventArgs;
using StundenVerwaltung.Model;
using StundenVerwaltung.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;

namespace StundenVerwaltung.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Personal> _allPersonals;
        private Personal _selectedPersonal;
        
        public RelayCommand AddPersonalCommand { get; set; }
        public ObservableCollection<Personal> AllPersonals{
            get { return _allPersonals; }
            set { Set(ref _allPersonals, value); }
        }
        public IDataProvider DataProvider { get; }
        public RelayCommand<Personal> DeletePersonalCommand { get; set; }
        public IDialogService DialogService { get; }
        public RelayCommand<Personal> EditPersonalCommand { get; set; }
        public IEditWindowController EditWindowController { get; }
        public Personal SelectedPersonal
        {
            get { return _selectedPersonal; }
            set {
                Set(ref _selectedPersonal, value);
                DeletePersonalCommand.RaiseCanExecuteChanged();
                EditPersonalCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel(IDataProvider dataProvider, IEditWindowController editWindowController, IDialogService dialogService)
        {
            DataProvider = dataProvider;
            EditWindowController = editWindowController;
            DialogService = dialogService;

            AddPersonalCommand = new RelayCommand(AddPersonal);
            EditPersonalCommand = new RelayCommand<Personal>(EditPersonal, personal => SelectedPersonal != null);
            DeletePersonalCommand = new RelayCommand<Personal>(DeletePersonal, personal => SelectedPersonal != null);

            AllPersonals = new ObservableCollection<Personal>(dataProvider.GetAllPersonal().OfType<Personal>());
        }

        private void AddPersonal() {
            var result = EditWindowController.ShowDialog(new OpenEditWindowArgs { Type = ActionType.Add });
            if(result.HasValue && result.Value)
                AllPersonals = new ObservableCollection<Personal>(DataProvider.GetAllPersonal().OfType<Personal>());
        }
        private void DeletePersonal(Personal personal) { 
            if(DialogService.Confirm("Möchten Sie wirklich diesen Angestellten löschen?")) {
                AllPersonals.Remove(personal);
                DataProvider.Delete(personal);
                DialogService.ShowMessage("Angestellten erfolgreich gelöscht!");
            }
        }
        private void EditPersonal(Personal personal) {
            var result = EditWindowController.ShowDialog(new OpenEditWindowArgs { Type = ActionType.Edit, Personal = SelectedPersonal });
            if(result.HasValue && result.Value) {
                int index = AllPersonals.IndexOf(SelectedPersonal);
                AllPersonals = new ObservableCollection<Personal>(DataProvider.GetAllPersonal().OfType<Personal>());
                SelectedPersonal = AllPersonals[index];
                DialogService.ShowMessage("Angestellten erfolgreich bearbeitet");
            }
        }
    }
}