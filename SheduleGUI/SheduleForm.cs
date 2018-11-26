using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SheduleLib;

namespace SheduleGUI
{
    public partial class g_SheduleForm : Form
    {
        const int TIME_TO_READEXCEL_LIMIT = 3 * 60 * 1000;
        const string g_cFileWithPartiesName         = "parties.xlsx";
        const string g_cFileWithMachinesName        = "machine_tools.xlsx";
        const string g_cFileWithNomenclaturesName   = "nomenclatures.xlsx";
        const string g_cFileWithTimes               = "times.xlsx";

        SheduleLib.SheduleModel m_sheduleModel = new SheduleModel();
        SheduleLib.Shedule m_shedule = null;
        public g_SheduleForm()
        {
            InitializeComponent();
            g_bGenerateShedule.Enabled  = false;
            g_bSaveAs.Enabled           = false;
        }
        /// <summary>
        /// Загрузить папку с excel - файлами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void g_tsmiOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdg = new FolderBrowserDialog();
            fdg.Description = string.Format("Выберите папку, содержащую все файлы для обработки сырья:\n\"{0}\"\n\"{1}\"\n\"{2}\"\n\"{3}\"", g_cFileWithMachinesName,g_cFileWithNomenclaturesName,g_cFileWithPartiesName,g_cFileWithTimes);
            if (fdg.ShowDialog() == DialogResult.OK)
            {
                var folder = fdg.SelectedPath;
                System.IO.DirectoryInfo DI = new System.IO.DirectoryInfo(fdg.SelectedPath);

                string pathForParties       = "";
                string pathForNomenclatures = "";
                string pathForMachines      = "";
                string pathForTimes         = "";

                foreach (var a in DI.GetFiles())
                {
                    if (a.Name.Equals(g_cFileWithPartiesName))
                        pathForParties = a.FullName;
                    if (a.Name.Equals(g_cFileWithNomenclaturesName))
                        pathForNomenclatures = a.FullName;
                    if (a.Name.Equals(g_cFileWithMachinesName))
                        pathForMachines = a.FullName;
                    if (a.Name.Equals(g_cFileWithTimes))
                        pathForTimes = a.FullName;
                }

                bool bAllFilesConsist = true;

                if (pathForMachines.Equals(""))
                {
                    MessageBox.Show(string.Format("Отсутствует файл {0}", g_cFileWithMachinesName));
                    bAllFilesConsist = false;
                }
                if (pathForNomenclatures.Equals(""))
                {
                    MessageBox.Show(string.Format("Отсутствует файл {0}", g_cFileWithNomenclaturesName));
                    bAllFilesConsist = false;
                }
                if (pathForParties.Equals(""))
                {
                    MessageBox.Show(string.Format("Отсутствует файл {0}", g_cFileWithPartiesName));
                    bAllFilesConsist = false;
                }
                if (pathForTimes.Equals(""))
                {
                    MessageBox.Show(string.Format("Отсутствует файл {0}", g_cFileWithTimes));
                    bAllFilesConsist = false;
                }

                if (!bAllFilesConsist)
                    return;

                Cursor = Cursors.WaitCursor;
                var readExcelTask = Task.Run(
                () =>
                {
                    m_sheduleModel.ReadFromExcel(pathForMachines, pathForParties, pathForNomenclatures, pathForTimes);

                });
                readExcelTask.Wait(TIME_TO_READEXCEL_LIMIT);
                if (readExcelTask.Status != TaskStatus.RanToCompletion)
                {
                    MessageBox.Show("Ошибка, не удалось открыть файл");
                }
               
                g_bGenerateShedule.Enabled = true;
                g_bSaveAs.Enabled = true;
                Cursor = Cursors.Default;
                GenerateShedule();

            }
        }
        private void GenerateShedule()
        {
            m_shedule = m_sheduleModel.GenerateShedule("Простой способ");
            var shd = m_shedule.ToSortedList();

            g_dgvShedule.RowCount = shd.Count();
            g_dgvShedule.ColumnCount = 4;

            g_dgvShedule.Columns[0].HeaderText = "Партия";
            g_dgvShedule.Columns[1].HeaderText = "Оборудование";
            g_dgvShedule.Columns[2].HeaderText = "Начало обработки";
            g_dgvShedule.Columns[3].HeaderText = "Конец обработки";

            for (int sheduleI = 0; sheduleI < shd.Count; ++sheduleI)
            {

                g_dgvShedule[0, sheduleI].Value = shd[sheduleI].PartId.ToString();
                g_dgvShedule[1, sheduleI].Value = m_sheduleModel.GetMachines.MachinesList[shd[sheduleI].MachineId].Name;
                g_dgvShedule[2, sheduleI].Value = shd[sheduleI].StartTime.ToTime();
                g_dgvShedule[3, sheduleI].Value = shd[sheduleI].EndTime.ToTime();

                g_dgvShedule[0, sheduleI].ReadOnly = true;
                g_dgvShedule[1, sheduleI].ReadOnly = true;
                g_dgvShedule[2, sheduleI].ReadOnly = true;
                g_dgvShedule[3, sheduleI].ReadOnly = true;
            }
        }

        private void g_bGenerateShedule_Click(object sender, EventArgs e)
        {
        }

        private void g_bSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "EXCEL|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                m_shedule.SaveAs(sfd.FileName, m_sheduleModel.GetMachines, m_sheduleModel.GetParties, m_sheduleModel.GetNomenclatures) ;
            }
        }

        private void g_SheduleForm_Load(object sender, EventArgs e)
        {
        }
    }
}
