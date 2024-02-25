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
    public class AddResultWindowVM : INotifyPropertyChanged
    {
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

        private RelayCommand _AddResult;
        public RelayCommand AddResult
        {
            get => _AddResult ?? (_AddResult = new RelayCommand(obj =>
            {
                if ((!Title.IsNullOrEmpty()) && (!Description.IsNullOrEmpty()))
                {
                    int Creator = 0;

                    ObservableCollection<Results> ResultsCollection = new ObservableCollection<Results>();
                    ResultsCollection = DBController.GetResults();

                    using(StreamReader Reader = new StreamReader("user.txt"))
                    {
                        Creator = Convert.ToInt32(Reader.ReadToEnd());
                    }

                    if (DBController.AddResult(ResultsCollection[ResultsCollection.Count - 1].Id + 1,Title, Description, Issued, Creator, Creator, DateTime.Now.Date))
                    {
                        MessageBox.Show("Данные успешно внесены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Window ThisWindow = obj as Window;
                        AdminMainWindow InstanceAdminMainWindow = new AdminMainWindow();
                        InstanceAdminMainWindow.Show();
                        ThisWindow.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Поля 'Заголовок' и 'Описание' не могут быть пустыми, а также необходимо указать был ли выдан сертификат!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
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
