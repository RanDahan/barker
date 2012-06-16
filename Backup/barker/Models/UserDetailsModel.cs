using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using barker.data;

namespace barker.Controllers
{
    public class UserDetailsModel
    {
        public User User { get; set; }
        public List<UserFriend> Followers { get; set; }
    }
}
