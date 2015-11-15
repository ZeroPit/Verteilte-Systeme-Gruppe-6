using Cloo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerteilteSysteme
{
    public partial class Form1 : Form
    {
       
        double Sx = -2.1;
        double Sy = -1.3;
        double Fx = 1;
        double Fy = 1.3;

        Point pStart = Point.Empty;
        Point pFinish = Point.Empty;

        Color[] _DrawColors;

        Bitmap _OutputBitmap = null;

        public Form1()
        {
            InitializeComponent();
            createColor();
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            DrawMandel();
        }


     
        private void DrawMandel()
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            double x, y, x1, y1, xx, xmin, xmax, ymin, ymax = 0.0;
            int looper, s, z = 0;
            double intigralX, intigralY = 0.0;
            xmin = Sx;
            ymin = Sy;
            xmax = Fx;
            ymax = Fy;
            intigralX = (xmax - xmin) / palOutput.Width;
            intigralY = (ymax - ymin) / palOutput.Height;
            x = xmin;
            for (s = 1; s < palOutput.Width; s++)
            {
                y = ymin;
                for (z = 1; z < palOutput.Height; z++)
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
            _OutputBitmap = b;
            palOutput.BackgroundImage = (Image)_OutputBitmap;           
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



    }
}
