using System.Drawing;

namespace Bai1GiuaKy
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLine = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.nericWidthPen = new System.Windows.Forms.NumericUpDown();
            this.lblWidthPen = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnPen = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.ptbColor = new System.Windows.Forms.PictureBox();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.plMain = new Bai1GiuaKy.DoubleBufferedPanel();
            this.btnUnGroup = new System.Windows.Forms.Button();
            this.btnCurve = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nericWidthPen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbColor)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(1, 94);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(132, 30);
            this.btnLine.TabIndex = 1;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Location = new System.Drawing.Point(1, 130);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(132, 36);
            this.btnRectangle.TabIndex = 2;
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnEllipse
            // 
            this.btnEllipse.Location = new System.Drawing.Point(1, 172);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(132, 34);
            this.btnEllipse.TabIndex = 6;
            this.btnEllipse.Text = "Ellipse";
            this.btnEllipse.UseVisualStyleBackColor = true;
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // nericWidthPen
            // 
            this.nericWidthPen.Location = new System.Drawing.Point(101, 10);
            this.nericWidthPen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nericWidthPen.Name = "nericWidthPen";
            this.nericWidthPen.Size = new System.Drawing.Size(64, 26);
            this.nericWidthPen.TabIndex = 8;
            this.nericWidthPen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblWidthPen
            // 
            this.lblWidthPen.AutoSize = true;
            this.lblWidthPen.Location = new System.Drawing.Point(13, 12);
            this.lblWidthPen.Name = "lblWidthPen";
            this.lblWidthPen.Size = new System.Drawing.Size(82, 20);
            this.lblWidthPen.TabIndex = 9;
            this.lblWidthPen.Text = "Width Pen";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.Location = new System.Drawing.Point(242, 13);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(46, 20);
            this.lblColor.TabIndex = 10;
            this.lblColor.Text = "Color";
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(1, 212);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(132, 34);
            this.btnCircle.TabIndex = 12;
            this.btnCircle.Text = "Circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnFill
            // 
            this.btnFill.BackColor = System.Drawing.SystemColors.Control;
            this.btnFill.Location = new System.Drawing.Point(72, 47);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(61, 41);
            this.btnFill.TabIndex = 13;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = false;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnPen
            // 
            this.btnPen.Location = new System.Drawing.Point(1, 47);
            this.btnPen.Name = "btnPen";
            this.btnPen.Size = new System.Drawing.Size(65, 41);
            this.btnPen.TabIndex = 14;
            this.btnPen.Text = "Pen";
            this.btnPen.UseVisualStyleBackColor = true;
            this.btnPen.Click += new System.EventHandler(this.btnPen_Click);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(340, 7);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(129, 29);
            this.btnColor.TabIndex = 15;
            this.btnColor.Text = "Choose Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // ptbColor
            // 
            this.ptbColor.Location = new System.Drawing.Point(294, 1);
            this.ptbColor.Name = "ptbColor";
            this.ptbColor.Size = new System.Drawing.Size(40, 40);
            this.ptbColor.TabIndex = 16;
            this.ptbColor.TabStop = false;
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(1, 252);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(132, 34);
            this.btnPolygon.TabIndex = 17;
            this.btnPolygon.Text = "Polygon";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.Location = new System.Drawing.Point(1, 332);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(132, 34);
            this.btnGroup.TabIndex = 18;
            this.btnGroup.Text = "Group";
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // plMain
            // 
            this.plMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plMain.BackColor = System.Drawing.Color.White;
            this.plMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.plMain.Location = new System.Drawing.Point(139, 47);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(636, 391);
            this.plMain.TabIndex = 0;
            this.plMain.Paint += new System.Windows.Forms.PaintEventHandler(this.plMain_Paint);
            this.plMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseDown);
            this.plMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseMove);
            this.plMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseUp);
            this.plMain.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseWheel);
            this.plMain.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.plMain_PreviewKeyDown);
            // 
            // btnUnGroup
            // 
            this.btnUnGroup.Location = new System.Drawing.Point(1, 372);
            this.btnUnGroup.Name = "btnUnGroup";
            this.btnUnGroup.Size = new System.Drawing.Size(132, 34);
            this.btnUnGroup.TabIndex = 20;
            this.btnUnGroup.Text = "UnGroup";
            this.btnUnGroup.UseVisualStyleBackColor = true;
            this.btnUnGroup.Click += new System.EventHandler(this.btnUnGroup_Click);
            // 
            // btnCurve
            // 
            this.btnCurve.Location = new System.Drawing.Point(1, 292);
            this.btnCurve.Name = "btnCurve";
            this.btnCurve.Size = new System.Drawing.Size(132, 34);
            this.btnCurve.TabIndex = 21;
            this.btnCurve.Text = "Curve";
            this.btnCurve.UseVisualStyleBackColor = true;
            this.btnCurve.Click += new System.EventHandler(this.btnCurve_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 452);
            this.Controls.Add(this.btnCurve);
            this.Controls.Add(this.btnUnGroup);
            this.Controls.Add(this.btnGroup);
            this.Controls.Add(this.btnPolygon);
            this.Controls.Add(this.ptbColor);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnPen);
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.btnCircle);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.lblWidthPen);
            this.Controls.Add(this.nericWidthPen);
            this.Controls.Add(this.btnEllipse);
            this.Controls.Add(this.btnRectangle);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.plMain);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nericWidthPen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedPanel plMain;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.NumericUpDown nericWidthPen;
        private System.Windows.Forms.Label lblWidthPen;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnPen;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.PictureBox ptbColor;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.Button btnUnGroup;
        private System.Windows.Forms.Button btnCurve;
    }
}

