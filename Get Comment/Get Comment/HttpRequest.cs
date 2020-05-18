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
    
        /// <summary>
        /// đưa vào url cần get và tham chiếu đến 1 biến để chứa kết quả khi request thành công 
        /// trả về giá trí true khi request thành công , trả về false khi request thất bại  
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool getCommentFaceBook(string url,ref string json)
        {
            
            try
            {
                var webClient = new WebClient();
                json = webClient.DownloadString(@url);
            }
            catch (Exception)
            {
                string message = "Không truy cập được bài viết, xin kiểm tra lại id và token.";
                string caption = "Error!";
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
