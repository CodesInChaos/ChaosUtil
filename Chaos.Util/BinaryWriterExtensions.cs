using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System.IO
{
	public static class BinaryWriterExtensions
	{
		public static void WriteBigEndian(this BinaryWriter writer, ulong value, int bytes)
		{
			byte[] data = BitConverter.GetBytes(value);
			for (int i = bytes; i < sizeof(ulong); i++)
				if (data[i] != 0)
					throw new ArgumentException();
			for (int i = bytes - 1; i >= 0; i--)
				writer.Write(data[i]);
		}

		public static ulong ReadBigEndian(this BinaryReader reader, int bytes)
		{
			if ((uint)bytes > 8)
				throw new ArgumentException();
			ulong result = 0;
			for (int i = 0; i < bytes; i++)
			{
				result<<=8;
				result += reader.ReadByte();
			}
			return result;
		}
	}
}
