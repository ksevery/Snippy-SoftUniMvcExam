using Snippy.Data.Repositories;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Data.UnitOfWork
{
    public interface ISnippyData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Snippet> Snippets { get; }

        IRepository<Label> Labels { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Language> Languages { get; }

        int SaveChanges();
    }
}
