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
        }

        private void Bind()
        {
            cmbSessions.DisplayMember = "Name";
            cmbSessions.ValueMember = "SessionId";
            sessions = Data.Instance.GetSessions();
            cmbSessions.DataSource = sessions;
            cmbSessions.SelectedIndex = 0;

            selectedSession = sessions[0];
            DoSearch();
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
            int RowCount = dgvTaxGrid.RowCount;
            int ColumnCount = dgvTaxGrid.ColumnCount;

            // get column headers
            for (int currentCol = 0; currentCol < ColumnCount; currentCol++)
            {
                sb.Append(dgvTaxGrid.Columns[currentCol].HeaderText);
                if (currentCol < ColumnCount - 1)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            // get the rows data
            for (int currentRow = 0; currentRow < RowCount; currentRow++)
            {
                if (!dgvTaxGrid.Rows[currentRow].IsNewRow)
                {
                    for (int currentCol = 0; currentCol < ColumnCount; currentCol++)
                    {
                        if (dgvTaxGrid.Rows[currentRow].Cells[currentCol].Value != null)
                        {
                            sb.Append(dgvTaxGrid.Rows[currentRow].Cells[currentCol].Value.ToString());
                        }
                        if (currentCol < ColumnCount - 1)
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
        }
    }
}
