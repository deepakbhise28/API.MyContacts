using System;
using API.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyContact.DAL;
using System.Linq;

namespace API.UnitTest
{
    [TestClass]
    public class ContactRepositoryTest
    {
        private IRepository<Contact, Guid> _repository;
        Guid? id = default(Guid?);
        public ContactRepositoryTest()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug", ""));
            _repository = new ContactRepository();
        }

        [TestMethod]
        [Priority(0)]
        public void InsertAsync()
        {
            var contact = new Contact { Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "9994357416", Status = true };
           var result = _repository.InsertAsync("TestUser", contact).Result;
           Assert.IsNotNull(result);
           id = contact.Id;
        }

        [TestMethod]
        [Priority(1)]
        public void GetAsync()
        {
            var result = _repository.GetAsync("TestUser").Result;
            Assert.IsNotNull(result);
            if (result != null && result.Count() > 0)
            {
                id = result.FirstOrDefault().Id;
            }
        }

        [TestMethod]
        [Priority(2)]
        public void GetAsyncWithID()
        {
            if (id == null )
            {
                GetAsync();
            }

            var result = _repository.GetAsync("TestUser",id.Value).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Priority(3)]
        public void UpdateAsync()
        {
            if (id == null)
            {
                InsertAsync();
            }

            var result = _repository.UpdateAsync("TestUser", new Contact {Id= id.Value, Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "9994357416", Status = true }).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Priority(4)]
        public void DeleteAsync()
        {
            if (id == null)
            {
                InsertAsync();
            }
            var result = _repository.DeleteAsync("TestUser", id.Value).Result;
            Assert.IsNotNull(result);
            id = null;
        }
    }
}
