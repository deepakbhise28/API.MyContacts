using API.Entities;
using MyContact.DAL;
using System;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API.MyContacts
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();           
            
            container.RegisterType<IRepository<Contact, Guid>, ContactRepository>();           

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}