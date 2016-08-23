using System;

namespace Snippy.App.Models.ViewModels
{
    public class ConciseCommentVM
    {
        public int CommentId { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public string SnippetTitle { get; set; }

        public DateTime CreationDate { get; set; }
    }
}