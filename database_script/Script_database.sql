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
create table [Documents](
	[document_id] uniqueidentifier not null constraint pk_document primary key constraint def_document_guid default newid(),
	[title] nvarchar(50) not null constraint un_document_title unique,
	[path] nvarchar(max) not null
);
go

go
create table [Appointments](
	[appintment_id] uniqueidentifier not null constraint pk_appintment primary key default newid(),
	[problem] nvarchar(50) not null,
	[discription_problem] nvarchar(max),
	[place] nvarchar(max) not null,
	[date_create] datetime not null default getdate(),
	[date_approve] datetime,
	[date_fix] datetime,
	[document] nvarchar(max),
	[status] int not null constraint fk_status_to_appintment foreign key
	references [Status]([status_id]) on delete cascade constraint def_appointment_status default 1,
	[user] uniqueidentifier not null constraint fk_user_to_appintment foreign key
	references [User]([user_id]),
	[approval] uniqueidentifier constraint fk_approval_to_appintment foreign key
	references [User]([user_id]),
	[master] uniqueidentifier constraint fk_master_to_appintment foreign key
	references [User]([user_id])
);

--select * from [Roles];
--select * from [User];
--select * from [Status];
--select * from [Documents];
--select * from [Appointments];

--drop table [Roles];
--drop table [User];
--drop table [Status];
--drop table [Documents];
--drop table [Appointments];

--use master;
--drop database RequestHandler;