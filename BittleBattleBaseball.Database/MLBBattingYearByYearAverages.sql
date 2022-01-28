CREATE TABLE [dbo].[MLBBattingYearByYearAverages]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DateCompleted] DATETIME NOT NULL, 
    [HomeTeamId] INT NOT NULL, 
    [AwayTeamId] INT NOT NULL, 
    [HomeTeamScore] INT NOT NULL, 
    [HomeTeamRuns] INT NOT NULL, 
    [HomeTeamHits] INT NOT NULL, 
    [HomeTeamErrors] INT NOT NULL, 
    [DateStarted] DATETIME NOT NULL, 
    [HomeTeamYear] INT NOT NULL, 
    [AwayTeamScore] INT NOT NULL, 
    [AwayTeamRuns] INT NOT NULL, 
    [AwayTeamHits] INT NOT NULL, 
    [AwayTeamErrors] INT NOT NULL, 
    [Ballpark] NVARCHAR(50) NOT NULL, 
    [InningPlayed] INT NOT NULL, 
    [BBBLeagueId] INT NOT NULL
)
