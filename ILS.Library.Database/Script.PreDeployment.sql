/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'Asset' 
            AND  TABLE_NAME = 'Status'))
BEGIN  
    DELETE FROM [Asset].[LibraryAsset]; 
    DBCC CHECKIDENT ('Asset.LibraryAsset',RESEED, 0)

    DELETE FROM [Asset].[Status];
    DBCC CHECKIDENT ('[Asset].[Status]',RESEED, 0)

    DELETE FROM [Branch].[BranchDetails];
    DBCC CHECKIDENT ('[Branch].[BranchDetails]',RESEED, 0)

    DELETE FROM [Branch].[BranchHours];
    DBCC CHECKIDENT ('[Branch].[BranchHours]',RESEED, 0)

    DELETE FROM [Branch].[LibraryCard];
    DBCC CHECKIDENT ('[Branch].[LibraryCard]',RESEED, 0)

    DELETE FROM [Comms].[Notices];
    DBCC CHECKIDENT ('[Comms].[Notices]',RESEED, 0)

    DELETE FROM [Users].[Patron];
    DBCC CHECKIDENT ('[Users].[Patron]',RESEED, 0)
END  