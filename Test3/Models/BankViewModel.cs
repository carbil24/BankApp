using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3.Models
{
    public class BankViewModel
    {
        BankDBContext context = new BankDBContext();

        public List<User> GetAllUsersData()
        {
            return context.Users.Include("UserAccount").ToList();
        }

        public Account GetAccountPerUser(int ID)
        {
            return (from u in context.Users
                    where u.ID == ID
                    select u.UserAccount).SingleOrDefault();
        }

        public void AddNewUser(User newUser)
        {
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void UpdateUser(User updated)
        {
            User current = (from u in context.Users
                                where u.ID == updated.ID
                                select u).SingleOrDefault();

            current.Name = updated.Name;
            current.Password = updated.Password;

            context.SaveChanges();
        }

        public void DeleteUserRecord(int ID)
        {
            User toBeDeleted = (from u in context.Users
                                    where u.ID == ID
                                    select u).SingleOrDefault();

            Account accountToBeDeleted = GetAccountPerUser(ID);

            context.Accounts.Remove(accountToBeDeleted);
            context.Users.Remove(toBeDeleted);
            context.SaveChanges();
        }

        public User LoginUser(int ID, string Password)
        {
            return (from u in context.Users.Include("UserAccount")
                    where u.ID == ID && u.Password == Password
                    select u).SingleOrDefault();
        }

        public void UpdateBalance(User updated)
        {
            User current = (from u in context.Users.Include("UserAccount")
                            where u.ID == updated.ID
                            select u).SingleOrDefault();

            current.UserAccount.Balance = updated.UserAccount.Balance;
            context.SaveChanges();
        }
    }
}
