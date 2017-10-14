using System;
using System.Collections.ObjectModel;

namespace DQ3
{
	class Bag
	{
		public ObservableCollection<BagItem> Items { get; set; } = new ObservableCollection<BagItem>();

		public Bag()
		{
			Load();
		}

		private void Load()
		{
			Items.Clear();
			SaveData saveData = SaveData.Instance();
			for (uint i = 0; i < Util.BagCount; i++)
			{
				BagItem item = new BagItem(Util.BagAddress + i * 2);
				item.ChangeItem += Item_ChangeItem;
				Items.Add(item);
			}
		}

		private void Item_ChangeItem(object sender, EventArgs e)
		{
			BagItem target = sender as BagItem;
			if (target == null) return;
			if (target.ID != 0) return;

			for (uint i = 0; i < Util.BagCount; i++)
			{
				if (Items[(int)i] == target)
				{
					SaveData saveData = SaveData.Instance();
					for (uint j = i; j < Util.BagCount - 1; j++)
					{
						saveData.Copy(Util.BagAddress + j + 1, Util.BagAddress + j, 1);
					}
					Load();
					break;
				}
			}
		}
	}
}
