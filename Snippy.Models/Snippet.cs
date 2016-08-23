using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snippy.Models
{
    public class Snippet
    {
        public Snippet()
        {
            this.Labels = new HashSet<Label>();
            this.Comments = new HashSet<Comment>();
            this.CreationDate = DateTime.Now;
        }

        [Key]
        public int SnippetId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        [Index("idx_SnippetTitle", IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("Language")]
        [Required]
        public int LanguageId { get; set; }
        
        public virtual Language Language { get; set; }

        [ForeignKey("Author")]
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Label> Labels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}