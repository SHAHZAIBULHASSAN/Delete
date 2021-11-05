using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;

namespace Delete
{
    public class Delete : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for use in debugging sandboxed plug-ins.

            ITracingService tracingService =

            (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            // Obtain the execution context from the service provider.

            IPluginExecutionContext context =

            (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));


            // Obtain the organization service factory.

            IOrganizationServiceFactory serviceFactory =

            (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));


            // Obtain the organization service.    
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);


            if (context.InputParameters.Contains("Target") &&

                          context.InputParameters["Target"] is Entity)

            {

                // Obtain the target entity from the input parameters.

                Entity entity = (Entity)context.InputParameters["Target"];

                // User is updating only email address in lead form so we will get only 
               
        // get the lead email from context.

                string email = entity["emailaddress1"].ToString();

                // get the current record guid from context

                Guid leadRecordGuid = entity.Id;


                // Define variables to store Preimage and Postimage
                string pretopic = string.Empty; string posttopic = string.Empty; 

                // in below leadimage has been added in plugin registration tool

                // get PreImage from Context

                if (context.PreEntityImages.Contains("LeadTopicImage") && context.PreEntityImages["LeadTopicImage"] is Entity)
                {

                    Entity preMessageImage = (Entity)context.PreEntityImages["LeadTopicImage"];
                    // get topic field value before database update perform
                    pretopic = (String)preMessageImage.Attributes["subject"];

                }


                // get PostImage from Context
                if (context.PostEntityImages.Contains("LeadTopicImage") &&
                       context.PostEntityImages["LeadTopicImage"] is Entity)

                {

                    Entity postMessageImage = (Entity)context.PostEntityImages["LeadTopicImage"];

                    // get topic field value after database update performed
                    posttopic = (String)postMessageImage.Attributes["subject"];

                }


                // update the old and new values of topic field in description field

                Entity leadObj = new Entity("lead", context.PrimaryEntityId);
                //service.Retrieve(context.PrimaryEntityName, leadRecordGuid, new ColumnSet("description"));

                leadObj["description"] =
                "Pre-Image of description- " + pretopic + "   " + "Post-Image of description-- " + posttopic;

                service.Update(leadObj);

            }





            //protected void ExecutePostAccountUpdateContacts(LocalPluginContext localContext)
            //    {

            //        if (localContext == null)
            //        {
            //            throw new ArgumentNullException("localContext");
            //        }


            //        IPluginExecutionContext context = localContext.PluginExecutionContext;

            //        //Get a IOrganizationService
            //        IOrganizationService service = localContext.OrganizationService;

            //        //create a service context
            //        var ServiceContext = new OrganizationServiceContext(service);
            //        //ITracingService tracingService = localContext.TracingService;

            //        // The InputParameters collection contains all the data passed in the message request.
            //        if (context.InputParameters.Contains("Target") &&
            //        context.InputParameters["Target"] is Entity)
            //        {
            //            // Obtain the target entity from the input parmameters.
            //            Entity entity = (Entity)context.InputParameters["Target"];

            //            //get the customerid
            //            EntityReference a = (EntityReference)entity.Attributes["customerid"];

            //            decimal totalAmount = 0;

            //            try
            //            {
            //                //fetchxml to get the sum total of estimatedvalue
            //                string estimatedvalue_sum = string.Format(@" 
            //        <fetch distinct='false' mapping='logical' aggregate='true'> 
            //            <entity name='opportunity'> 
            //                <attribute name='estimatedvalue' alias='estimatedvalue_sum' aggregate='sum' /> 
            //                <filter type='and'>
            //                    <condition attribute='statecode' operator='eq' value='Open' />
            //                        <condition attribute='customerid' operator='eq' value='{0}' uiname='' uitype='' />
            //                </filter>
            //            </entity>
            //        </fetch>", a.Id);
            //                EntityCollection estimatedvalue_sum_result = service.RetrieveMultiple(new FetchExpression(estimatedvalue_sum));

            //                foreach (var c in estimatedvalue_sum_result.Entities)
            //                {
            //                    totalAmount = ((Money)((AliasedValue)c["estimatedvalue_sum"]).Value).Value;
            //                }

            //                //updating the field on the account
            //                Entity acc = new Entity("account");
            //                acc.Id = a.Id;
            //                acc.Attributes.Add("shah_totalnumberofcontacts", new Money(totalAmount));
            //                service.Update(acc);


            //            }
            //            catch (FaultException ex)
            //            {
            //                throw new InvalidPluginExecutionException("An error occurred in the plug-in.", ex);
            //            }
            //        }

            #region code
            //IPluginExecutionContext context = (IPluginExecutionContext)serviceprovider.GetService(typeof(IPluginExecutionContext));
            //IOrganizationServiceFactory servicefactory = (IOrganizationServiceFactory)serviceprovider.GetService(typeof(IOrganizationServiceFactory));
            //IOrganizationService service = servicefactory.CreateOrganizationService(context.UserId);

            //if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference)
            //{
            //    //If Delete, you should use "entityReference". Not Entity

            //    EntityReference entityreference = (EntityReference)context.InputParameters["Target"];

            //    if (context.MessageName == "Delete")
            //    {
            //        QueryExpression query = new QueryExpression();

            //        // Set the properties of the QueryExpression object.





            //        // Retrieve the contacts.

            //        BusinessEntityCollection contacts = service.RetrieveMultiple(query);
            //        Entity ent = service.RetrieveMultiple(entityreference.LogicalName, entityreference.Id, new ColumnSet("shah_"));

            //        // Product Name is a Look Up Field That Contain the name of product which i want to update the quantity.
            //        var productname = ent.GetAttributeValue<EntityReference>("new_productname");

            //        if (productname != null)
            //        {
            //            // Getting quantity field from child entity
            //            Entity product = service.Retrieve(productname.LogicalName, productname.Id, new ColumnSet("shah_totalnumberofcontacts"));

            //            // Update the quantity on child entity
            //            var number = product.GetAttributeValue<Decimal>("shah_totalnumberofcontacts");
            //            var sisaunit = number + 1;
            //            product.Attributes["shah_totalnumberofcontacts"] = sisaunit;
            //            service.Update(product);

            //        }



        }
    }
}
        #endregion




        //    IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
        //ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
        //Entity entity = null;
        //if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
        //{
        //    entity = (Entity)context.InputParameters["Target"];
        //}
    
    
        
   
 
