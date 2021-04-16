using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDo.MVC.Models;

namespace ToDo.MVC.Controllers
{
    public class ToDoController : Controller
    {
        private readonly TodoDbContext _dbContext;
        public ToDoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var todos = _dbContext.Todos.ToList();
            return View(todos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            var todoId = _dbContext.Todos.Select(x => x.Id).Max() + 1;
            todo.Id = todoId;
            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var todo = _dbContext.Todos.Find(id);
            return todo == null ? NotFound() : View(todo);
        }

        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
            _dbContext.Todos.Update(todo);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var todo = _dbContext.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var todo = _dbContext.Todos.Find(id);
            _dbContext.Todos.Remove(todo);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}