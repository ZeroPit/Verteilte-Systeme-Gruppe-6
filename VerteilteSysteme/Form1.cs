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
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        #region Variablen

        double _Sx = -2.1;
        double _Sy = -1.3;
        double _Fx = 1;
        double _Fy = 1.3;

        Point _StartPoint = Point.Empty;
        Bitmap _AreaBitmap = null;
        Bitmap _MadnelBitmap = null;
        Color[] _DrawColors;

        bool _XYFinder = false;

        #endregion Variablen

        #region Konstrukto

        /// <summary>
        /// Konstrukto der Form für die anzeige von Mandelbrot
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            createColor();
        }

        #endregion Konstrukto

        #region Button

        private void btnFindPosition_Click(object sender, EventArgs e)
        {
            findXYPosition();
        }
        private void btnJumpToPosition_Click(object sender, EventArgs e)
        {

        }
        private void btnStartZoom_Click(object sender, EventArgs e)
        {

        }
        private void btnStopZoom_Click(object sender, EventArgs e)
        {

        }
        private void btnZoomDown_Click(object sender, EventArgs e)
        {
            upOrDownZoom(false);
        }
        private void btnZoomUp_Click(object sender, EventArgs e)
        {
            upOrDownZoom(true);
        }
        private void btnTest1_Click(object sender, EventArgs e)
        {
            test1();
        }
        private void btnTest2_Click(object sender, EventArgs e)
        {
            test2();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            initMandel();
        }

        #endregion Button

        #region Events

        #region Form Events

        /// <summary>
        /// Wird beim Starten des Programms ausgeführt,
        /// sodass das Mandelbrot angezeigt wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            initMandel();
        }

        /// <summary>
        /// Wird aufgeruffen wenn die größe des Fensters angepasst wird 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            initMandel();
        }

        #endregion Form Events

        #region Mouse Events

        /// <summary>
        /// Setzt den Start Point oder die XY Psotion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void palOutput_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_XYFinder)
                    setFindXYPosition(new Point(e.X, e.Y));
                else
                    _StartPoint = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// Zeichnet eine neu zu Zoomende Bereich 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void palOutput_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_StartPoint != Point.Empty && !_XYFinder)
                {
                    Point lTempPoint = new Point(e.X, _StartPoint.Y + ((int)(((plFore.Height - 28) / (plFore.Width * 1.0)) * (e.X - _StartPoint.X))));
                    Rectangle lRect = Rectangle.FromLTRB(_StartPoint.X, _StartPoint.Y, lTempPoint.X, lTempPoint.Y);
                    if(_AreaBitmap != null)
                        _AreaBitmap.Dispose();
                    _AreaBitmap = new Bitmap(plFore.Width, plFore.Height);
                    Graphics g = Graphics.FromImage((Image)_AreaBitmap);
                    g.DrawRectangle(new Pen(Brushes.Red, 3), lRect);
                    plFore.BackgroundImage = null;
                    plFore.BackgroundImage = (Image)_AreaBitmap;
                }
            }
        }

        /// <summary>
        /// Wenn ein Start Point voranden ist wird das Zoomen gestartet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void palOutput_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_StartPoint != Point.Empty && !_XYFinder)
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
                    Thread tsv = new Thread(new ThreadStart(this.drawMandel));
                    tsv.Start();
                    _StartPoint = Point.Empty;
                    plFore.BackgroundImage = null;
                }
            }
        }

        /// <summary>
        /// Beim verlassen wird alles zurück gesetzt 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void palOutput_MouseLeave(object sender, EventArgs e)
        {
            _StartPoint = Point.Empty;
            plFore.BackgroundImage = null;
        }

        #endregion Mouse Events

        #endregion Events


        #region Private Funktionen

        #region Init Funktionen

        /// <summary>
        /// Initialisiert die erste Anzeige von Mandelbrot
        /// mittels CPU Berechnung
        /// </summary>
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
                    double perc = looper / (255.0);
                    int val = ((int)(perc * 255));
                    b.SetPixel(s, z, _DrawColors[val]);
                    y += intigralY;
                }
                x += intigralX;
            }
            plBack.BackgroundImage = (Image)b;
            update();
        }

        private void initOpenCL()
        {
            // Laden des OpenCL Source Code
            StreamReader streamReader = new StreamReader("../../kernels.cl");
            string lClSourceCode = streamReader.ReadToEnd();
            streamReader.Close();

            //Initializes OpenCL Platforms and Devices and sets everything up
            OpenCLTemplate.CLCalc.InitCL();

            //Compiles the source codes. The source is a string array because the user may want
            //to split the source into many strings.
            OpenCLTemplate.CLCalc.Program.Compile(new string[] { lClSourceCode });

            //Gets host access to the OpenCL floatVectorSum kernel
            OpenCLTemplate.CLCalc.Program.Kernel VectorSum = new OpenCLTemplate.CLCalc.Program.Kernel("helloWorld");
        }

        #endregion Init Funktionen

        #region Erstell Funktionen

        /// <summary>
        /// Erstellt ein Farbschema für die nutzung der Mandelbrot Anzeige
        /// </summary>
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

        #endregion Erstell Funktionen

        #region XY Position Zoom

        /// <summary>
        /// 
        /// </summary>
        private void findXYPosition()
        {
            _XYFinder = true;
            grbSettings.Enabled = false;
            plFore.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPoint"></param>
        private void setFindXYPosition(Point pPoint)
        {
            _XYFinder = false;
            grbSettings.Enabled = true;
            plFore.Cursor = Cursors.Default;

            double intigralX = (_Sy - _Sx) / plBack.Width;
            double intigralY = (_Fy - _Fx) / plBack.Height;

            txtPositionX.Text = (intigralX * pPoint.X).ToString();
            txtPositionY.Text = (intigralY * pPoint.Y).ToString();
        }

        #endregion XY Position Zoom

        #region Quick Zoom Funktionen

        /// <summary>
        /// Zoom eine ebene tiefer oder hoch vom Mittelpunkt ausgesehen
        /// </summary>
        /// <param name="Up"></param>
        private void upOrDownZoom(bool Up)
        {
            double lWidth, lHeight, lNewWidth, lNewHeight;

            lWidth = (_Fx - _Sx);
            lHeight = (_Fy - _Sy);

            if (Up)
            {
                lNewWidth = lWidth * 2;
                lNewHeight = lHeight * 2;

                _Sx = _Sx - lNewWidth;
                _Fx = _Fx + lNewWidth;

                _Sy = _Sy - lNewHeight;
                _Fy = _Fy + lNewHeight;
            }
            else
            {
                lNewWidth = lWidth / 2;
                lNewHeight = lHeight / 2;

                _Sx = _Sx + lNewWidth;
                _Fx = _Fx - lNewWidth;

                _Sy = _Sy + lNewHeight;
                _Fy = _Fy - lNewHeight;
            }

            drawMandel();
        }

        #endregion Quick Zoom Funktionen

        #region OpenCL Test Funktionen

        /// <summary>
        /// 
        /// </summary>
        private void test1()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void test2()
        {

        }

        #endregion OpenCL Test Funktionen

        #endregion  Private Funktionen

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


        private void sbutton2_Click(object sender, EventArgs e)
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


        /// <summary>
        ///  Berrechnung mittels der CPU
        /// </summary>
        private void drawMandel()
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
                    double perc = looper / (255.0);
                    int val = ((int)(perc * 255));
                    b.SetPixel(s, z, _DrawColors[val]);
                    y += intigralY;
                }
                x += intigralX;
            }
            plBack.BackgroundImage = (Image)b;
            update();
        }
    }
}
