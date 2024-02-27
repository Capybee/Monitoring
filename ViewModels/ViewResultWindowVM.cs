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
    public class ViewResultWindowVM : INotifyPropertyChanged
    {
        private string CallerWindow = "";

        private Results _Result;
        public Results Result
        {
            get { return _Result; }
            set { _Result = value; OnPropertyChanged(nameof(Result)); }
        }

        private ObservableCollection<Users> _UsersCollection;
        public ObservableCollection<Users> UsersCollection
        {
            get { return _UsersCollection; }
            set { _UsersCollection = value; }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged(nameof(Description)); }
        }

        private string _WhoContributed = "Внес информацию в систему: ";
        public string WhoContributed
        {
            get { return _WhoContributed; }
            set { _WhoContributed = value; OnPropertyChanged(nameof(WhoContributed)); }
        }

        private string _WhoChangeIt = "Последний вносил изменения: ";
        public string WhoChangeIt
        {
            get { return _WhoChangeIt; }
            set { _WhoChangeIt = value; OnPropertyChanged(nameof(WhoChangeIt)); }
        }

        public ViewResultWindowVM(Results InputResult, string TheWindowCausedIt)
        {
            Result = InputResult;
            CallerWindow = TheWindowCausedIt;
            UsersCollection = DBController.GetUsers();
            PreparationOfInformation();
        }

        private void PreparationOfInformation()
        {
            Title = Result.Title;
            Description = Result.Description;
            foreach (var User in UsersCollection) 
            {
                if (User.Id == Result.WhoContributed)
                {
                    WhoContributed += $"{User.Surname} {User.Name[0]}. {User.MidleName[0]}.";
                }
                if (User.Id == Result.WhoChangedIt)
                {
                    WhoChangeIt += $"{User.Surname} {User.Name[0]}. {User.MidleName[0]}.";
                }
            }
        }

        private RelayCommand _Back;
        public RelayCommand Back
        {
            get => _Back ?? (_Back = new RelayCommand(obj =>
            {
                if(CallerWindow == "Админ")
                {
                    Window ThisWindow = obj as Window;
                    AdminMainWindow InstanceAdminMainWindow = new AdminMainWindow();
                    InstanceAdminMainWindow.Show();
                    ThisWindow.Close();
                }
                else if (CallerWindow == "Пользователь")
                {
                    Window ThisWindow = obj as Window;
                    MainWindow InstanceMainWindow = new MainWindow();
                    InstanceMainWindow.Show();
                    ThisWindow.Close();
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
