using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TreeGenerator
{
    public partial class BoxRowEditor : BaseRowEditor
    {
        public BoxRowEditor()
        {
            InitializeComponent();
        }

        private void BoxRowEditor_Load(object sender, EventArgs e)
        {
           
        }
        public void SetDetails(BoxDetail box)
        {
            editpropertyname.Text = box.name;
            numericUpDown1.Value = box.boxthickness;
        }
    }
}
