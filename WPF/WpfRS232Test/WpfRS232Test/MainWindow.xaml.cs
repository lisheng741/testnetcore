using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRS232Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SerialPort Port { get; set; } = new SerialPort();

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Port.ReadTimeout = 8000;//串口读超时8秒  
            Port.WriteTimeout = 8000;//串口写超时8秒，在1ms自动发送数据时拔掉串口，写超时5秒后，会自动停止发送，如果无超时设定，这时程序假死  
            Port.ReadBufferSize = 1024;//数据读缓存  
            Port.WriteBufferSize = 1024;//数据写缓存  
            Port.DataReceived += ReceiveEvent;

            var ports = SerialPort.GetPortNames();

            foreach (var port in ports)
            {
                comboBox.Items.Add(port);
            }
        }

        private void OpenPortClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Port.PortName = comboBox.SelectedValue.ToString() ?? "";
                Port.BaudRate = 9600; //波特率默认设置9600  
                Port.Parity = Parity.None; //校验位默认设置值为0，对应NONE 
                Port.DataBits = 8; //数据位默认设置8位  
                Port.StopBits = StopBits.One; //停止位默认设置1  
                Port.Open();
            }
            catch
            {
            }
        }

        private void ClosePortClicked(object sender, RoutedEventArgs e)
        {
            Port.Close();
        }

        private void SendClicked(object sender, RoutedEventArgs e)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(sendText.Text);

            Port.Write(sendBuffer, 0, sendBuffer.Length);
        }

        private void ReceiveEvent(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] receiveBuffer = new byte[Port.BytesToRead];
                Port.Read(receiveBuffer, 0, receiveBuffer.Length);
                string receiveString = Encoding.ASCII.GetString(receiveBuffer);
                //receiveText.Text += receiveString;
                receiveText.Dispatcher.Invoke(() =>
                {
                    receiveText.Text += receiveString;
                });
            }
            catch
            {

            }
        }
    }
}
