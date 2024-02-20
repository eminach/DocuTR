using System;
using System.IO;
using System.Threading.Tasks;

namespace ByteProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Define buffer size for reading images
            int bufferSize = 1024 * 1024; // 1 MB

            // Define sub-image size
            int subImageSize = 12500; // 12.5 KB

            // Define buffer for reading image
            byte[] buffer = new byte[bufferSize];

            // Read images sequentially from input stream
            using (FileStream stream = new FileStream("sample-jpg-image-5mb.jpg", FileMode.Open))
            {
                while (stream.Position < stream.Length)
                {
                    // Read image into buffer
                    //ReadAll shouldn't be used here!
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    // Split the image buffer into 4 chunks
                    await ProcessImageAsync(buffer, bytesRead, subImageSize);

                    // Clear the buffer to release memory
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
        }

        static async Task ProcessImageAsync(byte[] image, int length, int subImageSize)
        {
            // Split the image into 4 sub-images and process each sub-image
            for (int i = 0; i < 4; i++)
            {
                byte[] subImage = new byte[subImageSize];
                Array.Copy(image, i * (length / 4), subImage, 0, subImageSize);

                // Peek some bytes within the sub-image
                // Your processing logic goes here
                await Task.Delay(10); // Processing delay
            }
        }
    }
}
