using AutoMapper;
using Books.API.Entities;
using Books.API.Models;
using Books.API.Models.External;

namespace Books.API.Profiles
{
    public class StoryProfile : Profile
    {
        public StoryProfile()
        {
            CreateMap<StoryDto, Story>()
             //.ForMember(dest => dest.AuthorName, 
             //opt => opt.MapFrom(src =>
             //    $"{src.Author.FirstName} {src.Author.LastName}"))
             .ConstructUsing(src =>
                        new Story(
                            src.Title,
                            src.Url,
                            src.By,
                            src.Time,
                            src.Score,
                            src.Descendants
                            )
                        );

       
    }
    }
}
