using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1GiuaKy.Object
{
    public class Line : DrawObject
    {
        float a = 0, b = 0;
        public Line(Pen pen, int widthPen)
        {
            this.pen = pen;
            this.widthPen = widthPen;
        }
        public override void ChangePointWithRatioHeight(Point p1, float h, List<float> ratioH)
        {
            this.p1.Y = p1.Y + (int)(ratioH[0] * h / (1 + ratioH[0]));
            this.p2.Y = p1.Y + (int)(ratioH[1] * h / (1 + ratioH[1]));
        }
        public override void ChangePointWithRatioWidth(Point p1, float w, List<float> ratioW)
        {
            this.p1.X = p1.X + (int)(ratioW[0] * w / (1 + ratioW[0]));
            this.p2.X = p1.X + (int)(ratioW[1] * w / (1 + ratioW[1]));
        }
        public override List<float> GetRatioWidth(Point p1, Point p2)
        {
            List<float> temp = new List<float>();
            if(p2.X - this.p1.X == 0)
                temp.Add(((float)this.p1.X - (float)p1.X) / ((float)p2.X - ((float)this.p1.X - (float)0.01)));
            else
                temp.Add(((float)this.p1.X - (float)p1.X)/((float)p2.X - (float)this.p1.X));
            if(p2.X - this.p2.X == 0)
                temp.Add(((float)this.p2.X - (float)p1.X) / ((float)p2.X - ((float)this.p2.X - (float)0.01)));
            else
                temp.Add(((float)this.p2.X - (float)p1.X) / ((float)p2.X - (float)this.p2.X));
            return temp;
        }
        public override List<float> GetRatioHeight(Point p1, Point p2)
        {
            List<float> temp = new List<float>();
            if(p2.Y - this.p1.Y == 0)
                temp.Add(((float)this.p1.Y - (float)p1.Y) / ((float)p2.Y - ((float)this.p1.Y - (float)0.01)));
            else
                temp.Add(((float)this.p1.Y - (float)p1.Y) / ((float)p2.Y - (float)this.p1.Y));
            if(p2.Y - this.p2.Y == 0)
                temp.Add(((float)this.p2.Y - (float)p1.Y) / ((float)p2.Y - ((float)this.p2.Y - (float)0.01)));
            else
                temp.Add(((float)this.p2.Y - (float)p1.Y) / ((float)p2.Y - (float)this.p2.Y));
            return temp;
        }
        public override void ZoomOut()
        {
            if (pen.Width > 1 && p2.X > p1.X + 5)
            {
                pen.Width--;
                if (p2.X - p1.X < 100)
                {
                    p1.Y += 10;
                    p1.X = (int)(((float)p1.Y - b) / a);
                    p2.Y -= 10;
                    p2.X = (int)(((float)p2.Y - b) / a);
                }
                else
                {
                    p1.X += 10;
                    p1.Y = (int)(a * (float)p1.X + b);
                    p2.X -= 10;
                    p2.Y = (int)(a * (float)p2.X + b);
                }
            }
        }
        public override void ZoomIn()
        {
            pen.Width++;
            if (p2.X - p1.X < 100)
            {
                p1.Y -= 10;
                p1.X = (int)(((float)p1.Y - b) / a);
                p2.Y += 10;
                p2.X = (int)(((float)p2.Y - b) / a);
            }
            else
            {
                p1.X -= 10;
                p1.Y = (int)(a * (float)p1.X + b);
                p2.X += 10;
                p2.Y = (int)(a * (float)p2.X + b);
            }
        }
        public override void reSize(Point p)
        {
            if (pointCtrl == p2)
            {
                p2 = p;
                pointCtrl = p2;
            }
            else
            {
                p1 = p;
                pointCtrl = p1;
            }
            convertPoint();
        }
        public override void Move(Point p)
        {
            if (isMoving == false)
            {
                this.isMoving = true;
                this.distanceWidth = p2.X - p1.X;
                this.distanceHeight = p2.Y - p1.Y;
                this.tempWidth = p.X - p1.X;
                this.tempHeight = p.Y - p1.Y;
            }
            else
            {
                p1.X = p.X - tempWidth;
                p1.Y = p.Y - tempHeight;
                p2.X = p1.X + distanceWidth;
                p2.Y = p1.Y + distanceHeight;
            }
            convertPoint();
        }
        public override bool isPointCtrl(Point p)
        {
            GraphicsPath path = new GraphicsPath();
            if(pen.Width > 3)
            {
                System.Drawing.Rectangle a = new System.Drawing.Rectangle(p1.X - (int)pen.Width, p1.Y - (int)pen.Width, 2 * (int)pen.Width, 2 * (int)pen.Width);
                path.AddRectangle(a);
                if(path.IsVisible(p))
                {
                    pointCtrl = p1;
                    path.Dispose();
                    return true;
                }
                path.ClearMarkers();
                System.Drawing.Rectangle b = new System.Drawing.Rectangle(p2.X - (int)pen.Width, p2.Y - (int)pen.Width, 2 * (int)pen.Width, 2 * (int)pen.Width);
                path.AddRectangle(b);
                if (path.IsVisible(p))
                {
                    pointCtrl = p2;
                    path.Dispose();
                    return true;
                }
            }
            else
            {
                System.Drawing.Rectangle a = new System.Drawing.Rectangle(p1.X - 4, p1.Y - 4, 8, 8);
                path.AddRectangle(a);
                if (path.IsVisible(p))
                {
                    pointCtrl = p1;
                    path.Dispose();
                    return true;
                }
                path.ClearMarkers();
                System.Drawing.Rectangle b = new System.Drawing.Rectangle(p2.X - 4, p2.Y - 4, 8, 8);
                path.AddRectangle(b);
                if (path.IsVisible(p))
                {
                    pointCtrl = p2;
                    path.Dispose();
                    return true;
                }
            }
            path.Dispose();
            return false;
        }
        public override void showSelect(PaintEventArgs e)
        {
            Brush brush = new SolidBrush(pen.Color);
            if(pen.Width > 3)
            {
                e.Graphics.FillRectangle(brush, p1.X - pen.Width, p1.Y - pen.Width, 2 * pen.Width, 2 * pen.Width);
                e.Graphics.FillRectangle(brush, p2.X - pen.Width, p2.Y - pen.Width, 2 * pen.Width, 2 * pen.Width);
            }
            else
            {
                e.Graphics.FillRectangle(brush, p1.X - 4, p1.Y - 4, 8, 8);
                e.Graphics.FillRectangle(brush, p2.X - 4, p2.Y - 4, 8, 8);
            }
            brush.Dispose();
        }
        public void convertPoint()
        {
            if (p1.X > p2.X) { Point x = p1; p1 = p2; p2 = x; }
            this.a = ((float)p2.Y - (float)p1.Y) / ((float)p2.X - (float)p1.X);
            this.b = (float)p1.Y - a * (float)p1.X;
        }
        public override bool isHit(Point p)
        {
            bool hit = false;
            convertPoint();
            GraphicsPath path = new GraphicsPath();
            path.AddLine(p1, p2);
            if(pen.Width < 3)
            {
                Pen temp = new Pen(pen.Color, 6);
                hit = path.IsOutlineVisible(p, temp);
            }
            else
                hit = path.IsOutlineVisible(p, pen);
            path.Dispose();
            return hit;
        }
        public override void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen, p1, p2);
        }
        ~Line() { }
    }
}
