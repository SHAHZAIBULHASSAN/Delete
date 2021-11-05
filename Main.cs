using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delete
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string authType = "OAuth";
                string userName = "shahzaib@SHAHZAIBSAFDAR1.onmicrosoft.com";
                string password = "safdar786ALI!";
                string url = "https://org666f01ac.crm.dynamics.com";
                string appId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
                string reDirectURI = "app://58145B91-0C36-4500-8554-080854F2AC97";
                string loginPrompt = "Auto";
                string ConnectionString = string.Format("AuthType = {0};Username = {1};Password = {2}; Url = {3}; AppId={4}; RedirectUri={5};LoginPrompt={6}", authType, userName, password, url, appId, reDirectURI, loginPrompt);



                CrmServiceClient conn = new CrmServiceClient(ConnectionString);




            

                IOrganizationService service = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;

                string fetchxmlquery = @" 
                                            <fetch distinct='false' mapping='logical' aggregate='true'> 
                                                <entity name='account'> 
                                                   <attribute name='accountid' alias='account_count' aggregate='count'/> 
                                                </entity> 
                                            </fetch>";

                EntityCollection account_count = service.RetrieveMultiple(new FetchExpression(fetchxmlquery));

                foreach (var c in account_count.Entities)
                {
                    Int32 count = (Int32)((AliasedValue)c["account_count"]).Value;
                    System.Console.WriteLine("Count of all accounts: " + count);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }

}
