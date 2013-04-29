using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyHubClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RunHubClient();

            while (true) ;
        }

        static async Task RunHubClient()
        {
            var connection = new HubConnection("http://localhost:3448/");
            var myHub = connection.CreateHubProxy("MyHubServer");
            myHub.On("RefreshCustomers", (customers) =>
            {
                Console.WriteLine("New customers added, total amount of customers {0}", customers.Count);
            });
            connection.Start();

            string line = null;
            int customerId = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (int.TryParse(line, out customerId))
                {
                    var customer = await myHub.Invoke<dynamic>("Get", customerId);
                    if (customer == null) Console.WriteLine("No customer found");
                    else Console.WriteLine("Server found a customer with name {0}", customer.Name);
                }
            }
        }
    }
}
