using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MailPopUp
{
	/// <summary>
	/// Summary description for MailPopUp.
	/// </summary>
	public class MailPopUp : System.Windows.Forms.Form
	{
		private NotifyIcon tIcon;
		private Timer CheckTimer;
		private MailChecker mailSystem;

		private System.ComponentModel.Container components = null;

		public MailPopUp()
		{
			InitializeComponent();

			mailSystem = new MailChecker();

			tIcon = new NotifyIcon();
			tIcon.Icon = 
				new System.Drawing.Icon (@".\Mail.ico");
			tIcon.Visible = true;
			tIcon.Text = "Mail Pop-Up";
			tIcon.ContextMenu = new ContextMenu();
			tIcon.ContextMenu.MenuItems.Add("Settings", new System.EventHandler(this.Icon_Settings));
			tIcon.ContextMenu.MenuItems.Add("Start", new System.EventHandler(this.Icon_Start));
			tIcon.ContextMenu.MenuItems.Add("Stop", new System.EventHandler(this.Icon_Stop));
			tIcon.ContextMenu.MenuItems.Add("-");
			tIcon.ContextMenu.MenuItems.Add("Exit", new System.EventHandler(this.Icon_Exit));

			CheckTimer = new Timer();
			CheckTimer.Enabled = true;
			CheckTimer.Tick += new EventHandler(CheckTimer_Tick);
			CheckTimer.Stop();

			Form sForm = new Settings(this);
			sForm.ShowDialog();
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				tIcon.Visible = false;
				tIcon.Dispose();
			}
			base.Dispose( disposing );
		}
		//Application Entry Point
		[STAThread]
		static void Main() 
		{
			Application.Run(new MailPopUp());
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// MailPopUp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 142);
			this.Enabled = false;
			this.Name = "MailPopUp";
			this.ShowInTaskbar = false;
			this.Text = "MailPopUp";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

		}
		#endregion

		private void CheckTimer_Tick(object Sender, EventArgs e)   
		{
			
			mailSystem.CheckMail();
			//For debugging purposes only:
			//CheckTimer.Stop();
		}

		//Event Handlers for menu options that appear in the pop-up menu on the Taskbar
		private void Icon_Settings(object sender, System.EventArgs e)
		{
			Form sForm = new Settings(this);
			sForm.ShowDialog();
		}
		private void Icon_Start(object sender, System.EventArgs e)
		{
			StartTimer();
		}
		private void Icon_Stop(object sender, System.EventArgs e)
		{
			CheckTimer.Stop();
		}
		private void Icon_Exit(object sender, System.EventArgs e)
		{
			CheckTimer.Stop();
			this.Close();
		}
		
		//Start a timer to check for messages periodically
		public void StartTimer()
		{
			string timeUnit = mailSystem.Units;
			if(timeUnit == "Hours")
				CheckTimer.Interval = mailSystem.Interval * 3600000;
			else if(timeUnit == "Minutes")
				CheckTimer.Interval = mailSystem.Interval * 60000;
			else  //Assume Seconds
				CheckTimer.Interval = mailSystem.Interval * 1000;

			if(CheckTimer.Interval > 0)
				CheckTimer.Start();
			else
			{
				Form sForm = new Settings();
				sForm.ShowDialog();
			}
		}
	}
}
