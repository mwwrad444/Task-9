using System;
using System.IO;
using System.Windows.Forms;

namespace FaylMeneceri
{
    public partial class Form1 : Form
    {
        string secilenQovluqYolu = "";

        public Form1()
        {
            InitializeComponent();
        }

               private void btnQovluq_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                secilenQovluqYolu = fbd.SelectedPath;
                txtYol.Text = secilenQovluqYolu;
                FayllariYukle();
            }
        }

        
        private void FayllariYukle()
        {
            lstFayllar.Items.Clear();

            
            string[] fayllar = Directory.GetFiles(secilenQovluqYolu, "*.txt");

            foreach (string fayl in fayllar)
            {
                
                lstFayllar.Items.Add(Path.GetFileName(fayl));
            }

            
            lblStatus.Text = "Hazırdır | Fayl sayı: " + lstFayllar.Items.Count;
        }

        
        private void btnAc_Click(object sender, EventArgs e)
        {
            if (lstFayllar.SelectedItem != null)
            {
                string fayl Adi = lstFayllar.SelectedItem.ToString();
                string tamYol = Path.Combine(secilenQovluqYolu, faylAdi);

                
                txtMetn.Text = File.ReadAllText(tamYol);

                
                FileInfo fi = new FileInfo(tamYol);
                double olcuKb = fi.Length / 1024.0; 

                lblMelumat.Text = $"Ad: {fi.Name}   Ölçü: {olcuKb:F1} KB   Yaranma tarixi: {fi.CreationTime.ToShortDateString()}";
            }
            else
            {
                MessageBox.Show("Zəhmət olmasa siyahıdan bir fayl seçin!", "Diqqət", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        private void btnSaxla_Click(object sender, EventArgs e)
        {
            if (secilenQovluqYolu == "")
            {
                MessageBox.Show("Əvvəlcə qovluq seçin!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            string yeniFaylAdi = "yeni_qeyd.txt";
            string tamYol = Path.Combine(secilenQovluqYolu, yeniFaylAdi);

            
            File.WriteAllText(tamYol, txtMetn.Text);

            MessageBox.Show("Fayl uğurla yadda saxlanıldı!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            
            FayllariYukle();
        }

        
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstFayllar.SelectedItem != null)
            {
                string faylAdi = lstFayllar.SelectedItem.ToString();
                string tamYol = Path.Combine(secilenQovluqYolu, faylAdi);

                
                DialogResult cavab = MessageBox.Show($"'{faylAdi}' faylını silmək istədiyinizdən əminsinizmi?", "Silmə təsdiqi", MessageBoxButtons.YesNo, 
                                                     MessageBoxIcon.Question);

                if (cavab == DialogResult.Yes)
                {
                    File.Delete(tamYol); 
                    FayllariYukle();     
                    txtMetn.Clear();     
                    lblMelumat.Text = "Ad: -   Ölçü: -   Yaranma tarixi: -";
                    
                    MessageBox.Show("Fayl silindi.", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                
                MessageBox.Show("Silmək üçün siyahıdan heç bir fayl seçilməyib!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```[cite: 1]