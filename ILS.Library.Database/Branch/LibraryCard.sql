CREATE TABLE [Branch].[LibraryCard]
(
	[LibraryCardId]             INT                     IDENTITY(1,1), 
    [Created]                   DATETIME                NOT NULL, 
    [Fees]                      DECIMAL(19, 4)          NOT NULL

    CONSTRAINT PK_LibraryCard PRIMARY KEY CLUSTERED
    (
        [LibraryCardId]
    )
)


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores library card details with fees.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'LibraryCard'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The surrogate primary key for this table.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'LibraryCard',
    @level2type = N'COLUMN',
    @level2name = N'LibraryCardId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time when the library card was issued.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'LibraryCard',
    @level2type = N'COLUMN',
    @level2name = N'Created'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Any outstanding fees on the library card.',
    @level0type = N'SCHEMA',
    @level0name = N'Branch',
    @level1type = N'TABLE',
    @level1name = N'LibraryCard',
    @level2type = N'COLUMN',
    @level2name = N'Fees'