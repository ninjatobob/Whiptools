using System;
using System.IO;
using System.Diagnostics;

namespace Whiptools
{
    class clsUnmangler
    {
        public static byte[] Unmangle(byte[] inputData)
        {
            int outputLength = BitConverter.ToInt32(inputData, 0); // output length is first 4 bytes of input
            byte[] outputData = new byte[outputLength];

            // start positions
            int inputPos = 4;
            int outputPos = 0;

            while ((inputPos < inputData.Length) && (outputPos < outputLength))
            {
                int byteValue = Convert.ToInt32(inputData[inputPos]);

                if (byteValue <= 0x3F) // 0x00 to 0x3F: read bytes from input
                {
                    byte[] tempArray = new byte[byteValue];
                    Array.Copy(inputData, inputPos + 1, tempArray, 0, byteValue);
                    Array.Copy(tempArray, 0, outputData, outputPos, byteValue);
                    inputPos += byteValue + 1;
                    outputPos += byteValue;
                }
                else if (byteValue <= 0x4F) // 0x40 to 0x4F: generate ascending bytes based on last 2 bytes
                {
                    int delta = outputData[outputPos - 1] - outputData[outputPos - 2];
                    for (int i = 0; i < ((byteValue & 0x0F) + 3); i++)
                    {
                        outputData[outputPos] = (byte)((outputData[outputPos - 1] + delta) & 0xFF);
                        outputPos++;
                    }
                    inputPos++;
                }
                else if (byteValue <= 0x5F) // 0x50 to 0x5F: generate ascending words based on last 2 words
                {
                    short delta = (short)(BitConverter.ToInt16(outputData, outputPos - 2) -
                        BitConverter.ToInt16(outputData, outputPos - 4));
                    for (int i = 0; i < ((byteValue & 0x0F) + 2); i++)
                    {
                        short newShort = (short)(BitConverter.ToInt16(outputData, outputPos - 2) + delta);
                        outputData[outputPos] = (byte)(newShort & 0xFF);
                        outputData[outputPos + 1] = (byte)((newShort >> 8) & 0xFF);
                        outputPos += 2;
                    }
                    inputPos++;
                }
                else if (byteValue <= 0x6F) // 0x60 to 0x6F: clone last byte in output
                {
                    for (int i = 0; i < ((byteValue & 0x0F) + 3); i++)
                    {
                        outputData[outputPos] = outputData[outputPos - 1];
                        outputPos++;
                    }
                    inputPos++;
                }
                else if (byteValue <= 0x7F) // 0x70 to 0x7F: clone last word in output
                {
                    for (int i = 0; i < ((byteValue & 0x0F) + 2); i++)
                    {
                        outputData[outputPos] = outputData[outputPos - 2];
                        outputData[outputPos + 1] = outputData[outputPos - 1];
                        outputPos += 2;
                    }
                    inputPos++;
                }
                else if (byteValue <= 0xBF) // 0x80 to 0xBF: clone 3 bytes using offset
                {
                    int offset = (byteValue & 0x3F);
                    outputData[outputPos] = outputData[outputPos - offset - 3];
                    outputData[outputPos + 1] = outputData[outputPos - offset - 2];
                    outputData[outputPos + 2] = outputData[outputPos - offset - 1];
                    outputPos += 3;
                    inputPos++;
                }
                else if (byteValue <= 0xDF) // 0xC0 to 0xDF: clone using offset and length from next byte
                {
                    int offset = ((byteValue & 0x03) << 8) + Convert.ToInt32(inputData[inputPos + 1]) + 3;
                    int length = ((byteValue >> 2) & 0x07) + 4;
                    for (int i = 0; i < length; i++)
                    {
                        outputData[outputPos] = outputData[outputPos - offset];
                        outputPos++;
                    }
                    inputPos += 2;
                }
                else // 0xE0 to 0xFF: clone using offset and length from next 2 bytes
                {
                    int offset = ((byteValue & 0x1F) << 8) + Convert.ToInt32(inputData[inputPos + 1]) + 3;
                    int length = Convert.ToInt32(inputData[inputPos + 2]) + 5;
                    for (int i = 0; i < length; i++)
                    {
                        outputData[outputPos] = outputData[outputPos - offset];
                        outputPos++;
                    }
                    inputPos += 3;
                }
            }
            return outputData;
        }

        public static byte[] FibDecode(byte[] inputData, int a0, int a1)
        {
            int length = inputData.Length;
            byte[] outputData = new byte[length];

            for (int i = 0; i < length; i++)
            {
                int a2 = (a0 + a1) & 0xFF;
                outputData[i] = (byte)(inputData[i] ^ a2);
                a0 = a1;
                a1 = a2;
            }
            return outputData;
        }
    }
}