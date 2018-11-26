using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheduleLib
{
    /// <summary>
    /// Менеджер времени
    /// </summary>
    public class TimeManager : IreadablyFromEXCEL
    {
        Dictionary<int, Dictionary<int, int>> m_times = new Dictionary<int, Dictionary<int, int>>();
        /// <summary>
        /// Добавить время необходимой машине на обработку сырья
        /// </summary>
        /// <param name="machineId">Идентификатор машины</param>
        /// <param name="nomenclatureId"> Идентификатор номенклатуры</param>
        /// <param name="time"></param>
        public void Add(int machineId, int nomenclatureId, int time)
        {
            if (!m_times.ContainsKey(machineId))
            {
                m_times.Add(machineId, new Dictionary<int, int>());
            }
            m_times[machineId].Add(nomenclatureId, time);
        }

        /// <summary>
        /// Get time for a processing
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="nomenclatureId"></param>
        /// <returns></returns>
        public int GetTime(int machineId,int nomenclatureId)
        {
            if (!m_times.ContainsKey(machineId))
                return -1;
            if (!m_times[machineId].ContainsKey(nomenclatureId))
                return -1;
            return m_times[machineId][nomenclatureId];
        }

        public bool CanMachineModernNomenclature(int machineId, int nomenclatureId)
        {
            return (GetTime(machineId,nomenclatureId) != (-1));
                //if (GetTime())
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var machin in m_times)
            {
                foreach (var nomenclatureTime in machin.Value) {
                    sb.Append(machin.Key.ToString()).Append(":").Append(nomenclatureTime.Key).Append(" ").Append(nomenclatureTime.Value).Append("\n");
                }
            }
            return sb.ToString();
        }

        class TMPForTimeManager
        {
            public int MachineId;
            public int NomenclatureId;
            public int Time;
        };
        public void ReadFromFile(string fileName)
        {
            m_times.Clear();
            List<TMPForTimeManager> tmpList = TheExcelManager.Instance().ReadFile(fileName,
                (Excel.Worksheet excelWS, int row) =>
                {
                    if ((excelWS.Cells[row, 1].Value2 == null) && (excelWS.Cells[row, 2].Value2 == null) && (excelWS.Cells[row, 3].Value2 == null))
                        return null;
                    else
                    if ((excelWS.Cells[row, 1].Value2 == null) || (excelWS.Cells[row, 2].Value2 == null) || (excelWS.Cells[row, 3].Value2 == null))
                        throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                    else
                    {
                        bool bParseFl;
                        int machineId, nomenclatureId, time;
                        bParseFl = int.TryParse(excelWS.Cells[row, 1].Value2.ToString(), out machineId);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }

                        bParseFl = int.TryParse(excelWS.Cells[row, 2].Value2.ToString(), out nomenclatureId);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }

                        bParseFl = int.TryParse(excelWS.Cells[row, 3].Value2.ToString(), out time);
                        if (!bParseFl)
                        {
                            throw new Exception(string.Format("Не верный формат файла \"{0}\".\nНе верные данные в строке №\"{1}", fileName, row));
                        }

                        return new TMPForTimeManager { MachineId = machineId, NomenclatureId = nomenclatureId, Time = time };
                    }
                });
            foreach (var item in tmpList)
            {
                Add(item.MachineId, item.NomenclatureId, item.Time);
            }
            tmpList.Clear();
            /*
            m_times.Clear();

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
                    this.Add(int.Parse(excelWS.Cells[i, 1].Value2.ToString()), int.Parse( excelWS.Cells[i, 2].Value2.ToString()), int.Parse( excelWS.Cells[i, 3].Value2.ToString()));
                }
                ++i;
            } while (bReadDataFromExcelCycle);
            excelWB.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);*/
        }
    }
}
