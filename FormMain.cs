using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using funny_neko_giver.Properties;

namespace funny_neko_giver
{
    public partial class FormMain : Form
    {
        private readonly HttpClient _localHttpClient = new HttpClient();
        private IImageProviderApi _apiInstance;
        private GifRenderLayer _gifPictures;
        private int _percentZoom = 100;

        public FormMain()
        {
            InitializeComponent();
        }

        private void SetProgressMaxValue(int i)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = i;
        }

        private void UpdateProgressBarValue(string s)
        {
            progressBar.PerformStep();
            groupBoxProgress.Text = s;
        }

        private void FinalizeProgressBarValue()
        {
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = 0;
            groupBoxProgress.Text = Resources.progress_completed;
        }

        private void SetZoomPercentage(int percent)
        {
            var description = listFilesLoaded.SelectedItem as ResultImage;
            // ReSharper disable once LocalizableElement
            labelPercentage.Text = $"{percent}% ({(int)(percent * 0.01 * description.ImageItself.Width)}x{(int)(percent * 0.01 * description.ImageItself.Width)})";
            _percentZoom = percent;
        }

        private void UpdateZoomPicture()
        {
            var description = listFilesLoaded.SelectedItem as ResultImage;
            if (_percentZoom == 100)
            {
                pictureBox.Image = description.ImageItself;
                pictureBox.Invalidate();
                return;
            }
            var ratioUpdate = _percentZoom * 0.01; //Same as "/ 100"
            var width = (int)(ratioUpdate * description.ImageItself.Width);
            var height = (int)(ratioUpdate * description.ImageItself.Height);
            
            var zoomedImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(zoomedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(description.ImageItself, 0, 0, width, height);
            }

            pictureBox.Image = zoomedImage;
            pictureBox.Invalidate();
        }
        
        private void CallNewApi()
        {
            listCategory.Items.Clear();

            var access1 = listAvailableApi.SelectedItem as ImageApiDescription;
            
            buttonLoad.Enabled = listCategory.Enabled = false;
            _apiInstance = access1.CreateInstance();
            _apiInstance.Init(
                _localHttpClient,
                stringError =>
                {
                    MessageBox.Show(
                        string.Format(Resources.error_connectapi, stringError), Resources.dialog_messages_error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                    buttonLoad.Enabled = listCategory.Enabled = false;
                },
                instance =>
                {
                    foreach (var i in instance.GetCategories())
                    {
                        listCategory.Items.Add(i);
                    }
                    listCategory.SelectedIndex = 0;
                    buttonLoad.Enabled = listCategory.Enabled = true;
                });
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            /* Setting up the progress bar and other utils */
            toolMenuImage.Enabled = actionButtonZoomIn.Enabled = actionButtonZoomOut.Enabled = actionButtonZoomRestore.Enabled = false;
            progressBar.Value = 0;
            progressBar.Maximum = 0;
            progressBar.Step = 1;

            /* Localization */
            groupBoxProgress.Text = Resources.progress_notasksrunning;
            groupBoxDescription.Text = Resources.form_file_description;
            groupBoxMain.Text = Resources.form_configure;
            buttonLoad.Text = Resources.form_button_load;
            labelAmount.Text = Resources.form_label_amount;
            labelCurrentApi.Text = Resources.form_label_currentapi;
            buttonDeleteAll.Text = Resources.form_button_deleteall;
            buttonDownloadAll.Text = Resources.form_button_downloadall;
            toolMenuMain.Text = Resources.form_tool_strip_main;
            toolMenuImage.Text = Resources.form_tool_strip_image;
            toolMenuAbout.Text = Resources.form_tool_strip_about;
            actionButtonLoad.Text = Resources.form_button_load;
            actionButtonLocalSave.Text = Resources.form_tool_strip_save_local;
            actionButtonSearch.Text = Resources.form_tool_strip_search;
            actionButtonCopyUrl.Text = Resources.form_tool_strip_copyurl;
            actionButtonCopyImage.Text = Resources.form_tool_strip_copyimage;
            actionButtonCopyResizedImage.Text = Resources.form_tool_strip_copyresimage;
            actionButtonZoomIn.ToolTipText = Resources.form_tool_strip_zoomin;
            actionButtonZoomOut.ToolTipText = Resources.form_tool_strip_zoomout;
            actionButtonZoomRestore.ToolTipText = Resources.form_tool_strip_zoomrestore;
            
            /* Filling and configuring API list */
            listAvailableApi.Items.Add(new NekosBestApiProvider());
            listAvailableApi.Items.Add(new NekosFunApiProvider());
            listAvailableApi.SelectedIndex = 0;
        }

        private void OnLoadButtonClick(object sender, EventArgs e)
        {
            if (listCategory.SelectedIndex >= 0)
            {
                SetProgressMaxValue(2 + (int)numAmount.Value);
                buttonLoad.Enabled = numAmount.Enabled = listAvailableApi.Enabled = false;
                _apiInstance.LoadCategoryImage(listCategory.SelectedItem as CategoryImage, (int)numAmount.Value,
                    stringError =>
                    {
                        buttonLoad.Enabled = numAmount.Enabled = listAvailableApi.Enabled = true;
                        MessageBox.Show(
                            string.Format(Resources.error_connectapi, new object[]{stringError}), Resources.dialog_messages_error,
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    },
                    obtainedFile =>
                    {
                        buttonLoad.Enabled = numAmount.Enabled = listAvailableApi.Enabled = true;
                        listFilesLoaded.Items.Add(obtainedFile);
                        listFilesLoaded.SelectedItem = obtainedFile;
                    },
                    UpdateProgressBarValue, FinalizeProgressBarValue);
            }
            else
            {
                MessageBox.Show(
                    Resources.error_category, Resources.dialog_messages_error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void OnListBoxIndexChange(object sender, EventArgs e)
        {
            if (listFilesLoaded.SelectedItem != null)
            {
                toolMenuImage.Enabled = actionButtonZoomIn.Enabled = actionButtonZoomOut.Enabled = actionButtonZoomRestore.Enabled = true;
                var description = listFilesLoaded.SelectedItem as ResultImage;
                textDescription.Text = description.FormattedDescription;
                SetZoomPercentage(100); //Default value
                if (description.NeedAnimation)
                {
                    _gifPictures = new GifRenderLayer(pictureBox.Image);
                    pictureBox.Image = _gifPictures.GetFrame(0);
                    pictureBox.Invalidate();
                }
                else
                {
                    _gifPictures = null;
                    pictureBox.Image = description.ImageItself;
                    pictureBox.Invalidate();
                }
            }
            else
            {
                pictureBox.Image = Resources.image_icon;
                textDescription.Text = "";
                toolMenuImage.Enabled = actionButtonZoomIn.Enabled = actionButtonZoomOut.Enabled = actionButtonZoomRestore.Enabled = false;
            }
        }

        private void OnApiListIndexChange(object sender, EventArgs e)
        {
            if (listAvailableApi.SelectedItem != null)
            {
                CallNewApi();
            }
        }

        private void OnSaveImageClick(object sender, EventArgs e)
        {
            if (listFilesLoaded.SelectedItem == null) return;
            var image = listFilesLoaded.SelectedItem as ResultImage;
            var saveDialog = new SaveFileDialog();
            saveDialog.FileName = image.ImageName;
            saveDialog.Filter = image.NeedAnimation ? "GIF File (*.gif)|*.gif" : "Picture File (*.png)|*.png";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                image.ImageItself.Save(saveDialog.FileName, image.NeedAnimation ? ImageFormat.Gif : ImageFormat.Png);
            }
        }

        private void OnCopyURLClick(object sender, EventArgs e)
        {
            if (listFilesLoaded.SelectedItem == null) return;
            var image = listFilesLoaded.SelectedItem as ResultImage;
            Clipboard.SetText(image.SourceUrl);
        }

        private void OnCopyImageClick(object sender, EventArgs e)
        {
            if (listFilesLoaded.SelectedItem == null) return;
            var image = listFilesLoaded.SelectedItem as ResultImage;
            if (!image.NeedAnimation)
            {
                Clipboard.SetImage(image.ImageItself);
                return;
            }
            var uniqueTempFilePath = Path.Combine(Path.GetTempPath(), image.ImageName + ".gif");
            image.ImageItself.Save(uniqueTempFilePath, ImageFormat.Gif);
            Clipboard.SetFileDropList(new StringCollection { uniqueTempFilePath });
            Console.WriteLine(uniqueTempFilePath);
        }

        private void OnZoomInImage(object sender, EventArgs e)
        {
            if (_percentZoom >= 200) return;
            SetZoomPercentage(_percentZoom + 10);
            UpdateZoomPicture();
        }

        private void OnZoomOutImage(object sender, EventArgs e)
        {
            if (_percentZoom <= 10) return;
            SetZoomPercentage(_percentZoom - 10);
            UpdateZoomPicture();
        }

        private void OnZoomRestoreImage(object sender, EventArgs e)
        {
            SetZoomPercentage(100);
            UpdateZoomPicture();
        }

        private void OnCopyResizedImageClick(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox.Image);
        }

        private void OnButtonDeleteAllClick(object sender, EventArgs e)
        {
            var k = MessageBox.Show(this, "Are you sure you want to delete everything?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (k != DialogResult.Yes) return;
            foreach (var a in listFilesLoaded.Items)
            {
                ((ResultImage)a).ImageItself.Dispose();
            }
            listFilesLoaded.Items.Clear();
            listFilesLoaded.SelectedItem = null;
            OnListBoxIndexChange(sender, e);
        }

        private void buttonDownloadAll_Click(object sender, EventArgs e)
        {           
            var k = MessageBox.Show(this, $"Are you sure you want to downlaod {listFilesLoaded.Items.Count} images?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (k != DialogResult.Yes) return;
            foreach (var item in listFilesLoaded.Items)
            {
                var image = item as ResultImage;
                image.ImageItself.Save(image.ImageName + (image.NeedAnimation ? ".gif" : ".png"), image.NeedAnimation ? ImageFormat.Gif : ImageFormat.Png);
            }
        }
    }
}