using AutoMapper;
using Learnify.Business.Abstract;
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

        public async Task AddAsync(CreateNotificationDto dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            await _unitOfWork.Notifications.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notification == null) return;

            _unitOfWork.Notifications.Delete(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultNotificationDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Notifications.GetAllAsync();
            return _mapper.Map<List<ResultNotificationDto>>(values.OrderByDescending(x => x.CreatedDate).ToList());
        }

        public async Task<ResultNotificationDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Notifications.GetByIdAsync(id);
            return _mapper.Map<ResultNotificationDto?>(entity);
        }

        public async Task UpdateAsync(UpdateNotificationDto dto)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(dto.Id);
            if (notification == null) return;

            _mapper.Map(dto, notification);
            _unitOfWork.Notifications.Update(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultNotificationDto>> GetUnreadAsync(int userId)
        {
            var unread = await _unitOfWork.Notifications.GetUnreadAsync(userId);
            return _mapper.Map<List<ResultNotificationDto>>(unread);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notification == null) return;

            notification.IsRead = true;
            _unitOfWork.Notifications.Update(notification);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
