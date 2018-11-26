using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace SheduleLib
{
    /// <summary>
    /// Базовый класс для всех элементов у которых есть ID
    /// </summary>
    public abstract class BasePart
    {
        protected BasePart() { }
        public BasePart(int id) { m_id = id; }
        private int m_id;
        public int ID { get { return m_id; } }
    }
}
