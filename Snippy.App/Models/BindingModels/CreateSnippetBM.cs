using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.App.Models.BindingModels
{
    public class CreateSnippetBM
    {
        public int SnippetId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Labels { get; set; }
    }
}
