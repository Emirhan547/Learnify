using Learnify.DTO.DTOs.EnrollmentDto;

public interface IEnrollmentService
{
    Task<List<ResultEnrollmentDto>> GetAllAsync();
    Task<ResultEnrollmentDto?> GetByIdAsync(int id);
    Task AddAsync(CreateEnrollmentDto dto);
    Task UpdateAsync(UpdateEnrollmentDto dto);
    Task DeleteAsync(int id);

    // ✅ Yeni metot
    Task DeleteByStudentAndCourseAsync(int studentId, int courseId);
}
