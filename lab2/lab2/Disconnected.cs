using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace lab2
{
    public partial class Disconnected : Form
    {
        SqlConnection connection;
        DataTable dt;
        public Disconnected()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["itiEntites"].ConnectionString);
        }

        private void Disconnected_Load(object sender, EventArgs e)
        {
            SqlCommand selectCommand = new SqlCommand("select * from course", connection);

            //object to store my data
            dt = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand= selectCommand;

            dataAdapter.Fill(dt);

            dgv_courses.DataSource = dt;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();
            row["crs_id"] = txt_id.Text;
            row["crs_name"] = txt_name.Text;
            row["crs_duration"] = txt_duration.Text;
            row["top_id"] = txt_topicid.Text;

            dt.Rows.Add(row);
            dgv_courses.DataSource = dt;
        }

        private void dgv_courses_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_id.Text = dgv_courses.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = dgv_courses.SelectedRows[0].Cells[1].Value.ToString();
            txt_duration.Text = dgv_courses.SelectedRows[0].Cells[2].Value.ToString();
            txt_topicid.Text = dgv_courses.SelectedRows[0].Cells[3].Value.ToString();
            txt_id.Enabled = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (item["crs_id"].ToString()==txt_id.Text)
                {
                    item["crs_name"] = txt_name.Text;
                    item["crs_duration"] = txt_duration.Text;
                    item["top_id"] = txt_topicid.Text;
                }
            }
            dgv_courses.DataSource = dt;
            txt_id.Enabled = true;
        }

        private void btn_savechange_Click(object sender, EventArgs e)
        {
            SqlCommand Insert = new SqlCommand("insert into Course Values(@id,@name,@duration,@topic)", connection);
            Insert.Parameters.Add("id", SqlDbType.Int, 4, "Crs_Id");
            Insert.Parameters.Add("name", SqlDbType.VarChar, 50, "Crs_Name");
            Insert.Parameters.Add("duration", SqlDbType.Int, 4, "Crs_Duration");
            Insert.Parameters.Add("topic", SqlDbType.Int, 4, "Top_Id");

            SqlCommand update = new SqlCommand("update Course set Crs_Name=@name,Crs_Duration=@duration,Top_Id=@topic where Crs_Id=@id", connection);
            update.Parameters.Add("id", SqlDbType.Int, 4, "Crs_Id");
            update.Parameters.Add("name", SqlDbType.VarChar, 50, "Crs_Name");
            update.Parameters.Add("duration", SqlDbType.Int, 4, "Crs_Duration");
            update.Parameters.Add("topic", SqlDbType.Int, 4, "Top_Id");

            SqlCommand delete = new SqlCommand("Delete from Course where Crs_Id=@id", connection);
            delete.Parameters.Add("id", SqlDbType.Int, 4, "Crs_Id");

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.DeleteCommand = delete;
            dataAdapter.InsertCommand = Insert;
            dataAdapter.UpdateCommand = update;

            dataAdapter.Update(dt);

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //string id = (string)txt_id.Text;
            foreach (DataRow item in dt.Rows)
            {
                if (item["crs_id"].ToString() == txt_id.Text)
                {
                    item.Delete();
                }
            }
        }
    }
}
