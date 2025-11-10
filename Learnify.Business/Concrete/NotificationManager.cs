using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.NotificationDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(CreateNotificationDto dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            await _unitOfWork.Notifications.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Bildirim başarıyla eklendi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (entity == null)
                return new ErrorResult("Bildirim bulunamadı.");

            _unitOfWork.Notifications.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Bildirim silindi.");
        }

        public async Task<IDataResult<List<ResultNotificationDto>>> GetAllAsync()
        {
            var values = await _unitOfWork.Notifications.GetAllAsync();
            var mapped = values.OrderByDescending(x => x.CreatedDate).ToList();
            return new SuccessDataResult<List<ResultNotificationDto>>(
                _mapper.Map<List<ResultNotificationDto>>(mapped)
            );
        }

        public async Task<IDataResult<ResultNotificationDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (entity == null)
                return new ErrorDataResult<ResultNotificationDto>("Bildirim bulunamadı.");

            return new SuccessDataResult<ResultNotificationDto>(_mapper.Map<ResultNotificationDto>(entity));
        }

        public async Task<IResult> UpdateAsync(UpdateNotificationDto dto)
        {
            var entity = await _unitOfWork.Notifications.GetByIdAsync(dto.Id);
            if (entity == null)
                return new ErrorResult("Bildirim bulunamadı.");

            _mapper.Map(dto, entity);
            _unitOfWork.Notifications.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Bildirim güncellendi.");
        }

        public async Task<IDataResult<List<ResultNotificationDto>>> GetUnreadAsync(int userId)
        {
            var values = await _unitOfWork.Notifications.GetUnreadAsync(userId);
            return new SuccessDataResult<List<ResultNotificationDto>>(
                _mapper.Map<List<ResultNotificationDto>>(values)
            );
        }

        public async Task<IResult> MarkAsReadAsync(int id)
        {
            var entity = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (entity == null)
                return new ErrorResult("Bildirim bulunamadı.");

            entity.IsRead = true;

            _unitOfWork.Notifications.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Bildirim okundu olarak işaretlendi.");
        }

        public async Task<IDataResult<List<ResultNotificationDto>>> GetAllByUserIdAsync(int userId)
        {
            var values = await _unitOfWork.Notifications.GetAllAsync(n => n.UserId == userId);
            return new SuccessDataResult<List<ResultNotificationDto>>(
                _mapper.Map<List<ResultNotificationDto>>(values)
            );
        }
    }
}
