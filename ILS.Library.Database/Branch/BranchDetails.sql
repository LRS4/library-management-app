CREATE TABLE [Branch].[BranchDetails]
(
	[BranchId]          INT                 IDENTITY(1,1), 
    [Address]           NVARCHAR(MAX)       NOT NULL, 
    [Description]       NVARCHAR(MAX)       NULL, 
    [ImageUrl]          NVARCHAR(MAX)       NULL, 
    [Name]              NVARCHAR(30)        NULL, 
    [OpenDate]          DATETIME            NULL, 
    [Telephone]         NVARCHAR(50)        NULL

    CONSTRAINT PK_BranchDetails PRIMARY KEY CLUSTERED
    (
        [BranchId]
    )
)
