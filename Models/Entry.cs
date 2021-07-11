using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dejtinghemsida.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string RecipientId { get; set; }
    }
}