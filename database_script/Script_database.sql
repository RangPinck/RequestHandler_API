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
values ('Администратор'),
('Мастер'),
('Руководитель'),
('Пользователь');
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
insert into [User] ([user_id],[login],[password],[surname],[name],[role])
values
--password without hexed for checking work
--12345
('38C13A21-ABE4-40C3-9877-33126F386E7B','Ivan_Kuznetsov','5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5','Кузнецов','Иван',1),
--67891
('86716A63-8093-4706-A975-0046FBEC60F8','Maria_Ivanova','44011a51f70a067c690dfb9959cdab5d7c37a28044f7604f047fd9dafb45cd02','Иванова','Мария',2),
--01112
('28ABB33E-D9AF-449E-AD72-C3510935612B','Peter_Sokolov','3d60c92d2c4610ac1245238497b7902e66eef0ec669d2907ddeaa7e40254df41','Соколов','Пётр',3),
--13141
('23CE0334-8083-412F-AEE6-1F3FF7B1DD04','Olga_Vasilieva','986158efae5d9b5106d797a9f7bb4a990c1ddcbb9460de3259241b798d37d0b9','Васильева','Ольга',4),
--51617
('1DCA5D23-E9CD-48F2-A343-F0F58644AD6C','Jacob_Sidorov','54037ac535a79aab88e96fa2321265456b637e9c679f5f8ed1d7962bd805cad1','Сидоров','Яков',4);
go
create table [Status](
	[status_id] int identity constraint pk_status primary key,
	[title] nvarchar(50) not null constraint un_status_title unique
);
go
insert into [Status] ([title])
values ('Рассматривается'),
('Рассмотрена'),
('Исправлена')
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
insert into [Appointments] ([appointment_id], [problem], [discription_problem], [place], [status],[date_create], [date_approve], [date_fix])
values 
('D0451E80-5123-41F1-9BDE-E9D51E646C52','Установка ПО', 'Пожалуйста, установите такие прогрммы как Visual Studio, Git Bash и 1C.', 'Отдел управления цехами', 2,'','', null),
('22F8DD73-D48C-441D-BFE0-2D488947E111', 'Сломался телефон',null,'Бухгалтерия, 4-тое здание, 11 этаж, 10 кабинет', 1,getdate(),null, null),
('31E53662-E7AB-42CF-BD7D-48B2717152AA','Компьютер не видит принтер',null,'Бухгалтерия, 4-тое здание, 7 этаж, 1 кабинет', 3, '2024-06-01T09:00:00','2024-06-02T09:00:00', '2024-06-03T09:00:00'),
('FE1AC267-8732-484B-BE2D-CD7566A4E43B','Протёк куллер',null,'Бухгалтерия, 4-тое здание, 7 этаж, 1 кабинет', 1, getdate(),null, null),
('C2B5F9CB-F033-4004-8862-84C34AB13501','установка ПО','Пожалуйста, установите 1C.','Бухгалтерия, 4-тое здание, 7 этаж, 1 кабинет', 3, '2024-06-03T09:00:00','2024-06-04T09:00:00', '2024-06-05T09:00:00')
go
create table [User_appointment](
	[appointment] uniqueidentifier not null constraint fk_appointment_to_User_appointment foreign key
	references [Appointments]([appointment_id]) on delete cascade,
	[user] uniqueidentifier not null constraint fk_user_to_User_appointment foreign key
	references [User]([user_id]) on delete cascade
);
go
insert into [User_appointment] ([appointment], [user]) values
('22F8DD73-D48C-441D-BFE0-2D488947E111','1DCA5D23-E9CD-48F2-A343-F0F58644AD6C'),
('31E53662-E7AB-42CF-BD7D-48B2717152AA','86716A63-8093-4706-A975-0046FBEC60F8'),
('C2B5F9CB-F033-4004-8862-84C34AB13501','23CE0334-8083-412F-AEE6-1F3FF7B1DD04'),
('FE1AC267-8732-484B-BE2D-CD7566A4E43B','28ABB33E-D9AF-449E-AD72-C3510935612B'),
('D0451E80-5123-41F1-9BDE-E9D51E646C52','1DCA5D23-E9CD-48F2-A343-F0F58644AD6C')
go
--если заявка пустая, то документ от предприятия
create table [Documents](
	[document_id] uniqueidentifier not null constraint pk_document primary key constraint def_document_guid default newid(),
	[title] nvarchar(50) not null constraint un_document_title unique,
	[appointment] uniqueidentifier constraint fk_appointment_to_document foreign key
	references [Appointments]([appointment_id]) on delete cascade
);
insert into [Documents] ([title], [appointment]) values
('Накладная.txt', 'D0451E80-5123-41F1-9BDE-E9D51E646C52'),
('Схема.txt', '31E53662-E7AB-42CF-BD7D-48B2717152AA'),
('Фото.txt', 'FE1AC267-8732-484B-BE2D-CD7566A4E43B'),
('Список_ip.txt', '22F8DD73-D48C-441D-BFE0-2D488947E111'),
('fi.txt', null)

--select * from [Roles];
--select * from [User] order by [role];
--select * from [Status] order by [status_id];
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