using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.MessageDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateMessageDto dto)
        {
            var message = _mapper.Map<Message>(dto);
            message.Date = DateTime.UtcNow;
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);
            if (message == null) return;

            message.IsDeleted = true;
            _unitOfWork.Messages.Update(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultMessageDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Messages.GetAllAsync();
            return _mapper.Map<List<ResultMessageDto>>(values);
        }

        public async Task<ResultMessageDto?> GetByIdAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);
            return _mapper.Map<ResultMessageDto?>(message);
        }

        public async Task UpdateAsync(UpdateMessageDto dto)
        {
            var entity = await _unitOfWork.Messages.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);
            _unitOfWork.Messages.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultMessageDto>> GetInboxAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetInboxMessagesAsync(userId);
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }

        public async Task<List<ResultMessageDto>> GetSentAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetSentMessagesAsync(userId);
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }

        public async Task<List<ResultMessageDto>> GetDraftsAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetDraftMessagesAsync(userId);
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }

        public async Task<List<ResultMessageDto>> GetDeletedAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetDeletedMessagesAsync(userId);
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }

        public async Task<List<ResultMessageDto>> GetSpamAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetSpamMessagesAsync(userId);
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }
    }
}
