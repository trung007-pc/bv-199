using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Contract.MeetingContents;
using Core.Const;
using Core.Exceptions;
using Domain.MeetingContents;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.MeetingContents;
using Volo.Abp.DependencyInjection;

namespace Application.MeetingContents
{
    public class MeetingContentService : ServiceBase,IMeetingContentService,ITransientDependency
    {
        private readonly MeetingContentRepository _unitTypeRepository;
        public MeetingContentService(MeetingContentRepository unitTypeRepository)
        {
            _unitTypeRepository = unitTypeRepository;
        }
        
        public async Task<List<MeetingContentDto>> GetListAsync()
        {
            var contents = await _unitTypeRepository.GetListAsync();
                var contentsDto = ObjectMapper.Map<List<MeetingContent>, List<MeetingContentDto>>(contents);
            return contentsDto;
        }
        
        public async Task<MeetingContentDto> CreateAsync(CreateUpdateMeetingContentDto input)
        {
            input.Name = input.Name.Trim();
            var content = ObjectMapper.Map<CreateUpdateMeetingContentDto, MeetingContent>(input);
            await _unitTypeRepository.AddAsync(content);
            return ObjectMapper.Map<MeetingContent, MeetingContentDto>(content);
        }

        public async Task<MeetingContentDto> UpdateAsync(CreateUpdateMeetingContentDto input, Guid id)
        {
            input.Name = input.Name.Trim();
            var item = await _unitTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            
            var content = ObjectMapper.Map(input,item);
            _unitTypeRepository.Update(content);
            
            return ObjectMapper.Map<MeetingContent, MeetingContentDto>(content);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var item = await _unitTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            _unitTypeRepository.Remove(item);
        }
    }
}