using DomainStore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Helpers
{
    public static class Extention
    {
        public static ApplicationUser FindByEmailWithId(this UserManager<ApplicationUser> Manager, string Id)
        {
            ApplicationUser User = Manager.Users.Include(x => x.Address).FirstOrDefault(x => x.Id == Id);
            
            if(User is null)
            {
                return null;
            }

            return User;
        }
    }
}
