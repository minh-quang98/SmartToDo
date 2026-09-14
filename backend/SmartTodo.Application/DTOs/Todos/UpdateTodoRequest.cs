using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartTodo.Domain.Enums;

namespace SmartTodo.Application.DTOs.Todos
{
    public class UpdateTodoRequest
    {
        // Id lấy từ URL nên không lặp lại trong body; [Required] trên Guid không chặn Guid.Empty.
        // UpdateAt do server đặt bằng DateTime.UtcNow nên không nhận từ client.
        [Required(ErrorMessage = "Tiêu đề công việc không được để trống")]
        [MaxLength(200, ErrorMessage = "Tiêu đề không được quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        public string? Description { get; set; }

        // Chặn giá trị số ngoài enum (ví dụ 999); [ApiController] tự trả 400 khi không hợp lệ.
        [EnumDataType(typeof(TodoStatus), ErrorMessage = "Trạng thái không hợp lệ")]
        public TodoStatus Status { get; set; }

        // Chỉ nhận các mức ưu tiên được định nghĩa trong TodoPriority.
        [EnumDataType(typeof(TodoPriority), ErrorMessage = "Độ ưu tiên không hợp lệ")]
        public TodoPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
