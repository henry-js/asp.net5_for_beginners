using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ToDo.MVC.Models
{
    public class TodoDbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var context = new TodoDbContext(serviceProvider.GetRequiredService<DbContextOptions<TodoDbContext>>());

            // Look for any todos.
            if (context.Todos.Any())
            {
                // if we get here then the data is already seeded.
                return;
            }

            context.Todos.AddRange(
                new Todo
                {
                    Id = 1,
                    TaskName = "Work on book chapter",
                    IsComplete = false
                },
                new Todo
                {
                    Id = 2,
                    TaskName = "Create video content",
                    IsComplete = false
                },
                new Todo
                {
                    Id = 3,
                    TaskName = "Go for a run",
                    IsComplete = false
                }
            );
            context.SaveChanges();
        }
    }
}