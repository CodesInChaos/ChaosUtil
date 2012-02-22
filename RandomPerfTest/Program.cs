using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chaos.Util;

namespace RandomPerfTest
{
	class Program
	{
		private static void NullGenerator(int[] randomData)
		{
		}

		static void Main(string[] args)
		{
			var start = DateTime.UtcNow;
			//var rng = new RandomGen(NullGenerator, 8 * 1024);
			var rng = RandomGen.CreateFast();
			Console.WriteLine(rng.GetType());
			int[] buckets = new int[0x10000];
			UInt64 sum = 0;
			for (int i = 0; i < 100000000; i++)
			{
				var u = rng.UInt64();
				sum += u;
				//int bucket = (int)(u * buckets.Length);
				//buckets[bucket]++;
			}
			Console.WriteLine(sum);
			Console.WriteLine((DateTime.UtcNow - start).TotalSeconds);
		}
	}
}
