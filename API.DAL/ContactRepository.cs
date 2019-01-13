using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyContact.DAL
{
    public class ContactRepository : IRepository<Contact, Guid>
    {
        ContactContext context = null;
        public ContactRepository()
        {
            context = new ContactContext();
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="key">contact id</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string userId, Guid key)
        {
            var record = await GetAsync(userId, key);
            if (record == null)
            {
                return false;
            }
            else
            {
                context.Contacts.Remove(record);
                int savedRec = await context.SaveChangesAsync();
                return true;
            }
        }

        /// <summary>
        /// Get All contact for user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>contact list</returns>
        public async Task<IEnumerable<Contact>> GetAsync(string userId)
        {
            return await Task.Run(() => context.Contacts.Where(x => x.UserId == userId));
        }

        /// <summary>
        /// Get one contact card
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="key">contact id</param>
        /// <returns>contact</returns>
        public async Task<Contact> GetAsync(string userId, Guid key)
        {
          return await Task.Run(() => context.Contacts.FirstOrDefault(x => x.UserId == userId && x.Id == key));
        }

        /// <summary>
        /// Insert new contact
        /// </summary>
        /// <param name="userId">user</param>
        /// <param name="entity">contact</param>
        /// <returns>boolean</returns>
        public async Task<bool> InsertAsync(string userId, Contact entity)
        {
            context.Contacts.Add(entity);
            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="entity">contact</param>
        /// <returns>boolean</returns>
        public async Task<bool> UpdateAsync(string userId, Contact entity)
        {
            var record = await GetAsync(userId, entity.Id);
            if (record == null)
            {
                return false;
            }
            else
            {
                record.FirstName = entity.FirstName;
                record.LastName = entity.LastName;
                record.PhoneNumber = entity.PhoneNumber;
                record.Status = entity.Status;
                record.Email = entity.Email;
                int savedRec = await context.SaveChangesAsync();
                return true;
            }          
        }

        /// <summary>
        /// dispose 
        /// </summary>
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}
