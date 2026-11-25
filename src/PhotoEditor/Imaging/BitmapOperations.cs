using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoEditor.Imaging;

internal static class BitmapOperations
{
    internal static Bitmap Ensure32bpp(Image image)
    {
        Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
        }
        return bmp;
    }

    internal static Bitmap ResizeBitmap(Image source, int width, int height)
    {
        Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(result))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(source, 0, 0, width, height);
        }
        return result;
    }

    // Попіксельне застосування кольорового перетворення
    internal static Bitmap ApplyColorTransform(Bitmap src, string mode)
    {
        // Створення нового порожнього зображення з підтримкою альфа-каналу
        Bitmap result = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);

        // Обхід кожного пікселя зображення
        for (int x = 0; x < src.Width; x++)
        {
            for (int y = 0; y < src.Height; y++)
            {
                Color pixel = src.GetPixel(x, y);
                int gray = GetBrightness(pixel); // Обчислення рівня сірого

                // Режим 1: Бінаризація (порогове перетворення за значенням 128)
                if (mode == "binary")
                {
                    int value = gray >= 128 ? 255 : 0;
                    result.SetPixel(x, y, Color.FromArgb(pixel.A, value, value, value));
                }
                // Режим 2: Інверсія кольорів (негатив)
                else if (mode == "negative")
                {
                    result.SetPixel(x, y, Color.FromArgb(
                        pixel.A,
                        255 - pixel.R,
                        255 - pixel.G,
                        255 - pixel.B));
                }
                // Режим 3 (default): Градації сірого (Grayscale)
                else
                {
                    result.SetPixel(x, y, Color.FromArgb(pixel.A, gray, gray, gray));
                }
            }
        }

        return result;
    }

    // Обчислення значення яскравості пікселя (Luminance)
    internal static int GetBrightness(Color pixel)
    {
        // Формула взваженого сумування RGB компонентів за стандартом ITU-R BT.601
        return Clamp((int)Math.Round(
            0.299 * pixel.R +
            0.587 * pixel.G +
            0.114 * pixel.B));
    }

    internal static int Clamp(int value)
    {
        if (value < 0) return 0;
        if (value > 255) return 255;
        return value;
    }

    internal static int ReplaceColor(Bitmap bmp, Color targetColor, Color newColor, int tolerance)
    {
        int replacedPixels = 0;

        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color pixel = bmp.GetPixel(x, y);

                if (Math.Abs(pixel.R - targetColor.R) <= tolerance &&
                    Math.Abs(pixel.G - targetColor.G) <= tolerance &&
                    Math.Abs(pixel.B - targetColor.B) <= tolerance)
                {
                    bmp.SetPixel(x, y, Color.FromArgb(pixel.A, newColor.R, newColor.G, newColor.B));
                    replacedPixels++;
                }
            }
        }

        return replacedPixels;
    }

    internal static void AdjustBalance(Bitmap bmp, double factorR, double factorG, double factorB, double factorBr)
    {
        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color pixel = bmp.GetPixel(x, y);

                int r = Clamp((int)(pixel.R * factorR * factorBr));
                int g = Clamp((int)(pixel.G * factorG * factorBr));
                int b = Clamp((int)(pixel.B * factorB * factorBr));

                bmp.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
            }
        }
    }

    internal static void AdjustAlpha(Bitmap bmp, double factorAlpha)
    {
        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color pixel = bmp.GetPixel(x, y);
                int newAlpha = Clamp((int)(pixel.A * factorAlpha));
                bmp.SetPixel(x, y, Color.FromArgb(newAlpha, pixel.R, pixel.G, pixel.B));
            }
        }
    }

    internal static void AdjustContrast(Bitmap bmp, double factorContrast)
    {
        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color pixel = bmp.GetPixel(x, y);

                int r = Clamp((int)((pixel.R - 128) * factorContrast + 128));
                int g = Clamp((int)((pixel.G - 128) * factorContrast + 128));
                int b = Clamp((int)((pixel.B - 128) * factorContrast + 128));

                bmp.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
            }
        }
    }
}
