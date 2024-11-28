CREATE DATABASE SpejderApplikationTestDB

-- Opretter Unit-tabellen
CREATE TABLE Unit (
    UnitID INT IDENTITY(1,1) PRIMARY KEY,
    Description NVARCHAR(255),
    UnitName NVARCHAR(100),
    Picture NVARCHAR(MAX),
    Link NVARCHAR(MAX)
);

-- Opretter Badge-tabellen
CREATE TABLE Badge (
    BadgeID INT IDENTITY(1,1) PRIMARY KEY,
    BadgeName NVARCHAR(100),
    Description NVARCHAR(255),
    Picture NVARCHAR(MAX),
    Link NVARCHAR(MAX)
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
    ActivityDescription NVARCHAR(255),
    Preparation NVARCHAR(MAX),
    Notes NVARCHAR(MAX),
	Activity NVARCHAR(500)
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