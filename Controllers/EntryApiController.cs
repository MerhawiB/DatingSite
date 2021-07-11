using Dejtinghemsida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace Dejtinghemsida.Controllers
{
    public class EntryApiController : Controller
    {
        [HttpPost]
        public void Post([FromBody] Entry _entry)
        {
            using (var _context = new ApplicationDbContext())
            {
                _context.Entries.Add(_entry);
                _context.SaveChanges();
            }
        }

        [HttpDelete]
        public void DeleteFromDb([FromBody] Entry _entry)
        {
            using (var _context = new ApplicationDbContext())
            {
                var entry = _context.Entries.Single(m => m.Id == _entry.Id);
                _context.Entries.Remove(entry);
                _context.SaveChanges();
            }
        }
    }
}