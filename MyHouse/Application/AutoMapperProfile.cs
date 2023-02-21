using System.Collections.Generic;
using AutoMapper;
using Contract.Departments;
using Contract.DocumentFiles;
using Contract.FileFolders;
using Contract.FileTypes;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.IssuingAgencys;
using Contract.Positions;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Contract.UnitTypes;
using Domain.Departments;
using Domain.DocumentFiles;
using Domain.FileFolders;
using Domain.FileTypes;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.IssuingAgencys;
using Domain.Positions;
using Domain.UnitReviewDetails;
using Domain.UnitReviews;
using Domain.Units;
using Domain.UnitTypes;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserWithNavigationProperties, UserWithNavigationPropertiesDto>().ReverseMap();
            CreateMap<CreateUserDto,User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<CreateUpdateUseDto, CreateUserDto>();
            CreateMap<CreateUpdateUseDto, UpdateUserDto>();
            CreateMap<User,UserDto>().ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<CreateUpdateUseDto, User>().ReverseMap();
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

            
            
            CreateMap<Position,PositionDto>().ReverseMap();
            CreateMap<CreateUpdatePositionDto,Position>().ReverseMap();


            CreateMap<Department,DepartmentDto>().ReverseMap();
            CreateMap<CreateUpdateDepartmentDto,Department>().ReverseMap();
            
            
            CreateMap<FileFolder,FileFolderDto>().ReverseMap();
            CreateMap<CreateUpdateFileFolderDto,FileFolder>().ReverseMap();
            
            CreateMap<FileType,FileTypeDto>().ReverseMap();
            CreateMap<CreateUpdateFileTypeDto,FileType>().ReverseMap();
            
                        
            CreateMap<IssuingAgency,IssuingAgencyDto>().ReverseMap();
            CreateMap<CreateUpdateIssuingAgencyDto,IssuingAgency>().ReverseMap();

            CreateMap<DocumentFile, DocumentFileDto>().ReverseMap();
            CreateMap<CreateUpdateDocumentFileDto, DocumentFile>().ReverseMap();


            CreateMap<DocumentFileWithNavProperties, DocumentFileWithNavPropertiesDto>().ReverseMap();
        }            

    }
}