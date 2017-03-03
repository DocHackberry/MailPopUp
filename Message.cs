using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace MailPopUp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Message : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private MessageInfo newMessage;
		private int MessageNum;
		private string strPrintText;
		private int iStartPage, iNumPages, iPageNumber;
		private PrintDocument prndoc = new PrintDocument();
		private PageSetupDialog setdlg = new PageSetupDialog();
		private PrintPreviewDialog predlg = new PrintPreviewDialog();
		private PrintDialog prndlg = new PrintDialog();

		private System.Windows.Forms.Label lblSender;
		private System.Windows.Forms.Label lblSubject;
		private System.Windows.Forms.GroupBox grpMessage;
		private System.Windows.Forms.GroupBox grpHeader;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.TextBox txtBody;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Message()
		{
			InitializeComponent();
			MessageNum = 0;
			this.Name = "Message" + MessageNum;
			this.Text = "Message: " + MessageNum;
			this.lblSender.Text = "Error";
			this.lblSubject.Text = "Error";
			this.txtBody.Text = "Invalid Message";

		}

		public Message(string mSender, string mSubject, string MessageBody, int MessageNumber)
		{
			newMessage = new MessageInfo(mSender, mSubject, MessageBody);
			InitializeComponent();
			MessageNum = MessageNumber;
			this.Name = "Message" + MessageNum;
			this.Text = "Message: " + MessageNum;
			this.lblSender.Text = "Sender: " + newMessage.Sender;
			this.lblSubject.Text = "Subject: " + newMessage.Subject;
			this.txtBody.Text = newMessage.Body;

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblSender = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblSubject = new System.Windows.Forms.Label();
			this.grpMessage = new System.Windows.Forms.GroupBox();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.grpHeader = new System.Windows.Forms.GroupBox();
			this.btnPrint = new System.Windows.Forms.Button();
			this.grpMessage.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblSender
			// 
			this.lblSender.Location = new System.Drawing.Point(16, 24);
			this.lblSender.Name = "lblSender";
			this.lblSender.Size = new System.Drawing.Size(448, 24);
			this.lblSender.TabIndex = 1;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(232, 248);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(112, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblSubject
			// 
			this.lblSubject.Location = new System.Drawing.Point(16, 48);
			this.lblSubject.Name = "lblSubject";
			this.lblSubject.Size = new System.Drawing.Size(448, 24);
			this.lblSubject.TabIndex = 8;
			// 
			// grpMessage
			// 
			this.grpMessage.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.txtBody});
			this.grpMessage.Location = new System.Drawing.Point(8, 80);
			this.grpMessage.Name = "grpMessage";
			this.grpMessage.Size = new System.Drawing.Size(464, 168);
			this.grpMessage.TabIndex = 9;
			this.grpMessage.TabStop = false;
			this.grpMessage.Text = "Message";
			// 
			// txtBody
			// 
			this.txtBody.Location = new System.Drawing.Point(8, 16);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ReadOnly = true;
			this.txtBody.Size = new System.Drawing.Size(448, 144);
			this.txtBody.TabIndex = 0;
			this.txtBody.Text = "";
			// 
			// grpHeader
			// 
			this.grpHeader.Location = new System.Drawing.Point(8, 8);
			this.grpHeader.Name = "grpHeader";
			this.grpHeader.Size = new System.Drawing.Size(464, 72);
			this.grpHeader.TabIndex = 10;
			this.grpHeader.TabStop = false;
			this.grpHeader.Text = "Header";
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(112, 248);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(112, 24);
			this.btnPrint.TabIndex = 0;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// Message
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 278);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnPrint,
																		  this.lblSubject,
																		  this.btnClose,
																		  this.lblSender,
																		  this.grpMessage,
																		  this.grpHeader});
			this.Name = "Message";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.grpMessage.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			strPrintText = newMessage.Body;
			PrintDocument prndoc = new PrintDocument();
			prndoc.DocumentName = "MailPopUp Message " + MessageNum.ToString();
			prndoc.PrintPage += new PrintPageEventHandler(OnPrintPage);
			prndoc.Print();
		}

		private void PrintDocumentOnPrintPage(object obj, PrintPageEventArgs printArgs)
		{
			Font printFont = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			int yPos = 0;
			Graphics pGraphics = printArgs.Graphics;
			yPos = printFont.Height * 4;
			pGraphics.DrawString("Sender: " + newMessage.Sender, printFont, Brushes.Black, 0, yPos);
			yPos += printFont.Height;
			pGraphics.DrawString("Subject: " + newMessage.Subject, printFont, Brushes.Black, 0, yPos);
			yPos += printFont.Height * 3;
			pGraphics.DrawString(newMessage.Body, printFont, Brushes.Black, 0, yPos);
			SizeF sizef = pGraphics.MeasureString(newMessage.Body, printFont);

			pGraphics.DrawLine(Pens.Black, sizef.ToPointF(), pGraphics.VisibleClipBounds.Size.ToPointF());

		}
		void OnPrintPage(object obj, PrintPageEventArgs ppea)
		{
			Graphics     grfx   = ppea.Graphics;
			Font         font   = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			float        cyFont = font.GetHeight(grfx);
			StringFormat strfmt = new StringFormat();
			RectangleF   rectfFull, rectfText;
			int          iChars, iLines;

			// Calculate RectangleF for header and footer.

			if (grfx.VisibleClipBounds.X < 0)       // Print preview
				rectfFull = ppea.MarginBounds;
			else                                    // Regular print
				rectfFull = new RectangleF(
					ppea.MarginBounds.Left - (ppea.PageBounds.Width - 
					grfx.VisibleClipBounds.Width) / 2,
					ppea.MarginBounds.Top - (ppea.PageBounds.Height - 
					grfx.VisibleClipBounds.Height) / 2,
					ppea.MarginBounds.Width, ppea.MarginBounds.Height);

			// Calculate RectangleF for text.

			rectfText = RectangleF.Inflate(rectfFull, 0, -2 * cyFont);

			int iDisplayLines = (int) Math.Floor(rectfText.Height / cyFont);
			rectfText.Height = iDisplayLines * cyFont;

			// Set up StringFormat object for rectanglar display of text.

			if (txtBody.WordWrap)
			{
				strfmt.Trimming = StringTrimming.Word;
			}
			else
			{
				strfmt.Trimming = StringTrimming.EllipsisCharacter;
				strfmt.FormatFlags |= StringFormatFlags.NoWrap;
			}
			// For "some pages" get to the first page.

			while ((iPageNumber < iStartPage) && (strPrintText.Length > 0))
			{
				if (txtBody.WordWrap)
					grfx.MeasureString(strPrintText, font, rectfText.Size, 
						strfmt, out iChars, out iLines);
				else
					iChars = CharsInLines(strPrintText, iDisplayLines);

				strPrintText = strPrintText.Substring(iChars);
				iPageNumber++;
			}
			// If we've prematurely run out of text, cancel the print job.

			if (strPrintText.Length == 0)
			{
				ppea.Cancel = true;
				return;
			}
			// Display text for this page

			grfx.DrawString(strPrintText, font, Brushes.Black, 
				rectfText, strfmt);

			// Get text for next page.

			if (txtBody.WordWrap)
				grfx.MeasureString(strPrintText, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
			else
				iChars = CharsInLines(strPrintText, iDisplayLines);

			strPrintText = strPrintText.Substring(iChars);

			// Reset StringFormat display header and footer.
          
			strfmt = new StringFormat();

			// Display filename at top.

			strfmt.Alignment = StringAlignment.Center;
			grfx.DrawString(newMessage.Subject, font, Brushes.Black, 
				rectfFull, strfmt);

			// Display page number at bottom.

			strfmt.LineAlignment = StringAlignment.Far;
			grfx.DrawString("Page " + iPageNumber, font, Brushes.Black, 
				rectfFull, strfmt);

			// Decide whether to print another page.

			iPageNumber++;
			ppea.HasMorePages = (strPrintText.Length > 0) && 
				(iPageNumber < iStartPage + iNumPages);

			// Reinitialize variables for printing from preview form.

			if (!ppea.HasMorePages)
			{
				strPrintText = txtBody.Text;
				iStartPage   = 1;
				iNumPages    = prndlg.PrinterSettings.MaximumPage;
				iPageNumber  = 1;
			}
		}
		int CharsInLines(string strPrintText, int iNumLines)
		{
			int index = 0;

			for (int i = 0; i < iNumLines; i++)
			{
				index = 1 + strPrintText.IndexOf('\n', index);

				if (index == 0)
					return strPrintText.Length;
			}
			return index;
		}
	}
}
