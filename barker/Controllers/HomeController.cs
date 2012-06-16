using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using barker.data;
using barker.repository.Interfaces;
using barker.repository.LinqToSql;
using barker.repository.Repositories;

namespace barker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private IBarkRepository barkRepository;

        public HomeController()
        {
            this.barkRepository = new BarkRepository(new DataContext());
        }
        
        public ActionResult Index()
        {
            var userRepo = new UserRepository(new DataContext());
            User user = userRepo.GetUserByName(User.Identity.Name);
            ViewBag.Followers = userRepo.FollowersCount(user);
            ViewBag.CurrentUser = user;
            ViewBag.LastMessage = user.Barks.OrderByDescending(o=>o.CreateAt).First();
            //return View(user);
            return View(barkRepository.GetBarksByUser(user.ID).OrderByDescending(t => t.CreateAt));            
        }

        public ActionResult Show(string id)
        {
            var userRepo = new UserRepository(new DataContext());
            User user = userRepo.GetUserByName(id);
            User me = userRepo.GetUserByName(User.Identity.Name);
            ViewBag.CurrentUser = user;            

            ViewBag.IsFriends = me.Friends.FirstOrDefault(u => u.FriendId == user.ID) != null;
            if (user != null)
            {
                ViewBag.LastMessage = user.Barks.OrderByDescending(o => o.CreateAt).First();
                //return View(user);
                return View(barkRepository.GetBarksByUser(user.ID).OrderByDescending(t => t.CreateAt));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TaggleFollow(string id)
        {
            var userRepo = new UserRepository(new DataContext());
            User me = userRepo.GetUserByName(User.Identity.Name);
            User friend = userRepo.GetUserByID(Guid.Parse(id));
            //var Friends = me.Friends.FirstOrDefault(u => u.FriendId == user.ID);

            if (me.IsFriends(friend))
            {
                me.RemoveFriend(friend);
            }
            else
            {
                me.AddFriend(friend);
            }
            
            userRepo.Save();

            return RedirectToAction("Show", new {id = friend.Username});
        }

        public ActionResult Following()
        {
            var userRepo = new UserRepository(new DataContext());
            List<User> user = userRepo.GetUserFriends(User.Identity.Name);            
            return View(user);
        }


        public  ActionResult UserDetails()
        {
            var userRepo = new UserRepository(new DataContext());
             User user = userRepo.GetUserByName(User.Identity.Name);
             List<UserFriend> friends = userRepo.FollowersCount(user);

            UserDetailsModel model = new UserDetailsModel { User = user, Followers = friends };

            return PartialView("_SideContents", model);
        }


        [Authorize]
        public ActionResult ViewBagSet()
        {
            ViewBag.Message = "Test this";

            return View();
        }
    }
}
