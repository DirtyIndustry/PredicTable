using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PredicTable.Commen
{
    /// <summary>
    /// 生成曲线图
    /// </summary>
    public class MyCurve
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; set; }
        public string title { get; set; }

        public int top { get; set; }
        public int bottom { get; set; }
        public int left { get; set; }
        public int right { get; set; }

        /// <summary>
        /// x轴标注，与values长度相同
        /// </summary>
        public string[] xKeys { get; set; }
        /// <summary>
        /// y轴标注
        /// </summary>
        public double[] yKeys { get; set; }
        /// <summary>
        /// 值，与xKeys长度相同
        /// </summary>
        public float[] values { get; set; }
        /// <summary>
        /// 整个图片的背景色，默认白色
        /// </summary>
        public Color bgColor = Color.White;
        /// <summary>
        /// 曲线图的背景色
        /// </summary>
        public Color xybgColor = Color.White;
        /// <summary>
        /// 轴线颜色
        /// </summary>
        public Color axisColor = Color.Black;
        /// <summary>
        /// 轴线宽度
        /// </summary>
        public int axisWidth = 1;
        /// <summary>
        /// 网格线
        /// </summary>
        public bool grid = false;
        /// <summary>
        /// 网格线颜色
        /// </summary>
        public Color gridColor = Color.Gray;
        /// <summary>
        ///  网格线宽度
        /// </summary>
        public int gridWidth = 1;
        public Color curveColor = Color.FromArgb(2, 114, 248);
        public int count = 100;
        public Bitmap CreateCurve()
        {
            Bitmap objBitmap = new Bitmap(width, height);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            //填充整个背景色
            objGraphics.FillRectangle(new SolidBrush(bgColor), 0, 0, width, height);
            //填充xy区域背景色
            objGraphics.FillRectangle(new SolidBrush(xybgColor), left, top, width - left - right, height - top - bottom);
            //xy轴上下左右线
            objGraphics.DrawLine(new Pen(new SolidBrush(axisColor), axisWidth), left, top, width - right, top);
            objGraphics.DrawLine(new Pen(new SolidBrush(axisColor), axisWidth), width - right, top, width - right, height - bottom);
            objGraphics.DrawLine(new Pen(new SolidBrush(axisColor), axisWidth), left, height - bottom
            , width - right, height - bottom);
            objGraphics.DrawLine(new Pen(new SolidBrush(axisColor), axisWidth), left, top, left, height - bottom);

            //x轴y轴
            int xWidht = width - left - right;
            int yHeight = height - top - bottom;
            double xSlice = xWidht * 1.0 / (xKeys.Length - 1);
            double ySlice = yHeight * 1.0 / (yKeys.Length - 1);

            for (int i = 0; i < xKeys.Length; i++)
            {
                if (grid)
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(gridColor), gridWidth), (int)(xSlice * i + left), top, (int)(xSlice * i + left), height - bottom);
                }
                if (i > 0 && i != xKeys.Length && i % count == 0)
                    objGraphics.DrawLine(new Pen(new SolidBrush(Color.Black), 2), (int)(xSlice * i + left), top, (int)(xSlice * i + left), height - bottom);
                objGraphics.DrawString(xKeys[i].ToString(), new Font("宋体", 10), new SolidBrush(Color.Black), (int)(xSlice * i + left) - 20, height - bottom);
            }

            for (int i = 0; i < yKeys.Length; i++)
            {
                if (grid)
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(gridColor), gridWidth), left, (int)(top + ySlice * i), width - right, (int)(top + ySlice * i));
                }
                objGraphics.DrawLine(new Pen(new SolidBrush(Color.Black), 1), left, (int)(top + ySlice * i), left + 5, (int)(top + ySlice * i));
                objGraphics.DrawString(yKeys[i].ToString(), new Font("宋体", 10), new SolidBrush(Color.Black), left - 30, (int)(top + ySlice * i) - 10);
            }

            //绘制曲线
            double slice = xWidht * 1.0 / (values.Length - 1);
            Pen CurvePen = new Pen(curveColor, 3);
            PointF[] CurvePointF = new PointF[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                CurvePointF[i] = new PointF((int)(left + slice * i),
                    (int)(top +
                    (height - top - bottom) * (yKeys[0] - values[i]) / (yKeys[0] - yKeys[yKeys.Length - 1]))
                    );
            }
            objGraphics.DrawCurve(CurvePen, CurvePointF, 0.5f);



            //objGraphics.DrawString("牧场预报", new Font("宋体", 15), new SolidBrush(Color.Black), width / 2 - 20, 0);
            //objGraphics.DrawString(title, new Font("宋体", 15, FontStyle.Bold), new SolidBrush(Color.Black), width / 2 - 50, 20);
            //objGraphics.DrawString(" 潮位72hr预报", new Font("宋体", 15), new SolidBrush(Color.Black), width / 2 + 20, 20);
            objGraphics.DrawString(title, new Font("宋体", 15), new SolidBrush(Color.Black), width / 2 - 150, 15);

            //Bitmap logo = new Bitmap("logo.png");
            Bitmap logo = new Bitmap(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory+ "/Images/", "logo.png"));

            objGraphics.DrawImage(logo, 755, 495);
            objGraphics.DrawString("预报要素：潮位（厘米）", new Font("宋体", 15), new SolidBrush(Color.Black), 845, 505);
            objGraphics.DrawString("起报时间：" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00", new Font("宋体", 15), new SolidBrush(Color.Black), 845, 530);
            objGraphics.DrawString("发布单位：国家海洋局北海预报中心", new Font("宋体", 15), new SolidBrush(Color.Black), 845, 550);
            objGraphics.DrawString("山东省海洋预报台", new Font("宋体", 15), new SolidBrush(Color.Black), 945, 575);
            //objBitmap.Save("mybit.jpg", ImageFormat.Jpeg);
            return objBitmap;
        }
    }
}