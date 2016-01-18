
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Napoleon.PublicCommon.Field;

namespace Napoleon.PublicCommon.Format
{
    public static class FormatFunc
    {

        #region 格式化通用类

        /// <summary>
        ///  数组转换通用类
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="split">分隔符</param>
        /// <param name="isQuote">是否需要引号</param>
        /// Author  : Napoleon
        /// Created : 2014-10-30 14:37:20
        public static string FormatArray(this string[] array, char split, bool isQuote = true)
        {
            StringBuilder str = new StringBuilder();
            foreach (var ar in array)
            {
                if (isQuote)
                {
                    str.AppendFormat("'{0}',", ar);
                }
                else
                {
                    str.AppendFormat("{0},", ar);
                }
            }
            return str.ToString().Trim(split);
        }

        #endregion

        #region 具体格式化形式

        /// <summary>
        ///  将{1,2,3}转换成{'1','2','3'}
        /// </summary>
        /// <param name="array">The array.</param>
        /// Author  : Napoleon
        /// Created : 2015-05-30 11:20:04
        public static string SwitchArray(this string array)
        {
            string[] arrays = array.Split(BaseFields.CommaSplit);
            string result = arrays.FormatArray(BaseFields.CommaSplit);
            return result;
        }

        #endregion

        /// <summary>
        ///  去除HTML标记
        /// </summary>
        /// <returns>已经去除后的文字</returns>
        /// Author  : Napoleon
        /// Created : 2015-10-29 09:52:45
        public static string NoHtml(string htmlString)
        {
            //删除脚本
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);
            htmlString = htmlString.Replace("<", "").Replace(">", "").Replace("\r\n", "");
            htmlString = HttpContext.Current.Server.HtmlEncode(htmlString).Trim();
            return htmlString;
        }

        #region 图形验证码

        /// <summary>
        ///  产生图形验证码
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-11-04 20:47:48
        public static byte[] CreateValidateGraphic(out String code, int codeLength, int width, int height, int fontSize)
        {
            String sCode = String.Empty;
            //颜色列表
            Color[] oColors =
            {
                Color.Black,
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Orange,
                Color.Brown,
                Color.Brown,
                Color.DarkBlue
            };
            //字体列表,用于验证码
            string[] oFontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            //验证码的子元集
            char[] oCharacter =
            {
                '2', '3', '4', '5', '6', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N','Q', 'P', 'R', 'S', 'T', 'U','V','W', 'X', 'Y','Z'
            };
            var oRnd = new Random();
            Bitmap oBmp;
            Graphics oGraphics;
            int n1;
            Point oPoint1 = default(Point);
            Point oPoint2 = default(Point);
            string sFontName;
            Font oFont;
            Color oColor;
            //生成验证码字串
            for (n1 = 0; n1 <= codeLength - 1; n1++)
            {
                sCode += oCharacter[oRnd.Next(oCharacter.Length)];
            }
            oBmp = new Bitmap(width, height);
            oGraphics = Graphics.FromImage(oBmp);
            oGraphics.Clear(Color.White);
            try
            {
                for (n1 = 0; n1 <= 4; n1++)
                {
                    //画线
                    oPoint1.X = oRnd.Next(width);
                    oPoint1.Y = oRnd.Next(height);
                    oPoint2.X = oRnd.Next(width);
                    oPoint2.Y = oRnd.Next(height);
                    oColor = oColors[oRnd.Next(oColors.Length)];
                    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
                }
                float spaceWith = 0, dotX, dotY;
                if (codeLength != 0)
                {
                    spaceWith = (width - fontSize * codeLength - 10) / codeLength;
                }
                for (n1 = 0; n1 <= sCode.Length - 1; n1++)
                {
                    //验证码字串
                    sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                    oFont = new Font(sFontName, fontSize, FontStyle.Italic);
                    oColor = oColors[oRnd.Next(oColors.Length)];
                    dotY = (height - oFont.Height) / 2 + 2; //中心下移2像素
                    dotX = Convert.ToSingle(n1) * fontSize + (n1 + 1) * spaceWith;
                    oGraphics.DrawString(sCode[n1].ToString(), oFont, new SolidBrush(oColor), dotX, dotY);
                }
                for (int i = 0; i <= 30; i++)
                {
                    //画噪点
                    int x = oRnd.Next(oBmp.Width);
                    int y = oRnd.Next(oBmp.Height);
                    Color clr = oColors[oRnd.Next(oColors.Length)];
                    oBmp.SetPixel(x, y, clr);
                }
                code = sCode;
                //保存图片数据
                var stream = new MemoryStream();
                oBmp.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                oGraphics.Dispose();
            }
        }

        #endregion



    }
}
