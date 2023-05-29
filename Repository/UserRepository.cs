using ImageRecognition.Data;
using ImageRecognition.Encryption;
using ImageRecognition.State;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.Repository
{
    public static class UserRepository
    {

        internal static bool GetUserByCredentials(string userEmail, string userPassword)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var result = db.users.Where(p => p.Name == userEmail && p.MasterPasswrod == userPassword);
                    if (result.Count() == 1) return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        internal static User GetUserByName(string userEmail)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    User resultuser = db.users.FirstOrDefault(p => p.Name == userEmail);
                    if (resultuser != null) return resultuser;
                    else return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal async static Task<User> GetUserByIdAsync(int userID)
        {
            using (var db = new DatabaseContext())
            {
                return await db.users.FirstOrDefaultAsync(user => user.Id == userID);
            }
        }

        internal static User GetUserObjectBycredentials(string userEmail, string userPassword)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    User resultuser = (User)db.users.FirstOrDefault(p => p.Name == userEmail && p.MasterPasswrod == userPassword);
                    if (resultuser != null) return resultuser;
                    else return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal async static Task<bool> CreateUserAsync(User user)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    await db.users.AddAsync(user);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateUserAsync(User user)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    db.users.Update(user);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DeleteUserAsync(int userID)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    User userToDelete = await GetUserByIdAsync(userID);

                    db.Remove(userToDelete);

                    return await db.SaveChangesAsync() >= 1;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        internal static bool DeleteAccount(User user, Account account)
        {

            using (var db = new DatabaseContext())
            {
                try
                {                    
                    user.ManagedAccounts.Remove(account);
                                        
                    db.accounts.Remove(account);
                                        
                    return db.SaveChanges() >=1;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
               
        }

        public static bool EditAccount(User user, Account account)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    
                    var existingAccount = user.ManagedAccounts.FirstOrDefault(a => a.AccountId == account.AccountId);

                    if (existingAccount != null)
                    {
                       
                        existingAccount.AccountName = account.AccountName;
                        existingAccount.AccountPassword = account.AccountPassword;
                        existingAccount.WebsiteURL = account.WebsiteURL;

                        
                        db.users.Update(user);

                        
                        db.accounts.Update(existingAccount);

                        
                        return db.SaveChanges() >=1;
                    }
                    else return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        internal static bool AddAccount(User user, Account account)
        {

            using (var db = new DatabaseContext())
            {
                try
                {
                    if (user == null)
                    {
                        throw new ArgumentNullException(nameof(user));
                    }

                    
                    if (user.ManagedAccounts == null)
                    {
                        user.ManagedAccounts = new List<Account>();
                    }

                    user.ManagedAccounts.Add(account);
                   
                    
                    db.users.Update(user);

                    
                    db.accounts.Add(account);

                    
                   return db.SaveChanges()>=1;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

        }

        internal static User GetUserWithAccounts(string usersname)
        {
            using (var db = new DatabaseContext())
            {
                User resultuser = db.users
                                    .Include(u => u.ManagedAccounts)
                                    .FirstOrDefault(u => u.Name == usersname);
                if (resultuser != null)
                {
                    return resultuser;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static async Task<bool> ReencryptingUserPasswords(User user, string decryptor)
        {
            User userPasswordToUpdate = GetUserWithAccounts(user.Name);
            if (userPasswordToUpdate.ManagedAccounts.Count() > 0)
            {
                using (var db = new DatabaseContext())
                {
                    Debug.WriteLine("Current user: " + CurrentUser.GetCurrentUserPasswrod);
                Debug.WriteLine("Updated user: " + user.MasterPasswrod);
                
                foreach (Account acc_psw in userPasswordToUpdate.ManagedAccounts)
                {
                    
                    string tempToUpdate = acc_psw.AccountPassword;
                    Debug.WriteLine("password before update:" + tempToUpdate);
                    string decryptedPsw = PasswordEncryption.Decrypt(tempToUpdate, decryptor);
                    Debug.WriteLine("password decrypted:" + decryptedPsw);
                    string encryptedPsq = PasswordEncryption.Encrypt(decryptedPsw, user.MasterPasswrod);
                    Debug.WriteLine("password encrypted:" + encryptedPsq);
                    acc_psw.AccountPassword = encryptedPsq;
                    db.accounts.Update(acc_psw);
                }
                
                    try
                    {
                        string encryptMaster = Hasher.Hash(user.MasterPasswrod);
                        user.MasterPasswrod = encryptMaster;
                       
                        db.users.Update(user);

                       
                        return db.SaveChanges() >= 1;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }


    }
}
