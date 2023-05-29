using ImageRecognition.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageRecognition.Repository
{
    public static class AccountRepository
    {
        

        internal static async Task<Account> GetAccountById(int accountId)
        {
            using (var db = new DatabaseContext())
            {
                return await db.accounts.FirstOrDefaultAsync(account => account.AccountId == accountId);
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
                    account.AccountId = 0;

                    user.ManagedAccounts.Add(account);

                    db.accounts.Add(account);

                    
                    db.users.Update(user);

                    db.Entry(user.ManagedAccounts).State = EntityState.Modified;

                    return db.SaveChanges() >= 1;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }

    }
}
