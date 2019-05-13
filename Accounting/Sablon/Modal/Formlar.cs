using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.Modal
{
    class Formlar
    {
        #region InformationEntry

        public int Category(bool secim=false)
        {
            Bilgi.frmCategoryEntry frm = new Bilgi.frmCategoryEntry();
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.ShowDialog();
            }
            return frmAnaSayfa.Aktarma;
        }
        public int Shippers(bool secim = false)
        {
            Bilgi.frmShippers frm = new Bilgi.frmShippers();
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.ShowDialog();
            }
            return frmAnaSayfa.Aktarma;
        }

        public int KulGiris(bool secim=false)
        {
            Bilgi.frmEmployee frm = new Bilgi.frmEmployee();           
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.ShowDialog();
            }
            return frmAnaSayfa.Aktarma;            
        }

        public void FirmaGiris()
        {
            Bilgi.frmCompany frm = new Bilgi.frmCompany();
            frm.MdiParent = Form.ActiveForm;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        public int FirmaList(bool secim=false)
        {
            Bilgi.frmCompanyList frm = new Bilgi.frmCompanyList();
            if(secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.MdiParent = Form.ActiveForm;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
            return frmAnaSayfa.Aktarma;
        }
        public void UrunGiris()
        {
            Bilgi.frmProduct frm = new Bilgi.frmProduct();
            frm.MdiParent = Form.ActiveForm;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        public int UrunList(bool secim = false)
        {
            Bilgi.frmProductList frm = new Bilgi.frmProductList();
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.MdiParent = Form.ActiveForm;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
            return frmAnaSayfa.Aktarma;
        }

        #endregion
        #region SalesProcess
        public void Alis()
        {
            AlSat.frmAlis frm = new AlSat.frmAlis();
            frm.MdiParent = Form.ActiveForm;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        public int AlisList(bool secim = false)
        {
            AlSat.frmAlisListe frm = new AlSat.frmAlisListe();
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.MdiParent = Form.ActiveForm;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
            return frmAnaSayfa.Aktarma;
        }
        public void Satis()
        {
            AlSat.frmSatis frm = new AlSat.frmSatis();
            frm.MdiParent = Form.ActiveForm;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        public int SatisList(bool secim=false)
        {
            AlSat.frmSatisListe frm = new AlSat.frmSatisListe();
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
            else
            {
                frm.MdiParent = Form.ActiveForm;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
            return frmAnaSayfa.Aktarma;
        }
        public string LotSeri(bool secim=false)
        {
            Bilgi.Lot_Stok frm = new Bilgi.Lot_Stok();
            if (secim)
            {
                frm.Secim = true;
                frm.ShowDialog();
            }
           
            return frmAnaSayfa.LotSeri;
        }
        #endregion

        #region StockProcess

        #endregion
    }
}
