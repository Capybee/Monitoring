using Monitoring.Models;
using Monitoring.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monitoring.ViewModels
{
    public class AutorizationWindowVM : INotifyPropertyChanged
    {
        private ObservableCollection<Users> _UsersCollection;
        public ObservableCollection<Users> UsersCollection
        {
            get { return _UsersCollection; }
            set { _UsersCollection = value; OnPropertyChanged(nameof(UsersCollection)); }
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

        public AutorizationWindowVM()
        {
            UsersCollection = DBController.GetUsers();
        }

        private RelayCommand _Authorization;
        public RelayCommand Authorization
        {
            get => _Authorization ?? (_Authorization = new RelayCommand(obj => {

                if (Login != null && Password != null) 
                {
                    foreach (var User in UsersCollection)
                    {
                        if (Login == User.Login && Password == User.Password)
                        {
                            if (User.IsAdmin)
                            {
                                AdminMainWindow adminMainWindow = new AdminMainWindow();
                                adminMainWindow.Show();
                                Window ThisWindow = obj as Window;
                                ThisWindow.Close();

                                using(StreamWriter Writer = new StreamWriter("user.txt"))
                                {
                                    Writer.WriteLine(User.Id);
                                }
                            }
                            else
                            {
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                Window ThisWindow = obj as Window;
                                ThisWindow.Close();
                                using (StreamWriter Writer = new StreamWriter("user.txt"))
                                {
                                    Writer.WriteLine(User.Id);
                                }
                            }
                        }
                    }
                }
                else 
                {
                    MessageBox.Show("Поля 'Логин' и 'Пароль' не могут быть пустыми!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                } 
                    
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
