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

			CreateItem();
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

		private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			CharactorItem item = sender as CharactorItem;
			if (item == null) return;
			SaveData saveData = SaveData.Instance();
			uint count = 0;
			for (uint i = 0; i < Util.ItemCount - 1; i++)
			{
				uint address = mAddress + 0x45 + i;
				uint id = saveData.ReadNumber(address, 1);
				if(id == 0x00)
				{
					saveData.Swap(address, address + 1, 1);
					id = saveData.ReadNumber(address, 1);
				}
				if (id != 0x00) count++;
			}
			if (saveData.ReadNumber(mAddress + 0x45 + Util.ItemCount - 1, 1) != 0x00) count++;
			Util.WriteNumber(mAddress + 0x44, 1, count, 0, 12);
			CreateItem();
		}

		private void CreateItem()
		{
			Items.Clear();
			for (uint i = 0; i < Util.ItemCount; i++)
			{
				CharactorItem item = new CharactorItem(mAddress + 0x45 + i);
				item.PropertyChanged += Item_PropertyChanged;
				Items.Add(item);
			}
		}
	}
}
