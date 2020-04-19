using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Get_Comment
{
    class XuLyData
    {
        //tim  tat ca so co 10 toi 11 chu so ma dung truoc va dang sau no la khoang trong 

        Regex regSdt = new Regex(@"\b[0-9]{10,11}\b");

        //tim tat ca so co 1 toi 2 chu so ma dung truoc va dang sau la khoang trong

        Regex regSl = new Regex(@"\b[0-9]{1,2}\b");

        Regex regEmail = new Regex(@"[a-z0-9._@+-]+@([a-z0-9]+\.)+[a-z]{2,6}");

        Regex regMaHang = new Regex(@"[MH|mh|Mh|mH]+[0-9]{3}");

        public XuLyData()
        {
        }

        public Match xulySDT(string chuoi)
        {
            // thêm khoảng trống vào đầu vs cuối chuỗi để tranh trường hợp số ở đầu hoặc cuối thì không có đủ 2 khoảng trống 2 bên

            Match result = regSdt.Match(" " + chuoi + " ");

            //tra ve Object Match xai Match.toString() de lay ra duoc string  

            return result;

        }
        public Match xulySL(string chuoi)
        {
            Match result = regSl.Match(" " + chuoi + " ");
            return result;

        }

        public Match xulyEmail(string chuoi)
        {
            Match result = regEmail.Match(" " + chuoi + " ");
            return result;

        }

        public Match xulyMaHang(string chuoi)
        {
            Match result = regMaHang.Match(" " + chuoi + " ");
            return result;

        }

        public Match XuLyMaHangNangCap(string chuoi, string mahang)
        {
            Regex regXuLy = new Regex(@mahang);
            Match result = regXuLy.Match(" "+chuoi+" ");
            return result;
        } 
    }
}
