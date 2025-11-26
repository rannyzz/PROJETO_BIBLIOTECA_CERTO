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
            
        }

        private void btn_login_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClientString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM tblogin WHERE Email = @email AND senha = @Senha";
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
                                string nomeUsuario = "";
                                if (dr.Read())
                                {
                                    nomeUsuario = dr["nome"].ToString();
                                }
                                Form3 telaPrincipal = new Form3(nomeUsuario);
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

        private void btn_cad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_nome_cad.Text) ||
       string.IsNullOrWhiteSpace(msk_cpf.Text) ||
       string.IsNullOrWhiteSpace(txt_email_cad.Text) ||
       string.IsNullOrWhiteSpace(txt_senha_cad.Text))
            {
                MessageBox.Show("Preencha todos os campos!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClientString))
                {
                    conn.Open();

                    string sql = @"INSERT INTO tblogin (nome, CPF, Email, senha)
                           VALUES (@Nome, @CPF, @Email, @Senha)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", txt_nome_cad.Text);
                        cmd.Parameters.AddWithValue("@CPF", msk_cpf.Text);
                        cmd.Parameters.AddWithValue("@Email", txt_email_cad.Text);
                        cmd.Parameters.AddWithValue("@Senha", txt_senha_cad.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cadastro realizado com sucesso!",
                            "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa os campos após cadastrar
                        txt_nome_cad.Clear();
                        msk_cpf.Clear();
                        txt_email_cad.Clear();
                        txt_senha_cad.Clear();
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // email duplicado (PK)
                {
                    MessageBox.Show("Este e-mail já está cadastrado!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Erro ao conectar ao banco: " + ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txt_senha_login_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
