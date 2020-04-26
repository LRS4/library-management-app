CREATE TABLE [Asset].[CheckoutHistory]
(
	[CheckoutHistoryId]         INT             IDENTITY(1,1),
    [CheckedIn]                 DATETIME        NULL, 
    [CheckedOut]                DATETIME        NOT NULL, 
    [LibraryAssetId]            INT             NOT NULL, 
    [LibraryCardId]             INT             NOT NULL

    CONSTRAINT PK_CheckoutHistory PRIMARY KEY CLUSTERED
    (
        [CheckoutHistoryId]
    ),

    CONSTRAINT [FK_CheckoutHistory_LibraryCardId] FOREIGN KEY
    (
        [LibraryCardId]
    ) REFERENCES [Branch].[LibraryCard]([LibraryCardId]),
    
    CONSTRAINT [FK_CheckoutHistory_LibraryAssetId] FOREIGN KEY
    (
        [LibraryAssetId]
    ) REFERENCES [Asset].[LibraryAsset]([LibraryAssetId])
)
