CREATE TABLE [Asset].[Status]
(
	[StatusId]              INT                 IDENTITY(1,1), 
    [Description]           NVARCHAR(MAX)       NOT NULL, 
    [Name]                  NVARCHAR(50)        NOT NULL

    CONSTRAINT PK_Status PRIMARY KEY CLUSTERED
    (
        [StatusId]
    )
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores asset status information.',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Status'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The surrogate primary key for this table.',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Status',
    @level2type = N'COLUMN',
    @level2name = N'StatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The description of the asset status.',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Status',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the status such as available, checked out, on hold, lost.',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Status',
    @level2type = N'COLUMN',
    @level2name = N'Name'