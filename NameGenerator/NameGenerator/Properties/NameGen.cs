using System;

namespace NameGenerator
{
	public class NameGen
	{
		public int min_l;
		public int max_l;

		private Random ran = new Random (new Guid().GetHashCode());

		private char[] h1 = {'r', 't', 'p', 'd', 'f', 'k', 'z', 'x', 'c', 'b', 'n', 'm'};
		private char[] h2 = {'r', 'l', 'j', 'n', 'm'};
		private char[] v = {'a', 'e', 'o', 'u', 'i', 'y'};

		public NameGen()
		{
			min_l = 4;
			max_l = 7;
		}

		public NameGen (int min_len, int max_len)
		{
			if (min_len < 3 || max_len > 12 || min_len > max_len) 
			{
				min_l = 4;
				max_l = 7;
			}
			else 
			{
				min_l = min_len;
				max_l = max_len;
			}
		}
			
		public String Generate()
		{
			int len = ran.Next (min_l, max_l + 1);
			String str = "", name;
			str = genStruct (len);
			name = parse (str);
			name = name.Remove(1).ToUpper() + name.Substring(1);
			return name;
		}

		protected String genStruct(int len)
		{
			//int _typ = ran.Next (1);
			int _num;
			int index = 2;
			int space = len;
			String str = "p";
			//str += (ran == 0) ? 'p' : 's';
			_num = Convert.ToInt32(ran.Next (1, 3));
			str += _num.ToString ();
			space -= _num;

			while (space > 0) 
			{
				
				_num = Convert.ToInt32(ran.Next (1, 3));

				_num = Math.Min (space, _num);

				if ((index / 2) % 2 == 1) {
					str += 's';
				} else
					str += 'p';
				str += _num.ToString ();
				space -= _num;
				index += 2;
			}

			return str;
		}

		protected String parse(String str)
		{
			String name = "";
			int _num;

			for (int i = 0; i < str.Length; i+=2) 
			{
				if (str [i] == 'p') 
				{
					_num = Convert.ToInt32 (ran.Next (h1.Length));
					name += h1 [_num];
					if (str [i + 1] == '2') 
					{
						_num = Convert.ToInt32 (ran.Next (h2.Length));
						name += h2 [_num];
					}
				}

				if (str [i] == 's') 
				{
					_num = Convert.ToInt32 (ran.Next (v.Length));
					name += v [_num];
					if (str [i + 1] == '2') 
					{
						if (_num > 2)
							_num = Convert.ToInt32 (ran.Next (3));
						else _num = Convert.ToInt32 (ran.Next (v.Length));
						name += v [_num];
					}
				}

			}
			return name;
		}
	}
}

