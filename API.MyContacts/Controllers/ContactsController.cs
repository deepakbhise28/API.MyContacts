using API.Entities;
using API.MyContacts.Filters;
using API.MyContacts.Models;
using Microsoft.AspNet.Identity;
using MyContact.DAL;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.MyContacts.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/contacts")]
    public class ContactsController : ApiController
    {
        private IRepository<Contact, Guid> _repository;
        //public ContactsController()
        //{
        //    _repository = new ContactRepository();
        //}

        public ContactsController(IRepository<Contact, Guid> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get Contacts List
        /// </summary>
        /// <returns>list of Contacts</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetContacts()
        {
            var contacts = await _repository.GetAsync(User.Identity.GetUserId());
            return Ok(contacts);
        }

        /// <summary>
        /// Save newly added contact
        /// </summary>
        /// <param name="contactViewModel">Contact details</param>
        /// <returns>return to contact list</returns>
        [HttpPost]
        [Route("")]
        [RequestValidationFilter()]
        public async Task<IHttpActionResult> AddContact(Contact contactViewModel)
        {
            contactViewModel.UserId = User.Identity.GetUserId();
            await _repository.InsertAsync(contactViewModel.UserId, contactViewModel);
            return Created(this.Request.RequestUri, contactViewModel);
        }

        /// <summary>
        /// GET: /Contact/Edit
        /// </summary>
        /// <param name="id">contact card id</param>
        /// <returns>show detail to edit</returns>

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetContact([FromUri]Guid id)
        {
            var contact = await _repository.GetAsync(User.Identity.GetUserId(), id);
            return Ok(contact);
        }

        /// <summary>
        /// POST: Contact/Update
        /// </summary>
        /// <param name="contactViewModel">contact details</param>
        /// <returns>return to contact list</returns>
        [HttpPut]
        [Route("{id}")]
        [RequestValidationFilter()]
        public async Task<IHttpActionResult> UpdateContact([FromUri]Guid id, [FromBody]Contact contactViewModel)
        {
            contactViewModel.UserId = User.Identity.GetUserId();
            var result = await _repository.UpdateAsync(contactViewModel.UserId, contactViewModel);
            var status = new StatusModel() { Id = id };
            status.ResultCode = result ? "Success" : "ME001";
            status.Reason = result ? null : "Contact does not exist";
            return Ok(status);
        }

        /// <summary>
        /// POST: contact/Delete
        /// </summary>
        /// <param name="id">contact id</param>
        /// <returns>return to contact list</returns>       
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteContact(Guid id)
        {
            var result = await _repository.DeleteAsync(User.Identity.GetUserId(), id);
            var status = new StatusModel() { Id = id };
            status.ResultCode = result ? "Success" : "ME001";
            status.Reason = result ? null : "Contact does not exist";
            return Ok(status);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_repository != null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
