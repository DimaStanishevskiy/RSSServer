using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RSSServer.Models
{
    public class Collection
    {

        public int Id { set; get; }

        [Required]
        public string Name { set; get; }
        [Required]
        public User Owner { set; get; }

        public List<Channel> Channels { set; get; }
    }
}
