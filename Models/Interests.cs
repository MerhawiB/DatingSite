using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dejtinghemsida.Models
{
    public class Interests
    {
        public Interests()
        {
            this.Användare = new HashSet<ApplicationUser>();
        }
        [Key]
        public int InterestID { get; set; }
        public virtual ICollection<ApplicationUser> Användare { get; set; }
        public string Name { get; set; }
    }
}