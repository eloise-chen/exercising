using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Net.Mail;



// For event receivers, don't use using to get current context/web because it will dispose of the object at the end and that may cause "Page cannot be displayed" errors or something.
// Exception is when you want to access an object not in the current context. Other types of things can use using, like timer jobs

// Next: Programmatically add the event receiver using a feature receiver instead of the xml way
// Also: expand on the information in the email body using properties

namespace Ex1_EventReceiver.ListItemNotification
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ListItemNotification : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// This function uses the SPUtility Send Email
        /// Pro: uses the SharePoint farm's SMTP settings so the developer doesn't need to know them
        /// Con: character limitation of 2048 per line, which can strip out the content of the email
        /// Note: Email body is in HTML and require HTML formatting.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            try
            {
                SPListItem item = properties.ListItem;
                SPUtility.SendEmail(
                        properties.Web, 
                        false, 
                        false, 
                        "Eloise.Chen@smsmt.com", 
                        "EventReceiver - Add",
                        "An item \"" + item.Title + "\" has been added to the Existing list.<br />" +
                        "The item has " + item.Attachments.Count + " attachments.\n" );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// An item was updated.
        /// This function uses the System.Net.Mail.MailMessage
        /// Pro: More granular control of the SMTP details and message, no character limitation of the message 
        /// Con: Requires knowledge of the SMTP details
        /// NOTE: Plain text email body
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            try
            {
                SPListItem item = properties.ListItem;
                
                MailMessage message = new MailMessage("eloise.chen@smstest.com", 
                                                        "eloise.chen@smsmt.com", 
                                                        "EventReceiver - Update",
                                                        "An item \"" + item.Title + "\" has been updated on the Existing list.\n" +
                                                        "The item has " + item.Attachments.Count + " attachments.\n" );
                SmtpClient client = new SmtpClient("d-int-sp02", 25);
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}