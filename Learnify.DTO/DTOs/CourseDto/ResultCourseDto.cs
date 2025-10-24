namespace Learnify.DTO.DTOs.CourseDto
{
   

        public class ResultCourseDto
        {
            public int CourseID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }      // ✅ eklendi
            public int InstructorID { get; set; }
            public string InstructorName { get; set; }    // ✅ eklendi
        }

    }

