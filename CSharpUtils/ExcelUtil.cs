using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSharpUtils
{
    public class ExcelUtil
    {

        /// <summary>
        /// Excel导出数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="fileName"></param>
        public static void ExportExcel(DataTable data, string fileName)
        {
            try
            {
                if (data != null && data.Rows.Count > 0)
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.Charset = "Utf-8";
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", System.Text.Encoding.UTF8));

                    System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();
                    sbHtml.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                    sbHtml.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

                    //写出列名
                    sbHtml.AppendLine("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");

                    foreach (System.Data.DataColumn column in data.Columns)
                    {
                        sbHtml.AppendLine("<td>" + column.ColumnName + "</td>");
                    }
                    sbHtml.AppendLine("</tr>");

                    //写数据
                    foreach (System.Data.DataRow row in data.Rows)
                    {
                        sbHtml.Append("<tr>");

                        foreach (System.Data.DataColumn column in data.Columns)
                        {
                            sbHtml.Append("<td>").Append(row[column].ToString()).Append("</td>");
                        }
                        sbHtml.AppendLine("</tr>");
                    }
                    sbHtml.AppendLine("</table>");
                    System.Web.HttpContext.Current.Response.Write(sbHtml.ToString());
                    System.Web.HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(String.Format("ExportExcel 异常"), ex);
            }
        }

        /// <summary>
        /// Excel检查版本
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string ConnectionString(string fileName)
        {
            bool isExcel2003 = fileName.EndsWith(".xls");
            string connectionString = string.Format(
                isExcel2003
                    ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;"
                    : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"",
                fileName);
            return connectionString;
        }
        /// <summary>
        /// Excel导入数据源
        /// </summary>
        /// <param name="sheet">sheet</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataSet(string sheet, string filename)
        {
            try
            {
                DataSet ds;
                OleDbConnection myConn = new OleDbConnection(ConnectionString(filename));
                myConn.Open();
                DataTable dttablename = myConn.GetSchema("Tables");

                sheet = sheet + "$";
                bool exist= dttablename.AsEnumerable().Any(n => n["Table_Name"].ToString() == sheet);
                if(!exist)
                {
                    sheet = dttablename.Rows[0]["Table_Name"].ToString();
                }
                string strCom = " SELECT * FROM [" + sheet + "]";
             
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                ds = new DataSet();
                myCommand.Fill(ds);
                myConn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                LogHelper.Error(String.Format("ExcelToDataSet {0}  {1}  异常", sheet, filename), ex);
                return null;
            }
        }
    }

}
