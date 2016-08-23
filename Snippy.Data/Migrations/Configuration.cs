namespace Snippy.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Snippy.Data.SnippyModel>
    {
        private IList<ApplicationUser> users;
        private IList<Snippet> snippets;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Snippy.Data.SnippyModel context)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }

            if (!context.Languages.Any())
            {
                this.SeedLanguages(context);
            }

            if (!context.Labels.Any())
            {
                this.SeedLabels(context);
            }

            if (!context.Snippets.Any())
            {
                this.SeedSnippets(context);
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
            }
        }

        private void SeedComments(SnippyModel context)
        {
            this.snippets = context.Snippets.ToList();
            this.users = context.Users.ToList();

            var comment = CreateComment("Now that's really funny! I like it!", "30.10.2015 11:50:38", "Ternary Operator Madness", "admin");

            context.Comments.Add(comment);

            comment = CreateComment("Here, have my comment!", "01.11.2015 15:52:42", "Ternary Operator Madness", "newUser");

            context.Comments.Add(comment);

            comment = CreateComment("I didn't manage to comment first :(", "02.11.2015 05:30:20", "Ternary Operator Madness", "someUser");

            context.Comments.Add(comment);

            comment = CreateComment("That's why I love Python - everything is so simple!", "27.10.2015 15:28:14", "Reverse a String", "newUser");

            context.Comments.Add(comment);

            comment = CreateComment("I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject", "29.10.2015 15:08:42", "Points Around A Circle For GameObject Placement", "someUser");

            context.Comments.Add(comment);

            comment = CreateComment("Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.", "03.11.2015 12:56:20", "Numbers only in an input field", "admin");

            context.Comments.Add(comment);

            context.SaveChanges();
        }

        private Comment CreateComment(string content, string creationDate, string snippetTitle, string authorName)
        {
            return new Comment()
            {
                Content = content,
                CreationTime = DateTime.ParseExact(creationDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Snippet = this.snippets.First(s => s.Title == snippetTitle),
                Author = this.users.First(u => u.UserName == authorName)
            };
        }

        private void SeedSnippets(SnippyModel context)
        {
            var authors = context.Users.ToList();

            var snippet = new Snippet()
            {
                Title = "Ternary Operator Madness",
                Description = "This is how you DO NOT user ternary operators in C#!",
                Code = "bool X = Glob.UserIsAdmin ? true : false;",
                CreationDate = new DateTime(2015, 10, 26, 17, 20, 33),
                Language = context.Languages.First(l => l.Name == "C#"),
                Labels = new HashSet<Label>(context.Labels.Where(l => l.Text == "funny")),
                Author = authors.First(a => a.UserName == "admin")
            };

            context.Snippets.Add(snippet);

            snippet = new Snippet()
            {
                Title = "Points Around A Circle For GameObject Placement",
                Description = "Determine points around a circle which can be used to place elements around a central point",
                Code = $"private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint) {Environment.NewLine} {{ float ptRatio = currentPoint / totalPoints; {Environment.NewLine} float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius; {Environment.NewLine} float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius; {Environment.NewLine} Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f); {Environment.NewLine} return panelCenter; {Environment.NewLine} }} ",
                CreationDate = new DateTime(2015, 10, 26, 20, 15, 30),
                Language = context.Languages.First(l => l.Name == "C#"),
                Labels = new HashSet<Label>(context.Labels.Where(l => l.Text == "geometry" || l.Text == "games")),
                Author = authors.First(a => a.UserName == "admin")
            };

            context.Snippets.Add(snippet);

            var labels = new List<string>()
            {
                "jquery",
                "useful",
                "web",
                "front-end"
            };

            snippet = new Snippet()
            {
                Title = "forEach. How to break?",
                Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                Code = $"var ary = [\"JavaScript\", \"Java\", \"CoffeeScript\", \"TypeScript\"]; {Environment.NewLine} ary.some(function(value, index, _ary) {{ {Environment.NewLine} console.log(index + \": \" + value); {Environment.NewLine} return value === \"CoffeeScript\"; {Environment.NewLine} }}); {Environment.NewLine} // output: {Environment.NewLine} // 0: JavaScript {Environment.NewLine} // 1: Java {Environment.NewLine} // 2: CoffeeScript {Environment.NewLine} ary.every(function(value, index, _ary) {{ {Environment.NewLine} console.log(index + \": \" + value); {Environment.NewLine} return value.indexOf(\"Script\") > -1; {Environment.NewLine} }}); {Environment.NewLine} // output: {Environment.NewLine} // 0: JavaScript {Environment.NewLine} // 1: Java",
                CreationDate = new DateTime(2015, 10, 27, 10, 53, 20),
                Language = context.Languages.First(l => l.Name == "JavaScript"),
                Labels = new HashSet<Label>(context.Labels.Where(l => labels.Contains(l.Text))),
                Author = authors.First(a => a.UserName == "newUser")
            };

            context.Snippets.Add(snippet);

            labels = new List<string>()
            {
                "web",
                "front-end"
            };

            snippet = new Snippet()
            {
                Title = "Numbers only in an input field",
                Description = "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                Code = $"$(\"#input\").keypress(function(event){{ {Environment.NewLine} var charCode = (event.which) ? event.which : window.event.keyCode; {Environment.NewLine} if (charCode <= 13) {{ {Environment.NewLine} return true; {Environment.NewLine} }} else {{ {Environment.NewLine} var keyChar = String.fromCharCode(charCode); {Environment.NewLine} var regex = new RegExp(\"[0-9,.-]\"); {Environment.NewLine} return regex.test(keyChar); {Environment.NewLine} }} {Environment.NewLine} }}); ",
                CreationDate = new DateTime(2015, 10, 28, 9, 0, 56),
                Language = context.Languages.First(l => l.Name == "JavaScript"),
                Labels = new HashSet<Label>(context.Labels.Where(l => labels.Contains(l.Text))),
                Author = authors.First(a => a.UserName == "someUser")
            };

            context.Snippets.Add(snippet);

            labels = new List<string>()
            {
                "bug",
                "funny",
                "mysql"
            };

            snippet = new Snippet()
            {
                Title = "Create a link directly in an SQL query",
                Description = "That's how you create links - directly in the SQL!",
                Code = $"SELECT DISTINCT {Environment.NewLine} b.Id, {Environment.NewLine} concat('<button type=\"\"button\"\" onclick=\"\"DeleteContact(', cast(b.Id as char), ')\"\">Delete...</button>') as lnkDelete {Environment.NewLine} FROM tblContact b {Environment.NewLine} WHERE .... ",
                CreationDate = new DateTime(2015, 10, 30, 11, 20, 0),
                Language = context.Languages.First(l => l.Name == "SQL"),
                Labels = new HashSet<Label>(context.Labels.Where(l => labels.Contains(l.Text))),
                Author = authors.First(a => a.UserName == "admin")
            };

            context.Snippets.Add(snippet);

            labels = new List<string>()
            {
                "useful"
            };

            snippet = new Snippet()
            {
                Title = "Reverse a String",
                Description = "Almost not worth having a function for...",
                Code = $"def reverseString(s): {Environment.NewLine} \"\"\"Reverses a string given to it.\"\"\" {Environment.NewLine} return s[::-1] ",
                CreationDate = new DateTime(2015, 10, 26, 9, 35, 13),
                Language = context.Languages.First(l => l.Name == "Python"),
                Labels = new HashSet<Label>(context.Labels.Where(l => labels.Contains(l.Text))),
                Author = authors.First(a => a.UserName == "admin")
            };

            context.Snippets.Add(snippet);

            labels = new List<string>()
            {
                "web",
                "front-end"
            };

            snippet = new Snippet()
            {
                Title = "Pure CSS Text Gradients",
                Description = "This code describes how to create text gradients using only pure CSS",
                Code = $"/* CSS text gradients */ {Environment.NewLine} h2[data-text] {{ {Environment.NewLine} position: relative; {Environment.NewLine} }} {Environment.NewLine} h2[data-text]::after {{ {Environment.NewLine} content: attr(data-text); {Environment.NewLine} z-index: 10; {Environment.NewLine} color: #e3e3e3; {Environment.NewLine} position: absolute; {Environment.NewLine} top: 0; {Environment.NewLine} left: 0; {Environment.NewLine} -webkit-mask-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0,0,0,0)), color-stop(50%, rgba(0,0,0,1)), to(rgba(0,0,0,0))); ",
                CreationDate = new DateTime(2015, 10, 22, 19, 26, 42),
                Language = context.Languages.First(l => l.Name == "CSS"),
                Labels = new HashSet<Label>(context.Labels.Where(l => labels.Contains(l.Text))),
                Author = authors.First(a => a.UserName == "someUser")
            };

            context.Snippets.Add(snippet);

            labels = new List<string>()
            {
                "funny"
            };

            snippet = new Snippet()
            {
                Title = "Check for a Boolean value in JS",
                Description = "How to check a Boolean value - the wrong but funny way :D",
                Code = $"var b = true; {Environment.NewLine} if (b.toString().length < 5) {{ {Environment.NewLine} //... {Environment.NewLine} }} ",
                CreationDate = new DateTime(2015, 10, 22, 5, 30, 4),
                Language = context.Languages.First(l => l.Name == "JavaScript"),
                Labels = new HashSet<Label>(context.Labels.Where(l => labels.Contains(l.Text))),
                Author = authors.First(a => a.UserName == "admin")
            };

            context.Snippets.Add(snippet);

            context.SaveChanges();
        }

        private void SeedLabels(SnippyModel context)
        {
            var labels = new List<string>()
            {
                "bug",
                "funny",
                "jquery",
                "mysql",
                "useful",
                "web",
                "geometry",
                "back-end",
                "games",
                "front-end"
            };

            foreach (var label in labels)
            {
                var newLabel = new Label()
                {
                    Text = label
                };

                context.Labels.Add(newLabel);
            }

            context.SaveChanges();
        }

        private void SeedLanguages(SnippyModel context)
        {
            var languages = new List<string>()
            {
                "C#",
                "JavaScript",
                "Python",
                "CSS",
                "SQL",
                "PHP"
            };

            foreach (var lang in languages)
            {
                var newLang = new Language()
                {
                    Name = lang
                };

                context.Languages.Add(newLang);
            }

            context.SaveChanges();
        }

        private void SeedUsers(SnippyModel context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = "Administrator" };
            roleManager.Create(role);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var userToInsert = new ApplicationUser { UserName = "admin", Email = "admin@snippy.softuni.com" };
            userManager.Create(userToInsert, "adminPass123");
            userManager.AddToRole(userToInsert.Id, "Administrator");

            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            userToInsert = new ApplicationUser { UserName = "newUser", Email = "new_user@gmail.com" };
            userManager.Create(userToInsert, "userPass123");

            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            userToInsert = new ApplicationUser { UserName = "someUser", Email = "someUser@example.com" };
            userManager.Create(userToInsert, "someUserPass123");
        }
    }
}
