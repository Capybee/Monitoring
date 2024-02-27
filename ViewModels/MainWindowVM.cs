using Monitoring.Models;
using Monitoring.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Monitoring.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private ObservableCollection<Results> _ResultsCollection;
        public ObservableCollection<Results> ResultsCollection
        {
            get { return _ResultsCollection; }
            set { _ResultsCollection = value; OnPropertyChanged(nameof(ResultsCollection)); }
        }

        private Results _SelectedResult;
        public Results SelectedResult
        {
            get { return _SelectedResult; }
            set { _SelectedResult = value; OnPropertyChanged(nameof(SelectedResult)); }
        }

        public MainWindowVM()
        {
            ResultsCollection = DBController.GetResults();
        }

        private RelayCommand _OpenViewResultWindow;
        public RelayCommand OpenViewResultWindow
        {
            get => _OpenViewResultWindow ?? (_OpenViewResultWindow = new RelayCommand(obj =>
            {
                Window ThisWindow = obj as Window;
                ViewResultWindow InstanceViewResultWindow = new ViewResultWindow();
                InstanceViewResultWindow.DataContext = new ViewResultWindowVM(SelectedResult, "Пользователь");
                InstanceViewResultWindow.Show();
                ThisWindow.Close();
            }));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
