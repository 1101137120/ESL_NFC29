
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZXing;                  // for BarcodeWriter

namespace EPaperDemo2
{
    public partial class Form1 : Form
    {

        static int bmpsizew = 212;
        static int bmpsizeh = 104;

        Bitmap bmp = new Bitmap(bmpsizew, bmpsizeh);
        SmcEink mSmcEink = new SmcEink();
        float DpiX, DpiY;

        public SerialPort port = new SerialPort();
        private List<byte> packet = new List<byte>();
        delegate void Display(Byte[] buffer);// UI讀取用
        Boolean isConnect;
        Boolean Write_Check;
        Boolean Red = false;
        
        string t, r;
        static System.Windows.Forms.Timer WriteTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer UpdateTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer UIDReaderTimer = new System.Windows.Forms.Timer();


        txtData tag1_1 = new txtData(); 

        txtData tag1_2 = new txtData(); 
        txtData tag1_3 = new txtData();  
        txtData tag2_1 = new txtData();  
        txtData  tag2_2 = new txtData();  
        txtData  tag3_1 = new txtData();  
        txtData  tag3_2 = new txtData(); 
        txtData  tag3_3 = new txtData(); 
        txtData  tag4_1 = new txtData();  
        txtData  tag4_2 = new txtData();  
        txtData  tag4_3 = new txtData();  
        txtData  tag4_4 = new txtData();  
        txtData  tag4_5 = new txtData();  
        txtData  tag4_6 = new txtData();  
        txtData  tag4_7 = new txtData();  
        txtData  tag4_8 = new txtData(); 
        txtData  tag4_9 = new txtData();  
        txtData  tag4_10 = new txtData();  
        txtData  tag4_11 = new txtData();  
        txtData  tag4_12 = new txtData();  
        txtData  tag4_13 = new txtData(); 
        txtData  tag4_14 = new txtData(); 
        txtData  tag4_15 = new txtData();  
        txtData  tag5_1 = new txtData(); 
        txtData  tag5_2 = new txtData();  
        txtData  tag6_1 = new txtData();  
        txtData  tag6_2 = new txtData(); 
        txtData  tag6_3 = new txtData();  
        txtData  tag6_4 = new txtData();

        private int  min = 0;
        private int sec = 0;
        private int secm = 0;
        private int updateCount = 0;
        private static byte[] blackRandomN = new byte[16]{ 0x36, 0x5A, 0xC5, 0x7A, 0x29, 0xB3, 0x1D, 0x8E, 0x3B, 0x59, 0x97, 0xF1, 0xC2, 0x4E, 0xD4, 0xA3 };
        private static byte[] redRandomN = new byte[16] { 0x5C, 0x99, 0xF5, 0x12, 0xD6, 0x3A, 0x38, 0x5C, 0x49, 0xE4, 0xAA, 0x67, 0x91, 0xBD, 0x83, 0x2F };
        bool continueWirte29 = false;
        bool ReadIDClick = false;
        Stopwatch stopwatch = new Stopwatch();
        Stopwatch stopwatchtext = new Stopwatch();

        Stopwatch WriteStopwatch = new Stopwatch();


        Panel panel3Demo = new Panel();
        Panel panel4Demo = new Panel();
        Panel panel5Demo = new Panel();
        Panel panel6Demo = new Panel();
        Panel panel7Demo = new Panel();
        Label panel6labelDemo = new Label();
        Label Label7Demo = new Label();
        Label Label6Demo = new Label();
        Label Label62Demo = new Label();
        Label Label63Demo = new Label();
        Label Label64Demo = new Label();
        Label Label5Demo = new Label();
        Label Label4Demo = new Label();
        Label Label3Demo = new Label();
        Label Label18Demo = new Label();
        Label Label19Demo = new Label();
        Label Label17Demo = new Label();
        Label Label2Demo = new Label();
        Label Label1Demo = new Label();
        Label label5Demo = new Label();
        Label label13Demo = new Label();
        PictureBox pictureBoxa = new PictureBox();
        Button button2 = new Button();
        // SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        Label Label8Demo = new Label();
        Label Label16Demo = new Label();
        public Form1()
        {


            tag1_1.tag = "EBUF7E8.1";
            tag1_2.tag = "EBUF7E8.1";
            tag1_3.tag = "C1TMJV85156A-M004";
            tag2_1.tag = "Info:";
            tag2_2.tag = "3";
            tag3_1.tag = "PSM";
            tag3_2.tag = "SECURITY";
            tag3_3.tag = "SSSHR";
            tag4_1.tag = "0.028,L,";
            tag4_2.tag = "193,";
            tag4_3.tag = "AJBX3-1,";
            tag4_4.tag = "BK#331";
            tag4_5.tag = "Tone=";
            tag4_6.tag = "C,";
            tag4_7.tag = "CCD=";
            tag4_8.tag = "0.72, ";
            tag4_9.tag = "PD=";
            tag4_10.tag = "23.08%";
            tag4_11.tag = "Pel=";
            tag4_12.tag = "36,";
            tag4_13.tag = "DD-BO, ";
            tag4_14.tag = "CLIP=";
            tag4_15.tag = "10/13 23:00";
            tag5_1.tag = "AAEEMD";
            tag5_2.tag = "(UL_MOSI_40_C)";
            tag6_1.tag = "QA1";
            tag6_2.tag = "ASI";
            tag6_3.tag = "IPRO";
            tag6_4.tag = "MPM";
            InitializeComponent();
            label2.Visible = false;
            label4.Visible = false;
            HINKViewerButton.Visible = false;
            /*  BarcodeWriter qr = new BarcodeWriter();
              qr.Format = BarcodeFormat.QR_CODE;
              qr.Options.Width = pictureBox2.Width;
              qr.Options.Height = pictureBox2.Height;
              qr.Options.Margin = 0;
              pictureBox2.Image = qr.Write("www.smartchip.com.tw");*/

            // 找出字體大小,並算出比例
            Graphics graphics = this.CreateGraphics();
            DpiX = graphics.DpiX;
            DpiY = graphics.DpiY;
            float floatDpi =  96f/DpiX;
      //      WriteTimer.Tick += new EventHandler(WriteContinueTimer);
      //      WriteTimer.Interval = 15 *1000;
            UpdateTimer.Tick += new EventHandler(UpdateContinueTimer);
            UpdateTimer.Interval = 15 * 1000;

            UIDReaderTimer.Tick += new EventHandler(UUIDTimer);
            UIDReaderTimer.Interval = 1*500;
            



          //  panel3Demo.BorderStyle = BorderStyle.FixedSingle;
            panel3Demo.Location = new Point(170, 1);
            panel3Demo.Size = new Size(103, 48);
            panel3Demo.TabIndex = 120;
           // panel4Demo.BorderStyle = BorderStyle.FixedSingle;
            panel4Demo.Location = new Point(128, 50);
            panel4Demo.AutoSize = true;
            panel4Demo.Size = new Size(150, 47);
            panel4Demo.TabIndex = 120;
          //  panel5Demo.BorderStyle = BorderStyle.FixedSingle;
            panel5Demo.Location = new Point(2, 92);
            panel5Demo.Size = new Size(98, 30);
            panel5Demo.TabIndex = 120;

        //    panel6Demo.BorderStyle = BorderStyle.FixedSingle;
            panel6Demo.Location = new Point(246, 98);
            panel6Demo.Size = new Size(72, 23);
            panel6Demo.TabIndex = 120;

            panel7Demo.Location = new Point(105, 100);
            panel7Demo.Size = new Size(130, 30);
            panel7Demo.TabIndex = 120;


            panel29.Controls.Add(panel3Demo);
            panel29.Controls.Add(panel4Demo);
            panel29.Controls.Add(panel5Demo);
            panel29.Controls.Add(panel6Demo);
            panel29.Controls.Add(panel7Demo);

            //  panel29.Controls.Add(Label7Demo);

            panel29.Controls.Add(Label5Demo);
            panel29.Controls.Add(Label4Demo);

            // panel1.Controls.Add(Label3Demo);
            // panel1.Controls.Add(Label2);
            //  panel1.Controls.Add(Label8Demo);
            // panel1.Controls.Add(Label16Demo);
            // panel1.Controls.Add(Label17Demo);
            //panel1.Controls.Add(Label1);
            // panel1.Controls.Add(label5Demo);
            //panel1.Controls.Add(label3Demo);
            panel29.Controls.Add(pictureBoxa);

            panel3Demo.Controls.Add(Label2Demo);
            panel3Demo.Controls.Add(Label16Demo);
            panel3Demo.Controls.Add(Label17Demo);
            panel4Demo.Controls.Add(Label1Demo);
            panel4Demo.Controls.Add(Label3Demo);
            panel4Demo.Controls.Add(Label19Demo);
            panel4Demo.Controls.Add(Label18Demo);
            panel4Demo.Controls.Add(Label8Demo);
            panel5Demo.Controls.Add(label5Demo);
            panel5Demo.Controls.Add(Label7Demo);
            panel6Demo.Controls.Add(label13Demo);
            panel6Demo.Controls.Add(panel6labelDemo);
            panel7Demo.Controls.Add(Label6Demo);
            panel7Demo.Controls.Add(Label62Demo);
            panel7Demo.Controls.Add(Label63Demo);
            panel7Demo.Controls.Add(Label64Demo);



            Label7Demo.BorderStyle = BorderStyle.None;
            Label7Demo.Font = new Font("Calibri", 9.5f* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7Demo.Location = new Point(2, 15);
            Label7Demo.ForeColor = tag5_2.tagColor;
            Label7Demo.Name = "Label7Demo";
            // Label7Demo.Size = new Size(73, 15);
            Label7Demo.TabIndex = 150;
            Label7Demo.Text = tag5_2.tag;
            Label7Demo.AutoSize = true;


            Label6Demo.BorderStyle = BorderStyle.None;
            Label6Demo.Font = new Font("Calibri", 9.2f* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6Demo.Location = new Point(0, 5);
            Label6Demo.Name = "Label6Demo";
            Label6Demo.ForeColor = tag6_1.tagColor;

           // Label6Demo.Size = new Size(20, 30);
            Label6Demo.TabIndex = 29;
            //      Label6Demo.TextAlign = HorizontalAlignment.Center;
            Label6Demo.AutoSize = true;
            Label6Demo.Text = tag6_1.tag;
            //Label6Demo.BackColor = Color.Blue;

            Label62Demo.BorderStyle = BorderStyle.None;
            Label62Demo.Font = new Font("Calibri", 9.2f * floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label62Demo.Location = new Point(30, 5);
            Label62Demo.Name = "Label62Demo";
            Label62Demo.ForeColor = tag6_2.tagColor;
            Label62Demo.Size = new Size(30, 30);
            Label62Demo.TabIndex = 29;
            Label62Demo.AutoSize = true;
           // Label62Demo.BackColor = Color.Blue;
            //      Label62Demo.TextAlign = HorizontalAlignment.Center;
            Label62Demo.Text = tag6_2.tag;
            Label62Demo.Tag = floatDpi;

            Label63Demo.BorderStyle = BorderStyle.None;
            Label63Demo.Font = new Font("Calibri", 9.2f * floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label63Demo.Location = new Point(60, 5);
            Label63Demo.Name = "Label63Demo";
            Label63Demo.ForeColor = tag6_3.tagColor;
            //    Label63Demo.Size = new Size(142, 50);
            Label63Demo.TabIndex = 29;
            Label63Demo.AutoSize = true;
          //  Label63Demo.BackColor = Color.Blue;
            //      Label63Demo.TextAlign = HorizontalAlignment.Center;
            Label63Demo.Text = tag6_3.tag;
            Label63Demo.Tag = floatDpi;

            Label64Demo.BorderStyle = BorderStyle.None;
            Label64Demo.Font = new Font("Calibri", 9.2f * floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label64Demo.Location = new Point(90, 5);
            Label64Demo.Name = "Label64Demo";
            Label64Demo.ForeColor = tag6_4.tagColor;
            //    Label64Demo.Size = new Size(142, 50);
            Label64Demo.TabIndex = 29;
            Label64Demo.AutoSize = true;
            //      Label64Demo.TextAlign = HorizontalAlignment.Center;
            Label64Demo.Text = tag6_4.tag;
           // Label64Demo.BackColor = Color.Blue;
            Label64Demo.Tag = floatDpi;


            //  Label5Demo.BorderStyle = BorderStyle.FixedSingle;
            Label5Demo.Font = new Font("Calibri", 10F* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5Demo.Location = new Point(0, 75);
            Label5Demo.Name = "Label5Demo";
           // Label5Demo.Size = new Size(144, 10);
            Label5Demo.TabIndex = 28;
            Label5Demo.ForeColor = tag1_3.tagColor;
            Label5Demo.AutoSize = true;
            //  Label5Demo.TextAlign = HorizontalAlignment.Center;
            Label5Demo.Text = tag1_3.tag;


          //  Label4Demo.BorderStyle = BorderStyle.FixedSingle;
            Label4Demo.Font = new Font("Calibri", 20.5F* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4Demo.Location = new Point(0, 38);
            Label4Demo.Name = "Label4Demo";
            Label4Demo.Size = new Size(130, 10);
            Label4Demo.TabIndex = 27;
            Label4Demo.ForeColor = tag1_2.tagColor;
            Label4Demo.Text = tag1_2.tag;
            Label4Demo.AutoSize = true;
            // Label4Demo.TextAlign = HorizontalAlignment.Center;
            /*  Label3Demo.BorderStyle = BorderStyle.None;
              Label3Demo.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
              Label3Demo.ForeColor = Color.Red;
              Label3Demo.Location = new Point(4, 24);
              Label3Demo.Name = "Label3Demo";
              Label3Demo.Size = new Size(139, 15);
              Label3Demo.TabIndex = 26;
              Label3Demo.Text = "瑞新電子股份有限公司";*/
            Label2Demo.BorderStyle = BorderStyle.None;
            Label2Demo.Font = new Font("Calibri", 12* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2Demo.ForeColor = tag3_1.tagColor;
            Label2Demo.Name = "Label2Demo";
           // Label2Demo.Size = new Size(68, 20);
            Label2Demo.TabIndex = 25;
            Label2Demo.Text = tag3_1.tag;
            Label2Demo.AutoSize = true;
            int c = panel3Demo.Width / 2 - Label2Demo.Width / 2;
            Label2Demo.Location = new Point(c, -3);
            //  Label2Demo.TextAlign = HorizontalAlignment.Center;

            label5Demo.AutoSize = true;
            label5Demo.Font = new Font("Calibri", 9.8F* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5Demo.Location = new Point(15, 0);
            label5Demo.ForeColor = tag5_1.tagColor;
            label5Demo.Name = "label5Demo";
        //    label5Demo.Size = new Size(25, 10);
            label5Demo.TabIndex = 21;
            label5Demo.Text = tag5_1.tag;



            label13Demo.AutoSize = true;
            label13Demo.Font = new Font("Calibri", 9f* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13Demo.Location = new Point(3, 5);
            label13Demo.Name = "Info";
            //  label3Demo.Size = new Size(10, 14);
            label13Demo.ForeColor = tag2_1.tagColor;
            label13Demo.TabIndex = 19;
            label13Demo.Text = tag2_1.tag;
            panel6labelDemo.AutoSize = true;
            string str = System.AppDomain.CurrentDomain.BaseDirectory;
            //   Console.WriteLine("str" + str);
            //  string filename = str + "circle.jpg";
            //  panel6labelDemo.Image = Image.FromFile(@filename);
            //  panel6labelDemo.BackColor = Color.Red;
            panel6labelDemo.Font = new Font("Calibri", 12* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            panel6labelDemo.Location = new Point(27, 3);
            panel6labelDemo.Name = "panel6labelDemo";
            panel6labelDemo.Size = new Size(19, 19);
            panel6labelDemo.TabIndex = 100;
            panel6labelDemo.Text = tag2_2.tag;
            panel6labelDemo.ForeColor = Color.White;
            panel6labelDemo.TextAlign = ContentAlignment.MiddleCenter;
          //  panel6labelDemo.Paint += new PaintEventHandler(panel6labelDemo_Paint);





            Label3Demo.BorderStyle = BorderStyle.None;
            Label3Demo.Font = new Font("Calibri", 9F* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3Demo.Location = new Point(0, 3);
            Label3Demo.Name = "Label3Demo";
            //  Label3Demo.Size = new Size(84, 15);
            Label3Demo.ForeColor = tag4_1.tagColor;
            Label3Demo.TabIndex = 26;
            Label3Demo.Text += (tag4_1.tag + tag4_2.tag + tag4_3.tag);
            Label3Demo.AutoSize = true;


            Label18Demo.BorderStyle = BorderStyle.None;
            Label18Demo.Font = new Font("Calibri", 9F* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label18Demo.ForeColor = tag4_4.tagColor;
            Label18Demo.Location = new Point(104, 3);
            Label18Demo.Name = "Label18Demo";
         //   Label18Demo.Size = new Size(30, 15);
            Label18Demo.TabIndex = 229;
            Label18Demo.Text += (tag4_4.tag);
            Label18Demo.AutoSize = true;
            // Label18Demo.TextAlign = HorizontalAlignment.Center;

            Label1Demo.BorderStyle = BorderStyle.None;
            Label1Demo.Font = new Font("Calibri", 9F* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1Demo.Location = new Point(0, 15);
            Label1Demo.Name = "Label1Demo";
            Label1Demo.ForeColor = tag4_5.tagColor;
            // Label1Demo.Size = new Size(139, 15);
            Label1Demo.TabIndex = 24;
            Label1Demo.Text += (tag4_5.tag + tag4_6.tag + tag4_7.tag + tag4_8.tag + tag4_9.tag + tag4_10.tag);
            Label1Demo.AutoSize = true;
            //Label1Demo.TextAlign = HorizontalAlignment.Center;


            Label8Demo.BorderStyle = BorderStyle.None;
            Label8Demo.Location = new Point(0, 28);
            Label8Demo.Font = new Font("Calibri", 9F* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8Demo.Name = "Label8Demo";
            Label8Demo.ForeColor = tag4_11.tagColor;
            //   Label8Demo.Size = new Size(87, 15);
            Label8Demo.TabIndex = 8;
            Label8Demo.Text += (tag4_11.tag + tag4_12.tag + tag4_13.tag + tag4_14.tag);
            Label8Demo.AutoSize = true;
            // Label8Demo.TextAlign = HorizontalAlignment.Center;

            Label19Demo.BorderStyle = BorderStyle.None;
            Label19Demo.Font = new Font("Calibri", 9F* floatDpi, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label19Demo.Location = new Point(105, 28);
            Label19Demo.Name = "Label19Demo";
            Label19Demo.ForeColor = tag4_15.tagColor;
            //  Label19Demo.Size = new Size(58, 15);
            Label19Demo.TabIndex = 229;
            Label19Demo.Text += (tag4_15.tag);
            Label19Demo.AutoSize = true;
            //    Label19Demo.TextAlign = HorizontalAlignment.Center;

            Label16Demo.BorderStyle = BorderStyle.None;
            
            Label16Demo.Font = new Font("Calibri", 12* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label16Demo.ForeColor = Color.Red;
            Label16Demo.Name = "Label16Demo";
            Label16Demo.ForeColor = tag3_2.tagColor;
            //    Label16Demo.Size = new Size(68, 15);
            Label16Demo.TabIndex = 55;
            Label16Demo.Text += (tag3_2.tag);
            Label16Demo.AutoSize = true;
            int b = panel3Demo.Width / 2 - Label16Demo.Width / 2;
            Label16Demo.Location = new Point(b, 12);
            //  Label16Demo.TextAlign = HorizontalAlignment.Center;



            Label17Demo.BorderStyle = BorderStyle.None;

            Label17Demo.Font = new Font("Calibri", 12* floatDpi, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17Demo.ForeColor = Color.Red;
            Label17Demo.Name = "Label17Demo";
            //  Label17Demo.Size = new Size(112, 15);
            Label17Demo.ForeColor = tag3_3.tagColor;
            Label17Demo.TabIndex = 56;
            Label17Demo.Text += (tag3_3.tag);
            Label17Demo.AutoSize = true;
            int a = panel3Demo.Width / 2 - Label17Demo.Width / 2;
            Label17Demo.Location = new Point(a, 28);
            pictureBoxa.Location = new Point(25, 10);
            pictureBoxa.Name = "pictureBoxa";
            pictureBoxa.Size = new Size(146, 30);

           // Bitmap bar = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            BarcodeWriter barcode_w = new BarcodeWriter();
            barcode_w.Format = BarcodeFormat.CODE_39;
            barcode_w.Options.Width = pictureBoxa.Width;
            barcode_w.Options.Height = pictureBoxa.Height;
            barcode_w.Options.PureBarcode = true;
          //  bar = barcode_w.Write(tag1_1);
            //pictureBoxa.Image = bar;

            foreach (string com in SerialPort.GetPortNames())//取得所有可用的連接埠
            {
                cbbComPort.Items.Add(com);
            }
            if (cbbComPort.Items.Count != 0)
            {
                cbbComPort.SelectedIndex = 0;
            }
            isConnect = false;
            ConnectStatus.ForeColor = Color.Green;



        }



        private void button1_Click(object sender, EventArgs e)
        {
            setImageData();
        }
        private string VerticalText(string text)
        {
            string str = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                str = string.Concat(str, string.Format("{0}\r\n", text.Substring(i, 1)));

            }
            return str;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          /*  ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
             Color.DimGray, 1, ButtonBorderStyle.Solid, //左边
　　　　　Color.DimGray, 1, ButtonBorderStyle.Solid, //上边
　　　　　Color.DimGray, 1, ButtonBorderStyle.Solid, //右边
　　　　　Color.DimGray, 1, ButtonBorderStyle.Solid);//底边*/
        }
        string tatolbit = "";


        public static Bitmap ConvertBlackAndWhite(Bitmap Image)
        {
            using (Graphics gr = Graphics.FromImage(Image)) // SourceImage is a Bitmap object
            {
                var gray_matrix = new float[][] {
                new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                new float[] { 0,      0,      0,      1, 0 },
                new float[] { 0,      0,      0,      0, 1 }
                };

                var ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(gray_matrix));
                ia.SetThreshold(0.8f); // Change this threshold as needed
                var rc = new Rectangle(0, 0, Image.Width, Image.Height);
                gr.DrawImage(Image, rc, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ia);
            }
            return Image;
        }


        private void setImageData()
        {
        /*    BarcodeWriter qr = new BarcodeWriter();
            qr.Format = BarcodeFormat.QR_CODE;
            qr.Options.Width = pictureBox2.Width;
            qr.Options.Height = pictureBox2.Height;
            qr.Options.Margin = 0;

            bmpsizew = 212;
            bmpsizeh = 104;
            bmp = new Bitmap(bmpsizew, bmpsizeh);


            if (Label8.Text.Length != 0)
            {
                pictureBox2.Image = qr.Write(Label8.Text);
            }

            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
                graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 212, 22);
            }
            foreach (Control ctl in panel1.Controls)
            {

                if (ctl is CheckBox) // 判斷為 CheckBox 時，將 CheckBox.Checked 設為 True。
                    ((CheckBox)ctl).Checked = true;
                else if (ctl is Label) // 判斷為 Label 時，將 CheckBox.Text 設為 "Hello world!"。
                {
                    //((Label)ctl).Text = "1";
                    int x = ((Label)ctl).Location.X;
                    int y = ((Label)ctl).Location.Y;
                    int w = ((Label)ctl).Width;
                    int h = ((Label)ctl).Height;
                  //  Console.WriteLine(x + "," + y + "  w:" + w + ", h:" + h);


                    bmp = mSmcEink.ConvertTextToImage(bmp, ((Label)ctl), Color.White, x, y);
                    //}

                }

                else if (ctl is PictureBox)
                {
                    int x = ((PictureBox)ctl).Location.X;
                    int y = ((PictureBox)ctl).Location.Y;
                    int w = ((PictureBox)ctl).Width;
                    int h = ((PictureBox)ctl).Height;

                //    Console.WriteLine("PictureBox " + x + "," + y + "  w:" + w + ", h:" + h);
                    bmp = mSmcEink.ConvertImageToImage(bmp, ((PictureBox)ctl).Image, x, y, w, h);
                }
                else if (ctl is Label)
                {
                    int x = ((Label)ctl).Location.X;
                    int y = ((Label)ctl).Location.Y;
                    int w = ((Label)ctl).Width;
                    int h = ((Label)ctl).Height;

                 //   Console.WriteLine("Label " + x + "," + y + "  w:" + w + ", h:" + h);
                    bmp = mSmcEink.ConvertTextToImage(bmp, ((Label)ctl), Color.Black, x, y);
                }

            }
            OutPictureBox.Image = bmp;
            */
        }


    

        private void button2_Click(object sender, EventArgs e)
        {
            Red = true;
            count = 0;
            total_count = 0;
            Write_Check = false;
            setImageData();
            if (bmp != null)
            {

                string bit = "";
                string rbit = "";
                string totaldata = "";
                string totalreddata = "";
                tatolbit = "";
                stopwatch.Reset();
                stopwatch.Start();
                Color color;
                for (int i = bmp.Width - 1; i >= 0; i--)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        color = bmp.GetPixel(i, j);

                        // int ttt = (color.R + color.B + color.G) / 3;

                        // int aaa = 150;

                        //if (color.R<aaa && color.B<aaa && color.G < aaa)
                        /* if(ttt<150)
                         {
                             bit = bit + "0";
                         }else
                         {
                             bit = bit + "1";
                         }*/

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
                            //tatolbit = tatolbit + bit;
                            totaldata = totaldata + Convert.ToInt32(bit, 2).ToString("X2");
                            totalreddata = totalreddata + Convert.ToInt32(rbit, 2).ToString("X2");
                            bit = "";
                            rbit = "";
                        }
                    }
                }
                t = totaldata;
                r = totalreddata;

                for (int i = t.Length; i < 5536; i++)//2768
                {
                    t = t + "0";
                    r = r + "0";
                }
                count = 0;
                total_count = 0;
                Write_Check = true;

                t = blackEnCode(t);
                r = redEnCode(r);
                string com = "090126" + "f80822c001";
                byte[] bcom = iCheckSum(StringToByteArray(com));
                SendData(bcom);
            }
        }

        private void Connect_COM_Button_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                port.Close();
                ConnectStatus.Text = "Connect Fail !";
                ConnectStatus.ForeColor = Color.Green;
                isConnect = false;
            }

            if (!port.IsOpen)
            {
                try
                {
                    int com = Convert.ToInt32(cbCOMPort.Text);
                    port.PortName = cbbComPort.Text;
                    this.port.BaudRate = com;            // baud rate = 9600
                    this.port.Parity = Parity.None;       // Parity = none
                    this.port.StopBits = StopBits.One;    // stop bits = one
                    this.port.DataBits = 8;               // data bits = 8
                    this.port.WriteTimeout = 1;
                    this.port.ReadTimeout = 1;
                    // 設定 PORT 接收事件
                    port.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);

                    // 打開 PORT
                    port.Open();
                }
                catch (Exception ex)
                {
                    port.Close();
                    MessageBox.Show("串口出問題請重新啟動程式");
                }
            }
            if (port.IsOpen == true)
            {
                ConnectStatus.Text = "Connect OK !";
                ConnectStatus.ForeColor = Color.Green;
                isConnect = true;
            }
            else
            {
                ConnectStatus.Text = "Connect Fail !";
                ConnectStatus.ForeColor = Color.Green;
                isConnect = false;
            }
        }

        private void UpCOM_Click(object sender, EventArgs e)
        {
            cbbComPort.Items.Clear();
            foreach (string com in SerialPort.GetPortNames())//取得所有可用的連接埠
            {
                cbbComPort.Items.Add(com);
            }
            if (cbbComPort.Items.Count != 0)
            {
                cbbComPort.SelectedIndex = 0;
            }
            isConnect = false;
            ConnectStatus.ForeColor = Color.Green;
        }

        private void ReadUID_Click(object sender, EventArgs e)
        {
         
            if (!ReadIDClick)
            {
                UIDReaderTimer.Start();
                ReadUID.Text = "停止讀取";
                ReadUID.ForeColor = Color.Red;
                ReadIDClick = true;
            }
            else
            {
                UIDReaderTimer.Stop();
                ReadUID.Text = "讀取Tag";
                ReadUID.ForeColor = Color.Black;
                ReadIDClick = false;

            }


        }



        private void TimeUpdate(int secT,int secmT)
        {

            int secmA = (secmT + secm) / 1000;
            secm = (secmT + secm) % 1000;
            int secA = (sec + secT + secmA) / 60;
            sec = (sec+secT + secmA) % 60;
            min  = min + secA;
        }

        private void WritePage_Click(object sender, EventArgs e)
        {
          /*  string com = "090126" + "04" + ultralightLabel.Text;
            byte[] bcom = iCheckSum(StringToByteArray(com));
            SendData(bcom);
            Write_Check = false;*/
        }

        private void WritePageLong_Click(object sender, EventArgs e)
        {
           /* string com = "150127" + "04" + A0Data.Text;
            byte[] bcom = iCheckSum(StringToByteArray(com));
            SendData(bcom);
            Write_Check = false;*/
        }
        private void SendData(byte[] data)
        {
            if (Write_Check|| continueWirte29)
            {

            }
            else
            {
                texMessageBox.Text = "發送:\r\n" + ByteArrayToString(data) + "\r\n";
            }
            if (isConnect)
            {
                port.Write(data, 0, data.Count());
            }
                
        }


        //接收UART資料
        private void port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           // stopwatchtext.Stop();
       //     Console.WriteLine("start" + stopwatchtext.Elapsed.Milliseconds + "ms");
            while (port.BytesToRead != 0)
            {


                packet.Add((byte)port.ReadByte());
            }
            byte[] bArrary = packet.ToArray();
            if (bArrary.Length > 1)
            {
                Display d = new Display(DisplayTextString);
                try
                {
                    this.Invoke(d, new Object[] { bArrary });
                }
                catch (Exception ex) { }

            }
        }
        int count = 0;
        int total_count = 0;
        int substringcount = 32;
        static byte tag0 = 0x00;
        static byte tag1 = 0x00;
        //取得資料並顯示
        private void DisplayTextString(byte[] RX)
        {
            packet.Clear();
            if (Write_Check)
            {
                //05012700d2
                //05012600d3

                if (RX[0] == (byte)0x05 && RX[2] == (byte)0x26 && RX[3] == (byte)0x00)
                {
                    texMessageBox.Text = "設定成功" + "\r\n";
                    Thread.Sleep(2000);
                    string com = "150127" + "00" + t.Substring(0, substringcount);
                    byte[] bcom = iCheckSum(StringToByteArray(com));
                    SendData(bcom);
                        Thread.Sleep(37);
                    count++;
               /*     for (int i = 0; i < 592; i++)
                    {
                        dddd();
                    }
                    */
                }
                else if (RX[0] == (byte)0x05 && RX[2] == (byte)0x26)
                {
                    if(continueWirte29)
                    UIDReaderTimer.Start();
                    //error
                    texMessageBox.Text =  "收: " + ByteArrayToString(RX) + "\r\n";
                    texMessageBox.Text = texMessageBox.Text + "資料錯誤" + "\r\n";
                }
                if (RX[0] == (byte)0x05 && RX[2] == (byte)0x27 && RX[3] == (byte)0x00)
                {
           
                   if (count < (t.Length/32) )
                    {
                     //   texMessageBox.Text = "黑 寫入成功:" + count + "\r\n";
                        string com = "150127" + "00" + t.Substring(count * substringcount, substringcount);
                        byte[] bcom = iCheckSum(StringToByteArray(com));
                        SendData(bcom);
                        count ++;
                    }
                    else if (count == (t.Length / 32) )
                    {
                        total_count = 0;
                        count ++;
                        if(Red == false)
                        {
                            string com = "150127" + "ff" + "ffffffffffffffffffffffffffffffff";
                            byte[] bcom = iCheckSum(StringToByteArray(com));
                            SendData(bcom);
                           // texMessageBox.Text = texMessageBox.Text + "傳送結束開始更新電子紙" + "\r\n";
                        }
                    }
                    if (count > (t.Length / 32) && total_count == 0 && Red == true)
                    {
                      //  texMessageBox.Text = "紅 寫入成功:" + total_count  + "\r\n";
                        string com = "150127" + "40" + r.Substring(total_count * substringcount , substringcount);
                        byte[] bcom = iCheckSum(StringToByteArray(com));
                        SendData(bcom);
                        count ++;
                        total_count ++;
                    }
                    else if (count > (t.Length / 32) && total_count < ((r.Length / 32) - 1) && Red == true)
                    {

                       
                     ///   texMessageBox.Text = "紅 寫入成功:" + total_count  + "\r\n";
                        string com = "150127" + "80" + r.Substring(total_count * substringcount , substringcount);
                        byte[] bcom = iCheckSum(StringToByteArray(com));
                  //      stopwatchtext.Reset();
                    //    stopwatchtext.Start();
                        SendData(bcom);
                        count++;
                        total_count++;
                    }
                    else if (count > (t.Length / 32) && total_count == ((r.Length / 32) - 1) && Red == true)
                    {
                      //  texMessageBox.Text = "紅 寫入成功:" + total_count  + "\r\n";
                        string com = "150127" + "ff" + r.Substring(total_count * substringcount , substringcount);
                        byte[] bcom = iCheckSum(StringToByteArray(com));
                        SendData(bcom);
                        count++;
                        total_count++;
                    }
                    else if (count > (t.Length / 32) && total_count == (r.Length / 32) && Red == true)
                    {

                      //  texMessageBox.Text = "紅 寫入成功 :" + total_count  + "\r\n";

                     //   texMessageBox.Text = texMessageBox.Text + "傳送秒數:" + stopwatchtext.Elapsed.Milliseconds + "ms" + "\r\n";
                        string com = "150127" + "ff" + "ffffffffffffffffffffffffffffffff";
                        byte[] bcom = iCheckSum(StringToByteArray(com));
                        SendData(bcom);
                        count++;
                        total_count++;


                       
                    }
                    else if (count > (t.Length / 32) && total_count > (r.Length / 32) && Red == true)
                    {

                        stopwatch.Stop();

                        updateCount++;
                        TimeUpdate(stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
                        //   texMessageBox.Text = texMessageBox.Text + "傳送結束開始更新電子紙" + "\r\n";
                        texMessageBox.Text = texMessageBox.Text + "傳送秒數:" + min + "m" + sec + "s " + secm + "ms" + "\r\n";
                        if(continueWirte29)
                            UpdateTimer.Start();

                        count = 0;
                        total_count = 0;
                        Write_Check = false;
                    }
                    
                }
                else if (RX[0] == (byte)0x05 && RX[2] == (byte)0x27)
                {
                    //error

                    if(continueWirte29)
                    UIDReaderTimer.Start() ;

                    texMessageBox.Text =  "收: " + ByteArrayToString(RX) + "\r\n";
                    texMessageBox.Text = texMessageBox.Text + "資料錯誤" + "\r\n";
                }

            }
            else
            {

                if (RX[2] == (byte)0x16 && RX[3] == (byte)0x01)
                {
                    tag0 = RX[4];
                    tag1 = RX[5];
                    texMessageBox.Text = texMessageBox.Text + "收: " + ByteArrayToString(RX) + "\r\n";

                    if (continueWirte29)
                    {
                      //  WriteTimer.Start();
                        UIDReaderTimer.Stop();
                        Red = true;
                        count = 0;
                        total_count = 0;
                        Write_Check = false;


                        Bitmap bmp = setESLimageDemo_29(panel29, tag1_1.tag);
                        if (bmp != null)
                        {
                            pictureBox_29.Image = bmp;
                            string bit = "";
                            string rbit = "";
                            string totaldata = "";
                            string totalreddata = "";
                            tatolbit = "";
                            stopwatch.Reset();
                            stopwatch.Start();
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
                            count = 0;
                            total_count = 0;
                            t = blackEnCode(t);
                            r = redEnCode(r);

                            Write_Check = true;
                            string com = "090126" + "f80829c001";
                            byte[] bcom = iCheckSum(StringToByteArray(com));

                            SendData(bcom);
                        }
                    }
                }
               
            }


        }

        public class txtData {
            public string tag;
            public Color tagColor = Color.Black;
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


        public static string blackEnCode(string data)
        {
            byte[] bytes = StringToByteArray(data);
            byte[] bytesend = new byte[0];
            byte xx = new byte();
            string aaa = "";
            int count16 = 0;

            int tol =  (tag0 + tag1) % 256;

            if (tol == 0)
                xx = 0x55;
            else
                xx = Convert.ToByte(tol);

           /* for (int i = 0; i < bytes.Length; i++)
            {
                if (i!=0&&i % 2 == 1)
                {
                    int tol = (bytes[i - 1] + bytes[i])%256;
                    if (tol ==0)
                    {
                        tag0
                        //Console.WriteLine("bytesend" + 0x55);
                        bytesend = addByteToArray(bytesend, 0x55);
                    }
                    else
                    {
                        //Console.WriteLine("bytesend" + Convert.ToByte(tol));
                        Convert.ToByte(tol);
                        bytesend = addByteToArray(bytesend, Convert.ToByte(tol));
                    }

                }
            }*/
            for (int i = 0; i < bytes.Length; i++)
            {

                // byteEncode[i] = (byte)(((bytesend[i]+0x02)%256) ^ (byte)0xff);
                //  int a1 = (bytesend[i] + 0x02) % 256;
                //   int a2 = (byte)a1 ^ (byte)0xff;
                //   string a3 = a2.ToString("X2");
                // Console.WriteLine(a3);
                if (count16==16)
                {
                    count16 = 0;
                }
                aaa = aaa + ((byte)(((bytes[i])) ^( xx+ blackRandomN[count16]))).ToString("X2");
                count16++;
            }
                
            return aaa;
        }


        public static byte[] addByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length+1];
            bArray.CopyTo(newArray, 0);
            newArray[bArray.Length] = newByte;
            return newArray;
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

            /*     for (int i = 0; i < bytes.Length; i++)
                 {
                     if (i != 0 && i % 2 == 1)
                     {
                         int tol = (bytes[i - 1] + bytes[i]) % 256;
                         if (tol == 0)
                         {

                             //Console.WriteLine("bytesend" + 0x55);
                             bytesend = addByteToArray(bytesend, 0x55);
                         }
                         else
                         {
                             //Console.WriteLine("bytesend" + Convert.ToByte(tol));
                             Convert.ToByte(tol);
                             bytesend = addByteToArray(bytesend, Convert.ToByte(tol));
                         }

                     }
                 }*/
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

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        //--------------------------合力泰---------------------------------------------
        private void HINKViewerButton_Click(object sender, EventArgs e)
        {
            Bitmap aa = setESLimageDemo_29(panel29,tag1_1.tag);
            pictureBox_29.Image = aa;
        }

        private void HINKWriteButton_Click(object sender, EventArgs e)
        {
            Red = true;
            count = 0;
            total_count = 0;
            Write_Check = false;

            if (ReadIDClick)
            {
                UIDReaderTimer.Stop();
                ReadUID.Text = "讀取Tag";
                ReadUID.ForeColor = Color.Black;
                ReadIDClick = false;
            }

           Bitmap bmp =  setESLimageDemo_29(panel29, tag1_1.tag);

            if (bmp != null)
            {
                pictureBox_29.Image = bmp;
                string bit = "";
                string rbit = "";
                string totaldata = "";
                string totalreddata = "";
                tatolbit = "";
                stopwatch.Reset();
                stopwatch.Start();
                Color color;
                for (int i = bmp.Width - 1; i >= 0; i--)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        color = bmp.GetPixel(i, j);
                        if (color.ToArgb() == Color.Black.ToArgb()){
                            bit = bit + "0";
                        }else {
                            bit = bit + "1";
                        }

                        if (color.ToArgb() == Color.Red.ToArgb()) {
                            rbit += "1";
                        } else{
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
                count = 0;
                total_count = 0;
                t = blackEnCode(t);
                r = redEnCode(r);
                
                Write_Check = true;
                string com = "090126" + "f80829c001";
                byte[] bcom = iCheckSum(StringToByteArray(com));

                SendData(bcom);
            }
        }

        private void setImageDataHINK()
        {
            BarcodeWriter qr = new BarcodeWriter();
            qr.Format = BarcodeFormat.QR_CODE;
         //   qr.Options.Width = pictureBox2.Width;
          //  qr.Options.Height = pictureBox2.Height;
            qr.Options.Margin = 0;

            bmpsizew = 296;
            bmpsizeh = 128;
            bmp = new Bitmap(bmpsizew, bmpsizeh);

            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            }
            foreach (Control ctl in panel29.Controls)
            {

                if (ctl is CheckBox) // 判斷為 CheckBox 時，將 CheckBox.Checked 設為 True。
                    ((CheckBox)ctl).Checked = true;
                else if (ctl is Label) // 判斷為 Label 時，將 CheckBox.Text 設為 "Hello world!"。
                {
                    //((Label)ctl).Text = "1";
                    int x = ((Label)ctl).Location.X;
                    int y = ((Label)ctl).Location.Y;
                    int w = ((Label)ctl).Width;
                    int h = ((Label)ctl).Height;
                 //   Console.WriteLine(x + "," + y + "  w:" + w + ", h:" + h);
                    bmp = mSmcEink.ConvertTextToImage(bmp, ((Label)ctl), Color.White, x, y);

                }

                else if (ctl is PictureBox)
                {
                    int x = ((PictureBox)ctl).Location.X;
                    int y = ((PictureBox)ctl).Location.Y;
                    int w = ((PictureBox)ctl).Width;
                    int h = ((PictureBox)ctl).Height;

                 //   Console.WriteLine("PictureBox " + x + "," + y + "  w:" + w + ", h:" + h);
                    bmp = mSmcEink.ConvertImageToImage(bmp, ((PictureBox)ctl).Image, x, y, w, h);
                }
                else if (ctl is Label)
                {
                    int x = ((Label)ctl).Location.X;
                    int y = ((Label)ctl).Location.Y;
                    int w = ((Label)ctl).Width;
                    int h = ((Label)ctl).Height;

                 //   Console.WriteLine("Label " + x + "," + y + "  w:" + w + ", h:" + h);
                    bmp = mSmcEink.ConvertTextToImage(bmp, ((Label)ctl), Color.Black, x, y);
                }

            }
            pictureBox_29.Image = bmp;
            bmp = RotateImageByAngle(bmp, 270);
        }

        public Bitmap setESLimageDemo_29(Panel panel1, string tag1_1)
        {
            Bitmap bmp = new Bitmap(296, 128);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, 296, 128);
            }

            BarcodeWriter barcodeWriter = new BarcodeWriter()
            {
                Format = BarcodeFormat.CODE_39
            };

            foreach (Control control in panel1.Controls)
            {
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = true;
                }
                else if (control is Label)
                {
                    int x = ((Label)control).Location.X;
                    int y = ((Label)control).Location.Y;
                    int width = ((Label)control).Width;
                    int height = ((Label)control).Height;
              //      Console.WriteLine(string.Concat(new object[] { x, ",", y, "  w:", width, ", h:", height }));
                    bmp = this.mSmcEink.ConvertTextToImageDemo(bmp, (Label)control, Color.White, x, y);
                }
                else if (control is Panel)
                {

                    int xPanel = ((Panel)control).Location.X;
                    int yPanel = ((Panel)control).Location.Y;
                    int widthPanel = ((Panel)control).Width;
                    int heightPanel = ((Panel)control).Height;
//Console.WriteLine("controlINPanel");


                    bmp = mSmcEink.ConvertPanelToImage(bmp, (Panel)control, Color.Black, xPanel, yPanel);

                }
                else if (control is PictureBox)
                {
                    int num = ((PictureBox)control).Location.X;
                    int y1 = ((PictureBox)control).Location.Y;
                    int width1 = ((PictureBox)control).Width;
                    int height1 = ((PictureBox)control).Height;
                    Bitmap bar93 = new Bitmap(width1, height1);

                    barcodeWriter.Options.Width = width1;
                    barcodeWriter.Options.Height = height1;

                    barcodeWriter.Options.Margin = 0;
                    barcodeWriter.Options.PureBarcode = true;
                    bar93 = barcodeWriter.Write(tag1_1);
                 //   Console.WriteLine("barW" + bar93.Width);
                  //  Console.WriteLine("barH" + bar93.Height);


                    // this.pictureBox2.Image = bar;
                   // Console.WriteLine(string.Concat(new object[] { "PictureBox ", num, ",", y1, "  w:", width1, ", h:", height1 }));
                    //    this.bmp = this.mSmcDataToImage.ConvertImageToImage(this.bmp, this.pictureBox2.Image, num, y1, this.pictureBox2.Width, this.pictureBox2.Height);
                    bmp = this.mSmcEink.ConvertBarToImage(bmp, bar93, num, y1);
                }
                else if (control is Label)
                {
                    int x1 = ((Label)control).Location.X;
                    int num1 = ((Label)control).Location.Y;
                    int width2 = ((Label)control).Width;
                    int height2 = ((Label)control).Height;
                  //  Console.WriteLine(string.Concat(new object[] { "Label ", x1, ",", num1, "  w:", width2, ", h:", height2 }));
                    bmp = mSmcEink.ConvertTextToImageDemo(bmp, (Label)control, Color.Black, x1, num1);
                }
            }

   

            return bmp;
        }




        private void panel6labelDemo_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Label aaa = (Label)sender;
            Brush greenBrush = new SolidBrush(Color.Red);//把这个变成与你背景一样的颜色
            int radius = 17;

            FontFamily fontFamily = new FontFamily("Calibri");
            Font font = new Font(fontFamily, aaa.Font.Size, aaa.Font.Style, GraphicsUnit.Point);
            // 绘制圆，(0, 0)为左上角的坐标，radius为直径
            graphic.FillEllipse(greenBrush, 0, 0, radius, radius);
            graphic.DrawString(aaa.Text, font, new SolidBrush(aaa.ForeColor), 2, 0);
        }

        /// <summary>
        /// Rotates the image by angle.
        /// </summary>
        /// <param name="oldBitmap">The old bitmap.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        private static Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            var graphics = Graphics.FromImage(newBitmap);
            graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
            graphics.DrawImage(oldBitmap, new Point(0, 0));
            return newBitmap;
        }


        private void UpdateContinueTimer(object sender, EventArgs e)
        {

            TimeUpdate(15, 0);
            texMessageBox.Text = texMessageBox.Text + "傳送秒數:" + min + "m" + sec + "s " + secm + "ms" + "  Count:"  + updateCount + "\r\n";
            label2.Text = min + "m" + sec + "s " + secm + "ms";
            label4.Text = updateCount.ToString();
            UpdateTimer.Stop();
            string com = "040116";
            byte[] bcom = iCheckSum(StringToByteArray(com));
            SendData(bcom);
            Write_Check = false;
            WriteStopwatch.Reset();
            UIDReaderTimer.Start();

        }

        private void UUIDTimer(object sender, EventArgs e)
        {

            string com = "040116";
            byte[] bcom = iCheckSum(StringToByteArray(com));
            SendData(bcom);
            Write_Check = false;

        }

        private void WriteContinueTimer(object sender, EventArgs e)
        {
            /*  string com = "040116";
              byte[] bcom = iCheckSum(StringToByteArray(com));
              SendData(bcom);
              Write_Check = false;
              WriteTimer.Stop();*/
            WriteTimer.Stop();
            UpdateTimer.Start();

        }
        

        private void continue29_Click(object sender, EventArgs e)
        {
            continueWirte29 = true;

            if (ReadIDClick)
            {
                UIDReaderTimer.Stop();
                ReadUID.Text = "讀取Tag";
                ReadUID.ForeColor = Color.Black;
                ReadIDClick = false;
            }
            label2.Visible = true;
            label4.Visible = true;
            string com = "040116";
            byte[] bcom = iCheckSum(StringToByteArray(com));
            SendData(bcom);
            Write_Check = false;
            WriteStopwatch.Start();
            UIDReaderTimer.Start();
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
                    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // InsertDataList.Clear();
                //List<text_data_list> dataList = new List<text_data_list>();
                string txtPath = openFileDialog1.FileName;
                string file = Path.GetExtension(openFileDialog1.FileName);
                if (file == ".txt")
                {
                    using (StreamReader sr = File.OpenText(openFileDialog1.FileName))
                    {
                        //listBox_Battery.Items.Clear();
                        String input;
                        int j = 0;
                        int t7 = 0;
                        tag1_1.tag = null;
                        tag1_2.tag = null;
                        tag1_3.tag = null;
                        tag2_1.tag = null;
                        tag2_2.tag = null;
                        tag3_1.tag = null;
                        tag3_2.tag = null;
                        tag3_3.tag = null;
                        tag4_1.tag = null;
                        tag4_2.tag = null;
                        tag4_3.tag = null;
                        tag4_4.tag = null;
                        tag4_5.tag = null;
                        tag4_6.tag = null;
                        tag4_7.tag = null;
                        tag4_8.tag = null;
                        tag4_9.tag = null;
                        tag4_10.tag = null;
                        tag4_11.tag = null;
                        tag4_12.tag = null;
                        tag4_13.tag = null;
                        tag4_14.tag = null;
                        tag4_15.tag = null;
                        tag5_1.tag = null;
                        tag5_2.tag = null;
                        tag6_1.tag = null;
                        tag6_2.tag = null;
                        tag6_3.tag = null;
                        tag6_4.tag = null;
                        while ((input = sr.ReadLine()) != null)
                        {
                            Color color = Color.Black;
                            if (j == 7 && t7 < 1)
                            {
                                t7++;
                            }
                            else
                            {
                                /*  if (j == 7)
                                      j++;*/
                                if (input.Length > 0)
                                {
                                    if (input.IndexOf('@') > -1)
                                    {
                                        color = Color.Red;
                                        input = input.Trim('@');
                                    }
                                        

                                    switch (j)
                                    {
                                        case 0:
                                            tag1_1.tagColor = color;
                                            tag1_1.tag = input;
                                            break;
                                        case 1:
                                            tag1_2.tagColor = color;
                                            tag1_2.tag = input;
                                            break;
                                        case 2:
                                            tag1_3.tagColor = color;
                                            tag1_3.tag = input;
                                            break;
                                        case 3:
                                            tag2_1.tagColor = color;
                                            tag2_1.tag = input;
                                            break;
                                        case 4:
                                            tag2_2.tagColor = color;
                                            tag2_2.tag = input;
                                            break;
                                        case 5:
                                            tag3_1.tagColor = color;
                                            tag3_1.tag = input;
                                            break;
                                        case 6:
                                            tag3_2.tagColor = color;
                                            tag3_2.tag = input;
                                            break;
                                        case 7:
                                            tag3_3.tagColor = color;
                                            tag3_3.tag = input;
                                            break;
                                        case 8:
                                            tag4_1.tagColor = color;
                                            tag4_1.tag = input;
                                            break;
                                        case 9:
                                            tag4_2.tagColor = color;
                                            tag4_2.tag = input;
                                            break;
                                        case 10:
                                            tag4_3.tagColor = color;
                                            tag4_3.tag = input;
                                            break;
                                        case 11:
                                            tag4_4.tagColor = color;
                                            tag4_4.tag = input;
                                            break;
                                        case 12:
                                            tag4_5.tagColor = color;
                                            tag4_5.tag = input;
                                            break;
                                        case 13:
                                            tag4_6.tagColor = color;
                                            tag4_6.tag = input;
                                            break;
                                        case 14:
                                            tag4_7.tagColor = color;
                                            tag4_7.tag = input;
                                            break;
                                        case 15:
                                            tag4_8.tagColor = color;
                                            tag4_8.tag = input;
                                            break;
                                        case 16:
                                            tag4_9.tagColor = color;
                                            tag4_9.tag = input;
                                            break;
                                        case 17:
                                            tag4_10.tagColor = color;
                                            tag4_10.tag = input;
                                            break;
                                        case 18:
                                            tag4_11.tagColor = color;
                                            tag4_11.tag = input;
                                            break;
                                        case 19:
                                            tag4_12.tagColor = color;
                                            tag4_12.tag = input;
                                            break;
                                        case 20:
                                            tag4_13.tagColor = color;
                                            tag4_13.tag = input;
                                            break;
                                        case 21:
                                            tag4_14.tagColor = color;
                                            tag4_14.tag = input;
                                            break;
                                        case 22:
                                            tag4_15.tagColor = color;
                                            tag4_15.tag = input;
                                            break;
                                        case 23:
                                            tag5_1.tagColor = color;
                                            tag5_1.tag = input;
                                            break;
                                        case 24:
                                            tag5_2.tagColor = color;
                                            tag5_2.tag = input;
                                            break;
                                        case 25:
                                            tag6_1.tagColor = color;
                                            tag6_1.tag = input;
                                            break;
                                        case 26:
                                            tag6_2.tagColor = color;
                                            tag6_2.tag = input;
                                            break;
                                        case 27:
                                            tag6_3.tagColor = color;
                                            tag6_3.tag = input;
                                            break;
                                        case 28:
                                            tag6_4.tagColor = color;
                                            tag6_4.tag = input;
                                            break;
                                        default:
                                            Console.WriteLine("Default case");
                                            break;
                                    }
                                    j++;

                                }

                            }


                        }
                        uodateTxt();
                        sr.Close();
                    }
                }
            }

            Bitmap aa = setESLimageDemo_29(panel29, tag1_1.tag);
            pictureBox_29.Image = aa;
        }


        public void uodateTxt()
        {
            this.Label4Demo.Text = tag1_2.tag;
            this.Label4Demo.ForeColor = tag1_2.tagColor;
            this.Label5Demo.Text = tag1_3.tag;
            this.Label5Demo.ForeColor = tag1_3.tagColor;
            this.label13Demo.Text = tag2_1.tag;
            this.label13Demo.ForeColor = tag2_1.tagColor;
          //  this.panel6labelDemo.Text = tag2_2.tag;
           // this.panel6labelDemo.ForeColor = tag2_2.tagColor;
            this.Label2Demo.Text = tag3_1.tag;
            this.Label2Demo.ForeColor = tag3_1.tagColor;
            this.Label16Demo.Text = tag3_2.tag;
            this.Label16Demo.ForeColor = tag3_2.tagColor;
            this.Label17Demo.Text = tag3_3.tag;
            this.Label17Demo.ForeColor = tag3_3.tagColor;
            this.Label3Demo.Text = (tag4_1.tag + tag4_2.tag + tag4_3.tag);
            this.Label3Demo.ForeColor = tag4_1.tagColor;
            this.Label18Demo.Text = (tag4_4.tag);
            this.Label18Demo.ForeColor = tag4_4.tagColor;
            this.Label1Demo.Text = (tag4_5.tag + tag4_6.tag + tag4_7.tag + tag4_8.tag + tag4_9.tag + tag4_10.tag);
            this.Label1Demo.ForeColor = tag4_5.tagColor;
            this.Label8Demo.Text = (tag4_11.tag + tag4_12.tag + tag4_13.tag + tag4_14.tag);
            this.Label8Demo.ForeColor = tag4_11.tagColor;
            this.Label19Demo.Text = (tag4_15.tag);
            this.Label19Demo.ForeColor = tag4_15.tagColor;
            this.label5Demo.Text = tag5_1.tag;
            this.label5Demo.ForeColor = tag5_1.tagColor;
            this.Label7Demo.Text = tag5_2.tag;
            this.Label7Demo.ForeColor = tag5_2.tagColor;
            this.Label6Demo.Text = tag6_1.tag;
            this.Label6Demo.ForeColor = tag6_1.tagColor;
            this.Label62Demo.Text = tag6_2.tag;
            this.Label62Demo.ForeColor = tag6_2.tagColor;
            this.Label63Demo.Text = tag6_3.tag;
            this.Label63Demo.ForeColor = tag6_3.tagColor;
            this.Label64Demo.Text = tag6_4.tag;
            this.Label64Demo.ForeColor = tag6_4.tagColor;


        }


        private void DpiGet(float dpiX, float dpiY)
        {
            // 找出字體大小,並算出比例
             Graphics  graphics = this.CreateGraphics();
             dpiX = graphics.DpiX;
             dpiY = graphics.DpiY;
        }

        private void dddd()
        {
            Console.WriteLine(count);
            if (count < (t.Length / 32))
            {
                texMessageBox.Text = "黑 寫入成功:" + count + "\r\n";
                string com = "150127" + "00" + t.Substring(count * substringcount, substringcount);
                byte[] bcom = iCheckSum(StringToByteArray(com));
                SendData(bcom);
                count++;
            }
            else if (count == (t.Length / 32))
            {
                total_count = 0;
                count++;
                if (Red == false)
                {
                    string com = "150127" + "ff" + "ffffffffffffffffffffffffffffffff";
                    byte[] bcom = iCheckSum(StringToByteArray(com));
                    SendData(bcom);
                    texMessageBox.Text = texMessageBox.Text + "傳送結束開始更新電子紙" + "\r\n";
                }
            }
            if (count > (t.Length / 32) && total_count == 0 && Red == true)
            {
                texMessageBox.Text = "紅 寫入成功:" + total_count + "\r\n";
                string com = "150127" + "40" + r.Substring(total_count * substringcount, substringcount);
                byte[] bcom = iCheckSum(StringToByteArray(com));
                SendData(bcom);
                count++;
                total_count++;
            }
            else if (count > (t.Length / 32) && total_count < ((r.Length / 32) - 1) && Red == true)
            {
                stopwatchtext.Stop();
                Console.WriteLine("start" + stopwatchtext.Elapsed.Milliseconds + "ms");
                stopwatchtext.Reset();
                stopwatchtext.Start();

                texMessageBox.Text = "紅 寫入成功:" + total_count + "\r\n";
                string com = "150127" + "80" + r.Substring(total_count * substringcount, substringcount);
                byte[] bcom = iCheckSum(StringToByteArray(com));
                SendData(bcom);
                count++;
                total_count++;
            }
            else if (count > (t.Length / 32) && total_count == ((r.Length / 32) - 1) && Red == true)
            {
                texMessageBox.Text = "紅 寫入成功:" + total_count + "\r\n";
                string com = "150127" + "ff" + r.Substring(total_count * substringcount, substringcount);
                byte[] bcom = iCheckSum(StringToByteArray(com));
                SendData(bcom);
                count++;
                total_count++;
            }
            else if (count > (t.Length / 32) && total_count == (r.Length / 32) && Red == true)
            {

                texMessageBox.Text = "紅 寫入成功 :" + total_count + "\r\n";
                stopwatch.Stop();
                texMessageBox.Text = texMessageBox.Text + "傳送結束開始更新電子紙" + "\r\n";
                texMessageBox.Text = texMessageBox.Text + "傳送秒數:" + stopwatch.Elapsed.Seconds + "s " + stopwatch.Elapsed.Milliseconds + "ms" + "\r\n";
                texMessageBox.Text = texMessageBox.Text + "傳送秒數:" + stopwatchtext.Elapsed.Milliseconds + "ms" + "\r\n";
                string com = "150127" + "ff" + "ffffffffffffffffffffffffffffffff";
                byte[] bcom = iCheckSum(StringToByteArray(com));
                SendData(bcom);
                count++;
                total_count++;
            }
            else if (count > (t.Length / 32) && total_count > (r.Length / 32) && Red == true)
            {
                count = 0;
                total_count = 0;
                Write_Check = false;
            }
        }

    }
}
