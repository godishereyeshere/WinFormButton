using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IGC_IPC.CustomButton
{
    public class ColorChangingButton : Button
    {
        private Timer timer;
        private Random random;

        public ColorChangingButton()
        {
            random = new Random();

            timer = new Timer();
            timer.Interval = 1000; // Set the timer to change color every 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.BackColor = GetRandomColor();
            this.Invalidate(); // Force the button to repaint itself
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var graphicsPath = new GraphicsPath();
            AddRoundedRectangle(graphicsPath, new Rectangle(0, 0, this.Width, this.Height),
                40); // Change the second parameter to adjust the roundness
            this.Region = new Region(graphicsPath);
            base.OnPaint(pevent);
        }

        private void AddRoundedRectangle(GraphicsPath path, Rectangle rectangle, int cornerRadius)
        {
            if (cornerRadius > 0)
            {
                path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rectangle.Right - cornerRadius, rectangle.Y, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rectangle.Right - cornerRadius, rectangle.Bottom - cornerRadius, cornerRadius, cornerRadius,
                    0, 90);
                path.AddArc(rectangle.X, rectangle.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            }
            else
            {
                path.AddRectangle(rectangle);
            }

            path.CloseFigure();
        }
    }
}
