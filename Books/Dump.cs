// Dump

using System;
using System.IO;
using System.Linq;

namespace NfcSample
{
	// Dump class
	public class Dump
	{
		public const string FILENAME_FORMAT = 
            "%04d-%02d-%02d_%02d%02d%02d_%d_%dRUB.txt";
		public const string FILENAME_REGEXP = 
            "([0-9]{4})-([0-9]{2})-([0-9]{2})_([0-9]{6})_([0-9]+)_([0-9]+)RUB.txt";

		public const int BLOCK_COUNT = 4;

        public static readonly int BLOCK_SIZE = 16;//MifareClassic.BLOCK_SIZE;

        public const int SECTOR_INDEX = 8;

		
		public static readonly sbyte[] KEY_A = new sbyte[] 
        {
            unchecked((sbyte) 0xA7),
            (sbyte) 0x3F,
            (sbyte) 0x5D,
            unchecked((sbyte) 0xC1),
            unchecked((sbyte) 0xD3),
            (sbyte) 0x33
        };

        public static readonly sbyte[] KEY_B = new sbyte[]
        {
            unchecked((sbyte) 0xE3),
            (sbyte) 0x51,
            (sbyte) 0x73,
            (sbyte) 0x49,
            (sbyte) 0x4A,
            unchecked((sbyte) 0x81)
        };

        public static readonly sbyte[] KEY_0 = new sbyte[] 
        {
            (sbyte) 0x00,
            (sbyte) 0x00,
            (sbyte) 0x00,
            (sbyte) 0x00,
            (sbyte) 0x00,
            (sbyte) 0x00
        };

		// raw
		protected internal sbyte[] uid;
		protected internal sbyte[][] data;

		// parsed
		protected internal int cardNumber;
		protected internal int balance;
		protected internal DateTime lastUsageDate;
		protected internal int lastValidatorId;

		public Dump(sbyte[] uid, sbyte[][] sector8)
		{
			this.uid = uid;
			this.data = sector8;
			parse();
		}

        //public static Dump fromTag(android.nfc.Tag tag) throws java.io.IOException
		public static Dump fromTag(Tag tag)
		{
			MifareClassic mfc = getMifareClassic(tag);

			int blockCount = mfc.getBlockCountInSector(SECTOR_INDEX);

			if (blockCount < BLOCK_COUNT)
			{
				throw new IOException("Wtf? Not enough blocks on this card");
			}

            //he following call to the 'RectangularArrays' helper class reproduces 
            // the rectangular array initialization that is automatic in Java:
            //sbyte[][] data = new sbyte[BLOCK_COUNT][BLOCK_SIZE];
			sbyte[][] data = RectangularArrays.RectangularSbyteArray(BLOCK_COUNT, BLOCK_SIZE);

			for (int i = 0; i < BLOCK_COUNT; i++)
			{
				data[i] = mfc.readBlock(mfc.sectorToBlock(SECTOR_INDEX) + i);
			}

			return new Dump(tag.Id, data);
		}

        //
		public static Dump fromFile(string file)//(File file)
		{
			FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);

			Scanner scanner = new Scanner(fs, "US-ASCII");

            sbyte[] uid = HexUtils.fromString(scanner.nextLine());


			sbyte[][] data = RectangularArrays.RectangularSbyteArray(BLOCK_COUNT, BLOCK_SIZE);

            for (int i = 0; i < BLOCK_COUNT; i++)
			{
				data[i] = HexUtils.fromString(scanner.nextLine());
			}

			return new Dump(uid, data);
		}

        // getMifareClassic
        static MifareClassic getMifareClassic(Tag tag)
		{
			MifareClassic mfc = MifareClassic.get(tag);

            mfc.connect();

			// fucked up card
			if (mfc.authenticateSectorWithKeyA(SECTOR_INDEX, KEY_0) 
				&& mfc.authenticateSectorWithKeyB(SECTOR_INDEX, KEY_0))
			{
				return mfc;
			}

			// good card
			if (mfc.authenticateSectorWithKeyA(SECTOR_INDEX, KEY_A) 
				&& mfc.authenticateSectorWithKeyB(SECTOR_INDEX, KEY_B))
			{
				return mfc;
			}

			throw new IOException("No permissions");

        }//getMifareClassic

        // Parse data
        protected internal virtual void parse()
		{
			// block#0 bytes#3-6
			cardNumber = intval(data[0][3], data[0][4], data[0][5], data[0][6]) >> 4;

			// incorrect
			//TODO: find correct field for validator ID
			lastValidatorId = intval(data[1][0], data[1][1]);

			int minutesDelta = intval((sbyte)(data[1][0]), (sbyte)(data[1][1]), (sbyte)(data[1][2])) >> 1;

            if (minutesDelta > 0)
			{
                DateTime c = DateTime.Now;//getInstance(TimeZone.getTimeZone("GMT+3"));
				c = new DateTime(2018, 11, 31, 0, 0, 0);
				c.AddMinutes(minutesDelta);
				lastUsageDate = c;
			}
			else
			{
                lastUsageDate = DateTime.Now;//null;
            }

            // calculate balance
			balance = intval((sbyte)(data[1][5]), (sbyte) data[1][6]) / 25;
		}

        // Write data
		public virtual void write(Tag tag)
		{
			MifareClassic mfc = getMifareClassic(tag);

			if (!tag.Id.SequenceEqual(this.Uid))
			{
				throw new IOException("Card UID mismatch: \n" + HexUtils.toString(tag.Id) 
					+ " (card) != " + HexUtils.toString(Uid) + " (dump)");
			}

			int numBlocksToWrite = BLOCK_COUNT - 1; // do not overwrite last block (keys)

            int startBlockIndex = mfc.sectorToBlock(SECTOR_INDEX);

			for (int i = 0; i < numBlocksToWrite; i++)
			{
				mfc.writeBlock(startBlockIndex + i, data[i]);
			}
		}

        // save
		public virtual string save(string dir)
		{
            /*
			string state = Environment.ExternalStorageState;

			if (!Environment.MEDIA_MOUNTED.Equals(state))
			{
				throw new IOException("Can not write to external storage");
			}
            */

            /*
			if (!dir.Directory)
			{
				throw new IOException("Not a dir");
			}

			if (!dir.exists() && !dir.mkdirs())
			{
				throw new IOException("Can not make save dir");
			}

			string file = new File(dir, makeFilename());

			FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write);

			StreamWriter @out = new StreamWriter(stream);

			@out.Write(UidAsString + "\r\n");

            foreach (string block in DataAsStrings)
			{
				@out.Write(block + "\r\n");
			}
            @out.Flush();//Close();

			return file;
            */
            return "filename"; // TEMP
		}

        //
		protected internal virtual string makeFilename()
		{
			DateTime now = DateTime.Now;
			return string.Format(FILENAME_FORMAT, now.Year + 1900, now.Month + 1, 
				now.Date, now.Hour, now.Minute, now.Second, CardNumber, Balance);
		}

        //
		public virtual sbyte[] Uid
		{
			get
			{
				return uid;
			}
		}

        //
		public virtual string UidAsString
		{
			get
			{
				return HexUtils.toString(Uid);
			}
		}

        //
		public virtual sbyte[][] Data
		{
			get
			{
				return data;
			}
		}

        //
		public virtual string[] DataAsStrings
		{
			get
			{
				string[] blocks = new string[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					blocks[i] = HexUtils.toString(data[i]);
				}
				return blocks;
			}
		}

        //
		public virtual DateTime LastUsageDate
		{
			get
			{
				return lastUsageDate;
			}
		}

        //
		public virtual string LastUsageDateAsString
		{
			get
			{
				if (lastUsageDate == null)
				{
					return "<NEVER USED>";
				}
                
				// TODO
				return lastUsageDate.ToString();
				//DateFormat.getDateTimeInstance(DateFormat.MEDIUM, DateFormat.SHORT).format(lastUsageDate);
			}
		}

        //
		public virtual int LastValidatorId
		{
			get
			{
				return lastValidatorId;
			}
		}

        //
		public virtual string LastValidatorIdAsString
		{
			get
			{
				return "ID# " + LastValidatorId;
			}
		}

        //
		public virtual int Balance
		{
			get
			{
				return balance;
			}
		}

        //
		public virtual string BalanceAsString
		{
			get
			{
				return "" + Balance + " RUB";
			}
		}

        //
		public virtual int CardNumber
		{
			get
			{
				return cardNumber;
			}
		}

        //
		public virtual string CardNumberAsString
		{
			get
			{
				return formatCardNumber(cardNumber);
			}
		}

        //
		public static string formatCardNumber(int cardNumber)
		{
			int cardNum3 = cardNumber % 1000;

			int cardNum2 = (int) Math.Floor((double)(cardNumber / 1000) % 1000);

			int cardNum1 = (int) Math.Floor((double)(cardNumber / 1000000) % 1000);

			return string.Format("{0:D4} {1:D3} {2:D3}", cardNum1, cardNum2, cardNum3);
		}

        //
		public override string ToString()
		{
			return "[Card UID=" + UidAsString + " " + BalanceAsString + "RUR]";
		}

        //
		protected internal static int intval(params sbyte[] bytes)
		{
			int value = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				int x = (int) bytes[bytes.Length - i - 1];
				while (x < 0)
				{
					x = 256 + x;
				}
				value += (int)(x * Math.Pow(0x100, i));
			}
			return value;
		}

	}//Dump class end

}//namespace end