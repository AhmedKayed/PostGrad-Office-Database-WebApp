CREATE DATABASE PostGradOffice;

go
use PostGradOffice;

CREATE TABLE PostGradUser(
id int primary key identity(1,1),
email varchar(50) not null,
password varchar(30) not null
)
CREATE TABLE Admin(
id int primary key foreign key references PostGradUser on delete cascade on update cascade
)
CREATE TABLE GucianStudent(
id int primary key foreign key references PostGradUser on delete cascade on update cascade,
firstName varchar(20),
lastName varchar(20),
type varchar(3),
faculty varchar(30),
address varchar(50),
GPA decimal(3,2),
undergradID int
)
CREATE TABLE NonGucianStudent(
id int primary key foreign key references PostGradUser on delete cascade on update cascade,
firstName varchar(20),
lastName varchar(20),
type varchar(3),
faculty varchar(30),
address varchar(50),
GPA decimal(3,2),
)
CREATE TABLE GUCStudentPhoneNumber(
id int foreign key references GucianStudent on delete cascade on update cascade,
phone int UNIQUE,
primary key(id,phone)
)
CREATE TABLE NonGUCStudentPhoneNumber(
id int foreign key references NonGucianStudent on delete cascade on update cascade,
phone int UNIQUE,
primary key(id,phone)
)
CREATE TABLE Course(
id int primary key identity(1,1),
fees int,
creditHours int,
code varchar(10)
)
CREATE TABLE Supervisor(
id int primary key foreign key references PostGradUser,
name varchar(20),
faculty varchar(30)
);
CREATE TABLE Examiner(
id int primary key foreign key references PostGradUser on delete cascade on update cascade,
name varchar(20),
fieldOfWork varchar(100),
isNational BIT
)
CREATE TABLE Payment(
id int primary key identity(1,1),
amount decimal(7,2),
noOfInstallments int,
fundPercentage decimal(4,2)
)
CREATE TABLE Thesis(
serialNumber int primary key identity(1,1), 
field varchar(20),
type varchar(3) not null,
title varchar(100) not null,
startDate date not null,
endDate date not null,
defenseDate date,
years as (year(endDate)- year(startDate)),
grade decimal(4,2),
payment_id int foreign key references payment on delete cascade on update cascade,
noOfExtensions int
)
CREATE TABLE Publication(
id int primary key identity(1,1),
title varchar(100) not null,
dateOfPublication date,
place varchar(100),
accepted BIT,
host varchar(100)
);
Create table Defense (serialNumber int,
date datetime,
location varchar(15),
grade decimal(4,2),
primary key (serialNumber, date),
foreign key (serialNumber) references Thesis on delete cascade on update cascade)
Create table GUCianProgressReport (
sid int foreign key references GUCianStudent on delete cascade on update cascade
, no int
, date datetime
, eval int
, state int
, description varchar(200)
, thesisSerialNumber int foreign key references Thesis on delete cascade on update cascade
, supid int foreign key references Supervisor
, primary key (sid, no) )
Create table NonGUCianProgressReport (sid int foreign key references NonGUCianStudent on delete cascade on update cascade,
no int
, date datetime
, eval int
, state int
, description varchar(200)
, thesisSerialNumber int foreign key references Thesis on delete cascade on update cascade
, supid int foreign key references Supervisor
, primary key (sid, no) )
Create table Installment (date datetime,
paymentId int foreign key references Payment on delete cascade on update cascade
, amount decimal(8,2)
, done bit
, primary key (date, paymentId))
Create table NonGucianStudentPayForCourse(sid int foreign key references NonGucianStudent on delete cascade on update cascade,
paymentNo int foreign key references Payment on delete cascade on update cascade,
cid int foreign key references Course on delete cascade on update cascade,
primary key (sid, paymentNo, cid))
Create table NonGucianStudentTakeCourse (sid int foreign key references NonGUCianStudent on delete cascade on update cascade
, cid int foreign key references Course on delete cascade on update cascade
, grade decimal (4,2)
, primary key (sid, cid) )
Create table GUCianStudentRegisterThesis (sid int foreign key references GUCianStudent on delete cascade on update cascade,
supid int foreign key references Supervisor
, serial_no int foreign key references Thesis on delete cascade on update cascade
, primary key(sid, supid, serial_no))
Create table NonGUCianStudentRegisterThesis (sid int foreign key references NonGUCianStudent on delete cascade on update cascade,
supid int foreign key references Supervisor,
serial_no int foreign key references Thesis on delete cascade on update cascade,
primary key (sid, supid, serial_no))
Create table ExaminerEvaluateDefense(date datetime,
serialNo int,
examinerId int foreign key references Examiner on delete cascade on update cascade,
comment varchar(300),
primary key(date, serialNo, examinerId),
foreign key (serialNo, date) references Defense (serialNumber, date) on delete cascade on update cascade)
Create table ThesisHasPublication(serialNo int foreign key references Thesis on delete cascade on update cascade,
pubid int foreign key references Publication on delete cascade on update cascade,
primary key(serialNo,pubid))

go
create proc studentRegister
@first_name varchar(20),
@last_name varchar(20),
@password varchar(20),
@faculty varchar(20),
@Gucian bit,
@email varchar(50),
@address varchar(50)
as
begin
insert into PostGradUser(email,password)
values(@email,@password)
declare @id int
SELECT @id=SCOPE_IDENTITY()
if(@Gucian=1)
insert into GucianStudent(id,firstName,lastName,faculty,address) values(@id,@first_name,@last_name,@faculty,@address)
else
insert into NonGucianStudent(id,firstName,lastName,faculty,address) values(@id,@first_name,@last_name,@faculty,@address)
end

go
create proc supervisorRegister
@first_name varchar(20),
@last_name varchar(20),
@password varchar(20),
@faculty varchar(20),
@email varchar(50)
as
begin
insert into PostGradUser(email,password)
values(@email,@password)
declare @id int
SELECT @id=SCOPE_IDENTITY()
declare @name varchar(50)
set @name = CONCAT(@first_name,@last_name)
insert into Supervisor(id,name,faculty) values(@id,@name,@faculty)
end

go
Create proc userLogin
@id int,
@password varchar(20),
@success bit output
as
begin
if exists(
select ID,password
from PostGradUser
where id=@id and password=@password)
set @success =1
else
set @success=0
end

go
create proc addMobile
@ID int,
@mobile_number int
as
begin
if @ID is not null and @mobile_number is not null
begin
if(exists(select * from GucianStudent where id=@ID))
insert into GUCStudentPhoneNumber values(@ID,@mobile_number)
if(exists(select * from NonGucianStudent where id=@ID))
insert into NonGUCStudentPhoneNumber values(@ID,@mobile_number)
end
end

go
CREATE Proc AdminListSup
As
Select u.id,u.email,u.password,s.name, s.faculty
from PostGradUser u inner join Supervisor s on u.id = s.id

go
CREATE Proc AdminViewSupervisorProfile
@supId int
As
Select u.id,u.email,u.password,s.name, s.faculty
from PostGradUser u inner join Supervisor s on u.id = s.id
WHERE @supId = s.id

go
CREATE Proc AdminViewAllTheses
As
Select serialNumber,field,type,title,startDate,endDate,defenseDate,years,grade,payment_id,noOfExtensions
From Thesis

go
CREATE Proc AdminViewOnGoingTheses
@thesesCount int output
As
Select @thesesCount=Count(*)
From Thesis
where endDate > Convert(Date,CURRENT_TIMESTAMP)

go
CREATE Proc AdminViewStudentThesisBySupervisor
As
Select s.name,t.title,gs.firstName
From Thesis t inner join GUCianStudentRegisterThesis sr on t.serialNumber=sr.serial_no
inner join Supervisor s on s.id=sr.supid inner join GucianStudent gs on sr.sid=gs.id
where t.endDate > Convert(Date,CURRENT_TIMESTAMP)
union
Select s.name,t.title,gs.firstName
From Thesis t inner join NonGUCianStudentRegisterThesis sr on t.serialNumber=sr.serial_no
inner join Supervisor s on s.id=sr.supid inner join NonGucianStudent gs on sr.sid=gs.id
where t.endDate > Convert(Date,CURRENT_TIMESTAMP)

go
CREATE Proc AdminListNonGucianCourse
@courseID int
As
if(exists(select * from Course where id=@courseID))
Select ng.firstName,ng.lastName,c.code,n.grade
From NonGucianStudentTakeCourse n inner join Course c on n.cid=c.id inner join NonGucianStudent ng on ng.id=n.sid
where n.cid=@courseID

go
CREATE Proc AdminUpdateExtension
@ThesisSerialNo int
As
if(exists(select * from Thesis where serialNumber=@ThesisSerialNo))
begin
declare @noOfExtensions int
select @noOfExtensions=noOfExtensions from Thesis where serialNumber=@ThesisSerialNo
update Thesis
set noOfExtensions=@noOfExtensions+1
where serialNumber=@ThesisSerialNo
end

go
CREATE Proc AdminIssueThesisPayment
@ThesisSerialNo int,
@amount decimal,
@noOfInstallments int,
@fundPercentage decimal
As
if(exists(select * from Thesis where serialNumber=@ThesisSerialNo))
begin
insert into Payment(amount,noOfInstallments,fundPercentage) values(@amount,@noOfInstallments,@fundPercentage)
declare @id int
SELECT @id=SCOPE_IDENTITY()
update Thesis
set payment_id=@id
where serialNumber=@ThesisSerialNo
end

go
CREATE Proc AdminViewStudentProfile
@sid int
As
if(exists(select * from GucianStudent where id=@sid))
Select u.id,u.email,u.password,s.firstName,s.lastName,s.type,s.faculty,s.address,s.address,s.GPA
from PostGradUser u inner join GucianStudent s on u.id=s.id
WHERE @sid = s.id
else if(exists(select * from NonGucianStudent where id=@sid))
Select u.id,u.email,u.password,s.firstName,s.lastName,s.type,s.faculty,s.address,s.address,s.GPA
from PostGradUser u inner join NonGucianStudent s on u.id=s.id
WHERE @sid = s.id

go
CREATE Proc AdminIssueInstallPayment
@paymentID int,
@InstallStartDate date
As
if(exists(select * from Payment where id=@paymentID))
begin
declare @numOfInst int
select @numOfInst=noOfInstallments
from Payment
where id=@paymentID
declare @payAmount int
select @payAmount=amount
from Payment
where id=@paymentID
DECLARE @Counter INT
SET @Counter=1
WHILE (@counter<=@numOfInst)
BEGIN
declare @instdate date
set @instdate=@InstallStartDate
declare @instAmount int
set @instAmount=@payAmount/@numOfInst
if(@counter=1)
insert into Installment(date,paymentId,amount,done)values(@InstallStartDate,@paymentID,@instAmount,0)
else
begin
set @instdate=DATEADD(MM, 6, @instdate);
insert into Installment(date,paymentId,amount,done)values(@instdate,@paymentID,@instAmount,0)
end
SET @counter=@counter+1
END
end

go
CREATE Proc AdminListAcceptPublication
As
select t.serialNumber,p.title
from ThesisHasPublication tp inner join Thesis t on tp.serialNo=t.serialNumber
inner join Publication p on p.id=tp.pubid
where p.accepted=1

go
CREATE Proc AddCourse
@courseCode varchar(10),
@creditHrs int,
@fees decimal
As
insert into Course values(@fees,@creditHrs,@courseCode)

go
CREATE Proc linkCourseStudent
@courseID int,
@studentID int
As
if(exists(select * from Course ) and exists(select * from NonGucianStudent where id=@studentID))
insert into NonGucianStudentTakeCourse(sid,cid,grade)values(@studentID,@courseID,null)

go
CREATE Proc addStudentCourseGrade
@courseID int,
@studentID int,
@grade decimal
As
if(exists(select * from NonGucianStudentTakeCourse where sid=@studentID and cid=@courseID))
update NonGucianStudentTakeCourse
set grade =@grade
where cid=@courseID and sid=@studentID

go
CREATE Proc ViewExamSupDefense
@defenseDate datetime
As
select s.serial_no,ee.date,e.name,sup.name
from ExaminerEvaluateDefense ee inner join examiner e on ee.examinerId=e.id
inner join GUCianStudentRegisterThesis s on ee.serialNo=s.serial_no
inner join Supervisor sup on sup.id=s.supid

go
CREATE Proc EvaluateProgressReport
@supervisorID int,
@thesisSerialNo int,
@progressReportNo int,
@evaluation int
As
if(exists(select * from Thesis where serialNumber=@thesisSerialNo ) and @evaluation in(0,1,2,3) )
begin
if(exists(select * from GUCianStudentRegisterThesis where serial_no=@thesisSerialNo and supid=@supervisorID))
begin
declare @gucSid int
select @gucSid=sid
from GUCianStudentRegisterThesis
where serial_no=@thesisSerialNo
update GUCianProgressReport
set eval=@evaluation
where sid=@gucSid and thesisSerialNumber=@thesisSerialNo and no=@progressReportNo
end
else if(exists(select * from NonGUCianStudentRegisterThesis where serial_no=@thesisSerialNo and supid=@supervisorID))
begin
declare @nonGucSid int
select @nonGucSid=sid
from NonGUCianStudentRegisterThesis
where serial_no=@thesisSerialNo
update NonGUCianProgressReport
set eval=@evaluation
where sid=@nonGucSid and thesisSerialNumber=@thesisSerialNo and no=@progressReportNo
end
end

go
CREATE Proc ViewSupStudentsYears
@supervisorID int
As
if(exists(select * from Supervisor where id=@supervisorID))
begin
select s.firstName,s.lastName,t.years
from GUCianStudentRegisterThesis sr inner join GucianStudent s on sr.sid=s.id
inner join Thesis t on t.serialNumber=sr.serial_no
union
select s.firstName,s.lastName,t.years
from NonGUCianStudentRegisterThesis sr inner join NonGucianStudent s on sr.sid=s.id
inner join Thesis t on t.serialNumber=sr.serial_no
end

go
CREATE Proc SupViewProfile
@supervisorID int
As
if(exists(select * from Supervisor where id=@supervisorID))
begin
select u.id,u.email,u.password,s.name,s.faculty
from PostGradUser u inner join Supervisor s on u.id=s.id
end

go
create proc UpdateSupProfile
@supervisorID int, @name varchar(20), @faculty varchar(20)
as
update Supervisor
set name = @name, faculty = @faculty
where id = @supervisorID

go
create proc ViewAStudentPublications
@StudentID int
as
select P.*
from GUCianStudentRegisterThesis GUC
inner join Thesis T
on GUC.serial_no = T.serialNumber
inner join ThesisHasPublication TP
on T.serialNumber = TP.serialNo
inner join Publication P
on P.id = TP.pubid
where GUC.sid = @StudentID
union all
select P.*
from NonGUCianStudentRegisterThesis NON
inner join Thesis T
on NON.serial_no = T.serialNumber
inner join ThesisHasPublication TP
on T.serialNumber = TP.serialNo
inner join Publication P
on P.id = TP.pubid
where NON.sid = @StudentID

go
create proc AddDefenseGucian
@ThesisSerialNo int , @DefenseDate Datetime , @DefenseLocation varchar(15)
as
insert into Defense values(@ThesisSerialNo,@DefenseDate,@DefenseLocation,null)

go
create proc AddDefenseNonGucian
@ThesisSerialNo int , @DefenseDate Datetime , @DefenseLocation varchar(15)
as
declare @idOfStudent int
select @idOfStudent = sid
from NonGUCianStudentRegisterThesis
where serial_no = @ThesisSerialNo
if(not exists(select grade
from NonGucianStudentTakeCourse
where sid = @idOfStudent and grade < 50))
begin
insert into Defense values(@ThesisSerialNo,@DefenseDate,@DefenseLocation,null)
end

go
create proc AddExaminer
@ThesisSerialNo int , @DefenseDate Datetime , @ExaminerName varchar(20),@Password varchar(30), @National bit, @fieldOfWork varchar(20)
as
insert into PostGradUser values(@ExaminerName,@Password)
declare @id int
set @id = SCOPE_IDENTITY()
insert into Examiner values(@id,@ExaminerName,@fieldOfWork,@National)
insert into ExaminerEvaluateDefense values(@DefenseDate,@ThesisSerialNo,@id,null)

go
create proc CancelThesis
@ThesisSerialNo int
as
if(exists(
select *
from GUCianProgressReport
where thesisSerialNumber = @ThesisSerialNo
))
begin
declare @gucianEval int
set @gucianEval = (
select top 1 eval
from GUCianProgressReport
where thesisSerialNumber = @ThesisSerialNo
order by no desc
)
if(@gucianEval = 0)
begin
delete from Thesis where serialNumber = @ThesisSerialNo
end
end
else
begin
declare @nonGucianEval int
set @nonGucianEval = (
select top 1 eval
from NonGUCianProgressReport
where thesisSerialNumber = @ThesisSerialNo
order by no desc
)
if(@nonGucianEval = 0)
begin
delete from Thesis where serialNumber = @ThesisSerialNo
end
end

go
create proc AddGrade
@ThesisSerialNo int
as
declare @grade decimal(4,2)
select @grade = grade
from Defense
where serialNumber = @ThesisSerialNo
update Thesis
set grade = @grade
where serialNumber = @ThesisSerialNo

go
create proc AddDefenseGrade
@ThesisSerialNo int , @DefenseDate Datetime , @grade decimal(4,2)
as
update Defense
set grade = @grade
where serialNumber = @ThesisSerialNo and date = @DefenseDate

go
create proc AddCommentsGrade
@ThesisSerialNo int , @DefenseDate Datetime , @comments varchar(300)
as
update ExaminerEvaluateDefense
set comment = @comments
where serialNo = @ThesisSerialNo and date = @DefenseDate

go
create proc viewMyProfile
@studentId int
as
if(exists(
select * from GucianStudent where id = @studentId
))
begin
select G.*,P.email
from GucianStudent G
inner join PostGradUser P
on G.id = P.id
where G.id = @studentId
end
else
begin
select N.*,P.email
from NonGucianStudent N
inner join PostGradUser P
on N.id = P.id
where N.id = @studentId
end

go
create proc editMyProfile
@studentID int, @firstName varchar(20), @lastName varchar(20), @password varchar(30), @email varchar(50)
, @address varchar(50), @type varchar(3)
as
update GucianStudent
set firstName = @firstName, lastName = @lastName, address = @address, type = @type
where id = @studentID
update NonGucianStudent
set firstName = @firstName, lastName = @lastName, address = @address, type = @type
where id = @studentID
update PostGradUser
set email = @email, password = @password
where id = @studentID

go
create proc addUndergradID
@studentID int, @undergradID varchar(10)
as
update GucianStudent
set undergradID = @undergradID
where id = @studentID

go
create proc ViewCoursesGrades
@studentID int
as
select grade
from NonGucianStudentTakeCourse
where sid = @studentID

go
create proc ViewCoursePaymentsInstall
@studentID int
as
select P.id as 'Payment Number', P.amount as 'Amount of Payment',P.fundPercentage as 'Percentage of fund for payment', P.noOfInstallments as 'Number of installments',
I.amount as 'Installment Amount',I.date as 'Installment date', I.done as 'Installment done or not'
from NonGucianStudentPayForCourse NPC
inner join Payment P
on NPC.paymentNo = P.id and NPC.sid = @studentID
inner join Installment I
on I.paymentId = P.id

go
create proc ViewThesisPaymentsInstall
@studentID int
as
select P.id as 'Payment Number', P.amount as 'Amount of Payment', P.fundPercentage as 'Fund',P.noOfInstallments as 'Number of installments',
I.amount as 'Installment amount',I.date as 'Installment date', I.done as 'Installment done or not'
from GUCianStudentRegisterThesis G
inner join Thesis T
on G.serial_no = T.serialNumber and G.sid = @studentID
inner join Payment P
on T.payment_id = P.id
inner join Installment I
on I.paymentId = P.id
union
select P.id as 'Payment Number',P.amount as 'Amount of Payment', P.fundPercentage as 'Fund',P.noOfInstallments as 'Number of installments',
I.amount as 'Installment amount',I.date as 'Installment date', I.done as 'Installment done or not'
from NonGUCianStudentRegisterThesis NG
inner join Thesis T
on NG.serial_no = T.serialNumber and NG.sid = @studentID
inner join Payment P
on T.payment_id = P.id
inner join Installment I
on I.paymentId = P.id

go
create proc ViewUpcomingInstallments
@studentID int
as
select I.date as 'Date of Installment' ,I.amount as 'Amount'
from Installment I
inner join NonGucianStudentPayForCourse NPC
on I.paymentId = NPC.paymentNo and NPC.sid = @studentID and I.date > CURRENT_TIMESTAMP
union
select I.date as 'Date of Installment' ,I.amount as 'Amount'
from Thesis T
inner join Payment P
on T.payment_id = P.id
inner join Installment I
on I.paymentId = P.id
inner join GUCianStudentRegisterThesis G
on G.serial_no = T.serialNumber and G.sid = @studentID
where I.date > CURRENT_TIMESTAMP
union
select I.date as 'Date of Installment' ,I.amount as 'Amount'
from Thesis T
inner join Payment P
on T.payment_id = P.id
inner join Installment I
on I.paymentId = P.id
inner join NonGUCianStudentRegisterThesis G
on G.serial_no = T.serialNumber and G.sid = @studentID
where I.date > CURRENT_TIMESTAMP

go
create proc ViewMissedInstallments
@studentID int
as
select I.date as 'Date of Installment' ,I.amount as 'Amount'
from Installment I
inner join NonGucianStudentPayForCourse NPC
on I.paymentId = NPC.paymentNo and NPC.sid = @studentID and I.date < CURRENT_TIMESTAMP and I.done = '0'
union
select I.date as 'Date of Installment' ,I.amount as 'Amount'
from Thesis T
inner join Payment P
on T.payment_id = P.id
inner join Installment I
on I.paymentId = P.id
inner join GUCianStudentRegisterThesis G
on G.serial_no = T.serialNumber and G.sid = @studentID
where I.date < CURRENT_TIMESTAMP and I.done = '0'
union
select I.date as 'Date of Installment' ,I.amount as 'Amount'
from Thesis T
inner join Payment P
on T.payment_id = P.id
inner join Installment I
on I.paymentId = P.id
inner join NonGUCianStudentRegisterThesis G
on G.serial_no = T.serialNumber and G.sid = @studentID
where I.date < CURRENT_TIMESTAMP and I.done = '0'

go
create proc AddProgressReport
@thesisSerialNo int, @progressReportDate date, @studentID int,@progressReportNo int
as
declare @gucian int
if(exists(
select id
from GucianStudent
where id = @studentID
))
begin
set @gucian = '1'
end
else
begin
set @gucian = '0'
end
if(@gucian = '1')
begin
insert into GUCianProgressReport values(@studentID,@progressReportNo,@progressReportDate,null,null,null,@thesisSerialNo,null)
end
else
begin
insert into NonGUCianProgressReport values(@studentID,@progressReportNo,@progressReportDate,null,null,null,@thesisSerialNo,null)
end

go
create proc FillProgressReport
@thesisSerialNo int, @progressReportNo int, @state int, @description varchar(200),@studentID int
as
declare @gucian bit
if(exists(
select * from GucianStudent
where id = @studentID
))
begin
set @gucian = '1'
end
else
begin
set @gucian = '0'
end
if(@gucian = '1')
begin
update GUCianProgressReport
set state = @state, description = @description, date = CURRENT_TIMESTAMP
where thesisSerialNumber = @thesisSerialNo and sid = @studentID and no = @progressReportNo
end
else
begin
update NonGUCianProgressReport
set state = @state, description = @description, date = CURRENT_TIMESTAMP
where thesisSerialNumber = @thesisSerialNo and sid = @studentID and no = @progressReportNo
end

go
create proc ViewEvalProgressReport
@thesisSerialNo int, @progressReportNo int,@studentID int
as
select eval
from GUCianProgressReport
where sid = @studentID and thesisSerialNumber = @thesisSerialNo and no = @progressReportNo
union
select eval
from NonGUCianProgressReport
where sid = @studentID and thesisSerialNumber = @thesisSerialNo and no = @progressReportNo

go
create proc addPublication
@title varchar(100), @pubDate date, @host varchar(100), @place varchar(100), @accepted bit
as
insert into Publication values(@title,@pubDate,@place,@accepted,@host)

go
create proc linkPubThesis
@PubID int, @thesisSerialNo int
as
insert into ThesisHasPublication values(@thesisSerialNo,@PubID)

go
create trigger deleteSupervisor
on Supervisor
instead of delete
as
delete from GUCianProgressReport where supid in (select id from deleted)
delete from NonGUCianProgressReport where supid in (select id from deleted)
delete from GUCianStudentRegisterThesis where supid in (select id from deleted)
delete from NonGUCianStudentRegisterThesis where supid in (select id from deleted)
delete from Supervisor where id in (select id from deleted)
delete from PostGradUser where id in (select id from deleted)
-----------------------------------------------------------------------------------
--New Procedures
go
CREATE PROC AdminIssueInstallPayment2
@paymentID int, 
@InstallStartDate date
AS
DECLARE @numOfInstallments int;
DECLARE @currentDate date;
DECLARE @counter int;
DECLARE @totalAmount decimal;
DECLARE @installmentAmount decimal;
DECLARE @fund decimal;
SET @currentDate = @InstallStartDate;
SET @counter=0;
SELECT @numOfInstallments = noOfInstallments, @totalAmount=amount,@fund=fundPercentage FROM Payment WHERE @paymentID = id;
SET @installmentAmount = (@totalAmount-(@totalAmount*@fund/100))/@numOfInstallments;
WHILE (@counter<@numOfInstallments)
BEGIN
	INSERT INTO Installment VALUES(@currentDate,@paymentID,@installmentAmount,0);
	SELECT @currentDate=DATEADD(month, 6, @currentDate);
	SET @counter = @counter + 1;
END

go
CREATE Proc ViewSupStudentsYears3
@supervisorID int
As
if(exists(select * from Supervisor where id=@supervisorID))
begin
select s.id,s.firstName,s.lastName,t.years
from GUCianStudentRegisterThesis sr inner join GucianStudent s on sr.sid=s.id
inner join Thesis t on t.serialNumber=sr.serial_no where sr.supid=@supervisorID
union
select s.id,s.firstName,s.lastName,t.years
from NonGUCianStudentRegisterThesis sr inner join NonGucianStudent s on sr.sid=s.id
inner join Thesis t on t.serialNumber=sr.serial_no where sr.supid=@supervisorID
end

go
create proc checkexaminerinthesis
@ThesisSerialNo int,
@ExminerID int,
@success bit output
as
if (exists (select * from ExaminerEvaluateDefense where serialNo=@ThesisSerialNo and examinerId=@ExminerID))
set @success =0;
else
set @success =1;

go
create proc supervisorhasthesis
@Supid int,
@ThesisSerialNo int,
@ProgressNo int,
@success bit output
as
if(exists(select * from GUCianProgressReport where no=@ProgressNo and thesisSerialNumber=@ThesisSerialNo and supid=@Supid  union 
select * from NonGUCianProgressReport where no=@ProgressNo and thesisSerialNumber=@ThesisSerialNo and supid=@Supid))
set @success=1;
else
set @success =0;

go
create proc checkemailpass
@email varchar(50),
@password varchar(20),
@success bit output,
@id int output
as
if (exists (select * from PostGradUser where email=@email and password=@password))
begin
set @success=1;
select @id=id from PostGradUser where email=@email and password=@password
end
else
set @success=0;

go
create proc checkemail
@email varchar(50),
@success bit output
as
if (exists (select * from PostGradUser where email=@email ))
set @success =0;
else
set @success =1;

go
create proc getUserType
@id int,
@type varchar(10) output
as
if (exists(SELECT id FROM GucianStudent Where Id = @id))
SET @type = 'Gucian'
else if(exists(SELECT ID FROM NonGucianStudent Where Id = @id))
SET @type = 'NonGucian'
else if (exists(SELECT id FROM Supervisor WHERE id = @id))
SET @type = 'Supervisor'
else if (exists(SELECT id FROM Admin WHERE id = @id))
SET @type = 'Admin'
else
SET @type = 'Examiner'

go
create proc getLastId
@id int output
as
SELECT @id=MAX(id) FROM PostGradUser

go
CREATE PROC viewExaminerInfo
@id INT
as
SELECT E.id ,  E.name, E.fieldOfWork, E.isNational
FROM Examiner E 
WHERE E.id = @id

go
create proc editExaminerinfo
@id int, @name varchar(20), @fieldOfWork varchar(30)
as
update Examiner
set name = @name, fieldOfWork = @fieldOfWork
where id = @id

go
create proc AddCommentsGrade1
@ThesisSerialNo int , @comments varchar(300)
as
update ExaminerEvaluateDefense
set comment = @comments
where serialNo = @ThesisSerialNo 

go
CREATE Proc ViewSupStudentsYears2
@supervisorID int
As
if(exists(select * from Supervisor where id=@supervisorID))
begin
select s.id,s.firstName,s.lastName,t.years
from GUCianStudentRegisterThesis sr inner join GucianStudent s on sr.sid=s.id
inner join Thesis t on t.serialNumber=sr.serial_no
union
select s.id,s.firstName,s.lastName,t.years
from NonGUCianStudentRegisterThesis sr inner join NonGucianStudent s on sr.sid=s.id
inner join Thesis t on t.serialNumber=sr.serial_no
end

go
create proc thesishaspayment
@thesisNo int,
@success bit output
as
if(exists (select * from Thesis where serialNumber=@thesisNo and payment_id is not null))
set @success =0;
else
set @success=1;

go
create proc getSidofThesis2
@TheisSerialNo int,
@sid int output,
@succes int output
as
if (exists (select * from GUCianStudentRegisterThesis  where serial_no=@TheisSerialNo ))
begin
set @succes=1;
select @sid=sid from GUCianStudentRegisterThesis  where serial_no=@TheisSerialNo
end
else
begin
if (exists (select * from NonGUCianStudentRegisterThesis  where serial_no=@TheisSerialNo ))
begin
set @succes=2;
select @sid=sid from NonGUCianStudentRegisterThesis  where serial_no=@TheisSerialNo
end
else 
begin 
if (exists (select * from Thesis where serialNumber=@TheisSerialNo))
begin
set @succes=3;
set @sid=99999;
end
else
begin
set @succes=0;
set @sid=99999;
end
end
end

go
create proc AddDefenseGucian2
@ThesisSerialNo int , @DefenseDate Datetime , @DefenseLocation varchar(15)
as
insert into Defense values(@ThesisSerialNo,@DefenseDate,@DefenseLocation,null)
update Thesis
set defenseDate=@DefenseDate 
where serialNumber=@ThesisSerialNo

go
create proc AddDefenseNonGucian2
@ThesisSerialNo int , @DefenseDate Datetime , @DefenseLocation varchar(15)
as
declare @idOfStudent int
select @idOfStudent = sid
from NonGUCianStudentRegisterThesis
where serial_no = @ThesisSerialNo
if(not exists(select grade
from NonGucianStudentTakeCourse
where sid = @idOfStudent and grade < 50))
begin
insert into Defense values(@ThesisSerialNo,@DefenseDate,@DefenseLocation,null)
update Thesis
set defenseDate=@DefenseDate 
where serialNumber=@ThesisSerialNo
end

go
create proc DefenseExists
@TheisSerialNo int,
@success bit output
as
if (exists (select * from Defense where @TheisSerialNo = serialNumber))
set @success=0;
else
set @success=1;

go
create proc checkthesisPR
@TheisSerialNo int,
@No int,
@success int output
as
if (exists (select * from GUCianProgressReport where thesisSerialNumber=@TheisSerialNo and no=@No and eval is null union select * from NonGUCianProgressReport where thesisSerialNumber=@TheisSerialNo and no=@No and eval is null))
set @success =2;
else
begin
if (exists (select * from GUCianProgressReport where thesisSerialNumber=@TheisSerialNo and no=@No union select * from NonGUCianProgressReport where thesisSerialNumber=@TheisSerialNo and no=@No))
set @success=1;
else
set @success=0;
end

go
create proc checkifPRzero
@ThesisSerialNo int,
@success bit output
as
set @success =0;
if(exists(
select *
from GUCianProgressReport
where thesisSerialNumber = @ThesisSerialNo
))
begin
declare @gucianEval int
set @gucianEval = (
select top 1 eval
from GUCianProgressReport
where thesisSerialNumber = @ThesisSerialNo
order by no desc
)
if(@gucianEval = 0)
begin
set @success=1;
end
end
else
begin
declare @nonGucianEval int
set @nonGucianEval = (
select top 1 eval
from NonGUCianProgressReport
where thesisSerialNumber = @ThesisSerialNo
order by no desc
)
if(@nonGucianEval = 0)
begin
set @success=1;
end
end

go
create proc AddExaminerNew
@ThesisSerialNo int , @DefenseDate Datetime , @id int
as
insert into ExaminerEvaluateDefense values(@DefenseDate,@ThesisSerialNo,@id,null)

go
create proc checkdateAndThesis
@date Datetime,
@serialNo int,
@success bit output
as
if(exists (select * from Defense where serialNumber=@serialNo and date=@date))
set @success = 1;
else
set @success = 0;

go
create proc checkExaminerId
@id int,
@success bit output
as
if(exists(select * from Examiner where id=@id))
set @success=1;
else
set @success=0;
go
CREATE PROC viewAllPublications
as
SELECT * FROM Publication;

go
create proc installmentsexists 
@Paymentid int ,
@success bit output
as
if (exists (select * from Installment where paymentId=@Paymentid))
set @success=1;
else
set @success=0;

go
create proc getExmainerDefense
@ExaminerId int
as
select ED.date,ED.examinerId,ED.comment,D.serialNumber,D.grade,D.location from ExaminerEvaluateDefense ED, Defense D
where ED.examinerId=@ExaminerId and ED.serialNo=D.serialNumber

go
create proc getExmainerDefense1
@ExaminerId int
as
select ED.date,ED.examinerId,ED.comment,D.serialNumber,D.grade from ExaminerEvaluateDefense ED, Defense D
where ED.examinerId=@ExaminerId and ED.serialNo=D.serialNumber

go
create proc AddaGrade
@ThesisSerialNo int,  @grade decimal(4,2)
as
update Defense
set grade = @grade
where serialNumber = @ThesisSerialNo
select @grade = grade
from Defense
where serialNumber = @ThesisSerialNo
update Thesis
set grade = @grade
where serialNumber = @ThesisSerialNo

go
CREATE Proc examinerViewStudentThesisSupervisor
@examinerId int
As
Select  s.name AS 'supervisorName',t.title,gs.firstName +' '+ gs.lastName AS 'studentName'
From Thesis t inner join GUCianStudentRegisterThesis sr on t.serialNumber=sr.serial_no
inner join Supervisor s on s.id=sr.supid inner join GucianStudent gs on sr.sid=gs.id inner join ExaminerEvaluateDefense evd on evd.serialNo = t.serialNumber
WHERE evd.examinerId=@examinerId
union
Select  s.name AS 'supervisorName',t.title,gs.firstName +' '+ gs.lastName AS 'studentName'
From Thesis t inner join NonGUCianStudentRegisterThesis sr on t.serialNumber=sr.serial_no
inner join Supervisor s on s.id=sr.supid inner join NonGucianStudent gs on sr.sid=gs.id inner join ExaminerEvaluateDefense evd on evd.serialNo = t.serialNumber
WHERE evd.examinerId=@examinerId

go
Create Proc ViewAllThesisThat
@keyword varchar(50)
As
Select serialNumber,field,type,title,startDate,endDate,defenseDate,years,grade,payment_id,noOfExtensions
From Thesis
Where title Like '%'+@keyword+'%'

go
create proc ExaminerRegister
@first_name varchar(20),
@last_name varchar(20),
@password varchar(20),
@feildOfWork varchar(100),
@email varchar(50),
@isNational bit
as
begin
insert into PostGradUser(email,password)
values(@email,@password)
declare @id int
SELECT @id=SCOPE_IDENTITY()
declare @name varchar(50)
set @name = CONCAT(@first_name,@last_name)
insert into Examiner(id,name,fieldOfWork,isNational) values(@id,@name,@feildOfWork,@isNational)
end

go
create proc ViewMyLinkedPublications
@StudentID int
as
select TP.*
from GUCianStudentRegisterThesis GUC
inner join Thesis T
on GUC.serial_no = T.serialNumber
inner join ThesisHasPublication TP
on T.serialNumber = TP.serialNo
inner join Publication P
on P.id = TP.pubid
where GUC.sid = @StudentID
union all
select TP.*
from NonGUCianStudentRegisterThesis NON
inner join Thesis T
on NON.serial_no = T.serialNumber
inner join ThesisHasPublication TP
on T.serialNumber = TP.serialNo
inner join Publication P
on P.id = TP.pubid
where NON.sid = @StudentID

go
create proc viewAllMyThesis
@studentId int
as
if(exists(SELECT id FROM GucianStudent WHERE @studentId=id))
SELECT t.* FROM GUCianStudentRegisterThesis st INNER JOIN Thesis t ON t.serialNumber=st.serial_no WHERE @studentId = st.sid
ELSE
SELECT t.* FROM NonGUCianStudentRegisterThesis st INNER JOIN Thesis t ON t.serialNumber=st.serial_no WHERE @studentId = st.sid

go
create proc viewAllMyOngoingThesis
@studentId int
as
if(exists(SELECT id FROM GucianStudent WHERE @studentId=id))
SELECT t.* FROM GUCianStudentRegisterThesis st INNER JOIN Thesis t ON t.serialNumber=st.serial_no WHERE @studentId = st.sid AND t.endDate > Convert(Date,CURRENT_TIMESTAMP)
ELSE
SELECT t.* FROM NonGUCianStudentRegisterThesis st INNER JOIN Thesis t ON t.serialNumber=st.serial_no WHERE @studentId = st.sid AND t.endDate > Convert(Date,CURRENT_TIMESTAMP)

go
create proc checkfrostudent
@studentid int,
@success bit output
as
if exists (select id from GucianStudent where id=@studentid   union  select id from NonGucianStudent where id=@studentid) 
set @success=1;
else
set @success=0;

go
create proc checkgrades
@sid int,
@success bit output
as
if (exists (select * from NonGucianStudentTakeCourse where sid=@sid and grade is not null and grade<=50))
set @success=0;
else
set @success=1;

go
create proc checkfrothesis
@thesisNo int,
@success bit output
as
if exists (select * from Thesis where serialNumber=@thesisNo)
set @success=1;
else
set @success=0;

go
create proc ViewCoursesInfo
@studentID int
as
select c.code,c.creditHours,st.grade
from NonGucianStudentTakeCourse st Inner Join Course c ON c.id = st.cid 
where st.sid = @studentID

go
CREATE PROC viewMyMobileNumbers
@studentId int
as
if(exists(SELECT id FROM GucianStudent WHERE @studentId=id))
SELECT phone FROM GUCStudentPhoneNumber WHERE @studentId = id
ELSE
SELECT phone FROM nonGUCStudentPhoneNumber WHERE @studentId = id

go
create proc checkfropayment
@paymentid int,
@success bit output
as
if exists (select * from Payment where id=@paymentid)
set @success=1;
else
set @success=0;

go
create proc checkThesisBelongs
@thesisSerialNo int, @studentID int, @success bit output
as
declare @gucian int
if(exists(
select id
from GucianStudent
where id = @studentID
))
begin
set @gucian = '1'
end
else
begin
set @gucian = '0'
end
if(@gucian = '1')
begin
if(exists(SELECT * FROM GUCianStudentRegisterThesis WHERE @studentID = sid AND @thesisSerialNo = serial_no))
SET @success = 1;
ELSE
SET @success = 0;
end
else
begin
if(exists(SELECT * FROM NonGUCianStudentRegisterThesis WHERE @studentID = sid AND @thesisSerialNo = serial_no))
SET @success = 1;
ELSE
SET @success = 0;
end

go
create proc checkThesisReportStudentExist
@thesisSerialNo int, @studentID int,@no int, @success bit output
as
declare @gucian int
if(exists(
select id
from GucianStudent
where id = @studentID
))
begin
set @gucian = '1'
end
else
begin
set @gucian = '0'
end
if(@gucian = '1')
begin
if(exists(SELECT * FROM GUCianProgressReport WHERE @studentID = sid AND @thesisSerialNo = thesisSerialNumber AND no = @no))
SET @success = 1;
ELSE
SET @success = 0;
end
else
begin
if(exists(SELECT * FROM NonGUCianProgressReport WHERE @studentID = sid AND @thesisSerialNo = thesisSerialNumber AND no = @no))
SET @success = 1;
ELSE
SET @success = 0;
end

go
create proc checkOngoingThesisBelongs
@thesisSerialNo int, @studentID int, @success bit output
as
if(exists(SELECT id FROM GucianStudent WHERE @studentId=id) AND exists(SELECT t.* FROM GUCianStudentRegisterThesis st INNER JOIN Thesis t ON t.serialNumber=st.serial_no WHERE @studentId = st.sid AND t.endDate > Convert(Date,CURRENT_TIMESTAMP)  AND @thesisSerialNo = st.serial_no))
SET @success = 1
ELSE if(exists(SELECT id FROM NonGucianStudent WHERE @studentId=id) AND exists(SELECT t.* FROM NonGUCianStudentRegisterThesis st INNER JOIN Thesis t ON t.serialNumber=st.serial_no WHERE @studentId = st.sid AND t.endDate > Convert(Date,CURRENT_TIMESTAMP) AND @thesisSerialNo = st.serial_no))
SET @success = 1
ELSE
SET @success = 0

go
create proc ViewMyProgressReports
@studentID int
as
select *
from GUCianProgressReport
where sid = @studentID 
union
select *
from NonGUCianProgressReport
where sid = @studentID 
-----------------------------------------------------------------------------------
--Insertions
go
use PostGradOffice

--1)Payments and Installments
Insert Into Payment Values (1545.50,3,0);
Insert Into Installment Values ('2020/10/10',1,545,1);
Insert Into Installment Values ('2020/11/10',1,500,0);
Insert Into Installment Values ('2022/12/10',1,500,0);

Insert Into Payment Values (2500,2,50.0);
Insert Into Installment Values ('2017/10/10',2,250,1);
Insert Into Installment Values ('2018/5/9',2,1000,0);

Insert Into Payment Values (3000,4,33.33);
Insert Into Installment Values ('2019/10/10',3,500,1);
Insert Into Installment Values ('2020/5/5',3,500,1);
Insert Into Installment Values ('2021/5/5',3,500,1);
Insert Into Installment Values ('2022/9/9',3,500,0);

Insert Into Payment Values (1000,2,0);
Insert Into Installment Values ('2020/11/10',4,500,1);
Insert Into Installment Values ('2020/12/10',4,500,0);

Insert Into Payment Values (2000.20,2,50);
Insert Into Installment Values ('2012/3/4',5,1000.1,0);
Insert Into Installment Values ('2019/7/7',5,1000.1,0);

Insert Into Payment Values (10000,10,0);
Insert Into Installment Values ('2018/10/10',6,1000,1);
Insert Into Installment Values ('2018/11/10',6,1000,1);
Insert Into Installment Values ('2018/12/10',6,1000,1);
Insert Into Installment Values ('2019/1/10',6,1000,1);
Insert Into Installment Values ('2019/2/10',6,1000,1);
Insert Into Installment Values ('2019/3/10',6,1000,1);
Insert Into Installment Values ('2019/4/10',6,1000,1);
Insert Into Installment Values ('2019/5/10',6,1000,0);
Insert Into Installment Values ('2019/6/10',6,1000,0);
Insert Into Installment Values ('2019/7/10',6,1000,0);

Insert Into Payment Values (1000,3,10);
Insert Into Installment Values ('2020/9/10',7,300,1);
Insert Into Installment Values ('2020/12/10',7,300,0);
Insert Into Installment Values ('2021/3/10',7,300,0);

Insert Into Payment Values (1000,1,50);
Insert Into Installment Values ('2022/10/10',8,500,0);

Insert Into Payment Values (1000,4,20);
Insert Into Installment Values ('2020/10/10',9,2000,1);
Insert Into Installment Values ('2021/10/10',9,2000,1);
Insert Into Installment Values ('2022/10/10',9,2000,0);
Insert Into Installment Values ('2023/10/10',9,2000,0);

Insert Into Payment Values (2000,2,0);
Insert Into Installment Values ('2019/2/2',10,1000,1);
Insert Into Installment Values ('2023/1/1',10,1000,1);

Insert Into Payment Values (3000,2,50);
Insert Into Installment Values ('2020/1/10',11,500,1);
Insert Into Installment Values ('2022/9/10',11,1500,0);

Insert Into Payment Values (3000,3,0);
Insert Into Installment Values ('2019/1/1',12,1000,1);
Insert Into Installment Values ('2019/2/2',12,1000,1);
Insert Into Installment Values ('2020/2/2',12,1000,1);

Insert Into Payment Values (5000,4,20);
Insert Into Installment Values ('2019/2/2',13,1000,1);
Insert Into Installment Values ('2020/2/2',13,1000,1);
Insert Into Installment Values ('2021/2/2',13,1000,1);
Insert Into Installment Values ('2022/2/2',13,1000,0);

Insert Into Payment Values (1000,2,0);
Insert Into Installment Values ('2020/2/2',14,500,1);
Insert Into Installment Values ('2022/2/2',14,500,0);

Insert Into Payment Values (2000,4,50);--
Insert Into Installment Values ('2020/7/7',15,250,0);
Insert Into Installment Values ('2021/7/7',15,250,0);
Insert Into Installment Values ('2022/7/7',15,250,0);
Insert Into Installment Values ('2023/7/7',15,250,0);

Insert Into Payment Values (3000,6,0);

Insert Into Payment Values (3000,3,0);
Insert Into Installment Values ('2020/1/1',17,1000,1);
Insert Into Installment Values ('2022/8/8',17,1000,0);
Insert Into Installment Values ('2024/10/1',17,1000,0);

Insert Into Payment Values (5000,2,20);
Insert Into Installment Values ('2020/2/2',18,2000,1);
Insert Into Installment Values ('2021/2/2',18,2000,0);

Insert Into Payment Values (4000,2,0);
Insert Into Installment Values ('2022/1/1',19,2000,1);
Insert Into Installment Values ('2023/8/8',19,2000,1);

Insert Into Payment Values (4000,10,0);
Insert Into Installment Values ('2020/10/10',20,400,1);
Insert Into Installment Values ('2020/11/10',20,400,1);
Insert Into Installment Values ('2020/12/10',20,400,1);
Insert Into Installment Values ('2021/1/10',20,400,1);
Insert Into Installment Values ('2021/2/10',20,400,1);
Insert Into Installment Values ('2021/3/10',20,400,1);
Insert Into Installment Values ('2021/4/10',20,400,0);
Insert Into Installment Values ('2021/5/10',20,400,0);
Insert Into Installment Values ('2022/6/10',20,400,0);
Insert Into Installment Values ('2022/7/10',20,400,0);


--2)Theses of both types.
INSERT INTO Thesis VALUES('Pharmacy', 'MA', 'Medical Lifestyle', '2018/10/12 07:18:05', '2020/11/01 23:59:59', '2020/01/05', 75.53, 1, 1)
INSERT INTO Thesis VALUES('Engineering', 'MA', 'Specifics of engineering materials', '2017/05/22 18:14:40', '2022/06/02 23:59:59', NULL, 90.45, 2, 2)
INSERT INTO Thesis VALUES('Pharmacy', 'PhD', 'Brain Cancer', '2015/09/13 14:20:50', '2019/07/02 23:59:59', '2016/12/25', 99.22, NULL,0)
INSERT INTO Thesis VALUES('Engineering', 'MA', 'Engineering economy', '2015/04/24 10:30:18', '2018/05/11 23:59:59', NULL, NULL, NULL, 2)
INSERT INTO Thesis VALUES('Pharmacy', 'MA', 'Pain Management', '2019/03/27 11:18:45', '2021/11/14 23:59:59', NULL, NULL, NULL, 1)
INSERT INTO Thesis VALUES('Engineering', 'MA', 'Engineering and maintenance', '2018/07/11 12:10:23', '2021/10/24 23:59:59', NULL, NULL, NULL,0)
INSERT INTO Thesis VALUES('Management', 'MA', 'How to handle a crisis in an organization', '2016/09/22 05:23:14', '2023/06/19 23:59:59', NULL, NULL, NULL, 1)
INSERT INTO Thesis VALUES('Engineering', 'PhD', 'Models of software engineering', '2013/12/01 01:14:05', '2017/11/12 23:59:59', NULL, NULL, NULL, 1)
INSERT INTO Thesis VALUES('Pharmacy', 'PhD', 'Immunization', '2012/11/04 06:24:21', '2016/03/15 23:59:59', NULL, NULL, NULL, 2)
INSERT INTO Thesis VALUES('Engineering', 'PhD', 'Adaptive security methods', '2014/10/07 07:45:59', '2018/02/22 23:59:59', NULL, NULL, NULL,0)
INSERT INTO Thesis VALUES('Pharmacy', 'PhD', 'Antibiotic Resistance', '2017/09/11 08:27:55', '2020/07/15 23:59:59', NULL, NULL, NULL, 1)
INSERT INTO Thesis VALUES('Engineering', 'PhD', 'The ethics of genetic engineering', '2018/08/16 14:23:34', '2022/08/23 23:59:59', NULL, NULL, 13, 2)
INSERT INTO Thesis VALUES('Pharmacy', 'PhD', 'Substance Abuse and Addiction', '2016/07/20 16:32:26', '2019/09/16 23:59:59', NULL, NULL, NULL, 2)
INSERT INTO Thesis VALUES('Management', 'PhD', 'Franchising', '2013/06/25 19:37:37', '2018/10/17 23:59:59', NULL, NULL, NULL,0)
INSERT INTO Thesis VALUES('Management', 'PhD', 'Gap Analysis', '2015/05/27 22:40:39', '2019/11/27 23:59:59', NULL, NULL, NULL, 1)

--3)Users with the different types.

--Admins
INSERT INTO PostGradUser VALUES('ziadhassan12@gmail.com','Zizo123');
INSERT INTO PostGradUser VALUES('Ahmedtito@hotmail.com','Ronaldo777');

INSERT INTO Admin VALUES(1);
INSERT INTO Admin VALUES(2);

--Gucians
INSERT INTO PostGradUser VALUES('mariambahaa1998@gmail.com','Marioma23');
INSERT INTO PostGradUser VALUES('taherhassan1999@gmail.com','Taher12');
INSERT INTO PostGradUser VALUES('mohamedhassanein21@gmail.com','October25');
INSERT INTO PostGradUser VALUES('ziadmamdouh122@gmail.com','Mamdouh23');
INSERT INTO PostGradUser VALUES('ahmedkayed1988@hotmail.com','Kayooda123');
INSERT INTO PostGradUser VALUES('salmahossam21@outlook.com','2291Salma');
INSERT INTO PostGradUser VALUES('ammarkeshta11@gmail.com','Ammaritos22445613');
INSERT INTO PostGradUser VALUES('basselmaher99@hotmail.com','basabeso10023');
INSERT INTO PostGradUser VALUES('nouranmaged29@gmail.com','noura2392');

INSERT INTO GucianStudent VALUES(3,'mariam','bahaa','MA','engineering','street291,New maadi,building12,apartment2',2.0,null);
INSERT INTO GucianStudent VALUES(4,'taher','hassan','MA','engineering','street270,New maadi,building22,apartment11',3.0,null);
INSERT INTO GucianStudent VALUES(5,'mohamed','hassanein','MA','pharmacy','street12,New cairo,building2,apartment9',0.8,401231);
INSERT INTO GucianStudent VALUES(6,'ziad','mamdouh','PhD','management','street90,6october,building7,apartment12',1.0,2814172);
INSERT INTO GucianStudent VALUES(7,'ahmed','kayed','Phd','engineering','street7,nasr city,building7,apartment88',0.7,402724);
INSERT INTO GucianStudent VALUES(8,'salma','hossam','PhD','pharmacy','street11,New cairo,building2,apartment11',1.3,315531);
INSERT INTO GucianStudent VALUES(9,'ammar','keshta','PhD','pharmacy','street15,old cairo,building22,apartment53',1.1,3412234);
INSERT INTO GucianStudent VALUES(10,'bassel','maher','PhD','pharmacy','street233,old maadi,building14,apartment31',2.8,3717351);
INSERT INTO GucianStudent VALUES(11,'nouran','maged','PhD','management','street2,heliopolis,building71,apartment42',1.4,281272);

INSERT INTO GUCStudentPhoneNumber VALUES(3,'01016851010');
INSERT INTO GUCStudentPhoneNumber VALUES(3,'01016851012');
INSERT INTO GUCStudentPhoneNumber VALUES(4,'01212741011');
INSERT INTO GUCStudentPhoneNumber VALUES(5,'01515891527');
INSERT INTO GUCStudentPhoneNumber VALUES(6,'01118510132');
INSERT INTO GUCStudentPhoneNumber VALUES(9,'01522381621');
INSERT INTO GUCStudentPhoneNumber VALUES(10,'01012574122');
INSERT INTO GUCStudentPhoneNumber VALUES(11,'01124842121');

--Non-gucians
INSERT INTO PostGradUser VALUES('omartarek@gmail.com','Tarook923');
INSERT INTO PostGradUser VALUES('Ahmedelshafey001@gmail.com','Elshafey333');
INSERT INTO PostGradUser VALUES('faridakamel111@gmail.com','Dida223');
INSERT INTO PostGradUser VALUES('kareemomar19@gmail.com','Qwerty1999');
INSERT INTO PostGradUser VALUES('joeahmed291@outlook.com','Superjoe291');
INSERT INTO PostGradUser VALUES('itenamer21@gmail.com','iteniteniten28');

INSERT INTO NonGucianStudent VALUES(12,'omar','tarek','MA','engineering','street11,New cairo,building2,apartment22',4.0);
INSERT INTO NonGucianStudent VALUES(13,'ahmed','elshafey','MA','management','street99,New cairo,building23,apartment15',3.2);
INSERT INTO NonGucianStudent VALUES(14,'farida','kamel','PhD','pharmacy','street14,6october,building2,apartment7',null);
INSERT INTO NonGucianStudent VALUES(15,'kareem','omar','PhD','engineering','street1,rehab city,building14,apartment6',3.9);
INSERT INTO NonGucianStudent VALUES(16,'youssef','ahmed','PhD','engineering','dist18,new cairo,building4,apartment16',2.5);
INSERT INTO NonGucianStudent VALUES(17,'iten','amer','MA','pharmacy','street11,heliopolis,building21,apartment27',3.1);

INSERT INTO NonGUCStudentPhoneNumber VALUES(12,'01016451259');
INSERT INTO NonGUCStudentPhoneNumber VALUES(13,'01218244051');
INSERT INTO NonGUCStudentPhoneNumber VALUES(14,'01512531720');
INSERT INTO NonGUCStudentPhoneNumber VALUES(15,'01115314036');
INSERT INTO NonGUCStudentPhoneNumber VALUES(16,'01012359176');
INSERT INTO NonGUCStudentPhoneNumber VALUES(17,'01222569468');

--Supervisors
INSERT INTO PostGradUser VALUES('abdullahaboelmagd@gmail.com','Abdooo99');
INSERT INTO PostGradUser VALUES('heshamahmedgamal@gmail.com','Hesho1996');
INSERT INTO PostGradUser VALUES('youssefamir1999@gmail.com','Joey122');

INSERT INTO Supervisor VALUES(18,'Abdullah Abo Elmagd','engineering');
INSERT INTO Supervisor VALUES(19,'Hesham Ahmed Gamal','management');
INSERT INTO Supervisor VALUES(20,'Youssef Amir','pharmacy');

--Examiners
INSERT INTO PostGradUser VALUES('monicabotros01@gmail.com','Monica212');
INSERT INTO PostGradUser VALUES('joumanagameel222@gmail.com','Friends4ever');
INSERT INTO PostGradUser VALUES('magedfady@gmail.com','Batman999');
INSERT INTO PostGradUser VALUES('carolahmed@gmail.com','Carol123')

INSERT INTO Examiner VALUES(21,'Monica Botros','engineering',0);
INSERT INTO Examiner VALUES(22,'Joumana Gameel','management',1);
INSERT INTO Examiner VALUES(23,'Maged Fady','pharmacy',0);
INSERT INTO Supervisor VALUES(24,'Carol Ahmed','pharmacy');

--linking theses
INSERT INTO GUCianStudentRegisterThesis VALUES(3,18,2);
INSERT INTO GUCianStudentRegisterThesis VALUES(4,18,4);
INSERT INTO GUCianStudentRegisterThesis VALUES(5,18,8);
INSERT INTO GUCianStudentRegisterThesis VALUES(6,20,1);
INSERT INTO GUCianStudentRegisterThesis VALUES(7,20,9);
INSERT INTO GUCianStudentRegisterThesis VALUES(8,20,11);
INSERT INTO GUCianStudentRegisterThesis VALUES(9,20,13);
INSERT INTO GUCianStudentRegisterThesis VALUES(10,19,14);
INSERT INTO GUCianStudentRegisterThesis VALUES(11,19,15);

INSERT INTO NonGUCianStudentRegisterThesis VALUES(12,18,6);
INSERT INTO NonGUCianStudentRegisterThesis VALUES(13,19,7);
INSERT INTO NonGUCianStudentRegisterThesis VALUES(14,18,12);
INSERT INTO NonGUCianStudentRegisterThesis VALUES(15,20,5);
INSERT INTO NonGUCianStudentRegisterThesis VALUES(16,20,3);
INSERT INTO NonGUCianStudentRegisterThesis VALUES(17,18,10);

--4)Courses 
INSERT INTO Course VALUES(1000, 4, 'MATH 502')
INSERT INTO Course VALUES(3000, 6, 'CSEN 501')
INSERT INTO Course VALUES(2000, 6, 'CSEN 503')
INSERT INTO Course VALUES(3000, 8, 'PHCM561')
INSERT INTO Course VALUES(1000, 4, 'PHBL511')
INSERT INTO Course VALUES(2000, 3, 'PHTC521')
INSERT INTO Course VALUES(1000, 6, 'OPER 501')
INSERT INTO Course VALUES(3000, 8, 'FINC 504')
INSERT INTO Course VALUES(2000, 4, 'MGMT 502')

INSERT INTO NonGucianStudentTakeCourse VALUES(12, 1,NULL)
INSERT INTO NonGucianStudentTakeCourse VALUES(13, 2, 40)
INSERT INTO NonGucianStudentTakeCourse VALUES(14, 3, 92.98)
INSERT INTO NonGucianStudentTakeCourse VALUES(15, 4, 95.89)
INSERT INTO NonGucianStudentTakeCourse VALUES(16, 5, 99.20)
INSERT INTO NonGucianStudentTakeCourse VALUES(17, 6, 86.78)
INSERT INTO NonGucianStudentTakeCourse VALUES(17, 2, 80.98)


INSERT INTO NonGucianStudentPayForCourse VALUES(12, 4, 1)
INSERT INTO NonGucianStudentPayForCourse VALUES(13, 11, 2)
INSERT INTO NonGucianStudentPayForCourse VALUES(14, 5, 3)
INSERT INTO NonGucianStudentPayForCourse VALUES(15, 3, 4)
INSERT INTO NonGucianStudentPayForCourse VALUES(16, 7, 5)
INSERT INTO NonGucianStudentPayForCourse VALUES(17, 10, 6)
INSERT INTO NonGucianStudentPayForCourse VALUES(17, 12, 2)

Insert Into Defense Values(1,'2020/01/05','New Cairo',75.53)
Insert Into Defense Values(2,'2018/03/17','Giza',NULL)
Insert Into Defense Values (3,'2016/12/25','Nasr City',99.22)
Insert Into ExaminerEvaluateDefense Values('2020/01/05',1,21,'The thesis idea still needs some refinment')
Insert Into ExaminerEvaluateDefense Values('2018/03/17',2,22,NULL)
Insert Into ExaminerEvaluateDefense Values('2016/12/25',3,21,'Excelent')

INSERT INTO Publication Values('How to live Healthy','2020/04/12 19:02:23','Bolaq El Dakror',0,'Hall of Bolaq');
INSERT INTO Publication Values('Best medications for the mind and soul','2020/02/12 17:32:13','Saft el laban',1,'Milky conference');
Insert Into Publication Values ('Engineering Materials to use','2018/05/22 20:15:30','Luxor',1,'Confrence of Engineering Materials')
Insert Into Publication Values ('Engineering Materials not to use','2018/09/22 20:30:30','Nasr City',1,'Confrence of Nasr')

INSERT INTO ThesisHasPublication VALUES(1,1)
INSERT INTO ThesisHasPublication VALUES(1,2)
Insert Into ThesisHasPublication Values (2,3)
Insert Into ThesisHasPublication Values (2,4)

--5)Progress Reports
Insert Into GUCianProgressReport Values (3,1,'2022/12/12',0,4,'This progress report is below average',2,18);
Insert Into GUCianProgressReport Values (3,2,'2020/1/1',null,null,null,2,18);
Insert Into GUCianProgressReport Values (5,1,'2020/10/10',2,6,'The progress report can be improved',8,18);
Insert Into GUCianProgressReport Values (7,1,'2021/8/8',null,null,null,9,20);
Insert Into GUCianProgressReport Values (9,1,'2018/9/12',0,2,'This progress report is below aveage',13,20);
Insert Into NonGUCianProgressReport Values (14,1,'2020/6/7',null,null,null,12,18);
Insert Into NonGUCianProgressReport Values (16,1,'2021/2/7',3,8,'Outstanding performance',3,20);