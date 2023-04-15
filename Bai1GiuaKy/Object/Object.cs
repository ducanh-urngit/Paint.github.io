using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1GiuaKy.Object
{
    public class DrawObject
    {
        // điểm đầu trong hình
        public Point p1;
        // điểm cuối trong hình
        public Point p2;
        // bút được dùng để vẽ 
        public Pen pen;
        // cọ để tô hình
        public Brush brush;
        // màu của cọ
        public Color colorBrush;
        // cho biết có ở chế độ tô hay không
        public bool isFill;
        // độ rộng nét vẽ
        public int widthPen;
        // cho biết hình được chọn hay không
        public bool isSelected = false;
        // cho biết hình được chọn cho di chuyển hay không
        public bool isMoved = false;
        // cho biết hình có đang trong quá trình di chuyển hay không
        public bool isMoving = false;
        public bool isUnGroup = false;
        // cho biết hình có đang được thay đổi kích thước không
        public bool isResize = false;
        // chiều rộng của p1 p2
        public int distanceWidth;
        // chiều cao của p1 p2
        public int distanceHeight;
        // chiều rộng của p1 với điểm đứng của chuột
        public int tempWidth;
        // chiều cao của p1 với điểm đứng của chuột
        public int tempHeight;
        // điểm được giữ để thay đổi kích thước
        public Point pointCtrl;

        public virtual void ZoomOut()
        { }
        public virtual void ZoomIn()
        { }
        public virtual int GetCount()
        { return 0; }
        public virtual void ChangePointWithRatioHeight(Point p1, float h, List<float> ratioH)
        { }
        public virtual void ChangePointWithRatioWidth(Point p1, float w, List<float> ratioW)
        { }
        public virtual List<float> GetRatioHeight(Point p1, Point p2)
        { return null; }
        public virtual List<float> GetRatioWidth(Point p1, Point p2)
        { return null; }
        public virtual void offAllStatusGroup()
        { }
        public virtual DrawObject UnGroup()
        { return null; }
        public virtual void reSize(Point p)
        { }
        public virtual bool isPointCtrl(Point p)
        { return false; }
        public virtual void Move(Point p)
        { }
        public virtual void showSelect(PaintEventArgs e)
        { }
        public virtual bool isHit(Point p)
        { return false; }
        public virtual void insertPoint(Point p, bool okPolygon)
        { }
        public virtual void Draw(PaintEventArgs e) { }
        ~DrawObject() { }
    }
}
