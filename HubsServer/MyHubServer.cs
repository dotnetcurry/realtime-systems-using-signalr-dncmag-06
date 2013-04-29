using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HubsServer
{
    public class MyHubServer : Hub
    {
        public static IList<Customer> Customers { get; set; }

        public void Add(Customer customer)
        {
            if (Customers == null) Customers = new List<Customer>();

            Customers.Add(customer);

            Clients.All.RefreshCustomers(Customers);
        }

        public IEnumerable<Customer> All()
        {
            return Customers;
        }

        public Customer Get(int id)
        {
            if (Customers == null) return null;
            return Customers.FirstOrDefault(x => x.Id == id);
        }
    }
  
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}