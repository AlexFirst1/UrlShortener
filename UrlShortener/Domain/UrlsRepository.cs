using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Domain
{
    public class UrlsRepository
    {
        private readonly AppDbContext context;

        public UrlsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<UrlModel> GetUrls()
        {
            return context.Urls.OrderBy(x => x.Surl);
        }

        public UrlModel GetUrlById(int id)
        {
            return context.Urls.Single(x => x.Id == id);
        }

        public int SaveUrl(UrlModel entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;

            context.SaveChanges();
            return entity.Id;
        }

        public void DeleteUrl(UrlModel entity)
        {
            context.Urls.Remove(entity);
            context.SaveChanges();
        }

        public string ShortUrl(string url)
        {
            if (url?.Trim() != "")
            {
                string shortUrl = GetRandomUrl();
                try
                {
                    while (context.Urls.Any(ur => ur.Surl == shortUrl))
                    {
                        shortUrl = GetRandomUrl();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Ошибка: {e}");
                }
                return shortUrl;
            }
            return "";
        }

        private string GetRandomUrl()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public string RedirectLink(string url)
        {
            try
            {
                return context.Urls.Where(u => u.Surl == url).Select(s => s.Url).FirstOrDefault().ToString();                
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка: {e}");
            }
            return "";

        }
    }
}
