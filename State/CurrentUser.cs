using ImageRecognition.Data;
using ImageRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.State
{
    public class CurrentUser
    {
        private static User currentUser = new User();

        public static void ChangeStatusToActive(User tempUser)
        {
            currentUser = UserRepository.GetUserByName(tempUser.Name);
        }

        public static void ChangeStatusToTemporary(string name)
        {
            currentUser.Name = name;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }

        public static string GetCurrentUserPasswrod()
        {
            return currentUser.MasterPasswrod;
        }
        public static void SetCurrentUserPassword(string psw)
        {
            currentUser.MasterPasswrod= psw;
        }
        public static string GetCurrentUserName()
        {
            return currentUser.Name;
        }
    }
}
