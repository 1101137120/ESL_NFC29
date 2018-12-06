using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace EPaperDemo2
{
    class NFCEslReader
    {
        private static byte[] blackRandomN = new byte[16] { 0x36, 0x5A, 0xC5, 0x7A, 0x29, 0xB3, 0x1D, 0x8E, 0x3B, 0x59, 0x97, 0xF1, 0xC2, 0x4E, 0xD4, 0xA3 };
        private static byte[] redRandomN = new byte[16] { 0x5C, 0x99, 0xF5, 0x12, 0xD6, 0x3A, 0x38, 0x5C, 0x49, 0xE4, 0xAA, 0x67, 0x91, 0xBD, 0x83, 0x2F };
        private static int  tag0 = 0;
        private static int tag1 = 0;
        public List<SerialPort> portList = new List<SerialPort>();
        delegate void Display(Byte[] buffer);// UI讀取用
        public class portClass
        {
           public string portName;
           public int BauRate;
        }


        public bool openPort(List<portClass> portDataList)
        {
            bool result = true;
            foreach (SerialPort port in portList)
            {
                port.Close();
            }
            portList.Clear();
            foreach (portClass portClass in portDataList)
            {
                 SerialPort port = new SerialPort();
                    try
                    {
                        port.PortName = portClass.portName;
                        port.BaudRate = portClass.BauRate;            // baud rate = 9600
                        port.Parity = Parity.None;       // Parity = none
                        port.StopBits = StopBits.One;    // stop bits = one
                        port.DataBits = 8;               // data bits = 8
                        port.WriteTimeout = 1;
                        port.ReadTimeout = 1;
                        // 設定 PORT 接收事件
                        port.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);

                        // 打開 PORT
                        if(!port.IsOpen)
                            port.Open();
                        portList.Add(port);
                    }
                    catch (Exception ex)
                    {
                        port.Close();
                        result = false;
                    }
            }
            return result;
        }


        public bool closePort(List<SerialPort> closePortList)
        {
            bool result = true;
            foreach (SerialPort port in closePortList)
            {
                try {
                    if (port.IsOpen)
                        port.Close();
                } catch (Exception ex) {

                        port.Close();
                    result = false;
                }

            }
            return result;
        }


        //接收UART資料
        private void port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // stopwatchtext.Stop();
            //     Console.WriteLine("start" + stopwatchtext.Elapsed.Milliseconds + "ms");
            List<byte> packet = new List<byte>();
            foreach (SerialPort port in portList)
            {
                while (port.BytesToRead != 0)
                {


                    packet.Add((byte)port.ReadByte());
                }
            }

            byte[] bArrary = packet.ToArray();
            if (bArrary.Length > 1)
            {
              /*  Display d = new Display(DisplayTextString);
                try
                {
                    this.Invoke(  d,bArrary );
                }
                catch (Exception ex) { }*/

            }
        }


        public void sss(Bitmap bmp) {

            string bit = "";
            string rbit = "";
            string totaldata = "";
            string totalreddata = "";
            string tatolbit = "";
            string t, r;
            Color color;
            for (int i = bmp.Width - 1; i >= 0; i--)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(i, j);
                    if (color.ToArgb() == Color.Black.ToArgb())
                    {
                        bit = bit + "0";
                    }
                    else
                    {
                        bit = bit + "1";
                    }

                    if (color.ToArgb() == Color.Red.ToArgb())
                    {
                        rbit += "1";
                    }
                    else
                    {
                        rbit += "0";
                    }

                    if (bit.Length == 8)
                    {
                        totaldata = totaldata + Convert.ToInt32(bit, 2).ToString("X2");
                        totalreddata = totalreddata + Convert.ToInt32(rbit, 2).ToString("X2");
                        bit = "";
                        rbit = "";
                    }
                }
            }
            t = totaldata;
            r = totalreddata;

            for (int i = t.Length; i < 5792; i++)//2896
            {
                t = t + "0";
                r = r + "0";
            }
          //  count = 0;
            //total_count = 0;
            t = blackEnCode(t);
            r = redEnCode(r);

           // Write_Check = true;
            string com = "090126" + "f80829c001";
            byte[] bcom = iCheckSum(StringToByteArray(com));

            //SendData(bcom);
        }

        public static byte[] iCheckSum(byte[] data)
        {
            byte[] bytes = new byte[data.Length + 1];
            int intValue = 0;
            for (int i = 0; i < data.Length; i++)
            {
                intValue = intValue + (int)data[i];
                bytes[i] = data[i];
            }
            byte[] intBytes = BitConverter.GetBytes(intValue);
            Array.Reverse(intBytes);
            bytes[data.Length] = (byte)(intBytes[intBytes.Length - 1] ^ (byte)0xff);
            //  Console.WriteLine(ByteArrayToString(bytes) + "");
            return bytes;
        }


        public static string redEnCode(string data)
        {
            byte[] bytes = StringToByteArray(data);
            byte[] bytesend = new byte[0];
            string aaa = "";
            int count16 = 0;
            byte xx = new byte();
            int tol = (tag0 + tag1) % 256;

            if (tol == 0)
                xx = 0x55;
            else
                xx = Convert.ToByte(tol);

            for (int i = 0; i < bytes.Length; i++)
            {

                // byteEncode[i] = (byte)(((bytesend[i]+0x02)%256) ^ (byte)0xff);
                //  int a1 = (bytesend[i] + 0x01) % 256;
                //   int a2 = (byte)a1 ^ (byte)0xff;
                //   string a3 = a2.ToString("X2");
                // Console.WriteLine(a3);
                if (count16 == 16)
                {
                    count16 = 0;
                }
                aaa = aaa + ((byte)(((bytes[i])) ^ (xx + redRandomN[count16]))).ToString("X2");
                count16++;
            }

            return aaa;
        }


        public static string blackEnCode(string data)
        {
            byte[] bytes = StringToByteArray(data);
            byte[] bytesend = new byte[0];
            byte xx = new byte();
            string aaa = "";
            int count16 = 0;

            int tol = (tag0 + tag1) % 256;

            if (tol == 0)
                xx = 0x55;
            else
                xx = Convert.ToByte(tol);

            for (int i = 0; i < bytes.Length; i++)
            {

                // byteEncode[i] = (byte)(((bytesend[i]+0x02)%256) ^ (byte)0xff);
                //  int a1 = (bytesend[i] + 0x02) % 256;
                //   int a2 = (byte)a1 ^ (byte)0xff;
                //   string a3 = a2.ToString("X2");
                // Console.WriteLine(a3);
                if (count16 == 16)
                {
                    count16 = 0;
                }
                aaa = aaa + ((byte)(((bytes[i])) ^ (xx + blackRandomN[count16]))).ToString("X2");
                count16++;
            }

            return aaa;
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }

}
