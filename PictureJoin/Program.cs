using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitmapList = new[]
            {
                new Bitmap(@"D:\画像\アズールレーン\DPeoZeDVQAckY4f.jpg"),
                new Bitmap(@"D:\画像\艦これ\DPeLW92UQAAsYZj.png")
            };
            
            var records = bitmapList.Select(x => new {
                Bitmap = x,
                Height = bitmapList.Select(y => y.Height).Min(),
                Width = x.Width * ((float)bitmapList.Select(y => y.Height).Min() / x.Height)
            });

            using (var img3 = new Bitmap(records.Sum(x => (int)x.Width), records.First().Height))
            using (var g3 = Graphics.FromImage(img3))
            {
                var nowPointX = 0.0f;

                foreach (var record in records)
                {
                    using (var g = Graphics.FromImage(record.Bitmap))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g3.DrawImage(record.Bitmap, nowPointX, 0, record.Width, record.Height);
                        nowPointX += record.Width;
                    }
                }

                img3.Save(@"D:\画像\アズールレーン\result.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            foreach (var bitmap in bitmapList)
            {
                bitmap.Dispose();
            }
        }
    }
}
