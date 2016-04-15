using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Aviso_Convenio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cboperadora_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn conexao = new conn();
            try
            {
                conexao.Iniciar();
                conexao.AbrirConexao();
                string mSQL = "Select * from tbl_operadora";

                MySqlCommand cmd = new MySqlCommand(mSQL, conexao.connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dtMensagens = new DataTable();
                da.Fill(dtMensagens);
                this.cboperadora.DataSource = dtMensagens;
                this.cboperadora.ValueMember = "id_operadora";
                this.cboperadora.SelectedItem = "id_operadora";
                this.cboperadora.DisplayMember = "Descricao";

            }
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao MySQL : " + msqle.Message, "Erro");
            }
            finally
            {
                conexao.CloseConnection();
            }
        }
        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Ocorreu um erro durante a liberação do objeto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn busca = new conn();
             busca.Update(cboperadora.SelectedValue.ToString(), txtsenha.Text);
            txtsenha.Text = "";
             MessageBox.Show("Atualizado com sucesso!");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cadastra nova = new Cadastra();
            nova.Show();
            this.Hide();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conn conexao = new conn();

            try
            {
                conexao.Iniciar();
                conexao.AbrirConexao();
                string mSQL = "Select * from tbl_operadora";

               

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                MySqlCommand cmd = new MySqlCommand(mSQL, conexao.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                reader.Close();
                MySqlDataReader leitor = cmd.ExecuteReader();
                xlWorkSheet.Cells[1, 1] = "Operadora";
                xlWorkSheet.Cells[1, 2] = "Login";
                xlWorkSheet.Cells[1, 3] = "Senha";
                while (leitor.Read())
                {
                    for (int i = 1; i <= count; i++)
                    {
                        xlWorkSheet.Cells[i + 1, 1] = leitor.GetString(1);
                        xlWorkSheet.Cells[i + 1, 2] = leitor.GetString(2);
                        xlWorkSheet.Cells[i + 1, 3] = leitor.GetString(3);

                    }

                }


                xlWorkBook.SaveAs(@"Z:\DADOS\Operadoras.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
 Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                liberarObjetos(xlWorkSheet);
                liberarObjetos(xlWorkBook);
                liberarObjetos(xlApp);

                MessageBox.Show(@"O arquivo Excel foi criado com sucesso. Você pode encontrá-lo em : Z:\DADOS\");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

        }

        private void btnfechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
