using System.Drawing.Imaging;

namespace PhotoEditor.Imaging;

internal static class NoiseFilters
{
    // Оновлення попереднього перегляду після застосування сигма-фільтра
    internal static Bitmap ApplySigmaFilter(Bitmap src, double sigma)
    {
        // Обмеження Sigma від 0 до 100
        sigma = Math.Max(0, Math.Min(100, sigma));

        Bitmap result = new Bitmap(
            src.Width,
            src.Height,
            PixelFormat.Format32bppArgb);

        // Обхід усіх пікселів зображення
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                Color center = src.GetPixel(x, y);

                int sumR = 0;
                int sumG = 0;
                int sumB = 0;
                int count = 0;

                // Аналіз сусідніх пікселів у вікні 3 × 3
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx < 0 || nx >= src.Width ||
                            ny < 0 || ny >= src.Height)
                            continue;

                        Color neighbor = src.GetPixel(nx, ny);

                        // Різниця яскравості
                        int centerBrightness = BitmapOperations.GetBrightness(center);
                        int neighborBrightness = BitmapOperations.GetBrightness(neighbor);

                        // Перевірка допустимої різниці
                        if (Math.Abs(centerBrightness - neighborBrightness) <= sigma)
                        {
                            sumR += neighbor.R;
                            sumG += neighbor.G;
                            sumB += neighbor.B;
                            count++;
                        }
                    }
                }

                if (count > 0)
                {
                    result.SetPixel(
                        x,
                        y,
                        Color.FromArgb(
                            center.A,
                            BitmapOperations.Clamp(sumR / count),
                            BitmapOperations.Clamp(sumG / count),
                            BitmapOperations.Clamp(sumB / count)));
                }
                else
                {
                    result.SetPixel(x, y, center);
                }
            }
        }

        return result;
    }

    internal static Bitmap ApplyNoiseFilter(Bitmap source, int filterIndex)
    {
        // 3 лінійні та 7 нелінійних фільтрів.
        // Реалізація не залежить від OpenCvSharp, тому додаткові NuGet-пакети не потрібні.
        switch (filterIndex)
        {
            // ---------- ЛІНІЙНІ ФІЛЬТРИ ----------
            case 0:
                return ApplyConvolutionFilter(source, new double[,]
            {
                { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
                { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
                { 1.0 / 9, 1.0 / 9, 1.0 / 9 }
            });

            case 1:
                return ApplyConvolutionFilter(source, new double[,]
            {
                { 1, 2, 1 },
                { 2, 4, 2 },
                { 1, 2, 1 }
            }, 16.0);

            case 2:
                return ApplyConvolutionFilter(source, new double[,]
            {
                { 1,  4,  6,  4, 1 },
                { 4, 16, 24, 16, 4 },
                { 6, 24, 36, 24, 6 },
                { 4, 16, 24, 16, 4 },
                { 1,  4,  6,  4, 1 }
            }, 256.0);

            // ---------- НЕЛІНІЙНІ ФІЛЬТРИ ----------
            case 3: return ApplyMedianFilter(source, 3);
            case 4: return ApplyMedianFilter(source, 5);
            case 5: return ApplyMinMaxFilter(source, true);
            case 6: return ApplyMinMaxFilter(source, false);
            case 7: return ApplyMidpointFilter(source);
            case 8: return ApplyAlphaTrimmedMeanFilter(source);
            case 9: return ApplyBilateralFilter(source);
            default: return BitmapOperations.Ensure32bpp(source);
        }
    }

    private static Bitmap ApplyConvolutionFilter(Bitmap source, double[,] kernel, double divisor = 1.0)
    {
        Bitmap result = BitmapOperations.Ensure32bpp(source);
        int size = kernel.GetLength(0);
        int radius = size / 2;

        for (int y = radius; y < source.Height - radius; y++)
        {
            for (int x = radius; x < source.Width - radius; x++)
            {
                double sumR = 0, sumG = 0, sumB = 0;
                for (int ky = -radius; ky <= radius; ky++)
                {
                    for (int kx = -radius; kx <= radius; kx++)
                    {
                        Color c = source.GetPixel(x + kx, y + ky);
                        double w = kernel[ky + radius, kx + radius];
                        sumR += c.R * w;
                        sumG += c.G * w;
                        sumB += c.B * w;
                    }
                }
                Color center = source.GetPixel(x, y);
                result.SetPixel(x, y, Color.FromArgb(center.A,
                    BitmapOperations.Clamp((int)Math.Round(sumR / divisor)),
                    BitmapOperations.Clamp((int)Math.Round(sumG / divisor)),
                    BitmapOperations.Clamp((int)Math.Round(sumB / divisor))));
            }
        }
        return result;
    }

    private static Bitmap ApplyMedianFilter(Bitmap source, int windowSize)
    {
        Bitmap result = BitmapOperations.Ensure32bpp(source);
        int radius = windowSize / 2;
        int count = windowSize * windowSize;
        int[] rs = new int[count];
        int[] gs = new int[count];
        int[] bs = new int[count];

        for (int y = radius; y < source.Height - radius; y++)
        {
            for (int x = radius; x < source.Width - radius; x++)
            {
                int n = 0;
                for (int dy = -radius; dy <= radius; dy++)
                {
                    for (int dx = -radius; dx <= radius; dx++)
                    {
                        Color c = source.GetPixel(x + dx, y + dy);
                        rs[n] = c.R; gs[n] = c.G; bs[n] = c.B; n++;
                    }
                }
                Array.Sort(rs); Array.Sort(gs); Array.Sort(bs);
                Color center = source.GetPixel(x, y);
                result.SetPixel(x, y, Color.FromArgb(center.A, rs[count / 2], gs[count / 2], bs[count / 2]));
            }
        }
        return result;
    }

    private static Bitmap ApplyMinMaxFilter(Bitmap source, bool minimum)
    {
        Bitmap result = BitmapOperations.Ensure32bpp(source);
        for (int y = 1; y < source.Height - 1; y++)
        {
            for (int x = 1; x < source.Width - 1; x++)
            {
                int r = minimum ? 255 : 0;
                int g = minimum ? 255 : 0;
                int b = minimum ? 255 : 0;
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        Color c = source.GetPixel(x + dx, y + dy);
                        r = minimum ? Math.Min(r, c.R) : Math.Max(r, c.R);
                        g = minimum ? Math.Min(g, c.G) : Math.Max(g, c.G);
                        b = minimum ? Math.Min(b, c.B) : Math.Max(b, c.B);
                    }
                }
                Color center = source.GetPixel(x, y);
                result.SetPixel(x, y, Color.FromArgb(center.A, r, g, b));
            }
        }
        return result;
    }

    private static Bitmap ApplyBilateralFilter(Bitmap source)
    {
        Bitmap result = BitmapOperations.Ensure32bpp(source);
        const int radius = 2;
        const double sigmaSpace = 2.0;
        const double sigmaColor = 30.0;

        for (int y = radius; y < source.Height - radius; y++)
        {
            for (int x = radius; x < source.Width - radius; x++)
            {
                Color center = source.GetPixel(x, y);
                double centerBrightness = (center.R + center.G + center.B) / 3.0;
                double sumW = 0, sumR = 0, sumG = 0, sumB = 0;

                for (int dy = -radius; dy <= radius; dy++)
                {
                    for (int dx = -radius; dx <= radius; dx++)
                    {
                        Color c = source.GetPixel(x + dx, y + dy);
                        double brightness = (c.R + c.G + c.B) / 3.0;
                        double spatial = dx * dx + dy * dy;
                        double colorDiff = brightness - centerBrightness;
                        double w = Math.Exp(-spatial / (2 * sigmaSpace * sigmaSpace)) *
                                   Math.Exp(-(colorDiff * colorDiff) / (2 * sigmaColor * sigmaColor));
                        sumW += w;
                        sumR += c.R * w;
                        sumG += c.G * w;
                        sumB += c.B * w;
                    }
                }

                result.SetPixel(x, y, Color.FromArgb(center.A,
                    BitmapOperations.Clamp((int)Math.Round(sumR / sumW)),
                    BitmapOperations.Clamp((int)Math.Round(sumG / sumW)),
                    BitmapOperations.Clamp((int)Math.Round(sumB / sumW))));
            }
        }
        return result;
    }

    private static Bitmap ApplyMidpointFilter(Bitmap source)
    {
        Bitmap result = new Bitmap(source.Width, source.Height);

        for (int y = 1; y < source.Height - 1; y++)
        {
            for (int x = 1; x < source.Width - 1; x++)
            {
                int min = 255;
                int max = 0;

                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        Color c = source.GetPixel(x + dx, y + dy);

                        int brightness =
                            (c.R + c.G + c.B) / 3;

                        if (brightness < min)
                            min = brightness;

                        if (brightness > max)
                            max = brightness;
                    }
                }

                int value = (min + max) / 2;

                result.SetPixel(
                    x,
                    y,
                    Color.FromArgb(value, value, value));
            }
        }

        return result;
    }

    private static Bitmap ApplyAlphaTrimmedMeanFilter(Bitmap source)
    {
        Bitmap result = new Bitmap(source.Width, source.Height);

        for (int y = 1; y < source.Height - 1; y++)
        {
            for (int x = 1; x < source.Width - 1; x++)
            {
                List<int> values = new List<int>();

                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        Color c = source.GetPixel(x + dx, y + dy);

                        int brightness =
                            (c.R + c.G + c.B) / 3;

                        values.Add(brightness);
                    }
                }

                values.Sort();

                // Відкидаємо мінімальне і максимальне значення
                values.RemoveAt(values.Count - 1);
                values.RemoveAt(0);

                int sum = 0;

                foreach (int value in values)
                    sum += value;

                int average = sum / values.Count;

                result.SetPixel(
                    x,
                    y,
                    Color.FromArgb(
                        average,
                        average,
                        average));
            }
        }

        return result;
    }
}
