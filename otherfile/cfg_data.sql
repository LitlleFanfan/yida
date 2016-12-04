
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


--B区板完成信号

INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B01','MicroWin.S7-1200.NewItem86','BAreaPanelFinish','板完成信号Ｂ区板号1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B02','MicroWin.S7-1200.NewItem87','BAreaPanelFinish','板完成信号Ｂ区板号2');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B03','MicroWin.S7-1200.NewItem88','BAreaPanelFinish','板完成信号Ｂ区板号3');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B04','MicroWin.S7-1200.NewItem89','BAreaPanelFinish','板完成信号Ｂ区板号4');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B05','MicroWin.S7-1200.NewItem90','BAreaPanelFinish','板完成信号Ｂ区板号5');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B06','MicroWin.S7-1200.NewItem91','BAreaPanelFinish','板完成信号Ｂ区板号6');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B07','MicroWin.S7-1200.NewItem92','BAreaPanelFinish','板完成信号Ｂ区板号7');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B08','MicroWin.S7-1200.NewItem93','BAreaPanelFinish','板完成信号Ｂ区板号8');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B09','MicroWin.S7-1200.NewItem94','BAreaPanelFinish','板完成信号Ｂ区板号9');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B10','MicroWin.S7-1200.NewItem95','BAreaPanelFinish','板完成信号Ｂ区板号10');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B11','MicroWin.S7-1200.NewItem96','BAreaPanelFinish','板完成信号Ｂ区板号11');

--B区半析完成信号
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B01','MicroWin.S7-1200.NewItem97','BAreaFloorFinish','半板完成信号Ｂ区板号1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B02','MicroWin.S7-1200.NewItem98','BAreaFloorFinish','半板完成信号Ｂ区板号2');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B03','MicroWin.S7-1200.NewItem99','BAreaFloorFinish','半板完成信号Ｂ区板号3');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B04','MicroWin.S7-1200.NewItem100','BAreaFloorFinish','半板完成信号Ｂ区板号4');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B05','MicroWin.S7-1200.NewItem101','BAreaFloorFinish','半板完成信号Ｂ区板号5');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B06','MicroWin.S7-1200.NewItem102','BAreaFloorFinish','半板完成信号Ｂ区板号6');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B07','MicroWin.S7-1200.NewItem103','BAreaFloorFinish','半板完成信号Ｂ区板号7');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B08','MicroWin.S7-1200.NewItem104','BAreaFloorFinish','半板完成信号Ｂ区板号8');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B09','MicroWin.S7-1200.NewItem105','BAreaFloorFinish','半板完成信号Ｂ区板号9');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B10','MicroWin.S7-1200.NewItem106','BAreaFloorFinish','半板完成信号Ｂ区板号10');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B11','MicroWin.S7-1200.NewItem107','BAreaFloorFinish','半板完成信号Ｂ区板号11');

--B区板状态信号
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B01','MicroWin.S7-1200.NewItem55','BAreaPanelState','板状态Ｂ区板号1');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B02','MicroWin.S7-1200.NewItem56','BAreaPanelState','板状态Ｂ区板号2');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B03','MicroWin.S7-1200.NewItem57','BAreaPanelState','板状态Ｂ区板号3');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B04','MicroWin.S7-1200.NewItem58','BAreaPanelState','板状态Ｂ区板号4');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B05','MicroWin.S7-1200.NewItem59','BAreaPanelState','板状态Ｂ区板号5');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B06','MicroWin.S7-1200.NewItem60','BAreaPanelState','板状态Ｂ区板号6');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B07','MicroWin.S7-1200.NewItem61','BAreaPanelState','板状态Ｂ区板号7');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B08','MicroWin.S7-1200.NewItem62','BAreaPanelState','板状态Ｂ区板号8');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B09','MicroWin.S7-1200.NewItem63','BAreaPanelState','板状态Ｂ区板号9');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B10','MicroWin.S7-1200.NewItem64','BAreaPanelState','板状态Ｂ区板号10');
INSERT OPCParam(IndexNo,Name,Code,Class,Remark) VALUES('83','B11','MicroWin.S7-1200.NewItem65','BAreaPanelState','板状态Ｂ区板号11');



TRUNCATE  TABLE  robotparam
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('1','0','-2343.578','0.0000','0.0000','-90.7961',convert(datetime,'2016-09-12 15:37:32.843',121),convert(datetime,'2016-09-12 15:37:32.843',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('1','1','-3066.760','0.0000','0.0000','90.1883',convert(datetime,'2016-09-12 15:37:35.600',121),convert(datetime,'2016-09-12 15:37:35.600',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('1','2','-3066.760','0.0000','0.0000','-0.4445',convert(datetime,'2016-09-12 15:37:37.800',121),convert(datetime,'2016-09-12 15:37:37.800',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('1','3','-3066.760','0.0000','0.0000','-179.6980',convert(datetime,'2016-09-12 15:37:40.030',121),convert(datetime,'2016-09-12 15:37:40.030',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('2','0','-4700.722','0.0000','0.0000','-89.6211',convert(datetime,'2016-09-12 15:37:43.570',121),convert(datetime,'2016-09-12 15:37:43.570',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('2','1','-5259.977','0.0000','0.0000','90.1725',convert(datetime,'2016-09-12 15:37:46.010',121),convert(datetime,'2016-09-12 15:37:46.010',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('2','2','-5259.977','0.0000','0.0000','2.2788',convert(datetime,'2016-09-12 15:37:48.147',121),convert(datetime,'2016-09-12 15:37:48.147',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('2','3','-5259.977','0.0000','0.0000','-178.4619',convert(datetime,'2016-09-12 15:37:51.290',121),convert(datetime,'2016-09-12 15:37:51.290',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('3','0','-3946.686','179.9994','0.0000','91.5774',convert(datetime,'2016-09-12 15:20:43.883',121),convert(datetime,'2016-09-12 15:20:43.883',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('3','1','-4724.895','-179.9994','0.0000','-85.0841',convert(datetime,'2016-09-12 15:23:14.297',121),convert(datetime,'2016-09-12 15:23:14.297',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('3','2','-4724.895','18.0000','0.0000','0.1253',convert(datetime,'2016-09-12 15:24:08.067',121),convert(datetime,'2016-09-12 15:24:08.067',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('3','3','-4724.895','18.0000','0.0000','179.9832',convert(datetime,'2016-09-12 15:24:36.817',121),convert(datetime,'2016-09-12 15:24:36.817',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('4','0','288.790','179.9996','0.0000','91.2165',convert(datetime,'2016-09-12 15:25:38.033',121),convert(datetime,'2016-09-12 15:25:38.033',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('4','1','-442.594','-179.9996','0.0000','-89.6258',convert(datetime,'2016-09-12 15:27:50.117',121),convert(datetime,'2016-09-12 15:27:50.117',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('4','2','-442.594','18.0000','0.0000','0.4404',convert(datetime,'2016-09-12 15:28:53.187',121),convert(datetime,'2016-09-12 15:28:53.187',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('4','3','-442.594','18.0000','0.0000','-179.3984',convert(datetime,'2016-09-12 15:29:16.810',121),convert(datetime,'2016-09-12 15:29:16.810',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('5','0','288.790','179.9998','0.0000','90.2642',convert(datetime,'2016-09-12 15:32:56.643',121),convert(datetime,'2016-09-12 15:32:56.643',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('5','1','-442.594','-179.9998','0.0000','-89.5851',convert(datetime,'2016-09-12 15:33:55.913',121),convert(datetime,'2016-09-12 15:33:55.913',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('5','2','-442.594','18.0000','0.0000','-0.5654',convert(datetime,'2016-09-12 15:34:34.300',121),convert(datetime,'2016-09-12 15:34:34.300',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('5','3','-442.594','18.0000','0.0000','179.5748',convert(datetime,'2016-09-12 15:35:02.233',121),convert(datetime,'2016-09-12 15:35:02.233',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('6','0','2494.432','179.9993','0.0000','89.9853',convert(datetime,'2016-09-12 15:35:41.067',121),convert(datetime,'2016-09-12 15:35:41.067',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('6','1','1747.652','-179.9993','0.0000','-89.5342',convert(datetime,'2016-09-12 15:35:46.553',121),convert(datetime,'2016-09-12 15:35:46.553',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('6','2','1747.652','-179.9998','0.0000','0.1979',convert(datetime,'2016-09-12 15:35:50.203',121),convert(datetime,'2016-09-12 15:35:50.203',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('6','3','1747.652','179.9998','0.0000','-179.5658',convert(datetime,'2016-09-12 15:35:57.177',121),convert(datetime,'2016-09-12 15:35:57.177',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('7','0','4541.974','179.9995','0.0000','90.3211',convert(datetime,'2016-09-12 15:36:02.307',121),convert(datetime,'2016-09-12 15:36:02.307',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('7','1','3940.736','-179.9995','0.0000','-89.5343',convert(datetime,'2016-09-12 15:36:04.970',121),convert(datetime,'2016-09-12 15:36:04.970',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('7','2','3940.736','18.0000','0.0000','0.6502',convert(datetime,'2016-09-12 15:36:08.643',121),convert(datetime,'2016-09-12 15:36:08.643',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('7','3','3940.736','18.0000','0.0000','-179.2040',convert(datetime,'2016-09-12 15:36:12.107',121),convert(datetime,'2016-09-12 15:36:12.107',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('8','0','3495.480','0.0000','0.0000','89.9430',convert(datetime,'2016-09-12 15:36:15.540',121),convert(datetime,'2016-09-12 15:36:15.540',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('8','1','3940.766','0.0000','0.0000','-89.5281',convert(datetime,'2016-09-12 15:36:19.423',121),convert(datetime,'2016-09-12 15:36:19.423',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('8','2','3940.766','0.0000','0.0000','0.7400',convert(datetime,'2016-09-12 15:36:23.987',121),convert(datetime,'2016-09-12 15:36:23.987',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('8','3','3940.766','0.0000','0.0000','-179.1645',convert(datetime,'2016-09-12 15:36:30.593',121),convert(datetime,'2016-09-12 15:36:30.593',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('9','0','4300.842','179.9997','0.0000','-89.1357',convert(datetime,'2016-09-12 15:36:38.770',121),convert(datetime,'2016-09-12 15:36:38.770',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('9','1','4682.171','-179.9997','0.0000','90.1174',convert(datetime,'2016-09-12 15:36:42.060',121),convert(datetime,'2016-09-12 15:36:42.060',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('9','2','4682.171','179.9999','0.0000','-1.1160',convert(datetime,'2016-09-12 15:36:44.943',121),convert(datetime,'2016-09-12 15:36:44.943',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('9','3','4682.171','-179.9999','0.0000','-179.0131',convert(datetime,'2016-09-12 15:36:49.270',121),convert(datetime,'2016-09-12 15:36:49.270',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('10','0','2090.708','-178.3657','0.0000','-88.9428',convert(datetime,'2016-09-12 15:36:56.187',121),convert(datetime,'2016-09-12 15:36:56.187',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('10','1','2596.232','178.3654','0.0000','90.1250',convert(datetime,'2016-09-12 15:37:00.200',121),convert(datetime,'2016-09-12 15:37:00.200',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('10','2','2596.232','179.7402','0.0000','-1.3532',convert(datetime,'2016-09-12 15:37:05.233',121),convert(datetime,'2016-09-12 15:37:05.233',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('10','3','2596.232','179.7500','0.0000','-178.7767',convert(datetime,'2016-09-12 15:37:08.593',121),convert(datetime,'2016-09-12 15:37:08.593',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('11','0','-370.168','179.9999','0.0000','-88.4146',convert(datetime,'2016-09-12 15:37:13.087',121),convert(datetime,'2016-09-12 15:37:13.087',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('11','1','367.342','-179.9999','0.0000','90.1150',convert(datetime,'2016-09-12 15:37:16.280',121),convert(datetime,'2016-09-12 15:37:16.280',121),'Base')
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('11','2','367.342','179.9999','0.0000','0.0510',convert(datetime,'2016-09-12 15:37:18.870',121),convert(datetime,'2016-09-12 15:37:18.870',121),NULL)
INSERT ROBOTPARAM(PanelIndexNo,BaseIndex,Base,Rx,Ry,Rz,CreateDate,UpdateDate,Remark) VALUES('11','3','367.342','-179.9999','0.0000','179.5699',convert(datetime,'2016-09-12 15:37:21.457',121),convert(datetime,'2016-09-12 15:37:21.457',121),NULL)