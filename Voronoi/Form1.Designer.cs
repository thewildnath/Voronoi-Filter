namespace Voronoi {
  partial class Form1 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.buttonOFD = new System.Windows.Forms.Button();
      this.labelSelectedImage = new System.Windows.Forms.Label();
      this.buttonReset = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(12, 49);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(632, 409);
      this.pictureBox.TabIndex = 0;
      this.pictureBox.TabStop = false;
      this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
      this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
      this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
      // 
      // buttonOFD
      // 
      this.buttonOFD.Location = new System.Drawing.Point(103, 12);
      this.buttonOFD.Name = "buttonOFD";
      this.buttonOFD.Size = new System.Drawing.Size(85, 25);
      this.buttonOFD.TabIndex = 1;
      this.buttonOFD.Text = "Select";
      this.buttonOFD.UseVisualStyleBackColor = true;
      this.buttonOFD.Click += new System.EventHandler(this.buttonOFD_Click);
      // 
      // labelSelectedImage
      // 
      this.labelSelectedImage.AutoSize = true;
      this.labelSelectedImage.Location = new System.Drawing.Point(194, 16);
      this.labelSelectedImage.Name = "labelSelectedImage";
      this.labelSelectedImage.Size = new System.Drawing.Size(131, 17);
      this.labelSelectedImage.TabIndex = 2;
      this.labelSelectedImage.Text = "labelSelectedImage";
      // 
      // buttonReset
      // 
      this.buttonReset.Location = new System.Drawing.Point(12, 12);
      this.buttonReset.Name = "buttonReset";
      this.buttonReset.Size = new System.Drawing.Size(85, 25);
      this.buttonReset.TabIndex = 3;
      this.buttonReset.Text = "Reset";
      this.buttonReset.UseVisualStyleBackColor = true;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(656, 468);
      this.Controls.Add(this.buttonReset);
      this.Controls.Add(this.labelSelectedImage);
      this.Controls.Add(this.buttonOFD);
      this.Controls.Add(this.pictureBox);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.Button buttonOFD;
    private System.Windows.Forms.Label labelSelectedImage;
    private System.Windows.Forms.Button buttonReset;
  }
}

