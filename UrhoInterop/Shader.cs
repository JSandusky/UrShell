using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho {
    public enum ShaderType {
        VERTEX,
        PIXEL
    }
    [ResourceType(new string[] {"Shader"})]
    public class Shader : NamedBaseClass {

        string file_;
        public string File { get { return file_; } set { file_ = value; OnPropertyChanged("File"); } }

        ShaderType type_;
        public ShaderType Type { get { return type_; } set { type_ = value; OnPropertyChanged("Type"); } }
    }
}
