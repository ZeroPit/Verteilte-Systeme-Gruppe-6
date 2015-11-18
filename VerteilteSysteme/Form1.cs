using Cloo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerteilteSysteme
{
    public partial class Form1 : Form
    {
        #region Variablen
        double _Sx = -2.1;
        double _Sy = -1.3;
        double _Fx = 1;
        double _Fy = 1.3;

        Point _StartPoint = Point.Empty;


        Color[] _DrawColors;

        #endregion Variablen

        #region Konstrukto

        public Form1()
        {
            InitializeComponent();
            createColor();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            initMandel();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            initMandel();
        }
        #endregion Konstrukto

        #region init

        private void createColor()
        {
            _DrawColors = new Color[256];
            int i = 0;
            for (int red = 0; red <= 255; red += 51)
            {/* the six values of red */
                for (int green = 0; green <= 255; green += 51)
                {
                    for (int blue = 0; blue <= 255; blue += 51)
                    {
                        Color lColor = Color.FromArgb(red, green, blue);
                        _DrawColors[i] = lColor;
                        ++i;
                    }
                }
            }
            int i2 = 0;
            for (; i < 256; i++)
            {
                _DrawColors[i] = _DrawColors[i2];
                i2++;
            }
        }
        private void initMandel()
        {
            _Sx = -2.1;
            _Sy = -1.3;
            _Fx = 1;
            _Fy = 1.3;

            Bitmap b = new Bitmap(this.Width, this.Height);
            double x, y, x1, y1, xx, xmin, xmax, ymin, ymax = 0.0;
            int looper, s, z = 0;
            double intigralX, intigralY = 0.0;
            xmin = _Sx;
            ymin = _Sy;
            xmax = _Fx;
            ymax = _Fy;
            intigralX = (xmax - xmin) / plBack.Width;
            intigralY = (ymax - ymin) / plBack.Height;
            x = xmin;
            for (s = 1; s < plBack.Width; s++)
            {
                y = ymin;
                for (z = 1; z < plBack.Height; z++)
                {
                    x1 = 0;
                    y1 = 0;
                    looper = 0;
                    while (looper < 100 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
                    {
                        looper++;
                        xx = (x1 * x1) - (y1 * y1) + x;
                        y1 = 2 * x1 * y1 + y;
                        x1 = xx;
                    }
                    double perc = looper / (100.0);
                    int val = ((int)(perc * 255));
                    b.SetPixel(s, z, _DrawColors[val]);
                    y += intigralY;
                }
                x += intigralX;
            }
            plBack.BackgroundImage = (Image)b;
            update();
        }

        #endregion init

        #region Button
        private void button1_Click(object sender, EventArgs e)
        {
            initMandel();
        }
        private void button2_Click(object sender, EventArgs e)
        {

            // pick first platform
            ComputePlatform platform = ComputePlatform.Platforms[0];

            // create context with all gpu devices
            ComputeContext context = new ComputeContext(ComputeDeviceTypes.Gpu,
                new ComputeContextPropertyList(platform), null, IntPtr.Zero);

            // create a command queue with first gpu found
            ComputeCommandQueue queue = new ComputeCommandQueue(context,
                context.Devices[0], ComputeCommandQueueFlags.None);

            // load opencl source
            StreamReader streamReader = new StreamReader("../../kernels.cl");
            string clSource = streamReader.ReadToEnd();
            streamReader.Close();

            // create program with opencl source
            ComputeProgram program = new ComputeProgram(context, clSource);

            // compile opencl source
            program.Build(null, null, null, IntPtr.Zero);

            // load chosen kernel from program
            ComputeKernel kernel = program.CreateKernel("helloWorld");

            // create a ten integer array and its length
            int[] message = new int[] { 1, 2, 3, 4, 5 };
            int messageSize = message.Length;

            // allocate a memory buffer with the message (the int array)
            ComputeBuffer<int> messageBuffer = new ComputeBuffer<int>(context,
                ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.UseHostPointer, message);

            kernel.SetMemoryArgument(0, messageBuffer); // set the integer array
            kernel.SetValueArgument(1, messageSize); // set the array size

            // execute kernel
            queue.ExecuteTask(kernel, null);

            // wait for completion
            queue.Finish();
        }
        private void btnZoom_Click(object sender, EventArgs e)
        {
            double lWidth, lHeight, lNewWidth, lNewHeight;

            lWidth = (_Fx - _Sx);
            lHeight = (_Fy - _Sy);

            lNewWidth = lWidth / 4;
            lNewHeight = lHeight / 4;

            _Sx = _Sx + lNewWidth;
            _Fx = _Fx - lNewWidth;

            _Sy = _Sy + lNewHeight;
            _Fy = _Fy - lNewHeight;

            DrawMandel();
        }

        #endregion Button

        #region Update
        private void update()
        {
            //delegate void myTC_fertigCallback(int e); 


            //if(this.InvokeRequired == true) 
            //{ 

            //   this.Invoke(callback); 
            //} else


        }
        private void updateInfo()
        {
            lblInfo.Text = string.Format("X Position: {0}, Y Position: {1} Breite/Höhe : {2}/{3}", _Sx, _Sy, (_Fx - _Sx), (_Fy - _Sy));
        }
        #endregion Update

        private void DrawMandel()
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            double x, y, x1, y1, xx, xmin, xmax, ymin, ymax = 0.0;
            int looper, s, z = 0;
            double intigralX, intigralY = 0.0;
            xmin = _Sx;
            ymin = _Sy;
            xmax = _Fx;
            ymax = _Fy;
            intigralX = (xmax - xmin) / plBack.Width;
            intigralY = (ymax - ymin) / plBack.Height;
            x = xmin;
            for (s = 1; s < plBack.Width; s++)
            {
                y = ymin;
                for (z = 1; z < plBack.Height; z++)
                {
                    x1 = 0;
                    y1 = 0;
                    looper = 0;
                    while (looper < 200 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
                    {
                        looper++;
                        xx = (x1 * x1) - (y1 * y1) + x;
                        y1 = 2 * x1 * y1 + y;
                        x1 = xx;
                    }
                    if(looper == 200)
                    { }
                    double perc = looper / (200.0);
                    int val = ((int)(perc * 255));
                    b.SetPixel(s, z, _DrawColors[val]);
                    y += intigralY;
                }
                x += intigralX;
            }
            plBack.BackgroundImage = (Image)b;
            update();
        }


        #region Events

        private void palOutput_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _StartPoint = new Point(e.X, e.Y);
            }
        }
        private void palOutput_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_StartPoint != Point.Empty)
                {
                    Point lTempPoint = new Point(e.X, _StartPoint.Y + ((int)(((plFore.Height - 28) / (plFore.Width * 1.0)) * (e.X - _StartPoint.X))));
                    Rectangle lRect = Rectangle.FromLTRB(_StartPoint.X, _StartPoint.Y, lTempPoint.X, lTempPoint.Y);
                    Bitmap lBitmap = new Bitmap(plFore.Width, plFore.Height);
                    Graphics g = Graphics.FromImage((Image)lBitmap);
                    g.DrawRectangle(new Pen(Brushes.Red, 3), lRect);
                    plFore.BackgroundImage = null;
                    plFore.BackgroundImage = (Image)lBitmap;
                }
            }
        }
        private void palOutput_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_StartPoint != Point.Empty)
                {
                    Point lFinishPoint = new Point(e.X, _StartPoint.Y + ((int)(((plFore.Height - 28) / (plFore.Width * 1.0)) * (e.X - _StartPoint.X))));
                    double distX = _Fx - _Sx;
                    double distY = _Fy - _Sy;
                    double oSx = _Sx;
                    double oSy = _Sy;
                    _Sx = (_StartPoint.X / (plFore.Width * 1.0)) * distX;
                    _Sx += oSx;
                    _Fx = (lFinishPoint.X / (plFore.Width * 1.0)) * distX;
                    _Fx += oSx;
                    _Sy = (_StartPoint.Y / (plFore.Height * 1.0)) * distY;
                    _Sy += oSy;
                    _Fy = (lFinishPoint.Y / (plFore.Height * 1.0)) * distY;
                    _Fy += oSy;
                    Thread tsv = new Thread(new ThreadStart(this.DrawMandel));
                    tsv.Start();
                    _StartPoint = Point.Empty;
                    plFore.BackgroundImage = null;
                }
            }
        }
        private void palOutput_MouseLeave(object sender, EventArgs e)
        {
            _StartPoint = Point.Empty;
            plFore.BackgroundImage = null;
        }

        #endregion Events

       






    }
}
