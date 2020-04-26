CREATE TABLE [Branch].[BranchHours]
(
	[BranchHoursId]             INT             IDENTITY(1,1),
	[BranchId]                  INT             NULL, 
    [CloseTime]                 INT             NOT NULL, 
    [DayOfWeek]                 INT             NOT NULL, 
    [OpenTime]                  INT             NOT NULL

    CONSTRAINT PK_BranchHours PRIMARY KEY CLUSTERED
    (
        [BranchHoursId]
    )

    CONSTRAINT [FK_BranchHours_BranchId] FOREIGN KEY
    (
        [BranchId]
    ) REFERENCES [Branch].[BranchDetails]([BranchId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the branch in which the hours belong to.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'BranchHours',
    @level2type = N'COLUMN',
    @level2name = N'BranchId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The closing time of the branch.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'BranchHours',
    @level2type = N'COLUMN',
    @level2name = N'CloseTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The days of the week that the branch is open.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'BranchHours',
    @level2type = N'COLUMN',
    @level2name = 'DayOfWeek'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The hour at which the branch opens.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'BranchHours',
    @level2type = N'COLUMN',
    @level2name = N'OpenTime'