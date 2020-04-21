using System;
namespace TipoutProject
{
    public class UpdateUserModel
    {
        public string userID { get; set; }
        public string dateToUpdate { get; set; } // only "username" or "password"
        public string updateData { get; set; }
    }
}
