using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;


public class NpoiExcelUtil
{
    public static byte[] GenerateXlsxBytes(string tableHtml)
    {
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + tableHtml;

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);

        XmlNode table = doc.SelectSingleNode("/table");

        int colspan = 1;
        int rowspan = 1;

        int rowNum;
        int columnNum;
        rowNum = 1;
        columnNum = 1;

        var workBook = new HSSFWorkbook();
        var ws = workBook.CreateSheet("Export");

        string mapKey = string.Empty;
        string mergKey = string.Empty;

        int rowCount = table.ChildNodes.Count;
        int colCount = FetchColCount(table.ChildNodes);

        InitSheet(ws, rowCount, colCount);

        bool[,] map = new bool[rowCount + 1, colCount + 1];

        foreach (XmlNode row in table.ChildNodes)
        {
            columnNum = 1;
            foreach (XmlNode column in row.ChildNodes)
            {
                if (column.Attributes["rowspan"] != null)
                {
                    rowspan = Convert.ToInt32(column.Attributes["rowspan"].Value);
                }
                else
                {
                    rowspan = 1;
                }

                if (column.Attributes["colspan"] != null)
                {
                    colspan = Convert.ToInt32(column.Attributes["colspan"].Value);
                }
                else
                {
                    colspan = 1;
                }

                while (map[rowNum, columnNum])
                {
                    columnNum++;
                }

                if (rowspan == 1 && colspan == 1)
                {
                    SetCellValue(ws, string.Format("{0}{1}", Char.Chr(columnNum), rowNum), column.InnerText);
                    map[rowNum, columnNum] = true;
                }
                else
                {
                    SetCellValue(ws, string.Format("{0}{1}", Char.Chr(columnNum), rowNum), column.InnerText);
                    mergKey =
                        string.Format("{0}{1}:{2}{3}",
                            Char.Chr(columnNum), rowNum, Char.Chr(columnNum + colspan - 1), rowNum + rowspan - 1);
                    MergCells(ws, mergKey);

                    for (int m = 0; m < rowspan; m++)
                    {
                        for (int n = 0; n < colspan; n++)
                        {
                            map[rowNum + m, columnNum + n] = true;
                        }
                    }
                }
                columnNum++;
            }
            rowNum++;
        }

        MemoryStream stream = new MemoryStream();
        workBook.Write(stream);

        return stream.ToArray();

    }

    static int FetchColCount(XmlNodeList nodes)
    {
        int colCount = 0;

        foreach (XmlNode row in nodes)
        {
            if (colCount < row.ChildNodes.Count)
            {
                colCount = row.ChildNodes.Count;
            }
        }

        return colCount;
    }

    static void InitSheet(ISheet sheet, int rowCount, int colCount)
    {
        for (int i = 0; i < rowCount; i++)
        {
            IRow row = sheet.CreateRow(i);
            for (int j = 0; j < colCount; j++)
            {
                row.CreateCell(j);
            }
        }
    }

    static void SetCellValue(ISheet sheet, string cellReferenceText, string value)
    {
        CellReference cr = new CellReference(cellReferenceText);
        IRow row = sheet.GetRow(cr.Row);
        ICell cell = row.GetCell(cr.Col);
        cell.SetCellValue(value);
    }

    static void MergCells(ISheet sheet, string mergeKey)
    {
        string[] cellReferences = mergeKey.Split(':');

        CellReference first = new CellReference(cellReferences[0]);
        CellReference last = new CellReference(cellReferences[1]);

        CellRangeAddress region = new CellRangeAddress(first.Row, last.Row, first.Col, last.Col);
        sheet.AddMergedRegion(region);
    }
}


public class Char
{
    public static string Chr(int i)
    {
        char c = (char)(64 + i);
        return c.ToString();
    }
}
