CREATE TABLE [Asset].[LibraryAsset]
(
	[LibraryAssetId]			INT				    IDENTITY(1,1), 
    [Discriminator]             NVARCHAR(256)       NOT NULL,
    [Title]                     NVARCHAR(256)       NOT NULL, 
    [Author]                    NVARCHAR(256)       NULL,
    [DeweyIndex]                NVARCHAR(256)       NULL,
    [ISBN]                      NVARCHAR(256)       NULL,
    [Year]                      INT                 NOT NULL, 
    [Cost]                      DECIMAL(19, 4)      NOT NULL, 
    [ImageUrl]                  NVARCHAR(MAX)       NULL, 
    [NumberOfCopies]            INT                 NOT NULL, 
    [Director]                  NVARCHAR(256)       NULL,
    [LibraryCardId]             INT                 NULL,
    [LocationId]                INT                 NULL,
    [StatusId]                  INT                 NOT NULL

    CONSTRAINT PK_LibraryAsset PRIMARY KEY CLUSTERED
    (
        [LibraryAssetId]
    ),

    CONSTRAINT [FK_LibraryAsset_LibraryCardId] FOREIGN KEY
    (
        [LibraryCardId]
    ) REFERENCES [Branch].[LibraryCard]([LibraryCardId]),

    CONSTRAINT [FK_LibraryAsset_LocationId] FOREIGN KEY
    (
        [LocationId]
    ) REFERENCES [Branch].[BranchDetails]([BranchId]), 
    

    CONSTRAINT [FK_LibraryAsset_StatusId] FOREIGN KEY
    (
        [StatusId]
    ) REFERENCES [Asset].[Status]([StatusId]) 
)
