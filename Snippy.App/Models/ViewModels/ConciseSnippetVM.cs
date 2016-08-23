using System.Collections.Generic;

namespace Snippy.App.Models.ViewModels
{
    public class ConciseSnippetVM
    {
        public ConciseSnippetVM()
        {
            this.Labels = new List<string>();
        }

        public int SnippetId { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Labels { get; set; }
    }
}