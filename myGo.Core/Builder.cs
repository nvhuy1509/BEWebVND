using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace MyGo.Core
{
    public class Builder
    {
        public static string BuildSQLQuery<T>(T objFrom, T objTo) where T : class
        {
            Type t = objFrom.GetType();
            PropertyInfo[] properties = t.GetProperties();
            string whereClause = "";


            foreach (PropertyInfo property in properties)
            {
                object x = property.GetValue(objFrom, null);

                //Console.WriteLine(string.Format("Name: {0}, Value: {1}, Constant value: {1}", property.Name, x));

                if (x != null)
                {
                    //Tim Trong khoang gia tri
                    if (property.PropertyType == typeof (float) && (float) x != 0 && objTo != null)
                    {
                        var vTo = (float) property.GetValue(objTo, null);
                        if (vTo != 0)
                        {
                            whereClause += string.Format("{0} BETWEEN {1} AND {2} AND ", property.Name, x, vTo);
                            continue;
                        }
                    }
                    //Khoang ngay
                    if (property.PropertyType == typeof (DateTime) &&
                        DateTime.Compare((DateTime) x, new DateTime(1, 1, 1)) != 0 && objTo != null)
                    {
                        var vTo = (DateTime) property.GetValue(objTo, null);
                        if (DateTime.Compare(vTo, new DateTime(1, 1, 1)) != 0)
                        {
                            whereClause += string.Format("{0} BETWEEN '{1}' AND '{2}' AND ", property.Name, x, vTo);
                            continue;
                        }
                    }


                    if (property.PropertyType == typeof (string))
                    {
                        whereClause += string.Format("{0} = '{1}' AND ", property.Name, x);
                        continue;
                    }
                    if ((property.PropertyType == typeof (byte)) && (byte) x != 0)
                    {
                        whereClause += string.Format("{0} = {1} AND ", property.Name, x);
                        continue;
                    }
                    if (property.PropertyType == typeof (int) && (int) x != 0)
                    {
                        whereClause += string.Format("{0} = {1} AND ", property.Name, x);
                        continue;
                    }
                    if (property.PropertyType == typeof (long) && (long) x != 0)
                    {
                        whereClause += string.Format("{0} = {1} AND ", property.Name, x);
                        continue;
                    }


                    if (property.PropertyType.IsEnum)
                    {
                        var v = (int) x;

                        if (v != 0)
                        {
                            whereClause += string.Format("{0} = {1} AND ", property.Name, v);
                            continue;
                        }
                    }
                }
            }

            if (whereClause.Length > 0)
            {
                whereClause = whereClause.Remove(whereClause.Length - 5, 5);
            }
            else
            {
                whereClause = "1=1";
            }

            return whereClause;
        }


        public static string WebPageToString(string urlString)
        {
            var uriObject = new Uri(urlString);
            WebRequest webRequest = WebRequest.Create(uriObject);
            WebResponse webResponse = webRequest.GetResponse();
            Stream streamObject = webResponse.GetResponseStream();
            var streamReader = new StreamReader(streamObject);

            string htmlText = streamReader.ReadToEnd();

            streamReader.Close();

            return htmlText;
        }
    }
}