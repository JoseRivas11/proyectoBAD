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

        public static usuarios isValid(String email, String password)
        {
            proyectoBADEntities db = new proyectoBADEntities();
            usuarios user = null;
            if (db.usuarios.Any(u => u.email == email))
                {
                    // TODO: Ver que el estado del usuario sea igual a 1(Activo)
                    user = db.usuarios.First(u => u.email == email);

                    if(!BCrypt.Net.BCrypt.Verify(password, user.password))
                    {
                        user = null;
                    }
                }
            return user;

        }
    }
}