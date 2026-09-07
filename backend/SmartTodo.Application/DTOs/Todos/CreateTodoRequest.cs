using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartTodo.Domain.Enums;

namespace SmartTodo.Application.DTOs.Todos
{
    public class CreateTodoRequest
    {
        [Required(ErrorMessage = "Tiêu đề công việc không được dể trống")]
        [MaxLength(200, ErrorMessage = "Tiêu đề không được quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        public string? Description { get; set; }

        public TodoPriority Priority { get; set; } = TodoPriority.Medium;

        public DateTime? DueDate { get; set; }
    }
}