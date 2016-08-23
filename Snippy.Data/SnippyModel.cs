namespace Snippy.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Migrations;

    public class SnippyModel : IdentityDbContext<ApplicationUser>
    {
        public SnippyModel()
            : base("SnippyModel", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SnippyModel, Configuration>());
        }

        public virtual IDbSet<Snippet> Snippets { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Label> Labels { get; set; }

        public virtual IDbSet<Language> Languages { get; set; }

        public static SnippyModel Create()
        {
            return new SnippyModel();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snippet>()
                .HasRequired(s => s.Author)
                .WithMany(a => a.Snippets)
                .HasForeignKey(s => s.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Label>()
                .HasMany<Snippet>(l => l.Snippets)
                .WithMany(s => s.Labels);

            base.OnModelCreating(modelBuilder);
        }
    }
}