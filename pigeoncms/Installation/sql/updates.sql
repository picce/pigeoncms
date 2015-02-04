
--20090330
ALTER TABLE #__menu ADD
	overridePageTitle bit NULL
GO

--20090406
ALTER TABLE #__menu
	DROP COLUMN contentParams
GO

--20090406 togliere identity da #__menu.id

--20090407
ALTER TABLE #__immobilia_immobili ADD
	tipoUso int NULL
GO

--20090422
/*in case of contentType=alias, redirect to referMenuId menu entry*/
ALTER TABLE #__menu ADD
	referMenuId int NULL
GO


--20090527
ALTER TABLE #__modules ADD
	currView varchar(50) NULL
GO

--20090616
--add tables for categories, sections and items
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [#__categories](
	[id] [int] NOT NULL,
	[sectionId] [int] NULL,
	[parentId] [int] NULL,
	[enabled] [bit] NULL,
	[ordering] [int] NULL,
	[defaultImageName] [varchar](50) NULL,
 CONSTRAINT [PK_#__categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [#__categories_Culture]    Script Date: 06/16/2009 16:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [#__categories_Culture](
	[cultureName] [varchar](10) NOT NULL,
	[categoryId] [int] NOT NULL,
	[title] [varchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_categories_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[categoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [#__items]    Script Date: 06/16/2009 16:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [#__items](
	[id] [int] NOT NULL,
	[categoryId] [int] NULL,
	[enabled] [bit] NULL,
	[ordering] [int] NULL,
	[defaultImageName] [varchar](50) NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [varchar](50) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [varchar](50) NULL,
 CONSTRAINT [PK_#__items] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [#__items_Culture]    Script Date: 06/16/2009 16:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [#__items_Culture](
	[cultureName] [varchar](50) NOT NULL,
	[itemId] [int] NOT NULL,
	[title] [varchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__items_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[itemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [#__sections]    Script Date: 06/16/2009 16:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [#__sections](
	[id] [int] NOT NULL,
	[enabled] [bit] NULL,
 CONSTRAINT [PK_#__sections] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [#__sections_Culture]    Script Date: 06/16/2009 16:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [#__sections_Culture](
	[cultureName] [varchar](10) NOT NULL,
	[sectionId] [int] NOT NULL,
	[title] [varchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__sections_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[sectionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


--20090619
CREATE TABLE [#__cultures](
	[cultureCode] [varchar](10) NOT NULL,
	[displayName] [varchar](50) NULL,
	[enabled] [bit] NULL,
	[ordering] [int] NULL,
 CONSTRAINT [PK_#__cultures] PRIMARY KEY CLUSTERED 
(
	[cultureCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO #__cultures (cultureCode,displayName,enabled, ordering)  VALUES( 'it-IT','Italiano',1, 0)
INSERT INTO #__cultures (cultureCode,displayName,enabled, ordering)  VALUES( 'en-US','English',1, 1)
INSERT INTO #__cultures (cultureCode,displayName,enabled, ordering)  VALUES( 'es-ES','Español',0, 2)
INSERT INTO #__cultures (cultureCode,displayName,enabled, ordering)  VALUES( 'de-DE','Deutsch',0, 3)
INSERT INTO #__cultures (cultureCode,displayName,enabled, ordering)  VALUES( 'sl-SI','Slovenski',0, 4)

--20090619
/*
ALTER TABLE #__cultures ADD
	ordering int NULL
GO*/

--20090622
ALTER TABLE #__menu ADD
	currMasterPage varchar(50) NULL, 
	currTheme varchar(50) NULL
GO

--20090709
ALTER TABLE #__menu ADD
	cssClass varchar(50) NULL
GO

--20090824
CREATE TABLE [#__menu_Culture](
	[cultureName] [varchar](50) NOT NULL,
	[menuId] [int] NOT NULL,
	[title] [varchar](200) NULL,
 CONSTRAINT [PK_#__menu_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[menuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--20090914
ALTER TABLE #__menu ADD
	visible bit NULL
GO

--20091006
ALTER TABLE #__menu ADD
	routeId int NULL
GO

UPDATE #__menu SET routeId=1 WHERE routeId is null
GO

/****** Object:  Table [dbo].[#__routes]    Script Date: 10/06/2009 15:37:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[#__routes]') AND type in (N'U'))
BEGIN
CREATE TABLE [#__routes](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[pattern] [varchar](255) NULL,
	[published] [bit] NULL,
	[ordering] [int] NULL,
 CONSTRAINT [PK_#__routes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[#__routeParams]    Script Date: 10/06/2009 15:37:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[#__routeParams]') AND type in (N'U'))
BEGIN
CREATE TABLE [#__routeParams](
	[routeId] [int] NOT NULL,
	[paramKey] [varchar](50) NOT NULL,
	[paramValue] [varchar](255) NULL,
	[paramConstraint] [varchar](255) NULL,
	[paramDataType] [varchar](255) NULL,
 CONSTRAINT [PK_#__routeParams_1] PRIMARY KEY CLUSTERED 
(
	[routeId] ASC,
	[paramKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO


--20091012
ALTER TABLE #__items ADD
	CustomBool1 bit NULL,
	CustomBool2 bit NULL,
	CustomBool3 bit NULL,
	CustomDate1 datetime NULL,
	CustomDate2 datetime NULL,
	CustomDate3 datetime NULL,
	CustomDecimal1 decimal(18, 0) NULL,
	CustomDecimal2 decimal(18, 0) NULL,
	CustomDecimal3 decimal(18, 0) NULL,
	CustomInt1 int NULL,
	CustomInt2 int NULL,
	CustomInt3 int NULL,
	CustomString1 varchar(255) NULL,
	CustomString2 varchar(255) NULL,
	CustomString3 varchar(255) NULL,
	ItemParams varchar(500) NULL,
	itemType varchar(255) NULL,
	accessType int NULL,
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL	
GO

--20091023
ALTER TABLE #__modules ADD
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL	
GO

ALTER TABLE #__menu ADD
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL
GO


CREATE TABLE [#__memberUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](255) NULL,
	[applicationName] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[comment] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[passwordQuestion] [varchar](255) NULL,
	[passwordAnswer] [varchar](255) NULL,
	[isApproved] [bit] NULL,
	[lastActivityDate] [datetime] NULL,
	[lastLoginDate] [datetime] NULL,
	[lastPasswordChangedDate] [datetime] NULL,
	[creationDate] [datetime] NULL,
	[isOnLine] [bit] NULL,
	[isLockedOut] [bit] NULL,
	[lastLockedOutDate] [datetime] NULL,
	[failedPasswordAttemptCount] [int] NULL,
	[failedPasswordAttemptWindowStart] [datetime] NULL,
	[failedPasswordAnswerAttemptCount] [int] NULL,
	[failedPasswordAnswerAttemptWindowStart] [datetime] NULL,
	[enabled] [bit] NULL,
	[accessCode] [varchar](255) NULL,
	[accessLevel] [int] NULL,
 CONSTRAINT [PK_#__memberUsers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [#__roles](
	[rolename] [varchar](255) NOT NULL,
	[applicationName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_#__roles] PRIMARY KEY CLUSTERED 
(
	[rolename] ASC,
	[applicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [#__usersInRoles](
	[username] [varchar](255) NOT NULL,
	[rolename] [varchar](255) NOT NULL,
	[applicationName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_#__usersInRoles] PRIMARY KEY CLUSTERED 
(
	[username] ASC,
	[rolename] ASC,
	[applicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [#__permissions](
	[id] [int] NOT NULL,
	[rolename] [varchar](255) NOT NULL,
 CONSTRAINT [PK_#__permissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[rolename] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--20091102
CREATE TABLE [#__paypalTmpOrders](
	[id] [int] NOT NULL,
	[amount] [decimal](18, 2) NULL,
	[dateInserted] [datetime] NULL,
	[dateUpdated] [datetime] NULL,
	[confirmed] [bit] NULL,
 CONSTRAINT [PK_#__paypalTmpOrders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [#__events](
	[id] [int] NOT NULL,
	[name] [varchar](255) NULL,
	[eventStart] [datetime] NULL,
	[eventEnd] [datetime] NULL,
	[resourceId] [int] NULL,
	[status] [int] NULL,
	[groupId] [int] NULL,
	[description] [varchar](500) NULL,
	[orderId] [int] NULL,
 CONSTRAINT [PK_#__events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--20091112
ALTER TABLE #__routes ADD
	currMasterPage varchar(50) NULL,
	currTheme varchar(50) NULL
GO

--20091117
/****** Object:  Table [dbo].[#__labels]    Script Date: 11/17/2009 16:35:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[#__labels]') AND type in (N'U'))
BEGIN
CREATE TABLE [#__labels](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cultureName] [varchar](50) NOT NULL,
	[resourceSet] [varchar](255) NOT NULL,
	[resourceId] [varchar](255) NOT NULL,
	[value] [varchar](max) NULL,
	[comment] [varchar](255) NULL,
 CONSTRAINT [PK_#__labels] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[#__labels]') AND name = N'IX_#__labels')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__labels] ON [#__labels] 
(
	[cultureName] ASC,
	[resourceSet] ASC,
	[resourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


--20091219
ALTER TABLE #__modules ADD
	cssFile varchar(50) NULL,
	cssClass varchar(50) NULL,
	useCache int NULL,
	useLog int NULL
GO

--20091221
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'UseCache','Global use of Cache','true','true | false')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'UseLog','Global use of LogProvider','false','true | false')
GO

/****** Object:  Table [dbo].[#__logItems]    Script Date: 12/21/2009 11:03:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[#__logItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [#__logItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [varchar](50) NULL,
	[moduleId] [int] NULL,
	[type] [int] NULL,
	[userHostAddress] [varchar](50) NULL,
	[url] [varchar](500) NULL,
	[description] [varchar](500) NULL,
 CONSTRAINT [PK_#__logItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--20091225
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[#__modulesMenuTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [#__modulesMenuTypes](
	[moduleId] [int] NOT NULL,
	[menuType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_#__modulesMenuTypes] PRIMARY KEY CLUSTERED 
(
	[moduleId] ASC,
	[menuType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


--20100215
alter #__items 
	CustomString1 varchar(MAX) NULL,
	CustomString2 varchar(MAX) NULL,
	CustomString3 varchar(MAX) NULL,
	ItemParams varchar(MAX) NULL

/*
--non inlinea
ALTER TABLE #__items_Culture ADD
	CustomString1 varchar(500) NULL,
	CustomString2 varchar(500) NULL,
	CustomString3 varchar(500) NULL,
	CustomText1 text NULL,
	CustomText2 text NULL,
	CustomText3 text NULL
GO*/

--20100223
ALTER TABLE #__items ADD
    itemDate datetime NULL,
	validFrom datetime NULL,
	validTo datetime NULL
GO

--20100305
ALTER TABLE #__menu_Culture ADD
	titleWindow varchar(200) NULL
GO

--20100412
CREATE TABLE [#__modules_Culture](
	[cultureName] [varchar](50) NOT NULL,
	[moduleId] [int] NOT NULL,
	[title] [varchar](200) NULL,
 CONSTRAINT [PK_#__modules_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[moduleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--20100412
--#__menu change 'link' from text to varchar(200) 
--#__modules change 'moduleParams' e '[content]' from text to varchar(MAX) 
--script
CREATE TABLE Tmp_#__menu
	(
	id int NOT NULL,
	menuType varchar(50) NULL,
	name varchar(200) NULL,
	alias varchar(200) NULL,
	link varchar(200) NULL,
	contentType smallint NULL,
	published bit NULL,
	parentId int NULL,
	moduleId int NULL,
	ordering int NULL,
	accessType smallint NULL,
	overridePageTitle bit NULL,
	referMenuId int NULL,
	currMasterPage varchar(50) NULL,
	currTheme varchar(50) NULL,
	cssClass varchar(50) NULL,
	visible bit NULL,
	routeId int NULL,
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL
	)  ON [PRIMARY]
GO
--ALTER TABLE Tmp_#__menu SET (LOCK_ESCALATION = TABLE)
--GO
IF EXISTS(SELECT * FROM #__menu)
	 EXEC('INSERT INTO Tmp_#__menu (id, menuType, name, alias, link, contentType, published, parentId, moduleId, ordering, accessType, overridePageTitle, referMenuId, currMasterPage, currTheme, cssClass, visible, routeId, permissionId, accessCode, accessLevel)
		SELECT id, menuType, name, alias, CONVERT(varchar(200), link), contentType, published, parentId, moduleId, ordering, accessType, overridePageTitle, referMenuId, currMasterPage, currTheme, cssClass, visible, routeId, permissionId, accessCode, accessLevel FROM #__menu WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE #__menu
GO
EXECUTE sp_rename N'Tmp_#__menu', N'#__menu', 'OBJECT' 
GO
ALTER TABLE #__menu ADD CONSTRAINT
	PK_#__menu PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO


CREATE TABLE Tmp_#__modules
	(
	id int NOT NULL,
	title varchar(200) NULL,
	[content] varchar(MAX) NULL,
	ordering int NULL,
	templateBlockName varchar(50) NULL,
	published bit NULL,
	moduleName varchar(50) NULL,
	moduleNamespace varchar(200) NULL,
	dateInserted datetime NULL,
	userInserted varchar(50) NULL,
	dateUpdated datetime NULL,
	userUpdated varchar(50) NULL,
	accessType int NULL,
	showTitle bit NULL,
	moduleParams varchar(MAX) NULL,
	isCore bit NULL,
	menuSelection int NULL,
	currView varchar(50) NULL,
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL,
	cssFile varchar(50) NULL,
	cssClass varchar(50) NULL,
	useCache int NULL,
	useLog int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
--ALTER TABLE Tmp_#__modules SET (LOCK_ESCALATION = TABLE)
--GO
IF EXISTS(SELECT * FROM #__modules)
	 EXEC('INSERT INTO Tmp_#__modules (id, title, [content], ordering, templateBlockName, published, moduleName, moduleNamespace, dateInserted, userInserted, dateUpdated, userUpdated, accessType, showTitle, moduleParams, isCore, menuSelection, currView, permissionId, accessCode, accessLevel, cssFile, cssClass, useCache, useLog)
		SELECT id, title, CONVERT(varchar(MAX), [content]), ordering, templateBlockName, published, moduleName, moduleNamespace, dateInserted, userInserted, dateUpdated, userUpdated, accessType, showTitle, CONVERT(varchar(MAX), moduleParams), isCore, menuSelection, currView, permissionId, accessCode, accessLevel, cssFile, cssClass, useCache, useLog FROM #__modules WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE #__modules
GO
EXECUTE sp_rename N'Tmp_#__modules', N'#__modules', 'OBJECT' 
GO
ALTER TABLE #__modules ADD CONSTRAINT
	PK_#__modules PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

--20100414
ALTER TABLE #__staticPages
	DROP COLUMN linkedPage, menuStyle, authUser, original, orderId
GO

--20100520
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'StaticFilesTracking','Google analytics static files tracking','false','Enable google analytics tracking for static files such pdf, images etc.. Works only in modules that implement this functionality. Values=true|false')
GO

--20100701
ALTER TABLE #__items ADD
	alias varchar(200) NULL
GO

CREATE TABLE [#__dbVersion](
	[componentFullName] [varchar](500) NOT NULL,
	[versionId] [int] NULL,
	[versionDate] [datetime] NULL,
 CONSTRAINT [PK_#__dbVersion] PRIMARY KEY CLUSTERED 
(
	[componentFullName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--20100913
UPDATE #__menuTypes set menuType='adminPopups', title='admin menu for popup functions' WHERE menuType='adminUpload'
GO
UPDATE #__menu set menuType='adminPopups' WHERE menuType='adminUpload'
GO

ALTER TABLE #__menu ADD
	isCore bit NULL
GO

ALTER TABLE #__routes ADD
	isCore bit NULL
GO

--add labels admin popup
begin
	declare @menuId int
	declare @moduleId int
	declare @menuOrdering int
	declare @routeId int
	
	update #__routes set isCore=1 where [name]='pages'
	update #__routes set isCore=1 where [name]='admin area'
	
	select @menuId = MAX(id)+1 from #__menu
	select @moduleId = MAX(id)+1 from #__modules
	select @menuOrdering = MAX(ordering)+1 from #__menu
	select @routeId = id from #__routes where [name]='admin area' and isCore=1

	INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,
	moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,
	showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,
	cssFile,cssClass,useCache,useLog)  
	VALUES(@moduleId,'labels admin popup','','0','content',1,
	'LabelsAdmin','PigeonCms',GETDATE(),'admin',GETDATE(),'admin','0',
	0,'ModuleFullName:=',0,'3','Default.ascx','0','','0',
	'','','2','2')

	INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,
	moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,
	cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore)
	VALUES( @menuId,'adminPopups','labels admin popup','labels-admin-popup','','0',1,'0',
	@moduleId,@menuOrdering,'1',1,'0','TemplateAdminPopup','',
	'',0,@routeId,'0','','0',1)
	
	update #__modules set moduleParams='TargetLabelsPopup:='+ convert(varchar(10),@menuId) where title='updates admin' and isCore=1
end
go

--20100916
insert into #__appSettings(keyName, keyTitle, keyValue, keyInfo) values('PhotoSize_Small', 'small images width', 90, '')
insert into #__appSettings(keyName, keyTitle, keyValue, keyInfo) values('PhotoSize_Medium', 'medium images width', 105, '')
insert into #__appSettings(keyName, keyTitle, keyValue, keyInfo) values('PhotoSize_Large', 'large images width', 250, '')
insert into #__appSettings(keyName, keyTitle, keyValue, keyInfo) values('PhotoSize_Xlarge', 'extra large images width', 350, '')
go

--20100930
--add staticpages admin popup
begin
	declare @menuId int
	declare @moduleId int
	declare @menuOrdering int
	declare @routeId int
	
	select @menuId = MAX(id)+1 from #__menu
	select @moduleId = MAX(id)+1 from #__modules
	select @menuOrdering = MAX(ordering)+1 from #__menu
	select @routeId = id from #__routes where [name]='admin area' and isCore=1

	INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,
	moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,
	showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,
	cssFile,cssClass,useCache,useLog)  
	VALUES(@moduleId,'static pages admin popup','','0','content',1,
	'StaticPagesAdmin','PigeonCms',GETDATE(),'admin',GETDATE(),'admin','0',
	0,'',1,'3','Default.ascx','0','','0',
	'','','2','2')

	INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,
	moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,
	cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore)
	VALUES( @menuId,'adminPopups','PigeonCms-StaticPageAdmin-popup','pigeoncms-staticpageadmin-popup','','0',1,'0',
	@moduleId,@menuOrdering,'1',1,'0','TemplateAdminPopup','',
	'',0,@routeId,'0','','0',1)	
end
go

--add items admin popup
begin
	declare @menuId int
	declare @moduleId int
	declare @menuOrdering int
	declare @routeId int
	
	select @menuId = MAX(id)+1 from #__menu
	select @moduleId = MAX(id)+1 from #__modules
	select @menuOrdering = MAX(ordering)+1 from #__menu
	select @routeId = id from #__routes where [name]='admin area' and isCore=1

	INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,
	moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,
	showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,
	cssFile,cssClass,useCache,useLog)  
	VALUES(@moduleId,'items admin popup','','0','content',1,
	'ItemsAdmin','PigeonCms',GETDATE(),'admin',GETDATE(),'admin','0',
	0,'',1,'3','Default.ascx','0','','0',
	'','','2','2')

	INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,
	moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,
	cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore)
	VALUES( @menuId,'adminPopups','PigeonCms-ItemsAdmin-popup','pigeoncms-itemsadmin-popup','','0',1,'0',
	@moduleId,@menuOrdering,'1',1,'0','TemplateAdminPopup','',
	'',0,@routeId,'0','','0',1)
end
go

--20101004
--add placeholder admin popup
begin
	declare @menuId int
	declare @moduleId int
	declare @menuOrdering int
	declare @routeId int
	
	select @menuId = MAX(id)+1 from #__menu
	select @moduleId = MAX(id)+1 from #__modules
	select @menuOrdering = MAX(ordering)+1 from #__menu
	select @routeId = id from #__routes where [name]='admin area' and isCore=1

	INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,
	moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,
	showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,
	cssFile,cssClass,useCache,useLog)  
	VALUES(@moduleId,'placeholders admin popup','','0','content',1,
	'PlaceholdersAdmin','PigeonCms',GETDATE(),'admin',GETDATE(),'admin','0',
	0,'',1,'3','Default.ascx','0','','0',
	'','','2','2')

	INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,
	moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,
	cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore)
	VALUES( @menuId,'adminPopups','PigeonCms-PlaceholdersAdmin-popup','pigeoncms-placeholdersadmin-popup','','0',1,'0',
	@moduleId,@menuOrdering,'1',1,'0','TemplateAdminPopup','',
	'',0,@routeId,'0','','0',1)	
end
go

--20101008
ALTER TABLE #__modules ADD
	directEditMode bit NULL
GO
UPDATE #__modules set directEditMode=0 where directEditMode is null
GO
UPDATE #__modules set directEditMode=1 where title='static pages admin popup' and moduleName='StaticPagesAdmin' and moduleNamespace='PigeonCms'
GO
UPDATE #__modules set directEditMode=1 where title='items admin popup' and moduleName='ItemsAdmin' and moduleNamespace='PigeonCms'
GO
UPDATE #__modules set directEditMode=1 where title='placeholders admin popup' and moduleName='PlaceholdersAdmin' and moduleNamespace='PigeonCms'
GO

--20101026
update #__modules set isCore=1 where menuSelection<>3 and title in ('Debug','Main menu', 'Admin menu')
go

--20101028
ALTER TABLE #__memberUsers ADD
	isCore bit NULL
GO
update #__memberUsers set isCore=1 where username in ('admin')
go

--20101029
--add error page core menu entry
begin
	declare @menuId int
	declare @moduleId int
	declare @menuOrdering int
	declare @routeId int
	
	select @menuId = MAX(id)+1 from #__menu
	select @moduleId = MAX(id)+1 from #__modules
	select @menuOrdering = MAX(ordering)+1 from #__menu
	select @routeId = id from #__routes where [name]='pages' and isCore=1

	insert into #__staticPages(pageName, visible, showPageTitle) values('error',1,1)

	insert into #__staticPages_Culture(cultureName,pageName,pageTitle,pageContent)
	values('en-US','error','Ops. An error occurred.','The resource you requested caused an error.<br />  Try to navigate to <a href="/pages/default.aspx">homepage</a>.')
	insert into #__staticPages_Culture(cultureName,pageName,pageTitle,pageContent)
	values('it-IT','error','Ops. Si è verificato un errore.','La risorsa richiesta ha generato un errore.<br />  Prova ad andare alla <a href="&quot;/pages/default.aspx&quot;">homepage</a>.')

	INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,
	moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,
	showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,
	cssFile,cssClass,useCache,useLog,directEditMode)  
	VALUES(@moduleId,'error page','','0','content',1,
	'StaticPage','PigeonCms',GETDATE(),'admin',GETDATE(),'admin','0',
	0,'PageName:=error',1,'3','StaticPage.ascx','0','','0',
	'','','2','2',0)

	INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,
	moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,
	cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore)
	VALUES(@menuId,'mainmenu','Error page','error','','0',1,'0',
	@moduleId,@menuOrdering,'0',0,'0','','',
	'',0,@routeId,'0','','0',1)	

	insert into #__menu_Culture(cultureName,menuId,title,titleWindow) values('en-US',@menuId,'Error page','Error page')
	insert into #__menu_Culture(cultureName,menuId,title,titleWindow) values('it-IT',@menuId,'Error page','Error page')
end
go

--20101207
--added debug role
insert into #__roles(rolename, applicationName) values('debug', 'PigeonCms')
go
insert into #__usersInRoles(username, rolename, applicationName) values('admin', 'debug', 'PigeonCms')
go

--PigeonCms.Debug module instance only for users in debug role
begin
	declare @moduleId int
	declare @permissionId int

	set @moduleId = 0
	select @moduleId = id from #__modules where moduleNamespace='PigeonCms' and moduleName='Debug' and isCore=1
	select @permissionId = MAX(id)+1 from #__permissions
	
	if @moduleId > 0 
	begin
		update #__modules set accessType=1, permissionId=@permissionId where id=@moduleId
		insert into #__permissions(id, rolename) values(@permissionId, 'debug')
	end
end
go

--20101213
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.EmailContactForm', 'LblCaptchaText', 'enter the code shows')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.EmailContactForm', 'LblCaptchaText', 'inserisci il codice visualizzato')
go

--20110214
/****** Object:  Table [dbo].[#__comments]    Script Date: 02/22/2011 13:30:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__comments]') AND type in (N'U'))
DROP TABLE [#__comments]
GO
/****** Object:  Table [dbo].[#__comments]    Script Date: 02/22/2011 13:30:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__comments]') AND type in (N'U'))
BEGIN
CREATE TABLE [#__comments](
	[id] [int] NOT NULL,
	[groupId] [int] NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [varchar](255) NULL,
	[userHostAddress] [varchar](255) NULL,
	[name] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[comment] [text] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_#__comments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

--20110222
ALTER TABLE #__items ADD
	commentsGroupId int NULL
GO

--20110309
update #__routes set name='products list by category name', pattern='products/list/{pagename}/{*categoryname}' where name='products list'
GO
update #__routes set pattern='products/{pagename}/{*itemname}' where pattern='products/{pagename}/{itemname}'
GO
update #__routes set pattern='products/{pagename}/id/{*itemid}' where pattern='products/{pagename}/id/{itemid}'
GO

begin
	declare @routeId int
	declare @ordering int

	set @routeId = 0
	set @ordering = 0
	select @routeId=MAX(id)+1, @ordering=MAX(ordering)+1 from #__routes

	insert into #__routes(id, name, pattern, published, ordering, isCore)
	values(@routeId, 'products list by category id', 'products/list/{pagename}/id/{*categoryid}', 1,@ordering, 0)
end
go

--20110414
--add documents upload ImagesManager popup
begin
	declare @menuId int
	declare @moduleId int
	declare @menuOrdering int
	declare @routeId int
	
	select @menuId = MAX(id)+1 from #__menu
	select @moduleId = MAX(id)+1 from #__modules
	select @menuOrdering = MAX(ordering)+1 from #__menu
	select @routeId = id from #__routes where [name]='admin area' and isCore=1

	INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,
	moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,
	showTitle,moduleParams,
	isCore,menuSelection,currView,permissionId,accessCode,accessLevel,
	cssFile,cssClass,useCache,useLog,directEditMode)  
	VALUES(@moduleId,'documents-upload','','0','content',1,
	'FilesUpload','PigeonCms',GETDATE(),'admin',GETDATE(),'admin','0',
	0,'AllowFilesUpload:=1|AllowFilesSelection:=1|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=1|AllowFoldersNavigation:=1|FileExtensions:=pdf;doc;docx;xls;xlsx;jpg;jpeg;gif;png;zip|FileSize:=1024|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Docs|UploadFields:=3|NumOfFilesAllowed:=0|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|SuccessText:=File(s) uploaded|ErrorText:=Error uploading file(s)',
	1,'3','Default.ascx','0','','0',
	'','','2','2',0)

	INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,
	moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,
	cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore)
	VALUES( @menuId,'adminPopups','documents-upload','docs-upload','','0',1,'0',
	@moduleId,@menuOrdering,'1',0,'0','TemplateAdminPopup','',
	'',0,@routeId,'0','','0',1)
end
go

--20110415
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.FilesUpload', 'Preview', 'Preview')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.FilesUpload', 'Preview', 'Anteprima')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.FilesUpload', 'Select', 'Select')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.FilesUpload', 'Select', 'Seleziona')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.ContentEditorControl', 'ReadMore', 'Read more')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ContentEditorControl', 'ReadMore', 'Leggi tutto')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.ContentEditorControl', 'Pagebreak', 'Page break')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ContentEditorControl', 'Pagebreak', 'Salto pagina')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.ContentEditorControl', 'File', 'File')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ContentEditorControl', 'File', 'File')
go

--20110418
update #__labels set resourceSet='PigeonCms.FilesManager' where resourceSet='PigeonCms.FilesUpload'
go
update #__modules set moduleName='FilesManager' where moduleName='FilesUpload'
go

--20110421
update #__routes set pattern='products/{pagename}/{itemname}/{*pathinfo}' where pattern='products/{pagename}/{*itemname}'
GO
update #__routes set pattern='products/{pagename}/id/{itemid}/{*pathinfo}' where pattern='products/{pagename}/id/{*itemid}'
GO

--20111101
ALTER TABLE #__items ADD
	writeAccessType int NULL,
	writePermissionId int NULL,
	writeAccessCode varchar(255) NULL,
	writeAccessLevel int NULL
GO

ALTER TABLE #__menu ADD
	writeAccessType int NULL,
	writePermissionId int NULL,
	writeAccessCode varchar(255) NULL,
	writeAccessLevel int NULL
GO

ALTER TABLE #__modules ADD
	writeAccessType int NULL,
	writePermissionId int NULL,
	writeAccessCode varchar(255) NULL,
	writeAccessLevel int NULL
GO

--20111101
ALTER TABLE #__categories ADD
	accessType int NULL,
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL,
	writeAccessType int NULL,
	writePermissionId int NULL,
	writeAccessCode varchar(255) NULL,
	writeAccessLevel int NULL
GO

ALTER TABLE #__sections ADD
	accessType int NULL,
	permissionId int NULL,
	accessCode varchar(255) NULL,
	accessLevel int NULL,
	writeAccessType int NULL,
	writePermissionId int NULL,
	writeAccessCode varchar(255) NULL,
	writeAccessLevel int NULL
GO

--20111114
ALTER TABLE #__sections ADD
	maxItems int NULL,
	maxAttachSizeKB int NULL
GO

insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.SectionsAdmin', 'LblTitle', 'Title')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.SectionsAdmin', 'LblTitle', 'Titolo')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.SectionsAdmin', 'LblDescription', 'Description')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.SectionsAdmin', 'LblDescription', 'Descrizione')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.SectionsAdmin', 'LblEnabled', 'Enabled')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.SectionsAdmin', 'LblEnabled', 'Abilitato')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.SectionsAdmin', 'LblLimits', 'Limits')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.SectionsAdmin', 'LblLimits', 'Limiti')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.SectionsAdmin', 'LblMaxItems', 'Max items')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.SectionsAdmin', 'LblMaxItems', 'Numero max items')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.SectionsAdmin', 'LblMaxAttachSizeKB', 'Max size for attachments (KB)')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.SectionsAdmin', 'LblMaxAttachSizeKB', 'Dimensione massima allegati (KB)')
go

--20111118
ALTER TABLE #__memberUsers ADD
	sex varchar(1) NULL,
	companyName varchar(255) NULL,
	vat varchar(255) NULL,
	ssn varchar(255) NULL,
	firstName varchar(255) NULL,
	secondName varchar(255) NULL,
	address1 varchar(255) NULL,
	address2 varchar(255) NULL,
	city varchar(255) NULL,
	state varchar(255) NULL,
	zipCode varchar(255) NULL,
	nation varchar(255) NULL,
	tel1 varchar(255) NULL,
	mobile1 varchar(255) NULL,
	website1 varchar(255) NULL
GO

--20111125
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblPasswordControl', 'Ripeti password')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblOldPassword', 'Vecchia password')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblEnabled', 'Abilitato')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblComment', 'Note')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblSex', 'Sesso')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblCompanyName', 'Nome azienda')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblVat', 'Partita IVA')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblSsn', 'Codice fiscale')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblFirstName', 'Nome')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblSecondName', 'Cognome')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblAddress1', 'Indirizzo')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblAddress2', 'Indirizzo')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblCity', 'Citt&agrave;')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblState', 'Provincia')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblZipCode', 'Cap')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblNation', 'Nazione')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblTel1', 'Telefono')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblMobile1', 'Cellulare')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblWebsite1', 'Sito web')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblPasswordNotMatching', 'le password non corrispondono')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblInvalidEmail', 'email non valida')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblInvalidPassword', 'password non valida')
go


insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblPasswordControl', 'Repeat password')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblOldPassword', 'Old password')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblEnabled', 'Enabled')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblComment', 'Notes')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblSex', 'Gender')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblCompanyName', 'Company name')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblVat', 'Vat')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblSsn', 'Ssn')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblFirstName', 'First name')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblSecondName', 'Second name')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblAddress1', 'Address')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblAddress2', 'Address')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblCity', 'Citty;')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblState', 'State')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblZipCode', 'Zip')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblNation', 'Nation')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblTel1', 'Telephone')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblMobile1', 'Mobile phone')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblWebsite1', 'Website')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblPasswordNotMatching', 'passwords do not match')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblInvalidEmail', 'invalid email')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblInvalidPassword', 'invalid password')
go

insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MemberEditorControl', 'LblManadatoryFields', 'please fill mandatory fields')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MemberEditorControl', 'LblManadatoryFields', 'compila i campi obbligatori')
go

--20111128
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.MembersAdmin', 'LblCaptchaText', 'enter the code shown')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.MembersAdmin', 'LblCaptchaText', 'inserisci il codice visualizzato')
go

--20111220
ALTER TABLE #__items ADD
	parentId int NULL
GO

--20120109
ALTER TABLE #__items
	DROP COLUMN parentId
GO
ALTER TABLE #__items ADD
	threadId int NULL
GO
update #__items set threadId=id where threadId is null
GO

--20120208
CREATE TABLE #__messages
	(
	id int NOT NULL,
	ownerUser varchar(50) NULL,
	fromUser varchar(50) NULL,
	toUser varchar(500) NULL,
	title varchar(200) NULL,
	description varchar(MAX) NULL,
	dateInserted datetime NULL,
	priority int NULL,
	isRead bit NULL,
	isStarred bit NULL,
	visible bit NULL,
	itemId int NULL,
	moduleId int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE #__messages ADD CONSTRAINT
	PK_#__messages PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

--20120215
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.ItemsAdmin', 'ChooseSection', 'Choose a section before')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'ChooseSection', 'Scegli una sezione')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('en-US', 'PigeonCms.ItemsAdmin', 'ChooseCategory', 'Choose a category before')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'ChooseCategory', 'Scegli una categoria')
go

--20120221
--added showtitle to all menu admin entries; removed in all modules ascx files h1 moduletitle
update mo set showtitle=1 
from #__menu me inner join #__modules mo on me.moduleId=mo.id
where me.menuType in ('adminmenu','adminpopups')
go

--20120306
--flag to allow to receive internal messages and email from system, modules or other users
ALTER TABLE #__memberUsers ADD
	allowMessages bit NULL,
	allowEmails bit NULL
GO
UPDATE #__memberUsers SET allowMessages=0, allowEmails=0
GO

--20120307
--toList of members allowed to receive modules messages (if any)
--example: admin; user1; user2
ALTER TABLE #__modules ADD
	systemMessagesTo varchar(255) NULL
GO

--20120321
--customers table
CREATE TABLE [#__customers](
	[id] [int] NOT NULL,
	[companyName] [varchar](255) NULL,
	[vat] [varchar](255) NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [varchar](50) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [varchar](50) NULL,
 CONSTRAINT [PK_#__customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--20120920
--userTempData
CREATE TABLE [#__userTempData]
	(
	id int NOT NULL,
	username varchar(50) NOT NULL,
	sessionId varchar(255) NOT NULL,
	dateInserted datetime NOT NULL,
	dateExpiration datetime NOT NULL,
	col01 varchar(5000) NULL,
	col02 varchar(5000) NULL,
	col03 varchar(5000) NULL,
	col04 varchar(5000) NULL,
	col05 varchar(5000) NULL,
	col06 varchar(5000) NULL,
	col07 varchar(5000) NULL,
	col08 varchar(5000) NULL,
	col09 varchar(5000) NULL,
	col10 varchar(5000) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE #__userTempData ADD CONSTRAINT
	PK_#__userTempData PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--20120921
ALTER TABLE #__userTempData ADD
	enabled bit NULL
GO

--20121008 ssl management
--route generic settings
ALTER TABLE #__routes ADD
	useSsl bit NULL
GO
--false as default value
UPDATE #__routes SET useSsl=0
GO

--page specific setting
ALTER TABLE #__menu ADD
	useSsl int NULL
GO
--not set as default value (use route setting)
UPDATE #__menu SET useSsl=2
GO

--tickets labels
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'NewTicket', 'Nuovo ticket')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'PriorityFilter','--priorità--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Close', 'chiudi')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Reopen', 'riapri')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Lock', 'blocca')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Actions', '--azioni--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'StatusFilter', '--stato--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'CategoryFilter', '--categoria--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Always', 'tutto')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Today', 'oggi')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Last week', 'ultima settimana')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Last month', 'ultimo mese')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Subject', 'Oggetto')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Reply', 'Rispondi')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'BackToList', 'Torna alla lista')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'MyTickets', 'solo mie')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'AssignTo', '--assegna--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Status', 'Stato')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'AssignedTo', 'Assegnato a')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Low', 'Bassa')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Medium', 'Media')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'High', 'Alta')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Open', 'Aperto')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'WorkInProgress', 'In lavorazione')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Closed', 'Chiuso')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Locked', 'Bloccato')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Working', 'in lavorazione')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', '<subject>', '<oggetto>')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Priority', 'Priorità')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Operator', 'Operatore')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'DateInserted', 'Inserito il')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'LastActivity', 'Ultima attività')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Text', 'Testo')
go
insert into #__labels(cultureName, resourceSet, resourceId, value, comment) values('it-IT', 'PigeonCms.ItemsAdmin', 'MessageTicketTitle', 'Ticket "[[ItemTitle]]" ([[ItemId]]) [[Extra]]', 'allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]')
go
insert into #__labels(cultureName, resourceSet, resourceId, value, comment) values('it-IT', 'PigeonCms.ItemsAdmin', 'MessageTicketDescription', '[[ItemDescription]]', 'allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'CustomerFilter', '--cliente--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Customer', 'Cliente')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'Attachment', 'Allegati')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'AttachFiles', 'Allega files')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'SendEmail', 'Invia email')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'OperatorFilter', '--operatore--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'UserInsertedFilter', '--inserito da--')
go
insert into #__labels(cultureName, resourceSet, resourceId, value) values('it-IT', 'PigeonCms.ItemsAdmin', 'SaveAndClose', 'Salva e chiudi ticket')
go


--20121030 form fields management
CREATE TABLE #__formFields
	(
	id int NOT NULL,
	formId int NULL,
	[enabled] bit NULL,
	groupName varchar(255) NULL,
	name varchar(255) NOT NULL,
	defaultValue varchar(255) NULL,
	minValue int NULL,
	maxValue int NULL,
	rowsNo int NULL,
	colsNo int NULL,
	cssClass varchar(255) NULL,
	cssStyle varchar(255) NULL,
	fieldType varchar(255) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE #__formFields ADD CONSTRAINT
	PK_#__formFields PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE #__formFieldOptions
	(
	id int NOT NULL,
	formFieldId int NOT NULL,
	label varchar(255) NULL,
	value varchar(255) NULL,
	ordering int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE #__formFieldOptions ADD CONSTRAINT
	PK_#__formFieldOptions PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--forms table
CREATE TABLE #__forms
	(
	id int NOT NULL,
	name varchar(255) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE #__forms ADD CONSTRAINT
	PK_#__forms PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--20121119
--item dynamic form field values
CREATE TABLE #__itemFieldValues
	(
	formFieldId int NOT NULL,
	itemId int NOT NULL,
	value varchar(5000) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE #__itemFieldValues ADD CONSTRAINT
	PK_#__itemFieldValues PRIMARY KEY CLUSTERED 
	(
	formFieldId,
	itemId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

--20130322
--css class for items
ALTER TABLE #__items ADD
	cssClass varchar(50) NULL
GO

--20130627
--sessionId of current user
ALTER TABLE #__logItems ADD
	sessionId varchar(50) NULL
GO
UPDATE #__logItems set sessionId='' where sessionId is null
GO

--20140228
--css class for categories and sections
ALTER TABLE #__categories ADD
	cssClass varchar(50) NULL
GO
ALTER TABLE #__sections ADD
	cssClass varchar(50) NULL
GO

--20141223
--integration with PigeonCms.Controls.Label server control
--col textMode: (0,Html|1,BasicHtml|2,Text) see enum PigeonCms.ContentEditorProvider.Configuration.EditorTypeEnum
--col isLocalized: true|false manage or not translations
ALTER TABLE #__labels ADD
	textMode int NULL,
	isLocalized bit NULL
GO
UPDATE #__labels SET textMode = 2 WHERE textMode is NULL
GO
UPDATE #__labels SET isLocalized = 1 WHERE isLocalized is NULL
GO


--20150119 - SHOP
CREATE TABLE #__shop_orderHeader
	(
	id int NOT NULL IDENTITY (1, 1),
	orderRef varchar(50) NULL,
	ownerUser varchar(255) NULL,
	customerId int NULL, 
	orderDate datetime NULL,
	orderDateRequested datetime NULL,
	orderDateShipped datetime NULL,
	dateInserted datetime NULL,
	userInserted varchar(255) NULL,
	dateUpdated datetime NULL,
	userUpdated varchar(255) NULL,
	confirmed bit NULL,
	paid bit NULL,
	processed bit NULL,
	invoiced bit NULL,
	notes varchar(5000) NULL,
	qtyAmount decimal(18, 2) NULL,
	orderAmount decimal(18, 2) NULL,
	shipAmount decimal(18, 2) NULL,
	totalAmount decimal(18, 2) NULL,
	currency varchar(50) NULL,
	vatPercentage int NULL,
	invoiceId int NULL,
	invoiceRef varchar(200) NULL,
	ordName varchar(500) NULL,
	ordAddress varchar(500) NULL,
	ordZipCode varchar(50) NULL,
	ordCity varchar(50) NULL,
	ordState varchar(50) NULL,
	ordNation varchar(50) NULL,
	ordPhone varchar(200) NULL,
	ordEmail varchar(200) NULL,
	codeCoupon varchar(50) NULL,
	paymentCode varchar(10) NULL,
	shipCode varchar(10) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE #__shop_orderHeader ADD CONSTRAINT
	PK_#__shop_orderHeader PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX IXOrderRef_#__shop_orderHeader ON #__shop_orderHeader
	(
	orderRef
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


--20150119 - SHOP
CREATE TABLE #__shop_orderRows
	(
	id int NOT NULL IDENTITY (1, 1),
	orderId int NULL,
	productCode varchar(50) NULL,
	qty decimal(18, 2) NULL,
	priceFull decimal(18, 2) NULL,
	priceNet decimal(18, 2) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE #__shop_orderRows ADD CONSTRAINT
	PK_#__shop_orderRows PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--20150203 - SHOP
CREATE TABLE #__shop_payments
	(
	payCode nvarchar(20) NOT NULL,
	name nvarchar(50) NULL,
	assemblyName nvarchar(50) NULL,
	cssClass nvarchar(50) NULL,
	isDebug bit NULL,
	[enabled] bit NULL,
	payAccount nvarchar(50) NULL,
	paySubmitUrl nvarchar(100) NULL,
	payCallbackUrl nvarchar(100) NULL,
	siteOkUrl nvarchar(100) NULL,
	siteKoUrl nvarchar(100) NULL,
	minAmount decimal(18, 0) NULL,
	maxAmount decimal(18, 0) NULL,
	payParams nvarchar(500) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE #__shop_payments ADD CONSTRAINT
	PK_#__shop_payments PRIMARY KEY CLUSTERED 
	(
	payCode
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO