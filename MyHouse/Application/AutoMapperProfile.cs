using AutoMapper;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.UnitReviewDetails;
using Domain.UnitReviews;
using Domain.Units;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUpdateUserDto,User>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Role,RoleDto>().ReverseMap();
            CreateMap<RoleDto,CreateUpdateRoleDto>().ReverseMap();
            CreateMap<Role,CreateUpdateRoleDto>().ReverseMap();
            
            CreateMap<Unit,CreateUpdateUnitDto>().ReverseMap();
            CreateMap<Unit,UnitDto>().ReverseMap();
            CreateMap<CreateUpdateUnitDto,UnitDto>().ReverseMap();

            CreateMap<CreateUpdateUnitReviewDto, UnitReview>().ReverseMap();
            CreateMap<UnitReview, UnitReviewDto>().ReverseMap();
            CreateMap<UnitReviewDetailDto, UnitReviewDetail>().ReverseMap();
            CreateMap<CreateUpdateUnitReviewDetailDto, UnitReviewDetail>().ReverseMap();

        }
    }
}