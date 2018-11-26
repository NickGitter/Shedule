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
    /// Класс для работы с Excel
    /// </summary>
    public class TheExcelManager
    {
        static object m_lockForSingleTon = new object();
        private static TheExcelManager m_sInstance = null;
        private TheExcelManager() { }
        public static TheExcelManager Instance()
        {
            if (m_sInstance == null)
            {
                lock (m_lockForSingleTon)
                {
                    if (m_sInstance == null)
                    {
                        m_sInstance = new TheExcelManager();
                    }
                }
            }
            return m_sInstance;
        }
        ~TheExcelManager()
        {
            Clear();
        }

        Excel.Application m_ExcelApp = null;
        Excel.Workbook m_ExcelWB;
        Excel.Worksheet m_ExcelWS;
        private bool m_bInit = false;
        public void Init()
        {
            if (m_bInit)
                return;

            try
            {
                m_bInit = true;
                m_ExcelApp = new Excel.Application();
                m_ExcelApp.Visible = false;
            }
            catch
            {
                m_bInit = false;
                throw new Exception("Can't run Excel");
            }
        }
        public void Clear()
        {
            if (m_ExcelApp != null)
                Marshal.FinalReleaseComObject(m_ExcelApp);
        }
        public List<T> ReadFile<T>(string fileName,Func<Excel.Worksheet, int, T> readRow)
        {
            if (!m_bInit)
            {
                try
                {
                    Init();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            try
            {
                m_ExcelWB = m_ExcelApp.Workbooks.Open(fileName);
                if (m_ExcelWB.Sheets.Count == 0)
                {
                    throw new Exception(string.Format("Не верный формат файла \"{0}\"", fileName));
                }
                m_ExcelWS = m_ExcelWB.Sheets[1];

                int rowI = 2; // пропускаем заголовок таблицы + индексация не с нуля, а с единицы
                bool bReadDataFromExcelCycle = true;
                List<T> readedList = new List<T>();
                T readedItem;
                do
                {
                    try
                    {
                        readedItem = readRow(m_ExcelWS, rowI);

                        if (readedItem == null)
                            bReadDataFromExcelCycle = false;
                        else
                        {
                            readedList.Add(readedItem);
                            ++rowI;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                } while (bReadDataFromExcelCycle);
                m_ExcelWB.Close();
                return readedList;
            }
            catch
            {
                throw new Exception(string.Format("Не удалось открыть файл \"{0}\"", fileName));
            }
        }
    }
}
