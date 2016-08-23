using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snippy.Models
{
    public class Language
    {
        public Language()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        [Key]
        public int LanguageId { get; set; }

        [Required]
        [Index("idx_LanguageName", IsUnique = true)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}