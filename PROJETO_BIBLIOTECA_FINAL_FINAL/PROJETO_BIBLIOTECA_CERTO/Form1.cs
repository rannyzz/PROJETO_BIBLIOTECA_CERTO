using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PROJETO_BIBLIOTECA_CERTO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string MySqlClientString = "Server = localhost; Port = 3306; User = root; Database = bd_malu_igor_andrey; SslMode = Disabled";
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_nome_login.Text) ||
       string.IsNullOrWhiteSpace(txt_senha_login.Text))
            {
                MessageBox.Show("Preencha todos os campos!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sai do método e NÃO tenta inserir no banco
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClientString))
                {
                    conn.Open();
                    string sql = "INSERT INTO tblogin (email, Senha) VALUES (@email, @Senha)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Parâmetros evitam SQL Injection
                    cmd.Parameters.AddWithValue("email", txt_nome_login.Text);
                    cmd.Parameters.AddWithValue("@Senha", txt_senha_login.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cadastrado com sucesso!");

                    txt_nome_login.Clear();
                    txt_senha_login.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message,
                     "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_login_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClientString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM tblogin WHERE email = @email AND Senha = @Senha";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", txt_nome_login.Text);
                        cmd.Parameters.AddWithValue("@Senha", txt_senha_login.Text);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr != null && dr.HasRows)
                            {
                                MessageBox.Show("Login realizado com sucesso!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txt_nome_login.Clear();
                                txt_senha_login.Clear();
                                Form3 telaPrincipal = new Form3();
                                telaPrincipal.Show();
                                this.Hide();
                            }
                            else if(string.IsNullOrWhiteSpace(txt_nome_login.Text) || string.IsNullOrWhiteSpace(txt_senha_login.Text))
                            {
                                MessageBox.Show("Preencha todos os campos!", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Sai do método e NÃO tenta inserir no banco
                            }
                            else
                            {
                                MessageBox.Show("Usuário ou senha incorretos!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt_nome_login.Clear();
                                txt_senha_login.Clear();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao banco: " + ex.Message,
                     "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ImageRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void guna2ImageCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            txt_senha_login.UseSystemPasswordChar = !guna2CheckBox1.Checked;
        }
    }
}
