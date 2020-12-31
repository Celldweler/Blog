using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        public Post GetPost(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllPosts(string Category);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);

        Task<bool> SaveChangesAsync();
    }
}
