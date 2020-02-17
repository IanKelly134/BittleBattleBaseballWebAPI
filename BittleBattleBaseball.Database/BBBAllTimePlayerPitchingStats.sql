CREATE TABLE [dbo].[BBBAllTimePlayerPitchingStats]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PlayerId] INT NOT NULL, 
    [PlayerSeason] INT NOT NULL, 
    [G] INT NOT NULL, 
    [ERA] DECIMAL(3) NOT NULL, 
    [GS] INT NOT NULL, 
    [IP] INT NOT NULL, 
    [H] INT NOT NULL, 
    [R] INT NOT NULL, 
    [ER] INT NOT NULL, 
    [HR] INT NOT NULL, 
    [BB] INT NOT NULL, 
    [SO] INT NOT NULL, 
    [WHIP] DECIMAL(3) NOT NULL, 
    [HomeBBBPlayerId] INT NOT NULL, 
    [AwayBBBPlayerId] INT NOT NULL
)
