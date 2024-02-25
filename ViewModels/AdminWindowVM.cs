using Monitoring.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Monitoring.Views;
using System.Collections.ObjectModel;

namespace Monitoring.ViewModels
{
    public class AdminWindowVM : INotifyPropertyChanged
    {
        private ObservableCollection<Results> _ResultsCollection;
        public ObservableCollection<Results> ResultsCollection
        {
            get { return _ResultsCollection; }
            set { _ResultsCollection = value; OnPropertyChanged(nameof(ResultsCollection)); }
        }

        private ObservableCollection<Users> _UsersCollection;
        public ObservableCollection<Users> UsersCollection
        {
            get { return _UsersCollection; }
            set { _UsersCollection = value; OnPropertyChanged(nameof(UsersCollection)); }
        }

        private Users _SelectedUser;
        public Users SelectedUser
        {
            get { return _SelectedUser; }
            set { _SelectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }

        private Results _SelectedResult;
        public Results SelectedResult
        {
            get { return _SelectedResult; }
            set { _SelectedResult = value; OnPropertyChanged(nameof(SelectedResult)); }
        }


        public AdminWindowVM()
        {
            ResultsCollection = DBController.GetResults();
            UsersCollection = DBController.GetUsers();
        }

        private RelayCommand _Back;
        public RelayCommand Back
        {
            get => _Back ?? (_Back = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                AuthorizationWindow InstanceAuthorizationWindow = new AuthorizationWindow();
                InstanceAuthorizationWindow.Show();
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

        private RelayCommand _OpenAddResultWindow;
        public RelayCommand OpenAddResultWindow
        {
            get => _OpenAddResultWindow ?? (_OpenAddResultWindow = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                AddResultWindow InstanceAddResultWindow = new AddResultWindow();
                InstanceAddResultWindow.Show();
                ThisWindow.Close();
            }));
        }

        private RelayCommand _OpenChangeResultInfoWindow;
        public RelayCommand OpenChangeResultWindow
        {
            get => _OpenChangeResultInfoWindow ?? (_OpenChangeResultInfoWindow = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                ChangeResultsInfoWindow InstanceChangeResultInfoWindow = new ChangeResultsInfoWindow();
                InstanceChangeResultInfoWindow.DataContext = new ChangeResultInfoWindowVM(SelectedResult);
                InstanceChangeResultInfoWindow.Show();
                ThisWindow.Close();
            }));
        }

        private RelayCommand _OpenAddUserWindow;
        public RelayCommand OpenAddUserWindow
        {
            get => _OpenAddUserWindow ?? (_OpenAddUserWindow = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                AddUserWindow InstanceAddUserWindow = new AddUserWindow();
                InstanceAddUserWindow.Show();
                ThisWindow.Close();
            }));
        }

        private RelayCommand _OpenChangeUserInfoWindow;
        public RelayCommand OpenChangeUserWindow
        {
            get => _OpenChangeUserInfoWindow ?? (_OpenChangeUserInfoWindow = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                ChangeUserInfoWindow InstanceChangeUserInfoWindow = new ChangeUserInfoWindow();
                InstanceChangeUserInfoWindow.DataContext = new ChangeUserInfoWindowVM(SelectedUser);
                InstanceChangeUserInfoWindow.Show();
                ThisWindow.Close();
            }));
        }

        private RelayCommand _DeleteResult;
        public RelayCommand DeleteResult
        {
            get => _DeleteResult ?? (_DeleteResult = new RelayCommand(obj =>
            {
                var Result = MessageBox.Show("Вы действительно хотите удалить эти данные?", "Удалённые данные нельзя восстановить!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    if(DBController.DeleteResult(SelectedResult.Id))
                    {
                        MessageBox.Show("Данные успешно удалены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Error);
                        ResultsCollection = DBController.GetResults();
                    }
                }
            }));
        }

        private RelayCommand _DeleteUser;
        public RelayCommand DeleteUser
        {
            get => _DeleteUser ?? (_DeleteUser = new RelayCommand(obj =>
            {
                var Result = MessageBox.Show("Вы действительно хотите удалить эти данные?", "Удалённые данные нельзя восстановить!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    if (DBController.DeleteUser(SelectedUser.Id))
                    {
                        MessageBox.Show("Данные успешно удалены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Error);
                        UsersCollection = DBController.GetUsers();
                    }
                }
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
