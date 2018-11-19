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

namespace SerialCommunication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
        }
        private Parity getParity(object a)
        {
            switch (a)
            {
                case "none":return Parity.None;
                case "even":return Parity.Even;
                case "mark":return Parity.Mark;
                case "odd":return Parity.Odd;
                case "space":return Parity.Space;
                default:return Parity.None;
            }
        }
        private Handshake getHandshake(object a)
        {
            switch (a)
            {
                case "none": return Handshake.None;
                case "Xon/Xoff": return Handshake.XOnXOff;
                case "request to send": return Handshake.RequestToSend;
                case "request to send Xon/Xoff": return Handshake.RequestToSendXOnXOff;
                default: return Handshake.None;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Open")
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.SelectedItem);
                    serialPort1.DataBits = Convert.ToInt32(comboBox3.SelectedItem);
                    serialPort1.Parity = getParity(comboBox4.SelectedItem);
                    serialPort1.Handshake = getHandshake(comboBox5.SelectedItem);
                    serialPort1.Open();
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    comboBox5.Enabled = false;
                    button1.Text = "Close";
                }
                else
                {
                    serialPort1.Close();
                    button1.Text = "Open";
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    comboBox5.Enabled = true;
                }
            }catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void serialPort1_DataReceived(object sender,SerialDataReceivedEventArgs s)
        {
            string a = serialPort1.ReadExisting();
            if (a.Contains("[") && textBox1.Text.Length!=0)
            {
                a = "\r\n" +a;
            }
            textBox1.Text += a;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a = textBox2.Text;
            serialPort1.Write("["+serialPort1.PortName+"]"+a);
            a = "[me]" + a;
            if (textBox1.Text.Length != 0)
            {
                a = "\r\n" + a;
            }
            textBox1.Text += a;
        }
    }
}

