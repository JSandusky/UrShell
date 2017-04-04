using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Interfaces
{
    public interface ISearchResult
    {
        string SourceName { get; }
        string Location {get;}
        string Detail {get;}
        
        void GoTo();
    }

    public interface ISearchable
    {
        void Search(string[] terms, IList<ISearchResult> results);
    }
}
