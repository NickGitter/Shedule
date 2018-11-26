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
    /// Машина
    /// </summary>
    public class Machine : BasePart
    {
        protected Machine() { }
        string m_name;
        public string Name { get { return m_name; } }
        public Machine(int id, string name) : base(id) { m_name = name; }
    }
    /// <summary>
    /// Контейнер машин
    /// </summary>
    public class Machines : IreadablyFromEXCEL
    {
        private List<Machine> m_machines = new List<Machine>();
        public List<Machine> MachinesList { get { return m_machines; } }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var machine in m_machines)
            {
                sb.Append(machine.ID.ToString()).Append(" ").Append(machine.Name).Append("\n");
            }
            return sb.ToString();
        }
        public void ReadFromFile(string fileName)
        {
            m_machines.Clear();

            m_machines = TheExcelManager.Instance().ReadFile(fileName,
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
                        int machineId;
                        string name;
                        bParseFl = int.TryParse(excelWS.Cells[row, 1].Value2.ToString(), out machineId);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }
                        name = excelWS.Cells[row, 2].Value2.ToString();

                        return new Machine(machineId, name);
                    }
                });
            /*
            m_machines.Clear();

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
                    m_machines.Add(new Machine(int.Parse(excelWS.Cells[i, 1].Value2.ToString()), excelWS.Cells[i, 2].Value2.ToString()));
                }
                ++i;
            } while (bReadDataFromExcelCycle);
            excelWB.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);*/
        }
    }
}
