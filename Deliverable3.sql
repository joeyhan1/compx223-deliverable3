SET DATEFORMAT dmy; 
-- Table definitions

--drop table location
CREATE TABLE location (
	locationX int NOT NULL,
	locationY int NOT NULL,
	roadSurface varchar(20),
	roadLane tinyint,
	speedLimit tinyint NOT NULL,
	location varchar(100) NOT NULL,
	road varchar(100),
	region nvarchar(100) NOT NULL, --Uses nvarchar to store the ū in Manawatū-Whanganui 
	primary key (locationX, locationY)
)

--drop table vehicle
CREATE TABLE vehicle (
	registrationNumber varchar(100) NOT NULL,
	make varchar(100),
	model varchar(100),
	colour varchar(100),
	registrationYear int,
	vehicleYear int,
	primary key (registrationNumber)
)

--drop table license
CREATE TABLE license (
	licenseNumber varchar(20) NOT NULL,
	type varchar(20) NOT NULL,
	expireDate date NOT NULL,
	issueDate date NOT NULL,
	primary key (licenseNumber),
	check (expireDate > issueDate)
)

--drop table offender
CREATE TABLE offender (
	offenderId int NOT NULL,
	fname varchar(100) NOT NULL,
	lname varchar(100),
	dateOfBirth date,
	gender varchar(100),
	phone varchar(100),
	streetAddress varchar(100) NOT NULL,
	cityAddress varchar(100) NOT NULL,
	regionAddress varchar(100) NOT NULL,
	licenseNumber varchar(20),
	primary key (offenderId),
	foreign key (licenseNumber) references license,
)


--drop table offence
CREATE TABLE offence (
	offenceId int,
	description varchar(100),
	speedAlleged int NOT NULL,
	dateTime datetime NOT NULL,
	icn int NOT NULL,
	locationX int NOT NULL,
	locationY int NOT NULL,
	rainConditions varchar(20),
	lightConditions varchar(20),
	windConditions varchar(20),
	registrationNumber varchar(100) NOT NULL,
	offenderId int NOT NULL,
	primary key (offenceId),
	foreign key (locationX, locationY) references location,
	foreign key (registrationNumber) references vehicle,
	foreign key (offenderId) references offender,
	check (icn >= 0),
	check (speedAlleged >= 0)
)

--drop table infringementNotice
CREATE TABLE infringementNotice (
	noticeNumber varchar(20) NOT NULL,
	issueDate date NOT NULL,
	amount decimal(8,2) NOT NULL check (amount >= 0),
	status varchar(50) default 'unpaid',
	offenceID int NOT NULL references offence(offenceID) --Have the foreign key reference here to have the (0,1):(1,1) cardinality.
	primary key (noticeNumber)
)



--Inserting data

INSERT INTO license (licenseNumber, type, issueDate, expireDate)
VALUES
    ('KA111112', 'full', '01-07-2016', '01-07-2026'),
    ('AB123456', 'full', '11-08-2014', '11-08-2024'),
    ('GD567789', 'restricted', '23-04-2018', '23-04-2023'),
    ('HG456789', 'learner', '11-02-2021', '11-02-2026'),
    ('BD345678', 'full', '22-07-2015', '22-07-2025'),
    ('EF123456', 'restricted', '05-11-2019', '05-11-2024'),
    ('PL123456', 'full', '01-03-2017', '01-03-2027'),
    ('KT142526', 'full', '14-09-2014', '14-09-2024'),
    ('UY122225', 'learner', '02-05-2021', '02-05-2026')

INSERT INTO vehicle (registrationNumber, make, model, colour, registrationYear, vehicleYear)
VALUES
    ('ABC102', 'FORD', 'ESCAPE', 'red', '2020', '2018'),
    ('BCD550', 'HONDA', 'CIVIC', 'white', '2019', '2017'),
    ('JJS108', 'MZADA', 'MX-5', 'red', '2018', '2016'),
    ('MKL090', 'MZADA', 'CX-3', 'green', '2016', '2015'),
    ('HIL567', 'NISSAN', 'LEAF', 'white', '2021', '2020'),
    ('KDD453', 'SUZUKI', 'IGNIS', 'black', '2016', '2014'),
    ('LSD786', 'TOYOTA', 'CAMRY', 'white', '2018', '2016'),
    ('GLG909', 'TOYOTA', 'COROLLA', 'black', '2017', '2016')

INSERT INTO location(locationX, locationY, location, road, roadLane, region, roadSurface, speedLimit)
VALUES
    ('1406914', '4915023', 'MIDLAND ST', 'PORTSMOUTH DRIVE', 2, 'Otago ', 'Sealed', 70),
    ('1799023', '5814728', 'SH 1N', 'KILLARNEY ROAD', 4, 'Waikato ', 'Sealed', 60),
    ('1741193', '5976099', 'SH 1N', 'SAUNDERS ROAD', 2, 'Auckland ', 'Sealed', 100),
    ('1790288', '5837387', 'RIVERVIEW ROAD', 'HAKARIMATA ROAD', 2, 'Waikato ', 'Sealed', 100),
    ('1615675', '5423627', 'TALBOT ST', 'ELIZABETH ST', 2, 'Tasman ', 'Sealed', 50),
    ('1838932', '5519398', 'PAHIATUA MANGAHAO ROAD', 'RIDGE ROAD NORTH', 2, N'Manawatū-Whanganui ', 'Sealed', 100),
    ('1893341', '5812746', 'JELLICOE ST', 'KING ST', 2, 'Bay of Plenty ', 'Sealed', 80),
    ('1570406', '5178315', 'SH 73 BROUGHAM', 'DURHAM ST', 5, 'Canterbury ', 'Sealed', 60),
    ('1702855', '5635351', 'ELTHAM ROAD', 'LOWER DUTHIE ROAD', 2, 'Taranaki ', 'Sealed', 100),
    ('1766331', '5907432', 'SH 1N', 'EAST TAMAKI OBR', 4, 'Auckland ', 'Sealed', 100),
    ('2037143', '5708472', 'GLADSTONE ROAD', 'DERBY ST', 2, 'Gisborne ', 'Sealed', 50),
    ('1876231', '5820156', 'CAMERON ROAD', 'CORNWALL ST', 2, 'Bay of Plenty ', 'Sealed', 50),
    ('1764151', '5588246', 'SH 3', 'RANGITATAU EAST ROAD', 2, N'Manawatū-Whanganui ', 'Sealed', 100),
    ('1574177', '5179893', 'RASEN PLACE', 'JOLLIE ST', 1, 'Canterbury ', 'Sealed', 50),
    ('1727746', '6012466', 'SH 1N', 'GLENMOHR ROAD', 2, 'Northland ', 'Sealed', 100),
    ('1571772', '5179558', 'ST ASAPH ST', 'FITZGERALD AVENUE', 2, 'Canterbury ', 'Sealed', 50),
    ('1768750', '5904546', 'REDOUBT ROAD', 'HOLLYFORD ROAD', 4, 'Auckland ', 'Sealed', 50),
    ('1767091', '5912669', 'CARDIFF ROAD', 'CINDY PLACE', 2, 'Auckland ', 'Sealed', 50),
    ('1746990', '5915134', 'GREAT NORTH ROAD', 'HEPBURN ROAD', 4, 'Auckland ', 'Sealed', 50),
    ('1746749', '5919711', 'ROYAL VIEW ROAD', 'MILICH TERRACE', 2, 'Auckland ', 'Sealed', 50),
    ('1746822', '5918596', 'TE ATATU ROAD', 'WAKELING AVENUE', 2, 'Auckland ', 'Sealed', 50),
    ('1754254', '5918542', 'GREAT NORTH ROAD', 'BULLOCK TRACK', 4, 'Auckland ', 'Sealed', 50),
    ('1756988', '5920099', 'COOK ST', 'MAYORAL DRIVE', 6, 'Auckland ', 'Sealed', 50),
    ('1748980', '5424883', 'RIDDIFORD ST', 'CONSTABLE ST', 2, 'Wellington ', 'Sealed', 50),
    ('1757842', '5918624', 'PARK ROAD', 'CARLTON GORE ROAD', 4, 'Auckland ', 'Sealed', 40),
    ('1772739', '5896209', 'SERVICE LANE', 'WOOD ST S', 2, 'Auckland ', 'Sealed', 50),
    ('1767320', '5901246', 'RUSSELL ROAD', 'KENT ROAD', 2, 'Auckland ', 'Sealed', 50),
    ('1795738', '5821837', 'MAUI ST', 'CHANAN PLACE', 2, 'Waikato ', 'Sealed', 50),
    ('1620461', '5428070', 'WHAKATU DRIVE', 'ANNESBROOK DRIVE', 2, 'Nelson ', 'Sealed', 50),
    ('1573027', '5179936', 'CASHEL ST', 'OLLIVIERS ROAD', 2, 'Canterbury ', 'Sealed', 50),
    ('1407260', '4917383', 'SH 88', 'RAVENSBOURNE ROAD W', 2, 'Otago ', 'Sealed', 50),
    ('1405160', '4917081', 'ROSS ST', 'CITY ROAD', 2, 'Otago ', 'Sealed', 50),
    ('1752196', '5947913', 'ROSARIO CRESCENT', 'THORBURN AVENUE', 1, 'Auckland ', 'Sealed', 50),
    ('1821245', '5461836', 'SOUTH BELT', 'MANCHESTER ST', 2, 'Wellington ', 'Sealed', 50),
    ('1749613', '5430810', 'BARNARD ST', 'ANNE ST', 2, 'Wellington ', 'Sealed', 50),
    ('1792127', '5826359', 'GREAT SOUTH ROAD', 'PARK ROAD', 2, 'Waikato ', 'Sealed', 100),
    ('1570298', '5182104', 'SPRINGFIELD ROAD', 'EDGEWARE ROAD', 2, 'Canterbury ', 'Sealed', 50),
    ('1819887', '5531206', 'BENMORE AVENUE', 'WALTHAM COURT', 2, N'Manawatū-Whanganui ', 'Sealed', 50),
    ('1755196', '5434483', 'SH 2', 'HOROKIWI ROAD', 4, 'Wellington ', 'Sealed', 100),
    ('1691064', '5673772', 'OMATA ROAD', 'TUKAPA ST', 2, 'Taranaki ', 'Sealed', 50),
    ('1751978', '5432494', 'CENTENNIAL NBD', 'SLIP ROAD', 2, 'Wellington ', 'Sealed', 80),
    ('1801774', '5814937', 'BRIDGE ST', 'VON TEMPSKY ST', 2, 'Waikato ', 'Sealed', 50),
    ('1770163', '5902044', 'CHARLES PREVOST DRIVE', 'HILL ROAD', 2, 'Auckland ', 'Sealed', 50),
    ('1766663', '5912929', 'REEVES ROAD', 'TI RAKAU DRIVE', 2, 'Auckland ', 'Sealed', 50),
    ('1887078', '5824220', 'PAPAMOA BEACH ROAD', 'PACIFIC VIEW ROAD', 2, 'Bay of Plenty ', 'Sealed', 60),
    ('1803213', '5550233', 'SH 1N', 'CRITERION ST', 2, N'Manawatū-Whanganui ', 'Sealed', 50),
    ('1756295', '5920546', 'VICTORIA ST WEST', 'FRANKLIN ROAD', 6, 'Auckland ', 'Sealed', 50),
    ('1978456', '5670137', 'SH 2', 'KIWI VALLEY ROAD', 2, 'Hawkes Bay ', 'Sealed', 100),
    ('1759917', '5436791', 'MELLING LINK', 'RUTHERFORD ST', 2, 'Wellington ', 'Sealed', 50)

INSERT INTO offender (offenderId, fname, lname, dateOfBirth, gender, phone, streetaddress, cityaddress, regionaddress, licenseNumber)
VALUES
	(1, 'Colin', 'Pilbrow', '15-05-1992', 'Male', '0214434543', 'Gate 1 Knighton Road', 'Hamilton', 'Waikato', 'KA111112'),
	(2, 'Shaoqun', 'Wu', '15-05-1992', 'Female',  '0274454435', 'Gate 1 Knighton Road', 'Hamilton', 'Waikato', 'AB123456'),
	(3, 'Nilesh', 'Kanji', '15-05-1992', 'Male',  '0202344321', 'Gate 1 Knighton Road', 'Hamilton', 'Waikato', null),
	(4,'Solomon','Porteous','22-11-1984','Male','436-400-4380','436-400-4380','480 Northwestern Center','Dordrecht','AB123456'),
(5,'Josephina','Ashworth','22-07-1982','Female','935-963-6352','935-963-6352','9674 Onsgard Trail','Yuezhao','UY122225'),
(6,'Rodrick','Vautrey','29-01-1993','Male','404-760-9027','404-760-9027','08 Carey Alley','General Conesa','GD567789'),
(7,'Craggy','Scrimgeour','17-04-1983','Male','595-710-2161','595-710-2161','1551 Green Ridge Lane','Paingar','GD567789'),
(8,'Guthrey','Stannion','01-08-1987','Male','935-703-9569','935-703-9569','8 Banding Avenue','Ravne na Korem','HG456789'),
(9,'Cristian','Darwent','26-10-1992','Male','604-740-0235','604-740-0235','73693 Lukken Drive','Zelenodolsk','UY122225'),
(10,'Durante','Stadding','15-09-1988','Male','650-295-3053','650-295-3053','4529 Messerschmidt Junction','Verkhneye Kazanishche','UY122225'),
(11,'Chicky','Large',null,'Female','220-988-0084','220-988-0084','397 Leroy Junction','Tianzishan','KT142526'),
(12,'Milton','Evenden','24-06-1985','Male','864-436-6190','864-436-6190','2 Columbus Junction','Tagiura','AB123456'),
(13,'Cathie','Boylan','08-04-1995','Female','406-198-5610','406-198-5610','837 Roxbury Junction','Klumbu','KA111112'),
(14,'Barbette','Downage',null,'Female','460-430-6984','460-430-6984','43019 Schurz Point','Daet','AB123456'),
(15,'Jeremiah','Brightey','15-03-2001','Male','942-655-0820','942-655-0820','05875 Tennyson Pass','Himeji','AB123456'),
(16,'Marshal','Balassa','15-08-2000','Male','397-725-8681','397-725-8681','54948 Gerald Plaza','Dushan','AB123456'),
(17,'Sukey','Lewis','15-07-1983',null,'412-602-4116','412-602-4116','393 Lakewood Gardens Trail','Maorshan','KT142526'),
(18,'Kennith','Wogan','12-12-1997','Male','333-885-9185','333-885-9185','4 Debra Park','Enskede','HG456789'),
(19,'Alys','McCroft','26-01-1984','Female','646-636-7248','646-636-7248','11 Logan Road','New York City','PL123456'),
(20,'Daffie','Roos','24-11-1983',null,'193-711-7031','193-711-7031','90 Texas Circle','Balitai','BD345678'),
(21,'Myron','Cram',null,'Male','967-850-6377','967-850-6377','31696 Del Sol Drive','Donskoy','PL123456'),
(22,'Kendra','Bridle','14-02-1989','Female','364-625-6049','364-625-6049','2 Dakota Lane','Wudabao','KT142526'),
(23,'Leeanne','Wiffler','02-11-1989','Female','206-477-9606','206-477-9606','7 Pepper Wood Street','Ire','HG456789'),
(24,'Franchot','Woolner','20-11-1997','Male','286-735-9182','286-735-9182','90 Burning Wood Lane','Fresno','HG456789'),
(25,'Rowen','Saing','05-03-1997','Male','205-318-4763','205-318-4763','77048 Daystar Terrace','Porto Feliz','KT142526'),
(26,'Sydney','O''Heyne','25-10-1982','Female','327-744-9541','327-744-9541','139 Waywood Avenue','Kuantan','AB123456'),
(27,'Lewiss','Lennarde','01-02-2002','Male','562-320-8914','562-320-8914','18 American Drive','Palermo','PL123456'),
(28,'Donny','Wellbank','16-10-1992','Female','417-830-8667','417-830-8667','879 Melvin Parkway','Bolobo','UY122225'),
(29,'Junette','Postans','15-10-1995',null,'843-865-5129','843-865-5129','64 Meadow Ridge Road','Kveda Chkhorotsu','BD345678'),
(30,'Clea','Kittredge','16-11-1984','Female','293-867-9618','293-867-9618','06679 Dapin Plaza','Jacobina','GD567789'),
(31,'Dwain','Seery','16-11-1987','Male','843-679-9177','843-679-9177','13983 Dovetail Circle','Rimba Sekampung','KT142526'),
(32,'Maybelle','Lages','09-06-1981','Female','874-967-2048','874-967-2048','494 School Hill','Stockholm','KA111112'),
(33,'Mickie','Mougel','20-02-1983','Male','887-323-3457','887-323-3457','298 Maywood Lane','Yanshou','PL123456'),
(34,'Kit','Beastall','11-01-1985','Male','501-744-3232','501-744-3232','29 Portage Crossing','Nyagan','AB123456'),
(35,'Evangelina','Irvin','24-11-1994','Female','836-529-4840','836-529-4840','82 Muir Junction','Kuruman','KA111112'),
(36,'Renaud','Antonacci','04-11-1994',null,'844-159-9094','844-159-9094','01960 Bluejay Alley','Tuchkovo','AB123456'),
(37,'Toby','Swarbrigg','05-08-1981',null,'797-183-2196','797-183-2196','7 Karstens Place','Paris La Dfense','AB123456'),
(38,'Hanson','Djurisic','14-08-1994','Male','384-972-6326','384-972-6326','4290 Glacier Hill Center','Kedungasem','GD567789'),
(39,'Calv','Rhoddie','12-04-1995','Male','165-627-6969','165-627-6969','35 Buell Road','Aghavnadzor','AB123456'),
(40,'Fritz','Happert',null,'Male','902-264-1484','902-264-1484','4 Arapahoe Center','Serawai','PL123456'),
(41,'Agnella','Nielson','18-12-1984','Female','492-519-4009','492-519-4009','1 Holmberg Lane','Ays','EF123456'),
(42,'Benjie','Pardy','24-12-1994','Male','423-378-8959','423-378-8959','3538 Cherokee Pass','Kobelyaky','GD567789'),
(43,'Sherman','Beesley','21-08-1986',null,'718-977-0954','718-977-0954','3900 Northport Avenue','Alurbulu','KT142526'),
(44,'Merle','Moreland','11-06-2001',null,'856-530-3803','856-530-3803','57686 Ludington Road','Lingshan','KA111112'),
(45,'Karlotta','Laimable','14-04-2002','Female','358-233-6836','358-233-6836','152 Clyde Gallagher Hill','Nagrog','GD567789'),
(46,'Shermie','Glanville','29-06-2001','Male','964-371-4357','964-371-4357','6 Fuller Road','Mangli','EF123456'),
(47,'Nessa','Jina','21-01-1996','Female','853-752-4348','853-752-4348','181 Scott Center','Xiachu','HG456789'),
(48,'Morly','Conboy','24-10-1997',null,'726-865-7567','726-865-7567','855 Crownhardt Court','Toroy','PL123456'),
(49,'Lloyd','Chifney','24-08-2000','Male','619-145-9071','619-145-9071','678 West Parkway','Huangcun','KA111112'),
(50,'Payton','Jesson','26-10-1981',null,'325-628-5779','325-628-5779','097 Bartelt Court','Tha','BD345678')



INSERT INTO offence (offenceID, description, speedAlleged, dateTime, icn, locationX, locationY, rainConditions, lightConditions, windConditions, offenderId, registrationNumber)
VALUES
	(1, 'Speeding', 130, '15-05-2022 18:32:00', 614223004, 1819887, 5531206, 'Rainy', 'Bright', null, 1 ,'MKL090'),
	(2, 'Risky Driving', 108, '14-05-2022 7:49:12', 614223004, 1759917, 5436791, 'Wet', 'Dark', null, 2 ,'KDD453'),
	(3, 'Speeding', 150, '13-05-2022 21:12:21', 614223004, 1406914, 4915023, null, 'Dark', null, 3 ,'GLG909'),
	(4,'Racing',124,'10-08-2021 17:01:05',633448,1893341,5812746,'Rainy','Dim','Stormy',4,'KDD453'),
(5,'Racing',125,'04-04-2022 16:13:56',633353,1767320,5901246,'Stormy','Dark',null,5,'LSD786'),
(6,'Speeding',145,'01-11-2021 19:08:57',201535,1770163,5902044,'Stormy','Light','Stormy',6,'HIL567'),
(7,'Risky Driving',132,'15-03-2022 04:01:09',374288,1749613,5430810,'Dry','Light','Light',7,'GLG909'),
(8,'Speeding',102,'26-11-2021 10:07:15',493630,1746749,5919711,'Dry','Dim','Gusty',8,'JJS108'),
(9,'Racing',111,'12-12-2021 08:11:05',417500,1887078,5824220,'Rainy','Dark','Stormy',9,'HIL567'),
(10,'Speeding',142,'01-05-2022 19:29:13',358177,1887078,5824220,null,'Dim','Stormy',10,'KDD453'),
(11,'Speeding',122,'01-06-2021 20:06:13',560225,1727746,6012466,'Rainy','Light','Light',11,'BCD550'),
(12,'Speeding',140,'15-11-2021 01:18:59',630474,1772739,5896209,'Dry','Light','Gusty',12,'HIL567'),
(13,'Risky Driving',133,'30-10-2021 12:42:21',633110,1746749,5919711,null,'Light','Stormy',13,'MKL090'),
(14,'Speeding',126,'22-09-2021 17:28:28',675957,1405160,4917081,'Rainy',null,'Strong',14,'ABC102'),
(15,'Speeding',128,'14-05-2022 00:41:27',500766,1574177,5179893,'Rainy',null,'Still',15,'LSD786'),
(16,'Speeding',143,'11-03-2022 03:01:13',301053,1795738,5821837,null,'Dark','Stormy',16,'LSD786'),
(17,'Speeding',141,'16-05-2022 20:45:03',354773,1406914,4915023,'Stormy','Light',null,17,'KDD453'),
(18,'Speeding',131,'05-11-2021 12:38:48',638032,1767320,5901246,null,null,'Light',18,'BCD550'),
(19,'Racing',145,'04-09-2021 21:22:45',354627,1838932,5519398,'Stormy','Light','Strong',19,'BCD550'),
(20,'Racing',101,'13-02-2022 13:17:53',521267,1821245,5461836,'Wet','Light',null,20,'GLG909'),
(21,'Speeding',114,'18-05-2022 06:37:08',560221,1767320,5901246,'Dry','Dim','Still',21,'HIL567'),
(22,'Speeding',119,'06-10-2021 00:34:53',357570,1741193,5976099,'Wet','Dim',null,22,'ABC102'),
(23,'Risky Driving',139,'05-07-2021 16:34:50',676732,1876231,5820156,'Stormy','Light',null,23,'BCD550'),
(24,'Speeding',139,'03-07-2021 06:54:03',356859,1727746,6012466,'Wet',null,null,24,'KDD453'),
(25,'Speeding',131,'16-02-2022 04:09:20',304184,1838932,5519398,'Wet','Light','Still',25,'MKL090'),
(26,'Racing',114,'15-01-2022 15:22:28',630421,1406914,4915023,'Rainy','Light','Light',26,'HIL567'),
(27,'Racing',122,'24-06-2021 18:01:59',630485,1893341,5812746,'Stormy','Dim','Still',27,'LSD786'),
(28,'Speeding',139,'01-05-2022 03:21:18',374622,1702855,5635351,'Wet','Dark','Still',28,'HIL567'),
(29,'Risky Driving',141,'28-08-2021 00:11:30',353319,1573027,5179936,null,'Dim','Still',29,'GLG909'),
(30,'Risky Driving',143,'14-10-2021 09:36:32',356121,1799023,5814728,'Rainy','Dim','Still',30,'GLG909'),
(31,'Risky Driving',128,'24-07-2021 15:42:45',560221,1819887,5531206,'Wet','Light','Still',31,'GLG909'),
(32,'Speeding',124,'30-11-2021 16:55:20',356365,1768750,5904546,'Stormy','Dim','Stormy',32,'MKL090'),
(33,'Racing',107,'27-04-2022 19:25:42',358543,1759917,5436791,null,null,null,33,'BCD550'),
(34,'Risky Driving',104,'13-11-2021 16:31:53',677158,1573027,5179936,null,'Dark','Gusty',34,'KDD453'),
(35,'Speeding',126,'16-05-2022 10:32:11',500235,1756295,5920546,'Dry','Light','Still',35,'MKL090'),
(36,'Racing',106,'06-10-2021 15:55:23',201427,1801774,5814937,'Dry','Dim','Stormy',36,'ABC102'),
(37,'Racing',112,'27-01-2022 13:34:32',589349,1574177,5179893,'Wet','Dim','Gusty',37,'MKL090'),
(38,'Risky Driving',131,'08-02-2022 16:14:57',353988,1757842,5918624,'Wet','Dark',null,38,'GLG909'),
(39,'Racing',135,'30-07-2021 07:31:12',356838,1406914,4915023,'Wet','Dark','Still',39,'KDD453'),
(40,'Risky Driving',115,'04-09-2021 22:36:51',372301,1571772,5179558,'Rainy',null,'Still',40,'ABC102'),
(41,'Risky Driving',141,'18-07-2021 16:43:15',404159,1748980,5424883,'Dry','Light','Strong',41,'GLG909'),
(42,'Racing',114,'14-06-2021 00:12:42',305308,1727746,6012466,'Rainy','Dark','Stormy',42,'JJS108'),
(43,'Racing',135,'10-12-2021 09:11:57',491340,1702855,5635351,'Dry','Dark',null,43,'ABC102'),
(44,'Speeding',120,'23-05-2022 22:15:51',358588,1876231,5820156,null,'Light','Still',44,'JJS108'),
(45,'Risky Driving',135,'08-09-2021 05:33:59',355908,1752196,5947913,'Stormy','Dark','Strong',45,'BCD550'),
(46,'Risky Driving',145,'18-07-2021 17:53:02',201542,1766663,5912929,'Dry',null,'Light',46,'HIL567'),
(47,'Risky Driving',110,'24-11-2021 16:01:25',356813,1570298,5182104,'Wet','Light','Light',47,'KDD453'),
(48,'Speeding',137,'09-12-2021 00:00:00',353388,1876231,5820156,'Stormy',null,null,48,'JJS108'),
(49,'Speeding',131,'02-06-2021 15:15:23',354679,1573027,5179936,'Wet','Light','Still',49,'LSD786'),
(50,'Risky Driving',147,'08-10-2021 09:46:01',404159,1764151,5588246,'Stormy','Dark',null,50,'JJS108')


INSERT INTO infringementNotice (noticeNumber, amount, status, issueDate, offenceID)
VALUES
	('PC0000001', 50.2, 'Unpaid', '15-05-2022', 1),
	('PC0000002', 350, 'Paid', '15-05-2022', 2),
	('PC0000003', 120.75, 'Paid', '15-05-2022', 3),
	('PC0000004',61.2, 'Unpaid','18-09-2021',40),
('PC0000005',38.8, 'Unpaid','24-12-2021',43),
('PC0000006',42.32, 'Paid','15-05-2022',28),
('PC0000007',54.16, 'Unpaid','11-05-2022',33),
('PC0000008',76, 'Paid','27-05-2022',3),
('PC0000009',84.08, 'Paid','01-08-2021',41),
('PC0000010',83.2, 'Unpaid','29-11-2021',12),
('PC0000011',62.8, 'Paid','13-08-2021',39),
('PC0000012',75.28, 'Paid','19-11-2021',18),
('PC0000013',87.6, 'Paid','01-08-2021',46),
('PC0000014',58.56, 'Unpaid','10-02-2022',37),
('PC0000015',72.64, 'Paid','07-08-2021',31),
('PC0000016',51.52, 'Paid','27-11-2021',34),
('PC0000017',20.32, 'Unpaid','28-06-2021',42),
('PC0000018',43.36, 'Paid','08-07-2021',27),
('PC0000019',68.08, 'Paid','30-05-2022',17),
('PC0000020',70.88, 'Unpaid','30-05-2022',35),
('PC0000021',74.4, 'Paid','29-05-2022',1),
('PC0000022',47.6, 'Unpaid','18-09-2021',19),
('PC0000023',82.32, 'Paid','19-07-2021',23),
('PC0000024',48.88, 'Unpaid','27-02-2022',20),
('PC0000025',55.04, 'Paid','28-05-2022',2),
('PC0000026',27.36, 'Unpaid','15-06-2021',11),
('PC0000027',35.28, 'Unpaid','02-03-2022',25)


select * from license
select * from vehicle
select * from location
select * from offender
select * from offence
select * from infringementNotice

select * from offence where offenceId = '25'
select COUNT(offenceId) from infringementNotice
select * from infringementNotice where status = 'Unpaid'

DELETE FROM offence WHERE offenceId = 51
DELETE FROM offence WHERE offenceId = 52

/* For Summary Report */

SELECT COUNT(offenceId) from offence

SELECT SUM(amount) from infringementNotice

SELECT AVG(o.speedAlleged-l.speedLimit) as Difference from offence o join location l on o.locationX = l.locationX AND o.locationY = l.locationY
select * from location
select * from offence

Select Top 1 DATEPART(dw, dateTime), count(*) from offence group by DATEPART(dw, dateTime) order by DATEPART(dw, dateTime) desc

/* For Charts */

Select DATEDIFF(YEAR, dateOfBirth, CURRENT_TIMESTAMP) as Age, count(*) from offence, offender 
Where offence.offenderId = offender.offenderId AND dateOfBirth IS NOT NULL 
Group by DATEDIFF(YEAR, dateOfBirth, CURRENT_TIMESTAMP)

Select colour, COUNT(colour) AS counter from vehicle v join offence o on v.registrationNumber = o.registrationNumber GROUP BY colour
select colour from vehicle v join offence o on v.registrationNumber = o.registrationNumber ORDER BY colour

select * from offender

drop table license
drop table vehicle
drop table location
drop table offender
drop table offence
drop table infringementNotice
-- ALl the overdue infringement notice 25,20,37.12,43 as of 06/05/2022--