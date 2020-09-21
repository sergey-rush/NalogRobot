using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace NalogRobot
{
    public partial class SyncForm : Form
    {
        private FileForm fileForm;

        public SyncForm(FileForm fileForm)
        {
            InitializeComponent();
            this.fileForm = fileForm;
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string targetDir;
        private List<FileInfo> fileList;
        private List<Session> recordList;
        

        private void SyncForm_Load(object sender, EventArgs e)
        {
            targetDir = Settings.TargetDir;
            RefreshStat();
        }

        private void RefreshStat()
        {
            EnlistFiles(targetDir);
            recordList = Data.Instance.GroupByDestFile();
            lbxStat.Items.Add($"Файлов: {fileList.Count}");
            lbxStat.Items.Add($"Записей: {recordList.Count}");
        }

        private void EnlistFiles(string path)
        {
            try
            {
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                DirectoryInfo di = new DirectoryInfo(path);
                fileList = di.EnumerateFiles().ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SyncForm EnlistFiles", null);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Операция синхронизации может занять некоторое время. Продолжить?", "Синхронизация", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                bool removeEmptyRecords = chbRemoveRecords.Checked;
                SyncDataWithFiles(removeEmptyRecords);
                bool deleteFiles = chbDelete.Checked;
                SyncFilesWithData(deleteFiles);
                RefreshStat();
                fileForm.Bind();
            }
        }

        private void SyncFilesWithData(bool deleteFiles)
        {
            if (deleteFiles)
            {
                logger.Info("Sync files with database started with deleteFiles: {0}", deleteFiles);

                recordList = Data.Instance.GroupByDestFile();

                foreach (FileInfo fi in fileList)
                {
                    bool found = false;

                    foreach (Session ss in recordList)
                    {
                        if (fi.FullName == ss.Name)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        logger.Info("App is about to remove file {0} from target folder", fi.FullName);
                        if (File.Exists(fi.FullName))
                        {
                            File.Delete(fi.FullName);
                        }
                    }
                }
            }

            logger.Info("Sync files with database completed");
        }

        private void SyncDataWithFiles(bool removeEmptyRecords)
        {
            logger.Info("Sync data with files in target folder started with removeEmptyRecords: {0}", removeEmptyRecords);

            foreach (Session ss in recordList)
            {
                bool found = false;

                foreach (FileInfo fi in fileList)
                {
                    if (ss.Name == fi.FullName)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    logger.Info("App is about to delete record {0} from database", ss.Name);
                    Data.Instance.DeleteTaxByDestFile(ss.Name);
                }
            }

            Data.Instance.FinalizeSync(removeEmptyRecords);

            logger.Info("Sync data with files in target folder completed");
        }
    }
}
