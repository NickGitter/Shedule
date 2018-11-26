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
    /// Номенклатура
    /// </summary>
    public class Nomenclature : BasePart
    {
        string m_name;
        public string Name { get { return m_name; } }
        protected Nomenclature() { }
        public Nomenclature(int id, string name) : base(id) { m_name = name; }

        public void ReadFromFile(string excel)
        {
        }
    }
    /// <summary>
    /// Контейнер для номенклатур
    /// </summary>
    public class Nomenclatures : IreadablyFromEXCEL
    {
        private List<Nomenclature> m_nomenclatures = new List<Nomenclature>();
        public List<Nomenclature> NomenclaturesList { get { return m_nomenclatures; } }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var nomenclature in m_nomenclatures)
            {
                sb.Append(nomenclature.ID.ToString()).Append(" ").Append(nomenclature.Name).Append("\n");
            }
            return sb.ToString();
        }
        public void ReadFromFile(string fileName)
        {
            m_nomenclatures.Clear();

            m_nomenclatures = TheExcelManager.Instance().ReadFile(fileName,
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
                        int partId;
                        string name;
                        bParseFl = int.TryParse(excelWS.Cells[row, 1].Value2.ToString(), out partId);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }
                        name = excelWS.Cells[row, 2].Value2.ToString();
                        
                        return new Nomenclature(partId, name);
                    }
                });
            /*
            m_nomenclatures.Clear();

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
                    m_nomenclatures.Add(new Nomenclature(int.Parse(excelWS.Cells[i, 1].Value2.ToString()), excelWS.Cells[i, 2].Value2.ToString()));
                }
                ++i;
            } while (bReadDataFromExcelCycle);
            excelWB.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);*/
        }
    }
}
