using Accounting.Modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.AlSat
{
    public partial class frmSatisListe : Form
    {
        AccountingDBDataContext _db = new AccountingDBDataContext();
        public bool Secim = false;
        public int slId = -1;
        public frmSatisListe()
        {
            InitializeComponent();
        }

        private void frmSatisListe_Load(object sender, EventArgs e)
        {
            Listele();
        }
        public void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var lst = (from s in _db.tblSalesUps
                       where s.tblCompany.Name.Contains(txtFirmaBul.Text)
                       select new
                       {
                           p = s.SalesID,
                           n = s.tblCompany.Name,
                           d = s.Date
                       }).Distinct().OrderByDescending(x => x.d).OrderBy(y => y.n);
            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.p;
                Liste.Rows[i].Cells[1].Value = k.n;
                Liste.Rows[i].Cells[2].Value = k.d;
                i++;
            }
            Liste.AllowUserToAddRows = false;
            Liste.ReadOnly = true;
        }
        void Sec()
        {
            try
            {
                slId = int.Parse(Liste.CurrentRow.Cells[0].Value.ToString());
            }
            catch (Exception)
            {
                slId = -1;
            }
        }

        private void Liste_DoubleClick(object sender, EventArgs e)
        {
            Sec();
            if (Secim && slId > 0)
            {
                frmAnaSayfa.Aktarma = slId;
                Close();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
