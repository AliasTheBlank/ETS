using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ETS.Library;

namespace TelethonSystem
{
    public partial class ETSTelethon : Form
    {
        ETSManager myManager;
        public ETSTelethon(ETSManager manager)
        {
            InitializeComponent();
            myManager = manager;
        }
    }
}
