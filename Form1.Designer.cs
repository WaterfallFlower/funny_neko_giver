namespace funny_neko_giver
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
            this.button_generate = new System.Windows.Forms.Button();
            this.group_box_main = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_delete_all = new System.Windows.Forms.Button();
            this.button_download_all = new System.Windows.Forms.Button();
            this.list_files_loaded = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.num_amount = new System.Windows.Forms.NumericUpDown();
            this.list_category = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.group_box_main.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_amount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_generate
            // 
            this.button_generate.Location = new System.Drawing.Point(168, 21);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(75, 23);
            this.button_generate.TabIndex = 0;
            this.button_generate.Text = "Load";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
            // 
            // group_box_main
            // 
            this.group_box_main.AutoSize = true;
            this.group_box_main.Controls.Add(this.groupBox3);
            this.group_box_main.Controls.Add(this.groupBox2);
            this.group_box_main.Controls.Add(this.button_delete_all);
            this.group_box_main.Controls.Add(this.button_download_all);
            this.group_box_main.Controls.Add(this.list_files_loaded);
            this.group_box_main.Controls.Add(this.label1);
            this.group_box_main.Controls.Add(this.num_amount);
            this.group_box_main.Controls.Add(this.list_category);
            this.group_box_main.Controls.Add(this.button_generate);
            this.group_box_main.Dock = System.Windows.Forms.DockStyle.Left;
            this.group_box_main.Location = new System.Drawing.Point(0, 0);
            this.group_box_main.Name = "group_box_main";
            this.group_box_main.Size = new System.Drawing.Size(249, 401);
            this.group_box_main.TabIndex = 1;
            this.group_box_main.TabStop = false;
            this.group_box_main.Text = "Configure";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Location = new System.Drawing.Point(6, 339);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 56);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "No tasks running...";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(6, 19);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(225, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Location = new System.Drawing.Point(6, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 75);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Description";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(231, 56);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button_delete_all
            // 
            this.button_delete_all.Location = new System.Drawing.Point(130, 229);
            this.button_delete_all.Name = "button_delete_all";
            this.button_delete_all.Size = new System.Drawing.Size(113, 23);
            this.button_delete_all.TabIndex = 6;
            this.button_delete_all.Text = "Delete All";
            this.button_delete_all.UseVisualStyleBackColor = true;
            // 
            // button_download_all
            // 
            this.button_download_all.Location = new System.Drawing.Point(6, 229);
            this.button_download_all.Name = "button_download_all";
            this.button_download_all.Size = new System.Drawing.Size(113, 23);
            this.button_download_all.TabIndex = 5;
            this.button_download_all.Text = "Download All";
            this.button_download_all.UseVisualStyleBackColor = true;
            // 
            // list_files_loaded
            // 
            this.list_files_loaded.FormattingEnabled = true;
            this.list_files_loaded.Location = new System.Drawing.Point(6, 76);
            this.list_files_loaded.Name = "list_files_loaded";
            this.list_files_loaded.Size = new System.Drawing.Size(237, 147);
            this.list_files_loaded.TabIndex = 4;
            this.list_files_loaded.SelectedIndexChanged += new System.EventHandler(this.selected_index_change);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Amount:";
            // 
            // num_amount
            // 
            this.num_amount.Location = new System.Drawing.Point(58, 50);
            this.num_amount.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            this.num_amount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.num_amount.Name = "num_amount";
            this.num_amount.Size = new System.Drawing.Size(185, 20);
            this.num_amount.TabIndex = 2;
            this.num_amount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // list_category
            // 
            this.list_category.FormattingEnabled = true;
            this.list_category.Location = new System.Drawing.Point(6, 23);
            this.list_category.Name = "list_category";
            this.list_category.Size = new System.Drawing.Size(156, 21);
            this.list_category.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(249, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(400, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 401);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 382);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(320, 320);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 401);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group_box_main);
            this.MinimumSize = new System.Drawing.Size(620, 440);
            this.Name = "Form1";
            this.Text = "Little Grabber: Nekos";
            this.Load += new System.EventHandler(this.OnLoadingForm);
            this.group_box_main.ResumeLayout(false);
            this.group_box_main.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_amount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.ProgressBar progressBar1;

        private System.Windows.Forms.RichTextBox richTextBox1;

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Button button_download_all;
        private System.Windows.Forms.Button button_delete_all;

        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.ListBox list_files_loaded;

        private System.Windows.Forms.NumericUpDown num_amount;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.PictureBox pictureBox1;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.ComboBox list_category;

        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.GroupBox group_box_main;

        #endregion
    }
}