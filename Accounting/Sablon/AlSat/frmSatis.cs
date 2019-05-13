using Accounting.Modal;
using Accounting.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.AlSat
{
    public partial class frmSatis : Form
    {
        Modal.AccountingDBDataContext _db = new AccountingDBDataContext();
        Formlar _f = new Formlar();
        Mesajlar _m = new Mesajlar();
        Numaralar _n = new Numaralar();
        int _sid = -1;
        int _firmaId = -1;
        int _perId = -1;
        int _shipId = -1;
        string lot = "A";
        public string[] MyArray { get; set; }
        public frmSatis()
        {
            InitializeComponent();
        }

        private void frmSatis_Load(object sender, EventArgs e)
        {
            Temizle();
            Combo();
        }
        void Combo()
        {
            urncmb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection veri = new AutoCompleteStringCollection();
            var lst = _db.tblProducts.Select(s => s.Name).Distinct();
            foreach (string urn in lst)
            {
                veri.Add(urn);
                urncmb.Items.Add(urn);
            }
            urncmb.AutoCompleteCustomSource = veri;

            int dgv;
            dgv = urncmb.Items.Count;
            MyArray = new string[dgv];
            for (int p = 0; p < dgv; p++)
            {
                MyArray[p] = urncmb.Items[p].ToString();
            }
        }
        void Temizle()
        {
            txtSatisNo.Text = _n.SatisNo();
            Liste.Rows.Clear();
            txtFirma.Text = "";
            txtPer.Text = "";
            txtKargo.Text = "";
            dtpTarih.Text = DateTime.Now.ToShortDateString();
            cbSehir.DataSource = _db.tblCities;
            cbSehir.ValueMember = "ID";
            cbSehir.DisplayMember = "City";
            cbSehir.SelectedIndex = 33;
        }
        void YeniKaydet()
        {
            Liste.AllowUserToAddRows = false;
            try
            {
                tblSalesDown[] sud = new tblSalesDown[Liste.RowCount];
                tblSalesUp sup = new tblSalesUp();

                
                for (int i = 0; i < Liste.RowCount; i++)
                {
                    int pid = int.Parse(Liste.Rows[i].Cells[0].Value.ToString());
                    string lot = Liste.Rows[i].Cells[2].Value.ToString();

                    sud[i] = new tblSalesDown();
                    sud[i].SalesID = int.Parse(txtSatisNo.Text);
                    sud[i].ProductID = pid;
                    sud[i].LotSerial = lot;
                    sud[i].SalesPrice = decimal.Parse(Liste.Rows[i].Cells[4].Value.ToString());
                    sud[i].Quantity = int.Parse(Liste.Rows[i].Cells[5].Value.ToString());
                    

                    sup.SalesID = int.Parse(txtSatisNo.Text);
                    sup.CompanyID = _db.tblCompanies.First(x => x.Name == txtFirma.Text).ID;
                    sup.Date = DateTime.Parse(dtpTarih.Text);
                    sup.CityID = _db.tblCities.First(x => x.City == cbSehir.Text).Id;
                    sup.EmployeeID = _db.tblEmployees.First(x => x.Name == txtPer.Text).ID;
                    sup.ShipperID = _db.tblShippers.First(x => x.Name == txtKargo.Text).ID;

                    AccountingDBDataContext _gb = new AccountingDBDataContext();
                    tblStock st = _gb.tblStocks.First(x => x.ProductID == pid && x.LotSerial == lot);
                    if (st.Quantity.Value - int.Parse(Liste.Rows[i].Cells[5].Value.ToString()) > 0)
                    {
                        st.Quantity -= int.Parse(Liste.Rows[i].Cells[5].Value.ToString());
                        _gb.SubmitChanges();
                        _db.tblSalesDowns.InsertOnSubmit(sud[i]);
                        _db.tblSalesUps.InsertOnSubmit(sup);
                    }

                    else

                    {
                        MessageBox.Show("Elimizde Yeterli Ürün Yok!");
                    }
                }
                _db.SubmitChanges();
                _m.YeniKayit("Kayıt başarılı.");
            }
            catch (Exception e)
            {
                _m.Hata(e);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            var btnsano = new Button();
            btnsano.Size = new Size(25, txtSatisNo.ClientSize.Height + 2);
            btnsano.Location = new Point(txtSatisNo.ClientSize.Width - btnsano.Width, -1);
            btnsano.Cursor = Cursors.Default;
            btnsano.Image = Resources.arrow1;
            txtSatisNo.Controls.Add(btnsano);
            SendMessage(txtSatisNo.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnsano.Width << 16));
            btnsano.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            var btnfir = new Button();
            btnfir.Size = new Size(25, txtFirma.ClientSize.Height + 2);
            btnfir.Location = new Point(txtFirma.ClientSize.Width - btnfir.Width, -1);
            btnfir.Cursor = Cursors.Default;
            btnfir.Image = Resources.arrow1;
            txtFirma.Controls.Add(btnfir);
            SendMessage(txtFirma.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnfir.Width << 16));
            btnfir.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            var btnper = new Button();
            btnper.Size = new Size(25, txtPer.ClientSize.Height + 2);
            btnper.Location = new Point(txtPer.ClientSize.Width - btnper.Width, -1);
            btnper.Cursor = Cursors.Default;
            btnper.Image = Resources.arrow1;
            txtPer.Controls.Add(btnper);
            SendMessage(txtPer.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnper.Width << 16));
            btnper.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            var btnkar = new Button();
            btnkar.Size = new Size(25, txtKargo.ClientSize.Height + 2);
            btnkar.Location = new Point(txtKargo.ClientSize.Width - btnkar.Width, -1);
            btnkar.Cursor = Cursors.Default;
            btnkar.Image = Resources.arrow1;
            txtKargo.Controls.Add(btnkar);
            SendMessage(txtKargo.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnkar.Width << 16));
            btnkar.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            base.OnLoad(e);

            btnsano.Click += btnsano_Click;
            btnfir.Click += btnfir_Click;
            btnper.Click += btnper_Click;
            btnkar.Click += btnkar_Click;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void btnsano_Click(object sender, EventArgs e)
        {
            int id = _f.SatisList(true);
            if (id > 0)
            {
                Ac(id);
            }
            frmAnaSayfa.Aktarma = -1;
        }
        private void btnfir_Click(object sender, EventArgs e)
        {
            int id = _f.FirmaList(true);
            if (id > 0)
            {
                FirmaAc(id);
            }
            frmAnaSayfa.Aktarma = -1;
        }

        private void btnper_Click(object sender, EventArgs e)
        {
            int id = _f.KulGiris(true);
            if (id > 0)
            {
                PerAc(id);
            }
            frmAnaSayfa.Aktarma = -1;
        }
        private void btnkar_Click(object sender, EventArgs e)
        {
            int id = _f.Shippers(true);
            if (id > 0)
            {
                KargoAc(id);
            }
            frmAnaSayfa.Aktarma = -1;
        }
        public void Ac(int id)
        {
            Liste.Rows.Clear();
            _sid = id;
            tblSalesUp pur = _db.tblSalesUps.First(x => x.SalesID==_sid);
            txtSatisNo.Text = pur.SalesID.ToString().PadLeft(7, '0');
            txtFirma.Text = pur.tblCompany.Name;
            dtpTarih.Text = pur.Date.Value.ToString();
            cbSehir.Text = pur.tblCity.City;
            txtPer.Text = pur.tblEmployee.Name;
            txtKargo.Text = pur.tblShipper.Name;
            int i = 0;
            var srg = from s in _db.tblSalesDowns
                      where s.SalesID == _sid
                      select s;
            foreach (tblSalesDown k in srg)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.ProductID;
                Liste.Rows[i].Cells[1].Value = k.tblProduct.Name;
  
                Liste.Rows[i].Cells[4].Value = k.SalesPrice;
                Liste.Rows[i].Cells[5].Value = k.Quantity;
                i++;
            }
        }
        public void FirmaAc(int id)
        {
            _firmaId = id;
            txtFirma.Text = _db.tblCompanies.First(x => x.ID == _firmaId).Name;
        }
        public void PerAc(int id)
        {
            _perId = id;
            txtPer.Text = _db.tblEmployees.First(x => x.ID == _perId).Name;
        }
        public void KargoAc(int id)
        {
            _shipId = id;
            txtKargo.Text = _db.tblShippers.First(x => x.ID == _shipId).Name;
        }
        public void LotAc(string a)
        {
            lot = a;
            int i = 0;
            var srg = from s in _db.tblStocks where s.LotSerial == lot select s;
            foreach (tblStock k in srg)
            {
                Liste.Rows[i].Cells[2].Value = k.LotSerial;
                Liste.Rows[i].Cells[3].Value = k.Quantity;
                i++;
            }
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            YeniKaydet();
        }

        private void Liste_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Liste_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = e.Control as TextBox;
            if (Liste.CurrentCell.ColumnIndex == 1 && txt != null)
            {
                txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.AutoCompleteCustomSource.AddRange(MyArray);
            }
            else if (Liste.CurrentCell.ColumnIndex != 1 && txt != null)
            {
                txt.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        private void Liste_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DataGridViewButtonColumn bt = new DataGridViewButtonColumn();
       
                _f.LotSeri();
                string a = _f.LotSeri(true);
                if (a != "A")
                {
                    LotAc(a);
                }
                frmAnaSayfa.LotSeri = "A";
                
            }
        }
    }
}
