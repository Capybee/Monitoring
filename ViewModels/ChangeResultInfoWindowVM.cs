using Microsoft.IdentityModel.Tokens;
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
    public class ChangeResultInfoWindowVM : INotifyPropertyChanged
    {
        int _SelectedId = 0;

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

        private bool _Issued;
        public bool Issued
        {
            get { return _Issued; }
            set { _Issued = value; OnPropertyChanged(nameof(Issued)); }
        }

        private bool _NotIssued;
        public bool NotIssued
        {
            get { return _NotIssued; }
            set { _NotIssued = value; OnPropertyChanged(nameof(NotIssued)); }
        }


        public ChangeResultInfoWindowVM(Results SelectedResult)
        {
            _SelectedId = SelectedResult.Id;
            Title = SelectedResult.Title;
            Description = SelectedResult.Description;
            Issued = SelectedResult.Result;
            if (!SelectedResult.Result) 
            {
                NotIssued = true;
            }
        }

        private RelayCommand _ChangeResult;
        public RelayCommand ChangeResult
        {
            get => _ChangeResult ?? (_ChangeResult = new RelayCommand(obj =>
            {
                if ((!Title.IsNullOrEmpty()) && (!Description.IsNullOrEmpty()))
                {
                    int WhoChange = 0; 

                    using(StreamReader Reader = new StreamReader("user.txt"))
                    {
                        WhoChange = Convert.ToInt32(Reader.ReadToEnd());
                    }

                    if(DBController.UpdateResult(_SelectedId, Title, Description, Issued, WhoChange))
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
