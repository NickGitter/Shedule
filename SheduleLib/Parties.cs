using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheduleLib
{
    /// <summary>
    /// Элемент партии
    /// </summary>
    public class Part : BasePart
    {
        protected Part() { }
        private int m_nomenclatureId;
        public Part(int id, int nomenclatureId) : base(id) { m_nomenclatureId = nomenclatureId; }
        public int NomenclatureId
        {
            get { return m_nomenclatureId; }
        }
    }
    /// <summary>
    /// Контейнер партий
    /// </summary>
    public class Parties : IreadablyFromEXCEL
    {
        private List<Part> m_parties = new List<Part>();
        public List<Part> PartiesList { get { return m_parties; } }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var part in m_parties)
            {
                sb.Append(part.ID.ToString()).Append(" ").Append(part.NomenclatureId).Append("\n");
            }
            return sb.ToString();
        }
        public void ReadFromFile(string fileName)
        {
            m_parties.Clear();

            m_parties = TheExcelManager.Instance().ReadFile(fileName,
                (Excel.Worksheet excelWS, int row) =>
                {
                    if ((excelWS.Cells[row, 1].Value2 == null) && (excelWS.Cells[row, 2].Value2 == null))
                        return null;
                    else
                    if ((excelWS.Cells[row, 1].Value2 == null) || (excelWS.Cells[row, 2].Value2 == null))
                        throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                    else
                    {
                        bool bParseFl;
                        int partId, nomenclatureId;
                        bParseFl = int.TryParse(excelWS.Cells[row, 1].Value2.ToString(), out partId);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }

                        bParseFl = int.TryParse(excelWS.Cells[row, 2].Value2.ToString(), out nomenclatureId);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }

                        return new Part(partId, nomenclatureId);
                    }
                });
            /*
            m_parties.Clear();

            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception(string.Format("No file consist \"{0}\"", fileName));
            }
            Excel.Application excelApp;
            Excel.Workbook excelWB;
            Excel.Worksheet excelWS;

            try
            {
                excelApp = new Excel.Application();
            }
            catch
            {
                throw new Exception("Can't run Excel");
            }

            try
            {
                excelApp.Visible = false;
                excelWB = excelApp.Workbooks.Open(fileName);
            }
            catch
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
                throw new Exception(string.Format("Can't open Excel file \"{0}\"", fileName));
            }
            if (excelWB.Sheets.Count == 0)
            {
                throw new Exception("Not correct file format");
            }
            excelWS = excelWB.Sheets[1];

            bool bReadDataFromExcelCycle = true;
            int i = 2;
            do
            {

                if ((excelWS.Cells[i, 1].Value2 == null) && (excelWS.Cells[i, 2].Value2 == null))
                    bReadDataFromExcelCycle = false;
                else
                    if ((excelWS.Cells[i, 1].Value2 == null) || (excelWS.Cells[i, 2].Value2 == null))
                    throw new Exception(string.Format("Not correct file format.\nMissing data.\n\tFile: \"{0}\"\n\tLine:{1}", fileName, i));
                else
                {
                    m_parties.Add(new Part(int.Parse(excelWS.Cells[i, 1].Value2.ToString()), int.Parse(excelWS.Cells[i, 2].Value2.ToString())));
                }
                ++i;
            } while (bReadDataFromExcelCycle);
            excelWB.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);*/
        }
    }
}
