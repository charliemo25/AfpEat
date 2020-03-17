using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;


namespace AfpEat.Models
{
    public class CustomIdentity : GenericIdentity
    {
        public Utilisateur Utilisateur { get; private set; }
        public CustomIdentity(Utilisateur utilisateur, string type) : base(utilisateur.Matricule, type)
        {
            Utilisateur = utilisateur;
        }
    }
}