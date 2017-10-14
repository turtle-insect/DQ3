using System;
using System.Collections.ObjectModel;

namespace DQ3
{
	class Charactor
	{
		public ObservableCollection<CharactorItem> Items { get; set; } = new ObservableCollection<CharactorItem>();
		public ObservableCollection<Skill> Skills { get; set; } = new ObservableCollection<Skill>();

		private readonly uint mAddress;

		public Charactor(uint address)
		{
			mAddress = address;

			for (uint i = 0; i < Util.ItemCount; i++)
			{
				Items.Add(new CharactorItem(address + 0x45 + i));
			}

			foreach (var skill in Info.Instance().Skills)
			{
				Skills.Add(new Skill(address + 0x39, skill.Value) { Name = skill.Name });
			}
		}

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x01, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x01, 1, value, 1, 99);
			}
		}

		public uint Exp
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x02, 3);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x02, 3, value, 0, 9999999);
			}
		}

		public uint Job
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x38, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress + 0x38, 1, value);
			}
		}

		public uint Personality
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x37, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress + 0x37, 1, value);
			}
		}

		public String Name
		{
			get
			{
				return SaveData.Instance().ReadUTF8(mAddress + 0x17, 4);
			}

			set
			{
				SaveData.Instance().WriteUTF8(mAddress + 0x17, 4, value);
			}
		}

		public uint Sex
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x00, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress + 0x00, 1, value);
			}
		}

		public uint MaxHP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x05, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x05, 2, value, 0, 999);
			}
		}

		public uint HP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x07, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x07, 2, value, 0, 999);
			}
		}

		public uint MaxMP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x09, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x09, 2, value, 0, 999);
			}
		}

		public uint MP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0B, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0B, 2, value, 0, 999);
			}
		}


		public uint Power
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0E, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0E, 1, value, 0, 255);
			}
		}

		public uint Speed
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x10, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x10, 1, value, 0, 255);
			}
		}

		public uint Strength
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x12, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x12, 1, value, 0, 255);
			}
		}

		public uint Intelligence
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x14, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x14, 1, value, 0, 255);
			}
		}

		public uint Luck
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x16, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x16, 1, value, 0, 255);
			}
		}

		public uint ItemEquipment
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x43, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x43, 1, value, 0, 12);
			}
		}

		public uint ItemCount
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x44, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x44, 1, value, 0, 12);
			}
		}
	}
}
