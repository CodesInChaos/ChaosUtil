using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	public static class ComparisionHelper
	{
		public static int MultiHash(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		public static int MultiHash(int h1, int h2, int h3)
		{
			int h = (h1 << 5) + h1 ^ h2;
			return (h << 5) + h ^ h3;
		}

		public static int MultiHash(int h1, int h2, int h3, int h4)
		{
			return MultiHash(MultiHash(h1, h2), MultiHash(h3, h4));
		}

		public static int MultiHash(int h1, int h2, int h3, int h4, int h5)
		{
			return MultiHash(MultiHash(h1, h2), MultiHash(h3, h4, h5));
		}

		public static int MultiHash(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return MultiHash(MultiHash(h1, h2, h3), MultiHash(h4, h5, h6));
		}

		public static int MultiHash(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return MultiHash(MultiHash(h1, h2, h3, h4), MultiHash(h5, h6, h7));
		}

		public static int MultiHash(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return MultiHash(MultiHash(h1, h2, h3, h4), MultiHash(h5, h6, h7, h8));
		}
	}
}
