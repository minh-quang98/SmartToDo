using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTodo.Application.DTOs.Todos;
using SmartTodo.Domain.Entities;
using SmartTodo.Domain.Enums;
using SmartTodo.Infrastructure.Persistence;

namespace SmartTodo.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoControllers : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TodoControllers(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var todos = await _dbContext.TodoItems
                .AsNoTracking()
                .Where(todo => !todo.IsDeleted)
                .OrderByDescending(todo => todo.CreateAt)
                .Select(todo => new TodoResponse
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Status = todo.Status,
                    Priority = todo.Priority,
                    dueDate = todo.DueDate,
                    CreateAt = todo.CreateAt
                }).ToListAsync(cancellationToken);

            return Ok(todos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoResponse>> GetById(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var todo = await _dbContext.TodoItems
                .AsNoTracking()
                .Where(todo => !todo.IsDeleted)
                .Where(todo => todo.Id == id)
                .Select(todo => new TodoResponse
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Status = todo.Status,
                    Priority = todo.Priority,
                    dueDate = todo.DueDate,
                    CreateAt = todo.CreateAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (todo is null)
            {
                return NotFound(new
                {
                    message = $"Không tìm thấy công việc có id = {id}"
                });
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoResponse>> Create(
            CreateTodoRequest request,
            CancellationToken cancellationToken
        )
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new
                {
                    message = "Tiêu đề không được để trống"
                });
            }

            var todo = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                Status = TodoStatus.New,
                Priority = request.Priority,
                DueDate = request.DueDate,
                IsDeleted = false,
                CreateAt = DateTime.UtcNow
            };
            _dbContext.TodoItems.Add(todo);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = new TodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status,
                Priority = todo.Priority,
                dueDate = todo.DueDate,
                CreateAt = todo.CreateAt
            };

            return CreatedAtAction(
                nameof(GetById), new { id = todo.Id }, response
            );
        }
    }
}