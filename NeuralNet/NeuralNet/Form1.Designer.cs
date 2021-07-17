namespace NeuralNet
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
            this.trainNN = new System.Windows.Forms.Button();
            this.testNN = new System.Windows.Forms.Button();
            this.createNN = new System.Windows.Forms.Button();
            this.ResultInfo = new System.Windows.Forms.ListBox();
            this.EpochTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LayerTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // trainNN
            // 
            this.trainNN.Location = new System.Drawing.Point(50, 276);
            this.trainNN.Name = "trainNN";
            this.trainNN.Size = new System.Drawing.Size(202, 76);
            this.trainNN.TabIndex = 1;
            this.trainNN.Text = "TrainNN";
            this.trainNN.UseVisualStyleBackColor = true;
            this.trainNN.Click += new System.EventHandler(this.trainNN_Click);
            // 
            // testNN
            // 
            this.testNN.Location = new System.Drawing.Point(50, 358);
            this.testNN.Name = "testNN";
            this.testNN.Size = new System.Drawing.Size(202, 76);
            this.testNN.TabIndex = 2;
            this.testNN.Text = "TestNN";
            this.testNN.UseVisualStyleBackColor = true;
            this.testNN.Click += new System.EventHandler(this.testNN_Click);
            // 
            // createNN
            // 
            this.createNN.Location = new System.Drawing.Point(50, 194);
            this.createNN.Name = "createNN";
            this.createNN.Size = new System.Drawing.Size(202, 76);
            this.createNN.TabIndex = 3;
            this.createNN.Text = "CreateNN";
            this.createNN.UseVisualStyleBackColor = true;
            this.createNN.Click += new System.EventHandler(this.createNN_Click);
            // 
            // ResultInfo
            // 
            this.ResultInfo.FormattingEnabled = true;
            this.ResultInfo.ItemHeight = 16;
            this.ResultInfo.Location = new System.Drawing.Point(293, 43);
            this.ResultInfo.Name = "ResultInfo";
            this.ResultInfo.Size = new System.Drawing.Size(365, 548);
            this.ResultInfo.TabIndex = 5;
            // 
            // EpochTextBox
            // 
            this.EpochTextBox.Location = new System.Drawing.Point(50, 63);
            this.EpochTextBox.Name = "EpochTextBox";
            this.EpochTextBox.Size = new System.Drawing.Size(237, 22);
            this.EpochTextBox.TabIndex = 6;
            this.EpochTextBox.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total Epochs to train for: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Layer Layout (comma seperated): ";
            // 
            // LayerTextBox
            // 
            this.LayerTextBox.Location = new System.Drawing.Point(50, 108);
            this.LayerTextBox.Name = "LayerTextBox";
            this.LayerTextBox.Size = new System.Drawing.Size(237, 22);
            this.LayerTextBox.TabIndex = 9;
            this.LayerTextBox.Text = "10, 10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ex. with 3 layers of 20 neurons each:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "20, 20, 20";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 440);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(202, 151);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 627);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LayerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EpochTextBox);
            this.Controls.Add(this.ResultInfo);
            this.Controls.Add(this.createNN);
            this.Controls.Add(this.testNN);
            this.Controls.Add(this.trainNN);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button trainNN;
        private System.Windows.Forms.Button testNN;
        private System.Windows.Forms.Button createNN;
        private System.Windows.Forms.ListBox ResultInfo;
        private System.Windows.Forms.TextBox EpochTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LayerTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
    }
}

