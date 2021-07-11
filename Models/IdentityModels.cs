using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dejtinghemsida.Models
{
    public class ApplicationUser : IdentityUser
    {

        public enum Gender
        {
            Man,
            Kvinna,
            Annat
        }

        public string Kön { get; set; }
        [Required(ErrorMessage = "Fältet Förnamn får inte vara tomt")]
        [RegularExpression("[a-öA-Ö]+", ErrorMessage = "Ange Förnamn med bokstäver från A-Ö och måste börja med stor bokstav")]
        public string Förnamn { get; set; }
        [Required(ErrorMessage = "Fältet Efternamn får inte vara tomt")]
        [RegularExpression("[a-öA-Ö]+", ErrorMessage = "Ange Efternamn med bokstäver från A-Ö och måste börja med stor bokstav")]
        public string Efternamn { get; set; }
        [Required(ErrorMessage = "Fältet ålder får inte vara tomt")]
        [Range(1, 100, ErrorMessage = "Ange en ålder mellan 1 och 100")]
        public int Ålder { get; set; }
        [Required(ErrorMessage = "Fältet sysselsättning får inte vara tomt")]
        [RegularExpression("[a-öA-Ö]+", ErrorMessage = "Ange Sysselsättning med bokstäver från A-Ö och måste börja med stor bokstav")]
        public string Sysselsättning { get; set; }
        public bool IsFriend = false;
        public ICollection<ApplicationUser> Friends { get; set; }
        public ICollection<Entry> Inlägg = new List<Entry>();
        public virtual ICollection<Interests> Intressen { get; set; }
        [Display(Name = "Profilbild")]
        public byte[] UserPhoto { get; set; }
        public ICollection<ApplicationUser> ListOfFriends { get; set; }
        public ICollection<ApplicationUser> FriendRequests { get; set; }


        public ApplicationUser()
        {
            Intressen = new HashSet<Interests>();

        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public DbSet<PendingFriendRequests> Friendrequests { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Interests> Intressen { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friends>()
                .HasKey(k => new { k.FriendId, k.UserId });

            modelBuilder.Entity<PendingFriendRequests>()
                .HasKey(k => new { k.FriendId, k.UserId });

            modelBuilder.Entity<ApplicationUser>()
             .HasMany(användare => användare.Intressen)
             .WithMany(intresse => intresse.Användare)
                 .Map(mc =>
                 {
                     mc.ToTable("användares_Intressen");
                     mc.MapLeftKey("id");
                     mc.MapRightKey("InterestID");
                 });
            base.OnModelCreating(modelBuilder);
        }


    }
}