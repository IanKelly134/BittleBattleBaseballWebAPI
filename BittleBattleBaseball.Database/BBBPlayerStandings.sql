CREATE TABLE [dbo].[BBBPlayerStandings]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [BBBPlayerId] INT NOT NULL, 
    [W] INT NOT NULL, 
    [L] INT NOT NULL, 
    [RunsFor] INT NOT NULL, 
    [RunsAgainst] INT NOT NULL, 
    [LastUpdated] DATETIME NOT NULL
)
