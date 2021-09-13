using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures
{
    static class UtilFunctions
    {
        public static UInt32 EncodeRGBA(byte R, byte G, byte B, byte A)
        {
            return (uint)((R << 24) | (G << 16) | (B << 8) | A);
        }
        public static (byte R, byte G, byte B, byte A) DecodeRGBA(UInt32 RGBA)
        {
            byte RVal = unchecked((byte)(RGBA >> 24));
            byte GVal = unchecked((byte)(RGBA >> 16));
            byte BVal = unchecked((byte)(RGBA >> 8));
            byte AVal = unchecked((byte)RGBA);
            return (RVal, GVal, BVal, AVal);
        }
    }
}
