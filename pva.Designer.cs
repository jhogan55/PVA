namespace PVAfront
{
    partial class pva
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
            this.chkDepInf = new System.Windows.Forms.CheckBox();
            this.grpInf = new System.Windows.Forms.GroupBox();
            this.txtInfAge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdoMale = new System.Windows.Forms.RadioButton();
            this.rdoFemale = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.lstPop = new System.Windows.Forms.ListBox();
            this.txtStartPop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnEditRates = new System.Windows.Forms.Button();
            this.txtYears = new System.Windows.Forms.TextBox();
            this.txtTrials = new System.Windows.Forms.TextBox();
            this.lblYears = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDefaultStart = new System.Windows.Forms.Button();
            this.lblRunning = new System.Windows.Forms.Label();
            this.grpInf.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkDepInf
            // 
            this.chkDepInf.AutoSize = true;
            this.chkDepInf.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDepInf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chkDepInf.Location = new System.Drawing.Point(164, 101);
            this.chkDepInf.Margin = new System.Windows.Forms.Padding(2);
            this.chkDepInf.Name = "chkDepInf";
            this.chkDepInf.Size = new System.Drawing.Size(154, 24);
            this.chkDepInf.TabIndex = 3;
            this.chkDepInf.Text = "Dependent Infant";
            this.chkDepInf.UseVisualStyleBackColor = true;
            this.chkDepInf.CheckedChanged += new System.EventHandler(this.chkDepInf_CheckedChanged);
            // 
            // grpInf
            // 
            this.grpInf.BackColor = System.Drawing.SystemColors.Info;
            this.grpInf.Controls.Add(this.txtInfAge);
            this.grpInf.Controls.Add(this.label3);
            this.grpInf.Controls.Add(this.rdoMale);
            this.grpInf.Controls.Add(this.rdoFemale);
            this.grpInf.Enabled = false;
            this.grpInf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.grpInf.Location = new System.Drawing.Point(159, 145);
            this.grpInf.Margin = new System.Windows.Forms.Padding(2);
            this.grpInf.Name = "grpInf";
            this.grpInf.Padding = new System.Windows.Forms.Padding(2);
            this.grpInf.Size = new System.Drawing.Size(175, 129);
            this.grpInf.TabIndex = 4;
            this.grpInf.TabStop = false;
            this.grpInf.Text = "Infant Details";
            // 
            // txtInfAge
            // 
            this.txtInfAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtInfAge.Location = new System.Drawing.Point(104, 90);
            this.txtInfAge.Margin = new System.Windows.Forms.Padding(2);
            this.txtInfAge.Name = "txtInfAge";
            this.txtInfAge.Size = new System.Drawing.Size(55, 26);
            this.txtInfAge.TabIndex = 17;
            this.txtInfAge.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(4, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Infant Age:  ";
            // 
            // rdoMale
            // 
            this.rdoMale.AutoSize = true;
            this.rdoMale.Location = new System.Drawing.Point(11, 53);
            this.rdoMale.Margin = new System.Windows.Forms.Padding(2);
            this.rdoMale.Name = "rdoMale";
            this.rdoMale.Size = new System.Drawing.Size(61, 24);
            this.rdoMale.TabIndex = 1;
            this.rdoMale.Text = "Male";
            this.rdoMale.UseVisualStyleBackColor = true;
            // 
            // rdoFemale
            // 
            this.rdoFemale.AutoSize = true;
            this.rdoFemale.Checked = true;
            this.rdoFemale.Location = new System.Drawing.Point(11, 25);
            this.rdoFemale.Margin = new System.Windows.Forms.Padding(2);
            this.rdoFemale.Name = "rdoFemale";
            this.rdoFemale.Size = new System.Drawing.Size(80, 24);
            this.rdoFemale.TabIndex = 0;
            this.rdoFemale.TabStop = true;
            this.rdoFemale.Text = "Female";
            this.rdoFemale.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnAdd.Location = new System.Drawing.Point(13, 150);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 72);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add Ind";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAge
            // 
            this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtAge.Location = new System.Drawing.Point(280, 53);
            this.txtAge.Margin = new System.Windows.Forms.Padding(2);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(54, 26);
            this.txtAge.TabIndex = 6;
            this.txtAge.Text = "0";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblAge.Location = new System.Drawing.Point(163, 56);
            this.lblAge.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(113, 20);
            this.lblAge.TabIndex = 7;
            this.lblAge.Text = "Age (months): ";
            // 
            // lstPop
            // 
            this.lstPop.FormattingEnabled = true;
            this.lstPop.Location = new System.Drawing.Point(359, 53);
            this.lstPop.Margin = new System.Windows.Forms.Padding(2);
            this.lstPop.Name = "lstPop";
            this.lstPop.Size = new System.Drawing.Size(262, 199);
            this.lstPop.TabIndex = 8;
            // 
            // txtStartPop
            // 
            this.txtStartPop.Location = new System.Drawing.Point(511, 254);
            this.txtStartPop.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartPop.Name = "txtStartPop";
            this.txtStartPop.Size = new System.Drawing.Size(45, 20);
            this.txtStartPop.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(355, 254);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Starting female pop:";
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(248, 329);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(115, 55);
            this.btnRun.TabIndex = 11;
            this.btnRun.Text = "Run Sim";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnEditRates
            // 
            this.btnEditRates.Enabled = false;
            this.btnEditRates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRates.Location = new System.Drawing.Point(388, 329);
            this.btnEditRates.Name = "btnEditRates";
            this.btnEditRates.Size = new System.Drawing.Size(142, 55);
            this.btnEditRates.TabIndex = 12;
            this.btnEditRates.Text = "Edit Vital Rates";
            this.btnEditRates.UseVisualStyleBackColor = true;
            // 
            // txtYears
            // 
            this.txtYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYears.Location = new System.Drawing.Point(157, 329);
            this.txtYears.Name = "txtYears";
            this.txtYears.Size = new System.Drawing.Size(84, 26);
            this.txtYears.TabIndex = 13;
            this.txtYears.Text = "50";
            // 
            // txtTrials
            // 
            this.txtTrials.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrials.Location = new System.Drawing.Point(157, 358);
            this.txtTrials.Name = "txtTrials";
            this.txtTrials.Size = new System.Drawing.Size(84, 26);
            this.txtTrials.TabIndex = 14;
            this.txtTrials.Text = "1000";
            // 
            // lblYears
            // 
            this.lblYears.AutoSize = true;
            this.lblYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYears.Location = new System.Drawing.Point(27, 335);
            this.lblYears.Name = "lblYears";
            this.lblYears.Size = new System.Drawing.Size(101, 20);
            this.lblYears.TabIndex = 15;
            this.lblYears.Text = "Years to sim:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Number of trials:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(357, 29);
            this.label4.TabIndex = 17;
            this.label4.Text = "1. Create founding population";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(365, 29);
            this.label5.TabIndex = 18;
            this.label5.Text = "2. Simulate population change";
            // 
            // btnDefaultStart
            // 
            this.btnDefaultStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultStart.Location = new System.Drawing.Point(13, 53);
            this.btnDefaultStart.Name = "btnDefaultStart";
            this.btnDefaultStart.Size = new System.Drawing.Size(115, 72);
            this.btnDefaultStart.TabIndex = 19;
            this.btnDefaultStart.Text = "Use Default Pop";
            this.btnDefaultStart.UseVisualStyleBackColor = true;
            this.btnDefaultStart.Click += new System.EventHandler(this.btnDefaultStart_Click);
            // 
            // lblRunning
            // 
            this.lblRunning.AutoSize = true;
            this.lblRunning.Location = new System.Drawing.Point(34, 407);
            this.lblRunning.Name = "lblRunning";
            this.lblRunning.Size = new System.Drawing.Size(0, 13);
            this.lblRunning.TabIndex = 20;
            // 
            // pva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 473);
            this.Controls.Add(this.lblRunning);
            this.Controls.Add(this.btnDefaultStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblYears);
            this.Controls.Add(this.txtTrials);
            this.Controls.Add(this.txtYears);
            this.Controls.Add(this.btnEditRates);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStartPop);
            this.Controls.Add(this.lstPop);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkDepInf);
            this.Controls.Add(this.grpInf);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "pva";
            this.Text = "PVA";
            this.grpInf.ResumeLayout(false);
            this.grpInf.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkDepInf;
        private System.Windows.Forms.GroupBox grpInf;
        private System.Windows.Forms.RadioButton rdoMale;
        private System.Windows.Forms.RadioButton rdoFemale;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.ListBox lstPop;
        private System.Windows.Forms.TextBox txtStartPop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnEditRates;
        private System.Windows.Forms.TextBox txtYears;
        private System.Windows.Forms.TextBox txtTrials;
        private System.Windows.Forms.Label lblYears;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInfAge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDefaultStart;
        private System.Windows.Forms.Label lblRunning;
    }
}

