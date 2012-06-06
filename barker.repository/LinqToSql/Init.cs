using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using barker.data;
using Faker;

namespace barker.repository.LinqToSql
{
    public class Init : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

            var user1 = new User() { Email = "rand80@gmail.com", FullName = "Ran Dahan", Password = "123456", Username = "RanDahan", CreateAt = DateTime.Now};
            context.Users.Add(user1);

            for (int i = 0; i < 10; i++)
            {
                context.Users.Add(new User()
                                      {
                                          FullName = Faker.Name.FullName(),
                                          Username  = Faker.Internet.UserName(),
                                          Email = Faker.Internet.Email(),
                                          CreateAt = DateTime.Now,
                                          Password = "123456"
                                      });
            }
                                  
            context.SaveChanges();


            foreach (var user in context.Users)
            {
                // Add Random user Barks
                for (int i = 0; i < 5; i++)
                {
                    context.Barks.Add(new Bark()
                                          {
                                              UserID = user.ID,
                                              CreateAt = DateTime.Now,
                                              Message = Faker.Lorem.Sentence()
                                          });
                }               
            }

            foreach (var user in context.Users)
            {
                // Add Random Friends
                Random random = new Random();
                for (int i = 0; i < 3; i++)
                {
                    var friend =
                        context.Users.OrderBy(o => o.Username).Skip(random.Next(context.Users.Count() - 1)).
                            FirstOrDefault();
                    if(user.Friends == null)
                        user.Friends = new Collection<UserFriend>();
                    if (user.Friends.Count(f => f.Friend == friend) == 0)                    
                        user.Friends.Add(new UserFriend() {Friend = friend, DateCreated = DateTime.Now});                     
                }
            }

            context.SaveChanges();                      
        }
    }
}
