using System;

namespace DQ3
{
	class Place
	{
		private readonly uint mID;

		public Place(uint id)
		{
			mID = id;
		}

		public String Name { get; set; }
		public bool Leam
		{
			get
			{
				return SaveData.Instance().ReadBit(Util.PlaceAddress + mID / 8, mID % 8);
			}

			set
			{
				SaveData.Instance().WriteBit(Util.PlaceAddress + mID / 8, mID % 8, value);
			}
		}
	}
}
