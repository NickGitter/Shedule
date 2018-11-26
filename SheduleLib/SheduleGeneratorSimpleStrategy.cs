using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheduleLib
{
    /// <summary>
    /// Самый примитивный способ генерации расписания:
    ///     Для каждой партии: 
    ///         - находим все машины, которые могут переработать сырье
    ///         - Сортируем машины по скорости обработки сырья
    ///         - Выбираем ту, которая ранее завершит обработку сырья, учитывая следующие показатели:
    ///             * Скорость обработки сырья;
    ///             * Время, когда машина освободиться shedule.LastMachineWorkTime(i.ID).
    ///         - добавляем строчку в расписание
    /// </summary>
    public class SheduleGeneratorSimpleStrategy : SheduleGenerateStrategyBase
    {
        public override Shedule GenerateShedule( List<Part> parties, List<Machine> machines, List<Nomenclature> nomenclatures, TimeManager TM)
        {
            Shedule shedule = new Shedule();
            foreach (var machine in machines)
                shedule.AddMachine(machine.ID);
            
            int bestMachineId;

            /* Формируем отдельный список подходящих машин для каждой номенклатуры */
            List<int>[] capableMachines = new List<int>[nomenclatures.Count];
            foreach (var nomenclature in nomenclatures)
            {
                capableMachines[nomenclature.ID] = 
                    (from machineI in machines
                        let machineProcessingTime = TM.GetTime(machineI.ID, nomenclature.ID)
                        where (machineProcessingTime != -1)
                        orderby machineProcessingTime
                        select machineI.ID
                        ).ToList();
            }

            foreach(var part in parties)
            {
                var bestMachinesForCurPart = from machineId in capableMachines[part.NomenclatureId] orderby shedule.LastMachineWorkTime(machineId) + TM.GetTime(machineId, part.NomenclatureId) select machineId;
                if (bestMachinesForCurPart.Count() == 0)
                {
                    throw new Exception(string.Format("Нельзя обработать партию №{0}",part.ID));
                }
                bestMachineId = bestMachinesForCurPart.First();
                shedule.AddList(bestMachineId, part.NomenclatureId, part.ID, TM.GetTime(bestMachineId, part.NomenclatureId));
            }
            return shedule;
        }
    }
}
