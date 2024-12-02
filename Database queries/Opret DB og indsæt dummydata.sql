 

-- Opretter Unit-tabellen
CREATE TABLE Unit (
    UnitID INT IDENTITY(1,1) PRIMARY KEY,
    Description VARCHAR(255),
    UnitName VARCHAR(100),
    Link VARCHAR(500),
	
);

    CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(50),
    Password VARCHAR(25),
    IsLeader BIT,
    UnitID INT,
  FOREIGN KEY (UnitID) REFERENCES Unit(UnitID)
	);
-- Opretter Badge-tabellen
CREATE TABLE Badge (
    BadgeID INT IDENTITY(1,1) PRIMARY KEY,
    BadgeName VARCHAR(100),
    Description VARCHAR(255),
    Picture VARBINARY(Max),
    Link VARCHAR(500)
);

-- Opretter Meeting-tabellen
CREATE TABLE Meeting (
    MeetingID INT IDENTITY(1,1) PRIMARY KEY,
    Date DATE,
    Start TIME,
    Stop TIME
);

-- Opretter Activity-tabellen
CREATE TABLE Activity (
    ActivityID INT IDENTITY(1,1) PRIMARY KEY,
    ActivityDescription VARCHAR(255),
    Preparation VARCHAR(500),
    Notes VARCHAR(500),
	Activity VARCHAR(500)
);



-- Opretter ActivityUnit-tabellen
CREATE TABLE ActivityUnit (
    ActivityID INT,
    UnitID INT,
    PRIMARY KEY (ActivityID, UnitID),
    FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID) ON DELETE NO ACTION,
    FOREIGN KEY (UnitID) REFERENCES Unit(UnitID) ON DELETE NO ACTION
);

-- Opretter ActivityBadge-tabellen
CREATE TABLE ActivityBadge (
    ActivityID INT,
    BadgeID INT,
    PRIMARY KEY (ActivityID, BadgeID),
    FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID) ON DELETE NO ACTION,
    FOREIGN KEY (BadgeID) REFERENCES Badge(BadgeID) ON DELETE NO ACTION
);

-- Opretter ActivityMeeting-tabellen
CREATE TABLE ActivityMeeting (
    MeetingID INT,
    ActivityID INT,
    PRIMARY KEY (MeetingID, ActivityID),
    FOREIGN KEY (MeetingID) REFERENCES Meeting(MeetingID) ON DELETE NO ACTION,
    FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID) ON DELETE NO ACTION
);

INSERT INTO Badge (BadgeName, Description, Picture, Link) VALUES
('navigatør', 'Moral og etik', null, 'https://kfumspejderne.dk/maerke/navigatoer/'), -- Simpel JPEG binary header
('Superhelte', 'motoriske bevægelser, beskrive hvad man selv er god til', null, 'https://kfumspejderne.dk/maerke/superhelte/');

-- Dummydata for Meeting
INSERT INTO Meeting (Date, Start, Stop) VALUES
('2024-05-08', '17:00:00', '19:00:00'),
('2024-05-27', '17:00:00', '19:00:00');

 --Dummydata for Unit 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Familiespejder', '3 - 6 år', 'https://kfumspejderne.dk/spejder/familiespejder/')
 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Bævere','0 - 1 klasse', 'https://kfumspejderne.dk/spejder/baever/')
 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Ulvene', '2 - 3 klasse', 'https://kfumspejderne.dk/spejder/ulv/')
 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Junior', '4 - 5 klasse', 'https://kfumspejderne.dk/spejder/junior/')
 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Trop', '6 - 8 klasse', 'https://kfumspejderne.dk/spejder/trop/')
 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Senior', '9. klasse - 17 år', 'https://kfumspejderne.dk/spejder/senior/')
 
INSERT INTO Unit (UnitName, [Description], Link)
VALUES ('Rover', '17 år og derover', 'https://kfumspejderne.dk/spejder/rover/')

 --Dummydata for Activity
INSERT INTO Activity (ActivityDescription, Preparation, Notes, Activity) VALUES
('Snakke om "trafikregler" i forskellige kontekster', 'Husk Berit forbereder', 'anders kommer ikke', 'trafikregler' ),
('opsumer regler for høj dolk føring og Snitte træmænd', 'Husk at finde knive frem, john har ansvaret', 'mickey kommer ikke', 'knivting');

INSERT INTO Users (UserName, Password, IsLeader) VALUES
('Anders', '313', 1), 
('Mickey', 'pluto', 0); 
