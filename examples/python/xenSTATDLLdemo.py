
import time
import clr
clr.AddReference('../../lib/xenSTAT')
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

