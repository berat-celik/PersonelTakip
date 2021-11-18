using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PersonelTakipProgrami
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter data;
        public Form1()
        {
            InitializeComponent();
        }
        void getPersonel()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-D3NIK45; Initial Catalog=Personeller; Integrated Security=True");
            baglanti.Open();
            data = new SqlDataAdapter("SELECT *FROM Personel", baglanti);
            DataTable tablo = new DataTable();
            data.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getPersonel();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtNo.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtYas.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtMaas.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            if (txtNo.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır");
                return;
            }
            string sorgu = "INSERT INTO Personel(Ad,Soyad,TcNo,Yas,Maas) VALUES(@Ad,@Soyad,@TcNo,@Yas,@Maas)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Ad",txtAd.Text);
            komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@TcNo", txtNo.Text.Trim());
            komut.Parameters.AddWithValue("@Yas", txtYas.Text);
            komut.Parameters.AddWithValue("@Maas", txtMaas.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            getPersonel();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            String sorgu = "DELETE FROM Personel WHERE id=@Id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Id",txtId.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            getPersonel();
        }

        private void btnCuncelle_Click(object sender, EventArgs e)
        {
            if (txtNo.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır");
                return;
            }
            string sorgu = "UPDATE Personel SET Ad=@Ad,Soyad=@Soyad,TcNo=@TcNo,Yas=@Yas,Maas=@Maas WHERE Ad=@Ad";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Ad", txtAd.Text);
            komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@TcNo", txtNo.Text);
            komut.Parameters.AddWithValue("@Yas", Convert.ToInt32(txtYas.Text));
            komut.Parameters.AddWithValue("@Maas", Convert.ToInt32(txtMaas.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            getPersonel();




        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtNo.Clear();
            txtYas.Clear();
            txtMaas.Clear();
        }
    } 
}

