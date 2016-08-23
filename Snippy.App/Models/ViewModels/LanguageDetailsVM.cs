using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.App.Models.ViewModels
{
    public class LanguageDetailsVM
    {
        public LanguageDetailsVM()
        {
            this.Snippets = new List<ConciseSnippetVM>();
        }

        public string Language { get; set; }

        public IEnumerable<ConciseSnippetVM> Snippets { get; set; }
    }
}
