namespace funny_neko_giver
{
    partial class FormMain
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
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.buttonLinkApi = new System.Windows.Forms.Button();
            this.listAvailableApi = new System.Windows.Forms.ComboBox();
            this.labelCurrentApi = new System.Windows.Forms.Label();
            this.groupBoxProgress = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.textDescription = new System.Windows.Forms.RichTextBox();
            this.buttonDeleteAll = new System.Windows.Forms.Button();
            this.buttonDownloadAll = new System.Windows.Forms.Button();
            this.listFilesLoaded = new System.Windows.Forms.ListBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.listCategory = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelForPicture = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolMenuMain = new System.Windows.Forms.ToolStripDropDownButton();
            this.actionButtonLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.actionButtonSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuExternal = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolMenuImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.actionButtonLocalSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actionButtonCopyUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.actionButtonCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.actionButtonCopyResizedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuAbout = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.actionButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.actionButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.actionButtonZoomRestore = new System.Windows.Forms.ToolStripButton();
            this.labelPercentage = new System.Windows.Forms.ToolStripLabel();
            this.groupBoxMain.SuspendLayout();
            this.groupBoxProgress.SuspendLayout();
            this.groupBoxDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelForPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(168, 45);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.OnLoadButtonClick);
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.AutoSize = true;
            this.groupBoxMain.Controls.Add(this.buttonLinkApi);
            this.groupBoxMain.Controls.Add(this.listAvailableApi);
            this.groupBoxMain.Controls.Add(this.labelCurrentApi);
            this.groupBoxMain.Controls.Add(this.groupBoxProgress);
            this.groupBoxMain.Controls.Add(this.groupBoxDescription);
            this.groupBoxMain.Controls.Add(this.buttonDeleteAll);
            this.groupBoxMain.Controls.Add(this.buttonDownloadAll);
            this.groupBoxMain.Controls.Add(this.listFilesLoaded);
            this.groupBoxMain.Controls.Add(this.labelAmount);
            this.groupBoxMain.Controls.Add(this.numAmount);
            this.groupBoxMain.Controls.Add(this.listCategory);
            this.groupBoxMain.Controls.Add(this.buttonLoad);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(249, 401);
            this.groupBoxMain.TabIndex = 1;
            this.groupBoxMain.TabStop = false;
            // 
            // buttonLinkApi
            // 
            this.buttonLinkApi.Image = global::funny_neko_giver.Properties.Resources.link_icon;
            this.buttonLinkApi.Location = new System.Drawing.Point(220, 16);
            this.buttonLinkApi.Name = "buttonLinkApi";
            this.buttonLinkApi.Size = new System.Drawing.Size(23, 23);
            this.buttonLinkApi.TabIndex = 11;
            this.buttonLinkApi.UseVisualStyleBackColor = true;
            this.buttonLinkApi.Click += new System.EventHandler(this.OnButtonLinkApiClick);
            // 
            // listAvailableApi
            // 
            this.listAvailableApi.FormattingEnabled = true;
            this.listAvailableApi.Location = new System.Drawing.Point(76, 18);
            this.listAvailableApi.Name = "listAvailableApi";
            this.listAvailableApi.Size = new System.Drawing.Size(138, 21);
            this.listAvailableApi.TabIndex = 10;
            this.listAvailableApi.SelectedIndexChanged += new System.EventHandler(this.OnApiListIndexChange);
            // 
            // labelCurrentApi
            // 
            this.labelCurrentApi.AutoSize = true;
            this.labelCurrentApi.Location = new System.Drawing.Point(9, 21);
            this.labelCurrentApi.Name = "labelCurrentApi";
            this.labelCurrentApi.Size = new System.Drawing.Size(64, 13);
            this.labelCurrentApi.TabIndex = 9;
            this.labelCurrentApi.Text = "Current API:";
            // 
            // groupBoxProgress
            // 
            this.groupBoxProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxProgress.Controls.Add(this.progressBar);
            this.groupBoxProgress.Location = new System.Drawing.Point(6, 339);
            this.groupBoxProgress.Name = "groupBoxProgress";
            this.groupBoxProgress.Size = new System.Drawing.Size(237, 56);
            this.groupBoxProgress.TabIndex = 8;
            this.groupBoxProgress.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(6, 19);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(225, 23);
            this.progressBar.TabIndex = 8;
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDescription.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxDescription.Controls.Add(this.textDescription);
            this.groupBoxDescription.Location = new System.Drawing.Point(6, 258);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Size = new System.Drawing.Size(237, 75);
            this.groupBoxDescription.TabIndex = 7;
            this.groupBoxDescription.TabStop = false;
            // 
            // textDescription
            // 
            this.textDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textDescription.Location = new System.Drawing.Point(3, 16);
            this.textDescription.Name = "textDescription";
            this.textDescription.ReadOnly = true;
            this.textDescription.Size = new System.Drawing.Size(231, 56);
            this.textDescription.TabIndex = 0;
            this.textDescription.Text = "";
            // 
            // buttonDeleteAll
            // 
            this.buttonDeleteAll.Location = new System.Drawing.Point(130, 229);
            this.buttonDeleteAll.Name = "buttonDeleteAll";
            this.buttonDeleteAll.Size = new System.Drawing.Size(113, 23);
            this.buttonDeleteAll.TabIndex = 6;
            this.buttonDeleteAll.Text = "Delete All";
            this.buttonDeleteAll.UseVisualStyleBackColor = true;
            this.buttonDeleteAll.Click += new System.EventHandler(this.OnButtonDeleteAllClick);
            // 
            // buttonDownloadAll
            // 
            this.buttonDownloadAll.Location = new System.Drawing.Point(6, 229);
            this.buttonDownloadAll.Name = "buttonDownloadAll";
            this.buttonDownloadAll.Size = new System.Drawing.Size(113, 23);
            this.buttonDownloadAll.TabIndex = 5;
            this.buttonDownloadAll.Text = "Download All";
            this.buttonDownloadAll.UseVisualStyleBackColor = true;
            this.buttonDownloadAll.Click += new System.EventHandler(this.OnButtonDownloadAllClick);
            // 
            // listFilesLoaded
            // 
            this.listFilesLoaded.FormattingEnabled = true;
            this.listFilesLoaded.Location = new System.Drawing.Point(6, 100);
            this.listFilesLoaded.Name = "listFilesLoaded";
            this.listFilesLoaded.Size = new System.Drawing.Size(237, 121);
            this.listFilesLoaded.TabIndex = 4;
            this.listFilesLoaded.SelectedIndexChanged += new System.EventHandler(this.OnListBoxIndexChange);
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(6, 76);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(0, 13);
            this.labelAmount.TabIndex = 3;
            // 
            // numAmount
            // 
            this.numAmount.Location = new System.Drawing.Point(58, 74);
            this.numAmount.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            this.numAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(185, 20);
            this.numAmount.TabIndex = 2;
            this.numAmount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // listCategory
            // 
            this.listCategory.FormattingEnabled = true;
            this.listCategory.Location = new System.Drawing.Point(6, 47);
            this.listCategory.Name = "listCategory";
            this.listCategory.Size = new System.Drawing.Size(156, 21);
            this.listCategory.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.panelForPicture);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(249, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(400, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 401);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // panelForPicture
            // 
            this.panelForPicture.AutoScroll = true;
            this.panelForPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelForPicture.Controls.Add(this.pictureBox);
            this.panelForPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForPicture.Location = new System.Drawing.Point(3, 16);
            this.panelForPicture.Name = "panelForPicture";
            this.panelForPicture.Size = new System.Drawing.Size(461, 382);
            this.panelForPicture.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.ErrorImage = global::funny_neko_giver.Properties.Resources.image_icon;
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.MinimumSize = new System.Drawing.Size(320, 320);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(334, 350);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.WaitOnLoad = true;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolMenuMain, this.toolMenuExternal, this.toolMenuImage, this.toolMenuAbout, this.toolStripSeparator1, this.actionButtonZoomIn, this.actionButtonZoomOut, this.actionButtonZoomRestore, this.labelPercentage });
            this.toolStripMain.Location = new System.Drawing.Point(249, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(467, 25);
            this.toolStripMain.TabIndex = 4;
            // 
            // toolMenuMain
            // 
            this.toolMenuMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolMenuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.actionButtonLoad, this.actionButtonSearch });
            this.toolMenuMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMenuMain.Name = "toolMenuMain";
            this.toolMenuMain.Size = new System.Drawing.Size(47, 22);
            this.toolMenuMain.Text = "Main";
            // 
            // actionButtonLoad
            // 
            this.actionButtonLoad.Image = global::funny_neko_giver.Properties.Resources.image_icon;
            this.actionButtonLoad.Name = "actionButtonLoad";
            this.actionButtonLoad.Size = new System.Drawing.Size(152, 22);
            this.actionButtonLoad.Text = "Load";
            this.actionButtonLoad.Click += new System.EventHandler(this.OnLoadButtonClick);
            // 
            // actionButtonSearch
            // 
            this.actionButtonSearch.Image = global::funny_neko_giver.Properties.Resources.websearch_icon;
            this.actionButtonSearch.Name = "actionButtonSearch";
            this.actionButtonSearch.Size = new System.Drawing.Size(152, 22);
            this.actionButtonSearch.Text = "Search (Query)";
            // 
            // toolMenuExternal
            // 
            this.toolMenuExternal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolMenuExternal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMenuExternal.Name = "toolMenuExternal";
            this.toolMenuExternal.Size = new System.Drawing.Size(62, 22);
            this.toolMenuExternal.Text = "External";
            // 
            // toolMenuImage
            // 
            this.toolMenuImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolMenuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.actionButtonLocalSave, this.toolStripSeparator2, this.actionButtonCopyUrl, this.actionButtonCopyImage, this.actionButtonCopyResizedImage });
            this.toolMenuImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMenuImage.Name = "toolMenuImage";
            this.toolMenuImage.Size = new System.Drawing.Size(53, 22);
            this.toolMenuImage.Text = "Image";
            // 
            // actionButtonLocalSave
            // 
            this.actionButtonLocalSave.Image = global::funny_neko_giver.Properties.Resources.save_icon;
            this.actionButtonLocalSave.Name = "actionButtonLocalSave";
            this.actionButtonLocalSave.Size = new System.Drawing.Size(180, 22);
            this.actionButtonLocalSave.Text = "Save (Local)";
            this.actionButtonLocalSave.Click += new System.EventHandler(this.OnSaveImageClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // actionButtonCopyUrl
            // 
            this.actionButtonCopyUrl.Image = global::funny_neko_giver.Properties.Resources.link_icon;
            this.actionButtonCopyUrl.Name = "actionButtonCopyUrl";
            this.actionButtonCopyUrl.Size = new System.Drawing.Size(180, 22);
            this.actionButtonCopyUrl.Text = "Copy Image URL";
            this.actionButtonCopyUrl.Click += new System.EventHandler(this.OnCopyURLClick);
            // 
            // actionButtonCopyImage
            // 
            this.actionButtonCopyImage.Image = global::funny_neko_giver.Properties.Resources.copyfile_icon;
            this.actionButtonCopyImage.Name = "actionButtonCopyImage";
            this.actionButtonCopyImage.Size = new System.Drawing.Size(180, 22);
            this.actionButtonCopyImage.Text = "Copy Image";
            this.actionButtonCopyImage.Click += new System.EventHandler(this.OnCopyImageClick);
            // 
            // actionButtonCopyResizedImage
            // 
            this.actionButtonCopyResizedImage.Image = global::funny_neko_giver.Properties.Resources.copyfile_icon;
            this.actionButtonCopyResizedImage.Name = "actionButtonCopyResizedImage";
            this.actionButtonCopyResizedImage.Size = new System.Drawing.Size(180, 22);
            this.actionButtonCopyResizedImage.Text = "Copy Resized Image";
            this.actionButtonCopyResizedImage.Click += new System.EventHandler(this.OnCopyResizedImageClick);
            // 
            // toolMenuAbout
            // 
            this.toolMenuAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolMenuAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMenuAbout.Name = "toolMenuAbout";
            this.toolMenuAbout.Size = new System.Drawing.Size(53, 22);
            this.toolMenuAbout.Text = "About";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // actionButtonZoomIn
            // 
            this.actionButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionButtonZoomIn.Image = global::funny_neko_giver.Properties.Resources.zoomin_icon;
            this.actionButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionButtonZoomIn.Name = "actionButtonZoomIn";
            this.actionButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.actionButtonZoomIn.Text = "toolStripButton1";
            this.actionButtonZoomIn.ToolTipText = "Zoom In";
            this.actionButtonZoomIn.Click += new System.EventHandler(this.OnZoomInImage);
            // 
            // actionButtonZoomOut
            // 
            this.actionButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionButtonZoomOut.Image = global::funny_neko_giver.Properties.Resources.zoomout_icon;
            this.actionButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionButtonZoomOut.Name = "actionButtonZoomOut";
            this.actionButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.actionButtonZoomOut.Text = "toolStripButton1";
            this.actionButtonZoomOut.ToolTipText = "Zoom Out";
            this.actionButtonZoomOut.Click += new System.EventHandler(this.OnZoomOutImage);
            // 
            // actionButtonZoomRestore
            // 
            this.actionButtonZoomRestore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionButtonZoomRestore.Image = global::funny_neko_giver.Properties.Resources.image_icon;
            this.actionButtonZoomRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionButtonZoomRestore.Name = "actionButtonZoomRestore";
            this.actionButtonZoomRestore.Size = new System.Drawing.Size(23, 22);
            this.actionButtonZoomRestore.Text = "toolStripButton1";
            this.actionButtonZoomRestore.ToolTipText = "Restore Zoom";
            this.actionButtonZoomRestore.Click += new System.EventHandler(this.OnZoomRestoreImage);
            // 
            // labelPercentage
            // 
            this.labelPercentage.Name = "labelPercentage";
            this.labelPercentage.Size = new System.Drawing.Size(23, 22);
            this.labelPercentage.Text = "0%";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 401);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxMain);
            this.MinimumSize = new System.Drawing.Size(620, 440);
            this.Name = "FormMain";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.groupBoxProgress.ResumeLayout(false);
            this.groupBoxDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panelForPicture.ResumeLayout(false);
            this.panelForPicture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonLinkApi;

        private System.Windows.Forms.ToolStripDropDownButton toolMenuExternal;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem actionButtonCopyResizedImage;

        private System.Windows.Forms.ToolStripLabel labelPercentage;
        private System.Windows.Forms.ToolStripButton actionButtonZoomRestore;

        private System.Windows.Forms.ToolStripButton actionButtonZoomOut;

        private System.Windows.Forms.ToolStripButton actionButtonZoomIn;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

        private System.Windows.Forms.ToolStripMenuItem actionButtonCopyImage;

        private System.Windows.Forms.ToolStripMenuItem actionButtonCopyUrl;

        private System.Windows.Forms.ToolStripDropDownButton toolMenuAbout;

        private System.Windows.Forms.ToolStripMenuItem actionButtonSearch;

        private System.Windows.Forms.ToolStripMenuItem actionButtonLocalSave;

        private System.Windows.Forms.ToolStripDropDownButton toolMenuImage;

        private System.Windows.Forms.ComboBox listAvailableApi;

        private System.Windows.Forms.Label labelCurrentApi;

        private System.Windows.Forms.ToolStripMenuItem actionButtonLoad;

        private System.Windows.Forms.ToolStripDropDownButton toolMenuMain;

        private System.Windows.Forms.ToolStrip toolStripMain;

        private System.Windows.Forms.GroupBox groupBoxProgress;

        private System.Windows.Forms.ProgressBar progressBar;

        private System.Windows.Forms.RichTextBox textDescription;

        private System.Windows.Forms.GroupBox groupBoxDescription;

        private System.Windows.Forms.Button buttonDownloadAll;
        private System.Windows.Forms.Button buttonDeleteAll;

        private System.Windows.Forms.Panel panelForPicture;

        private System.Windows.Forms.ListBox listFilesLoaded;

        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label labelAmount;

        private System.Windows.Forms.PictureBox pictureBox;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.ComboBox listCategory;

        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.GroupBox groupBoxMain;

        #endregion
    }
}