using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;
using proyectoBAD.Models;

namespace proyectoBAD.Authentication
{
    public class UserManager
    {
        public static USUARIO isValid(String email, String password)
        {
            USUARIO user = null;
            using (var db = new proyectoBADCon())
            {
                if (db.USUARIOs.Any(u => u.EMAIL == email))
                {
                    // TODO: Ver que el estado del usuario sea igual a 1(Activo)
                    user = db.USUARIOs.First(u => u.EMAIL == email);

                    if(!BCrypt.Net.BCrypt.Verify(password, user.PASSWORD))
                    {
                        user = null;
                    }
                }
            }
            return user;

        }
    }
}