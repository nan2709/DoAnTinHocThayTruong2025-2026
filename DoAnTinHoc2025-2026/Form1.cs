using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSV_Demo
{
    public partial class Form1 : Form
    {
        string filePath = "data.csv";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file data.csv trong thư mục Debug!");
                return;
            }

            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length > 0)
            {
                // Tạo cột từ dòng đầu tiên
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string header in headerLabels)
                {
                    dt.Columns.Add(header);
                }

                // Thêm dữ liệu các dòng còn lại
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    dt.Rows.Add(dataWords);
                }
            }

            dataGridView1.DataSource = dt;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để ghi!");
                return;
            }

            DataTable dt = (DataTable)dataGridView1.DataSource;
            var lines = new string[dt.Rows.Count + 1];

            // Ghi header
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(column => column.ColumnName)
                                             .ToArray();
            lines[0] = string.Join(",", columnNames);

            // Ghi từng dòng dữ liệu
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] fields = dt.Rows[i].ItemArray.Select(field => field.ToString()).ToArray();
                lines[i + 1] = string.Join(",", fields);
            }

            File.WriteAllLines(filePath, lines);
            MessageBox.Show("Ghi file CSV thành công!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
