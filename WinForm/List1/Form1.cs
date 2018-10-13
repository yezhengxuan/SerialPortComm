using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DataSource;

namespace Display
{
    public partial class Form1 : Form
    {
        private bool leave = false;
        private bool pause = false;
        private int SIZE;
        private List<int> DataList;
        private Point[] DataPoint;
        const int SPACE = 10;

        public Form1()
        {
            SIZE = 13 * 2 - 1;
            DataList = new List<int>();
            DataPoint = new Point[SIZE];

            InitializeComponent();
            PictureBox.CheckForIllegalCrossThreadCalls = false;
        }

        public void RefreshPoint()
        {
            int data;

            while (true)
            {
                // 关闭窗体时结束线程
                if (leave == true)
                {
                    break;
                }
                
                // 按钮控制运行或暂停
                if (pause == false)
                {
                    // 获取最新的数据添加到链表中
                    data = RXData.ReadData();
                    DataList.Add(data);

                    if (DataList.Count > SIZE)
                    {
                        DataList.RemoveAt(0);
                    }

                    // 由链表生成点集
                    if (DataList.Count != 0)
                    {
                        DataPoint = new Point[DataList.Count];
                    }

                    for (int i = 0; i < DataList.Count; i++)
                    {
                        // 描点 (x,y)
                        DataPoint[i] = new Point(i * 10, pictureBox1.Height - DataList[i] - SPACE);
                    }

                    // 刷新显示
                    pictureBox1.Refresh();

                    // 显示百分比
                    if (DataList.Count > 0)
                    {
                        label1.Text = DataList[DataList.Count - 1].ToString() + "%";
                    }
                }

                Thread.Sleep(400);
            }
        }

        private Pen greenPen = new Pen(Color.YellowGreen, 1);

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 平行x轴
            for (int locx = 0; locx < pictureBox1.Height; locx += 20)
            {
                g.DrawLine(new Pen(Color.Green), new Point(0, locx), new Point(pictureBox1.Width, locx));
            }

            // 平行y轴
            for (int locy = 0; locy < pictureBox1.Width; locy += 20)
            {
                g.DrawLine(new Pen(Color.Green), new Point(locy, 0), new Point(locy, pictureBox1.Height));
            }

            // 连点成线
            if (DataList.Count > 1)
            {
                e.Graphics.DrawCurve(greenPen, DataPoint);
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pause == true)
            {
                pause = false;
            }
            else
            {
                pause = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            leave = true;
        }
    }
}
