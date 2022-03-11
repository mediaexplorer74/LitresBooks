// MifireClassic

using System;

namespace NfcSample
{
    internal class MifareClassic
    {
        internal sbyte[] readBlock(object p)
        {
            throw new NotImplementedException();
        }

        internal int sectorToBlock(int sECTOR_INDEX)
        {
            throw new NotImplementedException();
        }

        internal int getBlockCountInSector(int sECTOR_INDEX)
        {
            throw new NotImplementedException();
        }

        internal static MifareClassic get(Tag tag)
        {
            throw new NotImplementedException();
        }

        internal void connect()
        {
            throw new NotImplementedException();
        }

        internal bool authenticateSectorWithKeyA(int sECTOR_INDEX, sbyte[] kEY_0)
        {
            throw new NotImplementedException();
        }

        internal bool authenticateSectorWithKeyB(int sECTOR_INDEX, sbyte[] kEY_0)
        {
            throw new NotImplementedException();
        }

        internal void writeBlock(int v1, sbyte[] v2)
        {
            throw new NotImplementedException();
        }
    }
}