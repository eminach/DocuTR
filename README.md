# DocuTextRecognition
Reads the TextFragments from txt file,  and search text occurrence in it.

Used two methods

    - string IndexOf

and 

     regex.Matches

Both variants Benchmarked

# Byte Processing
- The program defines a buffer size for reading image from the input stream. This buffer size can be adjusted based on available memory constraints.

- It reads images sequentially (ReadAsync) from the input stream in chunks determined by the buffer size. This helps in processing large images without loading the entire image into memory at once.

- For each image chunk read, it calls the ProcessImageAsync method to split the image into 4 sub-images and process each sub-image.

- Within the ProcessImageAsync method, it creates sub-image byte arrays and copies the corresponding portion of the image buffer into each sub-image array. This avoids holding the entire image in memory.

- To run this program within a constrained execution environment (Docker), the limit the memory usage of the container using Docker resource constraints. The memory usage of the .NET runtime within the Docker container by setting environment variables or using runtime configuration options. For example, limit the maximum heap size of the .NET runtime using the COMPlus_HeapLimitMB environment variable.