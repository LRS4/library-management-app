CREATE TABLE [Users].[Patron]
(
    [PatronId]                  INT                 IDENTITY(1,1),
    [Address]                   NVARCHAR(MAX)       NULL, 
    [DateOfBirth]               NVARCHAR(MAX)       NOT NULL, 
    [FirstName]                 NVARCHAR(MAX)       NULL, 
    [LastName]                  NVARCHAR(MAX)       NULL, 
    [Gender]                    NVARCHAR(50)        NULL,
    [TelephoneNumber]           NVARCHAR(50)        NULL,
    [HomeLibraryBranchId]       INT                 NOT NULL, 
    [LibraryCardId]             INT                 NOT NULL, 
    

    CONSTRAINT PK_Patron PRIMARY KEY CLUSTERED
    (
        [PatronId]
    ), 

    CONSTRAINT [FK_Patron_HomeLibraryBranchId] FOREIGN KEY
    (
        [HomeLibraryBranchId]
    ) REFERENCES [Branch].[BranchDetails]([BranchId]),

    CONSTRAINT [FK_Patron_LibraryCardId] FOREIGN KEY
    (
        [LibraryCardId]
    ) REFERENCES [Branch].[LibraryCard]([LibraryCardId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Surrogate primary key for this table.',
    @level0type = N'SCHEMA',
    @level0name = N'Users',
    @level1type = N'TABLE',
    @level1name = N'Patron',
    @level2type = N'COLUMN',
    @level2name = N'PatronId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The address of the patron.',
    @level0type = N'SCHEMA',
    @level0name = N'Users',
    @level1type = N'TABLE',
    @level1name = N'Patron',
    @level2type = N'COLUMN',
    @level2name = N'Address'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date of birth of the patron.',
    @level0type = N'SCHEMA',
    @level0name = N'Users',
    @level1type = N'TABLE',
    @level1name = N'Patron',
    @level2type = N'COLUMN',
    @level2name = N'DateOfBirth'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The first name of the patron.',
    @level0type = N'SCHEMA',
    @level0name = N'Users',
    @level1type = N'TABLE',
    @level1name = N'Patron',
    @level2type = N'COLUMN',
    @level2name = N'FirstName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The last name of the patron.',
    @level0type = N'SCHEMA',
    @level0name = N'Users',
    @level1type = N'TABLE',
    @level1name = N'Patron',
    @level2type = N'COLUMN',
    @level2name = N'LastName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The contact telephone number of the patron.',
    @level0type = N'SCHEMA',
    @level0name = N'Users',
    @level1type = N'TABLE',
    @level1name = N'Patron',
    @level2type = N'COLUMN',
    @level2name = N'TelephoneNumber'