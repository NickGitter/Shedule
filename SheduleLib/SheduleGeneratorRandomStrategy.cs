using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleLib
{
    class SheduleGeneratorRandomStrategy : SheduleGenerateStrategyBase
    {
        private static Random m_rnd = new Random();
        public override Shedule GenerateShedule(List<Part> parties, List<Machine> machines, List<Nomenclature> nomenclatures, TimeManager TM)
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

            foreach (var part in parties)
            {
                var bestMachinesForCurPart = from machineId in capableMachines[part.NomenclatureId] orderby shedule.LastMachineWorkTime(machineId) + TM.GetTime(machineId, part.NomenclatureId) select machineId;

                bestMachineId = bestMachinesForCurPart.ElementAt( m_rnd.Next( bestMachinesForCurPart.Count()));
                shedule.AddList(bestMachineId, part.NomenclatureId, part.ID, TM.GetTime(bestMachineId, part.NomenclatureId));
            }
            return shedule;
        }
    }
}
