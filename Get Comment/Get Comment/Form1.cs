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
                //lấy mã hàng vào khách hàng nhập
                //var resultMH = new XuLyData().XuLyMaHangNangCap(tempCommentData.comment, "mh017");

                var resultEmail = new XuLyData().xulyEmail(binhluan);


                //Console.WriteLine(result.ToString());
                if (resultSDT.Success || resultSL.Success || resultMH.Success  || resultEmail.Success)
                //if(0==0)
                {

                    // lấy nhiều mã hàng
                    //do
                    //{
                    //    tempCommentData.MH =tempComentData.MH + resultMH.ToString() + " ";
                    //    resultMH = resultMH.NextMatch();
                    //}
                    //while (resultMH != Match.Empty);


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
