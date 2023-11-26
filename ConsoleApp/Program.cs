using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
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



            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' savedqueryid='00000000-0000-0000-00aa-000010001003' no-lock='false' distinct='true'>
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
                </fetch>";

           EntityCollection collection =  service.RetrieveMultiple(new FetchExpression(query));

            foreach(Entity constact in collection.Entities)
            {
                Console.WriteLine(constact.Attributes["fullname"].ToString());
            }

            Console.Read();
        }
    }
}
