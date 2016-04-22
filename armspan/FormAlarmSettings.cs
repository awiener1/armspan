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
    public partial class FormAlarmSettings : ThinDialog
    {
        public FormAlarmSettings()
        {
            InitializeComponent();

        }
        private void InitializeControlLists()
        {
            cbBeforeList = new List<CheckBox>();
            cbBeforeList.AddRange(tlpBefore.Controls.OfType<CheckBox>());
            nudBeforeList = new List<NumericUpDown>();
            nudBeforeList.AddRange(tlpBefore.Controls.OfType<NumericUpDown>());
            ddlBeforeList = new List<ComboBox>();
            ddlBeforeList.AddRange(tlpBefore.Controls.OfType<ComboBox>());

            cbDuringList = new List<CheckBox>();
            cbDuringList.AddRange(tlpDuring.Controls.OfType<CheckBox>());
            nudDuringList = new List<NumericUpDown>();
            nudDuringList.AddRange(tlpDuring.Controls.OfType<NumericUpDown>());
            ddlDuringList = new List<ComboBox>();
            ddlDuringList.AddRange(tlpDuring.Controls.OfType<ComboBox>());

            cbAfterList = new List<CheckBox>();
            cbAfterList.AddRange(tlpAfter.Controls.OfType<CheckBox>());
            nudAfterList = new List<NumericUpDown>();
            nudAfterList.AddRange(tlpAfter.Controls.OfType<NumericUpDown>());
            ddlAfterList = new List<ComboBox>();
            ddlAfterList.AddRange(tlpAfter.Controls.OfType<ComboBox>());

            ddlAllList = new List<ComboBox>();
            ddlAllList.AddRange(ddlBeforeList);
            ddlAllList.AddRange(ddlDuringList);
            ddlAllList.AddRange(ddlAfterList);

            foreach (ComboBox ddl in ddlAllList)
            {
                ddl.SelectedIndex = ddl.FindStringExact("Minutes");
            }

            //MessageBox.Show(string.Join(", ", cbBeforeList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", nudBeforeList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", ddlBeforeList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", cbDuringList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", nudDuringList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", ddlDuringList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", cbAfterList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", nudAfterList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", ddlAfterList.Select(o => o.Name).ToArray()));
        }
        private void FormAlarmSettings_Load(object sender, EventArgs e)
        {
            InitializeControlLists();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<CheckBox> cbBeforeList;
        private List<NumericUpDown> nudBeforeList;
        private List<ComboBox> ddlBeforeList;
        private List<CheckBox> cbDuringList;
        private List<NumericUpDown> nudDuringList;
        private List<ComboBox> ddlDuringList;
        private List<CheckBox> cbAfterList;
        private List<NumericUpDown> nudAfterList;
        private List<ComboBox> ddlAfterList;
        private List<ComboBox> ddlAllList;
    }
}
