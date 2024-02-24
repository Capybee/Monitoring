using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring.Models
{
    public class Users : INotifyPropertyChanged
    {
        private int _ID;
        public int Id
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(nameof(Id)); }
        }

        private string _Surname;
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string _MidleName;
        public string MidleName
        {
            get { return _MidleName; }
            set { _MidleName = value; OnPropertyChanged(nameof(MidleName)); }
        }

        private string _Login;
        public string Login
        {
            get { return _Login; }
            set { _Login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(nameof(Password)); }
        }


        private bool _IsAdmin;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; OnPropertyChanged(nameof(IsAdmin)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
