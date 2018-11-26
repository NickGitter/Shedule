using System;
using System.Collections.Generic;
using System.Text;

namespace SheduleLib
{
    /// <summary>
    /// Один элемент из расписания
    /// 
    /// </summary>
    public class SheduleList
    {
        public int MachineId { get; set; }
        public int NomenclatureId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int PartId { get; set; } 
        public SheduleList(int machineId, int nomenclatureId, int partId, int startTime, int endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            NomenclatureId = nomenclatureId;
            MachineId = machineId;
            PartId = partId;
        }
        public override string ToString()
        {
            return string.Format("Партия: {0,3} оборудование: {1,3} начало: {1,5} конец {1,5}", PartId, MachineId, StartTime, EndTime);
        }
    }
}
