using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.MessageDto;
using Learnify.Entity.Concrete;
using Learnify.Entity.Enums;
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

        public async Task<IResult> AddAsync(CreateMessageDto dto)
        {
            var message = _mapper.Map<Message>(dto);
            message.Date = DateTime.UtcNow;

            // Draft mı?
            message.Status = dto.IsDraft ? MessageStatus.Draft : MessageStatus.Sent;

            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(dto.IsDraft ? "Mesaj taslaklara kaydedildi." : "Mesaj gönderildi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);
            if (message == null)
                return new ErrorResult("Mesaj bulunamadı.");

            message.Status = MessageStatus.Deleted;

            _unitOfWork.Messages.Update(message);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Mesaj silindi.");
        }

        public async Task<IDataResult<List<ResultMessageDto>>> GetAllAsync()
        {
            var values = await _unitOfWork.Messages.GetAllAsync();
            return new SuccessDataResult<List<ResultMessageDto>>(
                _mapper.Map<List<ResultMessageDto>>(values)
            );
        }

        public async Task<IDataResult<ResultMessageDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Messages.GetByIdAsync(id);
            if (entity == null)
                return new ErrorDataResult<ResultMessageDto>("Mesaj bulunamadı.");

            return new SuccessDataResult<ResultMessageDto>(
                _mapper.Map<ResultMessageDto>(entity)
            );
        }

        public async Task<IResult> UpdateAsync(UpdateMessageDto dto)
        {
            var entity = await _unitOfWork.Messages.GetByIdAsync(dto.Id);
            if (entity == null)
                return new ErrorResult("Mesaj bulunamadı.");

            _mapper.Map(dto, entity);
            entity.Status = dto.Status; // direkt enum üzerinden

            _unitOfWork.Messages.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Mesaj güncellendi.");
        }


        public async Task<IDataResult<List<ResultMessageDto>>> GetInboxAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetInboxAsync(userId);
            return new SuccessDataResult<List<ResultMessageDto>>(
                _mapper.Map<List<ResultMessageDto>>(messages)
            );
        }

        public async Task<IDataResult<List<ResultMessageDto>>> GetSentAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetSentAsync(userId);
            return new SuccessDataResult<List<ResultMessageDto>>(
                _mapper.Map<List<ResultMessageDto>>(messages)
            );
        }

        public async Task<IDataResult<List<ResultMessageDto>>> GetDraftsAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetDraftsAsync(userId);
            return new SuccessDataResult<List<ResultMessageDto>>(
                _mapper.Map<List<ResultMessageDto>>(messages)
            );
        }

        public async Task<IDataResult<List<ResultMessageDto>>> GetDeletedAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetDeletedAsync(userId);
            return new SuccessDataResult<List<ResultMessageDto>>(
                _mapper.Map<List<ResultMessageDto>>(messages)
            );
        }

        public async Task<IDataResult<List<ResultMessageDto>>> GetSpamAsync(int userId)
        {
            var messages = await _unitOfWork.Messages.GetSpamAsync(userId);
            return new SuccessDataResult<List<ResultMessageDto>>(
                _mapper.Map<List<ResultMessageDto>>(messages)
            );
        }
    }
}
