using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
namespace app_xin
{
    public partial class Form1 : Form
    {
        SerialPort port = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            this.port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            string[] COM = SerialPort.GetPortNames();//lấy tất cả các cổng com có trên màn hình
            comboBox1.Items.AddRange(COM);
            string[] BAUD = { "4800", "9600", "19200", "38400", "115200" };
            comboBox2.Items.AddRange(BAUD);
            string[] Databit = { "7", "8", "9" };
            comboBox3.Items.AddRange(Databit);
            string[] ParityBit = { "none", "odd", "even" };
            comboBox4.Items.AddRange(ParityBit);
            string[] Stopbit = { "1", "1.5", "2" };
            comboBox5.Items.AddRange(Stopbit);
        }
        string data;
        string thetich1, mucnuoc1;
        int i = 1;
        int start, end;
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                data = port.ReadExisting();
                start = data.IndexOf('@');
                end = data.IndexOf('#');
                mucnuoc1 = data.Substring(start + 1, end - start - 1);
                int hienthi1 = Convert.ToInt32(mucnuoc1);
                textBox1.Text = hienthi1.ToString();
              
            }
            catch (Exception ex)
            { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port.IsOpen) port.Close();
            port.PortName = comboBox1.SelectedIndex.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port.IsOpen) port.Close();
            port.BaudRate = Convert.ToInt16(comboBox2.Text);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port.IsOpen) port.Close();
            port.DataBits = Convert.ToInt32(comboBox3.Text);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port.IsOpen) port.Close();
            switch (comboBox4.SelectedIndex.ToString())
            {
                case "none":
                    port.Parity = Parity.None;
                    break;
                case "odd":
                    port.Parity = Parity.Odd;
                    break;
                case "even":
                    port.Parity = Parity.Even;
                    break;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port.IsOpen) port.Close();
            switch (comboBox5.SelectedIndex.ToString())
            {
                case "1":
                    port.StopBits = StopBits.One;
                    break;
                case "1.5":
                    port.StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    port.StopBits = StopBits.Two;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                port.PortName = comboBox1.Text;
                port.Open();
                button1.Enabled = false;
                button2.Enabled = true;
                timer2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("LỖI KẾT NỐI", "THỬ LẠI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            port.Close();
            button1.Enabled = true;
            button2.Enabled = false;
            MessageBox.Show("Đã ngắt kết nối");
            timer2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 1;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            hienthi_csdl();
            timer2.Enabled = false;
        }
        ketnoi_csdl kn = new ketnoi_csdl();
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToString("hh.mm.ss dd.MM.yyyy");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ketnoi_csdl kn = new ketnoi_csdl();
            kn.themdulieu(i,textBox3.Text, textBox1.Text);
            i++;
            hienthi_csdl();
        }
        private void hienthi_csdl()
        {
            ketnoi_csdl kn = new ketnoi_csdl();
            kn.hienthi(this.dataGridView1);
        }




    }
}
