﻿using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{   
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repos;
        private IFileManager _fileManager;
        public PanelController(
            IRepository repository,
            IFileManager fileManager
            )
        {
            _repos = repository;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var posts = _repos.GetAllPosts();
            return View(posts);
        }
        
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repos.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CuurentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags,
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,
            };

            if ( vm.Image == null)
            {
                post.Image = vm.CuurentImage;  // image handle
            }
            else
            {
                post.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (post.Id > 0)
                _repos.UpdatePost(post);
            else
                _repos.AddPost(post);
            if (await _repos.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repos.DeletePost(id);
            await _repos.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
