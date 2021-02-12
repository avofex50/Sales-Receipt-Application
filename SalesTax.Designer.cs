namespace SalesTaxes
{
    partial class SalesTax
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
            this.rtbInputList = new System.Windows.Forms.RichTextBox();
            this.rtbOutputList = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCalculateTax = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtbInputList
            // 
            this.rtbInputList.Location = new System.Drawing.Point(12, 43);
            this.rtbInputList.Name = "rtbInputList";
            this.rtbInputList.Size = new System.Drawing.Size(949, 170);
            this.rtbInputList.TabIndex = 0;
            this.rtbInputList.Text = "";
            // 
            // rtbOutputList
            // 
            this.rtbOutputList.Location = new System.Drawing.Point(12, 288);
            this.rtbOutputList.Name = "rtbOutputList";
            this.rtbOutputList.ReadOnly = true;
            this.rtbOutputList.Size = new System.Drawing.Size(949, 225);
            this.rtbOutputList.TabIndex = 1;
            this.rtbOutputList.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add products to cart:";
            // 
            // btnCalculateTax
            // 
            this.btnCalculateTax.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCalculateTax.Location = new System.Drawing.Point(802, 219);
            this.btnCalculateTax.Name = "btnCalculateTax";
            this.btnCalculateTax.Size = new System.Drawing.Size(145, 41);
            this.btnCalculateTax.TabIndex = 3;
            this.btnCalculateTax.Text = "Calculate Tax ";
            this.btnCalculateTax.UseVisualStyleBackColor = true;
            this.btnCalculateTax.Click += new System.EventHandler(this.btnCalculateTax_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sales Receipt:";
            // 
            // SalesTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 525);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCalculateTax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbOutputList);
            this.Controls.Add(this.rtbInputList);
            this.Name = "SalesTax";
            this.Text = "SalesTaxStreamReader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbInputList;
        private System.Windows.Forms.RichTextBox rtbOutputList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalculateTax;
        private System.Windows.Forms.Label label2;
    }
}