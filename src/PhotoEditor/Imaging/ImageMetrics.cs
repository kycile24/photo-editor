namespace PhotoEditor.Imaging;

internal static class ImageMetrics
{
    internal static double CalculateMSE(Bitmap image1, Bitmap image2)
    {
        if (image1.Width != image2.Width ||
            image1.Height != image2.Height)
        {
            throw new ArgumentException(
                "Зображення повинні мати однаковий розмір.");
        }

        double sum = 0;

        for (int y = 0; y < image1.Height; y++)
        {
            for (int x = 0; x < image1.Width; x++)
            {
                Color c1 = image1.GetPixel(x, y);
                Color c2 = image2.GetPixel(x, y);

                double b1 =
                    (c1.R + c1.G + c1.B) / 3.0;

                double b2 =
                    (c2.R + c2.G + c2.B) / 3.0;

                double difference = b1 - b2;

                sum += difference * difference;
            }
        }

        return sum /
            (image1.Width * image1.Height);
    }
}
