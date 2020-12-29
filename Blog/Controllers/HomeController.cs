using Blog.Data;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    
    public class HomeController : Controller
    {
        private IRepository _repos;
        public HomeController(IRepository repos)
        {
            _repos = repos;
        }
        public IActionResult Index()
        {
            var posts = _repos.GetAllPosts();
            return View(posts);
        }
      
    }
}
