using System;
using System.Collections;

namespace MailPopUp
{
	/// <summary>
	/// Summary description for MessageInfo.
	/// </summary>
	public class MessageInfo
	{
		private string	mSender;
		public string	Sender
		{
			get
			{
				return	mSender;
			}
		}
		private string	mSubject;
		public string	Subject
		{
			get
			{
				return	mSubject;
			}
		}
		private string	mBody;
		public string	Body
		{
			get
			{
				return mBody;
			}
		}

		public MessageInfo()
		{
			mSender = null;
			mSubject = null;
			mBody = null;
		}
		public MessageInfo(MessageInfo oldMessage)
		{
			mSender = new string(oldMessage.Sender.ToCharArray());
			mSubject = new string(oldMessage.Subject.ToCharArray());
			mBody = new string(oldMessage.Body.ToCharArray());
		}
		public MessageInfo(string newSender, string newSubject, string newBody)
		{
			mSender = new string(newSender.ToCharArray());
			mSubject = new string(newSubject.ToCharArray());
			mBody = new string(newBody.ToCharArray());
		}
	}

	public class MessageQueue
	{
		public MessageInfo tempMessage;
		public ArrayList messageList;

		public MessageQueue()
		{
			messageList = new ArrayList();
			
		}

		public int Add(string newSender, string newSubject, string newBody)
		{
			tempMessage = new MessageInfo(newSender, newSubject, newBody);
			messageList.Add(tempMessage);
			return 0;
		}
		public int RemoveByIndex(string mIndex)
		{
			messageList.RemoveAt(mIndex);
		
			return 0;
		}
	}
}
