using System;

namespace MailPopUp
{
	/// <summary>
	/// Summary description for encrypt.
	/// </summary>
	public class encrypt
	{
		public encrypt()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private int char_to_int(char letter)
		{
			int decvalue=0;
			switch (letter)
			{
				case 'a': decvalue=0;break;
				case 'b': decvalue=1;break;
				case 'c': decvalue=2;break;
				case 'd': decvalue=3;break;
				case 'e': decvalue=4;break;
				case 'f': decvalue=5;break;
				case 'g': decvalue=6;break;
				case 'h': decvalue=7;break;
				case 'i': decvalue=8;break;
				case 'j': decvalue=9;break;
				case 'k': decvalue=10;break;
				case 'l': decvalue=11;break;
				case 'm': decvalue=12;break;
				case 'n': decvalue=13;break;
				case 'o': decvalue=14;break;
				case 'p': decvalue=15;break;
				case 'q': decvalue=16;break;
				case 'r': decvalue=17;break;
				case 's': decvalue=18;break;
				case 't': decvalue=19;break;
				case 'u': decvalue=20;break;
				case 'v': decvalue=21;break;
				case 'w': decvalue=22;break;
				case 'x': decvalue=23;break;
				case 'y': decvalue=24;break;
				case 'z': decvalue=25;break;
				case 'A': decvalue=26;break;
				case 'B': decvalue=27;break;
				case 'C': decvalue=28;break;
				case 'D': decvalue=29;break;
				case 'E': decvalue=30;break;
				case 'F': decvalue=31;break;
				case 'G': decvalue=32;break;
				case 'H': decvalue=33;break;
				case 'I': decvalue=34;break;
				case 'J': decvalue=35;break;
				case 'K': decvalue=36;break;
				case 'L': decvalue=37;break;
				case 'M': decvalue=38;break;
				case 'N': decvalue=39;break;
				case 'O': decvalue=40;break;
				case 'P': decvalue=41;break;
				case 'Q': decvalue=42;break;
				case 'R': decvalue=43;break;
				case 'S': decvalue=44;break;
				case 'T': decvalue=45;break;
				case 'U': decvalue=46;break;
				case 'V': decvalue=47;break;
				case 'W': decvalue=48;break;
				case 'X': decvalue=49;break;
				case 'Y': decvalue=50;break;
				case 'Z': decvalue=51;break;
				case ' ': decvalue=52;break;
				case '0': decvalue=53;break;
				case '1': decvalue=54;break;
				case '2': decvalue=55;break;
				case '3': decvalue=56;break;
				case '4': decvalue=57;break;
				case '5': decvalue=58;break;
				case '6': decvalue=59;break;
				case '7': decvalue=60;break;
				case '8': decvalue=61;break;
				case '9': decvalue=62;break;
			}
			return decvalue;
		}
		private char int_to_char(int decvalue)
		{
			char letter=' ';
			
			switch(decvalue)
			{
				case 0:letter='a';break;
				case 1:letter='b';break;
				case 2:letter='c';break;
				case 3:letter='d';break;
				case 4:letter='e';break;
				case 5:letter='f';break;
				case 6:letter='g';break;
				case 7:letter='h';break;
				case 8:letter='i';break;
				case 9:letter='j';break;
				case 10:letter='k';break;
				case 11:letter='l';break;
				case 12:letter='m';break;
				case 13:letter='n';break;
				case 14:letter='o';break;
				case 15:letter='p';break;
				case 16:letter='q';break;
				case 17:letter='r';break;
				case 18:letter='s';break;
				case 19:letter='t';break;
				case 20:letter='u';break;
				case 21:letter='v';break;
				case 22:letter='w';break;
				case 23:letter='x';break;
				case 24:letter='y';break;
				case 25:letter='z';break;
				case 26:letter='A';break;
				case 27:letter='B';break;
				case 28:letter='C';break;
				case 29:letter='D';break;
				case 30:letter='E';break;
				case 31:letter='F';break;
				case 32:letter='G';break;
				case 33:letter='H';break;
				case 34:letter='I';break;
				case 35:letter='J';break;
				case 36:letter='K';break;
				case 37:letter='L';break;
				case 38:letter='M';break;
				case 39:letter='N';break;
				case 40:letter='O';break;
				case 41:letter='P';break;
				case 42:letter='Q';break;
				case 43:letter='R';break;
				case 44:letter='S';break;
				case 45:letter='T';break;
				case 46:letter='U';break;
				case 47:letter='V';break;
				case 48:letter='W';break;
				case 49:letter='X';break;
				case 50:letter='Y';break;
				case 51:letter='Z';break;
				case 52:letter=' ';break;
				case 53:letter='0';break;
				case 54:letter='1';break;
				case 55:letter='2';break;
				case 56:letter='3';break;
				case 57:letter='4';break;
				case 58:letter='5';break;
				case 59:letter='6';break;
				case 60:letter='7';break;
				case 61:letter='8';break;
				case 62:letter='9';break;
			}

			return letter;
		}
		public string encrypt_str(string plain, string key)
		{
			
			string cipher="";
			string pblock=plain;
			bool done=false;
			int j2=0;
			char p=' ';
			char c=' ';
			char k=' ';

			int p_int=0;
			int c_int=0;
			int k_int=0;

			if(key.Length==0) 
				return "error: please input key";

			j2=0;
			for(int j=0;j<pblock.Length;j++)
			{
				p=pblock.Substring(j,1)[0];

				if (p=='.'||p=='/'||p=='?'||p=='&'||p==':'||p=='\\') 
				{
					cipher=cipher+p.ToString();
				}
				else
				{
					if(j2<key.Length-1)
						j2++;
					else
						j2=0;
					k=key.Substring(j2,1)[0];
					p_int=char_to_int(p);
					k_int=char_to_int(k);
			
					c_int=(p_int+k_int);
					done=false;
					while(!done)
					{
						if(c_int>63)
							c_int=c_int-63;
						else
							done=true;
					}
					c=int_to_char(c_int);
					cipher=cipher+c;
				}
			}
			return cipher;
		}
		public string decrypt_str(string plain, string key)
		{
			
			string cipher="";
			string pblock=plain;
			bool done=false;
			int j2=0;
			char p=' ';
			char c=' ';
			char k=' ';

			int p_int=0;
			int c_int=0;
			int k_int=0;

			if(key.Length==0) 
				return "error: please input key";

			j2=0;
			for(int j=0;j<pblock.Length;j++)
			{
				p=pblock.Substring(j,1)[0];

				if (p=='.'||p=='/'||p=='?'||p=='&'||p==':'||p=='\\') 
				{
					cipher=cipher+p.ToString();
				}
				else
				{
					if(j2<key.Length-1)
						j2++;
					else
						j2=0;
					k=key.Substring(j2,1)[0];
					p_int=char_to_int(p);
					k_int=char_to_int(k);
			
					c_int=(p_int-k_int);
					done=false;
					while(!done)
					{
						if(c_int<0)
							c_int=c_int+63;
						else
							done=true;
					}
					c=int_to_char(c_int);
					cipher=cipher+c;
				}
			}
			return cipher;
		}
		public string decrypt_str_old(string plain, string key)
		{
			
			string cipher="";
			string pblock=plain;
			bool done=false;
			int j2=0;
			char p=' ';
			char c=' ';
			char k=' ';

			int p_int=0;
			int c_int=0;
			int k_int=0;

			if(key.Length==0) 
				return "error: please input key";

			j2=0;
			for(int j=0;j<pblock.Length;j++)
			{
				p=pblock.Substring(j,1)[0];

				if (p=='.'||p=='/'||p=='?'||p=='&'||p==':'||p=='\\') 
				{
					cipher=cipher+p.ToString();
				}
				else
				{
					if(j2<key.Length-1)
						j2++;
					else
						j2=0;
					k=key.Substring(j2,1)[0];
					
					p_int=char_to_int(p);
					k_int=char_to_int(k);
			
					c_int=(p_int-k_int);
					done=false;
					while(!done)
					{
						if(c_int<0)
							c_int=c_int+63;
						else
							done=true;
					}
					c=int_to_char(c_int);
					cipher=cipher+c;
				}
			}
			return cipher;
		}
	}
}