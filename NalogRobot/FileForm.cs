using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NalogRobot
{
    public partial class FileForm : Form
    {
        public FileForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Tax tax = new Tax();
            //tax.RegNum = "Registration";
            //tax.ImportState = ImportState.Completed;
            //tax.Updated = DateTime.Now;
            //tax.TempFile = "TemporalFile.xlsx";
            //tax.DestFile = "DestinationFile.xlsx";
            //tax.Id = Data.Instance.InsertTax(tax);

            //bool res = Data.Instance.UpdateTax(tax);
            int limit = 1000;
            string term = txbSearch.Text;
            List<Tax> taxList = Data.Instance.GetTaxList(term, limit);
            dgvTaxGrid.DataSource = taxList;
            dgvTaxGrid.Refresh();
        }
    }
}
