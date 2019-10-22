using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using StundenVerwaltung.EventArgs;
using StundenVerwaltung.Model;
using StundenVerwaltung.Services;
using System;

namespace StundenVerwaltung.ViewModel
{
    public class EditViewModel : ViewModelBase
    {
        public Personal CurrentPersonal { get; set; }
        public IDataProvider DataProvider { get; }
        public IDialogService DialogService { get; set; }
        public RelayCommand SaveDataCommand { get; set; }
        protected OpenEditWindowArgs Args { get; }
        
        public EditViewModel(OpenEditWindowArgs args, IDataProvider dataProvider, IDialogService dialogService) {
            Args = args;
            DataProvider = dataProvider;
            DialogService = dialogService;

            switch (args.Type) {
                case ActionType.Add:
                    CurrentPersonal = new Personal { ID = Guid.NewGuid().ToString(), Stunden = 0.0f };
                    break;
                case ActionType.Edit:
                    CurrentPersonal = new Personal {
                        ID = args.Personal.ID,
                        MID = args.Personal.MID,
                        Name = args.Personal.Name,
                        VName = args.Personal.VName,
                        Stunden = args.Personal.Stunden,
                        IstUnterrichtet = args.Personal.IstUnterrichtet
                    };
                    break;
            }
            SaveDataCommand = new RelayCommand(SaveData);
        }

        private void SaveData() {
            if (string.IsNullOrWhiteSpace(CurrentPersonal.MID)) {
                DialogService.Warning("Mitarbeiter ID wird benötigt");
                return;
            }else if (string.IsNullOrWhiteSpace(CurrentPersonal.Name)) {
                DialogService.Warning("Name wird benötigt");
                return;
            }else if(string.IsNullOrWhiteSpace(CurrentPersonal.VName)) {
                DialogService.Warning("Vorname fehlt!");
                return;
            }
            bool result = false;
            switch (Args.Type) {
                case ActionType.Add:
                    result = DataProvider.Insert(CurrentPersonal);
                    break;
                case ActionType.Edit:
                    result = DataProvider.Update(CurrentPersonal);
                    break;
            }
            if (result) {
                DialogService.ShowMessage($"{Args.Type} Personal erfolgreich");
                Messenger.Default.Send(new CloseWindowEventArgs());
            }else {
                DialogService.Warning($"Ein Fehler ist aufgetretten, Datenspeichern abgebrochen");
            }
        }

    }
}
