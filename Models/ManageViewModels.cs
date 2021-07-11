using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Dejtinghemsida.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public IList<ApplicationUser> ExampleProfiles { get; set; }
        public List<ApplicationUser> Requests { get; set; }
    }

    public class ProfileViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public ICollection<Entry> WallEntrys { get; set; }
        public ICollection<ApplicationUser> Friends { get; set; }
        public List<ApplicationUser> Requests { get; set; }
    }

    public class OtherProfileViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public ICollection<Entry> WallEntrys { get; set; }
        public ICollection<ApplicationUser> Friends { get; set; }
        public List<ApplicationUser> Requests { get; set; }
    }

    public class SearchResultsViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public List<ApplicationUser> Requests { get; set; }
        public List<ApplicationUser> Results { get; set; }
    }

    public class PotentialMatchesViewModel
    {
        public List<ApplicationUser> Requests { get; set; }
    }
    public class MyMatchesViewModel
    {
        public List<ApplicationUser> Requests { get; set; }

    }

    public class EditYourProfileViewModel
    {
        public List<Interests> Interests { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public List<ApplicationUser> Requests { get; set; }
        public string Användarnamn { get; set; }
        public string Kön { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }
        public int Ålder { get; set; }
        public string Lösenord { get; set; }
        public string Sysselsättning { get; set; }
        [Display(Name = "Profilbild")]
        public byte[] UserPhoto { get; set; }
    }
}