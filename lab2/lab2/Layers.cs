using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Layers : Form
    {
        public Layers()
        {
            InitializeComponent();
        }

        private void Layers_Load(object sender, EventArgs e)
        {
           dgv_Course.DataSource= DBLayer.select("course");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            CourseBusinessLayer.Insert(txt_id.Text, txt_name.Text, txt_duration.Text, txt_topic.Text);
            dgv_Course.DataSource = CourseBusinessLayer.getAll();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            CourseBusinessLayer.update(txt_id.Text, txt_name.Text, txt_duration.Text, txt_topic.Text);
            dgv_Course.DataSource = CourseBusinessLayer.getAll();
        }

        private void dgv_Course_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_id.Text = dgv_Course.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = dgv_Course.SelectedRows[0].Cells[1].Value.ToString();
            txt_duration.Text = dgv_Course.SelectedRows[0].Cells[2].Value.ToString();
            txt_topic.Text = dgv_Course.SelectedRows[0].Cells[3].Value.ToString();
            txt_id.Enabled = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            CourseBusinessLayer.delete(txt_id.Text);
            dgv_Course.DataSource = CourseBusinessLayer.getAll();
        }
    }
}
