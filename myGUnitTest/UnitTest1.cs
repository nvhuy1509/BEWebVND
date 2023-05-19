using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using MyGo.Core;
using Xunit;

namespace myGUnitTest
{
    public class Test_Encrypt
    {
        [Fact]
        public void Test1()
        {
            var rijndaelKey = new RijndaelEnhanced("OhayVN", "@1B2c3D4e5F6g7H8");
            // string conn =
            //   "ffoylQVUl3zGfVGEz0Rmdfye5wvw74wjyDi6liKprTm9QCbL6BLG9DjWFbnE6D2bF/lP1+OpLCxw7x/IdgBG8ru87yj+Wtqrz/zRM0WBndY0R0yTWYWx3T8CHX/RC0t6";
            string conn = "T+TvkhdXvP+hoCX//Zaz/C8+BaokbiXiCTuqnD9NDNZp+7CxOIktWNYSQt+GSdMC7CyRuxR+Jpfgb9PCsW63Zc37HooTq4raQEx1D+g9xT0=";
            var decryptConn = rijndaelKey.Decrypt(conn);

            Console.WriteLine(decryptConn);
        }
        [Fact]
        public void Test_Encrypt_ConnectString()
        {
            {
                var rijndaelKey = new RijndaelEnhanced("OhayVN", "@1B2c3D4e5F6g7H8");
                //string conn = "server=192.168.10.106;database=myg.CelebeCms;uid=myg.id;pwd=myG.id_c0re123*654;";
                //string conn = "Server=123.30.245.58;port=21032;Database=game_guild;User Id=postgres;Password=123JQKGame6789;CommandTimeout=60;Pooling=true;MinPoolSize=1;MaxPoolSize=100";
                // string conn = "Server=123.30.245.58;port=16232;Database=webtest;User Id=postgres;Password=develop_myg_db;CommandTimeout=60;Pooling=true;MinPoolSize=1;MaxPoolSize=100";
                string conn = "server=ANPHATPC\\MYGSERVER;database=myG.VNDmoneyLocal;Trusted_Connection=True;";//local
               // string conn = "server=192.168.10.106;database=myG.MMC;uid=myg.id;pwd=myG.id_c0re123*654;"; // public
                //conn = "Server=123.30.245.58;Port=16089;User=root;Password=AFFWINNOW2022;Database=webux_aff;SSL Mode=None";
                //string conn = "server=123.30.245.58,61433;database=myG.BeAStar;uid=myg.id;pwd=myG.id_c0re123*654;";
                var decryptConn = rijndaelKey.Encrypt(conn);
                Console.WriteLine(decryptConn);
                //trace.writeline(decryptconn);
            }
        }
        [Fact]
        public void testString()
        {
            //"{\"uploaded\": 1,\"fileName\": \"{0}\",\"url\": \"{1}\"}"
            string reponse = string.Format("{{\"uploaded\": 1,\"fileName\": \"{0}\",\"url\": \"{1}\",\"web\":\"{2}\"}}", "fileName", "responseUrl", "FilePath");
            Console.WriteLine(reponse);
        }

        [Fact]
        public void TestRegex()
        {
            var r = new Regex(@"(0)+([0-9]{9})\b");
            Console.WriteLine("a "+r.IsMatch("097498078963"));
        }
    }
}
