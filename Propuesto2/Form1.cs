using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Propuesto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Baltazar"].ConnectionString);

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void ListaAnios()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaAnios", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaAnios");
                        boxAnio.DataSource = df.Tables["ListaAnios"];
                        boxAnio.DisplayMember = "Anios";
                        boxAnio.ValueMember = "Anios";

                    }
                }
            }
        }
        public void ListaMes()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaMes", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaMes");
                        boxMes.DataSource = df.Tables["ListaMes"];
                        boxMes.DisplayMember = "Mes";
                        boxMes.ValueMember = "Mes";

                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaAnios();
            ListaMes();
        }

        private void boxMes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_Pedido_Propuesto", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@fechaanio", boxAnio.SelectedValue);
                    Da.SelectCommand.Parameters.AddWithValue("@fechames", boxMes.SelectedValue);
                    using (DataSet df = new DataSet())
                    {
                        Da.Fill(df, "Pedidos");
                        dataPedido.DataSource = df.Tables["Pedidos"];
                        
                    }
                }
            }
        }
    }
}
