using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Constants
{
    public static class UserRoles
    {
        //public const string User = "User";
        //public const string Manager = "Manager";
        //public const string HR = "HR";
        //public const string Admin = "Admin";

        public static List<string> Roles = new List<string>
        {    
            "Employee", 
            "Manager", 
            "HR", 
            "Admin" 
        };
    }
}
