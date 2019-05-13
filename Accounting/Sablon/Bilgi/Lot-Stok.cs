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

namespace Accounting.Bilgi
{
    public partial class Lot_Stok : Form
    {
        AccountingDBDataContext _db = new AccountingDBDataContext();
        public bool Secim = false;
        public string lot = "A";
        public Lot_Stok()
        {
            InitializeComponent();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
        void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var lst = (from s in _db.tblStocks
                       where s.LotSerial.Contains(txtLotBul.Text)
                       select new
                       {
                           p = s.LotSerial,
                           n = s.Quantity

                       }).Distinct().OrderByDescending(x => x.n);
            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.p;
                Liste.Rows[i].Cells[1].Value = k.n;
                
                i++;
            }
            Liste.AllowUserToAddRows = false;
            Liste.ReadOnly = true;
        }
        void Sec()
        {
            try
            {
                Secim = true;
                lot= Liste.CurrentRow.Cells[0].Value.ToString();
            }
            catch (Exception)
            {
                lot = "A";
            }
        }

        private void Lot_Stok_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Liste_DoubleClick(object sender, EventArgs e)
        {
            Sec();
            if (Secim && lot!="A")
            {
                frmAnaSayfa.LotSeri = lot;
                Close();
            }
        }
    }
}
