﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using dts.server.Commons;

namespace dts.server.host
{
    class Program
    {
        private static Uri baseAddress = new Uri("http://localhost:3030/PersonSubscriptionService");

        static void Main(string[] args)
        {
            //var serviceLocator = new ServiceLocator();
            var service = new PersonSubscriptionService();
            
            // Create the ServiceHost.
            using (var host = new ServiceHost(typeof(PersonSubscriptionService), baseAddress))
            {
                // Enable metadata publishing.
                var smb = new ServiceMetadataBehavior
                              {
                                  HttpGetEnabled = true, MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                              };

                host.Description.Behaviors.Add(smb);

                host.AddServiceEndpoint(typeof (IPersonSubscriptionService), new WSDualHttpBinding(), baseAddress);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
