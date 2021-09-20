using System;
using System.Collections.Generic;

namespace Pictures
{
    static class UtilFunctions
    {
        public static List<Point> Getline(Point A, Point B)
        {
            uint deltaX = B.x > A.x ? B.x - A.x : A.x - B.x;
            uint deltaY = B.y > A.y ? B.y - A.y : A.y - B.y;
            uint Slope = deltaY / deltaX;
            float YIntercept = A.y - Slope * A.x;
            
            List<Point> ReturnValue = new();
            return ReturnValue;
        }

        /// <summary>
        /// Converts a 2D RGBA-encoded uint Array into an array of bytes that can be read by the Pixel plotting alghorithm
        /// </summary>
        /// <param name="matrix">matrix to be converted</param>
        /// <returns>array of bytes that can be read by the Pixel plotting alghorithm</returns>
        public static byte[] UintMatrixToByteArray(uint[,] matrix)
        {
            byte[] retVal = new byte[matrix.Length*4];
            int index = 0;
            for ( int cols = 0; cols < matrix.GetLength(0); cols++)
            {
                for (int rows = 0; rows < matrix.GetLength(1); rows++)
                {
                    (byte R, byte G, byte B, byte A) DecodedPixel = DecodeRGBA(matrix[rows, cols]);
                    retVal[index+0] = DecodedPixel.R;
                    retVal[index+1] = DecodedPixel.G;
                    retVal[index+2] = DecodedPixel.B;
                    retVal[index+3] = DecodedPixel.A;
                    index += 4;
                }
            }
            return retVal;
        }
        /// <summary>
        /// Converts a byte array that can be read by the PixelPlot Algorithm into an uint-encoded RGBA matrix
        /// </summary>
        /// <param name="array">Initial byte array</param>
        /// <param name="height">Number of rows in the final matrix</param>
        /// <param name="width">number of columns in the final matrix</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">provided byte array is not large enough to fill the matrix of desired size</exception>
        public static uint[,] ByteArrToRGBAArray(byte[] array, int height, int width)
        {
            if (!(array.Length == width * height * 4))
            {
                throw new ArgumentOutOfRangeException("height, width",
                    "byte array is not large enough to fill the matrix of desired size");
            }

            uint[,] retVal = new uint[height, width];
            int index = 0;
            for (int cols = 0; cols < height; cols++)
            {
                for (int rows = 0; rows < width; rows++)
                {
                    retVal[cols, rows] =
                        EncodeRGBA(array[index], array[index + 1], array[index + 2], array[index + 3]);
                    index += 4;
                }
            }
            return retVal;
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
