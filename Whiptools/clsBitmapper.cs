using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Whiptools
{
    class Bitmapper
    {
        public static Color[] ConvertByteToPalette(byte[] inputArray)
        {
            Color[] output = new Color[inputArray.Length / 3];
            for (int i = 0; i < output.Length; i++)
            {
                int R = Convert6BitTo8Bit(inputArray[i * 3]);
                int G = Convert6BitTo8Bit(inputArray[i * 3 + 1]);
                int B = Convert6BitTo8Bit(inputArray[i * 3 + 2]);
                output[i] = Color.FromArgb(R, G, B);
            }
            return output;
        }

        public static Bitmap ConvertPaletteToBitmap(Color[] palette)
        {
            using (Bitmap bitmap = new Bitmap(palette.Length, 1, PixelFormat.Format24bppRgb))
            {
                for (int i = 0; i < palette.Length; i++)
                {
                    bitmap.SetPixel(i, 0, palette[i]);
                }
                return new Bitmap(bitmap);
            }
        }

        public static byte[] CreateRGBArray(byte[] bitmapArray, Color[] palette)
        {
            byte[] output = new byte[bitmapArray.Length * 3];
            for (int i = 0; i < bitmapArray.Length; i++)
            {
                output[i * 3] = palette[bitmapArray[i]].B;
                output[i * 3 + 1] = palette[bitmapArray[i]].G;
                output[i * 3 + 2] = palette[bitmapArray[i]].R;
            }
            return output;
        }

        public static Bitmap CreateBitmapFromRGB(int width, int height, byte[] rgbArray)
        {
            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                Marshal.Copy(rgbArray, 0, bitmapData.Scan0, rgbArray.Length);
                bitmap.UnlockBits(bitmapData);
                return new Bitmap(bitmap);
            }
        }

        public static Bitmap ConvertBitmapTo6Bit(Bitmap inputBitmap)
        {
            int width = inputBitmap.Width;
            int height = inputBitmap.Height;
            using (Bitmap outputBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                for (int y = 0; y < inputBitmap.Height; y++)
                {
                    for (int x = 0; x < inputBitmap.Width; x++)
                    {
                        Color pixel = inputBitmap.GetPixel(x, y);
                        outputBitmap.SetPixel(x, y, Color.FromArgb(pixel.R & 0xFC, pixel.G & 0xFC, pixel.B & 0xFC));
                    }
                }
                return new Bitmap(outputBitmap);
            }
        }

        public static Color[] GetPaletteFromBitmap(Bitmap bitmap)
        {
            HashSet<Color> hashColors = new HashSet<Color>();
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    hashColors.Add(pixel);
                }
            }
            Color[] output = new Color[hashColors.Count];
            hashColors.CopyTo(output);
            return output;
        }

        public static byte[] GetPaletteArray(Color[] palette)
        {
            byte[] output = new byte[palette.Length * 3];
            for (int i = 0; i < palette.Length; i++)
            {
                output[i * 3] = (byte)(palette[i].R >> 2);
                output[i * 3 + 1] = (byte)(palette[i].G >> 2);
                output[i * 3 + 2] = (byte)(palette[i].B >> 2);
            }
            return output;
        }

        public static byte[] GetBitmapArray(Bitmap bitmap, Color[] palette)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            byte[] output = new byte[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    for (int i = 0; i < palette.Length; i++)
                    {
                        if (palette[i] == pixel)
                        {
                            output[y * width + x] = (byte)i;
                            break;
                        }
                    }
                }
            }
            return output;
        }

        // utils

        private static int Convert6BitTo8Bit(int input)
        {
            return (input << 2) + (input >> 4);
        }
    }
}