using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAutoNumerator
{
    public partial class FormatDialog : Form
    {
        public string MainName
        {
            set { textBox2.Text = value; }
            get { return textBox2.Text; }
        }

        public string Devider
        {
            set { textBox1.Text = value; }
            get { return textBox1.Text; }
        }

        public int NumberOfDigits
        {
            set 
            {
                int val = value;
                if (val < 2) val = 2;
                if (val > 10) val = 10;
                numericUpDown1.Value = (decimal)val;
            }
            get 
            {
                return (int)numericUpDown1.Value;
            }
        }

        public int Shift
        {
            set
            {
                int val = value;
                if (val < 0) val = 0;
                if (val > 65535) val = 65535;
                numericUpDown2.Value = (decimal)val;
            }
            get
            {
                return (int)numericUpDown2.Value;
            }
        }

        public FormatDialog()
        {
            InitializeComponent();
        }
    }
}
