using System.Drawing;
using System.Drawing.Imaging;

namespace Whiptools
{
    class clsBitmapper
    {
        public static byte[] CreateRGB(byte[] rawData, byte[] palette)
        {
            int palSize = palette.Length / 3;
            byte[] R = new byte[palSize];
            byte[] G = new byte[palSize];
            byte[] B = new byte[palSize];
            for (int i = 0; i < palSize; i++)
            {
                R[i] = (byte)(palette[i * 3] * 4);
                G[i] = (byte)(palette[i * 3 + 1] * 4);
                B[i] = (byte)(palette[i * 3 + 2] * 4);
            }

            byte[] output = new byte[rawData.Length * 3];

            for (int i = 0; i < rawData.Length; i++)
            {
                byte c = rawData[i];
                output[i * 3] = B[c];
                output[i * 3 + 1] = G[c];
                output[i * 3 + 2] = R[c];
            }
            return output;
        }

        public static Bitmap CreateBitmap(int width, int height, byte[] rgbData)
        {
            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                System.Runtime.InteropServices.Marshal.Copy(rgbData, 0, bitmapData.Scan0, rgbData.Length);
                bitmap.UnlockBits(bitmapData);
                return new Bitmap(bitmap);
            }
        }
    }
}
