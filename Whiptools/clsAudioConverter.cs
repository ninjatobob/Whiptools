using System;
using System.IO;

namespace Whiptools
{
    class WavAudio
    {
        static int sampleRate = 11025;
        static short bitDepth = 8;

        public static byte[] ConvertRawToWav(byte[] rawBytes)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                WriteWavHeader(outputStream, rawBytes.Length, sampleRate, bitDepth);
                outputStream.Write(rawBytes, 0, rawBytes.Length);
                return outputStream.ToArray();
            }
        }

        private static void WriteWavHeader(Stream stream, int dataLength, int sampleRate, short bitDepth)
        {
            const int headerSize = 44;

            // RIFF header
            WriteString(stream, "RIFF");
            WriteInt32(stream, headerSize + dataLength - 8);
            WriteString(stream, "WAVE");

            // Format chunk
            WriteString(stream, "fmt ");
            WriteInt32(stream, 16); // Subchunk1Size
            WriteInt16(stream, 1); // AudioFormat (1 for PCM)
            WriteInt16(stream, 1); // NumChannels (1 for mono)
            WriteInt32(stream, sampleRate); // SampleRate
            WriteInt32(stream, sampleRate * (bitDepth / 8)); // ByteRate
            WriteInt16(stream, (short)(bitDepth / 8)); // BlockAlign
            WriteInt16(stream, bitDepth); // BitsPerSample

            // Data chunk
            WriteString(stream, "data");
            WriteInt32(stream, dataLength); // Subchunk2Size
        }

        static void WriteInt16(Stream stream, short value)
        {
            stream.WriteByte((byte)(value & 0xFF));
            stream.WriteByte((byte)((value >> 8) & 0xFF));
        }

        static void WriteInt32(Stream stream, int value)
        {
            stream.WriteByte((byte)(value & 0xFF));
            stream.WriteByte((byte)((value >> 8) & 0xFF));
            stream.WriteByte((byte)((value >> 16) & 0xFF));
            stream.WriteByte((byte)((value >> 24) & 0xFF));
        }

        static void WriteString(Stream stream, string value)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    class HMPMIDI
    {
        public static byte[] ConvertToRevisedFormat(byte[] inputData)
        {
            int outputLength = inputData.Length + 1000;
            byte[] outputData = new byte[outputLength];
            return outputData;
        }
    }
}