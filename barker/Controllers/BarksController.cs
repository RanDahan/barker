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
    public class BarksController : Controller
    {
        //
        // GET: /Barks/
          private IBarkRepository barkRepository;

          public BarksController()
        {
            this.barkRepository = new BarkRepository(new DataContext());
        }
    
        [HttpPost]
        public ActionResult Bark(string message)
        {
            var userRepo = new UserRepository(new DataContext());
            User user = userRepo.GetUserByName(User.Identity.Name);
            var model = new Bark();
            model.Message = message;
            model.CreateAt = DateTime.Now;
            model.UserID = user.ID;
            if (ModelState.IsValid)
            {
                barkRepository.InsertBark(model);
                barkRepository.Save();
            }
            return RedirectToAction("", "Home");
        }       
    }
}
