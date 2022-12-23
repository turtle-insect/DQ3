﻿using System;
using System.Collections.ObjectModel;

namespace DQ3
{
	class DataContext
	{
		public Info Info { get; set; } = Info.Instance();
		public ObservableCollection<Charactor> Party { get; set; } = new ObservableCollection<Charactor>();
		public ObservableCollection<Place> Places { get; set; } = new ObservableCollection<Place>();
		public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
		public Bag Bag { get; set; } = new Bag();

		public DataContext()
		{
			SaveData saveData = SaveData.Instance();
			uint address = 0x0004;
			for (uint i = 0; i < Util.MaxParty; i++)
			{
				if (saveData.ReadNumber(address, 2) != 0)
				{
					Party.Add(new Charactor(address));
				}
				address += 0x51;

				Orders.Add(new Order(0x074B + i));
			}

			foreach (var place in Info.Instance().Places)
			{
				Places.Add(new Place(place.Value) { Name = place.Name });
			}
		}

		public uint PartyCount
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0AAA, 1);
			}

			set
			{
				Util.WriteNumber(0x0AAA, 1, value, 0, Util.MaxParty);
			}
		}

		public uint HandGold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0AAE, 3);
			}

			set
			{
				Util.WriteNumber(0x0AAE, 3, value, 0, 9999999);
			}
		}

		public uint BankGold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0AB1, 2);
			}

			set
			{
				Util.WriteNumber(0x0AB1, 2, value, 0, 0xFFFF);
			}
		}

		public bool Ship
		{
			get
			{
				return SaveData.Instance().ReadBit(0x098B, 2);
			}

			set
			{
				SaveData.Instance().WriteBit(0x098B, 2, value);
			}
		}

		public bool Lamia
		{
			get
			{
				return SaveData.Instance().ReadBit(0x098B, 3);
			}

			set
			{
				SaveData.Instance().WriteBit(0x098B, 3, value);
			}
		}

		public bool Baramos
		{
			get
			{
				return SaveData.Instance().ReadBit(0x0992, 2);
			}

			set
			{
				SaveData.Instance().WriteBit(0x0992, 2, value);
			}
		}

		public bool Zoma
		{
			get
			{
				return SaveData.Instance().ReadBit(0x0993, 2);
			}

			set
			{
				SaveData.Instance().WriteBit(0x0993, 2, value);
			}
		}

		public String PlayerName
		{
			get
			{
				return SaveData.Instance().ReadUTF8(0x0AD6, 12);
			}
			set
			{
				SaveData.Instance().WriteUTF8(0x0AD6, 12, value);
			}
		}

		public uint BirthDay
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0AB4, 1);
			}

			set
			{
				Util.WriteNumber(0x0AB4, 1, value, 1, 31);
			}
		}

		public uint BirthMonth
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0AB5, 1);
			}

			set
			{
				Util.WriteNumber(0x0AB5, 1, value, 1, 12);
			}
		}
	}
}
