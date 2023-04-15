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
    internal class Curve:DrawObject
    {
        List<Point> points;
        // danh sách chiều rộng giữa các điểm
        List<int> distancePointsX;
        // đanh sách chiều cao giữa các điểm
        List<int> distancePointsY;
        public Curve(Pen pen, bool isFill)
        {
            this.pen = pen;
            points = new List<Point>();
            this.isFill = isFill;
            distancePointsX = new List<int>();
            distancePointsY = new List<int>();
        }
        public Curve(Brush brush, bool isFill, Color colorBrush)
        {
            this.brush = brush;
            this.isFill = isFill;
            points = new List<Point>();
            this.colorBrush = colorBrush;
            distancePointsX = new List<int>();
            distancePointsY = new List<int>();
        }
        public override void ChangePointWithRatioHeight(Point p1, float h, List<float> ratioH)
        {
            Point temp;
            for (int i = 0; i < points.Count; i++)
            {
                temp = points[i];
                temp.Y = p1.Y + (int)(ratioH[i] * h / (1 + ratioH[i]));
                points[i] = temp;
            }
        }
        public override void ChangePointWithRatioWidth(Point p1, float w, List<float> ratioW)
        {
            Point temp;
            for (int i = 0; i < points.Count; i++)
            {
                temp = points[i];
                temp.X = p1.X + (int)(ratioW[i] * w / (1 + ratioW[i]));
                points[i] = temp;
            }
        }
        public override List<float> GetRatioWidth(Point p1, Point p2)
        {
            List<float> temp = new List<float>();
            for (int i = 0; i < points.Count; i++)
            {
                if (p2.X - points[i].X == 0)
                    temp.Add(((float)points[i].X - (float)p1.X) / ((float)p2.X - ((float)points[i].X - (float)0.01)));
                else
                    temp.Add(((float)points[i].X - (float)p1.X) / ((float)p2.X - (float)points[i].X));
            }
            return temp;
        }
        public override List<float> GetRatioHeight(Point p1, Point p2)
        {
            List<float> temp = new List<float>();
            for (int i = 0; i < points.Count; i++)
            {
                if (p2.Y - points[i].Y == 0)
                    temp.Add(((float)points[i].Y - (float)p1.Y) / ((float)p2.Y - ((float)points[i].Y - (float)0.01)));
                else
                    temp.Add(((float)points[i].Y - (float)p1.Y) / ((float)p2.Y - (float)points[i].Y));
            }
            return temp;
        }
        public override void ZoomOut()
        {
            if (!isFill)
            {
                if (pen.Width > 1)
                    pen.Width--;
                else
                    return;
            }
            setDistancePoints();
            for (int i = 0; i < distancePointsX.Count; i++)
            {
                if (distancePointsX[i] < 0)
                    distancePointsX[i] += 10;
                else
                    distancePointsX[i] -= 10;
                if (distancePointsY[i] < 0)
                    distancePointsY[i] += 10;
                else
                    distancePointsY[i] -= 10;
            }
            Point[] tempPoints = points.ToArray();
            points[0] = tempPoints[0];
            appDistancepoints(tempPoints);
            convertPoint();
        }
        public override void ZoomIn()
        {
            if (!isFill)
                pen.Width++;
            setDistancePoints();
            for (int i = 0; i < distancePointsX.Count; i++)
            {
                if (distancePointsX[i] < 0)
                    distancePointsX[i] -= 10;
                else
                    distancePointsX[i] += 10;
                if (distancePointsY[i] < 0)
                    distancePointsY[i] -= 10;
                else
                    distancePointsY[i] += 10;
            }
            Point[] tempPoints = points.ToArray();
            points[0] = tempPoints[0];
            appDistancepoints(tempPoints);
            convertPoint();

        }
        public override int GetCount()
        {
            return points.Count();
        }
        public override void reSize(Point p)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] == pointCtrl)
                {
                    points[i] = p;
                    pointCtrl = points[i];
                    return;
                }
            }
        }
        public override bool isPointCtrl(Point p)
        {
            GraphicsPath path = new GraphicsPath();
            System.Drawing.Rectangle a;
            for (int i = 0; i < points.Count; i++)
            {
                if (!isFill)
                {
                    if (pen.Width > 3)
                    {
                        a = new System.Drawing.Rectangle(points[i].X - (int)pen.Width, points[i].Y - (int)pen.Width, 2 * (int)pen.Width, 2 * (int)pen.Width);
                    }
                    else
                    {
                        a = new System.Drawing.Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
                    }
                }
                else
                    a = new System.Drawing.Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
                path.AddRectangle(a);
                if (path.IsVisible(p))
                {
                    pointCtrl = points[i];
                    path.Dispose();
                    return true;
                }
            }
            path.Dispose();
            return false;
        }
        public override void Move(Point p)
        {
            if (isMoving == false)
            {
                this.isMoving = true;
                this.tempWidth = p.X - points[0].X;
                this.tempHeight = p.Y - points[0].Y;
                setDistancePoints();
            }
            else
            {
                Point[] tempPoints = points.ToArray();
                tempPoints[0].X = p.X - tempWidth;
                tempPoints[0].Y = p.Y - tempHeight;
                points[0] = tempPoints[0];
                appDistancepoints(tempPoints);
                convertPoint();
            }
        }
        public void appDistancepoints(Point[] tempPoints)
        {
            for (int i = 1; i < points.Count; i++)
            {
                tempPoints[i].X = tempPoints[i - 1].X + distancePointsX[i - 1];
                tempPoints[i].Y = tempPoints[i - 1].Y + distancePointsY[i - 1];
                points[i] = tempPoints[i];
            }
        }
        public void setDistancePoints()
        {
            distancePointsX.Clear();
            distancePointsY.Clear();
            for (int i = 1; i < points.Count; i++)
            {
                distancePointsX.Add(points[i].X - points[i - 1].X);
                distancePointsY.Add(points[i].Y - points[i - 1].Y);
            }
        }
        public override void showSelect(PaintEventArgs e)
        {
            Brush a = new SolidBrush(this.colorBrush);
            if (!isFill)
            {
                a = new SolidBrush(this.pen.Color);
            }
            for (int i = 0; i < points.Count; i++)
            {
                if (!isFill)
                {
                    if (pen.Width > 3)
                    {
                        e.Graphics.FillRectangle(a, points[i].X - pen.Width, points[i].Y - pen.Width, 2 * pen.Width, 2 * pen.Width);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(a, points[i].X - 4, points[i].Y - 4, 8, 8);
                    }
                }
                else
                    e.Graphics.FillRectangle(a, points[i].X - 4, points[i].Y - 4, 8, 8);
            }
            a.Dispose();
        }
        public override bool isHit(Point p)
        {
            bool hit = false;
            GraphicsPath path = new GraphicsPath();
            path.AddCurve(points.ToArray());
            if (isFill)
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
        public void convertPoint()
        {
            p1 = points[0];
            p2 = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                if (p1.X > points[i].X)
                    p1.X = points[i].X;
                if (p1.Y > points[i].Y)
                    p1.Y = points[i].Y;
                if (p2.X < points[i].X)
                    p2.X = points[i].X;
                if (p2.Y < points[i].Y)
                    p2.Y = points[i].Y;
            }
        }
        public override void insertPoint(Point p, bool pointCertain)
        {
            if (points.Count < 2)
            {
                points.Add(p);
            }
            else
            {
                if (pointCertain)
                {
                    points.Add(p);
                    convertPoint();
                }
                else
                    points[points.Count - 1] = p;
            }
        }
        public override void Draw(PaintEventArgs e)
        {
            if (points.Count > 1)
            {
                if (isFill == false)
                    e.Graphics.DrawCurve(pen, points.ToArray());
                else
                    e.Graphics.FillClosedCurve(brush, points.ToArray());
            }
        }
        ~Curve() { }
    }
}
