using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring.Models
{
    public class Reports : INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; OnPropertyChanged(nameof(Id)); }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string _Content;
        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(nameof(Content)); }
        }

        private int _WhoRequestedIt;
        public int WhoRequestedIt
        {
            get { return _WhoRequestedIt; }
            set { _WhoRequestedIt = value; OnPropertyChanged(nameof(WhoRequestedIt)); }
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
