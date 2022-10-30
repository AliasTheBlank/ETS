namespace TelethonSystem
{
    partial class ShowPrizes
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.rbAllPrize = new System.Windows.Forms.RadioButton();
            this.rbQualifiedPrizes = new System.Windows.Forms.RadioButton();
            this.lAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(12, 275);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(158, 33);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(353, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 33);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(230, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Prizes";
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 68);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(498, 192);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // rbAllPrize
            // 
            this.rbAllPrize.AutoSize = true;
            this.rbAllPrize.Location = new System.Drawing.Point(33, 13);
            this.rbAllPrize.Name = "rbAllPrize";
            this.rbAllPrize.Size = new System.Drawing.Size(67, 17);
            this.rbAllPrize.TabIndex = 5;
            this.rbAllPrize.TabStop = true;
            this.rbAllPrize.Text = "All Prizes";
            this.rbAllPrize.UseVisualStyleBackColor = true;
            this.rbAllPrize.CheckedChanged += new System.EventHandler(this.rbAllPrize_CheckedChanged);
            // 
            // rbQualifiedPrizes
            // 
            this.rbQualifiedPrizes.AutoSize = true;
            this.rbQualifiedPrizes.Location = new System.Drawing.Point(33, 37);
            this.rbQualifiedPrizes.Name = "rbQualifiedPrizes";
            this.rbQualifiedPrizes.Size = new System.Drawing.Size(97, 17);
            this.rbQualifiedPrizes.TabIndex = 6;
            this.rbQualifiedPrizes.TabStop = true;
            this.rbQualifiedPrizes.Text = "Qualified Prizes";
            this.rbQualifiedPrizes.UseVisualStyleBackColor = true;
            this.rbQualifiedPrizes.CheckedChanged += new System.EventHandler(this.rbQualifiedPrizes_CheckedChanged);
            // 
            // lAmount
            // 
            this.lAmount.AutoSize = true;
            this.lAmount.Location = new System.Drawing.Point(396, 9);
            this.lAmount.Name = "lAmount";
            this.lAmount.Size = new System.Drawing.Size(46, 13);
            this.lAmount.TabIndex = 7;
            this.lAmount.Text = "Amount:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(399, 34);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(73, 20);
            this.txtAmount.TabIndex = 8;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // ShowPrizes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 320);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lAmount);
            this.Controls.Add(this.rbQualifiedPrizes);
            this.Controls.Add(this.rbAllPrize);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSelect);
            this.Name = "ShowPrizes";
            this.Text = "ShowPrizes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.RadioButton rbAllPrize;
        private System.Windows.Forms.RadioButton rbQualifiedPrizes;
        private System.Windows.Forms.Label lAmount;
        private System.Windows.Forms.TextBox txtAmount;
    }
}