using System;
using System.Collections.Generic;

namespace TipoutProject
{
    public class UserModel
    {
        public string userID { get; set; }
        public AuthorizationModel userAuth { get; set; }
        public List<TipModel> TipList { get; set; }
    }
}
