using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain
{
    public class UrlModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Короткий URL")]
        public string Surl { get; set; }
        [Required]
        [Display(Name = "Длинный URL")]
        public string Url { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
