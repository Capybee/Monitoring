using Microsoft.IdentityModel.Tokens;
using Monitoring.Models;
using Monitoring.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monitoring.ViewModels
{
    public class ChangeUserInfoWindowVM : INotifyPropertyChanged
    {
        int _SelectedId = 0;

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

        private string _MiddleName;
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; OnPropertyChanged(nameof(MiddleName)); }
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

        private bool _NotAdmin;
        public bool NotAdmin
        {
            get { return _NotAdmin; }
            set { _NotAdmin = value; OnPropertyChanged(nameof(NotAdmin)); }
        }


        public ChangeUserInfoWindowVM(Users SelectedUser)
        {
            _SelectedId = SelectedUser.Id;
            Surname = SelectedUser.Surname;
            Name = SelectedUser.Name;
            MiddleName = SelectedUser.MidleName;
            Login = SelectedUser.Login;
            Password = SelectedUser.Password;
            IsAdmin = SelectedUser.IsAdmin;

            if (!SelectedUser.IsAdmin)
                NotAdmin = true;
        }

        private RelayCommand _ChangeUser;
        public RelayCommand ChangeUser
        {
            get => _ChangeUser ?? (_ChangeUser = new RelayCommand(obj =>
            {
                if ((!Surname.IsNullOrEmpty()) && (!Name.IsNullOrEmpty()) && (!MiddleName.IsNullOrEmpty()) && (!Login.IsNullOrEmpty()) && (!Password.IsNullOrEmpty()))
                {
                    if (DBController.UpdateUser(_SelectedId, Surname, Name, MiddleName, Login, Password, IsAdmin))
                    {
                        MessageBox.Show("Данные успешно обновлены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Window ThisWindow = obj as Window;
                        AdminMainWindow InstanceAdminMainWindow = new AdminMainWindow();
                        InstanceAdminMainWindow.Show();
                        ThisWindow.Close();
                    }
                }
            }));
        }

        private RelayCommand _Back;
        public RelayCommand Back
        {
            get => _Back ?? (_Back = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                AdminMainWindow InstanceAdminMainWindow = new AdminMainWindow();
                InstanceAdminMainWindow.Show();
                ThisWindow.Close();
            }));
        }

        private RelayCommand _Close;
        public RelayCommand Close
        {
            get => _Close ?? (_Close = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                ThisWindow.Close();
            }));
        }

        private RelayCommand _RollUp;
        public RelayCommand RollUp
        {
            get => _RollUp ?? (_RollUp = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                ThisWindow.WindowState = WindowState.Minimized;
            }));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
