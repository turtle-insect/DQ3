using System;
using System.ComponentModel;

namespace DQ3
{
	class Skill : INotifyPropertyChanged
	{
		private readonly uint mAddress;
		private readonly uint mID;

		public Skill(uint address, uint id)
		{
			mAddress = address;
			mID = id;
		}

		public String Name { get; set; }
		public bool Leam
		{
			get
			{
				return SaveData.Instance().ReadBit(mAddress + mID / 8, mID % 8);
			}

			set
			{
				SaveData.Instance().WriteBit(mAddress + mID / 8, mID % 8, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Leam"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
