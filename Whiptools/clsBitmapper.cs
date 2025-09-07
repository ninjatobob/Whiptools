using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Whiptools
{
    class Bitmapper
    {
        public static Color[] ConvertRGBToPalette(byte[] inputArray)
        {
            Color[] output = new Color[inputArray.Length / 3];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Color.FromArgb(
                    inputArray[i * 3],
                    inputArray[i * 3 + 1],
                    inputArray[i * 3 + 2]);
            }
            return output;
        }

        public static Bitmap ConvertPaletteToBitmap(Color[] palette)
        {
            using (var bitmap = new Bitmap(palette.Length, 1, PixelFormat.Format24bppRgb))
            {
                for (int i = 0; i < palette.Length; i++)
                    bitmap.SetPixel(i, 0, getColorHigh(palette[i]));
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
            using (var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                int stride = bitmapData.Stride;
                IntPtr ptr = bitmapData.Scan0;
                byte[] rgbArrayHigh = getByteArrayHigh(rgbArray);
                for (int y = 0; y < height; y++)
                    Marshal.Copy(rgbArrayHigh, y * width * 3, ptr + y * stride, width * 3);
                bitmap.UnlockBits(bitmapData);
                return new Bitmap(bitmap);
            }
        }

        // bitmap creator

        public static Color[] GetPaletteFromBitmap(Bitmap bitmap)
        {
            var hashColors = new HashSet<Color>();
            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                    hashColors.Add(getColorLow(bitmap.GetPixel(x, y)));
            Color[] output = new Color[hashColors.Count];
            hashColors.CopyTo(output);
            return output;
        }

        public static byte[] GetPaletteArray(Color[] palette)
        {
            byte[] output = new byte[palette.Length * 3];
            for (int i = 0; i < palette.Length; i++)
            {
                output[i * 3] = (byte)(palette[i].R);
                output[i * 3 + 1] = (byte)(palette[i].G);
                output[i * 3 + 2] = (byte)(palette[i].B);
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
                    Color colorLow = getColorLow(bitmap.GetPixel(x, y));
                    bool found = false;
                    for (int i = 0; i < palette.Length; i++)
                    {
                        if (palette[i] == colorLow)
                        {
                            output[y * width + x] = (byte)i;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        return Array.Empty<byte>();
                }
            }
            return output;
        }

        // utils

        private static byte[] getByteArrayHigh(byte[] input)
        {
            byte[] output = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = (byte)getIntHigh(Convert.ToInt32(input[i]));
            return output;
        }

        private static Color getColorHigh(Color input)
        {
            return Color.FromArgb(
                getIntHigh(input.R),
                getIntHigh(input.G),
                getIntHigh(input.B));
        }
        private static Color getColorLow(Color input)
        {
            return Color.FromArgb(
                getIntLow(input.R),
                getIntLow(input.G),
                getIntLow(input.B));
        }

        private static int getIntHigh(int input)
        {
            return (input << 2) + (input >> 4);
        }

        private static int getIntLow(int input)
        {
            return (input >> 2);
        }
    }
}