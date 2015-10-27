using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Reflection;

namespace Ex1_EventReceiver.Features.EmailItemAdded
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("3a12309e-4727-4554-b4e3-27906d194b69")]
    public class EmailItemAddedEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        // QUESTION: Why do JavaScript errors appear when items added, whether Existing or other unrelated lists
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb;
            SPList list = web.Lists["Existing"];
            string assembly = Assembly.GetExecutingAssembly().FullName;
            string cls = "Ex1_EventReceiver.ListItemNotification.ListItemNotification";

            // Bind the event receiver definition to the list and define the receiver properties
            SPEventReceiverDefinition defn = list.EventReceivers.Add();
            defn.Type = SPEventReceiverType.ItemAdded;
            defn.Synchronization = SPEventReceiverSynchronization.Asynchronous;
            defn.Class = cls;
            defn.Assembly = assembly;
            defn.Update();

            SPEventReceiverDefinition defn2 = list.EventReceivers.Add();
            defn2.Type = SPEventReceiverType.ItemUpdated;
            defn2.Synchronization = SPEventReceiverSynchronization.Asynchronous;
            defn2.Class = cls;
            defn2.Assembly = assembly;
            defn2.Update();
            
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
