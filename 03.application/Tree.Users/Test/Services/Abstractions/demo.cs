using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Users.Test.Services.Abstractions
{
    public class demo
    {
        public async Task<object> Main()
        {
            //文字内容
            string Content = "行到水穷处\n坐看云起时";
            string Contents = "待看云起时\n云深不知处";
            //生成图片的地址
            var fielnameNew = AppDomain.CurrentDomain.BaseDirectory + "Image\\ImageTest1.jpg";
            //底图
            var fielnames = AppDomain.CurrentDomain.BaseDirectory + "Image\\ImageTest.jpg";
            Image img = Image.FromFile(fielnames);
            Bitmap bmp = new Bitmap(img, img.Width, img.Height);
            Graphics g = Graphics.FromImage(bmp);
            //问题背景添加（暂时不用）
            //g.FillRectangle(Brushes.LightGray, new Rectangle() { X =0, Y = 0, Height = 0, Width = 0 });

            Font font = new Font("微软雅黑", 15, (FontStyle.Regular));
            Font fonts = new Font("微软雅黑", 40, (FontStyle.Regular));
            //文字内容的坐标以及大小 
            RectangleF textArea = new RectangleF(100, 100, 150, 50);
            RectangleF textAreas = new RectangleF(400, 400, 200, 200);
            //文字内容的颜色
            Brush brush = new SolidBrush(Color.Blue);
            Brush brushs = new SolidBrush(Color.Black);
            g.DrawString(Content, font, brush, textArea);
            g.DrawString(Contents, fonts, brushs, textAreas);
            bmp.Save(fielnameNew);
            //var data = await AppletStudentShowRepository.GetAsync(Guid.Empty);
            return "";
        }
    }
}
