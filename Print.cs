using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Data;
//using System.DateTime;

namespace questor
{
	public class Print_item
	{
		public string subject;
		public string p1;
		public string p2;
		public string p3;
		public string p4;
		public string p5;

	}
	/// <summary>
	/// Summary description for Print.
	/// </summary>
	public class Print
	{
		private int start=1;
		private Data				db_conn=new Data();
		private PrintDocument		prndoc = new PrintDocument();
		private PageSetupDialog		setdlg = new PageSetupDialog();
		private PrintPreviewDialog	predlg = new PrintPreviewDialog();
		private PrintDialog			prndlg = new PrintDialog();
		private int					iPageNumber;
		private string user_id;
		private string session;
		private string lan;
		public string header;
		private Print_item []p_item =new Print_item[50];
		private int p_item_count=0;
		public float len =0;
		public int onpaint_count=0;

		public Print(string user_id, string session, string lan)
		{
			this.user_id=user_id;
			this.session=session;
			this.lan=lan;

			prndoc.PrintPage += new PrintPageEventHandler(OnPrintPage);
			setdlg.Document = prndoc;
			predlg.Document = prndoc;
			prndlg.Document = prndoc;

			prndlg.AllowSomePages = true;
			prndlg.PrinterSettings.FromPage = 1;
			prndlg.PrinterSettings.ToPage = 
				prndlg.PrinterSettings.MaximumPage;
			Get_Printable_report();
			Get_Printable_header();
		}
		public void print_to_paper()
		{
			//prndlg.AllowSelection = 0 > 0;
			prndlg.AllowSelection=false;
			prndlg.AllowSomePages=false;

			if (prndlg.ShowDialog() == DialogResult.OK)
			{
				iPageNumber  = 1;
				prndoc.Print();
				
			}
				// And commence printing.
		}
		public bool Get_Printable_report()
		{
			string str="";
			string CatKey="";
			string CatKey_display="";
			int res=0;
			bool ret=true;
			System.Data.OleDb.OleDbCommand oleDbCommand1 = new System.Data.OleDb.OleDbCommand();
			System.Data.OleDb.OleDbConnection oleDbConnection1 = new System.Data.OleDb.OleDbConnection();;
			oleDbConnection1.ConnectionString = db_conn.conn;
			//			DataTable dt;
			DataColumn dc;
			DataColumn dc2;
			System.Data.DataSet dataSet = new System.Data.DataSet();
			System.Data.DataSet dataSet2 = new System.Data.DataSet();
			System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();


			try
			{
				
				oleDbConnection1.Open();
				oleDbCommand1.Connection = oleDbConnection1;
				oleDbCommand1.CommandText = "select * from Print order by [order]";
				
				oleDbDataAdapter1.SelectCommand = oleDbCommand1;
				oleDbDataAdapter1.Fill(dataSet,0,999,"print");	

				foreach(DataRow dr in dataSet.Tables["print"].Rows)
				{
					dc=dataSet.Tables["print"].Columns["CatKey"];	
					if(dr[dc]!=System.DBNull.Value)
						CatKey=(string)dr[dc];	
					dc=dataSet.Tables["print"].Columns["Name"];	
					if(dr[dc]!=System.DBNull.Value)
						CatKey_display=(string)dr[dc];	
				
					if(CatKey.CompareTo("BREAK")!=0)
					{
						oleDbCommand1.CommandText = "select  user_resp.response_number, questions"+lan+".response1, questions"+lan+".response2,questions"+lan+".response3, "+
							"questions"+lan+".response4, user_resp.priority "+
							"from user_resp, questions"+lan+" "+
							"where user_resp.order_id=questions_en.order and "+
							"user_resp.CatKey='"+CatKey+"' and questions"+lan+".response1<>'NPR' and "+ 
							"user_resp.user_id='"+user_id+"' and "+
							"user_resp.session_number="+session+" "+
							"order by user_resp.priority, user_resp.response_number ";
						oleDbDataAdapter1.SelectCommand = oleDbCommand1;
						if(dataSet.Tables.Count>1)
							if(dataSet.Tables["resp"].Rows.Count>0)
								dataSet.Tables["resp"].Clear();
						oleDbDataAdapter1.Fill(dataSet,0,999,"resp");
						if(dataSet.Tables["resp"].Rows.Count>0)
						{
							this.p_item_count++;
							this.p_item[p_item_count]= new Print_item();
							//init vars/////////////////
							p_item[p_item_count].p1="";
							p_item[p_item_count].p2="";
							p_item[p_item_count].p3="";
							p_item[p_item_count].p4="";
							//end init vars/////////////
							this.p_item[p_item_count].subject=CatKey_display;
							str+="\n\n" + CatKey + "\n";
							foreach (DataRow dr2 in dataSet.Tables["resp"].Rows)
							{
								
								dc2=dataSet.Tables["resp"].Columns[0];	
								res=(int)dr2[dc2];
								
								dc2=dataSet.Tables["resp"].Columns[res];	
								str=(string)dr2[dc2]+" ";
								
								dc2=dataSet.Tables["resp"].Columns[5];	
								int pri=(int)dr2[dc2];

								if(pri==1)
									p_item[p_item_count].p1+=str;
								else if(pri==2)
									p_item[p_item_count].p2+=str;
								else if(pri==3)
									p_item[p_item_count].p3+=str;
								else
									p_item[p_item_count].p4+=str;

							}
						}
					}
				}
				
			}
			catch(System.Exception e)
			{
				ret=false;
				MessageBox.Show(e.ToString());
			}
			finally 
			{
				oleDbConnection1.Close();
			}

			
			return ret;
		}
		
		public bool Get_Printable_header()
		{

			bool ret=true;
			System.Data.OleDb.OleDbCommand oleDbCommand1 = new System.Data.OleDb.OleDbCommand();
			System.Data.OleDb.OleDbConnection oleDbConnection1 = new System.Data.OleDb.OleDbConnection();;
			oleDbConnection1.ConnectionString = db_conn.conn;
			//			DataTable dt;
			DataColumn dc;
			System.Data.DataSet dataSet = new System.Data.DataSet();
			System.Data.DataSet dataSet2 = new System.Data.DataSet();
			DateTime today= DateTime.Now;
			DateTime bday;
			TimeSpan age;
			string bday_str="";
			int year, month, day;
			System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();


			try
			{
				
				oleDbConnection1.Open();
				oleDbCommand1.Connection = oleDbConnection1;
				oleDbCommand1.CommandText = "SELECT * FROM users where [id]='"+this.user_id+"'";
				
				oleDbDataAdapter1.SelectCommand = oleDbCommand1;
				oleDbDataAdapter1.Fill(dataSet,0,999,"header");	

				DataRow dr= dataSet.Tables["header"].Rows[0];

				dc=dataSet.Tables["header"].Columns["lname"];	
				if(dr[dc]!=System.DBNull.Value)
					header+=(string)dr[dc]+", ";
				dc=dataSet.Tables["header"].Columns["fname"];	
				if(dr[dc]!=System.DBNull.Value)
					header+=(string)dr[dc]+"     ";
		
				dc=dataSet.Tables["header"].Columns["bday"];	
				if(dr[dc]!=System.DBNull.Value)
					bday_str=(string)dr[dc];

				string [] temp2=bday_str.Split('-');
				month=Convert.ToInt32(temp2[0]);
				day=Convert.ToInt32(temp2[1]);
				year=Convert.ToInt32(temp2[2]);
				if(today.Month<month)
					year++;

				bday= new DateTime(year,month,day);
				age =today.Subtract(bday);
			
				DateTime age_dt=new DateTime(age.Ticks);
				header+=age_dt.Year.ToString()+"     ";
				header+=bday_str+"     ";
				header+=this.db_conn.Clinic;
				
			}
			catch(System.Exception e)
			{
				ret=false;
				MessageBox.Show(e.ToString());
			}
			finally 
			{
				oleDbConnection1.Close();
			}

			
			return ret;
		}
		void OnPrintPage(object obj, PrintPageEventArgs ppea)
		{
			Graphics     grfx   = ppea.Graphics;
			Font         font	= new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			Font         font_b = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold |System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			float        cyFont_heading = font_b.GetHeight(grfx);
			float		 cyFont = font.GetHeight(grfx);
			PointF		 ptf	= new PointF(0,0);
			StringFormat strfmt = new StringFormat();
			float temp_h;
			float t_height;

			RectangleF   rectfFull, rectfText;
			int          iChars, iLines;

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
			ptf.X=rectfText.X;
			ptf.Y=rectfText.Y;
			float init_Y=rectfText.Y;

			// Set up StringFormat object for rectanglar display of text.
			strfmt.Trimming = StringTrimming.Word;

			// Display text for this page
			bool atend=true;
			for(int i=start;i<=this.p_item_count;i++)
			{
				//subject
				grfx.DrawString(p_item[i].subject, font_b, Brushes.Black, 
				rectfText, strfmt);
				rectfText.Height-=cyFont_heading;
				rectfText.Y+=cyFont_heading;
				//

				//p1
				grfx.DrawString(p_item[i].p1, font, Brushes.Red, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p1, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				if(iChars<p_item[i].p1.Length && atend)
				{
					atend=false;
					p_item[i].subject="";
					p_item[i].p1=p_item[i].p1.Substring(iChars);
				}
				temp_h= cyFont * iLines;
				if(temp_h>0)
					temp_h+=cyFont;
				rectfText.Y +=temp_h;
				rectfText.Height-=temp_h;
				//p2
				grfx.DrawString(p_item[i].p2, font, Brushes.Black, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p2, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				if(iChars<p_item[i].p2.Length && atend)
				{
					atend=false;
					p_item[i].subject="";
					p_item[i].p1="";
					p_item[i].p2=p_item[i].p2.Substring(iChars);
				}
				temp_h=  cyFont * iLines;
				rectfText.Y +=temp_h;
				rectfText.Height-=temp_h;
				//p3
				grfx.DrawString(p_item[i].p3, font, Brushes.Black, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p3, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				if(iChars<p_item[i].p3.Length && atend)
				{
					atend=false;
					p_item[i].subject="";
					p_item[i].p1="";
					p_item[i].p2="";
					p_item[i].p3=p_item[i].p3.Substring(iChars);
				}
				temp_h= cyFont * iLines;
				rectfText.Y +=temp_h;
				rectfText.Height-=temp_h;
				//p4///////////////////////////////////////////////////
				grfx.DrawString(p_item[i].p4, font, Brushes.Black, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p4, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				if(iChars<p_item[i].p4.Length && p_item[i].p4.Length>0 && atend)
				{
					atend=false;
					p_item[i].subject="";
					p_item[i].p1="";
					p_item[i].p2="";
					p_item[i].p3="";
					p_item[i].p4=p_item[i].p4.Substring(iChars);
				}
				temp_h= cyFont * iLines;
				rectfText.Y +=temp_h;
				rectfText.Height-=temp_h;
				//end p4///////////////////////////////////////////////

				if(rectfText.Height<=0)
				{
					if(atend)
						i++;
					start=i;
					i=p_item_count+1;
					ppea.HasMorePages = true;					
				}

				
			}

			// Reset StringFormat display header and footer. 
			strfmt = new StringFormat();

			// Display filename at top.
			strfmt.Alignment = StringAlignment.Center;
			grfx.DrawString(header, font, Brushes.Black, 
				rectfFull, strfmt);

			// Display page number at bottom.
			strfmt.LineAlignment = StringAlignment.Far;
			grfx.DrawString("Page " + iPageNumber, font, Brushes.Black, 
				rectfFull, strfmt);

			if(ppea.HasMorePages)
				iPageNumber++;

		}

		public void Print_to_panel(object obj, System.Windows.Forms.PaintEventArgs ppea)
		{
			Graphics     grfx   = ppea.Graphics;
			Font         font	= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			Font         font_b = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold |System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			float        cyFont_heading = font_b.GetHeight(grfx);
			float		 cyFont = font.GetHeight(grfx);
			PointF		 ptf	= new PointF(0,0);
			StringFormat strfmt = new StringFormat();
			float temp_h;
			if(onpaint_count<1)
				len=this.Print_to_page_length(grfx);
			else
				onpaint_count++;
			

			RectangleF   rectfFull, rectfText;
			rectfFull = new RectangleF(0,0,20,500);
			int          iChars, iLines;

			// Calculate RectangleF for text.
			rectfText = new RectangleF(0,0,600,999999999);
			int iDisplayLines = (int) Math.Floor(rectfText.Height / cyFont);
			rectfText.Height = iDisplayLines * cyFont;
			ptf.X=0F;
			ptf.Y=0F;
			float init_Y=rectfText.Y;

			// Set up StringFormat object for rectanglar display of text.
			strfmt.Trimming = StringTrimming.Word;

			// Display text for this page
			for(int i=1;i<=this.p_item_count;i++)
			{
				//subject///////////////////////////////////////////
				grfx.DrawString(p_item[i].subject, font_b, Brushes.Black, 
					rectfText, strfmt);
				rectfText.Y +=cyFont_heading;
				//p1/////////////////////////////////////////////
				grfx.DrawString(p_item[i].p1, font, Brushes.Red, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p1, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h= cyFont * iLines ;
				if(temp_h>0)
					temp_h+=cyFont;
				rectfText.Y +=temp_h;
				//p2//////////////////////////////////////////////
				grfx.DrawString(p_item[i].p2, font, Brushes.Black, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p2, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h=  cyFont * iLines;
				rectfText.Y +=temp_h;
				//p3//////////////////////////////////////////////
				grfx.DrawString(p_item[i].p3, font, Brushes.Black, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p3, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h= cyFont * iLines;
				rectfText.Y +=temp_h;
				//p4///////////////////////////////////////////////////
				grfx.DrawString(p_item[i].p4, font, Brushes.Black, 
					rectfText, strfmt);
				grfx.MeasureString(p_item[i].p4, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h= cyFont * iLines;
				rectfText.Y +=temp_h;
				//end p4///////////////////////////////////////////////		
			}
		}
		private float Print_to_page_length(Graphics gr)
		{
			Graphics     grfx= gr;
			float len=0F;
			float temp_h;
			Font         font	= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			Font         font_b = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold |System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			float        cyFont_heading = font_b.GetHeight(grfx);
			float		 cyFont = font.GetHeight(grfx);
			StringFormat strfmt = new StringFormat();
			RectangleF   rectfText;
			int          iChars, iLines;
			
			rectfText = new RectangleF(0,0,600,999999999);

			for(int i=1;i<=this.p_item_count;i++)
			{
				//subject///////////////////////////////////////////
				len +=cyFont_heading;
				//p1/////////////////////////////////////////////
				grfx.MeasureString(p_item[i].p1, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h= cyFont * iLines ;
				if(temp_h>0)
					temp_h+=cyFont;
				len +=temp_h;
				//p2//////////////////////////////////////////////
				grfx.MeasureString(p_item[i].p2, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h=  cyFont * iLines;
				len +=temp_h;
				//p3//////////////////////////////////////////////
				grfx.MeasureString(p_item[i].p3, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h= cyFont * iLines;
				len +=temp_h;
				//p4///////////////////////////////////////////////////
				grfx.MeasureString(p_item[i].p4, font, rectfText.Size, 
					strfmt, out iChars, out iLines);
				temp_h= cyFont * iLines;
				len +=temp_h;
				//end p4///////////////////////////////////////////////		
			}
			
			return len;
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
