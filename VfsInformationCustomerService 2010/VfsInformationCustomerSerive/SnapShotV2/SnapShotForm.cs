using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vfs.WebCrawler.Destination.Business;
using SnapShot;

namespace SnapShotV2
{
    public partial class SnapShotFrm : Form
    {
        private FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
        public SnapShotFrm()
        {
            InitializeComponent();
            LoadComponent();
        }
        private void LoadComponent()
        {
            NameFilecomboBox.Items.Clear();
            NameFilecomboBox.Items.Add("VolumeReview");
            NameFilecomboBox.Items.Add("SnapShot");

        }

        private void SelectFilebutton_Click(object sender, EventArgs e)
        {
            snapShotFileDialog.InitialDirectory = "c:\\";
            snapShotFileDialog.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";

            if (snapShotFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.SnapShotFileTextBox.Text = snapShotFileDialog.FileName;

            }
        }

        private void CreateSnapShotButton_Click(object sender, EventArgs e)
        {
            string HoseFilePath = string.Empty;
            string HnxFilePath = string.Empty;
            #region Error Infomation
            if (NameFilecomboBox.Text == string.Empty)
            {
                MessageBox.Show(Properties.Resources.NameFiletextBox, Properties.Resources.ErrorInfoTitle);
                NameFilecomboBox.Focus();
                return;
            }
            if (NameCelltextBox.Text == string.Empty)
            {
                MessageBox.Show(Properties.Resources.NameCelltextBox, Properties.Resources.ErrorInfoTitle);
                NameCelltextBox.Focus();
                return;
            }
            if (SnapShotFileTextBox.Text == string.Empty)
            {
                MessageBox.Show(Properties.Resources.SnapShotFileTextBox, Properties.Resources.ErrorInfoTitle);
                SnapShotFileTextBox.Focus();
                return;
            }
            if (NameFolderHOSEtextBox.Text == string.Empty)
            {
                MessageBox.Show(Properties.Resources.NameFolderHOSEtextBox, Properties.Resources.ErrorInfoTitle);
                NameFolderHOSEtextBox.Focus();
                return;
            }
            if (NameFolderHNXtextBox.Text == string.Empty)
            {
                MessageBox.Show(Properties.Resources.NameFolderHNXtextBox, Properties.Resources.ErrorInfoTitle);
                NameFolderHNXtextBox.Focus();
                return;
            }
            #endregion
            string fileNameSnapShot = NameFilecomboBox.Text + ".xls";
            string CellNameSnapShot = NameCelltextBox.Text;
            try
            {
                HoseFilePath = NameFolderHOSEtextBox.Text + "\\";
                HnxFilePath = NameFolderHNXtextBox.Text + "\\";
                Vfs.WebCrawler.Destination.Utility.UploadService.Info("Start creating SnapShot");
                Vfs.WebCrawler.Destination.Business.SnapShotService snapShotService = new SnapShotService();
                snapShotService.NameSheet = ApplicationHelper.NameSheet;
                snapShotService.CreateSnapShot(this.SnapShotFileTextBox.Text, HoseFilePath, HnxFilePath, fileNameSnapShot, CellNameSnapShot);
                Vfs.WebCrawler.Destination.Utility.UploadService.Info("End of creating SnapShot");
            }
            catch (Exception ex)
            {
                Vfs.WebCrawler.Destination.Utility.UploadService.Error(ex.Message);
            }
            #region Copy

            string fileSoureHOSE, fileTargetHOSE, fileSoureHNX, fileTargetHNX;

            fileSoureHOSE = ApplicationHelper.GetSourceFolerHOSE;
            fileTargetHOSE = ApplicationHelper.GetTargetFolerHOSE;

            fileSoureHNX = ApplicationHelper.GetSourceFolerHNX;
            fileTargetHNX = ApplicationHelper.GetTargetFolerHNX;

            try
            {
                Console.WriteLine("--------HOSE-------");
                ApplicationHelper.CopyDirectory(fileSoureHOSE, fileTargetHOSE);
                Console.WriteLine("--------HNX--------");
                ApplicationHelper.CopyDirectory(fileSoureHNX, fileTargetHNX);
            }
            catch
            {
                Console.WriteLine("Ko the copy");
            }
            #endregion
        }


        private void buttonSelectFolderHOSE_Click(object sender, EventArgs e)
        {
            string s;
            selectFolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                NameFolderHOSEtextBox.Text = selectFolderDialog.SelectedPath;
                s = NameFolderHNXtextBox.Text;
            }
        }

        private void buttonSelectFolderHNX_Click(object sender, EventArgs e)
        {
            selectFolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                NameFolderHNXtextBox.Text = selectFolderDialog.SelectedPath;
            }
        }

        private void NameFilecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.NameFilecomboBox.Text == "SnapShot")
                NameCelltextBox.Text = "I6";
            else
                NameCelltextBox.Text = "J2";
        }
    }
}