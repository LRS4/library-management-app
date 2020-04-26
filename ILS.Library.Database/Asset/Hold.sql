CREATE TABLE [Asset].[Hold]
(
	[HoldId]                INT             IDENTITY(1,1), 
    [HoldPlaced]            DATETIME        NOT NULL, 
    [LibraryAssetId]        INT             NULL, 
    [LibraryCardId]         INT             NULL

    CONSTRAINT PK_Hold PRIMARY KEY CLUSTERED
    (
        [HoldId]
    ),

    CONSTRAINT [FK_Hold_LibraryCardId] FOREIGN KEY
    (
        [LibraryCardId]
    ) REFERENCES [Branch].[LibraryCard]([LibraryCardId]),
    
    CONSTRAINT [FK_Hold_LibraryAssetId] FOREIGN KEY
    (
        [LibraryAssetId]
    ) REFERENCES [Asset].[LibraryAsset]([LibraryAssetId])
)
