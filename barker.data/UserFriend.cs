using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace barker.data
{
    public class UserFriend
    {
        [Key, Column(Order = 0), ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Key, Column(Order = 1), ForeignKey("Friend")]
        public Guid FriendId { get; set; }
        public User Friend { get; set; }

        public DateTime DateCreated { get; set; }
       
    }
}
