﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlazorDateRangePicker;
using Blazorise;
using Contract;
using Contract.Dashboards;
using Core.Enum;
using Core.Extension;
using FluentDateTimeOffset;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Radzen;
using Radzen.Blazor;
using WebClient.Exceptions;

namespace WebClient.Shared
{

    public abstract  class BaseBlazorPage : ComponentBase
    {
        
          [Inject] private NavigationManager _navigationManager { get; set;}
          [Inject] private NotificationService _notificationService { get; set;}


        protected IMapper ObjectMapper { get;}
        
        public BaseBlazorPage()
        {
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            ObjectMapper = config.CreateMapper();
            
        }
        

        public async Task<bool> InvokeAsync(Func<Task> action, ActionType type, bool showNotification = false)
        {
         
            try
            {
                await action();

                if (showNotification)
                {
                    switch (type)
                    {
                        case ActionType.Create:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Tạo Thành Công", Duration = 4000 });
                            break;
                        }
                        case ActionType.Update:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Cập Nhật Thành cộng", Duration = 4000 });
                            break;
                        }
                        case ActionType.Get:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Lấy về Thành cộng", Duration = 4000 });
                            break;
                        }
                        case ActionType.GetList:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Lấy về Thành cộng", Duration = 4000 });
                            break;
                        }
                        case ActionType.Delete:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Xóa Thành cộng", Duration = 4000 });
                            break;
                        }
                        case ActionType.SignIn:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Sign In Success", Duration = 4000 });
                            break;
                        }
                        case ActionType.SignOut:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Sign Out Success", Duration = 4000 });
                            break;
                        }
                        case ActionType.SignUp:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Sign Up Success", Duration = 4000 });
                            break;
                        }
                        
                        case ActionType.Review:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Đánh giá thành công", Duration = 4000 });
                            break;
                        }
                        
                        case ActionType.UploadFile:
                        {
                            _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Upload File Succeeded ", Duration = 4000 });
                            break;
                        }
                    
                    }
                }
                
                
                return true;
            }
            catch (Exception e)
            {
                var exceptionType = e.GetType();

                if (exceptionType == typeof(BadRequestException))
                {
                    _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error Summary", Detail = e.Message, Duration = 4000});

                }
                if (exceptionType == typeof(UnauthorizedException))
                {
                    _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = "your token is so old. please log in again to get a new token. 4s later you automatically logout", Detail = e.Message, Duration = 4000});
                    Thread.Sleep(4000);
                    _navigationManager.NavigateTo("login",true);
                }
                if (exceptionType == typeof(ServerErrorException))
                {
                    _navigationManager.NavigateTo("server-error",true);
                }

                if (exceptionType == typeof(DbConnectionException))
                {
                    _navigationManager.NavigateTo("connection-error",true);
                }

                if (exceptionType == typeof(ConflictException))
                {
                    _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = "", Detail = e.Message, Duration = 4000});
                }

                if (exceptionType == typeof(TooManyRequests))
                {
                    // too many request
                }

                if (exceptionType == typeof(NotFoundFile))
                {
                    _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = "Not Found File", Detail = e.Message, Duration = 4000});
                }
                
                if (exceptionType == typeof(FailedOperation))
                {
                    _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = "The Failed Operation", Detail ="The Failed Operation", Duration = 4000});

                }
                
                // _navigationManager.NavigateTo("server-error",true);

            }

            return false;

        }

        public void NotifyMessage(NotificationSeverity type,string message,int duration)
        {
            _notificationService.Notify(new NotificationMessage { Severity = type, Summary = message, Duration = duration });
        }


        protected async Task<Dictionary<string, DateRange>> GetDateRangePickers()
        {
            
            var now = new DateTimeOffset(DateTime.Now.Date);
            
          var ranges =  new Dictionary<string, DateRange>()
          {
              {
                  DateRangeType.Today.GetDescriptionOrName(),
                  new DateRange()
                  {
                      Start = now,
                      End = now.Add(new TimeSpan(23,59,59))
                  }
              },
              {
                  DateRangeType.Yesterday.GetDescriptionOrName(),
                  new DateRange()
                  {
                      Start = now.AddDays(-1),
                      End = now.AddDays(-1).Add(new TimeSpan(23,59,59))
                  }
              },
              {
                  DateRangeType.Last7Days.GetDescriptionOrName(),
                  new DateRange()
                  {
                      Start = now.AddDays(-7).Add(new TimeSpan(23,59,59)),
                      End = now
                  }
              },
              {
                  DateRangeType.Last30Days.GetDescriptionOrName(),
                  new DateRange()
                  {
                      Start = now.AddDays(-30).Add(new TimeSpan(23,59,59)),
                      End = now
                  }
              },
              {
                  DateRangeType.ThisMonth.GetDescriptionOrName(),
                  new DateRange()
                  {
                      Start = now.FirstDayOfMonth(),
                      End = now.LastDayOfMonth()
                  }
              },
              {
                  DateRangeType.LastMonth.GetDescriptionOrName(),
                  new DateRange()
                  {
                      Start = now.AddMonths(-1).FirstDayOfMonth(),
                      End = now.AddMonths(-1).LastDayOfMonth()
                  }
              }
              
          };

          
          

          return ranges;

        }

        protected (DateTime?,DateTime?) GetDateTimeFromOffSet(DateTimeOffset? fromDateOffset, DateTimeOffset? toDateTimeOffset)
        {
            if (!fromDateOffset.HasValue || !toDateTimeOffset.HasValue) return (null, null);
            return (fromDateOffset.Value.DateTime, toDateTimeOffset.Value.DateTime);
        }

     
        
        
        
    }
}