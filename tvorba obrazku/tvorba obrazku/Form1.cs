using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tvorba_obrazku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap originalBitMap = new Bitmap("C:\\Users\\cerve\\Downloads\\bomba.jpg");
            Bitmap modifiedBitmap = applyConvolution(originalBitMap);
            pictureBox1.Image = modifiedBitmap;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            /* Bitmap temp = new Bitmap(256, 256);
             for (int i = 0; i < temp.Height; i++)
             {
                 for (int j = 0; j < temp.Width; j++)
                 {

                     temp.SetPixel(i, j, Color.FromArgb(i, j, trackBar1.Value));


                 }
             }
             pictureBox1.Image = temp;
         }*/






            
        }
        public Bitmap applyConvolution(Bitmap original)
        {
            Bitmap export = new Bitmap(original.Width, original.Height);

            //y svisle, x vodorovně - [y,x]
            int[,] kernel = new int[3, 3];
            kernel[0, 0] = int.Parse(textBox1.Text);
            kernel[0, 1] = int.Parse(textBox4.Text);
            kernel[0, 2] = int.Parse(textBox6.Text);

            kernel[1, 0] = int.Parse(textBox2.Text);
            kernel[1, 1] = int.Parse(textBox3.Text);
            kernel[1, 2] = int.Parse(textBox5.Text);

            kernel[2, 0] = int.Parse(textBox9.Text);
            kernel[2, 1] = int.Parse(textBox8.Text);
            kernel[2, 2] = int.Parse(textBox7.Text);

            for (int y = 1; y < original.Height - 1; y++)
            {
                for (int x = 1; x < original.Width - 1; x++)
                {
                    // Kernelo
                    int sumR = 0;

                    for (int yk = 0; yk < 3; yk++)
                    {
                        for (int xk = 0; xk < 3; xk++)
                        {
                            Color pixelColor = original.GetPixel(x - 1 + xk, y - 1 + yk);
                            sumR += pixelColor.R * kernel[yk, xk];
                        }
                    }

                    sumR /= 8;
                    sumR = Math.Max(0, Math.Min(255, sumR)); // Omezení na rozsah 0-255

                    export.SetPixel(x, y, Color.FromArgb(sumR, sumR, sumR));
                }
            }

            return export;
        }
    }
}
