using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snippy.Models
{
    public class Comment
    {
        public Comment()
        {
            this.CreationTime = DateTime.Now;
        }

        [Key]
        public int CommentId { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [ForeignKey("Author")]
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("Snippet")]
        public int SnippetId { get; set; }

        public virtual Snippet Snippet { get; set; }
    }
}