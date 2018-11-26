using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleLib
{
    /// <summary>
    /// Хранилище всех возможных стратегий
    /// </summary>
    public class SheduleGenerateFactory
    {
        private Dictionary<string, SheduleGenerateStrategyBase> m_SheduleGenerateStrategyFabric = new Dictionary<string, SheduleGenerateStrategyBase>();
        public SheduleGenerateFactory()
        {
            m_SheduleGenerateStrategyFabric.Add("Простой способ", DefaultStrategy);
        }
        public string[] GetAllStrategyNames()
        {
            return m_SheduleGenerateStrategyFabric.Keys.ToArray();
        }
        private SheduleGenerateStrategyBase m_defaultStrategy = new SheduleGeneratorSimpleStrategy();
        public SheduleGenerateStrategyBase DefaultStrategy { get { return m_defaultStrategy; } }
        public SheduleGenerateStrategyBase GetStrategyByName(string name)
        {
            if (!m_SheduleGenerateStrategyFabric.Keys.Contains(name))
            {
                return DefaultStrategy;
            }
            else
            {
                return m_SheduleGenerateStrategyFabric[name];
            }
        }
    }
}
