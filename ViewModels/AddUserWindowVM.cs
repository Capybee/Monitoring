using Microsoft.IdentityModel.Tokens;
using Monitoring.Models;
using Monitoring.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monitoring.ViewModels
{
    public class AddUserWindowVM : INotifyPropertyChanged
    {

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


        private RelayCommand _AddUser;
        public RelayCommand AddUser
        {
            get => _AddUser ?? (_AddUser = new RelayCommand(obj =>
            {
                if ((!Surname.IsNullOrEmpty()) && (!Name.IsNullOrEmpty()) && (!MiddleName.IsNullOrEmpty()) && (!Login.IsNullOrEmpty()) && (!Password.IsNullOrEmpty()))
                {
                    ObservableCollection<Users> UsersCollection = new ObservableCollection<Users>();
                    UsersCollection = DBController.GetUsers();

                    if (DBController.AddUser(UsersCollection[UsersCollection.Count - 1].Id+1, Surname, Name, MiddleName, Login, Password, IsAdmin))
                    {
                        MessageBox.Show("Пользователь успешно добавлен!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Window ThisWindow = obj as Window;
                        AdminMainWindow InstanceAdminMainWindow = new AdminMainWindow();
                        InstanceAdminMainWindow.Show();
                        ThisWindow.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
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
