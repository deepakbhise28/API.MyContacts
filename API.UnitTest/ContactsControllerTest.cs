using API.Entities;
using API.MyContacts.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyContact.DAL;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace API.UnitTest
{
    public class TestUSer : IPrincipal
    {
        public IIdentity Identity
        {
            get
            {
                var identity = new GenericIdentity("TestUser");
                identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "TestUser1"));
                identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "TestUser1"));
                return identity;
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
    [TestClass]
    public class ContactsControllerTest
    {
        public ContactsControllerTest()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug", ""));
        }

        private ContactsController Instance()
        {
            ContactsController contactController = new ContactsController(new ContactRepository())
            {
                Request = new System.Net.Http.HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:49943/api/v1/contacts")
                },
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            // contactController.Configuration.
            TestUSer user = new TestUSer();
            Thread.CurrentPrincipal = user;
            contactController.User = user;
            return contactController;
        }

        Guid id = Guid.NewGuid();

        [TestMethod]
        public async Task AddContact()
        {
            var result = await Instance().AddContact(new Contact { Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "9994357416", Status = true });
            var response = result.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var addedContact = JsonConvert.DeserializeObject<Contact>(response.Content.ReadAsStringAsync().Result);
                id = addedContact.Id;
            }
        }

        [TestMethod]
        public void GetContacts()
        {
            var result = Instance().GetContacts().Result;
            Assert.IsNotNull(result);
            var response = result.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void GetContactsWithID()
        {
            var result = Instance().GetContact(id).Result;
            Assert.IsNotNull(result);
            var response = result.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void UpdateContact()
        {
            var result = Instance().UpdateContact(id, new Contact { Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "88888888888", Status = true }).Result;
            Assert.IsNotNull(result);
            var response = result.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteContact()
        {
            var result = Instance().DeleteContact(id).Result;
            Assert.IsNotNull(result);
            var response = result.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
