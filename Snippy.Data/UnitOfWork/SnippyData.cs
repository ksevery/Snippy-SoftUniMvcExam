using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snippy.Data.Repositories;
using Snippy.Models;
using System.Data.Entity;

namespace Snippy.Data.UnitOfWork
{
    public class SnippyData : ISnippyData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public SnippyData()
            : this(new SnippyModel())
        {
        }

        public SnippyData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Label> Labels
        {
            get
            {
                return this.GetRepository<Label>();
            }
        }

        public IRepository<Language> Languages
        {
            get
            {
                return this.GetRepository<Language>();
            }
        }

        public IRepository<Snippet> Snippets
        {
            get
            {
                return this.GetRepository<Snippet>();
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
