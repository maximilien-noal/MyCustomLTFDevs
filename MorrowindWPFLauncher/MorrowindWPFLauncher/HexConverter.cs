using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace MorrowindWPFLauncher
{
    internal static class HexConverter
    {
        public static byte[] GetStringToBytes(string value)
        {
            SoapHexBinary shb = SoapHexBinary.Parse(value);
            return shb.Value;
        }

        public static string GetBytesToString(byte[] value)
        {
            SoapHexBinary shb = new SoapHexBinary(value);
            return shb.ToString();
        }
    }
}
