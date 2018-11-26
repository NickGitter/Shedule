using System;
using System.Collections.Generic;
using System.Text;

namespace SheduleLib
{
    public abstract class SheduleGenerateStrategyBase
    {
        public abstract Shedule GenerateShedule(List<Part> parties, List<Machine> machines, List<Nomenclature> nomenclatures, TimeManager TM);
    }
}
