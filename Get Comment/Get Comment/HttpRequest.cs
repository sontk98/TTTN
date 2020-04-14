using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Get_Comment
{
    public class HttpRequest
    {
    

        public bool getCommentFaceBook(string url,ref string json)
        {
            
            try
            {
                var webClient = new WebClient();
                json = webClient.DownloadString(@url);
            }
            catch (Exception)
            {
                string message = "không truy cập được bài viết , xin xem lại id và token";
                string caption = "error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Closes the parent form.

                    return false;
                }
            }
            return true;
        }
    }
}
