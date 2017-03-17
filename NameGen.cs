using System;
using System.Collections;

namespace NameGenerator
{
	public class NameGen
	{	
		//here the letters are broken into different groups.
		//the groups appear one after the other according to a ruleset written in grpRules()
		//the same group cannot appear twice in a row. some letters appear more than once in a certain
		//group to increase the probability of that letter appearing in a name. 
		private char[] p0 = {'d', 'f', 'h', 'v', 'j','n', 'm'};	
		private char[] p1 = {'t', 'p', 'k', 'c', 'b', 'g'};	
		private char[] p2 = {'r', 'r', 'l', 's'};	
		private char[] s1 = {'a', 'e', 'o','a', 'e', 'o', 'o', 'y'};
		private char[] s2 = {'a', 'e', 'o'};
		private string[] ss = {"th", "sh", "ch" , "w", "q", "z", "x",  "i", "kh"};	
		private string[] sp = {"w", "z", "x", "q", "th", "sh", "ch", "u"};	

		//a random value is chosen from this array to represent the length of the name.
		//current avg name length is 5.3 letters
		private int[] len_distribution = { 3, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 7, 8};

		//this is the method generates and returns a new random name
		public String Generate()
		{
			Guid id = Guid.NewGuid ();
			Random ran = new Random(id.GetHashCode());
			//length chosen randomly from 
			int len = len_distribution[Convert.ToInt32 (ran.Next (len_distribution.Length))]; 
			String struc = "", name = "";

			//sometimes the name could cut shorter than the rolled length
			//this is due to some rules on ending characters. 
			//in those cases we just roll a new structure and name
			while (name.Length < len) 
			{
				struc = genStruct (len);
				name = parse (struc);
			}

			//this capitalizes the name
			name = name.Remove(1).ToUpper() + name.Substring(1);

			return name;
		}

		//this method generates the structure (in groups) of the name
		//according to the rolled length
		protected String genStruct(int len)
		{
			int i = 0;
			String strct = "", curr = "00";
			ArrayList grp = new ArrayList();
		
			while (i < len) 
			{
				Guid id = Guid.NewGuid ();
				Random ran = new Random(id.GetHashCode());

				grp = grpRules (curr);
				curr = grp[Convert.ToInt32(ran.Next(grp.ToArray().Length))].ToString();
				strct += curr;
				i++;
			}

			//this is to check that the name didnt end on a particularly nasty combination of groups (p1p2)
			//and if it did, this resets the structure and a new one will be generated
			if (strct.Length >= 4)
			if (strct [strct.Length - 2] == strct [strct.Length - 4] && strct [strct.Length - 2] == 'p')
				strct = "";

			return strct;
		}

		//this method parses the group structure of the name into actual random letters
		//so for example "p1p2s2sp" --> "krex"
		protected String parse(String str)
		{
			String name = "";
			int _num;

			for (int i = 0; i < str.Length; i+=2) 
			{
				Guid id = Guid.NewGuid ();
				Random ran = new Random(id.GetHashCode());

				if (str [i] == 'p') 
				{
					if (str [i + 1] == '0') 
					{
						_num = Convert.ToInt32 (ran.Next (p0.Length));
						name += p0 [_num];
					}
					if (str [i + 1] == '1') 
					{
						_num = Convert.ToInt32 (ran.Next (p1.Length));
						name += p1 [_num];
					}
					if (str [i + 1] == '2') 
					{
						_num = Convert.ToInt32 (ran.Next (p2.Length));
						name += p2 [_num];
					}
				} 

				else if (str [i] == 's') 
				{
					if (str [i + 1] == '1') 
					{
						_num = Convert.ToInt32 (ran.Next (s1.Length));
						name += s1 [_num];
					}
					if (str [i + 1] == '2') 
					{
						_num = Convert.ToInt32 (ran.Next (s2.Length));
						name += s2 [_num];
					}
					if (str [i + 1] == 's') 
					{
						_num = Convert.ToInt32 (ran.Next (ss.Length));
						name += ss [_num];
					}
					if (str [i + 1] == 'p') 
					{
						_num = Convert.ToInt32 (ran.Next (sp.Length));
						name += sp [_num];
					}
				} 
			}
			return name;
		}
			
		//these are the rules of which group can follow after the current group
		//all names start with "00" that symbolizes the selection for the first group
		//curr - represents the group that has been added to the name in the previous iteration
		//sp - a finish only group, no other group can follow up after sp
		private ArrayList grpRules(String curr)
		{
			ArrayList arr = new ArrayList ();

			if (curr == "00") 
			{
				arr.Clear ();
				arr.Add ("p0"); 
				arr.Add ("p1"); 
				arr.Add ("p1"); 
				arr.Add ("s2");	
				arr.Add ("ss");			
			}
			else if (curr == "p0") 
			{
				arr.Clear (); 
				arr.Add ("s1");	
				arr.Add ("s2"); 
				arr.Add ("s2");
			}
			else if (curr == "p1") 
			{
				arr.Clear ();
				arr.Add ("p2"); 
				arr.Add ("p2"); 
				arr.Add ("s1");	
				arr.Add ("s2");
				arr.Add ("s2");
			}
			else if (curr == "p2") 
			{
				arr.Clear ();
				arr.Add ("s1");
				arr.Add ("s1");
				arr.Add ("s2");
				arr.Add ("s2");
				arr.Add ("s2");
			}
			else if (curr == "s1") 
			{
				arr.Clear ();
				arr.Add ("p0"); 
				arr.Add ("p1"); 
				arr.Add ("p2"); 
				arr.Add ("s2");
				arr.Add ("sp");
			}
			else if (curr == "s2") 
			{
				arr.Clear ();
				arr.Add ("p0"); 
				arr.Add ("p1"); 
				arr.Add ("p2"); 
				arr.Add ("sp");

			}
			else if (curr == "ss") 
			{
				arr.Clear ();
				arr.Add ("p2"); 
				arr.Add ("p2"); 
				arr.Add ("s1"); 
				arr.Add ("s2"); 
				arr.Add ("s2");

			}
			else if (curr == "sp" || curr == "") 
			{
				arr.Clear ();
				arr.Add (""); 
			}

			return arr;
		}
	}
}