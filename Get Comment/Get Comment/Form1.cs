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
        List<comments> dataClear = null;
        private void button1_Click(object sender, EventArgs e)
        {

            //var webClient = new WebClient();
            string link = "https://graph.facebook.com/" + textBox1.Text + "_" + textBox2.Text + "?fields=comments.limit(10000)&access_token=" + textBox3.Text;

            string json="";

            //try
            //{
            //    json = webClient.DownloadString(@link);
            //}
            //catch (Exception)
            //{
            //    string message = "không truy cập được bài viết , xin xem lại id và token";
            //    string caption = "error";
            //    MessageBoxButtons buttons = MessageBoxButtons.OK;
            //    DialogResult result;
            //    result = MessageBox.Show(message, caption, buttons);
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        // Closes the parent form.

            //        return;
            //    }
            //}

            var resultRequest = new HttpRequest().getCommentFaceBook(link, ref json);

            if (resultRequest == false) return;
            dynamic dataSet = JsonConvert.DeserializeObject<dynamic>(json);
            dynamic array = dataSet.comments.data;

            dataClear = new List<comments>();
            int i = 1;
            foreach (var item in array)
            {
                var tempCommentData = new comments();
                tempCommentData.time = item.created_time;
                tempCommentData.id = item.id;
                tempCommentData.comment = item.message;

                string binhluan = Regex.Replace(tempCommentData.comment, "\n", " ");

                var resultSDT = new XuLyData().xulySDT(binhluan);
                var resultSL = new XuLyData().xulySL(binhluan);
                var resultMH = new XuLyData().xulyMaHang(binhluan);
                var resultEmail = new XuLyData().xulyEmail(binhluan);
                //Console.WriteLine(result.ToString());
                if (resultSDT.Success || resultSL.Success || resultMH.Success || resultEmail.Success)
                //if(0==0)
                {


                    tempCommentData.MH = resultMH.ToString();
                    tempCommentData.SL = resultSL.ToString();
                    tempCommentData.email = resultEmail.ToString();
                    tempCommentData.sdt = resultSDT.ToString();

                    ListViewItem item1 = new ListViewItem();
                    item1.Text = i.ToString();
                    i++;
                    item1.SubItems.Add(tempCommentData.time);
                    item1.SubItems.Add(tempCommentData.id);
                    item1.SubItems.Add(tempCommentData.comment);
                    item1.SubItems.Add(tempCommentData.MH);
                    item1.SubItems.Add(tempCommentData.SL);
                    item1.SubItems.Add(tempCommentData.email);
                    item1.SubItems.Add(tempCommentData.sdt);
                    listView1.Items.Add(item1);
                    dataClear.Add(tempCommentData);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "(*.csv)|*.csv";
            if (save.ShowDialog() == DialogResult.OK)
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

                for (int i = 0; i < dataClear.Count; i++)
                {

                    outputFile.WriteLine(dataClear[i].time
                        + "," + dataClear[i].id
                        + "," + dataClear[i].comment
                        + "," + dataClear[i].MH
                        + "," + dataClear[i].SL
                        + "," + dataClear[i].email
                        + "," + dataClear[i].sdt);



                }


                outputFile.Close();
                MessageBox.Show("Export Successfull!");
            }
        }
    }
}
