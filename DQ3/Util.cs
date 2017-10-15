namespace DQ3
{
	class Util
	{
		public const uint BlockSize = 0x0C00;
		public const uint MaxParty = 23;

		public const uint BagAddress = 0x076F;
		public const uint BagCount = 227;

		public const uint ItemCount = 12;

		public const uint PlaceAddress = 0x0B06;

		public static void WriteNumber(uint address, uint size, uint value, uint min, uint max)
		{
			if (value < min) value = min;
			if (value > max) value = max;
			SaveData.Instance().WriteNumber(address, size, value);
		}
	}
}
