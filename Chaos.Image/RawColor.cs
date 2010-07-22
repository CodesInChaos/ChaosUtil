using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Chaos.Util;
using System.Drawing;

namespace Chaos.Image
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RawColor
	{
		private readonly uint raw;

		public uint ARGB { get { return raw; } }
		public byte A { get { return (byte)(raw >> 24); } }
		public byte R { get { return (byte)(raw >> 16); } }
		public byte G { get { return (byte)(raw >> 8); } }
		public byte B { get { return (byte)raw; } }
		public uint RGB { get { return raw & 0x00FFFFFF; } }

		private RawColor(uint raw)
		{
			this.raw = raw;
		}

		public static RawColor FromRGB(uint rgb)
		{
			return new RawColor(0xFF000000u | rgb);
		}

		public static RawColor FromRGB(int rgb)
		{
			return FromRGB((uint)rgb);
		}

		public static RawColor FromRGB(byte r, byte g, byte b)
		{
			return FromRGB((uint)r << 16 | (uint)g << 8 | (uint)b);
		}

		public static RawColor FromARGB(int argb)
		{
			return new RawColor((uint)argb);
		}

		public static RawColor FromARGB(UInt32 argb)
		{
			return new RawColor(argb);
		}

		public static RawColor FromARGB(byte a, byte r, byte g, byte b)
		{
			return new RawColor((uint)a << 24 | (uint)r << 16 | (uint)g << 8 | (uint)b);
		}

		public static RawColor FromARGB(byte a, RawColor rgb)
		{
			return new RawColor((uint)a << 24 | (rgb.raw & 0xFFFFFF));
		}

		public static RawColor FromColor(Color color)
		{
			return FromARGB(color.ToArgb());
		}

		public static bool operator ==(RawColor c1, RawColor c2)
		{
			return c1.raw == c2.raw;
		}

		public static bool operator !=(RawColor c1, RawColor c2)
		{
			return c1.raw != c2.raw;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return this == (RawColor)obj;
		}

		public override int GetHashCode()
		{
			return (int)raw;
		}

		public float GetUnweightedBrightness()
		{
			return (R + G + B) * (1f / (255 * 3));
		}

		public override string ToString()
		{
			if (A == 0xFF)
				return RGB.ToHex(6);
			else
				return ARGB.ToHex(8);
		}

		public static RawColor Transparent { get { return new RawColor(); } }
		public static RawColor Black { get { return new RawColor(0xFF000000); } }
		public static RawColor White { get { return new RawColor(0xFFFFFFFF); } }
		public static RawColor Red { get { return new RawColor(0xFFFF0000); } }
		public static RawColor Green { get { return new RawColor(0xFF00FF00); } }
		public static RawColor Blue { get { return new RawColor(0xFF0000FF); } }
		public static RawColor Yellow { get { return new RawColor(0xFFFFFF00); } }
		public static RawColor Magenta { get { return new RawColor(0xFFFF00FF); } }
		public static RawColor Cyan { get { return new RawColor(0xFF00FFFF); } }
		public static RawColor Gray { get { return new RawColor(0xFF808080); } }
	}
}
