using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace lab1
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["itiEntites"].ConnectionString);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillGrid();
            Filldropdownlist();

        }
        public void FillGrid()
        {
            //define conection


            // connection.ConnectionString = "Data Source =.; Initial Catalog = ITI; Integrated Security = True";


            //define Command

            //command.CommandText = "select * from Course";
            //command.CommandType = CommandType.Text;
            //command.Connection = connection;

            command = new SqlCommand("select * from Course", connection);//by default Text

            //open connection
            connection.Open();

            //Execute command
            SqlDataReader dataReader = command.ExecuteReader();

            //Display my data in DataGridView
            List<Course> courses = new List<Course>();
            while (dataReader.Read())
            {
                Course crs = new Course();
                crs.Crs_Id = (int)dataReader["Crs_Id"];
                crs.Crs_Name = dataReader["Crs_Name"].ToString();
                crs.Crs_Duration = (int)dataReader["Crs_Duration"];
                crs.Top_Id = (int)dataReader["Top_Id"];

                courses.Add(crs);
            }
            dgv_courses.DataSource = courses;
            //close connection
            connection.Close();
        }

        public  void Filldropdownlist()
        {
            command = new SqlCommand("select * from topic", connection);//by default Text

            //open connection
            connection.Open();

            //Execute command
            SqlDataReader dataReader = command.ExecuteReader();

            //Display my data in DataGridView
            List<Topic> topics = new List<Topic>();
            while (dataReader.Read())
            {
                Topic top = new Topic();
                top.id = (int)dataReader["Top_Id"];
                top.name = dataReader["Top_Name"].ToString();

                topics.Add(top);
            }
            ddl_topic.DisplayMember = "name";
            ddl_topic.ValueMember = "id";
            ddl_topic.DataSource = topics;
            //close connection
            connection.Close();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            //define command
            command = new SqlCommand("insert into course values(@id,@crsname,@duration,@topic)", connection);
            command.Parameters.AddWithValue("id", txt_id.Text);
            command.Parameters.AddWithValue("crsname", txt_name.Text);
            command.Parameters.AddWithValue("duration", txt_duration.Text);
            command.Parameters.AddWithValue("topic", ddl_topic.SelectedValue);

            //open cconnection
            connection.Open();

            //execute query
            int roweffected = command.ExecuteNonQuery();
            if (roweffected>0)
            {
                MessageBox.Show("Row is Added");
                
            }

            connection.Close();
            FillGrid();
        }
        private int id;
        private void dgv_courses_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = (int)dgv_courses.SelectedRows[0].Cells[0].Value;
            txt_id.Text = dgv_courses.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = dgv_courses.SelectedRows[0].Cells[1].Value.ToString();
            txt_duration.Text = dgv_courses.SelectedRows[0].Cells[2].Value.ToString();
            ddl_topic.SelectedValue = dgv_courses.SelectedRows[0].Cells[3].Value;
        }

        private void btn_update(object sender, EventArgs e)
        {
            // define command
            command = new SqlCommand("Update course set Crs_Id = @id, Crs_Name = @name,Crs_Duration=@duration,Top_Id=@topicid where Crs_Id = @id");
            command.Connection = connection;
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", txt_name.Text);
            command.Parameters.AddWithValue("duration", txt_duration.Text);
            command.Parameters.AddWithValue("topicid", ddl_topic.SelectedValue);

            //open connection
            connection.Open();

            //execute
            int result = command.ExecuteNonQuery();
            //close connection
            connection.Close();

            if (result > 0)
            {
                MessageBox.Show("Data Updated Successfully");
                FillGrid();

            }
        }

        private void btn_delete(object sender, EventArgs e)
        {
            command.Connection = connection;
            //define command
            SqlCommand commmand = new SqlCommand("delete from course where Crs_Id = @id", connection);
            commmand.Parameters.Add(new SqlParameter("id", id));

            // open connection
            connection.Open();

            //execute
            int result = commmand.ExecuteNonQuery();

            //close connection
            connection.Close();

            if (result > 0)
            {
                MessageBox.Show("Data deleted successfully");
                FillGrid();
            }
        }
    }
}
