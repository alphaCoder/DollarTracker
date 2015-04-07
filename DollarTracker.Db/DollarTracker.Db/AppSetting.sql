CREATE TABLE [dbo].[AppSetting]
(
	[AppSettingId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(255) NULL, 
    [Value] VARCHAR(255) NOT NULL
)
