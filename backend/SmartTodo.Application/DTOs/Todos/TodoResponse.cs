using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartTodo.Domain.Enums;

namespace SmartTodo.Application.DTOs.Todos
{
    public class TodoResponse
    {
        public Guid Id {get; set;}
        public string Title {get; set;} = string.Empty;
        public string? Description {get; set;}
        public TodoStatus Status {get; set;}
        public TodoPriority Priority {get; set;}
        public DateTime? dueDate {get; set;}
        public DateTime CreateAt {get; set;}
    }
}