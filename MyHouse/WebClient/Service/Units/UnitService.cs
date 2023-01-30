using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Units;
using Contract.UnitTypes;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebClient.RequestHttp;

namespace WebClient.Service.Units
{
    public class UnitService : IUnitService
    {
        private NotificationService _notificationService;
        private NavigationManager _navigationManager;
        public UnitService(NotificationService notificationService,
            NavigationManager navigationManager)
        {
            _notificationService = notificationService;
            _navigationManager = navigationManager;
        }

        
        public async Task<UnitDto> CreateAsync(CreateUpdateUnitDto input)
        {
            return  await  RequestClient.PostAPIAsync<UnitDto>("unit/",input);
        }

        public async Task<UnitDto> UpdateAsync(CreateUpdateUnitDto input, Guid id)
        {
            return  await  RequestClient.PutAPIAsync<UnitDto>($"unit/{id}",input);

        }
        

        public async Task DeleteAsync(Guid id)
        {
              await  RequestClient.DeleteAPIAsync<Task>($"unit/{id}");
        }

        public async Task<List<UnitWithNavPropertiesDto>> GetListWithNavPropertiesAsync(UnitFilter input)
        {
            return  await RequestClient.PostAPIAsync<List<UnitWithNavPropertiesDto>>
                ("unit/get-list-with-nav-properties",input);

        }

        public async Task<List<UnitDto>> GetListAsync()
        {
            return  await  RequestClient.GetAPIAsync<List<UnitDto>>("unit");
        }
        

        public async Task<List<UnitTypeDto>> LookUpUnitTypes()
        {
            return await  RequestClient.GetAPIAsync<List<UnitTypeDto>>("unit/look-up-unit-types");
        }
    }
}