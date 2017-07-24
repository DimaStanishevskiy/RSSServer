using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSSServer.Models
{
    public class Channel
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Url { set; get; }
        [Required]
        public Collection Collection { set; get; }
        [NotMapped]
        public List<News> News { set; get; }
        [NotMapped]
        public string Title { set; get; }
    }
}
