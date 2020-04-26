CREATE TABLE [Asset].[Checkout]
(
	[CheckoutId]            INT             IDENTITY(1,1), 
    [LibraryAssetId]        INT             NOT NULL, 
    [LibraryCardId]         INT             NULL, 
    [Since]                 DATETIME        NULL, 
    [Until]                 DATETIME        NULL

    CONSTRAINT PK_Checkout PRIMARY KEY CLUSTERED
    (
        [CheckoutId]
    )

    CONSTRAINT [FK_Checkout_LibraryAssetId] FOREIGN KEY
    (
        [LibraryAssetId]
    ) REFERENCES [Asset].[LibraryAsset]([LibraryAssetId]),

    CONSTRAINT [FK_Checkout_LibraryCardId] FOREIGN KEY
    (
        [LibraryCardId]
    ) REFERENCES [Branch].[LibraryCard]([LibraryCardId])


)
