using AutoMapper;
using OnlineShopping.Model.Models;
using OnlineShopping.Web.Models;

namespace OnlineShopping.Web.Mappings
{
    public class AutoMapperCofiguration
    {
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
            });
        }
    }
}