using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MyGo.Core.Utils
{
    public class TextHelper
    {
        public static string JustBefore(string Str, string Seq)
        {
            string Orgi = Str;
            try
            {
                Str = Str.ToLower();
                Seq = Seq.ToLower();

                return Orgi.Substring(0, Str.Length - (Str.Length - Str.IndexOf(Seq)));
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetDomain(String URL)
        {
            return URL.Substring(0, URL.LastIndexOf("/"));
        }

        public static string GetFilename(String URL)
        {
            string Filename = URL.Substring(URL.LastIndexOf("/") + 2, URL.Length - URL.LastIndexOf("/") - 2);

            if (Filename.IndexOf("&") != -1)
                Filename = JustBefore(Filename, "&");

            return Filename;
        }

        public static string JustAfter(string Str, string Seq, string SeqEnd)
        {
            string Orgi = Str;
            try
            {
                Str = Str.ToLower();
                Seq = Seq.ToLower();

                int i = Str.IndexOf(Seq);

                if (i < 0)
                    return null;

                i = i + Seq.Length;

                int j = Str.IndexOf(SeqEnd, i);
                int end;

                if (j > 0) end = j - i;
                else end = Str.Length - i;

                return Orgi.Substring(i, end);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string JustAfter(string Str, string Seq)
        {
            string Orgi = Str;
            try
            {
                Str = Str.ToLower();
                Seq = Seq.ToLower();

                int i = Str.IndexOf(Seq);

                if (i < 0)
                    return null;

                i = i + Seq.Length;

                return Orgi.Substring(i, Str.Length - i);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Left(string param, int length)
        {
        
            string result = param.Substring(0, length);
            
            return result;
        }
        public static string Right(string param, int length)
        {
        
            string result = param.Substring(param.Length - length, length);
            
            return result;
        }

        public static string Mid(string param,int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            
            return result;
        }

        public static string Mid(string param,int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }
        public static string NonUnicode(string unicodeString)
        {

            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = unicodeString.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(unicodeString[index]);
                unicodeString = unicodeString.Replace(unicodeString[index], ReplText[index2]);
            }
            return unicodeString;

        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        
    }
    public static class StringExtension
    {
        public static string Repeat(this string replacement, int times)
        {
           
            string result = string.Empty;
            for (var i = 0; i < times; i++)
            {
                result += replacement;
            }
            return result;
        }

        
  
        
        
    }
}
