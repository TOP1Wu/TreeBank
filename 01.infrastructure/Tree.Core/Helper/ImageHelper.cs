using System;
using System.DrawingCore.Drawing2D;
using System.IO;
using System.Net;
using Bitmap = System.DrawingCore.Bitmap;
using Brush = System.DrawingCore.Brush;
using Color = System.DrawingCore.Color;
using Font = System.DrawingCore.Font;
using FontStyle = System.DrawingCore.FontStyle;
using Graphics = System.DrawingCore.Graphics;
using GraphicsUnit = System.DrawingCore.GraphicsUnit;
using Pen = System.DrawingCore.Pen;
using Rectangle = System.DrawingCore.Rectangle;
using SolidBrush = System.DrawingCore.SolidBrush;

namespace Tree.Core.Helper
{
   public static class ImageHelper
    {
        /// <summary>
        /// 验证码可以显示的字符集合 
        /// </summary>
        private const string VChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
                                     ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
                                     ",R,S,T,U,V,W,X,Y,Z";

        /// <summary>  
        /// 该方法用于生成指定位数的随机数  
        /// </summary>  
        /// <param name="vCodeNum">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        private static string RndNum(int vCodeNum)
        {
            var vcArray = VChar.Split(','); //拆分成数组   
            var code = ""; //产生的随机数  
            var temp = -1; //记录上次随机数值，尽量避避免生产几个一样的随机数  

            var rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (var i = 1; i < vCodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.UtcNow.Ticks)); //初始化随机类  
                }

                var t = rand.Next(61); //获取随机数  
                if (temp != -1 && temp == t)
                {
                    return RndNum(vCodeNum); //如果获取的随机数重复，则递归调用  
                }

                temp = t; //把本次产生的随机数记录起来  
                code += vcArray[t]; //随机数的位数加一  
            }

            return code;
        }


        /// <summary>  
        /// 该方法是将生成的随机数写入图像文件  
        /// </summary>  
        /// <param name="code">code是一个随机数</param>
        public static MemoryStream CreateVerifyImage(out string code)
        {
            const int codeW = 60;
            const int codeH = 30;
            code = RndNum(4); //生成4位数验证码
            var random = new Random();

            //验证码颜色集合  
            Color[] c =
            {
                Color.Black, Color.Red, Color.DarkBlue,
                Color.Green, Color.Orange, Color.Brown,
                Color.DarkCyan, Color.Purple
            };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };


            Bitmap img = null;
            Graphics g = null;
            var ms = new MemoryStream();
            try
            {
                //根据图像的大小定义，生成图像的实例  
                img = new Bitmap(codeW, codeH);
                g = Graphics.FromImage(img);

                //g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象     
                g.Clear(Color.White); //背景设为白色  

                ////画噪线 
                //for (int i = 0; i < 1; i++)
                //{
                //    int x1 = random.Next(codeW);
                //    int y1 = random.Next(codeH);
                //    int x2 = random.Next(codeW);
                //    int y2 = random.Next(codeH);
                //    System.DrawingCore.Color clr = c[random.Next(c.Length)];
                //    // g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                //    g.DrawRectangle(new Pen(System.DrawingCore.Color.Silver), x2, y2, x1 - 1, y1 - 1);
                //}

                //在随机位置画背景点  
                for (var i = 0; i < 100; i++)
                {
                    var x = random.Next(img.Width);
                    var y = random.Next(img.Height);
                    g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
                }

                //验证码绘制在g中  
                for (var i = 0; i < code.Length; i++)
                {
                    var cIndex = random.Next(7); //随机颜色索引值  
                    var fIndex = random.Next(5); //随机字体索引值  
                    var f = new Font(fonts[fIndex], 15, FontStyle.Bold); //字体  
                    Brush b = new SolidBrush(c[cIndex]); //颜色  
                    var ii = 4;
                    if ((i + 1) % 2 == 0) //控制验证码不在同一高度  
                    {
                        ii = 2;
                    }

                    g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii); //绘制一个验证字符  
                }

                img.Save(ms, System.DrawingCore.Imaging.ImageFormat.Jpeg); //将此图像以Png图像文件的格式保存到流中  


                return ms;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                //回收资源  
                g.Dispose();
                img?.Dispose();
                ms.Dispose();
            }



        }


        /// <summary>
        /// 按照指定的高和宽生成相应的规格的图片，采用此方法生成的缩略图片不会失真
        /// </summary>
        /// <param name="images"></param>
        /// <param name="width">指定宽度</param>
        /// <param name="height">指定高度</param>
        /// <returns>返回新生成的图</returns>
        public static byte[] ReducedImage(string images, int width, int height)
        {
            var bmp = new Bitmap(width, height, System.DrawingCore.Imaging.PixelFormat.Format24bppRgb);

            using (var imageFrom = System.DrawingCore.Image.FromFile(images))
            {
                // 生成的缩略图在上述"画布"上的位置
                var X = 0;
                var Y = 0;

                // 创建画布

                bmp.SetResolution(imageFrom.HorizontalResolution, imageFrom.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // 用白色清空 
                    g.Clear(Color.White);

                    // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // 指定高质量、低速度呈现。 
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
                    g.DrawImage(imageFrom, new Rectangle(X, Y, width, height),
                        new Rectangle(0, 0, imageFrom.Width, imageFrom.Height), GraphicsUnit.Pixel);
                }

                using (var stream = new MemoryStream())
                {
                    bmp.Save(stream, System.DrawingCore.Imaging.ImageFormat.Jpeg);
                    var data = new byte[stream.Length];
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Read(data, 0, Convert.ToInt32(stream.Length));
                    bmp.Dispose();

                    return data;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static byte[] CreateThumbImage(string imagePath, string newImagePath)
        {
            //固定高度 150px
            System.DrawingCore.Image srcImage = System.DrawingCore.Image.FromFile(imagePath);
            // 源尺寸
            var sourceHeight = srcImage.Height;
            var sourceWeight = srcImage.Width;

            int nowWeight = (int)(150.0 * sourceWeight / sourceHeight);
            int nowHeight = 150;

            try
            {
                // 要保存到的图片 
                System.DrawingCore.Bitmap b = new System.DrawingCore.Bitmap(nowWeight, nowHeight);
                System.DrawingCore.Graphics g = System.DrawingCore.Graphics.FromImage(b);
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(srcImage, new System.DrawingCore.Rectangle(0, 0, nowWeight, nowHeight), new System.DrawingCore.Rectangle(0, 0, srcImage.Width, srcImage.Height), System.DrawingCore.GraphicsUnit.Pixel);
                g.Dispose();
                return Bitmap2Byte(b);
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] Bitmap2Byte(System.DrawingCore.Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.DrawingCore.Imaging.ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }


        /// <summary>
        /// 从url读取内容到内存MemoryStream流中
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MemoryStream DownLoadFileToMemoryStream(string url)
        {
            var wreq = HttpWebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = wreq.GetResponse() as HttpWebResponse;
            MemoryStream ms = null;
            using (var stream = response.GetResponseStream())
            {
                Byte[] buffer = new Byte[response.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                } while (actuallyRead > 0);

                ms = new MemoryStream(buffer);
            }

            response.Close();
            return ms;
        }

        /// <summary>
        /// 流转base64
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToBase64(MemoryStream stream)
        {
            byte[] bt = new byte[stream.Length];
            //调用read读取方法
            stream.Read(bt, 0, bt.Length);
            return Convert.ToBase64String(bt);
        }

    }
}
