using ForumApp.Data;
using ForumApp.Data.Entities;
using ForumApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext data;

        public PostsController(ForumAppDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var posts = data.Posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
                .ToList();

            return View(posts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostFormModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content
            };

            data.Add(post);
            data.SaveChanges();
            return RedirectToAction(nameof(Add));
        }

        public IActionResult Edit(int id)
        {
            var post = data.Posts.Find(id);

            return View(new PostFormModel
            {
                Title = post.Title,
                Content = post.Content
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, PostFormModel model)
        {
            var post = data.Posts.Find(id);

            post.Title = model.Title;
            post.Content = model.Content;
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = this.data.Posts.Find(id);

            this.data.Posts.Remove(post);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
