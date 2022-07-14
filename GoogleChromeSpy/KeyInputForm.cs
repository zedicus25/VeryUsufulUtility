using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleChromeSpy
{
    public partial class KeyInputForm : Form
    {
        public event Func<string, bool> KeyInsert;
        private int _keyLenght;
        private string _key;
        public KeyInputForm()
        {
            InitializeComponent();
        }
        public KeyInputForm(int keyLenght)
        {
            InitializeComponent();
            this._keyLenght = keyLenght;
            _key = String.Empty;
        }

        private void keyTB_TextChanged(object sender, EventArgs e)
        {
            _key = keyTB.Text;
            if (_key.Length == _keyLenght)
            {
                KeyInsert?.Invoke(_key);
                keyTB.Text = String.Empty;
                _key = String.Empty;
                this.Close();
            }
        }
    }
}
