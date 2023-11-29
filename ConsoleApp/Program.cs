using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using static System.Net.WebRequestMethods;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        { // Authenticate
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycrm"].ConnectionString;
            CrmServiceClient service = new CrmServiceClient(connectionString);

            // 47. Introduction to Service.Execute Method
            //service.Execute(OrganizationRequest)
           /* Entity newContact = new Entity("contact");
            newContact.Attributes.Add("fullname", "Smith Yang");
            CreateRequest req = new CreateRequest();
            req.Target = newContact;

            CreateResponse response = (CreateResponse)service.Execute(req);

            Console.Read();*/

            // 40. querying data using FetchXML
            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' savedqueryid='00000000-0000-0000-00aa-000010001003' no-lock='false' distinct='true'>
                      <entity name='contact'>
                          <attribute name='entityimage_url'/>
                          <attribute name='fullname'/>
                          <attribute name='parentcustomerid'/>
                          <attribute name='telephone1'/>
                          <attribute name='emailaddress1'/>
                          <attribute name='contactid'/>
                          <order attribute='fullname' descending='true'/>
                          <filter type='and'>
                              <condition attribute='ownerid' operator='eq-userid'/>
                              <condition attribute='statecode' operator='eq' value='0'/>
                          </filter>
                      </entity>
                  </fetch>";

            EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

            foreach (Entity constact in collection.Entities)
            {
                // Console.WriteLine(((AliasedValue)constact.Attributes["NumberOfcontacts"]).Value.ToString());
                Console.WriteLine((constact.Attributes["fullname"]).ToString());
            }
            Console.Read();


            // 41. Aggregate Operations using Fetch XML
            /* string query = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                         <entity name='contact'>
                             <attribute name ='fullname' alias='NumberOfcontacts' aggregate='count' />
                         </entity>
                        </fetch>";
             EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

             foreach (Entity constact in collection.Entities)
             {
                  Console.WriteLine(((AliasedValue)constact.Attributes["NumberOfcontacts"]).Value.ToString());
             }
             Console.Read();*/




            //42. Querying data using LINQ - Late Binding
            /* using (OrganizationServiceContext context = new OrganizationServiceContext(service))
             {
                // Pull contacts
                var records = from c in context.CreateQuery("contact")
                              join
                              a in context.CreateQuery("account")
                              on c["parentcustomerid"] equals a["accountid"]
                              where c["parentcusmtomerid"] != null
                              select new
                              {
                                  FullName = c["fullname"],
                                  AccountName = a["name"]
                              };

                 foreach(var record in records)
                 {
            
                     Console.WriteLine(record.FullName+" "+record.AccountName);
                 }
             }*/

            // 46. Using LINQ with Early Binding
            /*using (OrganizationServiceContext context = new OrganizationServiceContext(service))
            {
                // Join with LINQ
                var records = from a in context.AccountsSet
                               where a.Address1_City = "Seattle"
                               select a;


            }*/



        }
    }
}