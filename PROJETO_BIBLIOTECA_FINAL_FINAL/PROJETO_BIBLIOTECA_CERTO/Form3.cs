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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.FormClosed += Form3_FormClosed;
        }
        string MySqlClient = "Server = localhost; Port = 3306; User = root; Database = bd_malu_igor_andrey; SslMode = Disabled";
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // força o término do app => volta para a IDE
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void btn_listar_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlClient);
                conn.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM tbcadastro", conn);
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro ao listar: " + ex.Message,
                     "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_inserir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text) ||
        string.IsNullOrWhiteSpace(txt_titulo.Text) ||
        string.IsNullOrWhiteSpace(txt_autor.Text) ||
        string.IsNullOrWhiteSpace(txt_editora.Text) ||
        string.IsNullOrWhiteSpace(msk_data.Text) ||
        string.IsNullOrWhiteSpace(txt_valor.Text) ||
        string.IsNullOrWhiteSpace(txt_qtde.Text) ||
        string.IsNullOrWhiteSpace(txt_cod_publi.Text) ||
        string.IsNullOrWhiteSpace(txt_genero.Text))
            {
                MessageBox.Show(
                    "Preencha todos os campos antes de inserir!",
                    "Campos vazios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClient))
                {
                    conn.Open();

                    string sql = @"INSERT INTO tbcadastro
                           (ID, Titulo, Autor, Editora, Datadepublicacao, Valor, Qtde, cd_publi, Genero)
                           VALUES
                           (@ID, @Titulo, @Autor, @Editora, @DataPub, @Valor, @Qtde, @CodPub, @Genero)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Parâmetros seguros
                        cmd.Parameters.AddWithValue("@ID", txt_id.Text.Trim());
                        cmd.Parameters.AddWithValue("@Titulo", txt_titulo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Autor", txt_autor.Text.Trim());
                        cmd.Parameters.AddWithValue("@Editora", txt_editora.Text.Trim());
                        cmd.Parameters.AddWithValue("@DataPub", msk_data.Text.Trim());
                        cmd.Parameters.AddWithValue("@Valor", txt_valor.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qtde", txt_qtde.Text.Trim());
                        cmd.Parameters.AddWithValue("@CodPub", txt_cod_publi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Genero", txt_genero.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show(
                        "Cadastro realizado com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    // Limpa os campos
                    txt_id.Clear();
                    txt_titulo.Clear();
                    txt_autor.Clear();
                    txt_editora.Clear();
                    msk_data.Clear();
                    txt_valor.Clear();
                    txt_qtde.Clear();
                    txt_cod_publi.Clear();
                    txt_genero.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao inserir: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_Atualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                MessageBox.Show("Informe o ID para atualizar!", "Alerta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClient))
                {
                    conn.Open();

                    List<string> campos = new List<string>();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    // TÍTULO
                    if (!string.IsNullOrWhiteSpace(txt_titulo.Text))
                    {
                        campos.Add("Titulo = @Titulo");
                        cmd.Parameters.AddWithValue("@Titulo", txt_titulo.Text);
                    }

                    // AUTOR
                    if (!string.IsNullOrWhiteSpace(txt_autor.Text))
                    {
                        campos.Add("Autor = @Autor");
                        cmd.Parameters.AddWithValue("@Autor", txt_autor.Text);
                    }

                    // EDITORA
                    if (!string.IsNullOrWhiteSpace(txt_editora.Text))
                    {
                        campos.Add("Editora = @Editora");
                        cmd.Parameters.AddWithValue("@Editora", txt_editora.Text);
                    }

                    // DATA DE PUBLICAÇÃO (AGORA EM dd/MM/yyyy)
                    if (msk_data.MaskFull && DateTime.TryParse(msk_data.Text, out DateTime data))
                    {
                        campos.Add("Datadepublicacao = @Data");
                        cmd.Parameters.AddWithValue("@Data", data.ToString("dd/MM/yyyy"));
                        // Se quiser com HÍFEN: "dd-MM-yyyy"
                    }
                    // VALOR
                    if (!string.IsNullOrWhiteSpace(txt_valor.Text))
                    {
                        campos.Add("Valor = @Valor");
                        cmd.Parameters.AddWithValue("@Valor", txt_valor.Text);
                    }
                    // QTDE
                    if (!string.IsNullOrWhiteSpace(txt_qtde.Text))
                    {
                        campos.Add("Qtde = @Qtde");
                        cmd.Parameters.AddWithValue("@Qtde", txt_qtde.Text);
                    }
                    // CÓDIGO DE PUBLICAÇÃO
                    if (!string.IsNullOrWhiteSpace(txt_cod_publi.Text))
                    {
                        campos.Add("cd_publi = @Cod");
                        cmd.Parameters.AddWithValue("@Cod", txt_cod_publi.Text);
                    }
                    // GÊNERO
                    if (!string.IsNullOrWhiteSpace(txt_genero.Text))
                    {
                        campos.Add("Genero = @Genero");
                        cmd.Parameters.AddWithValue("@Genero", txt_genero.Text);
                    }
                    // Se nenhum campo foi alterado
                    if (campos.Count == 0)
                    {
                        MessageBox.Show("Nenhum campo para atualizar.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    // Monta o SQL dinâmico
                    string sql = "UPDATE tbcadastro SET " + string.Join(", ", campos) + " WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@ID", txt_id.Text);
                    cmd.CommandText = sql;

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Atualização realizada com sucesso!",
                        "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_deletar_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlClient);
                conn.Open();
                string sql = "DELETE FROM tbcadastro WHERE ID = '" + txt_id.Text + "'";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cadastro deletado com sucesso!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_id.Clear();
                txt_titulo.Clear();
                txt_autor.Clear();
                txt_editora.Clear();
                msk_data.Clear();
                txt_valor.Clear();
                txt_qtde.Clear();
                txt_cod_publi.Clear();
                txt_genero.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar: " + ex.Message,
                     "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_titulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlClient);
                conn.Open();
                if (txt_tit_pesq.Text != "")
                {
                    string sql = "SELECT * FROM tbcadastro WHERE Titulo LIKE '" + txt_tit_pesq.Text + "%'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    guna2DataGridView2.DataSource = dt;
                }
                else
                {
                    guna2DataGridView2.DataSource = null;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar: " + ex.Message,
                     "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {

        }

        private void btn_pesq_id_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_pesq_id.Text))
            {
                MessageBox.Show("Preencha o campo de ID para pesquisar!", "Alerta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // impede continuar
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySqlClient))
                {
                    conn.Open();
                    string sql = "SELECT * FROM tbcadastro WHERE ID = @ID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", txt_pesq_id.Text);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    guna2DataGridView2.DataSource = dt;
                }
                txt_pesq_id.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
