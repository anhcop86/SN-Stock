namespace SnapShot
{
    partial class SnapShotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnapShotForm));
            this.snapShotFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SelectFilebutton = new System.Windows.Forms.Button();
            this.SnapShotFileTextBox = new System.Windows.Forms.TextBox();
            this.CreateSnapShotButton = new System.Windows.Forms.Button();
            this.NameCelltextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NameFolderHOSEtextBox = new System.Windows.Forms.TextBox();
            this.NameFolderHNXtextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelectFolderHNX = new System.Windows.Forms.Button();
            this.buttonSelectFolderHOSE = new System.Windows.Forms.Button();
            this.NameFilecomboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // snapShotFileDialog
            // 
            this.snapShotFileDialog.FileName = "snapShotFileDialog";
            // 
            // SelectFilebutton
            // 
            this.SelectFilebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectFilebutton.Location = new System.Drawing.Point(268, 63);
            this.SelectFilebutton.Name = "SelectFilebutton";
            this.SelectFilebutton.Size = new System.Drawing.Size(111, 23);
            this.SelectFilebutton.TabIndex = 0;
            this.SelectFilebutton.Text = "Chọn File";
            this.SelectFilebutton.UseVisualStyleBackColor = true;
            this.SelectFilebutton.Click += new System.EventHandler(this.SelectFilebutton_Click);
            // 
            // SnapShotFileTextBox
            // 
            this.SnapShotFileTextBox.Location = new System.Drawing.Point(12, 63);
            this.SnapShotFileTextBox.Name = "SnapShotFileTextBox";
            this.SnapShotFileTextBox.Size = new System.Drawing.Size(250, 20);
            this.SnapShotFileTextBox.TabIndex = 3;
            // 
            // CreateSnapShotButton
            // 
            this.CreateSnapShotButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateSnapShotButton.Location = new System.Drawing.Point(168, 147);
            this.CreateSnapShotButton.Name = "CreateSnapShotButton";
            this.CreateSnapShotButton.Size = new System.Drawing.Size(211, 23);
            this.CreateSnapShotButton.TabIndex = 6;
            this.CreateSnapShotButton.Text = "Tạo File";
            this.CreateSnapShotButton.UseVisualStyleBackColor = true;
            this.CreateSnapShotButton.Click += new System.EventHandler(this.CreateSnapShotButton_Click);
            // 
            // NameCelltextBox
            // 
            this.NameCelltextBox.Location = new System.Drawing.Point(105, 36);
            this.NameCelltextBox.Name = "NameCelltextBox";
            this.NameCelltextBox.Size = new System.Drawing.Size(121, 20);
            this.NameCelltextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nhập tên File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nhập Cell";
            // 
            // NameFolderHOSEtextBox
            // 
            this.NameFolderHOSEtextBox.Location = new System.Drawing.Point(105, 95);
            this.NameFolderHOSEtextBox.Name = "NameFolderHOSEtextBox";
            this.NameFolderHOSEtextBox.Size = new System.Drawing.Size(157, 20);
            this.NameFolderHOSEtextBox.TabIndex = 4;
            // 
            // NameFolderHNXtextBox
            // 
            this.NameFolderHNXtextBox.Location = new System.Drawing.Point(105, 121);
            this.NameFolderHNXtextBox.Name = "NameFolderHNXtextBox";
            this.NameFolderHNXtextBox.Size = new System.Drawing.Size(157, 20);
            this.NameFolderHNXtextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Thư mục HOSE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Thu mục HNX";
            // 
            // buttonSelectFolderHNX
            // 
            this.buttonSelectFolderHNX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectFolderHNX.Location = new System.Drawing.Point(268, 118);
            this.buttonSelectFolderHNX.Name = "buttonSelectFolderHNX";
            this.buttonSelectFolderHNX.Size = new System.Drawing.Size(111, 23);
            this.buttonSelectFolderHNX.TabIndex = 12;
            this.buttonSelectFolderHNX.Text = "Chọn thư mục";
            this.buttonSelectFolderHNX.UseVisualStyleBackColor = true;
            this.buttonSelectFolderHNX.Click += new System.EventHandler(this.buttonSelectFolderHNX_Click);
            // 
            // buttonSelectFolderHOSE
            // 
            this.buttonSelectFolderHOSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectFolderHOSE.Location = new System.Drawing.Point(268, 91);
            this.buttonSelectFolderHOSE.Name = "buttonSelectFolderHOSE";
            this.buttonSelectFolderHOSE.Size = new System.Drawing.Size(111, 23);
            this.buttonSelectFolderHOSE.TabIndex = 13;
            this.buttonSelectFolderHOSE.Text = "Chọn thư mục";
            this.buttonSelectFolderHOSE.UseVisualStyleBackColor = true;
            this.buttonSelectFolderHOSE.Click += new System.EventHandler(this.buttonSelectFolderHOSE_Click);
            // 
            // NameFilecomboBox
            // 
            this.NameFilecomboBox.FormattingEnabled = true;
            this.NameFilecomboBox.Location = new System.Drawing.Point(105, 11);
            this.NameFilecomboBox.Name = "NameFilecomboBox";
            this.NameFilecomboBox.Size = new System.Drawing.Size(121, 21);
            this.NameFilecomboBox.TabIndex = 14;
            this.NameFilecomboBox.SelectedIndexChanged += new System.EventHandler(this.NameFilecomboBox_SelectedIndexChanged);
            // 
            // SnapShotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(400, 192);
            this.Controls.Add(this.NameFilecomboBox);
            this.Controls.Add(this.buttonSelectFolderHOSE);
            this.Controls.Add(this.buttonSelectFolderHNX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NameFolderHNXtextBox);
            this.Controls.Add(this.NameFolderHOSEtextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameCelltextBox);
            this.Controls.Add(this.CreateSnapShotButton);
            this.Controls.Add(this.SnapShotFileTextBox);
            this.Controls.Add(this.SelectFilebutton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SnapShotForm";
            this.Text = "Create File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog snapShotFileDialog;        
        private System.Windows.Forms.Button SelectFilebutton;
        private System.Windows.Forms.TextBox SnapShotFileTextBox;
        private System.Windows.Forms.Button CreateSnapShotButton;
        private System.Windows.Forms.TextBox NameCelltextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameFolderHOSEtextBox;
        private System.Windows.Forms.TextBox NameFolderHNXtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSelectFolderHNX;
        private System.Windows.Forms.Button buttonSelectFolderHOSE;
        private System.Windows.Forms.ComboBox NameFilecomboBox;
    }
}

