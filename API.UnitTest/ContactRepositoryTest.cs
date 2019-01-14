using System;
using API.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyContact.DAL;

namespace API.UnitTest
{
    [TestClass]
    public class ContactRepositoryTest
    {
        private IRepository<Contact, Guid> _repository;
        Guid id = Guid.NewGuid();
        public ContactRepositoryTest()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug", ""));
            _repository = new ContactRepository();
        }

        [TestMethod]
        public void InsertAsync()
        {
            var contact = new Contact { Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "9994357416", Status = true };
           var result = _repository.InsertAsync("TestUser", contact).Result;
           Assert.IsNotNull(result);
           id = contact.Id;
        }

        [TestMethod]
        public void GetAsync()
        {
            var result = _repository.GetAsync("TestUser").Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAsyncWithID()
        {
            var result = _repository.GetAsync("TestUser",id).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateAsync()
        {
            var result = _repository.UpdateAsync("TestUser", new Contact {Id= id, Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "9994357416", Status = true }).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteAsync()
        {
            var result = _repository.DeleteAsync("TestUser", id).Result;
            Assert.IsNotNull(result);
        }
    }
}
