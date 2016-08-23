using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.App.Models.ViewModels
{
    public class LabelDetailsVM
    {
        public LabelDetailsVM()
        {
            this.Snippets = new List<ConciseSnippetVM>();
        }

        public string Label { get; set; }

        public IEnumerable<ConciseSnippetVM> Snippets { get; set; }
    }
}
