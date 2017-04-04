using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    public interface ISceneDocument
    {
        event EventHandler SceneChanged;

        void ExecuteScript(string code, UrhoBackend.VariantVector args);
    }
}
