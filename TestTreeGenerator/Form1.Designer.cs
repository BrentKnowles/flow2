namespace TestTreeGenerator
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tipColor = new System.Windows.Forms.ToolTip(this.components);
            this.lblFontColor = new System.Windows.Forms.Label();
            this.lblBoxFillColor = new System.Windows.Forms.Label();
            this.lblLineColor = new System.Windows.Forms.Label();
            this.lblBGColor = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.nudBoxWidth = new System.Windows.Forms.NumericUpDown();
            this.nudBoxHeight = new System.Windows.Forms.NumericUpDown();
            this.nudMargin = new System.Windows.Forms.NumericUpDown();
            this.nudHorizontalSpace = new System.Windows.Forms.NumericUpDown();
            this.nudVerticalSpace = new System.Windows.Forms.NumericUpDown();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.nudLineWidth = new System.Windows.Forms.NumericUpDown();
            this.picTree = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShowChart = new System.Windows.Forms.Button();
            this.lblNodeText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.helpBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxOverridePerson = new System.Windows.Forms.CheckBox();
            this.checkBoxCategory = new System.Windows.Forms.CheckBox();
            this.buttonDuties = new System.Windows.Forms.Button();
            this.textBoxDuties = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPeople = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.datafilelabel = new System.Windows.Forms.ToolStripLabel();
            this.savedata = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelStyleSheet = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudBoxWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBoxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHorizontalSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTree)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFontColor.Location = new System.Drawing.Point(33, 442);
            this.lblFontColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(83, 20);
            this.lblFontColor.TabIndex = 2;
            this.lblFontColor.Text = "Font Color";
            this.tipColor.SetToolTip(this.lblFontColor, "Font color");
            this.lblFontColor.Click += new System.EventHandler(this.lblFontColor_Click);
            // 
            // lblBoxFillColor
            // 
            this.lblBoxFillColor.AutoSize = true;
            this.lblBoxFillColor.Location = new System.Drawing.Point(33, 475);
            this.lblBoxFillColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBoxFillColor.Name = "lblBoxFillColor";
            this.lblBoxFillColor.Size = new System.Drawing.Size(69, 20);
            this.lblBoxFillColor.TabIndex = 2;
            this.lblBoxFillColor.Text = "Fill Color";
            this.tipColor.SetToolTip(this.lblBoxFillColor, "Fill color");
            this.lblBoxFillColor.Click += new System.EventHandler(this.lblBoxFillColor_Click);
            // 
            // lblLineColor
            // 
            this.lblLineColor.AutoSize = true;
            this.lblLineColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLineColor.Location = new System.Drawing.Point(33, 512);
            this.lblLineColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLineColor.Name = "lblLineColor";
            this.lblLineColor.Size = new System.Drawing.Size(80, 20);
            this.lblLineColor.TabIndex = 2;
            this.lblLineColor.Text = "Line Color";
            this.tipColor.SetToolTip(this.lblLineColor, "Line color");
            this.lblLineColor.Click += new System.EventHandler(this.lblLineColor_Click);
            // 
            // lblBGColor
            // 
            this.lblBGColor.AutoSize = true;
            this.lblBGColor.Location = new System.Drawing.Point(33, 548);
            this.lblBGColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBGColor.Name = "lblBGColor";
            this.lblBGColor.Size = new System.Drawing.Size(74, 20);
            this.lblBGColor.TabIndex = 2;
            this.lblBGColor.Text = "BG Color";
            this.tipColor.SetToolTip(this.lblBGColor, "BG Color");
            this.lblBGColor.Click += new System.EventHandler(this.lblBGColor_Click);
            // 
            // nudBoxWidth
            // 
            this.nudBoxWidth.Location = new System.Drawing.Point(166, 392);
            this.nudBoxWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBoxWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBoxWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudBoxWidth.Name = "nudBoxWidth";
            this.nudBoxWidth.Size = new System.Drawing.Size(93, 26);
            this.nudBoxWidth.TabIndex = 5;
            this.nudBoxWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudBoxWidth.ValueChanged += new System.EventHandler(this.nudBoxWidth_ValueChanged);
            // 
            // nudBoxHeight
            // 
            this.nudBoxHeight.Location = new System.Drawing.Point(166, 352);
            this.nudBoxHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBoxHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBoxHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudBoxHeight.Name = "nudBoxHeight";
            this.nudBoxHeight.Size = new System.Drawing.Size(93, 26);
            this.nudBoxHeight.TabIndex = 5;
            this.nudBoxHeight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudBoxHeight.ValueChanged += new System.EventHandler(this.nudBoxHeight_ValueChanged);
            // 
            // nudMargin
            // 
            this.nudMargin.Location = new System.Drawing.Point(166, 312);
            this.nudMargin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudMargin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMargin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMargin.Name = "nudMargin";
            this.nudMargin.Size = new System.Drawing.Size(93, 26);
            this.nudMargin.TabIndex = 5;
            this.nudMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMargin.ValueChanged += new System.EventHandler(this.nudMargin_ValueChanged);
            // 
            // nudHorizontalSpace
            // 
            this.nudHorizontalSpace.Location = new System.Drawing.Point(166, 272);
            this.nudHorizontalSpace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudHorizontalSpace.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHorizontalSpace.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHorizontalSpace.Name = "nudHorizontalSpace";
            this.nudHorizontalSpace.Size = new System.Drawing.Size(93, 26);
            this.nudHorizontalSpace.TabIndex = 5;
            this.nudHorizontalSpace.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHorizontalSpace.ValueChanged += new System.EventHandler(this.nudHorizontalSpace_ValueChanged);
            // 
            // nudVerticalSpace
            // 
            this.nudVerticalSpace.Location = new System.Drawing.Point(166, 232);
            this.nudVerticalSpace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudVerticalSpace.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudVerticalSpace.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVerticalSpace.Name = "nudVerticalSpace";
            this.nudVerticalSpace.Size = new System.Drawing.Size(93, 26);
            this.nudVerticalSpace.TabIndex = 5;
            this.nudVerticalSpace.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVerticalSpace.ValueChanged += new System.EventHandler(this.nudVerticalSpace_ValueChanged);
            // 
            // nudFontSize
            // 
            this.nudFontSize.Location = new System.Drawing.Point(166, 192);
            this.nudFontSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudFontSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(93, 26);
            this.nudFontSize.TabIndex = 5;
            this.nudFontSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // nudLineWidth
            // 
            this.nudLineWidth.Location = new System.Drawing.Point(166, 152);
            this.nudLineWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudLineWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineWidth.Name = "nudLineWidth";
            this.nudLineWidth.Size = new System.Drawing.Size(93, 26);
            this.nudLineWidth.TabIndex = 5;
            this.nudLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineWidth.ValueChanged += new System.EventHandler(this.nudLineWidth_ValueChanged);
            // 
            // picTree
            // 
            this.picTree.Location = new System.Drawing.Point(4, 5);
            this.picTree.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picTree.Name = "picTree";
            this.picTree.Size = new System.Drawing.Size(639, 410);
            this.picTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTree.TabIndex = 4;
            this.picTree.TabStop = false;
            this.picTree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picOrgChart_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 395);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Box Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 355);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Box Height";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 315);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Margin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 275);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Horizontal Space";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 235);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Vertical Space";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 195);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Font Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 155);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Line width";
            // 
            // btnShowChart
            // 
            this.btnShowChart.Location = new System.Drawing.Point(4, 5);
            this.btnShowChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowChart.Name = "btnShowChart";
            this.btnShowChart.Size = new System.Drawing.Size(135, 35);
            this.btnShowChart.TabIndex = 0;
            this.btnShowChart.Text = "Show Org Chart";
            this.btnShowChart.UseVisualStyleBackColor = true;
            this.btnShowChart.Click += new System.EventHandler(this.btnShowChart_Click);
            // 
            // lblNodeText
            // 
            this.lblNodeText.AutoSize = true;
            this.lblNodeText.Location = new System.Drawing.Point(38, 628);
            this.lblNodeText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodeText.Name = "lblNodeText";
            this.lblNodeText.Size = new System.Drawing.Size(0, 20);
            this.lblNodeText.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.helpBox);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkBoxOverridePerson);
            this.panel1.Controls.Add(this.checkBoxCategory);
            this.panel1.Controls.Add(this.buttonDuties);
            this.panel1.Controls.Add(this.textBoxDuties);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBoxPeople);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btnShowChart);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblNodeText);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.nudBoxWidth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nudBoxHeight);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nudMargin);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.nudHorizontalSpace);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.nudVerticalSpace);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.nudFontSize);
            this.panel1.Controls.Add(this.lblFontColor);
            this.panel1.Controls.Add(this.nudLineWidth);
            this.panel1.Controls.Add(this.lblBoxFillColor);
            this.panel1.Controls.Add(this.lblLineColor);
            this.panel1.Controls.Add(this.lblBGColor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 1081);
            this.panel1.TabIndex = 8;
            // 
            // helpBox
            // 
            this.helpBox.Location = new System.Drawing.Point(30, 889);
            this.helpBox.Multiline = true;
            this.helpBox.Name = "helpBox";
            this.helpBox.Size = new System.Drawing.Size(229, 135);
            this.helpBox.TabIndex = 19;
            this.helpBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 802);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 41);
            this.button2.TabIndex = 18;
            this.button2.Text = "color to clipboard int";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 711);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 61);
            this.button1.TabIndex = 17;
            this.button1.Text = "Font";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxOverridePerson
            // 
            this.checkBoxOverridePerson.AutoSize = true;
            this.checkBoxOverridePerson.Location = new System.Drawing.Point(27, 98);
            this.checkBoxOverridePerson.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxOverridePerson.Name = "checkBoxOverridePerson";
            this.checkBoxOverridePerson.Size = new System.Drawing.Size(270, 24);
            this.checkBoxOverridePerson.TabIndex = 16;
            this.checkBoxOverridePerson.Text = "Show Current Person Doing Task";
            this.checkBoxOverridePerson.UseVisualStyleBackColor = true;
            this.checkBoxOverridePerson.CheckedChanged += new System.EventHandler(this.checkBoxOverridePerson_CheckedChanged);
            // 
            // checkBoxCategory
            // 
            this.checkBoxCategory.AutoSize = true;
            this.checkBoxCategory.Location = new System.Drawing.Point(27, 63);
            this.checkBoxCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxCategory.Name = "checkBoxCategory";
            this.checkBoxCategory.Size = new System.Drawing.Size(143, 24);
            this.checkBoxCategory.TabIndex = 15;
            this.checkBoxCategory.Text = "Show Category";
            this.checkBoxCategory.UseVisualStyleBackColor = true;
            this.checkBoxCategory.CheckedChanged += new System.EventHandler(this.checkBoxCategory_CheckedChanged);
            // 
            // buttonDuties
            // 
            this.buttonDuties.Location = new System.Drawing.Point(141, 5);
            this.buttonDuties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDuties.Name = "buttonDuties";
            this.buttonDuties.Size = new System.Drawing.Size(135, 35);
            this.buttonDuties.TabIndex = 14;
            this.buttonDuties.Text = "Show Duties";
            this.buttonDuties.UseVisualStyleBackColor = true;
            this.buttonDuties.Click += new System.EventHandler(this.buttonDuties_Click);
            // 
            // textBoxDuties
            // 
            this.textBoxDuties.Location = new System.Drawing.Point(76, 652);
            this.textBoxDuties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDuties.Multiline = true;
            this.textBoxDuties.Name = "textBoxDuties";
            this.textBoxDuties.Size = new System.Drawing.Size(246, 56);
            this.textBoxDuties.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 713);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "DUTIES";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 688);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "PEOPLE";
            // 
            // textBoxPeople
            // 
            this.textBoxPeople.Location = new System.Drawing.Point(46, 625);
            this.textBoxPeople.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPeople.Multiline = true;
            this.textBoxPeople.Name = "textBoxPeople";
            this.textBoxPeople.Size = new System.Drawing.Size(246, 61);
            this.textBoxPeople.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 577);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "ROLES";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(30, 597);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(246, 76);
            this.textBox1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.picTree);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1537, 1042);
            this.panel2.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2842, 2246);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 20);
            this.label11.TabIndex = 5;
            this.label11.Text = "label11";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(308, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1551, 1081);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1543, 1048);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.toolStrip1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1543, 1048);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 35);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1537, 1010);
            this.dataGridView1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.datafilelabel,
            this.savedata});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1537, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(53, 29);
            this.toolStripButton1.Text = "Save";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(55, 29);
            this.toolStripButton2.Text = "Load";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // datafilelabel
            // 
            this.datafilelabel.Name = "datafilelabel";
            this.datafilelabel.Size = new System.Drawing.Size(131, 29);
            this.datafilelabel.Text = "toolStripLabel1";
            // 
            // savedata
            // 
            this.savedata.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.savedata.Image = ((System.Drawing.Image)(resources.GetObject("savedata.Image")));
            this.savedata.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savedata.Name = "savedata";
            this.savedata.Size = new System.Drawing.Size(147, 29);
            this.savedata.Text = "Save Current File";
            this.savedata.Click += new System.EventHandler(this.savedata_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.labelStyleSheet);
            this.tabPage3.Controls.Add(this.toolStrip2);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1543, 1048);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Look and Feel Data";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // labelStyleSheet
            // 
            this.labelStyleSheet.AutoSize = true;
            this.labelStyleSheet.Location = new System.Drawing.Point(438, 3);
            this.labelStyleSheet.Name = "labelStyleSheet";
            this.labelStyleSheet.Size = new System.Drawing.Size(60, 20);
            this.labelStyleSheet.TabIndex = 3;
            this.labelStyleSheet.Text = "label12";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1537, 32);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(78, 29);
            this.toolStripButton3.Text = "Save As";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(55, 29);
            this.toolStripButton4.Text = "Load";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(140, 29);
            this.toolStripButton5.Text = "Reload Formats";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(53, 29);
            this.toolStripButton6.Text = "Save";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(1537, 1042);
            this.dataGridView2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1859, 1081);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudBoxWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBoxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHorizontalSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTree)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip tipColor;
        private System.Windows.Forms.Label lblBGColor;
        private System.Windows.Forms.Label lblLineColor;
        private System.Windows.Forms.Label lblBoxFillColor;
        private System.Windows.Forms.Label lblFontColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown nudBoxWidth;
        private System.Windows.Forms.NumericUpDown nudBoxHeight;
        private System.Windows.Forms.NumericUpDown nudMargin;
        private System.Windows.Forms.NumericUpDown nudHorizontalSpace;
        private System.Windows.Forms.NumericUpDown nudVerticalSpace;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.NumericUpDown nudLineWidth;
        private System.Windows.Forms.PictureBox picTree;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowChart;
        private System.Windows.Forms.Label lblNodeText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPeople;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDuties;
        private System.Windows.Forms.TextBox textBoxDuties;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxOverridePerson;
        private System.Windows.Forms.CheckBox checkBoxCategory;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.Label labelStyleSheet;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox helpBox;
        private System.Windows.Forms.ToolStripLabel datafilelabel;
        private System.Windows.Forms.ToolStripButton savedata;
    }
}

