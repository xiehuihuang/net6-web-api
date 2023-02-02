using AutoMapper;
using Project.DbModels;
using Project.DTO;
using Project.Framework;

namespace Project.Interfaces
{
    public class AutoMapObject : Profile
    {
        public AutoMapObject()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<PagingData<User>, PagingData<UserInfoDTO>>();

            //CreateMap<User, UserApplyDTO>()
            // .ForMember(u => u.UserId, x => x.MapFrom(u => u.Id))
            // .ReverseMap();
        }
    }
}
