using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Get_Comment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var webClient = new WebClient();
            string link = "https://graph.facebook.com/"+textBox1.Text+"_"+textBox2.Text+"?fields=comments.limit(10000)&access_token="+textBox3.Text;
            var json = webClient.DownloadString(@link);
            dynamic dataSet = JsonConvert.DeserializeObject<dynamic>(json);
            dynamic array = dataSet.comments.data;
            foreach (var item in array)
            {
                string time = item.created_time;
                string id = item.id;
                string comment = item.message;

                string binhluan = Regex.Replace(comment, "\n", " ");

                var result1 = new XuLyData().xulySDT(binhluan);
                var result2 = new XuLyData().xulySL(binhluan);
                var result3 = new XuLyData().xulyMaHang(binhluan);
                var result4 = new XuLyData().xulyEmail(binhluan);
                //Console.WriteLine(result.ToString());
                if (result1.Success||result2.Success||result3.Success||result4.Success)
                //if(0==0)
                {


                    string mh = result3.ToString();
                    string sl = result2.ToString();
                    string email = result4.ToString();
                    string sdt = result1.ToString();

                    ListViewItem item1 = new ListViewItem();
                    item1.Text = time;
                    item1.SubItems.Add(id);
                    item1.SubItems.Add(binhluan);
                    item1.SubItems.Add(mh);
                    item1.SubItems.Add(sl);
                    item1.SubItems.Add(email);
                    item1.SubItems.Add(sdt);
                    listView1.Items.Add(item1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "(*.csv)|*.csv";
            if(save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                //File.AppendAllText(path,"Time"+","+"ID User"+","+"Comment" + "," + "Mã Hàng" + "," + "Số Lượng" + "," + "Email" + "," + "Số điện thoại" + Environment.NewLine);
                //for (int i =0;i<listView1.Items.Count;i++)
                //{
                //    File.AppendAllText(path, listView1.Items[i].SubItems[0].Text
                //        +","+ listView1.Items[i].SubItems[1].Text
                //        +","+ listView1.Items[i].SubItems[2].Text
                //        +","+ listView1.Items[i].SubItems[3].Text
                //        +","+ listView1.Items[i].SubItems[4].Text
                //        +","+ listView1.Items[i].SubItems[5].Text
                //        +","+ listView1.Items[i].SubItems[6].Text
                //        + Environment.NewLine);

                //}


                StreamWriter outputFile = new StreamWriter(path, false, new UTF8Encoding(true));
                outputFile.WriteLine("Time" + "," + "ID User" + "," + "Comment" + "," + "Mã Hàng" + "," + "Số Lượng" + "," + "Email" + "," + "Số điện thoại");

                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    outputFile.WriteLine(listView1.Items[i].SubItems[0].Text
                        + "," + listView1.Items[i].SubItems[1].Text
                        + "," + listView1.Items[i].SubItems[2].Text
                        + "," + listView1.Items[i].SubItems[3].Text
                        + "," + listView1.Items[i].SubItems[4].Text
                        + "," + listView1.Items[i].SubItems[5].Text
                        + "," + listView1.Items[i].SubItems[6].Text);


                }


                outputFile.Close();
                MessageBox.Show("Export Successfull!");
            }
        }
    }
}
