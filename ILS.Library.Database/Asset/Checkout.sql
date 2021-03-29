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

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores checkout records for all library assets against library cards',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Checkout'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'This references the library asset that was checked out',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Checkout',
    @level2type = N'COLUMN',
    @level2name = N'LibraryAssetId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'This references the library card to which the asset was checked out',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Checkout',
    @level2type = N'COLUMN',
    @level2name = N'LibraryCardId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'When the asset has been checked out since.',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Checkout',
    @level2type = N'COLUMN',
    @level2name = N'Since'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'When the asset is checked out until.',
    @level0type = N'SCHEMA',
    @level0name = N'Asset',
    @level1type = N'TABLE',
    @level1name = N'Checkout',
    @level2type = N'COLUMN',
    @level2name = N'Until'