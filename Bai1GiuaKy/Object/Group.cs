using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1GiuaKy.Object
{
    internal class Group : DrawObject
    {
        List<DrawObject> groups;
        List<float> ratioWidth;
        List<float> ratioHeight;
        public Group()
        {
            groups = new List<DrawObject>();
            ratioWidth = new List<float>();
            ratioHeight = new List<float>();
        }
        public override void offAllStatusGroup()
        {
            foreach(DrawObject obj in groups)
            {
                obj.isMoved = false;
                obj.isMoving = false;
                obj.isResize = false;
            }
        }
        public override DrawObject UnGroup()
        {
            DrawObject temp = new DrawObject();
            if(groups.Count > 1)
            {
                temp = groups[0];
                groups.RemoveAt(0);
                return temp;
            }
            isUnGroup = true;
            temp = groups[0];
            groups.RemoveAt(0);
            return temp;
        }
        public override void reSize(Point p)
        {
            if (pointCtrl == p1)
            {
                p1 = p;
                pointCtrl = p1;
            }
            else if (pointCtrl.X == p2.X && pointCtrl.Y == p1.Y)
            {
                p2.X = p.X;
                p1.Y = p.Y;
                pointCtrl.X = p2.X;
                pointCtrl.Y = p1.Y;
            }
            else if (pointCtrl.X == p1.X && pointCtrl.Y == p2.Y)
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
            ChangePointObj();
        }
        public void ChangePointObj()
        {
            int d = 0, c = 0;
            float w = (float)p2.X - p1.X;
            float h = (float)p2.Y - p1.Y;
            List<float> a = new List<float>();
            List<float> b = new List<float>();
            foreach (DrawObject obj in groups)
            {
                if(obj is Polygon || obj is Curve)
                {
                    a.Clear();
                    b.Clear();
                    c = obj.GetCount();
                    for (int i = 0; i < c; i++)
                    {
                        a.Add(ratioWidth[d]);
                        b.Add(ratioHeight[d]);
                        d++;
                    }
                    obj.ChangePointWithRatioWidth(p1, w, a);
                    obj.ChangePointWithRatioHeight(p1, h, b);
                }
                else
                {
                    a.Clear();
                    a.Add(ratioWidth[d]);
                    a.Add(ratioWidth[d + 1]);
                    obj.ChangePointWithRatioWidth(p1, w, a);

                    a.Clear();
                    a.Add(ratioHeight[d]);
                    a.Add(ratioHeight[d + 1]);
                    obj.ChangePointWithRatioHeight(p1, h, a);
                    d += 2;
                }
            }
        }
        public override void ZoomOut()
        {
            foreach (DrawObject obj in groups)
                obj.ZoomOut();
            convertPoint();
        }
        public override void ZoomIn()
        {
            foreach (DrawObject obj in groups)
                obj.ZoomIn();
            convertPoint();

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
                getRatio();
                return true;
            }
            a = new System.Drawing.Rectangle(p2.X + 4, p1.Y - 8, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl.X = p2.X;
                pointCtrl.Y = p1.Y;
                path.Dispose();
                getRatio();
                return true;
            }
            a = new System.Drawing.Rectangle(p1.X - 8, p2.Y + 4, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl.X = p1.X;
                pointCtrl.Y = p2.Y;
                path.Dispose();
                getRatio();
                return true;
            }
            a = new System.Drawing.Rectangle(p2.X + 4, p2.Y + 4, 4, 4);
            path.AddRectangle(a);
            if (path.IsVisible(p))
            {
                pointCtrl = p2;
                path.Dispose();
                getRatio();
                return true;
            }
            path.Dispose();
            return false;
        }
        public void getRatio()
        {
            ratioWidth.Clear();
            ratioHeight.Clear();
            foreach(DrawObject obj in groups)
            {
                ratioWidth.AddRange(obj.GetRatioWidth(p1, p2));
                ratioHeight.AddRange(obj.GetRatioHeight(p1, p2));
            }
        }
        public override void showSelect(PaintEventArgs e)
        {
            Brush a = new SolidBrush(Color.Black);
            e.Graphics.FillRectangle(a, p1.X - 8, p1.Y - 8, 4, 4);
            e.Graphics.FillRectangle(a, p2.X + 4, p1.Y - 8, 4, 4);
            e.Graphics.FillRectangle(a, p1.X - 8, p2.Y + 4, 4, 4);
            e.Graphics.FillRectangle(a, p2.X + 4, p2.Y + 4, 4, 4);
            a.Dispose();
        }
        public override void Move(Point p)
        {
            for(int i = 0; i < groups.Count; i++)
            {
                groups[i].Move(p);
            }
            convertPoint();
        }
        public override bool isHit(Point p)
        {
            convertPoint();
            foreach (DrawObject obj in groups)
            {
                if (obj.isHit(p))
                    return true;
            }
            return false;
        }
        public void convertPoint()
        {
            int minX = groups[0].p1.X , minY = groups[0].p1.Y, maxX = groups[0].p1.X, maxY = groups[0].p1.Y;
            for(int i = 0; i < groups.Count; i++)
            {
                if (minX > groups[i].p1.X)
                    minX = groups[i].p1.X;
                if (minY > groups[i].p1.Y)
                    minY = groups[i].p1.Y;
                if (maxX < groups[i].p2.X)
                    maxX = groups[i].p2.X;
                if (maxY < groups[i].p2.Y)
                    maxY = groups[i].p2.Y;
            }
            p1.X = minX; p1.Y = minY;
            p2.X = maxX; p2.Y = maxY;
        }
        public override void Draw(PaintEventArgs e)
        {
            foreach (DrawObject obj in groups)
            {
                obj.Draw(e);
            }
        }
        public void addObj(DrawObject obj)
        {
            if(obj is Group == false)
                groups.Add(obj);
            else
            {
                while (obj.isUnGroup == false)
                    groups.Add(obj.UnGroup());
            }
            convertPoint();
        }
        ~Group() { }
    }
}
