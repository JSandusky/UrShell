using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho {
    public class UrhoSettings {
        static UrhoSettings inst_;

        private UrhoSettings() { }

        public static UrhoSettings inst() {
            if (inst_ == null) {
                inst_ = new UrhoSettings();
                inst_.AutoSave = false; //default as false
            }
            return inst_;
        }

        public bool AutoSave { get; set; }
    }
}
