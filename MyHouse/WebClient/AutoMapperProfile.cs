using AutoMapper;
using Contract.Departments;
using Contract.DocumentFiles;
using Contract.FileFolders;
using Contract.FileTypes;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.IssuingAgencys;
using Contract.Notifications;
using Contract.Positions;
using Contract.SendingFiles;
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
            CreateMap<UserDto, CreateUserDto>().ReverseMap();
            CreateMap<UserDto, UpdateUserDto>().ReverseMap();
            CreateMap<UserWithNavigationPropertiesDto, CreateUserDto>().ReverseMap();

            CreateMap<CreateUpdateUnitDto,UnitDto>().ReverseMap();
            CreateMap<UnitReviewDetailDto, CreateUpdateUnitReviewDetailDto>().ReverseMap();

            CreateMap<UnitReviewDto, CreateUpdateUnitReviewDto>().ReverseMap();
            CreateMap<UnitWithNavPropertiesDto, UnitReview.ReviewWithNavPropertiesModel>();

            CreateMap<UserDto, CreateUserDto>().ReverseMap();

            CreateMap<UnitReview.ReviewWithNavPropertiesModel, CreateUpdateUnitReviewDto>();

            CreateMap<UnitTypeDto,CreateUpdateUnitTypeDto>().ReverseMap();
            

            CreateMap<CreateUpdatePositionDto, PositionDto>().ReverseMap();

            CreateMap<DepartmentDto, CreateUpdateDepartmentDto>().ReverseMap();

            CreateMap<FileFolderDto, CreateUpdateFileFolderDto>().ReverseMap();

            CreateMap<FileTypeDto, CreateUpdateFileTypeDto>().ReverseMap();
            CreateMap<IssuingAgencyDto, CreateUpdateIssuingAgencyDto>().ReverseMap();
            CreateMap<DocumentFileDto, CreateUpdateDocumentFileDto>().ReverseMap();

            CreateMap<NotificationDto, UpdateNotification>().ReverseMap();
            CreateMap<SendingFileDto, CreateUpdateSendingFileDto>().ReverseMap();

        }
    }
}