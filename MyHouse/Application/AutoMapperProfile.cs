using System.Collections.Generic;
using AutoMapper;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Contract.UnitTypes;
using Domain.Identity.Roles;
using Domain.Identity.UnitTypes;
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
            CreateMap<UnitWithNavProperties, UnitWithNavPropertiesDto>().ReverseMap();

            CreateMap<(Unit Unit, IEnumerable<UnitReviewDetail> UnitReviewDetails),
                (UnitDto Unit, IEnumerable<UnitReviewDetailDto> UnitReviewDetails)>().ReverseMap();

            CreateMap<CreateUpdateUnitReviewDto, UnitReview>().ReverseMap();
            CreateMap<UnitReview, UnitReviewDto>().ReverseMap();
            CreateMap<UnitReviewDetailDto, UnitReviewDetail>().ReverseMap();
            CreateMap<CreateUpdateUnitReviewDetailDto, UnitReviewDetail>().ReverseMap();

            CreateMap<CreateUpdateUnitTypeDto, UnitTypeDto>().ReverseMap();
            CreateMap<UnitType, UnitTypeDto>().ReverseMap();
            CreateMap<CreateUpdateUnitTypeDto, UnitType>().ReverseMap();

        }
    }
}