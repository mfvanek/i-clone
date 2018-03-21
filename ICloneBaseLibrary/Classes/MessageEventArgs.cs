using System;

namespace ICloneBaseLibrary.Classes
{
   /// <summary>
   /// Вспомогательный класс для уведомления о состоянии события
   /// </summary>
   public class MessageEventArgs : EventArgs
   {
      /// <summary>
      /// Сообщение о состоянии события
      /// </summary>
      protected string m_Message;

      public MessageEventArgs(string _Message)
      {
         m_Message = _Message;
      }

      public MessageEventArgs()
         : this(string.Empty)
      { }

      /// <summary>
      /// Сообщение о состоянии события
      /// </summary>
      public string Message
      {
         get
         {
            return m_Message;
         }

         set
         {
            m_Message = value;
         }
      }
   }

   public delegate void MessageEventHandler(object sender, MessageEventArgs e);
}