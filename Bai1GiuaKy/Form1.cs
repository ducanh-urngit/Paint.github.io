using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bai1GiuaKy.Object;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Bai1GiuaKy
{
    public partial class Form1 : Form
    {
        // biến cho biết đang chọn chế độ tô màu hay không
        bool bFill = false;
        // biến cho biết đang chọn vẽ đường thẳng
        bool bLine = false;
        // biến cho biết đang chọ vẽ hình chữ nhật
        bool bRectangle = false;
        // biến cho biết đang chọn vẽ hình elip
        bool bEllipse = false;
        // biến cho biết đang chọn vẽ hình tròn
        bool bCircle = false;
        // biến cho biết đang chọn chế độ vẽ polygon, curve
        bool bPolygon = false;
        bool bCurve = false;
        // biến cho biết có đang trong quá trình vẽ polygon, curve hay không
        bool bIsDrawingPolygon = false;
        bool bIsDrawingCurve = false;
        // biến cho biết điểm được chọn trong quá trình vẽ polygon, curve
        bool pointCertain = false;
        // biến cho biết đang giữ chuột để vẽ hình hay không
        bool isPress = false;

        List<DrawObject> ListObject = new List<DrawObject>();
        public Form1()
        {
            InitializeComponent();
            this.btnPen.BackColor = Color.Red;
            this.ptbColor.BackColor = Color.Black;
        }

        private void plMain_Paint(object sender, PaintEventArgs e)
        {
            this.plMain.Width =  this.Size.Width - this.plMain.Location.X;
            this.plMain.Height = this.Size.Height - this.plMain.Location.Y;
            for (int i = 0; i < ListObject.Count; i++)
            {
                ListObject[i].Draw(e);
                if(ListObject[i].isSelected)
                {
                    ListObject[i].showSelect(e);
                }
            }
            this.plMain.Focus();
        }
        private void plMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (bLine)
            {
                isPress = true;
                Color color = ptbColor.BackColor;
                int chooseWidthPen = (int)nericWidthPen.Value;
                Pen pen = new Pen(color, chooseWidthPen);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                Line myObj = new Line(pen,chooseWidthPen);
                myObj.p1 = e.Location;
                ListObject.Add(myObj);
                return;
            }
            if (bRectangle)
            {
                isPress = true;
                Color color = ptbColor.BackColor;
                int chooseWidthPen = (int)nericWidthPen.Value;
                if (!bFill)
                {
                    Pen pen = new Pen(color, chooseWidthPen);
                    Bai1GiuaKy.Object.Rectangle myObj = new Bai1GiuaKy.Object.Rectangle(pen, bFill,chooseWidthPen);
                    myObj.p1 = e.Location;
                    ListObject.Add(myObj);
                }
                else
                {
                    Brush brush = new SolidBrush(color);
                    Bai1GiuaKy.Object.Rectangle myObj = new Bai1GiuaKy.Object.Rectangle(brush, bFill, color);
                    myObj.p1 = e.Location;
                    ListObject.Add(myObj);
                }
                return;
            }
            if (bEllipse || bCircle)
            {
                isPress = true;
                Color color = ptbColor.BackColor;
                int chooseWidthPen = (int)nericWidthPen.Value;
                DrawObject myObj;
                if (!bFill)
                {
                    Pen pen = new Pen(color, chooseWidthPen);
                    if (bEllipse)
                        myObj = new Ellipse(pen, bFill);
                    else
                        myObj = new Circle(pen,bFill);
                    myObj.p1 = e.Location;
                    ListObject.Add(myObj);
                }
                else
                {
                    Brush brush = new SolidBrush(color);
                    if (bEllipse)
                        myObj = new Ellipse(brush,bFill,color);
                    else
                        myObj = new Circle(brush,bFill,color);
                    myObj.p1 = e.Location;
                    ListObject.Add(myObj);
                }
                return;
            }
            if (bPolygon)
            {
                if (bIsDrawingPolygon)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        bPolygon = false;
                        this.bIsDrawingPolygon = false;
                        return;
                    }
                    pointCertain = true;
                    ListObject[ListObject.Count - 1].insertPoint(e.Location, pointCertain);
                    plMain.Refresh();
                }
                else
                {
                    pointCertain = false;
                    isPress = true;
                    Color color = ptbColor.BackColor;
                    int chooseWidthPen = (int)nericWidthPen.Value;
                    if (!bFill)
                    {
                        Pen pen = new Pen(color, chooseWidthPen);
                        Polygon myObj = new Polygon(pen, bFill);
                        myObj.insertPoint(e.Location, pointCertain);
                        ListObject.Add(myObj);
                    }
                    else
                    {
                        Brush brush = new SolidBrush(color);
                        Polygon myObj = new Polygon(brush, bFill, color);
                        myObj.insertPoint(e.Location, pointCertain);
                        ListObject.Add(myObj);
                    }
                    this.bIsDrawingPolygon = true;
                }
                return;
            }
            if(bCurve)
            {
                if (bIsDrawingCurve)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        bCurve = false;
                        this.bIsDrawingCurve = false;
                        return;
                    }
                    pointCertain = true;
                    ListObject[ListObject.Count - 1].insertPoint(e.Location, pointCertain);
                    plMain.Refresh();
                }
                else
                {
                    pointCertain = false;
                    isPress = true;
                    Color color = ptbColor.BackColor;
                    int chooseWidthPen = (int)nericWidthPen.Value;
                    if (!bFill)
                    {
                        Pen pen = new Pen(color, chooseWidthPen);
                        Curve myObj = new Curve(pen, bFill);
                        myObj.insertPoint(e.Location, pointCertain);
                        ListObject.Add(myObj);
                    }
                    else
                    {
                        Brush brush = new SolidBrush(color);
                        Curve myObj = new Curve(brush, bFill, color);
                        myObj.insertPoint(e.Location, pointCertain);
                        ListObject.Add(myObj);
                    }
                    this.bIsDrawingCurve = true;
                }
                return;
            }
            for (int i = 0; i < ListObject.Count; i++)
            {
                if (ListObject[i].isSelected == true)
                {
                    if (ListObject[i].isPointCtrl(e.Location))
                    {
                        ListObject[i].isResize = true;
                        ListObject[i].isMoved = false;
                        plMain.Cursor = System.Windows.Forms.Cursors.SizeAll;
                        return;
                    }
                }
            }
            if (Control.ModifierKeys.HasFlag(Keys.Control) == false)
                offAllSelected();
            int count = 0;
            for (int i = 0; i < ListObject.Count; i++)
            {
                if (ListObject[i].isHit(e.Location))
                {
                    if(count < 1)
                    {
                        ListObject[i].isSelected = true;
                        ListObject[i].isMoved = true;
                        count++;
                    }
                    ListObject[i].isMoving = false;
                    plMain.Cursor = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    if (Control.ModifierKeys.HasFlag(Keys.Control) == false)
                        ListObject[i].isSelected = false;
                }
                plMain.Refresh();
            }
        }

        private void plMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPress)
            {
                if (!bPolygon && !bCurve)
                {
                    ListObject[ListObject.Count - 1].p2 = e.Location;
                    plMain.Refresh();
                }
                else
                {
                    pointCertain = false;
                    ListObject[ListObject.Count - 1].insertPoint(e.Location, pointCertain);
                    plMain.Refresh();
                }
                return;
            }
            
            foreach (DrawObject Object in ListObject)
            {
                if (Object.isMoved)
                    Object.Move(e.Location);

                else if (Object.isResize)
                    Object.reSize(e.Location);

                plMain.Refresh();
            }
        }
        private void plMain_MouseUp(object sender, MouseEventArgs e)
        {
            if(bPolygon || bCurve)
            {
                pointCertain = true;
            }
            else
            {
                isPress = false;
                offAllObject();
                for(int i = 0; i < ListObject.Count; i++)
                {
                    ListObject[i].isMoved = false;
                    ListObject[i].isMoving = false;
                    ListObject[i].isResize = false;
                    if (ListObject[i] is Group)
                    {
                        ListObject[i].offAllStatusGroup();
                    }
                }
            }
            plMain.Refresh();
        }
        private void plMain_MouseWheel(object sender, MouseEventArgs e)
        {
            foreach (DrawObject obj in ListObject)
            {
                if (obj.isSelected)
                {
                    if (e.Delta > 0)
                    {
                        obj.ZoomIn();
                    }
                    else
                    {
                        obj.ZoomOut();
                    }
                    plMain.Refresh();
                }
            }
        }
        private void plMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                for (int i = 0; i < ListObject.Count; i++)
                {
                    if (ListObject[i].isSelected)
                    {
                        ListObject.RemoveAt(i);
                        plMain.Refresh();
                        i--;
                    }
                }
            }
        }
        public void offAllSelected()
        {
            for (int i = 0; i < ListObject.Count; i++)
            {
                ListObject[i].isSelected = false;
            }
        }
        public void offAllObject()
        {
            bLine = false;
            bRectangle = false;
            bEllipse = false;
            bCircle = false;
            bPolygon = false;
            bCurve = false;
            plMain.Cursor = System.Windows.Forms.Cursors.Default;
        }
        
        private void btnFill_Click(object sender, EventArgs e)
        {
            this.bFill = true;
            btnFill.BackColor = Color.Red;
            btnPen.BackColor = System.Drawing.SystemColors.Control;
            offAllSelected();
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            this.bFill = false;
            btnPen.BackColor = Color.Red;
            btnFill.BackColor = System.Drawing.SystemColors.Control;
            offAllSelected();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.ptbColor.BackColor = colorDialog.Color;    
            }
            offAllSelected();
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            bLine = true;
            offAllSelected();
            plMain.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            bRectangle = true;
            offAllSelected();
            plMain.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            bEllipse = true;
            offAllSelected();
            plMain.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        private void btnCircle_Click(object sender, EventArgs e)
        {
            bCircle = true;
            offAllSelected();
            plMain.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        private void btnPolygon_Click(object sender, EventArgs e)
        {
            this.bPolygon = true;
            this.bIsDrawingPolygon = false;
            offAllSelected();
            plMain.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        private void btnCurve_Click(object sender, EventArgs e)
        {
            bCurve = true;
            bIsDrawingCurve = false;
            offAllSelected();
            plMain.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        public int selectedQuantity()
        {
            int count = 0;
            foreach(DrawObject obj in ListObject)
            {
                if (obj.isSelected)
                    count++;
            }
            return count;
        }
        private void btnGroup_Click(object sender, EventArgs e)
        {
            if(selectedQuantity() > 1)
            {
                Group groupObj = new Group();
                for(int i = 0; i < ListObject.Count; i++)
                {
                    if (ListObject[i].isSelected)
                    {
                        groupObj.addObj(ListObject[i]);
                        ListObject.RemoveAt(i);
                        i--;
                    }
                }
                groupObj.isSelected = true;
                ListObject.Add(groupObj);
            }
        }
        private void btnUnGroup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListObject.Count; i++)
            {
                if (ListObject[i].isSelected)
                {
                    if (ListObject[i] is Group)
                    {
                        while (ListObject[i].isUnGroup == false)
                        {
                            ListObject.Add(ListObject[i].UnGroup());
                        }
                        ListObject.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        
        ~Form1() { }
    }
}
