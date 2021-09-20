using System;
using System.Drawing;

namespace Pictures
{
    class Program
    {
        const string PicFolderPath = @"C:\Users\Admin\Desktop\Generated pictures\";
        const int BytesPerPixel = 4;
        const int Height = 3840;
        const int Width = 2160;
        const byte DefNoTransparency = 0xFF;
        /*
         creates an array of bytes. each pixel has 4 bytes containing Red, Green, Blue and Alpha (Transparency) values
        image will be 256px X 256 px
         */
        static byte[] _ByteBuffer = new byte[Width * Height * BytesPerPixel];   // = 102400 bytes
        // 2d RGBA pixel array to make working with stuff less of a pain in the ass
        static uint[,] _ImageBuffer = new uint[Height,Width];

        static void Main(string[] args)
        {         
            // Cant be bothered to make a console interface bc fuck you
            CreateGradient(GradientType.Fancy);
        }
        static void CreateGradient(GradientType type)
        {
            string FileName = string.Empty;
            // Loops over the Image buffer, top-to-bottom and left-to right, plots pixel according to the selected Gradient type
            for (int y = 0; y < Height; y++)
            {
                byte YVal = (byte)y;
                for (int x = 0; x < Width; x++)
                {
                    UInt32 RGBAVal = new UInt32();
                    byte XVal = (byte)x;
                    byte AvgVal = (byte)((XVal + YVal) / 2);
                    switch (type)
                    {
                        case GradientType.Regular:
                            FileName = "ChungoidGradient";
                            RGBAVal = UtilFunctions.EncodeRGBA(XVal, XVal, XVal, DefNoTransparency);
                            PlotPixel(x, y, RGBAVal);
                            break;
                        case GradientType.Fancy:
                            FileName = "FancyGradient";
                            RGBAVal = UtilFunctions.EncodeRGBA(XVal, XVal, XVal, XVal);
                            PlotPixel(x, y, RGBAVal);
                            break;
                        case GradientType.Fancier:
                            FileName = "EvenFancierGradient";
                            RGBAVal = UtilFunctions.EncodeRGBA(AvgVal, AvgVal, AvgVal, DefNoTransparency);
                            PlotPixel(x, y, RGBAVal);
                            break;
                        default:
                            break;
                    }
                }
            }
            _ByteBuffer = UtilFunctions.UintMatrixToByteArray(_ImageBuffer);
            unsafe
            {
                fixed (byte* ptr = _ByteBuffer)
                {
                    using (Bitmap image = new Bitmap(Width, Height, Width * BytesPerPixel,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb, new IntPtr(ptr)))
                    {
                        image.Save(PicFolderPath + FileName + ".jpg");
                    }
                }
            }
        }
        static void PlotPixel(int X, int Y, /*byte Rval, byte GVal, byte Bval,*/ UInt32 RGBA)
        {
            _ImageBuffer[X, Y] = RGBA;
            Console.WriteLine($"x: {X}, y:{Y}");
        }
        enum GradientType
        {
            Regular,
            Fancy,
            Fancier
        }
    }
}
