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
    public class Rectangle : DrawObject
    {
        public Rectangle(Pen pen, bool isFill, int widthPen)
        {
            this.pen = pen;
            this.isFill = isFill;
            this.widthPen = widthPen;
        }
        public Rectangle(Brush brush, bool isFill, Color colorBrush)
        {
            this.brush = brush;
            this.isFill = isFill;
            this.colorBrush = colorBrush;
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
            if (p2.X - this.p1.X == 0)
                temp.Add(((float)this.p1.X - (float)p1.X) / ((float)p2.X - ((float)this.p1.X - (float)0.01)));
            else
                temp.Add(((float)this.p1.X - (float)p1.X) / ((float)p2.X - (float)this.p1.X));
            if (p2.X - this.p2.X == 0)
                temp.Add(((float)this.p2.X - (float)p1.X) / ((float)p2.X - ((float)this.p2.X - (float)0.01)));
            else
                temp.Add(((float)this.p2.X - (float)p1.X) / ((float)p2.X - (float)this.p2.X));
            return temp;
        }
        public override List<float> GetRatioHeight(Point p1, Point p2)
        {
            List<float> temp = new List<float>();
            if (p2.Y - this.p1.Y == 0)
                temp.Add(((float)this.p1.Y - (float)p1.Y) / ((float)p2.Y - ((float)this.p1.Y - (float)0.01)));
            else
                temp.Add(((float)this.p1.Y - (float)p1.Y) / ((float)p2.Y - (float)this.p1.Y));
            if (p2.Y - this.p2.Y == 0)
                temp.Add(((float)this.p2.Y - (float)p1.Y) / ((float)p2.Y - ((float)this.p2.Y - (float)0.01)));
            else
                temp.Add(((float)this.p2.Y - (float)p1.Y) / ((float)p2.Y - (float)this.p2.Y));
            return temp;
        }
        public override void ZoomOut()
        {
            if (pen.Width > 1 && p2.X > p1.X)
            {
                if(!isFill)
                    pen.Width--;
                p1.X += 10;
                p1.Y += 10;
                p2.X -= 10;
                p2.Y -= 10;
            }
        }
        public override void ZoomIn()
        {
            if (!isFill)
                pen.Width++;
            p1.X -= 10;
            p1.Y -= 10;
            p2.X += 10;
            p2.Y += 10;
        }
        public override void reSize(Point p)
        {
            if(pointCtrl == p1)
            {
                p1 = p;
                pointCtrl = p1;
            }
            else if(pointCtrl.X == p2.X && pointCtrl.Y == p1.Y)
            {
                p2.X = p.X;
                p1.Y = p.Y;
                pointCtrl.X = p2.X;
                pointCtrl.Y = p1.Y;
            }
            else if(pointCtrl.X == p1.X && pointCtrl.Y == p2.Y)
            {
                p1.X = p.X;
                p2.Y = p.Y;
                pointCtrl.X = p1.X;
                pointCtrl.Y = p2.Y;
            }
            else
            {
                p2 = p;
                pointCtrl = p2;
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
        }
        public override bool isPointCtrl(Point p)
        {
            GraphicsPath path = new GraphicsPath();
            System.Drawing.Rectangle a = new System.Drawing.Rectangle(p1.X - 8, p1.Y - 8, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl = p1;
                path.Dispose();
                return true;
            }
            a = new System.Drawing.Rectangle(p2.X + 4, p1.Y - 8, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl.X = p2.X;
                pointCtrl.Y = p1.Y;
                path.Dispose();
                return true;
            }
            a = new System.Drawing.Rectangle(p1.X - 8, p2.Y + 4, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl.X = p1.X;
                pointCtrl.Y = p2.Y;
                path.Dispose();
                return true;
            }
            a = new System.Drawing.Rectangle(p2.X + 4, p2.Y + 4, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl = p2;
                path.Dispose();
                return true;
            }
            path.Dispose();
            return false;
        }
        public override void showSelect(PaintEventArgs e)
        {
            Brush a = new SolidBrush(this.colorBrush);
            if (!isFill)
            {
                a = new SolidBrush(this.pen.Color);
            }
            e.Graphics.FillRectangle(a, p1.X - 8, p1.Y - 8, 4, 4);
            e.Graphics.FillRectangle(a, p2.X + 4, p1.Y - 8, 4, 4);
            e.Graphics.FillRectangle(a, p1.X - 8, p2.Y + 4, 4, 4);
            e.Graphics.FillRectangle(a, p2.X + 4, p2.Y + 4, 4, 4);
            a.Dispose();
        }
        public void convertPoint()
        {
            if (p1.X > p2.X) { int x = p1.X; p1.X = p2.X; p2.X = x; }
            if (p1.Y > p2.Y) { int x = p1.Y; p1.Y = p2.Y; p2.Y = x; }
        }
        public override bool isHit(Point p)
        {
            bool hit = false;
            convertPoint();
            GraphicsPath path = new GraphicsPath();
            System.Drawing.Rectangle a = new System.Drawing.Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            path.AddRectangle(a);
            if(isFill)
            {
                hit = path.IsVisible(p);
            }
            else
            {
                if (pen.Width < 3)
                {
                    Pen temp = new Pen(pen.Color, 5);
                    hit = path.IsOutlineVisible(p, temp);
                }
                else
                    hit = path.IsOutlineVisible(p, pen);
            }
            path.Dispose();
            return hit;
        }
        public override void Draw(PaintEventArgs e)
        {
            Point start = new Point(p1.X, p1.Y);
            if (p1.X > p2.X && p1.Y < p2.Y) { start = new Point(p2.X, p1.Y); }
            if (p1.X > p2.X && p1.Y > p2.Y) { start = new Point(p2.X, p2.Y); }
            if (p1.X < p2.X && p1.Y > p2.Y) { start = new Point(p1.X, p2.Y); }
            if (isFill == false)
                e.Graphics.DrawRectangle(pen, start.X, start.Y, Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
            else
                e.Graphics.FillRectangle(brush, start.X, start.Y, Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
        }
        ~Rectangle() { }
    }
}
