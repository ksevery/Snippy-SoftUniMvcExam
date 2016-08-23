using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snippy.Models
{
    public class Label
    {
        public Label()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        [Key]
        public int LabelId { get; set; }

        [Required]
        [Index("idx_LabelText", IsUnique = true)]
        [MinLength(1)]
        [MaxLength(200)]
        public string Text { get; set; }
        
        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}