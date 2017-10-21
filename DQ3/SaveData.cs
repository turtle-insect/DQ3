using System;

namespace DQ3
{
	class SaveData
	{
		private static SaveData mThis;
		private String mFileName = null;
		private Byte[] mBuffer = null;
		private uint mAdventure = 0;
		public uint Adventure
		{
			private get
			{
				return mAdventure;
			}

			set
			{
				WriteNumber(0x0002, 2, CheckSum());
				mAdventure = value;
			}
		}

		private SaveData()
		{}

		public static SaveData Instance()
		{
			if (mThis == null) mThis = new SaveData();
			return mThis;
		}

		public bool Open(String filename, bool force)
		{
			mFileName = filename;
			mBuffer = System.IO.File.ReadAllBytes(mFileName);

			if(force || CheckSum() == ReadNumber(0x0002, 2))
			{
				Backup();
				return true;
			}

			mFileName = null;
			mBuffer = null;
			return false;
		}

		public bool Save()
		{
			if (mFileName == null || mBuffer == null) return false;
			WriteNumber(0x0002, 2, CheckSum());
			System.IO.File.WriteAllBytes(mFileName, mBuffer);
			return true;
		}

		public bool SaveAs(String filenname)
		{
			if (mBuffer == null) return false;
			mFileName = filenname;
			return Save();
		}

		public uint ReadNumber(uint address, uint size)
		{
			if (mBuffer == null) return 0;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return 0;
			uint result = 0;
			for(int i = 0; i < size; i++)
			{
				result += (uint)(mBuffer[address + i]) << (i * 8);
			}
			return result;
		}

		// 0 to 7.
		public bool ReadBit(uint address, uint bit)
		{
			if (bit < 0) return false;
			if (bit > 7) return false;
			if (mBuffer == null) return false;
			address = CalcAddress(address);
			if (address > mBuffer.Length) return false;
			Byte mask = (Byte)(1 << (int)bit);
			Byte result = (Byte)(mBuffer[address] & mask);
			return result != 0;
		}

		public String ReadUTF8(uint address, uint len)
		{
			uint size = len * 4;
			if (mBuffer == null) return "";
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return "";

			Byte[] tmp = new Byte[size];
			uint count = 0;
			for(uint i = 0; i < size; i++)
			{
				if (mBuffer[address + i] == 0) continue;
				tmp[count] = mBuffer[address + i];
				count++;
			}
			return System.Text.Encoding.UTF8.GetString(tmp).Trim('\0');
		}

		public void WriteNumber(uint address, uint size, uint value)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				mBuffer[address + i] = (Byte)(value & 0xFF);
				value >>= 8;
			}
		}

		// 0 to 7.
		public void WriteBit(uint address, uint bit, bool value)
		{
			if (bit < 0) return;
			if (bit > 7) return;
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address > mBuffer.Length) return;
			Byte mask = (Byte)(1 << (int)bit);
			if (value) mBuffer[address] = (Byte)(mBuffer[address] | mask);
			else mBuffer[address] = (Byte)(mBuffer[address] & ~mask);
		}

		public void WriteUTF8(uint address, uint len, String value)
		{
			uint size = len * 3;
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + size + len > mBuffer.Length) return;
			Byte[] tmp = System.Text.Encoding.UTF8.GetBytes(value);
			Array.Resize(ref tmp, (int)size);
			uint count = 0;
			for (uint i = 0; i < size; i++)
			{
				mBuffer[address + count] = tmp[i];
				count++;
				if ((i + 1) % 3 == 0)
				{
					mBuffer[address + count] = 0x00;
					count++;
				}
			}
		}

		public void Fill(uint address, uint size, Byte number)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				mBuffer[address + i] = number;
			}
		}

		public void Copy(uint from, uint to, uint size)
		{
			if (mBuffer == null) return;
			from = CalcAddress(from);
			to = CalcAddress(to);
			if (from + size > mBuffer.Length) return;
			if (to + size > mBuffer.Length) return;
			for(uint i = 0; i < size; i++)
			{
				mBuffer[to + i] = mBuffer[from + i];
			}
		}

		public void Swap(uint from, uint to, uint size)
		{
			if (mBuffer == null) return;
			from = CalcAddress(from);
			to = CalcAddress(to);
			if (from + size > mBuffer.Length) return;
			if (to + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				Byte tmp = mBuffer[to + i];
				mBuffer[to + i] = mBuffer[from + i];
				mBuffer[from + i] = tmp;
			}
		}

		private uint CheckSum()
		{
			uint result = 0;
			for (uint i = 0x0004; i < Util.BlockSize; i+=2)
			{
				result += ReadNumber(i, 2);
			}
			return result & 0xFFFF;
		}

		private uint CalcAddress(uint address)
		{
			return address + Util.BlockSize * Adventure + 0x100;
		}

		private void Backup()
		{
			DateTime now = DateTime.Now;
			String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			path = System.IO.Path.Combine(path, "backup");
			if(!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			path = System.IO.Path.Combine(path, 
				String.Format("{0:0000}-{1:00}-{2:00} {3:00}-{4:00}", now.Year, now.Month, now.Day, now.Hour, now.Minute));
			System.IO.File.WriteAllBytes(path, mBuffer);
		}
	}
}
