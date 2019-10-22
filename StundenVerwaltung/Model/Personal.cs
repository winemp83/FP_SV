using System;
using GalaSoft.MvvmLight;

namespace StundenVerwaltung.Model
{
    public class Personal : ObservableObject, IPersonal
    {
        #region Variables
        private string _ID;
        private string _MID;
        private string _Name;
        private string _VName;
        private double _Stunden;
        private bool _IstUnterrichtet;

        public string ID
        {
            get { return _ID; }
            set { Set(ref _ID, value); }
        }
        public string MID
        {
            get { return _MID; }
            set { Set(ref _MID, value); }
        }
        public string Name
        {
            get { return _Name; }
            set { Set(ref _Name, value); }
        }
        public string VName
        {
            get { return _VName; }
            set { Set(ref _VName, value); }
        }
        public double Stunden
        {
            get { return _Stunden; }
            set { Set(ref _Stunden, value); }
        }
        public bool IstUnterrichtet
        {
            get { return _IstUnterrichtet; }
            set { Set(ref _IstUnterrichtet, value); }
        }
        #endregion
    }
}
