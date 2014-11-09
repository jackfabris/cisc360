namespace WindowsFormsApplication2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.accLabel = new System.Windows.Forms.Label();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.nextInstructionButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.zeroLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.aLabel = new System.Windows.Forms.Label();
            this.bLabel = new System.Windows.Forms.Label();
            this.oneLabel = new System.Windows.Forms.Label();
            this.pcLabel = new System.Windows.Forms.Label();
            this.marLabel = new System.Windows.Forms.Label();
            this.mdrLabel = new System.Windows.Forms.Label();
            this.tempLabel = new System.Windows.Forms.Label();
            this.irLabel = new System.Windows.Forms.Label();
            this.ccLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.runToEndButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.resetCPUButton = new System.Windows.Forms.Button();
            this.instructionIndexLabel = new System.Windows.Forms.Label();
            this.nextInstructionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hitMissLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.hitCountLabel = new System.Windows.Forms.Label();
            this.missCountLabel = new System.Windows.Forms.Label();
            this.cacheTypeDropdown = new System.Windows.Forms.ComboBox();
            this.cacheSizeDropdown = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.blockSizeDropdown = new System.Windows.Forms.ComboBox();
            this.applyCacheButton = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.storeLabel = new System.Windows.Forms.Label();
            this.executeLabel = new System.Windows.Forms.Label();
            this.decodeLabel = new System.Windows.Forms.Label();
            this.fetchLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Acc";
            // 
            // accLabel
            // 
            this.accLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accLabel.Location = new System.Drawing.Point(96, 114);
            this.accLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.accLabel.Name = "accLabel";
            this.accLabel.Size = new System.Drawing.Size(137, 19);
            this.accLabel.TabIndex = 1;
            this.accLabel.Text = "0x00000000";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(236, 19);
            this.loadFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(83, 32);
            this.loadFileButton.TabIndex = 2;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // nextInstructionButton
            // 
            this.nextInstructionButton.Location = new System.Drawing.Point(236, 282);
            this.nextInstructionButton.Margin = new System.Windows.Forms.Padding(2);
            this.nextInstructionButton.Name = "nextInstructionButton";
            this.nextInstructionButton.Size = new System.Drawing.Size(83, 33);
            this.nextInstructionButton.TabIndex = 3;
            this.nextInstructionButton.Text = "Next";
            this.nextInstructionButton.UseVisualStyleBackColor = true;
            this.nextInstructionButton.Click += new System.EventHandler(this.nextInstructionButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Zero";
            // 
            // zeroLabel
            // 
            this.zeroLabel.AutoSize = true;
            this.zeroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zeroLabel.Location = new System.Drawing.Point(96, 131);
            this.zeroLabel.Name = "zeroLabel";
            this.zeroLabel.Size = new System.Drawing.Size(86, 17);
            this.zeroLabel.TabIndex = 5;
            this.zeroLabel.Text = "0x00000000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "One";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "PC";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "MAR";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "MDR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "TEMP";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "IR";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(17, 249);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 17);
            this.label12.TabIndex = 14;
            this.label12.Text = "CC";
            // 
            // aLabel
            // 
            this.aLabel.AutoSize = true;
            this.aLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aLabel.Location = new System.Drawing.Point(96, 80);
            this.aLabel.Name = "aLabel";
            this.aLabel.Size = new System.Drawing.Size(86, 17);
            this.aLabel.TabIndex = 15;
            this.aLabel.Text = "0x00000000";
            // 
            // bLabel
            // 
            this.bLabel.AutoSize = true;
            this.bLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bLabel.Location = new System.Drawing.Point(96, 97);
            this.bLabel.Name = "bLabel";
            this.bLabel.Size = new System.Drawing.Size(86, 17);
            this.bLabel.TabIndex = 16;
            this.bLabel.Text = "0x00000000";
            // 
            // oneLabel
            // 
            this.oneLabel.AutoSize = true;
            this.oneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneLabel.Location = new System.Drawing.Point(96, 148);
            this.oneLabel.Name = "oneLabel";
            this.oneLabel.Size = new System.Drawing.Size(86, 17);
            this.oneLabel.TabIndex = 17;
            this.oneLabel.Text = "0x00000001";
            // 
            // pcLabel
            // 
            this.pcLabel.AutoSize = true;
            this.pcLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcLabel.Location = new System.Drawing.Point(96, 165);
            this.pcLabel.Name = "pcLabel";
            this.pcLabel.Size = new System.Drawing.Size(86, 17);
            this.pcLabel.TabIndex = 18;
            this.pcLabel.Text = "0x00000000";
            // 
            // marLabel
            // 
            this.marLabel.AutoSize = true;
            this.marLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marLabel.Location = new System.Drawing.Point(96, 182);
            this.marLabel.Name = "marLabel";
            this.marLabel.Size = new System.Drawing.Size(86, 17);
            this.marLabel.TabIndex = 19;
            this.marLabel.Text = "0x00000000";
            // 
            // mdrLabel
            // 
            this.mdrLabel.AutoSize = true;
            this.mdrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdrLabel.Location = new System.Drawing.Point(96, 199);
            this.mdrLabel.Name = "mdrLabel";
            this.mdrLabel.Size = new System.Drawing.Size(86, 17);
            this.mdrLabel.TabIndex = 20;
            this.mdrLabel.Text = "0x00000000";
            // 
            // tempLabel
            // 
            this.tempLabel.AutoSize = true;
            this.tempLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tempLabel.Location = new System.Drawing.Point(96, 216);
            this.tempLabel.Name = "tempLabel";
            this.tempLabel.Size = new System.Drawing.Size(86, 17);
            this.tempLabel.TabIndex = 21;
            this.tempLabel.Text = "0x00000000";
            // 
            // irLabel
            // 
            this.irLabel.AutoSize = true;
            this.irLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.irLabel.Location = new System.Drawing.Point(96, 232);
            this.irLabel.Name = "irLabel";
            this.irLabel.Size = new System.Drawing.Size(86, 17);
            this.irLabel.TabIndex = 22;
            this.irLabel.Text = "0x00000000";
            // 
            // ccLabel
            // 
            this.ccLabel.AutoSize = true;
            this.ccLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ccLabel.Location = new System.Drawing.Point(96, 249);
            this.ccLabel.Name = "ccLabel";
            this.ccLabel.Size = new System.Drawing.Size(86, 17);
            this.ccLabel.TabIndex = 23;
            this.ccLabel.Text = "0x00000000";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(15, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(130, 20);
            this.label22.TabIndex = 24;
            this.label22.Text = "Gemini Simulator";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(17, 63);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(69, 17);
            this.label23.TabIndex = 25;
            this.label23.Text = "Register";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(96, 63);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 17);
            this.label24.TabIndex = 26;
            this.label24.Text = "Value";
            // 
            // runToEndButton
            // 
            this.runToEndButton.Location = new System.Drawing.Point(324, 282);
            this.runToEndButton.Name = "runToEndButton";
            this.runToEndButton.Size = new System.Drawing.Size(85, 33);
            this.runToEndButton.TabIndex = 27;
            this.runToEndButton.Text = "Run To End";
            this.runToEndButton.UseVisualStyleBackColor = true;
            this.runToEndButton.Click += new System.EventHandler(this.runToEndButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(236, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(17, 282);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(99, 15);
            this.label25.TabIndex = 29;
            this.label25.Text = "Instruction Index:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(17, 300);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(94, 15);
            this.label26.TabIndex = 30;
            this.label26.Text = "Next Instruction:";
            // 
            // resetCPUButton
            // 
            this.resetCPUButton.Location = new System.Drawing.Point(324, 19);
            this.resetCPUButton.Name = "resetCPUButton";
            this.resetCPUButton.Size = new System.Drawing.Size(82, 32);
            this.resetCPUButton.TabIndex = 32;
            this.resetCPUButton.Text = "Reset CPU";
            this.resetCPUButton.UseVisualStyleBackColor = true;
            this.resetCPUButton.Click += new System.EventHandler(this.resetCPUButton_Click);
            // 
            // instructionIndexLabel
            // 
            this.instructionIndexLabel.AutoSize = true;
            this.instructionIndexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionIndexLabel.Location = new System.Drawing.Point(122, 282);
            this.instructionIndexLabel.Name = "instructionIndexLabel";
            this.instructionIndexLabel.Size = new System.Drawing.Size(14, 15);
            this.instructionIndexLabel.TabIndex = 33;
            this.instructionIndexLabel.Text = "0";
            // 
            // nextInstructionLabel
            // 
            this.nextInstructionLabel.AutoSize = true;
            this.nextInstructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextInstructionLabel.Location = new System.Drawing.Point(122, 300);
            this.nextInstructionLabel.Name = "nextInstructionLabel";
            this.nextInstructionLabel.Size = new System.Drawing.Size(25, 15);
            this.nextInstructionLabel.TabIndex = 34;
            this.nextInstructionLabel.Text = "- - -";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 35;
            this.label3.Text = "Cache:";
            // 
            // hitMissLabel
            // 
            this.hitMissLabel.AutoSize = true;
            this.hitMissLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hitMissLabel.Location = new System.Drawing.Point(121, 336);
            this.hitMissLabel.Name = "hitMissLabel";
            this.hitMissLabel.Size = new System.Drawing.Size(25, 15);
            this.hitMissLabel.TabIndex = 36;
            this.hitMissLabel.Text = "- - -";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 353);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 15);
            this.label13.TabIndex = 37;
            this.label13.Text = "Total Hits:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(16, 370);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 15);
            this.label14.TabIndex = 38;
            this.label14.Text = "Total Misses:";
            // 
            // hitCountLabel
            // 
            this.hitCountLabel.AutoSize = true;
            this.hitCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hitCountLabel.Location = new System.Drawing.Point(121, 353);
            this.hitCountLabel.Name = "hitCountLabel";
            this.hitCountLabel.Size = new System.Drawing.Size(14, 15);
            this.hitCountLabel.TabIndex = 39;
            this.hitCountLabel.Text = "0";
            // 
            // missCountLabel
            // 
            this.missCountLabel.AutoSize = true;
            this.missCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missCountLabel.Location = new System.Drawing.Point(121, 370);
            this.missCountLabel.Name = "missCountLabel";
            this.missCountLabel.Size = new System.Drawing.Size(14, 15);
            this.missCountLabel.TabIndex = 40;
            this.missCountLabel.Text = "0";
            // 
            // cacheTypeDropdown
            // 
            this.cacheTypeDropdown.DisplayMember = "Direct";
            this.cacheTypeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cacheTypeDropdown.FormattingEnabled = true;
            this.cacheTypeDropdown.Items.AddRange(new object[] {
            "Direct Mapped",
            "2-Way Set Associative"});
            this.cacheTypeDropdown.Location = new System.Drawing.Point(285, 321);
            this.cacheTypeDropdown.Name = "cacheTypeDropdown";
            this.cacheTypeDropdown.Size = new System.Drawing.Size(121, 21);
            this.cacheTypeDropdown.TabIndex = 41;
            // 
            // cacheSizeDropdown
            // 
            this.cacheSizeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cacheSizeDropdown.FormattingEnabled = true;
            this.cacheSizeDropdown.Items.AddRange(new object[] {
            "2",
            "8",
            "16"});
            this.cacheSizeDropdown.Location = new System.Drawing.Point(285, 345);
            this.cacheSizeDropdown.Name = "cacheSizeDropdown";
            this.cacheSizeDropdown.Size = new System.Drawing.Size(121, 21);
            this.cacheSizeDropdown.TabIndex = 42;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(205, 322);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 15);
            this.label15.TabIndex = 43;
            this.label15.Text = "Cache Type:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(205, 346);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 15);
            this.label16.TabIndex = 44;
            this.label16.Text = "Cache Size:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(205, 370);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 15);
            this.label17.TabIndex = 45;
            this.label17.Text = "Block Size:";
            // 
            // blockSizeDropdown
            // 
            this.blockSizeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blockSizeDropdown.FormattingEnabled = true;
            this.blockSizeDropdown.Items.AddRange(new object[] {
            "1",
            "2"});
            this.blockSizeDropdown.Location = new System.Drawing.Point(285, 369);
            this.blockSizeDropdown.Name = "blockSizeDropdown";
            this.blockSizeDropdown.Size = new System.Drawing.Size(121, 21);
            this.blockSizeDropdown.TabIndex = 46;
            // 
            // applyCacheButton
            // 
            this.applyCacheButton.Location = new System.Drawing.Point(285, 394);
            this.applyCacheButton.Name = "applyCacheButton";
            this.applyCacheButton.Size = new System.Drawing.Size(121, 32);
            this.applyCacheButton.TabIndex = 47;
            this.applyCacheButton.Text = "Apply Cache Settings";
            this.applyCacheButton.UseVisualStyleBackColor = true;
            this.applyCacheButton.Click += new System.EventHandler(this.applyCacheButton_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(16, 446);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 15);
            this.label18.TabIndex = 48;
            this.label18.Text = "Fetch:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(16, 431);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 15);
            this.label19.TabIndex = 49;
            this.label19.Text = "Decode:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(16, 416);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 15);
            this.label20.TabIndex = 50;
            this.label20.Text = "Execute:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(16, 401);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 15);
            this.label21.TabIndex = 51;
            this.label21.Text = "Store:";
            // 
            // storeLabel
            // 
            this.storeLabel.AutoSize = true;
            this.storeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeLabel.Location = new System.Drawing.Point(124, 401);
            this.storeLabel.Name = "storeLabel";
            this.storeLabel.Size = new System.Drawing.Size(25, 15);
            this.storeLabel.TabIndex = 52;
            this.storeLabel.Text = "- - -";
            // 
            // executeLabel
            // 
            this.executeLabel.AutoSize = true;
            this.executeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executeLabel.Location = new System.Drawing.Point(124, 416);
            this.executeLabel.Name = "executeLabel";
            this.executeLabel.Size = new System.Drawing.Size(25, 15);
            this.executeLabel.TabIndex = 53;
            this.executeLabel.Text = "- - -";
            // 
            // decodeLabel
            // 
            this.decodeLabel.AutoSize = true;
            this.decodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decodeLabel.Location = new System.Drawing.Point(124, 431);
            this.decodeLabel.Name = "decodeLabel";
            this.decodeLabel.Size = new System.Drawing.Size(25, 15);
            this.decodeLabel.TabIndex = 54;
            this.decodeLabel.Text = "- - -";
            // 
            // fetchLabel
            // 
            this.fetchLabel.AutoSize = true;
            this.fetchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fetchLabel.Location = new System.Drawing.Point(124, 446);
            this.fetchLabel.Name = "fetchLabel";
            this.fetchLabel.Size = new System.Drawing.Size(25, 15);
            this.fetchLabel.TabIndex = 55;
            this.fetchLabel.Text = "- - -";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 483);
            this.Controls.Add(this.fetchLabel);
            this.Controls.Add(this.decodeLabel);
            this.Controls.Add(this.executeLabel);
            this.Controls.Add(this.storeLabel);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.applyCacheButton);
            this.Controls.Add(this.blockSizeDropdown);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cacheSizeDropdown);
            this.Controls.Add(this.cacheTypeDropdown);
            this.Controls.Add(this.missCountLabel);
            this.Controls.Add(this.hitCountLabel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.hitMissLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nextInstructionLabel);
            this.Controls.Add(this.instructionIndexLabel);
            this.Controls.Add(this.resetCPUButton);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.runToEndButton);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.ccLabel);
            this.Controls.Add(this.irLabel);
            this.Controls.Add(this.tempLabel);
            this.Controls.Add(this.mdrLabel);
            this.Controls.Add(this.marLabel);
            this.Controls.Add(this.pcLabel);
            this.Controls.Add(this.oneLabel);
            this.Controls.Add(this.bLabel);
            this.Controls.Add(this.aLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zeroLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nextInstructionButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.accLabel);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Gemini";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label accLabel;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button nextInstructionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label zeroLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label aLabel;
        private System.Windows.Forms.Label bLabel;
        private System.Windows.Forms.Label oneLabel;
        private System.Windows.Forms.Label pcLabel;
        private System.Windows.Forms.Label marLabel;
        private System.Windows.Forms.Label mdrLabel;
        private System.Windows.Forms.Label tempLabel;
        private System.Windows.Forms.Label irLabel;
        private System.Windows.Forms.Label ccLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button runToEndButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button resetCPUButton;
        private System.Windows.Forms.Label instructionIndexLabel;
        private System.Windows.Forms.Label nextInstructionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label hitMissLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label hitCountLabel;
        private System.Windows.Forms.Label missCountLabel;
        private System.Windows.Forms.ComboBox cacheTypeDropdown;
        private System.Windows.Forms.ComboBox cacheSizeDropdown;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox blockSizeDropdown;
        private System.Windows.Forms.Button applyCacheButton;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label storeLabel;
        private System.Windows.Forms.Label executeLabel;
        private System.Windows.Forms.Label decodeLabel;
        private System.Windows.Forms.Label fetchLabel;
    }
}

