using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using xenSTAT;


namespace xenDLLSample1
{

	public partial class Form1 : Form
	{
		//创建设备对象
		xenSTAT.IOContrl myDll = new IOContrl();
		
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//取得串口号
			tbcomname.Text = myDll.comName;
            if (myDll.isOpen)
            {
				MessageBox.Show("设备连接成功！");
            }
            else
            {
				MessageBox.Show("设备连接失败！！");
            }
			//加入回调函数
			myDll.myEvent += MyRelProess;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//缺省参数设置
			bool rel = false;
			rel = myDll.SetDefaultCVParm();
			if (rel)
			{
				MessageBox.Show("设置成功");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int a1 = int.Parse(tb1.Text);
			int a2 = int.Parse(tb2.Text);
			int a3 = int.Parse(tb3.Text);
			int a4 = int.Parse(tb4.Text);
			int a5 = int.Parse(tb5.Text);
			int a6 = int.Parse(tb6.Text);
			int a7 = int.Parse(tb7.Text);

			//设置参数
			bool rel = false;
			rel = myDll.SetCVParameters(a1, a2, a3, a4, a5, a6, a7);
			if (rel)
			{
				MessageBox.Show("设置成功");
			}
		}

		//自定义的回调函数，处理结果，运行测试函数时作为参数传入：runCV（MyRelProess）
		private void MyRelProess(Object obj, EventArgs e)
		{
			//先判断是否结束
			if (myDll.isTestEnd)
			{
				MessageBox.Show("测试结束");
			}
			else
			{
				//处理结果，每次串口接收数据保存在DLL的returnValue属性数组中，
				Invoke((EventHandler)(delegate
				{
					double[] relList = myDll.returnValue;
					rtb1.AppendText(relList[0].ToString() + " " + relList[1].ToString() + "\n");

				}));

			}
		}
		private void button3_Click(object sender, EventArgs e)
		{
			//运行测试
			myDll.RunCV();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			//取得缺省参数
			int[] a1 = myDll.defaultCVParm;
			tb1.Text = a1[0].ToString();
			tb2.Text = a1[1].ToString();
			tb3.Text = a1[2].ToString();
			tb4.Text = a1[3].ToString();
			tb5.Text = a1[4].ToString();
			tb6.Text = a1[5].ToString();
			tb7.Text = a1[6].ToString();
		}
	}
}