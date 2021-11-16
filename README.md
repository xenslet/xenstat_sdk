## xenSTAT.dll
基于 C#的SDK包 ，可使用C#或Python语言为 xenSTAT仪器做 二次开发
![image](https://github.com/xenslet/xenstat_sdk/blob/feab0b943a90bd00759e9ec432a1353c363d0f05/imgforreadme/xenstat_github.png)
### xenSTAT SDK 说明：
**xenSTAT SDK:**

​	1 基本工具
​	2 运行方法
​	3 参数设置
​	4 示例程序
​	5 修改记录

1. **基本工具(属性)：**

   1.1. public string comName
   	获取串口号
   	返回：串口号，如“COM1"

   1.2. public string comDes
   	获取描述（名称）
   	返回：串口设备描述

   1.3. public bool isOpen
   	判断设备是否连接

   1.4. public double[] returnValue	
   	测试数据（单次）
   	注意：回调函数中使用，取得仪器发来的测试数据（单次）

   1.5. public int[] defaultCVParm
   	获取CV缺省参数
   	参数（7个）数组

   1.6. public int[] defaultSWVParm
   	获取SWV缺省参数
   	参数（7个）数组

   1.7. public int[] defaultITParm
   	获取IT缺省参数
   	参数（4个）数组

   1.8. public int[] defaultDPVParm
   	获取DPV缺省参数
   	参数（8个）数组

   1.9 public bool isTestEnd
   	测试是否结束
   	在用户回调函数中使用

   2.0 public event EventHandler myEvent;
   	设定用户回调函数



2. **运行方法：**

   2.1 public void RunCV()
      	运行CV测试

   2.2 public void RunSWV()
      	运行SWV测试

   2.3 public void RunIT()
      	运行IT测试

   2.4. public void RunDPV()		
      	运行DPV测试



3. **参数设置：**

   3.1. public bool SetCVParameters(int initE, int highR, int lowE, int scanR, int segments, int step, int Rtial)
   	设置CV测试参数
   	参数：CV设置的7个参数
   	返回：true（成功），false(失败)

   3.2. public bool SetSWVParameters(int initE, int finalE, int incrE, int ampltd, int freq, int sampleW, int Rtial)
   	设置SWV测试参数
   	参数：SWV设置的7个参数
   	返回：true（成功），false(失败)

   3.3. public bool SetITParameters(int initE, int runtime, int sampleP, int Rtial)
   	设置IT测试参数
   	参数：IT设置的4个参数
   	返回：true（成功），false(失败)

   3.4. public bool SetDPVParameters(int initE, int finalE, int incrE, int ampltd, int pulseW, int pulseP, int sampleW, int Rtial)
   	设置DPV测试参数
   	参数：DPV设置的8个参数
   	返回：true（成功），false(失败)

   3.5. public bool SetDefaultCVParm()
   	设置CV缺省参数
   	返回：true（成功），false(失败)

   3.6. public bool SetDefaultSWVParm()
   	设置SWV缺省参数
   	返回：true（成功），false(失败)

   3.7. public bool SetDefaultITParm()
   	设置IT缺省参数
   	返回：true（成功），false(失败)

   3.8. public bool SetDefaultDPVParm()
   	设置DPV缺省参数
   	返回：true（成功），false(失败)



4 示例程序：
4.1 C#示例：
```c#
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
			//设定用户自己的回调处理函数
			myDll.MyEvent += MyRelProess;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			//缺省参数设置
			bool rel = false;
			rel = myDll.setDefaultCVParm();
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
```

4.2 Python示例：

```python

	import time
	import clr
	clr.AddReference('xenSTAT')
	from xenSTAT import *

	myDll = IOContrl()
    
	def rf(o,e):
	  if myDll.isTestEnd==1:
		print("测试结束")
	  else:
		print("\t%g\t%g"%(myDll.returnValue[0],myDll.returnValue[1]))

	myDll.myEvent += rf

	print("CV测试：")
	rel = myDll.SetDefaultCVParm()
	if rel:
	  print("\tCV参数设置成功")
	print("\t测试开始：")
	myDll.RunCV()

	while myDll.isTestEnd==0:
	  time.sleep(0.1)
	print()

	print("SWV测试：")
	rel = myDll.SetDefaultSWVParm()
	if rel:
	  print("\tSWV参数设置成功")
	print("\t测试开始：")
	myDll.RunSWV()

	while myDll.isTestEnd==0:
	  time.sleep(0.1)
	print()

	print("IT测试：")
	rel = myDll.SetDefaultITParm()
	if rel:
	  print("\tIT参数设置成功")
	print("\t测试开始：")
	myDll.RunIT()

	while myDll.isTestEnd==0:
	  time.sleep(0.1)
	print()

	print("DPV测试：")
	rel = myDll.SetDefaultDPVParm()
	if rel:
	  print("\tDPV参数设置成功")
	print("\t测试开始：")
	myDll.RunDPV()

	while myDll.isTestEnd==0:
	  time.sleep(0.1)
	print()

	#End

```



5 修改记录：
5.1 2021-11-04 添加适合python调用API
5.2 2021-11-04 修改运行调用API，删除python调用API
5.3 2021-11-10 修改 deaultDPVParm ==> defaultDPVParm
