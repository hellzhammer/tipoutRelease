using System;
using System.Collections.Generic;

namespace TipoutProject.LogicControllers
{
    public class ScoreBoardLogic
    {
        private List<UserModel> Users { get; set; }
        public ScoreBoardLogic(List<UserModel> users)
        {
            Users = users;
        }

        public List<ScoreBoardModel> GetTopTen()
        {
            List<ScoreBoardModel> scores = null;
            try
            {
                List<ScoreBoardModel> allScores = new List<ScoreBoardModel>();
                foreach (var user in Users)
                {
                    ScoreBoardModel sb = new ScoreBoardModel();
                    sb.UsersName = user.userAuth.username;
                    sb.UsersScore = GetUsersTotalTips(user);
                    allScores.Add(sb);
                }


            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return scores;
        }

        private ScoreBoardModel GetHighestScore(List<ScoreBoardModel> allScores)
        {
            ScoreBoardModel highest = null;
            try
            {
                foreach (var item in allScores)
                {
                    if (highest == null)
                    {
                        highest = item;
                    }
                    else
                    {
                        if (highest.UsersScore < item.UsersScore)
                        {
                            highest = item;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return highest;
        }

        private double GetUsersTotalTips(UserModel user)
        {
            double total = 0;
            try
            {
                foreach (var item in user.TipList)
                {
                    total += item.TipAmount;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return total;
        }
    }
}
