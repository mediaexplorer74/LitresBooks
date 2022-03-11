//

using System;
using System.IO;

namespace NfcSample
{
    internal class Scanner
    {
        private FileStream fs;
        private string v;

        public Scanner(FileStream fs, string v)
        {
            this.fs = fs;
            this.v = v;
        }

        internal string nextLine()
        {
            throw new NotImplementedException();
        }
    }
}