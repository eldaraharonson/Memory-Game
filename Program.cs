using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsMemoryGame
{
    public class Program
    {
        public static void Main()
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }
    }
}
