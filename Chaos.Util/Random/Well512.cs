using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	//http://stackoverflow.com/a/1227137
	internal class Well512
	{
		public Well512()
		{
			rngstate = new UInt32[16];
			for (int i = 0; i < 16; i++)
				rngstate[i] = RandomGen.Default.UInt32();
		}

		/* initialize state to random bits */
		UInt32[] rngstate;
		/* init should also reset this to 0 */
		int index;

		public UInt32 GenerateUInt32()
		{
			UInt32 a, b, c, d;
			a = rngstate[index];
			c = rngstate[(index + 13) & 15];
			b = a ^ c ^ (a << 16) ^ (c << 15);
			c = rngstate[(index + 9) & 15];
			c ^= (c >> 11);
			a = rngstate[index] = b ^ c;
			d = a ^ ((a << 5) & 0xDA442D20u);
			index = (index + 15) & 15;
			a = rngstate[index];
			rngstate[index] = a ^ b ^ d ^ (a << 2) ^ (b << 18) ^ (c << 28);
			return rngstate[index];
		}

		public void GenerateInts(int[] randomData)
		{
			for (int i = 0; i < randomData.Length; i++)
				randomData[i] = (int)GenerateUInt32();
		}
	}
}
