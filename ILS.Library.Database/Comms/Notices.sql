CREATE TABLE [Comms].[Notices]
(
	[NoticeId] INT NOT NULL IDENTITY(1,1), 
    [Title] NVARCHAR(256) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [ValidFrom] DATETIMEOFFSET NOT NULL, 
    [ValidTo] DATETIMEOFFSET NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores transient front page notices.',
    @level0type = N'SCHEMA',
    @level0name = N'Comms',
    @level1type = N'TABLE',
    @level1name = N'Notices'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Surrogate primary key for the notice.',
    @level0type = N'SCHEMA',
    @level0name = N'Comms',
    @level1type = N'TABLE',
    @level1name = N'Notices',
    @level2type = N'COLUMN',
    @level2name = N'NoticeId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The title for the notice. This will be displayed as the notice header.',
    @level0type = N'SCHEMA',
    @level0name = N'Comms',
    @level1type = N'TABLE',
    @level1name = N'Notices',
    @level2type = N'COLUMN',
    @level2name = N'Title'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The content for the notice. This will be the body of the notice.',
    @level0type = N'SCHEMA',
    @level0name = N'Comms',
    @level1type = N'TABLE',
    @level1name = N'Notices',
    @level2type = N'COLUMN',
    @level2name = N'Content'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time the notice should be displayed from.',
    @level0type = N'SCHEMA',
    @level0name = N'Comms',
    @level1type = N'TABLE',
    @level1name = N'Notices',
    @level2type = N'COLUMN',
    @level2name = N'ValidFrom'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time the notice should be displayed to.',
    @level0type = N'SCHEMA',
    @level0name = N'Comms',
    @level1type = N'TABLE',
    @level1name = N'Notices',
    @level2type = N'COLUMN',
    @level2name = N'ValidTo'