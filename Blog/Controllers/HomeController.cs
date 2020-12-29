﻿using Blog.Data;
using Blog.Data.FileManager;
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
        private IFileManager _fileManager;

        public HomeController(IRepository repos,
            IFileManager fileManager)
        {
            _repos = repos;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var posts = _repos.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int id)
        {
            var post = _repos.GetPost(id);
            return View(post);
        }

        [HttpGet("/Image/{image}")]
       public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);

            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}
