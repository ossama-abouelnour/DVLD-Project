using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Tests.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();

            if(_dtAllTestTypes.Rows.Count > 0)
            {
                dgvListTestTypes.DataSource = _dtAllTestTypes;

                dgvListTestTypes.Columns[0].HeaderText = "ID";
                dgvListTestTypes.Columns[0].Width = 100;

                dgvListTestTypes.Columns[1].HeaderText = "Title";
                dgvListTestTypes.Columns[1].Width = 150;

                dgvListTestTypes.Columns[2].HeaderText = "Description";
                dgvListTestTypes.Columns[2].Width = 380;

                dgvListTestTypes.Columns[3].HeaderText = "Fees";
                dgvListTestTypes.Columns[3].Width = 100;
            }
        }
    }
}
