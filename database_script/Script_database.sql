create database [RequestHandler];
go
use [RequestHandler];
go
create table [Roles](
	[role_id] int identity constraint pk_role primary key,
	[title] nvarchar(50) not null constraint un_role_title unique
);
go
insert into [Roles] (title)
values ('�������������'),
('������'),
('������������'),
('������������');
go
create table [User](
	[user_id]  uniqueidentifier not null constraint pk_user primary key constraint def_user_guid default newid(),
	[login] nvarchar(50) not null constraint un_user_login unique,
	[password] nvarchar(max) not null,
	[surname] nvarchar(50) not null,
	[name] nvarchar(50) not null,
	[role] int not null constraint fk_role_to_user foreign key
	references [Roles]([role_id]) on delete cascade
	constraint chk_user_role check ([role] >= 1 and [role] <= 4)
);
go
insert into [User] ([login],[password],[surname],[name],[role])
values
--password without hexed for checking work
--12345
('Ivan_Kuznetsov','5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5','��������','����',1),
--67891
('Maria_Ivanova','44011a51f70a067c690dfb9959cdab5d7c37a28044f7604f047fd9dafb45cd02','�������','�����',2),
--01112
('Peter_Sokolov','3d60c92d2c4610ac1245238497b7902e66eef0ec669d2907ddeaa7e40254df41','�������','ϸ��',3),
--13141
('Olga_Vasilieva','986158efae5d9b5106d797a9f7bb4a990c1ddcbb9460de3259241b798d37d0b9','���������','�����',4),
--51617
('Jacob_Sidorov','54037ac535a79aab88e96fa2321265456b637e9c679f5f8ed1d7962bd805cad1','�������','����',4);
go
create table [Status](
	[status_id] int identity constraint pk_status primary key,
	[title] nvarchar(50) not null constraint un_status_title unique
);
go
insert into [Status] ([title])
values ('���������������'),
('�����������'),
('����������')
go
create table [Appointments](
	[appointment_id] uniqueidentifier not null constraint pk_appintment primary key default newid(),
	[problem] nvarchar(50) not null,
	[discription_problem] nvarchar(max),
	[place] nvarchar(max) not null,
	[date_create] datetime not null constraint def_appointment_date_create default getdate(),
	[date_approve] datetime constraint chk_appointment_date_approve check ([date_approve] <= getdate()),
	[date_fix] datetime constraint chk_appointment_date_fix check ([date_fix] < getdate()),
	[status] int not null constraint fk_status_to_appintment foreign key
	references [Status]([status_id]) on delete cascade 
	constraint def_appointment_status default 1
	constraint chk_appointment_status check ([status] >= 1 and [status] <= 3),
);
go
insert into [Appointments] ([problem], [discription_problem], [place], [status],[date_create], [date_approve], [date_fix])
values 
('��������� ��', '����������, ���������� ����� �������� ��� Visual Studio, Git Bash � 1C.', '����� ���������� ������', 2,'','', null),
('�������� �������',null,'�����������, 4-��� ������, 11 ����, 10 �������', 1,getdate(),null, null),
('��������� �� ����� �������',null,'�����������, 4-��� ������, 7 ����, 1 �������', 3, '2024-06-01T09:00:00','2024-06-02T09:00:00', '2024-06-03T09:00:00'),
('����� ������',null,'�����������, 4-��� ������, 7 ����, 1 �������', 1, getdate(),null, null),
('��������� ��','����������, ���������� 1C.','�����������, 4-��� ������, 7 ����, 1 �������', 3, '2024-06-03T09:00:00','2024-06-04T09:00:00', '2024-06-05T09:00:00')
go
create table [User_appointment](
	[appointment] uniqueidentifier not null constraint fk_appointment_to_User_appointment foreign key
	references [Appointments]([appointment_id]) on delete cascade,
	[user] uniqueidentifier not null constraint fk_user_to_User_appointment foreign key
	references [User]([user_id]) on delete cascade
);
insert into [User_appointment] ([appointment], [user]) values
('103C043B-3EB7-4A97-9E71-4E8FFBB081A8','26F31915-38CD-46EA-9E01-462CC0C59E34'),
('69D03AE9-6A0E-4990-93B9-6919322EDC16','2C94A801-74C6-4CFB-B431-775C71430DB6'),
('587DA282-8321-4DBE-93C4-A5913B0595BF','4B830D93-238E-4DB9-B840-77AD46EB6DFA'),
('50A4190A-4580-4BC1-B77A-C278DED96D58','1CC3052F-938A-4F0F-94B0-8054DEFB60E4'),
('25FCD773-14FC-4DC4-BFA8-C9F735A44432','26F31915-38CD-46EA-9E01-462CC0C59E34')
go
--���� ������ ������, �� �������� �� �����������
create table [Documents](
	[document_id] uniqueidentifier not null constraint pk_document primary key constraint def_document_guid default newid(),
	[title] nvarchar(50) not null constraint un_document_title unique,
	[appointment] uniqueidentifier constraint fk_appointment_to_document foreign key
	references [Appointments]([appointment_id]) on delete cascade
);
insert into [Documents] ([title], [appointment]) values
('���������.txt', '587DA282-8321-4DBE-93C4-A5913B0595BF'),
('�����.txt', '103C043B-3EB7-4A97-9E71-4E8FFBB081A8'),
('����.txt', '69D03AE9-6A0E-4990-93B9-6919322EDC16'),
('������_ip.txt', '25FCD773-14FC-4DC4-BFA8-C9F735A44432'),
('fi.txt', null)

--select * from [Roles];
--select * from [User];
--select * from [Status];
--select * from [Documents];
--select * from [Appointments];
--select * from [User_appointment];

--drop table [User_appointment];
--drop table [Documents];
--drop table [Appointments];
--drop table [User];
--drop table [Roles];
--drop table [Status];

--use tempdb;
--drop database RequestHandler;