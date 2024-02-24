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
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
