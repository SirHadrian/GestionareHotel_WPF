using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionareHotel.Models
{
    class UsersClass
    {
        public string UserName { get; set; }
        public string EmailAdress { get; set; }
        //public string Password { get; set; }
        //public Nullable<bool> Client { get; set; }
        public Nullable<bool> Angajat { get; set; }
        public Nullable<bool> Admin { get; set; }
    }
}
