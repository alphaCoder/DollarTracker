CREATE TABLE [dbo].[AppSetting]
(
	[AppSettingId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NULL
)
