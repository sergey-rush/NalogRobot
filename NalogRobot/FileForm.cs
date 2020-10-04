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

namespace NalogRobot
{
    public partial class FileForm : Form
    {
        private List<Tax> taxList;
        private List<Session> sessions;
        private Session selectedSession;
        public FileForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            int limit = 2000;
            string term = txbSearch.Text;
            taxList = Data.Instance.GetTaxList(term, limit, selectedSession.SessionId);
            dgvTaxGrid.DataSource = taxList;
            dgvTaxGrid.Refresh();
        }

        private void FileForm_Load(object sender, EventArgs e)
        {
            Bind();
            //string regNum = "1003605968";
            //Tax tax = Data.Instance.GetTaxByRegNum(regNum);
            //int taxCount = Data.Instance.CountByRegNum(regNum, ImportState.Created);
        }

        public void Bind()
        {
            cmbSessions.DisplayMember = "Name";
            cmbSessions.ValueMember = "SessionId";
            sessions = Data.Instance.GetSessions();
            cmbSessions.DataSource = sessions;
            if (sessions.Count > 0)
            {
                cmbSessions.Enabled = true;
                btnDelete.Enabled = true;
                btnSync.Enabled = true;
                btnExport.Enabled = true;
                cmbSessions.SelectedIndex = 0;
                selectedSession = sessions[0];
                DoSearch();
            }
            else
            {
                cmbSessions.Enabled = false;
                btnDelete.Enabled = false;
                btnSync.Enabled = false;
                btnExport.Enabled = false;
                dgvTaxGrid.DataSource = null;
                dgvTaxGrid.Refresh();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Вы действительно желаете удалить данные этой сессии?", "Подтверждение",  MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Data.Instance.DeleteTaxListBySessionId(selectedSession.SessionId);
                Bind();
            }
        }

        private void cmbSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session session = (Session)cmbSessions.SelectedItem;
            selectedSession = session;
            DoSearch();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int rowCount = dgvTaxGrid.RowCount;
            int columnCount = dgvTaxGrid.ColumnCount;

            // get column headers
            for (int currentCol = 0; currentCol < columnCount; currentCol++)
            {
                sb.Append(dgvTaxGrid.Columns[currentCol].HeaderText);
                if (currentCol < columnCount - 1)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            // get the rows data
            for (int currentRow = 0; currentRow < rowCount; currentRow++)
            {
                if (!dgvTaxGrid.Rows[currentRow].IsNewRow)
                {
                    for (int currentCol = 0; currentCol < columnCount; currentCol++)
                    {
                        if (dgvTaxGrid.Rows[currentRow].Cells[currentCol].Value != null)
                        {
                            sb.Append(dgvTaxGrid.Rows[currentRow].Cells[currentCol].Value.ToString());
                        }
                        if (currentCol < columnCount - 1)
                        {
                            sb.Append(",");
                        }
                        else
                        {
                            sb.AppendLine();
                        }
                    }
                }
            }
            
            File.WriteAllText("Export_" + selectedSession.SessionId + ".csv", sb.ToString(), Encoding.Default);
            MessageBox.Show("Сессия " + selectedSession.Name + " экспортирована", "Экспорт", MessageBoxButtons.OK);
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncForm syncForm = new SyncForm(this);
            syncForm.Show();

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("Будет произведена синхронизация файлов в каталоге и базе данных");
            //sb.AppendLine("Отсутствующие файлы в каталоге будут удалены из базы данных.");
            //sb.AppendLine("Отметьте "Удалить файлы", чтобы удалить из каталога файлы незарегистрированные в базе данных.");
            //sb.AppendLine("Операция может занять некоторое время. Продолжить?");

            //var confirmResult = MessageBox.Show(sb.ToString(), "Синхронизация", MessageBoxButtons.YesNo);
            //if (confirmResult == DialogResult.Yes)
            //{
            //    //Data.Instance.DeleteTaxListBySessionId(selectedSession.SessionId);
            //    //Bind();
            //}
        }
    }
}
