/****************** Description Class *********************************
 
Class for create format message Events (rows saved, updated, deleted)  
   
***********************************************************************/
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;


namespace Alvisoft.Helpers
{
    public enum MessageType { Success, Error, Notice, SuccessAd, ErrorAd, NoticeAd, MessageBox }

    public class MessageFormatter
    {


        public static string GetFormattedSuccessMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Success);
        }

        public static string GetFormattedSuccessAdMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.SuccessAd);
        }

        public static string GetFormattedErrorMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Error);
        }

        public static string GetFormattedErrorAdMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.ErrorAd);
        }

        public static string GetFormattedNoticeMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Notice);
        }

        public static string GetFormattedNoticeAdMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.NoticeAd);
        }


        public static string GetFormattedMsgBoxMessage ( string message )
            {
            return GetFormattedMessage ( message, MessageType.MessageBox );
            }

        public static string GetFormattedMessage(string message, MessageType messageType = MessageType.Notice)
        {
            switch (messageType)
            {
                case MessageType.Success: return "<div class='success'>" + message + "</div>";
                case MessageType.SuccessAd: return "<div class='successAd'>" + message + "</div>";
                case MessageType.Error: return "<div class='error'>" + message + "</div>";
                case MessageType.ErrorAd: return "<div class='errorAd'>" + message + "</div>";
                case MessageType.NoticeAd: return "<div class='noticeAd'>" + message + "</div>";
                case MessageType.MessageBox: return message;
                default: return "<div class='notice'>" + message + "</div>";
            }
        }
        
    }

    
   

}