using AutoMapper;
using Snippy.App.Models.BindingModels;
using Snippy.App.Models.ViewModels;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.App.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Snippet, ConciseSnippetVM>()
                .ForMember(vm => vm.Labels, opt => opt.MapFrom(m => m.Labels.Select(l => l.Text)));

            Mapper.CreateMap<Comment, ConciseCommentVM>()
                .ForMember(vm => vm.AuthorName, opt => opt.MapFrom(m => m.Author.UserName))
                .ForMember(vm => vm.SnippetTitle, opt => opt.MapFrom(m => m.Snippet.Title))
                .ForMember(vm => vm.CreationDate, opt => opt.MapFrom(m => m.CreationTime));

            Mapper.CreateMap<Label, ConciseLabelVM>()
                .ForMember(vm => vm.SnippetCount, opt => opt.MapFrom(l => l.Snippets.Count));

            Mapper.CreateMap<Snippet, FullSnippetVM>()
                .ForMember(vm => vm.Labels, opt => opt.MapFrom(m => Mapper.Map<IEnumerable<ConciseLabelVM>>(m.Labels)))
                .ForMember(vm => vm.Comments, 
                        opt => opt.MapFrom(s => 
                            Mapper.Map<IEnumerable<ConciseCommentVM>>(s.Comments.OrderByDescending(c => c.CreationTime))))
                .ForMember(vm => vm.AuthorName, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(vm => vm.Language, opt => opt.MapFrom(s => s.Language.Name));

            Mapper.CreateMap<Label, LabelDetailsVM>()
                .ForMember(vm => vm.Label, opt => opt.MapFrom(l => l.Text))
                .ForMember(vm => vm.Snippets, opt => opt.MapFrom(l => Mapper.Map<IEnumerable<ConciseSnippetVM>>(l.Snippets)));

            Mapper.CreateMap<Language, LanguageDetailsVM>()
                .ForMember(vm => vm.Language, opt => opt.MapFrom(l => l.Name))
                .ForMember(vm => vm.Snippets, opt => opt.MapFrom(l => Mapper.Map<IEnumerable<ConciseSnippetVM>>(l.Snippets)));

            Mapper.CreateMap<Snippet, CreateSnippetBM>()
                .ForMember(vm => vm.Language, opt => opt.MapFrom(s => s.Language.Name))
                .ForMember(vm => vm.Labels, opt => opt.MapFrom(s => string.Join("; ", s.Labels.Select(l => l.Text))));
        }
    }
}
