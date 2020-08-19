using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using WhiteBoard.Models.Entities;

namespace WhiteBoard
{
    public class SeedData
    {
         public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<WhiteBoardContext>();
            
            context.Database.Migrate();

            if (!context.Posts.Any())
            {
                var user1 = new User()
                {
                    UserName = "Samwise",
                    Email = "sam@ibm.com",
                    Password = "Sam123",
                };

                var user2 = new User()
                {
                    UserName = "Bertha",
                    Email = "bertha@ibm.com",
                    Password = "Bertha123"
                };
                
                var post1 = new Post()
                {
                    UserId = 1,
                    User = user1,
                    Content = "We have a business strategy around here -  it's called getting shit done",
                };
                
                var post2 = new Post()
                {
                    UserId = 2,
                    User = user2,
                    Content = "If you can think it, you can build it",
                };

                User[] users = {user1, user2};
                context.Users.AddRange(users);

                Post[] posts = {post1, post2};
                context.Posts.AddRange(posts);
                
                context.SaveChanges();
            }
        }
    }
}