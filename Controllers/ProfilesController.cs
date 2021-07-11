using Dejtinghemsida.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dejtinghemsida.Controllers
{
   
        [Authorize]
        public class ProfilesController : Controller
        {
            private ApplicationDbContext _context;

          
            public ProfilesController()
            {
                _context = new ApplicationDbContext();
            }

            public ApplicationUser GetUserByName(string name)
            {
                var appUser = _context.Users.Single(u => u.UserName == name);

                foreach (var entry in _context.Entries)
                {
                    if (entry.RecipientId == appUser.Id)
                    {
                        appUser.Inlägg.Add(entry);
                    }
                }
                return appUser;
            }

            public ActionResult Update(EditYourProfileViewModel LoggedInUser)
            {
                var UserToChange = GetUserByName(User.Identity.Name);

                LoggedInUser.CurrentUser.UserName = UserToChange.UserName;

                var user = _context.Users.SingleOrDefault(u => u.UserName == UserToChange.UserName);

                user.Förnamn = LoggedInUser.CurrentUser.Förnamn.Substring(0, 1).ToUpper() + LoggedInUser.CurrentUser.Förnamn.Substring(1).ToLower();
                user.Efternamn = LoggedInUser.CurrentUser.Efternamn.Substring(0, 1).ToUpper() + LoggedInUser.CurrentUser.Efternamn.Substring(1).ToLower();
                user.Sysselsättning = LoggedInUser.CurrentUser.Sysselsättning;
                user.Kön = LoggedInUser.CurrentUser.Kön;
                user.Ålder = LoggedInUser.CurrentUser.Ålder;

                _context.SaveChanges();
                return RedirectToAction("YourProfile");
            }

            public ActionResult ChangeProfilePicture([Bind(Exclude = "UserPhoto")] ApplicationUser LoggedInUser)
            {

                var UserToChange = GetUserByName(User.Identity.Name);
                LoggedInUser.UserName = UserToChange.UserName;
                var user = _context.Users.SingleOrDefault(u => u.UserName == UserToChange.UserName);

                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                user.UserPhoto = imageData;

                _context.SaveChanges();
                return RedirectToAction("YourProfile");
            }

            public ActionResult EditYourProfile(EditYourProfileViewModel model)
            {

                var user = GetUserByName(User.Identity.Name);
                var intressen = _context.Intressen.ToList();
                model.CurrentUser = new ApplicationUser();

                model.CurrentUser.Ålder = user.Ålder;
                model.CurrentUser.Förnamn = user.Förnamn;
                model.CurrentUser.Efternamn = user.Efternamn;
                model.CurrentUser.Sysselsättning = user.Sysselsättning;
                model.CurrentUser.Kön = user.Kön;

                model.Requests = new List<ApplicationUser>();
                var allFriendRequests = _context.Friendrequests.ToList();

                foreach (var u in allFriendRequests)
                {
                    if (model.CurrentUser.Id == u.FriendId)
                    {
                        model.Requests.Add(GetOtherUser(u.UserId));
                    }
                }
                return View(model);
            }

            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }

            public ActionResult MyMatches(MyMatchesViewModel model)
            {
                model.Requests = new List<ApplicationUser>();
                var allFriendRequests = _context.Friendrequests.ToList();
                var user = GetUserByName(User.Identity.Name);

                foreach (var u in allFriendRequests)
                {
                    if (user.Id == u.FriendId)
                    {
                        model.Requests.Add(GetOtherUser(u.UserId));
                    }
                }

                return View(model);
            }

            public ActionResult PotentialMatches(PotentialMatchesViewModel model)
            {
                model.Requests = new List<ApplicationUser>();
                var allFriendRequests = _context.Friendrequests.ToList();
                var user = GetUserByName(User.Identity.Name);

                foreach (var u in allFriendRequests)
                {
                    if (user.Id == u.FriendId)
                    {
                        model.Requests.Add(GetOtherUser(u.UserId));
                    }
                }

                return View(model);
            }

            public ActionResult OtherProfile(OtherProfileViewModel model, String Id)
            {
                var otherUser = GetOtherUser(Id);
                model.CurrentUser = new ApplicationUser();

                model.CurrentUser.UserName = otherUser.UserName;
                var you = GetUserByName(User.Identity.Name);
                var user = _context.Users.Single(u => u.UserName == model.CurrentUser.UserName);

                model.CurrentUser.Id = user.Id;
                model.CurrentUser.Förnamn = user.Förnamn;
                model.CurrentUser.Efternamn = user.Efternamn;
                model.CurrentUser.Ålder = user.Ålder;
                model.CurrentUser.Kön = user.Kön;
                model.CurrentUser.Sysselsättning = user.Sysselsättning;
                model.CurrentUser.UserName = user.UserName;
                foreach (var entry in _context.Entries)
                {
                    if (model.CurrentUser.Id == entry.RecipientId)
                    {
                        model.CurrentUser.Inlägg.Add(entry);
                    }
                }

                model.Requests = new List<ApplicationUser>();
                var allFriendRequests = _context.Friendrequests.ToList();

                foreach (var u in allFriendRequests)
                {
                    if (you.Id == u.FriendId)
                    {
                        model.Requests.Add(GetOtherUser(u.UserId));
                    }
                }



                return View(model);
            }

            public ActionResult YourProfile(ProfileViewModel model)
            {
                var user = GetUserByName(User.Identity.Name);
                var allFriends = _context.Friends.ToList();
                model.CurrentUser = user;
                model.CurrentUser.Id = user.Id;
                model.CurrentUser.Förnamn = user.Förnamn;
                model.CurrentUser.Efternamn = user.Efternamn;
                model.CurrentUser.Ålder = user.Ålder;
                model.CurrentUser.Kön = user.Kön;
                model.CurrentUser.Id = user.Id;
                model.CurrentUser.Sysselsättning = user.Sysselsättning;
                model.CurrentUser.UserPhoto = user.UserPhoto;
                model.CurrentUser.Inlägg = user.Inlägg;
                model.Friends = new List<ApplicationUser>();

                foreach (var f in allFriends)
                {
                    if (f.UserId == user.Id)
                    {
                        model.Friends.Add(GetOtherUser(f.FriendId));
                    }
                    if (f.FriendId == user.Id)
                    {
                        model.Friends.Add(GetOtherUser(f.UserId));
                    }
                }

                model.WallEntrys = new List<Entry>();

                foreach (var entry in _context.Entries)
                {
                    if (entry.RecipientId == user.Id)
                    {
                        model.WallEntrys.Add(entry);
                    }
                }

                model.Requests = new List<ApplicationUser>();
                var allFriendRequests = _context.Friendrequests.ToList();

                foreach (var u in allFriendRequests)
                {
                    if (user.Id == u.FriendId)
                    {
                        model.Requests.Add(GetOtherUser(u.UserId));
                    }
                }


                return View(model);
            }



            public FileContentResult UserPhotos()
            {
                string userId = User.Identity.GetUserId();
                var LoggedInUser = GetUserByName(User.Identity.Name);
                if (LoggedInUser.UserPhoto.Length == 0)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/hejhej.jpg");
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }

                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(u => u.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }

            public FileContentResult OtherUsersPhoto(string Id)
            {
                string userId = Id;
                var OtherUser = GetOtherUser(userId);
                if (OtherUser.UserPhoto == null || OtherUser.UserPhoto.Length == 0)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/hejhej.jpg");
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }


                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(u => u.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }

            public ApplicationUser GetOtherUser(string Id)
            {
                var appUser = _context.Users.Single(x => x.Id == Id);

                return appUser;
            }

        public ActionResult SearchView(SearchResultsViewModel model, String searchName)
        {
            var result = new List<ApplicationUser>();
            var allUsers = _context.Users.ToList();
            var user = GetUserByName(User.Identity.Name);

            model.Requests = new List<ApplicationUser>();
            var allFriendRequests = _context.Friendrequests.ToList();

            foreach (var u in allFriendRequests)
            {
                if (user.Id == u.FriendId)
                {
                    model.Requests.Add(GetOtherUser(u.UserId));
                }
            }

            if (!string.IsNullOrEmpty(searchName) && searchName.Contains(" "))
            {

                string[] searchArray = searchName.Split(' ');
                var searchByFullName = searchArray[0].Substring(0, 1).ToUpper() + searchArray[0].Substring(1).ToLower() + " " + searchArray[1].Substring(0, 1).ToUpper() + searchArray[1].Substring(1).ToLower();
                result = allUsers.Where(u => u.Förnamn + " " + u.Efternamn == searchByFullName).ToList();
            }
            else if (!string.IsNullOrEmpty(searchName))
            {
                var search = searchName.Substring(0, 1).ToUpper() + searchName.Substring(1).ToLower();
                result = allUsers.Where(u => u.Förnamn == search || u.Efternamn == search).ToList();
            }
            model.Results = result;

            return View(model);
        }
        public ActionResult AddFriend(ApplicationUser model)
            {

                var user = GetUserByName(User.Identity.Name);
                var UserToBefriend = GetOtherUser(model.Id);
                UserToBefriend.Id = model.Id;

                //Validering
                var ThisIsYou = UserToBefriend.Id == user.Id;
                var AlreadyFriendsUserId = _context.Friends.Where(f => f.UserId == user.Id && f.FriendId == UserToBefriend.Id).Any();
                var AlreadyFriendsFriendId = _context.Friends.Where(f => f.UserId == UserToBefriend.Id && f.FriendId == user.Id).Any();
                var FriendreqAlreadySent = _context.Friendrequests.Where(f => f.UserId == user.Id && f.FriendId == UserToBefriend.Id).Any();
                var FriendreqAlreadyRecived = _context.Friendrequests.Where(f => f.UserId == UserToBefriend.Id && f.FriendId == user.Id).Any();

                if (AlreadyFriendsUserId == false && AlreadyFriendsFriendId == false)
                {
                    if (FriendreqAlreadySent == false && FriendreqAlreadyRecived == false)
                    {
                        if (ThisIsYou == false)
                        {
                            _context.Friendrequests.Add(new PendingFriendRequests
                            {
                                FriendId = UserToBefriend.Id,
                                UserId = user.Id
                            });

                            _context.SaveChanges();
                        }
                    }
                }
                return RedirectToAction(model.Id, "Profiles/OtherProfile");
            }

            public ActionResult FriendRequests(PendingFriendRequests model)
            {

                model.Requests = new List<ApplicationUser>();

                var user = GetUserByName(User.Identity.Name);

                var allFriendRequests = _context.Friendrequests.ToList();

                foreach (var u in allFriendRequests)
                {

                    if (user.Id == u.FriendId)
                    {

                        model.Requests.Add(GetOtherUser(u.UserId));
                    }
                }
                return View(model);
            }

            public ActionResult AcceptFriend(String Id)
            {

                var user = GetUserByName(User.Identity.Name);


                _context.Friends.Add(new Friends
                {
                    FriendId = Id,
                    UserId = user.Id
                });

                var friendrequest = _context.Friendrequests.Single(request => request.UserId == Id && request.FriendId == user.Id);
                var allRequests = _context.Friendrequests.Count();

                _context.Friendrequests.Remove(friendrequest);
                _context.SaveChanges();

                return RedirectToAction("YourProfile");
            }

            public ActionResult IgnoreFriend(String Id)
            {

                var user = GetUserByName(User.Identity.Name);

                var friendrequest = _context.Friendrequests.Single(request => request.UserId == user.Id && request.FriendId == Id);
                var allRequests = _context.Friendrequests.Count();

                _context.Friendrequests.Remove(friendrequest);
                _context.SaveChanges();

                return RedirectToAction("YourProfile");
            }
        }
}
