namespace PasteIntoFile
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.CbxNoUI = new System.Windows.Forms.CheckBox();
            this.ComboText = new System.Windows.Forms.ComboBox();
            this.ComboImage = new System.Windows.Forms.ComboBox();
            this.TextboxFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(198, 86);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 0;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(279, 86);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CbxNoUI
            // 
            this.CbxNoUI.AutoSize = true;
            this.CbxNoUI.Location = new System.Drawing.Point(227, 37);
            this.CbxNoUI.Name = "CbxNoUI";
            this.CbxNoUI.Size = new System.Drawing.Size(127, 17);
            this.CbxNoUI.TabIndex = 2;
            this.CbxNoUI.Text = "Skip UI (use defaults)";
            this.CbxNoUI.UseVisualStyleBackColor = true;
            // 
            // ComboText
            // 
            this.ComboText.FormattingEnabled = true;
            this.ComboText.Items.AddRange(new object[] {
            "txt",
            "html",
            "js",
            "css",
            "csv",
            "json",
            "cs",
            "cpp",
            "java",
            "php"});
            this.ComboText.Location = new System.Drawing.Point(132, 6);
            this.ComboText.Name = "ComboText";
            this.ComboText.Size = new System.Drawing.Size(72, 21);
            this.ComboText.TabIndex = 3;
            // 
            // ComboImage
            // 
            this.ComboImage.FormattingEnabled = true;
            this.ComboImage.Items.AddRange(new object[] {
            "png",
            "jpg",
            "bmp",
            "gif",
            "ico"});
            this.ComboImage.Location = new System.Drawing.Point(132, 33);
            this.ComboImage.Name = "ComboImage";
            this.ComboImage.Size = new System.Drawing.Size(72, 21);
            this.ComboImage.TabIndex = 4;
            // 
            // TextboxFormat
            // 
            this.TextboxFormat.Location = new System.Drawing.Point(132, 60);
            this.TextboxFormat.Name = "TextboxFormat";
            this.TextboxFormat.Size = new System.Drawing.Size(222, 20);
            this.TextboxFormat.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Default Text Extension";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Default Image Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Filename Format";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 115);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextboxFormat);
            this.Controls.Add(this.ComboImage);
            this.Controls.Add(this.ComboText);
            this.Controls.Add(this.CbxNoUI);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckBox CbxNoUI;
        private System.Windows.Forms.ComboBox ComboText;
        private System.Windows.Forms.ComboBox ComboImage;
        private System.Windows.Forms.TextBox TextboxFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}