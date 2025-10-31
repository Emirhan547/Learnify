﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.CourseReviewDto
{
    public class ResultCourseReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
    }
}
