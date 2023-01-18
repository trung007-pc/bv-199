using AutoMapper;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using WebClient.Pages.Admin;
using WebClient.Pages.Client;

namespace WebClient
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RoleDto, CreateUpdateRoleDto>().ReverseMap();
            CreateMap<UserDto, CreateUpdateUserDto>().ReverseMap();
            CreateMap<UserWithNavigationDto, CreateUpdateUserWithNavDto>().ReverseMap();

            CreateMap<CreateUpdateUnitDto,UnitDto>().ReverseMap();
            CreateMap<UnitReviewDetailDto, CreateUpdateUnitReviewDetailDto>().ReverseMap();

            CreateMap<UnitReviewDto, CreateUpdateUnitReviewDto>().ReverseMap();
            CreateMap<UnitDto, UnitReview.ReviewWithNavPropertiesModel>().ForMember(
                x => x.UnitId,
                y => y.MapFrom(x => x.Id)
            );

            CreateMap<UserDto, UpdateUserNameWithNavDto>().ReverseMap();

            CreateMap<UnitReview.ReviewWithNavPropertiesModel, CreateUpdateUnitReviewDto>();













            // CreateMap<A, C>().ReverseMap();
            //
            // CreateMap<B, E>().ForMember(dest=>dest.deptrai1,
            //     opt=>opt.MapFrom(source => source.deptrai)).ReverseMap();


        }
    }
}