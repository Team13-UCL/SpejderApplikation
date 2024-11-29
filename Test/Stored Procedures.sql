-- SP til at tilføje / redigere aktivitet
CREATE PROCEDURE spAddOrEditActivity
    @ActivityID INT,
    @ActivityDescription VARCHAR(500),
    @Preparation VARCHAR(500),
    @Notes VARCHAR(500),
    @Activity VARCHAR(500)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check om der skal opdateres eller indsættes
        IF @ActivityID IS NOT NULL AND @ActivityID != 0
        BEGIN
            -- Update logik
            UPDATE Activity
            SET ActivityDescription = @ActivityDescription,
                Preparation = @Preparation,
                Notes = @Notes,
                Activity = @Activity
            WHERE ActivityID = @ActivityID;
        END
        ELSE
        BEGIN
            -- Insert logik
            INSERT INTO Activity (ActivityDescription, Preparation, Notes, Activity)
            VALUES (@ActivityDescription, @Preparation, @Notes, @Activity);

            -- Returner det indsatte ActivityID
            SELECT SCOPE_IDENTITY() AS ActivityID;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback ved fejl
        ROLLBACK TRANSACTION;

        -- Genkaste fejlen for debugging
        THROW;
    END CATCH;
END;
GO

-- SP Til at tilføje eller redigere møde
CREATE PROCEDURE spAddOrEditMeeting
	@ActivityID INT,
	@MeetingID INT,
	@Date date,
	@Start time(7),
	@Stop time(7)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		-- Check om der skal opdateres eller indsættes
		IF @MeetingID IS NOT NULL AND @MeetingID != 0
		BEGIN
			-- Opdater logik
			UPDATE Meeting
			SET [Date] = @Date,
				[Start] = @Start,
				[Stop] = @Stop
			WHERE MeetingID = @MeetingID
			
			IF NOT EXISTS (
				SELECT 1
				FROM ActivityMeeting
				WHERE ActivityID = @ActivityID AND MeetingID = @MeetingID
			)
			BEGIN
				INSERT INTO ActivityMeeting(ActivityID, MeetingID)
				VALUES (@ActivityID, @MeetingID);
			END;
		END
		ELSE
		BEGIN
			-- Insert logik
			INSERT INTO Meeting ([Date], [Start], [Stop])
			VALUES (@Date, @Start, @Stop)

			-- Tildel MeetingID
			SET @MeetingID = SCOPE_IDENTITY()

			-- Insert til Linkin Table
			INSERT INTO ActivityMeeting (ActivityID, MeetingID)
			VALUES (@ActivityID, @MeetingID)
		END

		-- Returner indsatte MeetingID
			SELECT @MeetingID AS MeetingID

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		-- Rollback ved fejl
		ROLLBACK TRANSACTION

		-- Genkast fejl for debugging
		THROW
	END CATCH
END

-- SP til at tilføje eller redigere mærke
CREATE PROCEDURE spAddOrEditBadge
	@ActivityID INT,
	@BadgeID INT,
	@BadgeName varchar(50),
	@Description varchar(500),
	@Picture varbinary(max),
	@Link varchar(100)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		--Check om at der skal opdateres eller indsættes
		IF @BadgeID IS NOT NULL AND @BadgeID != 0
		BEGIN
			--Opdater logik
			UPDATE Badge
			SET BadgeName = @BadgeName,
				[Description] = @Description,
				Picture = @Picture,
				Link = @Link
			WHERE BadgeID = @BadgeID

			IF NOT EXISTS (
				SELECT 1
				FROM ActivityBadge
				WHERE ActivityID = @ActivityID AND BadgeID = @BadgeID
			)
			BEGIN
				INSERT INTO ActivityBadge (ActivityID, BadgeID)
				VALUES (@ActivityID, @BadgeID);
			END;
		END
		ELSE
		BEGIN
			-- Insert logik
			INSERT INTO Badge (BadgeName, [Description], Picture, Link)
			VALUES (@BadgeName, @Description, @Picture, @Link)

			-- Tildel BadgeID
			SET @BadgeID = SCOPE_IDENTITY()

			-- Insert til Linkin Table
			INSERT INTO ActivityBadge (ActivityID, BadgeID)
			VALUES (@ActivityID, @BadgeID)
		END

		-- Returner indsatte MeetingID
			SELECT @BadgeID AS BadgeID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		-- Rollback ved fejl
		ROLLBACK TRANSACTION

		-- Genkast fejl for debugging
		THROW
	END CATCH
END

-- SP til at tilføje forbindelse til en enhed
CREATE PROCEDURE spAddOrEditUnit
    @ActivityID INT,
    @UnitID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check om Unit allerede er tildelt aktiviteten
        IF NOT EXISTS (
            SELECT 1
            FROM ActivityUnit
            WHERE ActivityID = @ActivityID AND UnitID = @UnitID
        )
        BEGIN
            -- Indsæt ny relation mellem aktivitet og unit
            INSERT INTO ActivityUnit (ActivityID, UnitID)
            VALUES (@ActivityID, @UnitID);
        END;

        -- Afslut transaktionen
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback ved fejl
        ROLLBACK TRANSACTION;

        -- Genkast fejl for debugging
        THROW;
    END CATCH
END;
GO

-- Nulstilling af test database
CREATE PROCEDURE ResetDatabase
AS
BEGIN
    -- Deaktiver foreign key constraints
    ALTER TABLE ActivityUnit NOCHECK CONSTRAINT ALL;
    ALTER TABLE ActivityBadge NOCHECK CONSTRAINT ALL;
    ALTER TABLE ActivityMeeting NOCHECK CONSTRAINT ALL;

    -- Tøm tabellerne med TRUNCATE
    TRUNCATE TABLE ActivityBadge;
    TRUNCATE TABLE ActivityMeeting;
    TRUNCATE TABLE ActivityUnit;
    TRUNCATE TABLE Activity;
    TRUNCATE TABLE Badge;
    TRUNCATE TABLE Meeting;

    -- Genaktiver foreign key constraints
    ALTER TABLE ActivityUnit CHECK CONSTRAINT ALL;
    ALTER TABLE ActivityBadge CHECK CONSTRAINT ALL;
    ALTER TABLE ActivityMeeting CHECK CONSTRAINT ALL;

    PRINT 'Database reset complete. Unit table remains untouched.';
END

-- Hent alle ScoutsMeetings
CREATE PROCEDURE [dbo].[spGetAllScoutmeetings]
AS
BEGIN
    BEGIN TRY
        -- Hent alle møder og deres tilknyttede oplysninger
        SELECT DISTINCT 
            Activity.ActivityID,
            Activity.ActivityDescription,
            Activity.Notes,
            Unit.UnitID,
            Unit.UnitName,
            Meeting.MeetingID,
            Meeting.Date,
            Meeting.Start,
            Meeting.Stop,
            -- Brug LEFT JOIN for ActivityBadge for at tillade aktiviteter uden mærker
            COALESCE(Badge.Picture, CAST(0x AS VARBINARY(MAX))) AS BadgePicture, -- Hvis ingen badge, returneres tom VARBINARY
            Badge.BadgeID
        FROM Activity
        INNER JOIN ActivityUnit
            ON Activity.ActivityID = ActivityUnit.ActivityID
        INNER JOIN Unit
            ON ActivityUnit.UnitID = Unit.UnitID
        INNER JOIN ActivityMeeting
            ON Activity.ActivityID = ActivityMeeting.ActivityID
        INNER JOIN Meeting
            ON ActivityMeeting.MeetingID = Meeting.MeetingID
        LEFT JOIN ActivityBadge
            ON Activity.ActivityID = ActivityBadge.ActivityID
        LEFT JOIN Badge
            ON ActivityBadge.BadgeID = Badge.BadgeID
    END TRY
    BEGIN CATCH
        -- Fejlhåndtering: Hvis der opstår en fejl, returner en besked
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END

-- Hent specifik ScoutMeeting efter ActivityID
CREATE PROCEDURE [dbo].[spGetScoutmeetingByID] @ActivityID INT
AS
BEGIN
    BEGIN TRY
        -- Hent alle møder og deres tilknyttede oplysninger
        SELECT DISTINCT 
            Activity.ActivityID,
            Activity.ActivityDescription,
            Activity.Notes,
            Unit.UnitID,
            Unit.UnitName,
            Meeting.MeetingID,
            Meeting.Date,
            Meeting.Start,
            Meeting.Stop,
            -- Brug LEFT JOIN for ActivityBadge for at tillade aktiviteter uden mærker
            COALESCE(Badge.Picture, CAST(0x AS VARBINARY(MAX))) AS BadgePicture, -- Hvis ingen badge, returneres tom VARBINARY
            Badge.BadgeID
        FROM Activity
        INNER JOIN ActivityUnit
            ON Activity.ActivityID = ActivityUnit.ActivityID
        INNER JOIN Unit
            ON ActivityUnit.UnitID = Unit.UnitID
        INNER JOIN ActivityMeeting
            ON Activity.ActivityID = ActivityMeeting.ActivityID
        INNER JOIN Meeting
            ON ActivityMeeting.MeetingID = Meeting.MeetingID
        LEFT JOIN ActivityBadge
            ON Activity.ActivityID = ActivityBadge.ActivityID
        LEFT JOIN Badge
            ON ActivityBadge.BadgeID = Badge.BadgeID
		WHERE Activity.ActivityID = @ActivityID
    END TRY
    BEGIN CATCH
        -- Fejlhåndtering: Hvis der opstår en fejl, returner en besked
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END