namespace ShellExtention {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing ) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.btnRemoveOption = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSetOption = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRemoveOption
            // 
            this.btnRemoveOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveOption.Location = new System.Drawing.Point(184, 36);
            this.btnRemoveOption.Name = "btnRemoveOption";
            this.btnRemoveOption.Size = new System.Drawing.Size(133, 23);
            this.btnRemoveOption.TabIndex = 13;
            this.btnRemoveOption.Text = "Remove Context Menu";
            this.btnRemoveOption.UseVisualStyleBackColor = true;
            this.btnRemoveOption.Click += new System.EventHandler(this.btnRemoveOption_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(242, 65);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSetOption
            // 
            this.btnSetOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetOption.Location = new System.Drawing.Point(71, 36);
            this.btnSetOption.Name = "btnSetOption";
            this.btnSetOption.Size = new System.Drawing.Size(107, 23);
            this.btnSetOption.TabIndex = 7;
            this.btnSetOption.Text = "Set Context Menu";
            this.btnSetOption.UseVisualStyleBackColor = true;
            this.btnSetOption.Click += new System.EventHandler(this.btnSetOption_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Add or Remove the option in the right-click menu of a APK file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 100);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveOption);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSetOption);
            this.MinimumSize = new System.Drawing.Size(345, 139);
            this.Name = "Form1";
            this.Text = "APK Shell Extention";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRemoveOption;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSetOption;
        private System.Windows.Forms.Label label1;
    }
}

