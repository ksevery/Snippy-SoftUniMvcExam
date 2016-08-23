using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.App.Models.ViewModels
{
    public class HomePageVM
    {
        public HomePageVM()
        {
            this.Snippets = new List<ConciseSnippetVM>();
            this.Comments = new List<ConciseCommentVM>();
            this.Labels = new List<ConciseLabelVM>();
        }

        public IEnumerable<ConciseSnippetVM> Snippets { get; set; }

        public IEnumerable<ConciseCommentVM> Comments { get; set; }

        public IEnumerable<ConciseLabelVM> Labels { get; set; }
    }
}
