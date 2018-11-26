using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleLib
{
    public class SheduleModel
    {
        SheduleGenerateFactory m_ShdGenerateStrategyFabric = new SheduleGenerateFactory();
        Machines m_machines = new Machines();
        Parties m_parties = new Parties();
        Nomenclatures m_nomenclatures = new Nomenclatures();
        TimeManager m_TM = new TimeManager();
        
        //public List<Machine> MachinesList { get { return m_machines.MachinesList; } }
        //public List<Part> PartiesList { get { return m_parties.PartiesList; } }
        //public List<Nomenclature> Nomenclatures { get { return m_nomenclatures.NomenclaturesList; } }
        //public TimeManager TimeManager { get { return m_TM; } }


        public Machines GetMachines { get { return m_machines; } }
        public Parties GetParties { get { return m_parties; } }
        public Nomenclatures GetNomenclatures { get { return m_nomenclatures; } }
        public TimeManager GetTimeManager { get { return m_TM; } }

        public string[] GetAllStrategyNames()
        {
            return m_ShdGenerateStrategyFabric.GetAllStrategyNames();
        }

        public void ReadFromExcel(string pathForMachine,string pathForParties, string pathForNomenclatures, string pathForTimes)
        {
            try
            {
                m_machines          .ReadFromFile(pathForMachine);
                m_parties           .ReadFromFile(pathForParties);
                m_nomenclatures     .ReadFromFile(pathForNomenclatures);
                m_TM                .ReadFromFile(pathForTimes);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Shedule GenerateShedule (string SheduleGenStrategyName)
        {
            var shdGenerateStrategy = m_ShdGenerateStrategyFabric.GetStrategyByName(SheduleGenStrategyName);
            if (shdGenerateStrategy == null)
            {
                return null;
            }
            return shdGenerateStrategy.GenerateShedule(m_parties.PartiesList, m_machines.MachinesList, m_nomenclatures.NomenclaturesList, m_TM);
        }
    }
}
