namespace Test3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using System.Collections.Generic;
    using Test3.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Test3.Models.BankDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Test3.Models.BankDBContext context)
        {
            //context.Users.RemoveRange(context.Users);
            //context.Accounts.RemoveRange(context.Accounts);

            List<User> users = new List<User>();

            users.Add(new User()
            {
                Name = "Carlos Bilbao",
                Password = "123",
                UserAccount = new Account()
                    {
                        Balance = 1010
                    }
            });

            users.Add(new User()
            {
                Name = "admin",
                Password = "admin",
                UserAccount = new Account()
                {
                    Balance = 0
                }
            });

            context.Users.AddRange(users);
            base.Seed(context);
        }
    }
}
