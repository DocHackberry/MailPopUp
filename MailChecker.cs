using System;
//Registry Information
using Microsoft.Win32;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;


namespace MailPopUp
{
	/// <summary>
	/// Summary description for MailChecker.
	/// </summary>
	public class MailChecker
	{
		private encrypt cryptString;
		private static string	cryptKey;
		private static string	pMailServer;
		public string			MailServer
		{
			get
			{
				return pMailServer;
			}
		}

		private static string	pUserName;
		public string	UserName
		{
			get
			{
				return pUserName;
			}
		}
	
		private static string	pPassword;
		public string	Password
		{
			get
			{
				return pPassword;
			}
		}

		private static int		pInterval;
		public int	Interval
		{
			get
			{
				return pInterval;
			}
		}

		private static string	pUnits;
		public string	Units
		{
			get
			{
				return pUnits;
			}
		}


		public MailChecker()
		{
			cryptKey = "aCtIoN";
			cryptString = new encrypt();
			pMailServer = cryptString.decrypt_str(GetRegKey("server"), cryptKey);
			if(pMailServer == null)
				pMailServer = "";

			pUserName = cryptString.decrypt_str(GetRegKey("user"), cryptKey);
			if(pUserName == null)
				pUserName = "";

			pPassword = cryptString.decrypt_str(GetRegKey("pass"), cryptKey);
			if(pPassword == null)
				pPassword = "";

			string tempInterval = GetRegKey("interval");
			if(tempInterval == null)
				pInterval = 1;
			else
				pInterval = Convert.ToInt32(tempInterval.ToString());

			pUnits = GetRegKey("units");
			if(pUnits == null)
				pUnits = "Seconds";
		}
		public int ChangeSettings(string userName, int mCheck, string mTime, string userPass, string serverName)
		{
			pMailServer = serverName;
			pInterval = mCheck;
			pUnits = mTime;
			pUserName = userName;
			pPassword = userPass;

			SetRegKey("user", cryptString.encrypt_str(pUserName, cryptKey));
			SetRegKey("pass", cryptString.encrypt_str(pPassword, cryptKey));
			SetRegKey("server", cryptString.encrypt_str(pMailServer, cryptKey));
			SetRegKey("interval", pInterval.ToString());
			SetRegKey("units", Units); 


			return 1;
		}

		private int SetRegKey(string keyName, string keyValue)
		{
			RegistryKey tempKey = Registry.LocalMachine.OpenSubKey("Software\\ActionVideo\\MailPopUp", true);
			// If the return value is null, the key doesn't exist
			if (tempKey == null) 
			{
				tempKey = Registry.LocalMachine.CreateSubKey("Software\\ActionVideo\\MailPopUp");	
				tempKey = Registry.LocalMachine.OpenSubKey("Software\\ActionVideo\\MailPopUp", true);
			}
			tempKey.SetValue(keyName, keyValue);

			return 1;
		}

		private string GetRegKey(string keyName)
		{
			RegistryKey tempKey = Registry.LocalMachine.OpenSubKey("Software\\ActionVideo\\MailPopUp");
			// If the return value is null, the key doesn't exist
			if (tempKey == null) 
				return null;
			else
			{
				if(tempKey.GetValue(keyName) != null)
					return tempKey.GetValue(keyName).ToString();
				else
					return null;
			}
		}

		public int CheckMail()
		{
			int numOfMessages = 0;
			TcpClient tcpClient = new TcpClient();
			MessageQueue currQueue = new MessageQueue();
			try
			{
				tcpClient.Connect(pMailServer, 110);
				NetworkStream networkStream = tcpClient.GetStream();

				if(networkStream.CanWrite && networkStream.CanRead)
				{
					// Read greeting
					byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
					networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);

					// Send user
					Byte[] sendBytes = Encoding.ASCII.GetBytes("USER " + pUserName + "@asite2see.com\r\n");
					networkStream.Write(sendBytes, 0, sendBytes.Length);

					// Read response
					bytes = new byte[tcpClient.ReceiveBufferSize];
					networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);

					// Send password
					sendBytes = Encoding.ASCII.GetBytes("PASS " + pPassword + "\r\n");
					networkStream.Write(sendBytes, 0, sendBytes.Length);

					// Read response
					bytes = new byte[tcpClient.ReceiveBufferSize];
					networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);

					// Send STAT
					sendBytes = Encoding.ASCII.GetBytes("STAT\r\n");
					networkStream.Write(sendBytes, 0, sendBytes.Length);

					// Read response
					bytes = new byte[tcpClient.ReceiveBufferSize];
					networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);
					string rawMailInfo = Encoding.ASCII.GetString(bytes);
					char [] Delimiters = {' ', '\n', Convert.ToChar(13)};
					string [] mailDropInfo = rawMailInfo.Split(Delimiters, 3);
					numOfMessages = Convert.ToInt32(mailDropInfo[1]);
					int stLength = (mailDropInfo[2].Length - 2);
					int sizeOfMessages = Convert.ToInt32(mailDropInfo[2].Substring(0, stLength));
					if(numOfMessages > 0)
					{
						for(int i = 0; i < numOfMessages; i++)
						{
							// Send RETR
							sendBytes = Encoding.ASCII.GetBytes("RETR " + (i + 1) + "\r\n");
							networkStream.Write(sendBytes, 0, sendBytes.Length);

							// Read response
							bytes = new byte[tcpClient.ReceiveBufferSize];
							networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);
							string messResult = Encoding.ASCII.GetString(bytes);
							string [] checkResult = messResult.Split(' ');
							if(checkResult[0] == "+OK")
							{
								bytes = new byte[tcpClient.ReceiveBufferSize];
								networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);
								string rawMessage = Encoding.ASCII.GetString(bytes);
								string [] parsedMessage = rawMessage.Split('\n');

								bytes = new byte[tcpClient.ReceiveBufferSize];
								networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);
								string messageEnd = Encoding.ASCII.GetString(bytes);
								//messageEnd should always equal "."

								string mSender = "";
								string mSubject = "";

								//display the message body
								int j = 0;
								while(parsedMessage[j].Substring(0, 7) != "X-UIDL:")
								{
									if(parsedMessage[j].Substring(0, 8) == "Subject:")
										mSubject = parsedMessage[j].Substring(8);
									if(parsedMessage[j].Substring(0, 5) == "From:")
										mSender = parsedMessage[j].Substring(5);
									j++;
								}
								j++;
								string tempBody = null;
								for(int k = 0; k < parsedMessage.Length - j; k++)
									tempBody += parsedMessage[j + k];
								Form messageForm = new Message(mSender, mSubject, /*cryptString.decrypt_str(*/tempBody/*, cryptKey)*/, i + 1);
								messageForm.Show();

								//Remove the current message from the mail server
								sendBytes = Encoding.ASCII.GetBytes("DELE " + (i + 1) + "\r\n");
								networkStream.Write(sendBytes, 0, sendBytes.Length);
								//Read Response
								bytes = new byte[tcpClient.ReceiveBufferSize];
								networkStream.Read(bytes, 0, (int) tcpClient.ReceiveBufferSize);
								string deleResult = Encoding.ASCII.GetString(bytes);
								//return value should be "+OK message x deleted"
							}

						}
					}
					/*
					else
					{
						MessageBox.Show("No new Messages");
					}
					*/


					// Send QUIT
					sendBytes = Encoding.ASCII.GetBytes("QUIT\r\n");
					networkStream.Write(sendBytes, 0, sendBytes.Length);

				}
				else if (!networkStream.CanRead)
				{
					//Console.WriteLine("You can not write data to this stream");
					tcpClient.Close();
				}
				else if (!networkStream.CanWrite)
				{             
					//Console.WriteLine("You can not read data from this stream");
					tcpClient.Close();
				}
			}
			catch (Exception e ) 
			{
				Console.WriteLine(e.ToString());
			}
			//System.Console.WriteLine("");
			
			//Check for new messages
			//parse/format messages

			//Display Messages
			/*
			for(int j = 0; j < numOfMessages; j++)
			{
				Form messageForm = new Message();
				messageForm.Show();
			}
			*/
			return 0;
		}
	}
}
