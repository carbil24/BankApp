using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test3.Models
{
    public class User
    {
        private int _id;
        private string _name;
        private string _password;
        private Account _userAccount;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [Required]
        [StringLength(100)]
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [Required]
        [StringLength(20)]
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public Account UserAccount
        {
            get
            {
                return _userAccount;
            }
            set
            {
                _userAccount = value;
            }
        }
    }
}
