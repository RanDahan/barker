using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace barker.data
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "You must pick a user name")]
        [DisplayName("User Name")]
        public string Username { get; set; }
        
        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You give a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
        [Required]
        public DateTime CreateAt { get; set; }

        public string Image { get; set; }

        public bool RemeberMe { get; set; }

        public bool Dummy { get; set; }

        public virtual ICollection<UserFriend> Friends { get; set; }

        public virtual ICollection<Bark> Barks { get; set; }

        public User()
        {
            ID = Guid.NewGuid();
            Dummy = true;
        }


        public void AddFriend(User friend)
        {
            Friends.Add(new UserFriend(){Friend =  friend, DateCreated = DateTime.Now});
        }

        public void RemoveFriend(User friend)
        {
            var friendship = Friends.FirstOrDefault(u => u.FriendId == friend.ID);
            if (friendship != null)
                Friends.Remove(friendship);
        }

        public bool IsFriends(User friend)
        {
            return (Friends.FirstOrDefault(u => u.FriendId == friend.ID) != null);
        }        
    }
}
