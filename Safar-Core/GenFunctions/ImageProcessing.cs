using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SafarCore.GenFunctions
{
    public class ImageProcessing
    {

        public static FuncResult CompressImage(string fileName, string inputDirectory, 
            string outputDirectory, ImageOption imageOption = null)
        {

            if (imageOption == null)
            {
                imageOption = new ImageOption();
            }
            var size = imageOption.Size;
            var quality = imageOption.Quality;

            using (var image = new Bitmap(Image.FromFile(inputDirectory + "/" + fileName)))
            {
                int width, height;
                if (image.Width > image.Height)
                {
                    width = size;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);
                    using (var output = File.Open(outputDirectory + "/" + fileName, FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1)
                        {
                            Param = {[0] = new EncoderParameter(qualityParamId, quality)}
                        };
                        var codec = ImageCodecInfo.GetImageDecoders()
                            .FirstOrDefault(codec1 => codec1.FormatID == ImageFormat.Jpeg.Guid);
                        resized.Save(output, codec, encoderParameters);
                    }
                }
            }

            return FuncResult.Successful;
        }


    }

    public class ImageOption
    {
        public int Size { get; set; }
        public int Quality { get; set; }

        public ImageOption()
        {
            Size = 150;
            Quality = 75;
        }

        public ImageOption(int size, int quality)
        {
            Size = size;
            Quality = quality;
        }
    }
}
