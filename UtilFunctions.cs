using System;

namespace Pictures
{
    static class UtilFunctions
    {
        /*static Vector2 GetPointValue(Vector2 A, Vector2 B, int x)
        {
            // a x + b
            // a = (yb - ya) / (Xb - Xa) #
            // x = - b/a
            // -b = a*x
            // b = -( a* x )  
            float a, b;
            a = (float)((B.y - A.y)/(B.x - A.x));
            b = 0 - (a*x);
            return new Vector2();
                        
        }*/

        /// <summary>
        /// Converts a 2D RGBA-encoded uint Array into an array of bytes that can be read by the Pixel plotting alghorithm
        /// </summary>
        /// <param name="Matrix">matrix to be converted</param>
        /// <returns>array of bytes that can be read by the Pixel plotting alghorithm</returns>
        static byte[] UintMatrixToByteArr(uint[,] Matrix)
        {
            byte[] RetVal = new byte[Matrix.Length*4];
            int rows, cols, index = 0;
            for (cols = 0; cols < Matrix.GetLength(0); cols++)
            {
                for (rows = 0; rows < Matrix.GetLength(1); rows++)
                {
                    (byte R, byte G, byte B, byte A) DecodedPixel = DecodeRGBA(Matrix[rows, cols]);
                    RetVal[index+0] = DecodedPixel.R;
                    RetVal[index+1] = DecodedPixel.G;
                    RetVal[index+2] = DecodedPixel.B;
                    RetVal[index+3] = DecodedPixel.A;
                    index += 4;
                }
            }
            return RetVal;
        }

        /// <summary>
        /// Converts a byte array that can be read by the PixelPlot Algorithm into an uint-encoded RGBA matrix
        /// </summary>
        /// <param name="array">Initial byte array</param>
        /// <param name="height">Number of rows in the final matrix</param>
        /// <param name="width">number of columns in the final matrix</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">provided byte array is not large enough to fill the matrix of desired size</exception>
        static uint[,] ByteArrToRGBAArr(byte[] array, int height, int width)
        {
            if ((array.Length/4)==(width*height))
            {
                uint[,] RetVal = new uint[height, width];
                int cols, rows, index = 0;
                for (cols = 0; cols < height; cols++)
                {
                    for (rows = 0; rows < width; rows++)
                    {
                        RetVal[cols, rows] =
                            EncodeRGBA(array[index], array[index + 1], array[index + 2], array[index + 3]);
                        index += 4;
                    }
                }
                return RetVal;
            }
            else
            {
                throw new ArgumentOutOfRangeException("height, width",
                    "byte array is not large enough to fill the matrix of desired size");
            }
        }
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
