using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Tools
{
    public class Rating
    {

        public static void Process(IRepository repository)
        {
            foreach (var user in repository.Users)
            {
                ProcessUser(user.ID, repository);
            }
        }

        public static void ProcessUser(int userID, IRepository repository)
        {
            var totalRating = 0;
            var user = repository.Users.FirstOrDefault(p => p.ID == userID);
            if (user != null && user.Person != null)
            {
                totalRating += user.Person.PublicationPersons.Count(p => p.Publication.Type == (int)Publication.TypeEnum.Book) * 10;
                totalRating += user.Person.PublicationPersons.Count(p => p.Publication.Type == (int)Publication.TypeEnum.Article) * 1;
                totalRating += user.Person.StudyMaterialPersons.Count() * 10;
                totalRating += user.Person.ImagePersons.Count() * 1;
                totalRating += user.Blog != null ? user.Blog.BlogPosts.Count * 1 : 0;
                totalRating += user.Documents.Count * 1;
                //лайки/анлайки за 
                totalRating += user.Person.PublicationPersons.Select(p => p.Publication).Sum(p => p.TotalLikes);
                totalRating += user.Person.StudyMaterialPersons.Select(p => p.StudyMaterial).Sum(p => p.TotalLikes);
                totalRating += user.Person.ImagePersons.Select(p => p.Image).Sum(p => p.TotalLikes);
                totalRating +=  user.Blog != null ? user.Blog.BlogPosts.Sum(p => p.TotalLikes) : 0;
                totalRating += user.Documents.Sum(p => p.TotalLikes);
                totalRating += user.Comments.Sum(p => p.TotalLikes);
                user.Rating = user.StartRating + totalRating;
             
                repository.UpdateUserRating(user);
            }

        }
    }
}