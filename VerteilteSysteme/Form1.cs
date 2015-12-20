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

using System.Runtime.InteropServices;

namespace VerteilteSysteme
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        #region Variablen

        //               _Fy
        //                |
        //                |
        //  _Sx-----------0------_Fx
        //                |
        //                |
        //               _Sy

        double _Sx = -2.1;
        double _Sy = -1.3;
        double _Fx = 1;
        double _Fy = 1.3;

        double _LastPositionX = 0.0;
        double _LastPositionY = 0.0;


        Point _StartPoint = Point.Empty;
        Bitmap _AreaBitmap = null;
        Color[] _DrawColors;

        bool _UseGPU = true;
        bool _XYFinder = false;

        // That's our custom TextWriter class
        TextWriter _writer = null;

        #endregion Variablen

        #region Konstrukto

        /// <summary>
        /// Konstrukto der Form für die anzeige von Mandelbrot
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            initOpenCL();
            createColor();
        }

        /// <summary>
        /// Kernel32 Funktion AllocConsole
        /// zum öffnen der Console zur laufzeit 
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        #endregion Konstrukto

        #region Button

        private void btnFindPosition_Click(object sender, EventArgs e)
        {
            findXYPosition();
        }
        private void btnJumpToPosition_Click(object sender, EventArgs e)
        {
            zoomToPosition();
        }
        private void btnStartZoom_Click(object sender, EventArgs e)
        {
            startAutoZoom();
        }
        private void btnStopZoom_Click(object sender, EventArgs e)
        {
            stopAutoZoom();
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
            AllocConsole();
            Console.WriteLine("Ausgabe GPU");
            // Instantiate the writer
            _writer = new StreamTextBox(txtConsole);
            // Redirect the out Console stream
            Console.SetOut(_writer);
            Console.WriteLine("Ausgabe CPU");
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
                    if (_AreaBitmap != null)
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

                    _StartPoint = Point.Empty;
                    plFore.BackgroundImage = null;
                    calculateMandel();
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

        #region Auto Zoom Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timZoom_Tick(object sender, EventArgs e)
        {

            double lZoom = 0.98;
            double lWidth = (_Fx - _Sx);
            double lHeight = (_Fy - _Sy);

            double lNewWidth = (lWidth - (lWidth * lZoom)) / 2;
            double lNewHeight = (lHeight - (lHeight * lZoom)) / 2;

            _Sx = _Sx + lNewWidth;
            _Fx = _Fx - lNewWidth;

            _Sy = _Sy + lNewHeight;
            _Fy = _Fy - lNewHeight;

            calculateMandel();
        }

        #endregion Auto Zoom Events

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
                    while (looper < 254 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
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
            updateInfo();
        }

        private void initOpenCL()
        {
            try
            {
                // Laden des OpenCL Source Code       
                string lClSourceCode = VerteilteSysteme.Properties.Resources.kernels;

                //Initializes OpenCL Platforms and Devices and sets everything up
                OpenCLTemplate.CLCalc.InitCL();

                //Compiles the source codes. The source is a string array because the user may want
                //to split the source into many strings.
                OpenCLTemplate.CLCalc.Program.Compile(new string[] { lClSourceCode });
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden der OpenCL Platforms und Devices");
            }
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

            double intigralX = (_Fx - _Sx) / plBack.Width;
            double intigralY = (_Fy - _Sy) / plBack.Height;

            int lAddX = pPoint.X;
            int lAddY = pPoint.Y;

            if (plBack.Width / 2 < lAddX)
            {
                lAddX = lAddX - (plBack.Width / 2);
                txtPositionX.Text = (_LastPositionX + (intigralX * lAddX)).ToString();
            }
            else
            {
                lAddX = (plBack.Width / 2) - lAddX;
                txtPositionX.Text = (_LastPositionX - (intigralX * lAddX)).ToString();
            }

            if (plBack.Height / 2 < lAddY)
            {
                lAddY = lAddY - (plBack.Height / 2);
                txtPositionY.Text = (_LastPositionY - (intigralY * lAddY)).ToString();
            }
            else
            {
                lAddY = (plBack.Height / 2) - lAddY;
                txtPositionY.Text = (_LastPositionY + (intigralY * lAddY)).ToString();
            }

        }

        private void zoomToPosition()
        {
            double lZoom = 1.0;
            double lPositionX = 0.0;
            double lPositionY = 0.0;
            if (double.TryParse(txtPositionX.Text, out lPositionX) && double.TryParse(txtPositionY.Text, out lPositionY))
            {
                lZoom = 1 - ((double)nudZoom.Value / 100);

                double lWidth = (_Fx - _Sx);
                double lHeight = (_Fy - _Sy);

                if (_LastPositionX != lPositionX)
                {
                    double lNewWidth = (lWidth * lZoom) / 2;
                    double lNewHeight = (lHeight * lZoom) / 2;

                    _Sx = lPositionX - lNewWidth;
                    _Fx = lPositionX + lNewWidth;

                    _Sy = (lPositionY * -1) - lNewHeight;
                    _Fy = (lPositionY * -1) + lNewHeight;
                }
                else
                {
                    double lNewWidth = (lWidth - (lWidth * lZoom)) / 2;
                    double lNewHeight = (lHeight - (lHeight * lZoom)) / 2;
                    _Sx = _Sx + lNewWidth;
                    _Fx = _Fx - lNewWidth;

                    _Sy = _Sy + lNewHeight;
                    _Fy = _Fy - lNewHeight;
                }
                calculateMandel();
            }
        }

        #endregion XY Position Zoom

        #region XY Auto Zoom

        /// <summary>
        /// Startet den Timer setzt den start aus XY Positionierung
        /// </summary>
        private void startAutoZoom()
        {
            if (!timZoom.Enabled)
            {
                double lZoom = 1.0;
                double lPositionX = 0.0;
                double lPositionY = 0.0;
                if (double.TryParse(txtPositionX.Text, out lPositionX) && double.TryParse(txtPositionY.Text, out lPositionY))
                {
                    lZoom = 1 - ((double)nudZoom.Value / 100);

                    double lWidth = (_Fx - _Sx);
                    double lHeight = (_Fy - _Sy);

                    double lNewWidth = (lWidth * lZoom) / 2;
                    double lNewHeight = (lHeight * lZoom) / 2;

                    if (_LastPositionX != lPositionX)
                    {
                        _Sx = lPositionX - lNewWidth;
                        _Fx = lPositionX + lNewWidth;

                        _Sy = (lPositionY * -1) - lNewHeight;
                        _Fy = (lPositionY * -1) + lNewHeight;
                    }
                    calculateMandel();
                    timZoom.Interval = (int)nudZoomSpeed.Value;
                    timZoom.Start();
                }
            }
        }

        /// <summary>
        /// Stoppt den Timer 
        /// </summary>
        private void stopAutoZoom()
        {
            timZoom.Stop();
        }

        #endregion XY Auto Zoom

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
                lNewWidth = lWidth / 2;
                lNewHeight = lHeight / 2;

                _Sx = _Sx - lNewWidth;
                _Fx = _Fx + lNewWidth;

                _Sy = _Sy - lNewHeight;
                _Fy = _Fy + lNewHeight;
            }
            else
            {
                lNewWidth = lWidth / 4;
                lNewHeight = lHeight / 4;

                _Sx = _Sx + lNewWidth;
                _Fx = _Fx - lNewWidth;

                _Sy = _Sy + lNewHeight;
                _Fy = _Fy - lNewHeight;
            }

            calculateMandel();
        }

        #endregion Quick Zoom Funktionen

        #region OpenCL Test Funktionen

        /// <summary>
        /// OpenCL Test 1
        /// Es wird die Funktion helloWorld genutzt
        /// Die Hello OpenCL über die GPU Console ausgibt
        /// </summary>
        private void test1()
        {
            //Gets host access to the OpenCL floatVectorSum kernel
            OpenCLTemplate.CLCalc.Program.Kernel lHelloWorldKernel = new OpenCLTemplate.CLCalc.Program.Kernel("helloWorld");
            //Execute the kernel
            lHelloWorldKernel.Execute(null, 1);
        }

        /// <summary>
        /// OpenCL Test 2
        /// Es wird die Funktion sumTwoIntegers genutzt
        /// Die zwei Integers Arrays übergeben bekommt und Summiert
        /// Die Berechnungen werden über die GPU Console ausgeben
        /// und das Ergebnis nochmals über die CPU Console
        /// </summary>
        private void test2()
        {
            //Gets host access to the OpenCL floatVectorSum kernel
            OpenCLTemplate.CLCalc.Program.Kernel VectorSum = new OpenCLTemplate.CLCalc.Program.Kernel("sumTwoIntegers");

            //We want to sum 10 numbers
            int n = 10;

            //Create vectors with 2000 numbers
            int[] lInteger1 = new int[n];
            int[] lInteger2 = new int[n];

            //Zufallszahl generieren mittels Random   
            Random lRandom = new Random();

            //Creates population for v1 and v2
            for (int i = 0; i < n; i++)
            {
                lInteger1[i] = lRandom.Next(0, n);
                lInteger2[i] = lRandom.Next(0, n);
            }

            //Creates vectors v1 and v2 in the device memory
            OpenCLTemplate.CLCalc.Program.Variable lVariable1 = new OpenCLTemplate.CLCalc.Program.Variable(lInteger1);
            OpenCLTemplate.CLCalc.Program.Variable lVariable2 = new OpenCLTemplate.CLCalc.Program.Variable(lInteger2);

            //Arguments of VectorSum kernel
            OpenCLTemplate.CLCalc.Program.Variable[] lArgsVariable = new OpenCLTemplate.CLCalc.Program.Variable[] { lVariable1, lVariable2 };

            //How many workers will there be? We need “n”, one for each element
            int[] lWorkers = new int[1] { n };

            //Execute the kernel
            VectorSum.Execute(lArgsVariable, lWorkers);

            //Read device memory varV1 to host memory v1
            lVariable1.ReadFromDeviceTo(lInteger1);

            //Ausgabe über die CPU Console -> TextBox in der Form
            for (int i = 0; i < n; i++)
                Console.WriteLine(String.Format("Ergebnis {0} : {1}", i + 1, lInteger1[i]));
        }

        #endregion OpenCL Test Funktionen

        #region Mandel Berechnung

        /// <summary>
        /// Berechnung der Mandelbrot Menge
        /// und Grafische ausgabe
        /// </summary>
        private void calculateMandel()
        {
            if (_UseGPU)
                calculateMandelwithGPU();
            else
                calculateMandelwithCPU();
            updateInfo();
        }

        /// <summary>
        /// Berechnung mittels der GPU
        /// </summary>
        private void calculateMandelwithGPU()
        {
            //Gets host access to the OpenCL floatVectorSum kernel
            OpenCLTemplate.CLCalc.Program.Kernel VectorSum = new OpenCLTemplate.CLCalc.Program.Kernel("calculateMandel");

            //Anzahl aller Pixel die Berechnet werden müssen
            int n = plBack.Width * plBack.Height;
            int[] lCalculation = new int[n];

            //Start Variablen für die Berechnung
            double[] lStartValues = new double[6];
            lStartValues[0] = _Sx;
            lStartValues[1] = _Sy;
            lStartValues[2] = _Fx;
            lStartValues[3] = _Fy;
            lStartValues[4] = plBack.Width;
            lStartValues[5] = plBack.Height;

            //Creates vectors v1 and v2 in the device memory
            OpenCLTemplate.CLCalc.Program.Variable lVariable1 = new OpenCLTemplate.CLCalc.Program.Variable(lCalculation);
            OpenCLTemplate.CLCalc.Program.Variable lVariable2 = new OpenCLTemplate.CLCalc.Program.Variable(lStartValues);

            //Arguments of VectorSum kernel
            OpenCLTemplate.CLCalc.Program.Variable[] lArgsVariable = new OpenCLTemplate.CLCalc.Program.Variable[] { lVariable1, lVariable2 };

            //How many workers will there be? We need “n”, one for each element
            int[] lWorkers = new int[1] { n };

            //Execute the kernel
            VectorSum.Execute(lArgsVariable, lWorkers);

            //Read device memory varV1 to host memory v1
            lVariable1.ReadFromDeviceTo(lCalculation);

            //Ausgabe über die CPU Console -> TextBox in der Form
            int lW = plBack.Width;
            int lH = plBack.Height;
            Bitmap b = new Bitmap(lW, lH);
            for (int i = 0; i < n; i++)
            {
                int s = i % lW;
                int z = i / lW;
                b.SetPixel(s, z, _DrawColors[lCalculation[i]]);
            }
            plBack.BackgroundImage = (Image)b;
        }

        /// <summary>
        /// Berechnung mittels der CPU
        /// </summary>
        private void calculateMandelwithCPU()
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
                    while (looper < 254 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
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

        }

        #endregion Mandel Berechnung

        #region Info

        /// <summary>
        /// Die Info Ausgabe Aktualisieren
        /// </summary>
        private void updateInfo()
        {
            _LastPositionX = _Sx + ((_Fx - _Sx) / 2);
            _LastPositionY = (_Sy + ((_Fy - _Sy) / 2)) * (-1);

            lblInfo.Text = string.Format("X Position: {0}, Y Position: {1}", _LastPositionX, _LastPositionY);
        }

        #endregion Info

        #endregion  Private Funktionen
    }
}
