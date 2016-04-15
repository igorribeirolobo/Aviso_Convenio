using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviso_Convenio
{
    public partial class Cadastra : Form
    {
        public Cadastra()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtsenha.Text == txtrsenha.Text)
            {
                conn conexao = new conn();
                conexao.Insert(txtconvenio.Text, txtlogin.Text, txtsenha.Text);
                txtconvenio.Text = "";
                txtlogin.Text = "";
                txtsenha.Text = "";
                txtrsenha.Text = "";
                MessageBox.Show("Operadora cadastrada com sucesso!");

            }
            else
            {
                MessageBox.Show("Senhas não conferem!");
                txtsenha.Text = "";
                txtrsenha.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 chama = new Form1();
            chama.Show();
      
        }
    }
}
