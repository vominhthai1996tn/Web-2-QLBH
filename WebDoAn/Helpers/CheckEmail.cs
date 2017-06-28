using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class CheckEmail
    {
        //check email có hợp lệ không
        public bool checkEmailHopLe(string email)
        {
            using (QLBHEntities ctx = new QLBHEntities())
            {
                var us = ctx.Users
                    .Where(i => i.f_Email == email)
                    .FirstOrDefault();

                if (us != null)
                    return false;
            }

            return true;
        }
        
    
    }
}