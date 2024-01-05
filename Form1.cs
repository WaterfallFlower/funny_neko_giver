using System;
using System.Windows.Forms;

namespace funny_neko_giver
{
    public partial class Form1 : Form
    {
        public NekoAccess apiInstance;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetProgressMaxValue(int i)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = i;
        }

        private void UpdateProgressBarValue(string s)
        {
            progressBar1.PerformStep();
            groupBox3.Text = s;
        }

        private void FinalizeProgressBarValue()
        {
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 0;
            groupBox3.Text = "Completed!";
        }

        private void OnLoadingForm(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 0;
            progressBar1.Step = 1;
            groupBox3.Text = "No tasks running...";

            apiInstance = new NekoAccess();
            apiInstance.Init(
                stringError =>
                {
                    MessageBox.Show($"Failed to connect API!\n{stringError}", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    button_generate.Enabled = list_category.Enabled = false;
                },
                instance =>
                {
                    foreach (var i in instance.GetCategories())
                    {
                        list_category.Items.Add(i);
                    }
                });
        }

        private void button_generate_Click(object sender, EventArgs e)
        {
            if (list_category.SelectedIndex >= 0)
            {
                SetProgressMaxValue(2 + (int)num_amount.Value);
                button_generate.Enabled = num_amount.Enabled = false;
                apiInstance.LoadCategoryImage(list_category.SelectedItem as CategoryImage, (int)num_amount.Value,
                    stringError =>
                    {
                        button_generate.Enabled = num_amount.Enabled = true;
                        MessageBox.Show($"Failed to connect API!\n{stringError}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    },
                    obtainedFile =>
                    {
                        button_generate.Enabled = num_amount.Enabled = true;
                        list_files_loaded.Items.Add(obtainedFile);
                        list_files_loaded.SelectedItem = obtainedFile;
                    },
                    UpdateProgressBarValue, FinalizeProgressBarValue);
            }
            else
            {
                MessageBox.Show("Please specify category!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selected_index_change(object sender, EventArgs e)
        {
            if (list_files_loaded.SelectedItem != null)
            {
                var description = list_files_loaded.SelectedItem as FileDescription;
                richTextBox1.Text =
                    $"Artist: {description.ArtistName}\nAuthor Page: {description.ArtistName}\nURL: {description.SourceUrl}";
                pictureBox1.Image = description.ImageItself;
            }
            else
            {
                pictureBox1.Image = null;
                richTextBox1.Text = "Artist:\nAuthor Page:\nURL:";
            }
        }
    }
}