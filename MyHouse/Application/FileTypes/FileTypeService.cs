using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.FileTypes;
using Core.Const;
using Core.Exceptions;
using Domain.FileTypes;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.FileTypes;
using Volo.Abp.DependencyInjection;

namespace Application.FileTypes
{
    public class FileTypeService : ServiceBase,IFileTypeService,ITransientDependency
    {
         private readonly FileTypeRepository _fileTypeRepository;
        public FileTypeService(FileTypeRepository fileFolderRepository)
        {
            _fileTypeRepository = fileFolderRepository;
        }
        
        
        public async Task<FileTypeDto> CreateAsync(CreateUpdateFileTypeDto input)
        {   
            (input.Name, input.Code) = TrimText(input.Name, input.Code);

            var types = ObjectMapper.Map<CreateUpdateFileTypeDto, FileType>(input);
            await _fileTypeRepository.AddAsync(types);
            return ObjectMapper.Map<FileType,FileTypeDto>(types);
        }



        public async Task<FileTypeDto> UpdateAsync(CreateUpdateFileTypeDto input, Guid id)
        {
            (input.Name, input.Code) = TrimText(input.Name, input.Code);

            var item = await _fileTypeRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            var types = ObjectMapper.Map(input,item);
            await _fileTypeRepository.UpdateAsync(types);

            return ObjectMapper.Map<FileType,FileTypeDto>(types);
        }

 

        public async Task DeleteAsync(Guid id)
        {
            var types = await _fileTypeRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (types is null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            _fileTypeRepository.Remove(types);
        }

        public async Task<List<FileTypeDto>> GetListAsync()
        {
            var types = await _fileTypeRepository.GetQueryable()
                .OrderBy(x=>x.ODX).ToListAsync();
               
            return ObjectMapper.Map<List<FileType>, List<FileTypeDto>>(types);
        }
        
        private (string Name, string? Code) TrimText(string name, string code)
        {
            return (name.Trim(), code?.Trim().ToUpper());
        }
    }
}