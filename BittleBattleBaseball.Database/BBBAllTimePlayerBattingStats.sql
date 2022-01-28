CREATE TABLE [dbo].[BBBAllTimePlayerBattingStats]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PlayerId] INT NOT NULL, 
    [AB] INT NOT NULL, 
    [H] INT NOT NULL, 
    [G] INT NOT NULL, 
    [PA] INT NOT NULL, 
    [2B] INT NOT NULL, 
    [3B] INT NOT NULL, 
    [HR] INT NOT NULL, 
    [RBI] INT NOT NULL, 
    [SB] INT NOT NULL, 
    [CS] INT NOT NULL, 
    [BB] INT NOT NULL, 
    [PlayerSeason] INT NOT NULL
)
