using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Monitoring.Models
{
    public class Results : INotifyPropertyChanged
    {
        private int _ID;
        public int Id
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(nameof(Id)); }
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

        private bool _Result;
        public bool Result
        {
            get { return _Result; }
            set { _Result = value; OnPropertyChanged(nameof(Result)); }
        }

        private int _WhoContributed;
        public int WhoContributed
        {
            get { return _WhoContributed; }
            set { _WhoContributed = value; OnPropertyChanged(nameof(WhoContributed)); }
        }

        private int _WhoChangedIt;
        public int WhoChangedIt
        {
            get { return _WhoChangedIt; }
            set { _WhoChangedIt = value; OnPropertyChanged(nameof(WhoChangedIt)); }
        }

        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(nameof(Date)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
