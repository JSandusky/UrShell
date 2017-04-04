using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho {
    [Serializable]
    public class BaseClass : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        public virtual void OnPropertyChanged() {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        // Create the OnPropertyChanged method to raise the event 
        public virtual void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
            if (UrhoSettings.inst().AutoSave)
                Save();
        }

        public virtual void Save() {
        }
    }

    public class NamedBaseClass : BaseClass {
        protected string name_;

        public string Name { get { return name_; } set { name_ = value; OnPropertyChanged("Name"); } }
    }
}
