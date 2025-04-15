using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokus
{
    class VykreslovaniGrafu : I_Observer //Generovano Copilotem
    {
        private List<List<int>> matice;
        private int N;
        private List<int> nodes;
        public void Observe(List<List<int>> KopieMatice)
        {
            this.matice = KopieMatice;
            this.N = KopieMatice.Count;
            this.nodes = new List<int>();
            for (int i = 1; i <= this.N; i++)
            {
                this.nodes.Add(i);
            }

        }
        public void VykresliGraf(Graphics g, int width, int height)
        {

            int radius = Math.Min(width, height) / 2 - 50; // Poloměr Kruzitka
            Point center = new Point(width / 2, height / 2); // Střed Kruzitka
            int uzlikDiameter = 40; // Průměr Uzliků
            List<Point> uzlikPositions = new List<Point>();

            // Vykreslení Uzliků
            for (int i = 0; i < this.N; i++)
            {
                double angle = 2 * Math.PI * i / nodes.Count; // Úhel pro každý Uzlik
                int x = center.X + (int)(radius * Math.Cos(angle)) - uzlikDiameter / 2;
                int y = center.Y + (int)(radius * Math.Sin(angle)) - uzlikDiameter / 2;

                // Uložení pozice Uzliku
                uzlikPositions.Add(new Point(x + uzlikDiameter / 2, y + uzlikDiameter / 2));


                // Vykreslení Uzliku (kruhu)
                g.FillEllipse(Brushes.White, x, y, uzlikDiameter, uzlikDiameter);
                g.DrawEllipse(Pens.Black, x, y, uzlikDiameter, uzlikDiameter);

                // Vykreslení čísla Uzliku mimo Kruzitko
                string uzlikLabel = nodes[i].ToString();
                Font font = new Font("Arial", 12);
                SizeF stringSize = g.MeasureString(uzlikLabel, font);
                int labelX = center.X + (int)((radius + uzlikDiameter / 2 + 10)
                    * Math.Cos(angle)) - (int)(stringSize.Width / 2);
                int labelY = center.Y + (int)((radius + uzlikDiameter / 2 + 10)
                    * Math.Sin(angle)) - (int)(stringSize.Height / 2);
                g.DrawString(uzlikLabel, font, Brushes.Black, labelX, labelY);

            }
            // Vykreslení Hranicky
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i==j)
                    {
                        continue;
                    }
                     else if(matice[i][j] == 1)
                    {
                        DrawArrow(g, uzlikPositions[i], uzlikPositions[j]);
                    }
                }
            }

        }
        private void DrawArrow(Graphics g, Point start, Point end)
        {
            Pen pen = new Pen(Color.Pink, 1);
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(4, 4);
            pen.CustomEndCap = bigArrow;
            g.DrawLine(pen, start, end);
        }
    }
}
