﻿using System;
using System.Windows.Forms;
using funny_neko_giver.Properties;

namespace funny_neko_giver
{
    public partial class FormMain : Form
    {
        private NekoAccess _apiInstance;

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

        private void OnFormLoad(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            progressBar.Maximum = 0;
            progressBar.Step = 1;
            
            groupBoxProgress.Text = Resources.progress_notasksrunning;
            groupBoxDescription.Text = Resources.form_file_description;
            groupBoxMain.Text = Resources.form_configure;
            buttonLoad.Text = Resources.form_button_load;
            labelAmount.Text = Resources.form_label_amount;
            buttonDeleteAll.Text = Resources.form_button_deleteall;
            buttonDownloadAll.Text = Resources.form_button_downloadall;
            toolMenuMain.Text = Resources.form_tool_strip_main;
            actionButtonLoad.Text = Resources.form_button_load;
            

            _apiInstance = new NekoAccess();
            _apiInstance.Init(
                stringError =>
                {
                    MessageBox.Show(string.Format(Resources.error_connectapi, stringError), Resources.dialog_messages_error, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    buttonLoad.Enabled = listCategory.Enabled = false;
                },
                instance =>
                {
                    foreach (var i in instance.GetCategories())
                    {
                        listCategory.Items.Add(i);
                    }
                });
        }

        private void OnLoadButtonClick(object sender, EventArgs e)
        {
            if (listCategory.SelectedIndex >= 0)
            {
                SetProgressMaxValue(2 + (int)numAmount.Value);
                buttonLoad.Enabled = numAmount.Enabled = false;
                _apiInstance.LoadCategoryImage(listCategory.SelectedItem as CategoryImage, (int)numAmount.Value,
                    stringError =>
                    {
                        buttonLoad.Enabled = numAmount.Enabled = true;
                        MessageBox.Show(string.Format(Resources.error_connectapi, stringError), Resources.dialog_messages_error, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    },
                    obtainedFile =>
                    {
                        buttonLoad.Enabled = numAmount.Enabled = true;
                        listFilesLoaded.Items.Add(obtainedFile);
                        listFilesLoaded.SelectedItem = obtainedFile;
                    },
                    UpdateProgressBarValue, FinalizeProgressBarValue);
            }
            else
            {
                MessageBox.Show(Resources.error_category, Resources.dialog_messages_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnComboBoxIndexChange(object sender, EventArgs e)
        {
            if (listFilesLoaded.SelectedItem != null)
            {
                var description = listFilesLoaded.SelectedItem as FileDescription;
                textDescription.Text =
                    string.Format(Resources.result_search_filled, description.ArtistName, description.ArtistName, description.SourceUrl);
                pictureBox.Image = description.ImageItself;
            }
            else
            {
                pictureBox.Image = null;
                textDescription.Text = Resources.result_search_empty;
            }
        }
    }
}