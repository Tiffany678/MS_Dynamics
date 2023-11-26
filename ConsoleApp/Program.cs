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
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using static System.Net.WebRequestMethods;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Authenticate
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycrm"].ConnectionString;
            CrmServiceClient service = new CrmServiceClient(connectionString);

            //Counting contact 
            /*string query = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                        <entity name='contact'>
                            <attribute name ='fullname' alias='NumberOfcontacts' aggregate='count' />
                        </entity>
                       </fetch>";*/


            // Operations using Fetch XML
            /*  string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' savedqueryid='00000000-0000-0000-00aa-000010001003' no-lock='false' distinct='true'>
                      <entity name='contact'>
                          <attribute name='entityimage_url'/>
                          <attribute name='fullname'/>
                          <attribute name='parentcustomerid'/>
                          <attribute name='telephone1'/>
                          <attribute name='emailaddress1'/>
                          <attribute name='contactid'/>
                          <order attribute='fullname' descending='false'/>
                          <filter type='and'>
                              <condition attribute='ownerid' operator='eq-userid'/>
                              <condition attribute='statecode' operator='eq' value='0'/>
                          </filter>
                      </entity>
                  </fetch>";*/

            //EntityCollection collection =  service.RetrieveMultiple(new FetchExpression(query));

            /* foreach(Entity constact in collection.Entities)
             {
                 Console.WriteLine(((AliasedValue)constact.Attributes["NumberOfcontacts"]).Value.ToString());
             }

             Console.Read();*/

            //LINQ Query
            using (OrganizationServiceContext context = new OrganizationServiceContext(service))
            {
                // Pull contacts
                var records = from contact in context.CreateQuery("contact")
                              select contact;
                foreach(var record in records)
                {
                    if(record.Attributes.Contains("fullname"))
                    Console.WriteLine(record.Attributes["fullname"].ToString());
                }
            }
        }
    }
}
