using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Span.GUI
{
    public partial class FormAddEvent : ThinDialog
    {
        public FormAddEvent()
        {
            InitializeComponent();
            ExtraInit(false);
        }

        public FormAddEvent(bool a_isTask)
        {
            InitializeComponent();
            ExtraInit(a_isTask);
        }

        private void ExtraInit(bool a_isTask)
        {
            cbType.SelectedIndex = cbType.FindStringExact(a_isTask ? "Task" : "Appointment");
            cbCatPrim.DataSource = Category.All.Values.ToList();
            cbCatPrim.DisplayMember = "Name";
            cbCatPrim.ValueMember = "Id";
            m_secondcats = new List<string>();
        }

        public bool IsTask
        {
            get
            {
                return m_isTask;
            }
            set
            {
                m_isTask = value;
                lblTaskNum.Enabled = m_isTask;
                nudTaskNum.Enabled = m_isTask;
            }
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            FormAlarmSettings popup = new FormAlarmSettings();
            popup.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddOcc_Click(object sender, EventArgs e)
        {
            bool canClose = true;
            if (tbName.Text.Trim().Equals(""))
            {
                canClose = false;
            }
            if (canClose)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("One or more fields are missing");
            }
        }

        private void FormAddEvent_Load(object sender, EventArgs e)
        {

        }

        private bool m_isTask;

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            IsTask = (cbType.SelectedItem.ToString().Equals("Task"));
        }

        private void btnCatSec_Click(object sender, EventArgs e)
        {
            FormAddCategories popup = new FormAddCategories(false);
            List<int> incats = new List<int>();
            foreach (string id in m_secondcats){
                int index = (int)Category.All[id].Number - 1;
                incats.Add(index);
               
            }
            popup.Checked = incats;
            popup.Disabled = (int)Category.All[m_primarycat].Number - 1;
            popup.ShowDialog();
            MessageBox.Show("end");
            
            m_secondcats.Clear();
            foreach (int index in popup.Checked)
            {
                m_secondcats.Add(Category.All.First(x => x.Value.Number == index + 1).Key);
            }
        }

        private List<string> m_secondcats;
        private string m_primarycat;
        

        private void cbCatPrim_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_primarycat = (string)cbCatPrim.SelectedValue;
        }
    }
}
