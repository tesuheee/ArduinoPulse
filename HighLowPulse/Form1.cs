using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace HighLowPulse
{
	public partial class Form1 : Form
	{
		SerialPort serial;
		ComboBox combo;
		Label label;

		public Form1()
		{
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Size = new Size(100, 220);
			StartPosition = FormStartPosition.CenterScreen;
			KeyPreview = true;
			serial = new SerialPort("COM3", 9600);
			serial.Open();
			FormClosed += Form1_FormClosed;

			Button btn1 = new Button();
			btn1.Parent = this;
			btn1.Location = new Point(25, 50);
			btn1.AutoSize = true;
			btn1.Text = "HIGH";
			btn1.FlatStyle = FlatStyle.System;
			btn1.Click += (object sender, EventArgs e) => { serial.Write(new byte[] { Convert.ToByte("1") }, 0, 1); label.Text = "HIGH"; serial.DiscardOutBuffer(); };

			Button btn2 = new Button();
			btn2.Parent = this;
			btn2.Location = new Point(25, btn1.Top + 30);
			btn2.AutoSize = true;
			btn2.Text = "LOW";
			btn2.FlatStyle = FlatStyle.System;
			btn2.Click += (object sender, EventArgs e) => { serial.Write(new byte[] { Convert.ToByte("2") }, 0, 1); label.Text = "LOW"; serial.DiscardOutBuffer(); };

			Button btn3 = new Button();
			btn3.Parent = this;
			btn3.Location = new Point(25, btn2.Top + 30);
			btn3.AutoSize = true;
			btn3.Text = "PULSE";
			btn3.FlatStyle = FlatStyle.System;
			btn3.Click += (object sender, EventArgs e) => { serial.Write(new byte[] { Convert.ToByte("3") }, 0, 1); label.Text = "PULSE"; serial.DiscardOutBuffer(); };

			combo = new ComboBox();
			combo.Parent = this;
			combo.AutoScrollOffset = new Point(100, 100);
			combo.DropDownStyle = ComboBoxStyle.DropDownList;
			combo.Width = btn1.Width;
			combo.Location = new Point(25, btn3.Top + 30);
			combo.Items.Add(100);
			combo.Items.Add(500);
			combo.Items.Add(1000);
			combo.SelectedIndex = 0;
			combo.SelectedValueChanged += serial_delay;

			label = new Label();
			label.Location = new Point(45, 20);
			label.Parent = this;
		}

		void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			serial.Write(new byte[] { Convert.ToByte("4") }, 0, 1);
			serial.Write(new byte[] { Convert.ToByte("0") }, 0, 1);
			serial.Write(new byte[] { Convert.ToByte("100") }, 0, 1);
			serial.Write(new byte[] { Convert.ToByte("2") }, 0, 1);
		}

		void serial_delay(object sender, EventArgs e)
		{
			serial.Write(new byte[] { Convert.ToByte("4") }, 0, 1);
			label.Text = combo.SelectedItem.ToString() + "ms";
			int value = (int)combo.SelectedItem;
			int high = value >> 8;
			int low = value & 255;
			serial.Write(new byte[] { Convert.ToByte(high) }, 0, 1);
			serial.Write(new byte[] { Convert.ToByte(low) }, 0, 1);
		}
	}
}
