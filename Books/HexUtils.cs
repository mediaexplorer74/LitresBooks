// HexUtils

using System;

namespace NfcSample // cc.troikadumper.utils
{
	public class HexUtils
	{

		private static readonly sbyte[] HEX_CHAR_TABLE = new sbyte[] {(sbyte) '0', (sbyte) '1', (sbyte) '2', (sbyte) '3', (sbyte) '4', (sbyte) '5', (sbyte) '6', (sbyte) '7', (sbyte) '8', (sbyte) '9', (sbyte) 'A', (sbyte) 'B', (sbyte) 'C', (sbyte) 'D', (sbyte) 'E', (sbyte) 'F'};

		public static string toString(sbyte[] raw)
		{
			int len = raw.Length;
			sbyte[] hex = new sbyte[2 * len];
			int index = 0;
			int pos = 0;

			foreach (sbyte b in raw)
			{
				if (pos >= len)
				{
					break;
				}

				pos++;
				int v = b & 0xFF;
				hex[index++] = HEX_CHAR_TABLE[(int)((uint)v >> 4)];
				hex[index++] = HEX_CHAR_TABLE[v & 0xF];
			}

			return StringHelper.NewString(hex);
		}

		public static sbyte[] fromString(string hex)
		{
			int len = hex.Length;
			if (len % 2 == 1)
			{
				throw new System.ArgumentException("hex length is not even");
			}
			len = len / 2; // actual

			sbyte[] bytes = new sbyte[len];
			for (int i = 0; i < len; i++)
			{
				bytes[i] = unchecked((sbyte)(Convert.ToInt32(hex.Substring(i * 2, 2), 16) & 0xFF));
			}
			return bytes;
		}

	}

}