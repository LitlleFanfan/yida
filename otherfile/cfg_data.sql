
create table LableCode(
SequenceNo Integer identity PRIMARY KEY,--
LCode varchar(64) not null unique,
ToLocation varchar(64) null,
PanelNo varchar(64) null,
Status int not null default 0,
Floor int null,
FloorIndex int null,
Diameter NUMERIC not null default 0,
Length NUMERIC not null default 0,
Coordinates varchar(64) null,
Cx NUMERIC not null default 0,
Cy NUMERIC not null default 0,
Cz NUMERIC not null default 0,
Crz NUMERIC not null default 0,
GetOutLCode varchar(64) null,
CreateDate datetime not null default getdate(),
UpdateDate datetime not null default getdate(),
Remark varchar(256)
);

