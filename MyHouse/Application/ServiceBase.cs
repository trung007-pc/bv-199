using System.Threading.Tasks;
using AutoMapper;
using Contract;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Microsoft.AspNetCore.Mvc;
using SqlServ4r.Repository;

namespace Application
{
    public class ServiceBase
    {
        protected IMapper ObjectMapper { get;}
        
        public ServiceBase()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            
            ObjectMapper = config.CreateMapper();
        }


        public void AttachIndex(dynamic inputs)
        {
            var index = 1;
            foreach (var item in inputs)
            {
                item.Index = index;
                index++;
            }
            
        }

        
    }



 
}