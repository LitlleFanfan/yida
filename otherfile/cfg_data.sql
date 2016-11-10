
TRUNCATE  TABLE  OPCParam

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('0','ScanState','MicroWin.S7-1200.NewItem1','Scan','采集处与PLC数据交换状态(读到0开始写，写完写1）')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('1','Diameter','MicroWin.S7-1200.NewItem10','Scan','直径')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('2','Length','MicroWin.S7-1200.NewItem11','Scan','长度')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('3','ToLocationArea','MicroWin.S7-1200.NewItem12','Scan','交地区')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('4','ToLocationNo','MicroWin.S7-1200.NewItem14','Scan','交地编号')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('5','ScanLable1','MicroWin.S7-1200.NewItem21','Scan','采集到的标签号01')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('6','ScanLable2','MicroWin.S7-1200.NewItem22','Scan','采集到的标签号01')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('9','CameraNo','MicroWin.S7-1200.NewItem15','Scan','相机编号（几号相机采集到的数据，0是手动采集）')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('11','GetWeigh','MicroWin.S7-1200.NewItem16','Scan','称重(1称重，称完置0，称重失败2）')


INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('31','BeforCacheStatus','MicroWin.S7-1200.NewItem108','Cache','缓存前标签（读完写好结果后置空）')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('32','BeforCacheLable1','MicroWin.S7-1200.NewItem44','Cache','缓存前标签（读完写好结果后置空）')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('33','BeforCacheLable2','MicroWin.S7-1200.NewItem45','Cache','缓存前标签')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('34','IsCache','MicroWin.S7-1200.NewItem50','Cache','是否缓存（1缓存，0不缓存）')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('35','GetOutLable1','MicroWin.S7-1200.NewItem46','Cache','从缓存区取出的标签')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('36','GetOutLable2','MicroWin.S7-1200.NewItem47','Cache','从缓存区取出的标签')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('37','IsLastOfPanel','MicroWin.S7-1200.NewItem20','Cache','是否是最后一个标签（1是最后一个，0不是）')


INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('41','Signal','MicroWin.S7-1200.NewItem109','RobotCarryA','开关信号（PC读到0读标签，读完写1)');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('42','LCode1','MicroWin.S7-1200.NewItem51','RobotCarryA','机器人处标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('43','LCode2','MicroWin.S7-1200.NewItem52','RobotCarryA','机器人处标签2');


INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('51','Signal','MicroWin.S7-1200.NewItem110','RobotCarryB','开关信号（PC读到0读标签，读完写1)');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('52','LCode1','MicroWin.S7-1200.NewItem53','RobotCarryB','机器人处标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('53','LCode2','MicroWin.S7-1200.NewItem54','RobotCarryB','机器人处标签2');


INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('61','Signal','MicroWin.S7-1200.NewItem111','DeleteLCode','删除布卷开关信号');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('62','LCode1','MicroWin.S7-1200.NewItem27','DeleteLCode','删除布卷标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('63','LCode2','MicroWin.S7-1200.NewItem28','DeleteLCode','删除布卷标签2');


INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('72','RobotWorkState','MicroWin.S7-1200.NewItem70','None','工作状态（运行：１；空闲：０；）')
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('73','RobotRunState','MicroWin.S7-1200.NewItem72','None','运行状态是否在安全位置（安全：１；危险：０；）')

--
-- 2016-11-8
-- A区-C区完成信号
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('12','Signal','MicroWin.S7-1200.NewItem3','AArea1','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('13','LCode1','MicroWin.S7-1200.NewItem134','AArea1','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('14','LCode2','MicroWin.S7-1200.NewItem135','AArea1','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('84','Signal','MicroWin.S7-1200.NewItem4','AArea2','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('85','LCode1','MicroWin.S7-1200.NewItem136','AArea2','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('86','LCode2','MicroWin.S7-1200.NewItem137','AArea2','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('87','Signal','MicroWin.S7-1200.NewItem5','AArea3','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('88','LCode1','MicroWin.S7-1200.NewItem138','AArea3','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('89','LCode2','MicroWin.S7-1200.NewItem139','AArea3','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('80','Signal','MicroWin.S7-1200.NewItem6','AArea4','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('81','LCode1','MicroWin.S7-1200.NewItem140','AArea4','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('82','LCode2','MicroWin.S7-1200.NewItem141','AArea4','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','Signal','MicroWin.S7-1200.NewItem7','AArea5','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('84','LCode1','MicroWin.S7-1200.NewItem142','AArea5','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('85','LCode2','MicroWin.S7-1200.NewItem143','AArea5','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('86','Signal','MicroWin.S7-1200.NewItem8','AArea6','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('87','LCode1','MicroWin.S7-1200.NewItem144','AArea6','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('88','LCode2','MicroWin.S7-1200.NewItem145','AArea6','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('89','Signal','MicroWin.S7-1200.NewItem9','AArea7','A1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('80','LCode1','MicroWin.S7-1200.NewItem146','AArea7','A1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('81','LCode2','MicroWin.S7-1200.NewItem147','AArea7','A1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('82','Signal','MicroWin.S7-1200.NewItem31','CArea1','C1板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','LCode1','MicroWin.S7-1200.NewItem148','CArea1','C1板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('84','LCode2','MicroWin.S7-1200.NewItem149','CArea1','C1板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('85','Signal','MicroWin.S7-1200.NewItem32','CArea2','C2板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('86','LCode1','MicroWin.S7-1200.NewItem150','CArea2','C2板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('87','LCode2','MicroWin.S7-1200.NewItem151','CArea2','C2板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('88','Signal','MicroWin.S7-1200.NewItem33','CArea3','C3板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('89','LCode1','MicroWin.S7-1200.NewItem152','CArea3','C3板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('80','LCode2','MicroWin.S7-1200.NewItem153','CArea3','C3板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('81','Signal','MicroWin.S7-1200.NewItem34','CArea4','C4板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('82','LCode1','MicroWin.S7-1200.NewItem154','CArea4','C4板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','LCode2','MicroWin.S7-1200.NewItem155','CArea4','C4板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('84','Signal','MicroWin.S7-1200.NewItem35','CArea5','C5板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('85','LCode1','MicroWin.S7-1200.NewItem156','CArea5','C5板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('86','LCode2','MicroWin.S7-1200.NewItem157','CArea5','C5板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('87','Signal','MicroWin.S7-1200.NewItem36','CArea6','C6板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('88','LCode1','MicroWin.S7-1200.NewItem158','CArea6','C6板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('89','LCode2','MicroWin.S7-1200.NewItem159','CArea6','C6板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('80','Signal','MicroWin.S7-1200.NewItem37','CArea7','C7板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('81','LCode1','MicroWin.S7-1200.NewItem160','CArea7','C7板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('82','LCode2','MicroWin.S7-1200.NewItem161','CArea7','C7板标签2');

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','Signal','MicroWin.S7-1200.NewItem38','CArea8','C8板标签信息开关量');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('84','LCode1','MicroWin.S7-1200.NewItem162','CArea8','C8板标签1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('85','LCode2','MicroWin.S7-1200.NewItem163','CArea8','C8板标签2');
