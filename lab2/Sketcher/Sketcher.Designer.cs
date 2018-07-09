using Sketcher.Controls;

namespace Sketcher
{
    partial class Sketcher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sketcher));
            this.segmentMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitSegmentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vertexMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteVertexMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletePolygonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.fillMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.unionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DrawArea = new DrawArea();
            this.mTextBox = new System.Windows.Forms.TextBox();
            this.hTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ksTextBox = new System.Windows.Forms.TextBox();
            this.kdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeHeightmapButton = new System.Windows.Forms.Button();
            this.loadHeightmapButton = new System.Windows.Forms.Button();
            this.backgroundGroupBox = new System.Windows.Forms.GroupBox();
            this.removeBackgroundButton = new System.Windows.Forms.Button();
            this.loadBackgroundButton = new System.Windows.Forms.Button();
            this.normalVectorsGroupBox = new System.Windows.Forms.GroupBox();
            this.pyramidNormalVectorButton = new System.Windows.Forms.RadioButton();
            this.trippyCirclesNormalVectorButton = new System.Windows.Forms.RadioButton();
            this.paraboloidNormalVectorButton = new System.Windows.Forms.RadioButton();
            this.plainNormalVectorButton = new System.Windows.Forms.RadioButton();
            this.lightGroupBox = new System.Windows.Forms.GroupBox();
            this.zLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.cycleLabel = new System.Windows.Forms.Label();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.cycleTextBox = new System.Windows.Forms.TextBox();
            this.radiusTextBox = new System.Windows.Forms.TextBox();
            this.lightColorButton = new System.Windows.Forms.Button();
            this.dynamicLightButton = new System.Windows.Forms.RadioButton();
            this.staticInfinityLightButton = new System.Windows.Forms.RadioButton();
            this.segmentMenu.SuspendLayout();
            this.vertexMenu.SuspendLayout();
            this.polygonMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.backgroundGroupBox.SuspendLayout();
            this.normalVectorsGroupBox.SuspendLayout();
            this.lightGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // segmentMenu
            // 
            this.segmentMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.splitSegmentMenuItem});
            this.segmentMenu.Name = "segmentMenu";
            this.segmentMenu.Size = new System.Drawing.Size(98, 26);
            // 
            // splitSegmentMenuItem
            // 
            this.splitSegmentMenuItem.Name = "splitSegmentMenuItem";
            this.splitSegmentMenuItem.Size = new System.Drawing.Size(97, 22);
            this.splitSegmentMenuItem.Text = "Split";
            this.splitSegmentMenuItem.Click += new System.EventHandler(this.splitSegmentMenuItem_Click);
            // 
            // vertexMenu
            // 
            this.vertexMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteVertexMenuItem});
            this.vertexMenu.Name = "vertexMenu";
            this.vertexMenu.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteVertexMenuItem
            // 
            this.deleteVertexMenuItem.Name = "deleteVertexMenuItem";
            this.deleteVertexMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteVertexMenuItem.Text = "Delete";
            this.deleteVertexMenuItem.Click += new System.EventHandler(this.deleteVertexMenuItem_Click);
            // 
            // polygonMenu
            // 
            this.polygonMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePolygonMenuItem,
            this.toolStripSeparator,
            this.fillMenuItem,
            this.selectMenuItem,
            this.toolStripSeparator1,
            this.unionMenuItem});
            this.polygonMenu.Name = "polygonMenu";
            this.polygonMenu.Size = new System.Drawing.Size(119, 104);
            this.polygonMenu.Opening += new System.ComponentModel.CancelEventHandler(this.polygonMenu_Opening);
            // 
            // deletePolygonMenuItem
            // 
            this.deletePolygonMenuItem.Name = "deletePolygonMenuItem";
            this.deletePolygonMenuItem.Size = new System.Drawing.Size(118, 22);
            this.deletePolygonMenuItem.Text = "Delete";
            this.deletePolygonMenuItem.Click += new System.EventHandler(this.deletePolygonMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(115, 6);
            // 
            // fillMenuItem
            // 
            this.fillMenuItem.Name = "fillMenuItem";
            this.fillMenuItem.Size = new System.Drawing.Size(118, 22);
            this.fillMenuItem.Text = "Filled";
            this.fillMenuItem.Click += new System.EventHandler(this.fillPolygonMenuItem_Click);
            // 
            // selectMenuItem
            // 
            this.selectMenuItem.Name = "selectMenuItem";
            this.selectMenuItem.Size = new System.Drawing.Size(118, 22);
            this.selectMenuItem.Text = "Selected";
            this.selectMenuItem.Click += new System.EventHandler(this.selectPolygonMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // unionMenuItem
            // 
            this.unionMenuItem.Name = "unionMenuItem";
            this.unionMenuItem.Size = new System.Drawing.Size(118, 22);
            this.unionMenuItem.Text = "Union";
            this.unionMenuItem.Click += new System.EventHandler(this.unionPolygonMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files|*.bmp;*.png;*.jpg;*.jpeg";
            this.openFileDialog.InitialDirectory = "Sketcher/Images/";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DrawArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.hTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.ksTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.kdTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.backgroundGroupBox);
            this.splitContainer1.Panel2.Controls.Add(this.normalVectorsGroupBox);
            this.splitContainer1.Panel2.Controls.Add(this.lightGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(699, 400);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 4;
            // 
            // DrawArea
            // 
            this.DrawArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawArea.Location = new System.Drawing.Point(0, 0);
            this.DrawArea.Name = "DrawArea";
            this.DrawArea.Size = new System.Drawing.Size(500, 400);
            this.DrawArea.TabIndex = 0;
            this.DrawArea.SizeChanged += new System.EventHandler(this.DrawArea_SizeChanged);
            this.DrawArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawArea_Paint);
            this.DrawArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawArea_KeyDown);
            this.DrawArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseDown);
            this.DrawArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseMove);
            this.DrawArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseUp);
            // 
            // mTextBox
            // 
            this.mTextBox.Location = new System.Drawing.Point(124, 375);
            this.mTextBox.Name = "mTextBox";
            this.mTextBox.Size = new System.Drawing.Size(59, 20);
            this.mTextBox.TabIndex = 11;
            this.mTextBox.Text = "20";
            this.mTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mTextBox_KeyDown);
            // 
            // hTextBox
            // 
            this.hTextBox.Location = new System.Drawing.Point(124, 353);
            this.hTextBox.Name = "hTextBox";
            this.hTextBox.Size = new System.Drawing.Size(59, 20);
            this.hTextBox.TabIndex = 10;
            this.hTextBox.Text = "50";
            this.hTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hTextBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 378);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "h";
            // 
            // ksTextBox
            // 
            this.ksTextBox.Location = new System.Drawing.Point(36, 375);
            this.ksTextBox.Name = "ksTextBox";
            this.ksTextBox.Size = new System.Drawing.Size(56, 20);
            this.ksTextBox.TabIndex = 7;
            this.ksTextBox.Text = "1";
            this.ksTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ksTextBox_KeyDown);
            // 
            // kdTextBox
            // 
            this.kdTextBox.Location = new System.Drawing.Point(36, 353);
            this.kdTextBox.Name = "kdTextBox";
            this.kdTextBox.Size = new System.Drawing.Size(56, 20);
            this.kdTextBox.TabIndex = 6;
            this.kdTextBox.Text = "0";
            this.kdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kdTextBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ks";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kd";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removeHeightmapButton);
            this.groupBox1.Controls.Add(this.loadHeightmapButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 290);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 60);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Heightmap";
            // 
            // removeHeightmapButton
            // 
            this.removeHeightmapButton.Location = new System.Drawing.Point(104, 19);
            this.removeHeightmapButton.Name = "removeHeightmapButton";
            this.removeHeightmapButton.Size = new System.Drawing.Size(80, 30);
            this.removeHeightmapButton.TabIndex = 1;
            this.removeHeightmapButton.Text = "Remove";
            this.removeHeightmapButton.UseVisualStyleBackColor = true;
            this.removeHeightmapButton.Click += new System.EventHandler(this.removeHeightmapButton_Click);
            // 
            // loadHeightmapButton
            // 
            this.loadHeightmapButton.Location = new System.Drawing.Point(12, 19);
            this.loadHeightmapButton.Name = "loadHeightmapButton";
            this.loadHeightmapButton.Size = new System.Drawing.Size(80, 30);
            this.loadHeightmapButton.TabIndex = 0;
            this.loadHeightmapButton.Text = "Load";
            this.loadHeightmapButton.UseVisualStyleBackColor = true;
            this.loadHeightmapButton.Click += new System.EventHandler(this.loadHeightmapButton_Click);
            // 
            // backgroundGroupBox
            // 
            this.backgroundGroupBox.Controls.Add(this.removeBackgroundButton);
            this.backgroundGroupBox.Controls.Add(this.loadBackgroundButton);
            this.backgroundGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.backgroundGroupBox.Location = new System.Drawing.Point(0, 230);
            this.backgroundGroupBox.Name = "backgroundGroupBox";
            this.backgroundGroupBox.Size = new System.Drawing.Size(195, 60);
            this.backgroundGroupBox.TabIndex = 2;
            this.backgroundGroupBox.TabStop = false;
            this.backgroundGroupBox.Text = "Background";
            // 
            // removeBackgroundButton
            // 
            this.removeBackgroundButton.Location = new System.Drawing.Point(104, 19);
            this.removeBackgroundButton.Name = "removeBackgroundButton";
            this.removeBackgroundButton.Size = new System.Drawing.Size(80, 30);
            this.removeBackgroundButton.TabIndex = 1;
            this.removeBackgroundButton.Text = "Remove";
            this.removeBackgroundButton.UseVisualStyleBackColor = true;
            this.removeBackgroundButton.Click += new System.EventHandler(this.removeBackgroundButton_Click);
            // 
            // loadBackgroundButton
            // 
            this.loadBackgroundButton.Location = new System.Drawing.Point(12, 19);
            this.loadBackgroundButton.Name = "loadBackgroundButton";
            this.loadBackgroundButton.Size = new System.Drawing.Size(80, 30);
            this.loadBackgroundButton.TabIndex = 0;
            this.loadBackgroundButton.Text = "Load";
            this.loadBackgroundButton.UseVisualStyleBackColor = true;
            this.loadBackgroundButton.Click += new System.EventHandler(this.loadBackgroundButton_Click);
            // 
            // normalVectorsGroupBox
            // 
            this.normalVectorsGroupBox.Controls.Add(this.pyramidNormalVectorButton);
            this.normalVectorsGroupBox.Controls.Add(this.trippyCirclesNormalVectorButton);
            this.normalVectorsGroupBox.Controls.Add(this.paraboloidNormalVectorButton);
            this.normalVectorsGroupBox.Controls.Add(this.plainNormalVectorButton);
            this.normalVectorsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.normalVectorsGroupBox.Location = new System.Drawing.Point(0, 160);
            this.normalVectorsGroupBox.Name = "normalVectorsGroupBox";
            this.normalVectorsGroupBox.Size = new System.Drawing.Size(195, 70);
            this.normalVectorsGroupBox.TabIndex = 1;
            this.normalVectorsGroupBox.TabStop = false;
            this.normalVectorsGroupBox.Text = "Normal vectors";
            // 
            // pyramidNormalVectorButton
            // 
            this.pyramidNormalVectorButton.AutoSize = true;
            this.pyramidNormalVectorButton.Location = new System.Drawing.Point(96, 42);
            this.pyramidNormalVectorButton.Name = "pyramidNormalVectorButton";
            this.pyramidNormalVectorButton.Size = new System.Drawing.Size(62, 17);
            this.pyramidNormalVectorButton.TabIndex = 3;
            this.pyramidNormalVectorButton.Text = "Pyramid";
            this.pyramidNormalVectorButton.UseVisualStyleBackColor = true;
            this.pyramidNormalVectorButton.CheckedChanged += new System.EventHandler(this.pyramidNormalVectorButton_CheckedChanged);
            // 
            // trippyCirclesNormalVectorButton
            // 
            this.trippyCirclesNormalVectorButton.AutoSize = true;
            this.trippyCirclesNormalVectorButton.Location = new System.Drawing.Point(96, 19);
            this.trippyCirclesNormalVectorButton.Name = "trippyCirclesNormalVectorButton";
            this.trippyCirclesNormalVectorButton.Size = new System.Drawing.Size(87, 17);
            this.trippyCirclesNormalVectorButton.TabIndex = 2;
            this.trippyCirclesNormalVectorButton.Text = "Trippy circles";
            this.trippyCirclesNormalVectorButton.UseVisualStyleBackColor = true;
            this.trippyCirclesNormalVectorButton.CheckedChanged += new System.EventHandler(this.trippyCirclesNormalVectorButton_CheckedChanged);
            // 
            // paraboloidNormalVectorButton
            // 
            this.paraboloidNormalVectorButton.AutoSize = true;
            this.paraboloidNormalVectorButton.Location = new System.Drawing.Point(12, 42);
            this.paraboloidNormalVectorButton.Name = "paraboloidNormalVectorButton";
            this.paraboloidNormalVectorButton.Size = new System.Drawing.Size(75, 17);
            this.paraboloidNormalVectorButton.TabIndex = 1;
            this.paraboloidNormalVectorButton.Text = "Paraboloid";
            this.paraboloidNormalVectorButton.UseVisualStyleBackColor = true;
            this.paraboloidNormalVectorButton.CheckedChanged += new System.EventHandler(this.paraboloidNormalVectorButton_CheckedChanged);
            // 
            // plainNormalVectorButton
            // 
            this.plainNormalVectorButton.AutoSize = true;
            this.plainNormalVectorButton.Checked = true;
            this.plainNormalVectorButton.Location = new System.Drawing.Point(12, 19);
            this.plainNormalVectorButton.Name = "plainNormalVectorButton";
            this.plainNormalVectorButton.Size = new System.Drawing.Size(48, 17);
            this.plainNormalVectorButton.TabIndex = 0;
            this.plainNormalVectorButton.TabStop = true;
            this.plainNormalVectorButton.Text = "Plain";
            this.plainNormalVectorButton.UseVisualStyleBackColor = true;
            this.plainNormalVectorButton.CheckedChanged += new System.EventHandler(this.plainNormalVectorButton_CheckedChanged);
            // 
            // lightGroupBox
            // 
            this.lightGroupBox.Controls.Add(this.zLabel);
            this.lightGroupBox.Controls.Add(this.yLabel);
            this.lightGroupBox.Controls.Add(this.xLabel);
            this.lightGroupBox.Controls.Add(this.cycleLabel);
            this.lightGroupBox.Controls.Add(this.radiusLabel);
            this.lightGroupBox.Controls.Add(this.zTextBox);
            this.lightGroupBox.Controls.Add(this.yTextBox);
            this.lightGroupBox.Controls.Add(this.xTextBox);
            this.lightGroupBox.Controls.Add(this.cycleTextBox);
            this.lightGroupBox.Controls.Add(this.radiusTextBox);
            this.lightGroupBox.Controls.Add(this.lightColorButton);
            this.lightGroupBox.Controls.Add(this.dynamicLightButton);
            this.lightGroupBox.Controls.Add(this.staticInfinityLightButton);
            this.lightGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.lightGroupBox.Location = new System.Drawing.Point(0, 0);
            this.lightGroupBox.Name = "lightGroupBox";
            this.lightGroupBox.Size = new System.Drawing.Size(195, 160);
            this.lightGroupBox.TabIndex = 0;
            this.lightGroupBox.TabStop = false;
            this.lightGroupBox.Text = "Light";
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(128, 112);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(15, 13);
            this.zLabel.TabIndex = 12;
            this.zLabel.Text = " z";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(68, 112);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(15, 13);
            this.yLabel.TabIndex = 11;
            this.yLabel.Text = " y";
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(9, 112);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(15, 13);
            this.xLabel.TabIndex = 10;
            this.xLabel.Text = " x";
            // 
            // cycleLabel
            // 
            this.cycleLabel.AutoSize = true;
            this.cycleLabel.Location = new System.Drawing.Point(101, 73);
            this.cycleLabel.Name = "cycleLabel";
            this.cycleLabel.Size = new System.Drawing.Size(33, 13);
            this.cycleLabel.TabIndex = 9;
            this.cycleLabel.Text = "Cycle";
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(9, 73);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(40, 13);
            this.radiusLabel.TabIndex = 8;
            this.radiusLabel.Text = "Radius";
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(131, 128);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(53, 20);
            this.zTextBox.TabIndex = 7;
            this.zTextBox.Text = "50";
            this.zTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lightTextBox_KeyDown);
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(71, 128);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(54, 20);
            this.yTextBox.TabIndex = 6;
            this.yTextBox.Text = "200";
            this.yTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lightTextBox_KeyDown);
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(12, 128);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(53, 20);
            this.xTextBox.TabIndex = 5;
            this.xTextBox.Text = "250";
            this.xTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lightTextBox_KeyDown);
            // 
            // cycleTextBox
            // 
            this.cycleTextBox.Location = new System.Drawing.Point(104, 89);
            this.cycleTextBox.Name = "cycleTextBox";
            this.cycleTextBox.Size = new System.Drawing.Size(80, 20);
            this.cycleTextBox.TabIndex = 4;
            this.cycleTextBox.Text = "5000";
            this.cycleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lightTextBox_KeyDown);
            // 
            // radiusTextBox
            // 
            this.radiusTextBox.Location = new System.Drawing.Point(12, 89);
            this.radiusTextBox.Name = "radiusTextBox";
            this.radiusTextBox.Size = new System.Drawing.Size(80, 20);
            this.radiusTextBox.TabIndex = 3;
            this.radiusTextBox.Text = "100";
            this.radiusTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lightTextBox_KeyDown);
            // 
            // lightColorButton
            // 
            this.lightColorButton.Location = new System.Drawing.Point(104, 19);
            this.lightColorButton.Name = "lightColorButton";
            this.lightColorButton.Size = new System.Drawing.Size(80, 30);
            this.lightColorButton.TabIndex = 2;
            this.lightColorButton.UseVisualStyleBackColor = true;
            this.lightColorButton.Click += new System.EventHandler(this.lightColorButton_Click);
            // 
            // dynamicLightButton
            // 
            this.dynamicLightButton.AutoSize = true;
            this.dynamicLightButton.Location = new System.Drawing.Point(12, 42);
            this.dynamicLightButton.Name = "dynamicLightButton";
            this.dynamicLightButton.Size = new System.Drawing.Size(66, 17);
            this.dynamicLightButton.TabIndex = 1;
            this.dynamicLightButton.Text = "Dynamic";
            this.dynamicLightButton.UseVisualStyleBackColor = true;
            this.dynamicLightButton.CheckedChanged += new System.EventHandler(this.dynamicLightButton_CheckedChanged);
            // 
            // staticInfinityLightButton
            // 
            this.staticInfinityLightButton.AutoSize = true;
            this.staticInfinityLightButton.Checked = true;
            this.staticInfinityLightButton.Location = new System.Drawing.Point(12, 19);
            this.staticInfinityLightButton.Name = "staticInfinityLightButton";
            this.staticInfinityLightButton.Size = new System.Drawing.Size(84, 17);
            this.staticInfinityLightButton.TabIndex = 0;
            this.staticInfinityLightButton.TabStop = true;
            this.staticInfinityLightButton.Text = "Static infinity";
            this.staticInfinityLightButton.UseVisualStyleBackColor = true;
            this.staticInfinityLightButton.CheckedChanged += new System.EventHandler(this.staticInfinityLightButton_CheckedChanged);
            // 
            // Sketcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 400);
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sketcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sketcher";
            this.segmentMenu.ResumeLayout(false);
            this.vertexMenu.ResumeLayout(false);
            this.polygonMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.backgroundGroupBox.ResumeLayout(false);
            this.normalVectorsGroupBox.ResumeLayout(false);
            this.normalVectorsGroupBox.PerformLayout();
            this.lightGroupBox.ResumeLayout(false);
            this.lightGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip segmentMenu;
        private System.Windows.Forms.ContextMenuStrip vertexMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitSegmentMenuItem;
        private System.Windows.Forms.ContextMenuStrip polygonMenu;
        private System.Windows.Forms.ToolStripMenuItem fillMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePolygonMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem selectMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DrawArea DrawArea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox backgroundGroupBox;
        private System.Windows.Forms.GroupBox normalVectorsGroupBox;
        private System.Windows.Forms.GroupBox lightGroupBox;
        private System.Windows.Forms.Button removeHeightmapButton;
        private System.Windows.Forms.Button loadHeightmapButton;
        private System.Windows.Forms.Button removeBackgroundButton;
        private System.Windows.Forms.Button loadBackgroundButton;
        private System.Windows.Forms.RadioButton pyramidNormalVectorButton;
        private System.Windows.Forms.RadioButton trippyCirclesNormalVectorButton;
        private System.Windows.Forms.RadioButton paraboloidNormalVectorButton;
        private System.Windows.Forms.RadioButton plainNormalVectorButton;
        private System.Windows.Forms.TextBox cycleTextBox;
        private System.Windows.Forms.TextBox radiusTextBox;
        private System.Windows.Forms.Button lightColorButton;
        private System.Windows.Forms.RadioButton dynamicLightButton;
        private System.Windows.Forms.RadioButton staticInfinityLightButton;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label cycleLabel;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.TextBox zTextBox;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mTextBox;
        private System.Windows.Forms.TextBox hTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ksTextBox;
        private System.Windows.Forms.TextBox kdTextBox;
    }
}

