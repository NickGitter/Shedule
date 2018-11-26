using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheduleLib
{
    /// <summary>
    /// Расписание
    /// </summary>
    public class Shedule
    {
        /* Проще хранить расписание в виде словаря, где:
         *  - ключ:     идентификатор машины
         *  - значение: расписание для конкретной машины
         */
        private Dictionary<int, List<SheduleList>> m_shedule = new Dictionary<int, List<SheduleList>>();
        public void AddMachine(int machineId)
        {
            m_shedule.Add(machineId, new List<SheduleList>());
        }
        /// <summary>
        /// Добавить Список SheduleList в расписание
        /// </summary>
        /// <param name="machineId"> Идентификатор машины </param>
        /// <param name="nomenclatureId"> Идентификатор номенклатуры</param>
        /// <param name="partId"> Идентификатор партии </param>
        /// <param name="workTime"> Время, необходимое на обработку </param>
        public void AddList(int machineId, int nomenclatureId,int partId, int workTime)
        {
            if (!m_shedule.ContainsKey(machineId))
                return;

            var sheduleForCurrentMachine = m_shedule[machineId];
            
            int lastTime = 0;

            if (sheduleForCurrentMachine.Count != 0)
            {
                lastTime = sheduleForCurrentMachine[sheduleForCurrentMachine.Count - 1].EndTime;
            }

            m_shedule[machineId].Add(new SheduleList(machineId, nomenclatureId, partId, lastTime, lastTime + workTime));
        }
        /// <summary>
        /// Удалить листь 
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="SL"></param>
        public void RemoveListFromShedule(int machineId,SheduleList SL)
        {
            if (!m_shedule.ContainsKey(machineId))
                return;
            m_shedule[machineId].Remove(SL); // remove from shedule
        }
        /// <summary>
        /// Генерирует список, отсортированный по времени начала выполнения операции переработки сырья
        /// </summary>
        /// <returns></returns>
        public List<SheduleList> ToSortedList()
        {
            List<SheduleList> fullLinearShedule = new List<SheduleList>();
            foreach (var machine in m_shedule.Keys)
            {
                foreach (var shedulePart in m_shedule[machine])
                {
                    int indexForInsert; // индекс, после которого необходимо вставить новый элемент с учетом сортировки
                    indexForInsert = fullLinearShedule.FindIndex((sheduleItem) => { if (shedulePart.StartTime < sheduleItem.StartTime) return true; else if ((shedulePart.StartTime == sheduleItem.StartTime) && (shedulePart.PartId < sheduleItem.PartId)) return true; return false; });
                    if (indexForInsert == -1)
                    {
                        fullLinearShedule.Add(shedulePart);
                    }
                    else
                    {
                        fullLinearShedule.Insert(indexForInsert, shedulePart);
                    }
                }
            }
            return fullLinearShedule;
        }
        /// <summary>
        /// Возвращает время окончания последней операции для заданной машины
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        public int LastMachineWorkTime(int machineId)
        {
            if (!m_shedule.ContainsKey(machineId))
                return -1;
            else
                if (m_shedule[machineId].Count == 0)
                    return 0;
                else
                    return m_shedule[machineId][m_shedule[machineId].Count - 1].EndTime;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var m in m_shedule.Keys)
            {
                sb.Append("Machine №" + m.ToString()).Append("\n");
                foreach (var shL in m_shedule[m])
                    sb.Append("\tNomenclature: " + shL.NomenclatureId.ToString() + " from " + shL.StartTime.ToString() + " to" + shL.EndTime).Append("\n");
            }

            return sb.ToString();
        }
        #region shedule help func
        /// <summary>
        /// Перерасчитывает время расписания после опреаци изменения расписания
        /// </summary>
        /// <param name="shedule"> расписание </param>
        /// <param name="startIndexForShifting"> индекс, начиная с которого необходимо перерасчитать расписание </param>
        /// <param name="workTime">время, на которое необходимо увеличить расписание</param>
        void shiftSheduleByWorkTime(List<SheduleList> shedule, int startIndexForShifting, int workTime)
        {
            for (int shdI = startIndexForShifting; shdI < shedule.Count; ++shdI)
            {
                shedule[shdI].StartTime += workTime;
                shedule[shdI].EndTime += workTime;
            }
        }
        #endregion
        #region Commands for change shedule
        /// <summary>
        /// Удаляет строку из расписания
        /// </summary>
        /// <param name="machineId"> Идентификатор мишны</param>
        /// <param name="posInShedule"> Номер в расписании (начиная с 0-я) </param>
        public void RemoveSheduleList(int machineId, int posInShedule)
        {
            if (!m_shedule.ContainsKey(machineId))  // проверяем на наличие машины
                return;
           
            var shd = m_shedule[machineId]; // элемент, который необходимо удалить


            if (posInShedule < shd.Count) 
            {
                shd.RemoveAt(posInShedule);                                         // удаляем элемент

                int freeTime = shd[machineId].EndTime - shd[machineId].StartTime;   // образовавшееся свободное время в расписании

                /* Перерасчитываем расписание в случае, если был удален элемент из середины расписания */
                shiftSheduleByWorkTime(shd, posInShedule, -freeTime);
            }
        }
        /// <summary>
        /// Вставляет строку в расписание
        /// </summary>
        /// <param name="machineId"> идентификатор машины </param>
        /// <param name="posInShedule"> позиция в расписании, на место которой нужно вставить стркоу </param>
        /// <param name="nomenclatureId"> номенклатура сырья </param>
        /// <param name="TM"> менеджер времени </param>
        public void InsertSheduleList(int machineId, int posInShedule, int nomenclatureId, int partId, TimeManager TM)
        {
            if (!m_shedule.ContainsKey(machineId))  // проверяем на наличие машины
                return;
            int workTime = TM.GetTime(machineId, nomenclatureId);
            if (workTime == -1) // проверяем, можем ли мы обработать сырье с помощью выбранной машины
                return;

            var shd = m_shedule[machineId];

            if (posInShedule < shd.Count) // Добавляем строку в расписание
            {
                int startTime, endTime;
                /* расчитываем время*/
                if (posInShedule == 0) // 
                {
                    startTime = 0;
                    endTime = workTime;
                }
                else
                {
                    startTime = shd[posInShedule].EndTime;
                    endTime = startTime + workTime;
                }
                shd.Insert(posInShedule, new SheduleList(machineId, nomenclatureId, partId, startTime, endTime));
                shiftSheduleByWorkTime(shd, posInShedule + 1, workTime);
            }
        }
        #endregion
        public void SaveAs(string path, Machines machines, Parties parties,  Nomenclatures nomenclatures)
        {
            if (m_shedule.Count == 0)
                return;
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            Excel.Workbook excelWB = excelApp.Workbooks.Add(1);
            Excel.Worksheet excelWS = (Excel.Worksheet)excelWB.Sheets[1];


            foreach (var part in parties.PartiesList)
            {

            }

            /* формируем заголовок */
            excelWS.Cells[1, 1] = "Партия";
            excelWS.Cells[1, 2] = "Оборудование";
            excelWS.Cells[1, 3] = "Начало обработки";
            excelWS.Cells[1, 4] = "Конец обработки";

            int machineId;
            int excelRow = 2;
            int excelColumn = 1;
            int firstTime;

            List<SheduleList> fullLinearShedule = this.ToSortedList();

            excelWS.Columns.EntireColumn.AutoFit();
            excelWS.Range[ excelWS.Cells[1,1], excelWS.Cells[1, 4]].Interior.Color = System.Drawing.Color.LightGreen;
            foreach (var sheduleList in fullLinearShedule)
            {
                excelWS.Cells[excelRow, 1] = sheduleList.PartId.ToString();
                excelWS.Cells[excelRow, 2] = machines.MachinesList[sheduleList.MachineId].Name;
                excelWS.Cells[excelRow, 3] = sheduleList.StartTime.ToString();
                excelWS.Cells[excelRow, 4] = sheduleList.EndTime.ToString();
                var range = excelWS.Range[excelWS.Cells[excelRow, 1], excelWS.Cells[excelRow, 4]];
                if (excelRow % 2 == 0)
                    range.Interior.Color = System.Drawing.Color.LightGray;
                else
                    range.Interior.Color = System.Drawing.Color.LightBlue;
                ++excelRow;
            }
            excelWB.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);
            excelApp.Workbooks.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
        }
    }
}
