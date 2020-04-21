using System;
using System.IO;
using Newtonsoft.Json;

namespace TipoutProject.LogicControllers
{
    public class UserLogicController
    {
        private string dbPath { get; set; }

        public UserLogicController(string dbName)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "/" + dbName))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "/" + dbName);
            }
            dbPath = Environment.CurrentDirectory + "/" + dbName;
        }

        public bool UserExists(string id)
        {
            bool b = false;
            try
            {
                b = File.Exists(dbPath + "/" + id + ".json");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return b;
        }

        public bool AddTip(string id, TipModel newTip)
        {
            bool b = false;
            try
            {
                bool exist = UserExists(id);
                if (exist)
                {
                    UserModel user = GetUser(id);
                    if (user != null)
                    {
                        newTip.ID = Guid.NewGuid().ToString();
                        user.TipList.Add(newTip);
                        string json = JsonConvert.SerializeObject(user);
                        using (StreamWriter sw = new StreamWriter(dbPath + "/" + user.userAuth.username + ".json"))
                        {
                            sw.Write(json);
                            sw.Close();

                            sw.Dispose();
                        }
                        b = UserExists(user.userAuth.username);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return b;
        }

        public bool UpdateUsername(string id, string newUsername)
        {
            bool b = false;
            try
            {
                bool exist = UserExists(id);
                if (!exist)
                {
                    UserModel user = GetUser(id);
                    if (user != null)
                    {
                        user.userAuth.username = newUsername;
                        string json = JsonConvert.SerializeObject(user);
                        using (StreamWriter sw = new StreamWriter(dbPath + "/" + user.userAuth.username + ".json"))
                        {
                            sw.Write(json);
                            sw.Close();

                            sw.Dispose();
                        }
                        b = UserExists(user.userAuth.username);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return b;
        }

        public bool UpdatePassword(string id, string newpass)
        {
            bool b = false;
            try
            {
                bool exist = UserExists(id);
                if (!exist)
                {
                    UserModel user = GetUser(id);
                    if (user != null)
                    {
                        user.userAuth.password = newpass;
                        string json = JsonConvert.SerializeObject(user);
                        using (StreamWriter sw = new StreamWriter(dbPath + "/" + user.userAuth.username + ".json"))
                        {
                            sw.Write(json);
                            sw.Close();

                            sw.Dispose();
                        }
                        b = UserExists(user.userAuth.username);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return b;
        }

        public UserModel GetUser(string id)
        {
            UserModel user = null;
            try
            {
                bool exists = UserExists(id);
                if (exists)
                {
                    string s = File.ReadAllText(dbPath + "/" + id + ".json");
                    if (!string.IsNullOrEmpty(s))
                    {
                        user = JsonConvert.DeserializeObject<UserModel>(s);
                        if (user != null)
                        {
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }

        public bool AddUser(AuthorizationModel newuser)
        {
            bool b = false;
            try
            {
                bool exist = UserExists(newuser.username);
                if (!exist)
                {
                    UserModel user = new UserModel();
                    user.userAuth = newuser;
                    user.TipList = new System.Collections.Generic.List<TipModel>();
                    user.userID = Guid.NewGuid().ToString();

                    string json = JsonConvert.SerializeObject(user);

                    using (StreamWriter sw = new StreamWriter(dbPath + "/" + user.userAuth.username + ".json"))
                    {
                        sw.Write(json);
                        sw.Close();

                        sw.Dispose();
                    }

                    b = UserExists(user.userAuth.username);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return b;
        }

        public bool DeleteUser(string id)
        {
            bool b = false;
            try
            {
                if (UserExists(id))
                {
                    File.Delete(dbPath + "/" + id + ".json");
                    b = UserExists(id);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return b;
        }
    }
}
