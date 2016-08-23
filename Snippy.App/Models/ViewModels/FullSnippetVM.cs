using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.App.Models.ViewModels
{
    public class FullSnippetVM
    {
        public FullSnippetVM()
        {
            this.Labels = new List<ConciseLabelVM>();
            this.Comments = new List<ConciseCommentVM>();
        }

        public int SnippetId { get; set; }

        public string Language { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<ConciseLabelVM> Labels { get; set; }

        public IEnumerable<ConciseCommentVM> Comments { get; set; }
    }
}
