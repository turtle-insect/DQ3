using System;
using System.ComponentModel;

namespace DQ3
{
	class BagItem : INotifyPropertyChanged
	{
		private uint mAddress;
		public event EventHandler ChangeItem;
		public event PropertyChangedEventHandler PropertyChanged;

		public BagItem(uint address)
		{
			mAddress = address;
		}

		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mAddress, 1, value);
				ChangeItem?.Invoke(this, null);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
			}
		}
		public uint Count
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 1, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 1, 1, value, 0, 255);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
			}
		}
	}
}
