using AutoMapper;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Contract.UnitTypes;
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
            CreateMap<UnitWithNavPropertiesDto, UnitReview.ReviewWithNavPropertiesModel>();

            CreateMap<UserDto, UpdateUserNameWithNavDto>().ReverseMap();

            CreateMap<UnitReview.ReviewWithNavPropertiesModel, CreateUpdateUnitReviewDto>();

            CreateMap<UnitTypeDto,CreateUpdateUnitTypeDto>().ReverseMap();












            // CreateMap<A, C>().ReverseMap();
            //
            // CreateMap<B, E>().ForMember(dest=>dest.deptrai1,
            //     opt=>opt.MapFrom(source => source.deptrai)).ReverseMap();


        }
    }
}