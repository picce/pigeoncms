--@created 20170621 - picce

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__appSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__appSettings](
	[keySet] [nvarchar](50) NOT NULL,
	[keyName] [nvarchar](50) NOT NULL,
	[keyTitle] [nvarchar](500) NULL,
	[keyValue] [nvarchar](500) NULL,
	[keyInfo] [nvarchar](500) NULL,
 CONSTRAINT [PK_#__appSettings] PRIMARY KEY CLUSTERED 
(
	[keySet] ASC,
	[keyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__attributes]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__attributes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__attributes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[allowCustomValue] [bit] NULL,
	[ordering] [int] NULL,
	[extId] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__attributes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__attributeSet]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__attributeSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__attributeSet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[attributesList] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__attributeSet] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__attributesValues]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__attributesValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__attributesValues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[attributeId] [int] NOT NULL,
	[valueString] [nvarchar](max) NULL,
	[ordering] [int] NULL,
	[extId] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__attributesValues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__categories]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__categories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sectionId] [int] NULL,
	[parentId] [int] NULL,
	[enabled] [bit] NULL,
	[ordering] [int] NULL,
	[defaultImageName] [nvarchar](50) NULL,
	[accessType] [int] NULL,
	[permissionId] [int] NULL,
	[accessCode] [nvarchar](255) NULL,
	[accessLevel] [int] NULL,
	[writeAccessType] [int] NULL,
	[writePermissionId] [int] NULL,
	[writeAccessCode] [nvarchar](255) NULL,
	[writeAccessLevel] [int] NULL,
	[cssClass] [nvarchar](50) NULL,
	[extId] [nvarchar](50) NULL,
	[seoId] [int] NULL,
 CONSTRAINT [PK_#__categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__categories_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__categories_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__categories_Culture](
	[cultureName] [nvarchar](10) NOT NULL,
	[categoryId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__categories_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__comments]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__comments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__comments](
	[id] [int] NOT NULL,
	[groupId] [int] NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](255) NULL,
	[userHostAddress] [nvarchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[comment] [text] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_#__comments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__cultures]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__cultures]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__cultures](
	[cultureCode] [nvarchar](10) NOT NULL,
	[displayName] [nvarchar](50) NULL,
	[enabled] [bit] NULL,
	[ordering] [int] NULL,
	[shortCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_#__cultures] PRIMARY KEY CLUSTERED 
(
	[cultureCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__customers]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__customers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__customers](
	[id] [int] NOT NULL,
	[companyName] [nvarchar](255) NULL,
	[vat] [nvarchar](255) NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](50) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__dbVersion]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__dbVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__dbVersion](
	[componentFullName] [nvarchar](500) NOT NULL,
	[versionId] [int] NOT NULL,
	[versionDate] [datetime] NULL,
	[versionDev] [nvarchar](500) NULL,
	[versionNotes] [text] NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__dbVersion] PRIMARY KEY CLUSTERED 
(
	[componentFullName] ASC,
	[versionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__events]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__events]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__events](
	[id] [int] NOT NULL,
	[name] [nvarchar](255) NULL,
	[eventStart] [datetime] NULL,
	[eventEnd] [datetime] NULL,
	[resourceId] [int] NULL,
	[status] [int] NULL,
	[groupId] [int] NULL,
	[description] [nvarchar](500) NULL,
	[orderId] [int] NULL,
 CONSTRAINT [PK_#__events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__formFieldOptions]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__formFieldOptions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__formFieldOptions](
	[id] [int] NOT NULL,
	[formFieldId] [int] NOT NULL,
	[label] [nvarchar](255) NULL,
	[value] [nvarchar](255) NULL,
	[ordering] [int] NULL,
 CONSTRAINT [PK_#__formFieldOptions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__formFields]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__formFields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__formFields](
	[id] [int] NOT NULL,
	[formId] [int] NULL,
	[enabled] [bit] NULL,
	[groupName] [nvarchar](255) NULL,
	[name] [nvarchar](255) NOT NULL,
	[defaultValue] [nvarchar](255) NULL,
	[minValue] [int] NULL,
	[maxValue] [int] NULL,
	[rowsNo] [int] NULL,
	[colsNo] [int] NULL,
	[cssClass] [nvarchar](255) NULL,
	[cssStyle] [nvarchar](255) NULL,
	[fieldType] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__formFields] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__forms]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__forms]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__forms](
	[id] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__forms] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__geoCountries]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__geoCountries]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__geoCountries](
	[code] [nvarchar](2) NOT NULL,
	[iso3] [nvarchar](3) NULL,
	[continent] [nvarchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[custom1] [nvarchar](255) NULL,
	[custom2] [nvarchar](255) NULL,
	[custom3] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__geoCountries] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__geoZones]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__geoZones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__geoZones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[countryCode] [nvarchar](2) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[name] [nvarchar](255) NULL,
	[custom1] [nvarchar](255) NULL,
	[custom2] [nvarchar](255) NULL,
	[custom3] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__geoZones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__itemFieldValues]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__itemFieldValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__itemFieldValues](
	[formFieldId] [int] NOT NULL,
	[itemId] [int] NOT NULL,
	[value] [nvarchar](4000) NULL,
 CONSTRAINT [PK_#__itemFieldValues] PRIMARY KEY CLUSTERED 
(
	[formFieldId] ASC,
	[itemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__items]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__items]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__items](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemType] [nvarchar](255) NULL,
	[categoryId] [int] NULL,
	[enabled] [bit] NULL,
	[ordering] [int] NULL,
	[defaultImageName] [nvarchar](50) NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](50) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [nvarchar](50) NULL,
	[CustomBool1] [bit] NULL,
	[CustomBool2] [bit] NULL,
	[CustomBool3] [bit] NULL,
	[CustomBool4] [bit] NULL,
	[CustomDate1] [datetime] NULL,
	[CustomDate2] [datetime] NULL,
	[CustomDate3] [datetime] NULL,
	[CustomDate4] [datetime] NULL,
	[CustomDecimal1] [decimal](18, 2) NULL,
	[CustomDecimal2] [decimal](18, 2) NULL,
	[CustomDecimal3] [decimal](18, 2) NULL,
	[CustomDecimal4] [decimal](18, 2) NULL,
	[CustomInt1] [int] NULL,
	[CustomInt2] [int] NULL,
	[CustomInt3] [int] NULL,
	[CustomInt4] [int] NULL,
	[CustomString1] [nvarchar](max) NULL,
	[CustomString2] [nvarchar](max) NULL,
	[CustomString3] [nvarchar](max) NULL,
	[CustomString4] [nvarchar](max) NULL,
	[ItemParams] [nvarchar](max) NULL,
	[accessType] [int] NULL,
	[permissionId] [int] NULL,
	[accessCode] [nvarchar](255) NULL,
	[accessLevel] [int] NULL,
	[itemDate] [datetime] NULL,
	[validFrom] [datetime] NULL,
	[validTo] [datetime] NULL,
	[alias] [nvarchar](200) NULL,
	[commentsGroupId] [int] NULL,
	[writeAccessType] [int] NULL,
	[writePermissionId] [int] NULL,
	[writeAccessCode] [nvarchar](255) NULL,
	[writeAccessLevel] [int] NULL,
	[threadId] [int] NULL,
	[cssClass] [nvarchar](50) NULL,
	[extId] [nvarchar](50) NULL,
	[seoId] [int] NULL,
 CONSTRAINT [PK_#__items] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__items_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__items_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__items_Culture](
	[cultureName] [nvarchar](50) NOT NULL,
	[itemId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__items_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[itemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__itemsAttributesValues]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__itemsAttributesValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__itemsAttributesValues](
	[itemId] [int] NOT NULL,
	[attributeId] [int] NOT NULL,
	[attributeValueId] [int] NOT NULL,
	[CustomValueString] [nvarchar](max) NULL,
 CONSTRAINT [PK_#__itemsAttributesValues] PRIMARY KEY CLUSTERED 
(
	[itemId] ASC,
	[attributeId] ASC,
	[attributeValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__itemsRelated]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__itemsRelated]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__itemsRelated](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemId] [int] NOT NULL,
	[relatedId] [int] NOT NULL,
	[itemsRelationTypeId] [int] NOT NULL,
 CONSTRAINT [PK_#__itemsRelated_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__itemsRelationTypes]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__itemsRelationTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__itemsRelationTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemType] [nvarchar](255) NULL,
	[ordering] [int] NULL,
	[extId] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__itemsRelationTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__itemsRelationTypes_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__itemsRelationTypes_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__itemsRelationTypes_Culture](
	[cultureName] [nvarchar](50) NOT NULL,
	[itemsRelationTypeId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__itemsRelationTypes_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[itemsRelationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__itemsTags]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__itemsTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__itemsTags](
	[itemId] [int] NOT NULL,
	[tagId] [int] NOT NULL,
 CONSTRAINT [PK_#__itemsTags] PRIMARY KEY CLUSTERED 
(
	[itemId] ASC,
	[tagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__labels]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__labels]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__labels](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cultureName] [nvarchar](50) NOT NULL,
	[resourceSet] [nvarchar](255) NOT NULL,
	[resourceId] [nvarchar](255) NOT NULL,
	[value] [nvarchar](max) NULL,
	[comment] [nvarchar](255) NULL,
	[textMode] [int] NULL,
	[isLocalized] [bit] NULL,
	[resourceParams] [nvarchar](max) NULL,
 CONSTRAINT [PK_#__labels] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__logItems]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__logItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__logItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](50) NULL,
	[moduleId] [int] NULL,
	[type] [int] NULL,
	[userHostAddress] [nvarchar](50) NULL,
	[url] [nvarchar](500) NULL,
	[description] [nvarchar](500) NULL,
	[sessionId] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__logItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__memberUsers]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__memberUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__memberUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NULL,
	[applicationName] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[comment] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[passwordQuestion] [nvarchar](255) NULL,
	[passwordAnswer] [nvarchar](255) NULL,
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
	[accessCode] [nvarchar](255) NULL,
	[accessLevel] [int] NULL,
	[isCore] [bit] NULL,
	[sex] [nvarchar](1) NULL,
	[companyName] [nvarchar](255) NULL,
	[vat] [nvarchar](255) NULL,
	[ssn] [nvarchar](255) NULL,
	[firstName] [nvarchar](255) NULL,
	[secondName] [nvarchar](255) NULL,
	[address1] [nvarchar](255) NULL,
	[address2] [nvarchar](255) NULL,
	[city] [nvarchar](255) NULL,
	[state] [nvarchar](255) NULL,
	[zipCode] [nvarchar](255) NULL,
	[nation] [nvarchar](255) NULL,
	[tel1] [nvarchar](255) NULL,
	[mobile1] [nvarchar](255) NULL,
	[website1] [nvarchar](255) NULL,
	[allowMessages] [bit] NULL,
	[allowEmails] [bit] NULL,
	[validationCode] [nvarchar](255) NULL,
	[nickName] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__memberUsers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__memberUsers_Meta]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__memberUsers_Meta]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__memberUsers_Meta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](200) NOT NULL,
	[metaKey] [nvarchar](200) NOT NULL,
	[metaValue] [nvarchar](500) NULL,
 CONSTRAINT [PK_#__memberUsers_Meta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__menu]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__menu](
	[id] [int] NOT NULL,
	[menuType] [nvarchar](50) NULL,
	[name] [nvarchar](200) NULL,
	[alias] [nvarchar](200) NULL,
	[link] [nvarchar](200) NULL,
	[contentType] [smallint] NULL,
	[published] [bit] NULL,
	[parentId] [int] NULL,
	[moduleId] [int] NULL,
	[ordering] [int] NULL,
	[accessType] [smallint] NULL,
	[overridePageTitle] [bit] NULL,
	[referMenuId] [int] NULL,
	[currMasterPage] [nvarchar](50) NULL,
	[currTheme] [nvarchar](50) NULL,
	[cssClass] [nvarchar](200) NULL,
	[visible] [bit] NULL,
	[routeId] [int] NULL,
	[permissionId] [int] NULL,
	[accessCode] [nvarchar](255) NULL,
	[accessLevel] [int] NULL,
	[isCore] [bit] NULL,
	[writeAccessType] [int] NULL,
	[writePermissionId] [int] NULL,
	[writeAccessCode] [nvarchar](255) NULL,
	[writeAccessLevel] [int] NULL,
	[useSsl] [int] NULL,
	[seoId] [int] NULL,
 CONSTRAINT [PK_#__menu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__menu_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__menu_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__menu_Culture](
	[cultureName] [nvarchar](50) NOT NULL,
	[menuId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[titleWindow] [nvarchar](200) NULL,
 CONSTRAINT [PK_#__menu_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[menuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__menuTypes]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__menuTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__menuTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[menuType] [nvarchar](50) NULL,
	[title] [nvarchar](200) NULL,
	[description] [nvarchar](200) NULL,
 CONSTRAINT [PK_#__menuTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__messages]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__messages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__messages](
	[id] [int] NOT NULL,
	[ownerUser] [nvarchar](50) NULL,
	[fromUser] [nvarchar](50) NULL,
	[toUser] [nvarchar](500) NULL,
	[title] [nvarchar](200) NULL,
	[description] [nvarchar](max) NULL,
	[dateInserted] [datetime] NULL,
	[priority] [int] NULL,
	[isRead] [bit] NULL,
	[isStarred] [bit] NULL,
	[visible] [bit] NULL,
	[itemId] [int] NULL,
	[moduleId] [int] NULL,
 CONSTRAINT [PK_#__messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__modules]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__modules](
	[id] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[content] [nvarchar](max) NULL,
	[ordering] [int] NULL,
	[templateBlockName] [nvarchar](50) NULL,
	[published] [bit] NULL,
	[moduleName] [nvarchar](50) NULL,
	[moduleNamespace] [nvarchar](200) NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](50) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [nvarchar](50) NULL,
	[accessType] [int] NULL,
	[showTitle] [bit] NULL,
	[moduleParams] [nvarchar](max) NULL,
	[isCore] [bit] NULL,
	[menuSelection] [int] NULL,
	[currView] [nvarchar](50) NULL,
	[permissionId] [int] NULL,
	[accessCode] [nvarchar](255) NULL,
	[accessLevel] [int] NULL,
	[cssFile] [nvarchar](50) NULL,
	[cssClass] [nvarchar](50) NULL,
	[useCache] [int] NULL,
	[useLog] [int] NULL,
	[directEditMode] [bit] NULL,
	[writeAccessType] [int] NULL,
	[writePermissionId] [int] NULL,
	[writeAccessCode] [nvarchar](255) NULL,
	[writeAccessLevel] [int] NULL,
	[systemMessagesTo] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__modules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__modules_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__modules_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__modules_Culture](
	[cultureName] [nvarchar](50) NOT NULL,
	[moduleId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
 CONSTRAINT [PK_#__modules_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[moduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__modulesMenu]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__modulesMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__modulesMenu](
	[moduleId] [int] NOT NULL,
	[menuId] [int] NOT NULL,
 CONSTRAINT [PK_#__modulesMenu] PRIMARY KEY CLUSTERED 
(
	[moduleId] ASC,
	[menuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__modulesMenuTypes]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__modulesMenuTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__modulesMenuTypes](
	[moduleId] [int] NOT NULL,
	[menuType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_#__modulesMenuTypes] PRIMARY KEY CLUSTERED 
(
	[moduleId] ASC,
	[menuType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__permissions]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__permissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__permissions](
	[id] [int] NOT NULL,
	[rolename] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__permissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[rolename] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__placeholders]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__placeholders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__placeholders](
	[name] [nvarchar](50) NOT NULL,
	[content] [text] NULL,
	[visible] [bit] NULL,
 CONSTRAINT [PK_#__placeholders] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__roles]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__roles](
	[rolename] [nvarchar](255) NOT NULL,
	[applicationName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__roles] PRIMARY KEY CLUSTERED 
(
	[rolename] ASC,
	[applicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__routeParams]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__routeParams]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__routeParams](
	[routeId] [int] NOT NULL,
	[paramKey] [nvarchar](50) NOT NULL,
	[paramValue] [nvarchar](255) NULL,
	[paramConstraint] [nvarchar](255) NULL,
	[paramDataType] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__routeParams_1] PRIMARY KEY CLUSTERED 
(
	[routeId] ASC,
	[paramKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__routes]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__routes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__routes](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[pattern] [nvarchar](255) NULL,
	[published] [bit] NULL,
	[ordering] [int] NULL,
	[currMasterPage] [nvarchar](50) NULL,
	[currTheme] [nvarchar](50) NULL,
	[isCore] [bit] NULL,
	[useSsl] [bit] NULL,
	[assemblyPath] [nvarchar](255) NULL,
	[handlerName] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__routes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__sections]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__sections]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__sections](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enabled] [bit] NULL,
	[accessType] [int] NULL,
	[permissionId] [int] NULL,
	[accessCode] [nvarchar](255) NULL,
	[accessLevel] [int] NULL,
	[writeAccessType] [int] NULL,
	[writePermissionId] [int] NULL,
	[writeAccessCode] [nvarchar](255) NULL,
	[writeAccessLevel] [int] NULL,
	[maxItems] [int] NULL,
	[maxAttachSizeKB] [int] NULL,
	[cssClass] [nvarchar](50) NULL,
	[itemType] [nvarchar](255) NULL,
	[extId] [nvarchar](50) NULL,
	[seoId] [int] NULL,
 CONSTRAINT [PK_#__sections] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__sections_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__sections_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__sections_Culture](
	[cultureName] [nvarchar](10) NOT NULL,
	[sectionId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__sections_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[sectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__seo]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__seo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__seo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[resourceSet] [nvarchar](100) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [varchar](50) NULL,
	[noIndex] [bit] NULL,
	[noFollow] [bit] NULL,
 CONSTRAINT [PK_#__seo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__seo_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__seo_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__seo_Culture](
	[cultureName] [varchar](50) NOT NULL,
	[seoId] [int] NOT NULL,
	[title] [nvarchar](200) NULL,
	[description] [ntext] NULL,
 CONSTRAINT [PK_#__seo_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[seoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_coupons]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_coupons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_coupons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[dateInserted] [datetime] NOT NULL,
	[userInserted] [int] NOT NULL,
	[dateUpdated] [datetime] NOT NULL,
	[userUpdated] [int] NOT NULL,
	[validFrom] [datetime] NOT NULL,
	[validTo] [datetime] NOT NULL,
	[enabled] [bit] NOT NULL,
	[amount] [decimal](9, 2) NOT NULL,
	[isPercentage] [bit] NOT NULL,
	[minOrderAmount] [decimal](9, 2) NULL,
	[categoriesIdList] [nvarchar](max) NULL,
	[itemType] [nvarchar](255) NULL,
	[maxUses] [int] NULL,
	[usesCounter] [int] NULL,
	[itemsIdList] [nvarchar](max) NULL,
 CONSTRAINT [PK_#__shop_coupons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_customers]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_customers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_customers](
	[id] [int] NOT NULL,
	[ownerUser] [nvarchar](255) NOT NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](50) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [nvarchar](50) NULL,
	[companyName] [nvarchar](255) NULL,
	[firstName] [nvarchar](255) NULL,
	[secondName] [nvarchar](255) NULL,
	[ssn] [nvarchar](255) NULL,
	[vat] [nvarchar](255) NULL,
	[address] [nvarchar](255) NULL,
	[city] [nvarchar](255) NULL,
	[state] [nvarchar](255) NULL,
	[zipCode] [nvarchar](255) NULL,
	[nation] [nvarchar](255) NULL,
	[tel1] [nvarchar](255) NULL,
	[mobile1] [nvarchar](255) NULL,
	[website1] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[enabled] [bit] NULL,
	[notes] [nvarchar](4000) NULL,
	[jsData] [nvarchar](max) NULL,
	[custom1] [nvarchar](255) NULL,
	[custom2] [nvarchar](255) NULL,
	[custom3] [nvarchar](255) NULL,
 CONSTRAINT [PK_#__shop_customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_orderHeader]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_orderHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_orderHeader](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderRef] [nvarchar](50) NULL,
	[ownerUser] [nvarchar](255) NULL,
	[customerId] [int] NULL,
	[orderDate] [datetime] NULL,
	[orderDateRequested] [datetime] NULL,
	[orderDateShipped] [datetime] NULL,
	[dateInserted] [datetime] NULL,
	[userInserted] [nvarchar](255) NULL,
	[dateUpdated] [datetime] NULL,
	[userUpdated] [nvarchar](255) NULL,
	[confirmed] [bit] NULL,
	[paid] [bit] NULL,
	[processed] [bit] NULL,
	[invoiced] [bit] NULL,
	[notes] [nvarchar](4000) NULL,
	[qtyAmount] [decimal](18, 2) NULL,
	[orderAmount] [decimal](18, 2) NULL,
	[shipAmount] [decimal](18, 2) NULL,
	[totalAmount] [decimal](18, 2) NULL,
	[totalPaid] [decimal](18, 2) NULL,
	[currency] [nvarchar](50) NULL,
	[invoiceId] [int] NULL,
	[invoiceRef] [nvarchar](200) NULL,
	[ordName] [nvarchar](500) NULL,
	[ordAddress] [nvarchar](500) NULL,
	[ordZipCode] [nvarchar](50) NULL,
	[ordCity] [nvarchar](50) NULL,
	[ordState] [nvarchar](50) NULL,
	[ordNation] [nvarchar](50) NULL,
	[ordPhone] [nvarchar](200) NULL,
	[ordEmail] [nvarchar](200) NULL,
	[couponCode] [nvarchar](50) NULL,
	[couponValue] [decimal](18, 2) NULL,
	[paymentCode] [nvarchar](20) NULL,
	[shipCode] [nvarchar](20) NULL,
	[couponIsPercentage] [bit] NULL,
	[jsData] [nvarchar](max) NULL,
	[custom1] [nvarchar](255) NULL,
	[custom2] [nvarchar](255) NULL,
	[custom3] [nvarchar](255) NULL,
	[ordVat] [nvarchar](255) NULL,
	[shipName] [nvarchar](500) NULL,
	[shipAddress] [nvarchar](500) NULL,
	[shipZipCode] [nvarchar](50) NULL,
	[shipCity] [nvarchar](50) NULL,
	[shipState] [nvarchar](50) NULL,
	[shipNation] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__shop_orderHeader] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_orderRows]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_orderRows]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_orderRows](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[productCode] [nvarchar](50) NULL,
	[qty] [decimal](18, 2) NULL,
	[priceNet] [decimal](18, 2) NULL,
	[taxPercentage] [decimal](18, 2) NULL,
	[rowNotes] [nvarchar](max) NULL,
 CONSTRAINT [PK_#__shop_orderRows] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_payments]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_payments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_payments](
	[payCode] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NULL,
	[assemblyName] [nvarchar](100) NULL,
	[cssClass] [nvarchar](50) NULL,
	[isDebug] [bit] NULL,
	[enabled] [bit] NULL,
	[payAccount] [nvarchar](50) NULL,
	[paySubmitUrl] [nvarchar](100) NULL,
	[payCallbackUrl] [nvarchar](100) NULL,
	[siteOkUrl] [nvarchar](100) NULL,
	[siteKoUrl] [nvarchar](100) NULL,
	[minAmount] [decimal](18, 0) NULL,
	[maxAmount] [decimal](18, 0) NULL,
	[payParams] [nvarchar](500) NULL,
 CONSTRAINT [PK_#__shop_payments] PRIMARY KEY CLUSTERED 
(
	[payCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_shipGeoZones]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_shipGeoZones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_shipGeoZones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[zoneCode] [nvarchar](50) NOT NULL,
	[countryCode] [nvarchar](2) NULL,
	[cityCode] [nvarchar](2) NOT NULL,
	[continent] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__shop_shipGeoZones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_shipments]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_shipments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_shipments](
	[shipCode] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NULL,
	[assemblyName] [nvarchar](100) NULL,
	[enabled] [bit] NULL,
	[isDebug] [bit] NULL,
	[shipType] [nvarchar](50) NULL,
	[shipParams] [nvarchar](500) NULL,
 CONSTRAINT [PK_#__shop_shipments] PRIMARY KEY CLUSTERED 
(
	[shipCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_shipZones]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_shipZones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_shipZones](
	[code] [nvarchar](50) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__shop_shipZones] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__shop_shipZonesWeight]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_shipZonesWeight]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__shop_shipZonesWeight](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[zoneCode] [nvarchar](50) NOT NULL,
	[weightFrom] [decimal](18, 2) NOT NULL,
	[weightTo] [decimal](18, 2) NOT NULL,
	[shippingPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_#__shop_shipZonesWeight] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__staticPages]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__staticPages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__staticPages](
	[pageName] [nvarchar](50) NOT NULL,
	[visible] [bit] NULL,
	[showPageTitle] [bit] NULL,
 CONSTRAINT [PK_#__staticPages] PRIMARY KEY CLUSTERED 
(
	[pageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__staticPages_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__staticPages_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__staticPages_Culture](
	[cultureName] [nvarchar](10) NOT NULL,
	[pageName] [nvarchar](50) NOT NULL,
	[pageTitle] [nvarchar](50) NULL,
	[pageContent] [text] NULL,
 CONSTRAINT [PK_#__staticPages_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[pageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__tags]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__tags](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tagTypeId] [int] NOT NULL,
	[ordering] [int] NULL,
	[extId] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__tags] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__tags_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__tags_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__tags_Culture](
	[cultureName] [nvarchar](50) NOT NULL,
	[tagId] [int] NOT NULL,
	[title] [nvarchar](255) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__tags_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[tagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__tagTypes]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__tagTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__tagTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemType] [nvarchar](255) NULL,
	[ordering] [int] NULL,
	[extId] [nvarchar](50) NULL,
 CONSTRAINT [PK_#__tagTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__tagTypes_Culture]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__tagTypes_Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__tagTypes_Culture](
	[cultureName] [nvarchar](50) NOT NULL,
	[tagTypeId] [int] NOT NULL,
	[title] [nvarchar](255) NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_#__tagTypes_Culture] PRIMARY KEY CLUSTERED 
(
	[cultureName] ASC,
	[tagTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__templateBlocks]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__templateBlocks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__templateBlocks](
	[name] [nvarchar](50) NOT NULL,
	[title] [nvarchar](500) NULL,
	[enabled] [bit] NULL,
	[orderId] [int] NULL,
 CONSTRAINT [PK_#__templateBlocks] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__usersInRoles]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__usersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__usersInRoles](
	[username] [nvarchar](255) NOT NULL,
	[rolename] [nvarchar](255) NOT NULL,
	[applicationName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_#__usersInRoles] PRIMARY KEY CLUSTERED 
(
	[username] ASC,
	[rolename] ASC,
	[applicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[#__userTempData]    Script Date: 21/06/2017 15:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#__userTempData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[#__userTempData](
	[id] [int] NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[sessionId] [nvarchar](255) NOT NULL,
	[dateInserted] [datetime] NOT NULL,
	[dateExpiration] [datetime] NOT NULL,
	[enabled] [bit] NULL,
	[col01] [nvarchar](max) NULL,
	[col02] [nvarchar](max) NULL,
	[col03] [nvarchar](max) NULL,
	[col04] [nvarchar](max) NULL,
	[col05] [nvarchar](max) NULL,
	[col06] [nvarchar](max) NULL,
	[col07] [nvarchar](max) NULL,
	[col08] [nvarchar](max) NULL,
	[col09] [nvarchar](max) NULL,
	[col10] [nvarchar](max) NULL,
	[col11] [nvarchar](max) NULL,
	[col12] [nvarchar](max) NULL,
	[col13] [nvarchar](max) NULL,
	[col14] [nvarchar](max) NULL,
	[col15] [nvarchar](max) NULL,
	[col16] [nvarchar](max) NULL,
	[col17] [nvarchar](max) NULL,
	[col18] [nvarchar](max) NULL,
	[col19] [nvarchar](max) NULL,
	[col20] [nvarchar](max) NULL,
 CONSTRAINT [PK_#__userTempData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'Acme.Site', N'BlogCatId', N'Blog category', N'4', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'Acme.Site', N'ContentsSectionId', N'Contents section', N'1', N'Section that contains website contents')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'Acme.Site', N'NewsCatId', N'News category', N'2', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'Acme.Site', N'StaticPagesCatId', N'Static pages category', N'1', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'CurrentMasterPage', N'Current Masterpage', N'PigeonModern', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'CurrentTheme', N'Current Theme', N'PigeonModern', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'DocsPrivatePath', N'Virtual Path for files upload/download in admin area', N'~/Private/Docs/', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'EmailSender', N'emails sender address', N'picce@yahoo.it', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'GMapsApiKey', N'Google maps API Key', N'AAAAAAA_BBBB_CCCCC_DDDDD', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'MetaDescription', N'', N'PigeonCms - ASP.NET content management system', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'MetaKeywords', N'', N'PigeonCms, ASP.NET, cms, content management system, csharp, open source', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'MetaSiteTitle', N'Default website pages title', N'Pigeon dev test', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'PgnVersion', N'PigeonCms last version data', N'2.1.0', N'@date: 20170621
@version: 2.1.0
@contributors: picce
@wn: see github release')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'PhotoSize_Large', N'large images width', N'250', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'PhotoSize_Medium', N'medium images width', N'105', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'PhotoSize_Small', N'small images width', N'90', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'PhotoSize_Xlarge', N'extra large images width', N'350', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'SmtpServer', N'smtp host address', N'127.0.0.1', N'')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'SmtpUser', N'smtp user', N'', NULL)
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'StaticFilesTracking', N'Google analytics static files tracking', N'false', N'Enable google analytics tracking for static files such pdf, images etc.. Works only in modules that implement this functionality. Values=true|false')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'TinyEditor-BasicHtml-menubar', N'TinyMCE editor BasicHTML menu', N'false', N'Menu bar configuration. See http://www.tinymce.com/wiki.php/Configuration:menubar')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'TinyEditor-BasicHtml-toolbar', N'TinyMCE editor BasicHTML toolbar', N'bold italic | link | alignleft aligncenter alignright', N'Menu bar configuration. See http://www.tinymce.com/wiki.php/Configuration:toolbar')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'TinyEditor-Html-menubar', N'TinyEditor-Html-menubar', N'true', N'SYSTEM')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'TinyEditor-Html-options', N'TinyEditor-Html-options', N'height: 200', N'SYSTEM')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'TinyEditor-Html-toolbar', N'TinyEditor-Html-toolbar', N'true', N'SYSTEM')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'UseCache', N'Global use of Cache', N'true', N'true | false')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Core', N'UseLog', N'Global use of LogProvider', N'false', N'true | false')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Export', N'Resources', N'Resources to export', N'#__sp_menu_cultureFlat, Menu | #__sp_modules_cultureFlat, Modules', N'example: storedProcedureName1, resourceName1 | storedProcedureName2, resourceName2')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Shop', N'CurrencyDefault', N'Default currency', N'EUR, €, Euro', N'Coma separated Code, Symbol and name. Example: EUR, €, Euro')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Shop', N'EmailTemplatesCatId', N'Email templates category', N'', N'Category linked for email templates')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Shop', N'FreeShippingMinValue', N'Free Shipping minimum value', N'-1', N'The min amout of user total cart to have free shipping, set to -1 if you don''t use it.')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Shop', N'SectionId', N'Shop section', N'', N'Section that contains Shop categories and products')
INSERT [dbo].[#__appSettings] ([keySet], [keyName], [keyTitle], [keyValue], [keyInfo]) VALUES (N'PigeonCms.Shop', N'WeightUnitDefault', N'Default weight unit', N'Kg', N'Default unit weight to calculate ship cost')
SET IDENTITY_INSERT [dbo].[#__categories] ON 

INSERT [dbo].[#__categories] ([id], [sectionId], [parentId], [enabled], [ordering], [defaultImageName], [accessType], [permissionId], [accessCode], [accessLevel], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [cssClass], [extId], [seoId]) VALUES (1, 1, 0, 1, 1, N'', 0, 0, N'', 0, 0, 0, N'', 0, N'', N'', 38)
INSERT [dbo].[#__categories] ([id], [sectionId], [parentId], [enabled], [ordering], [defaultImageName], [accessType], [permissionId], [accessCode], [accessLevel], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [cssClass], [extId], [seoId]) VALUES (2, 1, 0, 1, 2, N'', 0, 0, N'', 0, 0, 0, N'', 0, N'', N'', 21)
INSERT [dbo].[#__categories] ([id], [sectionId], [parentId], [enabled], [ordering], [defaultImageName], [accessType], [permissionId], [accessCode], [accessLevel], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [cssClass], [extId], [seoId]) VALUES (4, 1, 0, 1, 3, N'', 0, 0, N'', 0, 0, 0, N'', 0, N'', N'', 25)
SET IDENTITY_INSERT [dbo].[#__categories] OFF
INSERT [dbo].[#__categories_Culture] ([cultureName], [categoryId], [title], [description]) VALUES (N'en-US', 1, N'Static pages', N'')
INSERT [dbo].[#__categories_Culture] ([cultureName], [categoryId], [title], [description]) VALUES (N'en-US', 2, N'News', N'')
INSERT [dbo].[#__categories_Culture] ([cultureName], [categoryId], [title], [description]) VALUES (N'en-US', 4, N'Blog', N'')
INSERT [dbo].[#__categories_Culture] ([cultureName], [categoryId], [title], [description]) VALUES (N'it-IT', 1, N'Pagine statiche', N'')
INSERT [dbo].[#__categories_Culture] ([cultureName], [categoryId], [title], [description]) VALUES (N'it-IT', 2, N'News', N'')
INSERT [dbo].[#__categories_Culture] ([cultureName], [categoryId], [title], [description]) VALUES (N'it-IT', 4, N'Blog', N'')
INSERT [dbo].[#__cultures] ([cultureCode], [displayName], [enabled], [ordering], [shortCode]) VALUES (N'de-DE', N'Deutsch', 0, 3, N'de')
INSERT [dbo].[#__cultures] ([cultureCode], [displayName], [enabled], [ordering], [shortCode]) VALUES (N'en-US', N'English', 1, 1, N'en')
INSERT [dbo].[#__cultures] ([cultureCode], [displayName], [enabled], [ordering], [shortCode]) VALUES (N'es-ES', N'Espanol', 0, 2, N'es')
INSERT [dbo].[#__cultures] ([cultureCode], [displayName], [enabled], [ordering], [shortCode]) VALUES (N'it-IT', N'Italiano', 1, 0, N'it')
INSERT [dbo].[#__cultures] ([cultureCode], [displayName], [enabled], [ordering], [shortCode]) VALUES (N'sl-SI', N'Slovenski', 0, 4, N'sl')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 1, CAST(N'2016-11-01T20:53:03.110' AS DateTime), N'', N'', CAST(N'2016-11-01T20:53:03.110' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 2, CAST(N'2016-11-01T21:04:18.510' AS DateTime), N'', N'', CAST(N'2016-11-01T21:04:18.510' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 3, CAST(N'2015-10-11T00:00:00.000' AS DateTime), N'https://github.com/picce/', N'table #__itemsRelationTypes_Culture field name correction ', CAST(N'2016-11-01T21:04:18.670' AS DateTime), N'admin')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 4, CAST(N'2016-01-12T17:46:59.907' AS DateTime), N'', N'', CAST(N'2016-01-12T17:46:59.907' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 5, CAST(N'2016-01-12T17:46:59.917' AS DateTime), N'', N'', CAST(N'2016-01-12T17:46:59.917' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 6, CAST(N'2016-01-12T17:46:59.923' AS DateTime), N'', N'', CAST(N'2016-01-12T17:46:59.923' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 7, CAST(N'2016-10-10T00:00:00.000' AS DateTime), N'https://github.com/picce/', N'added handler info in routes table', CAST(N'2016-01-10T17:46:59.963' AS DateTime), N'admin')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 8, CAST(N'2017-11-01T14:44:00.000' AS DateTime), N'https://github.com/picce/', N'issue #75 (Cambio tipo di dati della proprietà DESCRIPTION in Items_Culture) - https://github.com/picce/pigeoncms/issues/75', CAST(N'2017-11-01T14:44:00.000' AS DateTime), N'admin')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Core', 9, CAST(N'2017-05-13T00:00:00.000' AS DateTime), N'https://github.com/picce/', N'feature/usermeta-oauth added members meta and user nickname', CAST(N'2017-06-21T12:48:34.457' AS DateTime), N'admin')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Shop', 1, CAST(N'2016-11-01T21:04:22.923' AS DateTime), N'', N'', CAST(N'2016-11-01T21:04:22.923' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Shop', 2, CAST(N'2016-11-01T21:04:22.927' AS DateTime), N'', N'', CAST(N'2016-11-01T21:04:22.927' AS DateTime), N'MANUAL')
INSERT [dbo].[#__dbVersion] ([componentFullName], [versionId], [versionDate], [versionDev], [versionNotes], [dateUpdated], [userUpdated]) VALUES (N'PigeonCms.Shop', 3, CAST(N'2015-03-11T00:00:00.000' AS DateTime), N'https://github.com/picce/', N'#__shop_shipments tables', CAST(N'2016-11-01T21:04:22.937' AS DateTime), N'admin')
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AD', N'AND', N'EU', N'Andorra', N'020', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AE', N'ARE', N'AS', N'United Arab Emirates', N'784', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AF', N'AFG', N'AS', N'Afghanistan', N'004', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AG', N'ATG', N'NA', N'Antigua and Barbuda', N'028', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AI', N'AIA', N'NA', N'Anguilla', N'660', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AL', N'ALB', N'EU', N'Albania', N'008', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AM', N'ARM', N'AS', N'Armenia', N'051', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AO', N'AGO', N'AF', N'Angola', N'024', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AQ', N'ATA', N'AN', N'Antarctica', N'010', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AR', N'ARG', N'SA', N'Argentina', N'032', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AS', N'ASM', N'OC', N'American Samoa', N'016', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AT', N'AUT', N'EU', N'Austria', N'040', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AU', N'AUS', N'OC', N'Australia', N'036', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AW', N'ABW', N'NA', N'Aruba', N'533', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AX', N'ALA', N'EU', N'?land', N'248', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'AZ', N'AZE', N'AS', N'Azerbaijan', N'031', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BA', N'BIH', N'EU', N'Bosnia and Herzegovina', N'070', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BB', N'BRB', N'NA', N'Barbados', N'052', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BD', N'BGD', N'AS', N'Bangladesh', N'050', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BE', N'BEL', N'EU', N'Belgium', N'056', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BF', N'BFA', N'AF', N'Burkina Faso', N'854', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BG', N'BGR', N'EU', N'Bulgaria', N'100', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BH', N'BHR', N'AS', N'Bahrain', N'048', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BI', N'BDI', N'AF', N'Burundi', N'108', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BJ', N'BEN', N'AF', N'Benin', N'204', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BL', N'BLM', N'NA', N'Saint Barth?lemy', N'652', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BM', N'BMU', N'NA', N'Bermuda', N'060', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BN', N'BRN', N'AS', N'Brunei', N'096', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BO', N'BOL', N'SA', N'Bolivia', N'068', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BQ', N'BES', N'NA', N'Bonaire', N'535', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BR', N'BRA', N'SA', N'Brazil', N'076', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BS', N'BHS', N'NA', N'Bahamas', N'044', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BT', N'BTN', N'AS', N'Bhutan', N'064', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BV', N'BVT', N'AN', N'Bouvet Island', N'074', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BW', N'BWA', N'AF', N'Botswana', N'072', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BY', N'BLR', N'EU', N'Belarus', N'112', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'BZ', N'BLZ', N'NA', N'Belize', N'084', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CA', N'CAN', N'NA', N'Canada', N'124', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CC', N'CCK', N'AS', N'Cocos [Keeling] Islands', N'166', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CD', N'COD', N'AF', N'Democratic Republic of the Congo', N'180', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CF', N'CAF', N'AF', N'Central African Republic', N'140', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CG', N'COG', N'AF', N'Republic of the Congo', N'178', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CH', N'CHE', N'EU', N'Switzerland', N'756', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CI', N'CIV', N'AF', N'Ivory Coast', N'384', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CK', N'COK', N'OC', N'Cook Islands', N'184', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CL', N'CHL', N'SA', N'Chile', N'152', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CM', N'CMR', N'AF', N'Cameroon', N'120', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CN', N'CHN', N'AS', N'China', N'156', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CO', N'COL', N'SA', N'Colombia', N'170', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CR', N'CRI', N'NA', N'Costa Rica', N'188', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CU', N'CUB', N'NA', N'Cuba', N'192', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CV', N'CPV', N'AF', N'Cape Verde', N'132', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CW', N'CUW', N'NA', N'Curacao', N'531', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CX', N'CXR', N'AS', N'Christmas Island', N'162', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CY', N'CYP', N'EU', N'Cyprus', N'196', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'CZ', N'CZE', N'EU', N'Czech Republic', N'203', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'DE', N'DEU', N'EU', N'Germany', N'276', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'DJ', N'DJI', N'AF', N'Djibouti', N'262', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'DK', N'DNK', N'EU', N'Denmark', N'208', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'DM', N'DMA', N'NA', N'Dominica', N'212', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'DO', N'DOM', N'NA', N'Dominican Republic', N'214', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'DZ', N'DZA', N'AF', N'Algeria', N'012', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'EC', N'ECU', N'SA', N'Ecuador', N'218', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'EE', N'EST', N'EU', N'Estonia', N'233', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'EG', N'EGY', N'AF', N'Egypt', N'818', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'EH', N'ESH', N'AF', N'Western Sahara', N'732', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ER', N'ERI', N'AF', N'Eritrea', N'232', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ES', N'ESP', N'EU', N'Spain', N'724', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ET', N'ETH', N'AF', N'Ethiopia', N'231', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'FI', N'FIN', N'EU', N'Finland', N'246', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'FJ', N'FJI', N'OC', N'Fiji', N'242', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'FK', N'FLK', N'SA', N'Falkland Islands', N'238', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'FM', N'FSM', N'OC', N'Micronesia', N'583', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'FO', N'FRO', N'EU', N'Faroe Islands', N'234', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'FR', N'FRA', N'EU', N'France', N'250', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GA', N'GAB', N'AF', N'Gabon', N'266', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GB', N'GBR', N'EU', N'United Kingdom', N'826', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GD', N'GRD', N'NA', N'Grenada', N'308', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GE', N'GEO', N'AS', N'Georgia', N'268', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GF', N'GUF', N'SA', N'French Guiana', N'254', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GG', N'GGY', N'EU', N'Guernsey', N'831', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GH', N'GHA', N'AF', N'Ghana', N'288', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GI', N'GIB', N'EU', N'Gibraltar', N'292', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GL', N'GRL', N'NA', N'Greenland', N'304', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GM', N'GMB', N'AF', N'Gambia', N'270', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GN', N'GIN', N'AF', N'Guinea', N'324', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GP', N'GLP', N'NA', N'Guadeloupe', N'312', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GQ', N'GNQ', N'AF', N'Equatorial Guinea', N'226', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GR', N'GRC', N'EU', N'Greece', N'300', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GS', N'SGS', N'AN', N'South Georgia and the South Sandwich Islands', N'239', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GT', N'GTM', N'NA', N'Guatemala', N'320', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GU', N'GUM', N'OC', N'Guam', N'316', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GW', N'GNB', N'AF', N'Guinea-Bissau', N'624', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'GY', N'GUY', N'SA', N'Guyana', N'328', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'HK', N'HKG', N'AS', N'Hong Kong', N'344', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'HM', N'HMD', N'AN', N'Heard Island and McDonald Islands', N'334', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'HN', N'HND', N'NA', N'Honduras', N'340', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'HR', N'HRV', N'EU', N'Croatia', N'191', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'HT', N'HTI', N'NA', N'Haiti', N'332', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'HU', N'HUN', N'EU', N'Hungary', N'348', NULL, NULL)
GO
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ID', N'IDN', N'AS', N'Indonesia', N'360', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IE', N'IRL', N'EU', N'Ireland', N'372', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IL', N'ISR', N'AS', N'Israel', N'376', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IM', N'IMN', N'EU', N'Isle of Man', N'833', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IN', N'IND', N'AS', N'India', N'356', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IO', N'IOT', N'AS', N'British Indian Ocean Territory', N'086', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IQ', N'IRQ', N'AS', N'Iraq', N'368', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IR', N'IRN', N'AS', N'Iran', N'364', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IS', N'ISL', N'EU', N'Iceland', N'352', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'IT', N'ITA', N'EU', N'Italy', N'380', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'JE', N'JEY', N'EU', N'Jersey', N'832', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'JM', N'JAM', N'NA', N'Jamaica', N'388', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'JO', N'JOR', N'AS', N'Jordan', N'400', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'JP', N'JPN', N'AS', N'Japan', N'392', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KE', N'KEN', N'AF', N'Kenya', N'404', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KG', N'KGZ', N'AS', N'Kyrgyzstan', N'417', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KH', N'KHM', N'AS', N'Cambodia', N'116', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KI', N'KIR', N'OC', N'Kiribati', N'296', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KM', N'COM', N'AF', N'Comoros', N'174', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KN', N'KNA', N'NA', N'Saint Kitts and Nevis', N'659', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KP', N'PRK', N'AS', N'North Korea', N'408', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KR', N'KOR', N'AS', N'South Korea', N'410', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KW', N'KWT', N'AS', N'Kuwait', N'414', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KY', N'CYM', N'NA', N'Cayman Islands', N'136', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'KZ', N'KAZ', N'AS', N'Kazakhstan', N'398', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LA', N'LAO', N'AS', N'Laos', N'418', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LB', N'LBN', N'AS', N'Lebanon', N'422', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LC', N'LCA', N'NA', N'Saint Lucia', N'662', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LI', N'LIE', N'EU', N'Liechtenstein', N'438', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LK', N'LKA', N'AS', N'Sri Lanka', N'144', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LR', N'LBR', N'AF', N'Liberia', N'430', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LS', N'LSO', N'AF', N'Lesotho', N'426', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LT', N'LTU', N'EU', N'Lithuania', N'440', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LU', N'LUX', N'EU', N'Luxembourg', N'442', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LV', N'LVA', N'EU', N'Latvia', N'428', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'LY', N'LBY', N'AF', N'Libya', N'434', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MA', N'MAR', N'AF', N'Morocco', N'504', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MC', N'MCO', N'EU', N'Monaco', N'492', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MD', N'MDA', N'EU', N'Moldova', N'498', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ME', N'MNE', N'EU', N'Montenegro', N'499', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MF', N'MAF', N'NA', N'Saint Martin', N'663', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MG', N'MDG', N'AF', N'Madagascar', N'450', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MH', N'MHL', N'OC', N'Marshall Islands', N'584', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MK', N'MKD', N'EU', N'Macedonia', N'807', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ML', N'MLI', N'AF', N'Mali', N'466', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MM', N'MMR', N'AS', N'Myanmar [Burma]', N'104', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MN', N'MNG', N'AS', N'Mongolia', N'496', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MO', N'MAC', N'AS', N'Macao', N'446', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MP', N'MNP', N'OC', N'Northern Mariana Islands', N'580', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MQ', N'MTQ', N'NA', N'Martinique', N'474', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MR', N'MRT', N'AF', N'Mauritania', N'478', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MS', N'MSR', N'NA', N'Montserrat', N'500', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MT', N'MLT', N'EU', N'Malta', N'470', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MU', N'MUS', N'AF', N'Mauritius', N'480', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MV', N'MDV', N'AS', N'Maldives', N'462', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MW', N'MWI', N'AF', N'Malawi', N'454', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MX', N'MEX', N'NA', N'Mexico', N'484', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MY', N'MYS', N'AS', N'Malaysia', N'458', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'MZ', N'MOZ', N'AF', N'Mozambique', N'508', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NA', N'NAM', N'AF', N'Namibia', N'516', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NC', N'NCL', N'OC', N'New Caledonia', N'540', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NE', N'NER', N'AF', N'Niger', N'562', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NF', N'NFK', N'OC', N'Norfolk Island', N'574', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NG', N'NGA', N'AF', N'Nigeria', N'566', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NI', N'NIC', N'NA', N'Nicaragua', N'558', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NL', N'NLD', N'EU', N'Netherlands', N'528', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NO', N'NOR', N'EU', N'Norway', N'578', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NP', N'NPL', N'AS', N'Nepal', N'524', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NR', N'NRU', N'OC', N'Nauru', N'520', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NU', N'NIU', N'OC', N'Niue', N'570', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'NZ', N'NZL', N'OC', N'New Zealand', N'554', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'OM', N'OMN', N'AS', N'Oman', N'512', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PA', N'PAN', N'NA', N'Panama', N'591', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PE', N'PER', N'SA', N'Peru', N'604', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PF', N'PYF', N'OC', N'French Polynesia', N'258', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PG', N'PNG', N'OC', N'Papua New Guinea', N'598', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PH', N'PHL', N'AS', N'Philippines', N'608', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PK', N'PAK', N'AS', N'Pakistan', N'586', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PL', N'POL', N'EU', N'Poland', N'616', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PM', N'SPM', N'NA', N'Saint Pierre and Miquelon', N'666', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PN', N'PCN', N'OC', N'Pitcairn Islands', N'612', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PR', N'PRI', N'NA', N'Puerto Rico', N'630', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PS', N'PSE', N'AS', N'Palestine', N'275', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PT', N'PRT', N'EU', N'Portugal', N'620', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PW', N'PLW', N'OC', N'Palau', N'585', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'PY', N'PRY', N'SA', N'Paraguay', N'600', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'QA', N'QAT', N'AS', N'Qatar', N'634', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'RE', N'REU', N'AF', N'R?union', N'638', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'RO', N'ROU', N'EU', N'Romania', N'642', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'RS', N'SRB', N'EU', N'Serbia', N'688', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'RU', N'RUS', N'EU', N'Russia', N'643', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'RW', N'RWA', N'AF', N'Rwanda', N'646', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SA', N'SAU', N'AS', N'Saudi Arabia', N'682', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SB', N'SLB', N'OC', N'Solomon Islands', N'090', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SC', N'SYC', N'AF', N'Seychelles', N'690', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SD', N'SDN', N'AF', N'Sudan', N'729', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SE', N'SWE', N'EU', N'Sweden', N'752', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SG', N'SGP', N'AS', N'Singapore', N'702', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SH', N'SHN', N'AF', N'Saint Helena', N'654', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SI', N'SVN', N'EU', N'Slovenia', N'705', NULL, NULL)
GO
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SJ', N'SJM', N'EU', N'Svalbard and Jan Mayen', N'744', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SK', N'SVK', N'EU', N'Slovakia', N'703', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SL', N'SLE', N'AF', N'Sierra Leone', N'694', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SM', N'SMR', N'EU', N'San Marino', N'674', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SN', N'SEN', N'AF', N'Senegal', N'686', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SO', N'SOM', N'AF', N'Somalia', N'706', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SR', N'SUR', N'SA', N'Suriname', N'740', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SS', N'SSD', N'AF', N'South Sudan', N'728', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ST', N'STP', N'AF', N'S?o Tom? and Pr?ncipe', N'678', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SV', N'SLV', N'NA', N'El Salvador', N'222', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SX', N'SXM', N'NA', N'Sint Maarten', N'534', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SY', N'SYR', N'AS', N'Syria', N'760', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'SZ', N'SWZ', N'AF', N'Swaziland', N'748', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TC', N'TCA', N'NA', N'Turks and Caicos Islands', N'796', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TD', N'TCD', N'AF', N'Chad', N'148', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TF', N'ATF', N'AN', N'French Southern Territories', N'260', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TG', N'TGO', N'AF', N'Togo', N'768', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TH', N'THA', N'AS', N'Thailand', N'764', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TJ', N'TJK', N'AS', N'Tajikistan', N'762', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TK', N'TKL', N'OC', N'Tokelau', N'772', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TL', N'TLS', N'OC', N'East Timor', N'626', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TM', N'TKM', N'AS', N'Turkmenistan', N'795', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TN', N'TUN', N'AF', N'Tunisia', N'788', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TO', N'TON', N'OC', N'Tonga', N'776', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TR', N'TUR', N'AS', N'Turkey', N'792', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TT', N'TTO', N'NA', N'Trinidad and Tobago', N'780', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TV', N'TUV', N'OC', N'Tuvalu', N'798', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TW', N'TWN', N'AS', N'Taiwan', N'158', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'TZ', N'TZA', N'AF', N'Tanzania', N'834', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'UA', N'UKR', N'EU', N'Ukraine', N'804', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'UG', N'UGA', N'AF', N'Uganda', N'800', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'UM', N'UMI', N'OC', N'U.S. Minor Outlying Islands', N'581', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'US', N'USA', N'NA', N'United States', N'840', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'UY', N'URY', N'SA', N'Uruguay', N'858', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'UZ', N'UZB', N'AS', N'Uzbekistan', N'860', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VA', N'VAT', N'EU', N'Vatican City', N'336', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VC', N'VCT', N'NA', N'Saint Vincent and the Grenadines', N'670', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VE', N'VEN', N'SA', N'Venezuela', N'862', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VG', N'VGB', N'NA', N'British Virgin Islands', N'092', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VI', N'VIR', N'NA', N'U.S. Virgin Islands', N'850', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VN', N'VNM', N'AS', N'Vietnam', N'704', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'VU', N'VUT', N'OC', N'Vanuatu', N'548', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'WF', N'WLF', N'OC', N'Wallis and Futuna', N'876', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'WS', N'WSM', N'OC', N'Samoa', N'882', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'XK', N'XKX', N'EU', N'Kosovo', N'0', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'YE', N'YEM', N'AS', N'Yemen', N'887', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'YT', N'MYT', N'AF', N'Mayotte', N'175', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ZA', N'ZAF', N'AF', N'South Africa', N'710', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ZM', N'ZMB', N'AF', N'Zambia', N'894', NULL, NULL)
INSERT [dbo].[#__geoCountries] ([code], [iso3], [continent], [name], [custom1], [custom2], [custom3]) VALUES (N'ZW', N'ZWE', N'AF', N'Zimbabwe', N'716', NULL, NULL)
SET IDENTITY_INSERT [dbo].[#__geoZones] ON 

INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (1, N'US', N'AL', N'Alabama', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (2, N'US', N'AK', N'Alaska', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (3, N'US', N'AZ', N'Arizona', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (4, N'US', N'AR', N'Arkansas', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (5, N'US', N'CA', N'California', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (6, N'US', N'CO', N'Colorado', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (7, N'US', N'CT', N'Connecticut', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (8, N'US', N'DE', N'Delaware', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (9, N'US', N'DC', N'District of Columbia', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (10, N'US', N'FL', N'Florida', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (11, N'US', N'GA', N'Georgia', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (12, N'US', N'HI', N'Hawaii', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (13, N'US', N'ID', N'Idaho', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (14, N'US', N'IL', N'Illinois', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (15, N'US', N'IN', N'Indiana', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (16, N'US', N'IA', N'Iowa', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (17, N'US', N'KS', N'Kansas', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (18, N'US', N'KY', N'Kentucky', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (19, N'US', N'LA', N'Louisiana', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (20, N'US', N'ME', N'Maine', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (21, N'US', N'MD', N'Maryland', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (22, N'US', N'MA', N'Massachusetts', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (23, N'US', N'MI', N'Michigan', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (24, N'US', N'MN', N'Minnesota', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (25, N'US', N'MS', N'Mississippi', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (26, N'US', N'MO', N'Missouri', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (27, N'US', N'MT', N'Montana', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (28, N'US', N'NE', N'Nebraska', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (29, N'US', N'NV', N'Nevada', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (30, N'US', N'NH', N'New Hampshire', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (31, N'US', N'NJ', N'New Jersey', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (32, N'US', N'NM', N'New Mexico', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (33, N'US', N'NY', N'New York', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (34, N'US', N'NC', N'North Carolina', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (35, N'US', N'ND', N'North Dakota', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (36, N'US', N'OH', N'Ohio', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (37, N'US', N'OK', N'Oklahoma', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (38, N'US', N'OR', N'Oregon', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (39, N'US', N'PA', N'Pennsylvania', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (40, N'US', N'RI', N'Rhode Island', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (41, N'US', N'SC', N'South Carolina', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (42, N'US', N'SD', N'South Dakota', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (43, N'US', N'TN', N'Tennessee', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (44, N'US', N'TX', N'Texas', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (45, N'US', N'UT', N'Utah', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (46, N'US', N'VT', N'Vermont', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (47, N'US', N'VA', N'Virginia', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (48, N'US', N'WA', N'Washington', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (49, N'US', N'WV', N'West Virginia', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (50, N'US', N'WI', N'Wisconsin', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (51, N'US', N'WY', N'Wyoming', NULL, NULL, NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (52, N'IT', N'TO', N'Torino', N'001', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (53, N'IT', N'VC', N'Vercelli', N'002', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (54, N'IT', N'NO', N'Novara', N'003', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (55, N'IT', N'CN', N'Cuneo', N'004', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (56, N'IT', N'AT', N'Asti', N'005', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (57, N'IT', N'AL', N'Alessandria', N'006', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (58, N'IT', N'AO', N'Valle d''Aosta/Vall?e d''Aoste', N'007', N'02', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (59, N'IT', N'IM', N'Imperia', N'008', N'07', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (60, N'IT', N'SV', N'Savona', N'009', N'07', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (61, N'IT', N'GE', N'Genova', N'010', N'07', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (62, N'IT', N'SP', N'La Spezia', N'011', N'07', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (63, N'IT', N'VA', N'Varese', N'012', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (64, N'IT', N'CO', N'Como', N'013', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (65, N'IT', N'SO', N'Sondrio', N'014', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (66, N'IT', N'MI', N'Milano', N'015', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (67, N'IT', N'BG', N'Bergamo', N'016', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (68, N'IT', N'BS', N'Brescia', N'017', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (69, N'IT', N'PV', N'Pavia', N'018', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (70, N'IT', N'CR', N'Cremona', N'019', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (71, N'IT', N'MN', N'Mantova', N'020', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (72, N'IT', N'BZ', N'Bolzano/Bozen', N'021', N'04', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (73, N'IT', N'TN', N'Trento', N'022', N'04', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (74, N'IT', N'VR', N'Verona', N'023', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (75, N'IT', N'VI', N'Vicenza', N'024', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (76, N'IT', N'BL', N'Belluno', N'025', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (77, N'IT', N'TV', N'Treviso', N'026', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (78, N'IT', N'VE', N'Venezia', N'027', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (79, N'IT', N'PD', N'Padova', N'028', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (80, N'IT', N'RO', N'Rovigo', N'029', N'05', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (81, N'IT', N'UD', N'Udine', N'030', N'06', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (82, N'IT', N'GO', N'Gorizia', N'031', N'06', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (83, N'IT', N'TS', N'Trieste', N'032', N'06', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (84, N'IT', N'PC', N'Piacenza', N'033', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (85, N'IT', N'PR', N'Parma', N'034', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (86, N'IT', N'RE', N'Reggio nell''Emilia', N'035', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (87, N'IT', N'MO', N'Modena', N'036', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (88, N'IT', N'BO', N'Bologna', N'037', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (89, N'IT', N'FE', N'Ferrara', N'038', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (90, N'IT', N'RA', N'Ravenna', N'039', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (91, N'IT', N'FC', N'Forl?-Cesena', N'040', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (92, N'IT', N'PU', N'Pesaro e Urbino', N'041', N'11', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (93, N'IT', N'AN', N'Ancona', N'042', N'11', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (94, N'IT', N'MC', N'Macerata', N'043', N'11', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (95, N'IT', N'AP', N'Ascoli Piceno', N'044', N'11', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (96, N'IT', N'MS', N'Massa-Carrara', N'045', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (97, N'IT', N'LU', N'Lucca', N'046', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (98, N'IT', N'PT', N'Pistoia', N'047', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (99, N'IT', N'FI', N'Firenze', N'048', N'09', NULL)
GO
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (100, N'IT', N'LI', N'Livorno', N'049', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (101, N'IT', N'PI', N'Pisa', N'050', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (102, N'IT', N'AR', N'Arezzo', N'051', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (103, N'IT', N'SI', N'Siena', N'052', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (104, N'IT', N'GR', N'Grosseto', N'053', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (105, N'IT', N'PG', N'Perugia', N'054', N'10', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (106, N'IT', N'TR', N'Terni', N'055', N'10', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (107, N'IT', N'VT', N'Viterbo', N'056', N'12', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (108, N'IT', N'RI', N'Rieti', N'057', N'12', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (109, N'IT', N'RM', N'Roma', N'058', N'12', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (110, N'IT', N'LT', N'Latina', N'059', N'12', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (111, N'IT', N'FR', N'Frosinone', N'060', N'12', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (112, N'IT', N'CE', N'Caserta', N'061', N'15', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (113, N'IT', N'BN', N'Benevento', N'062', N'15', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (114, N'IT', N'NA', N'Napoli', N'063', N'15', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (115, N'IT', N'AV', N'Avellino', N'064', N'15', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (116, N'IT', N'SA', N'Salerno', N'065', N'15', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (117, N'IT', N'AQ', N'L''Aquila', N'066', N'13', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (118, N'IT', N'TE', N'Teramo', N'067', N'13', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (119, N'IT', N'PE', N'Pescara', N'068', N'13', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (120, N'IT', N'CH', N'Chieti', N'069', N'13', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (121, N'IT', N'CB', N'Campobasso', N'070', N'14', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (122, N'IT', N'FG', N'Foggia', N'071', N'16', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (123, N'IT', N'BA', N'Bari', N'072', N'16', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (124, N'IT', N'TA', N'Taranto', N'073', N'16', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (125, N'IT', N'BR', N'Brindisi', N'074', N'16', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (126, N'IT', N'LE', N'Lecce', N'075', N'16', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (127, N'IT', N'PZ', N'Potenza', N'076', N'17', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (128, N'IT', N'MT', N'Matera', N'077', N'17', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (129, N'IT', N'CS', N'Cosenza', N'078', N'18', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (130, N'IT', N'CZ', N'Catanzaro', N'079', N'18', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (131, N'IT', N'RC', N'Reggio di Calabria', N'080', N'18', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (132, N'IT', N'TP', N'Trapani', N'081', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (133, N'IT', N'PA', N'Palermo', N'082', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (134, N'IT', N'ME', N'Messina', N'083', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (135, N'IT', N'AG', N'Agrigento', N'084', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (136, N'IT', N'CL', N'Caltanissetta', N'085', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (137, N'IT', N'EN', N'Enna', N'086', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (138, N'IT', N'CT', N'Catania', N'087', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (139, N'IT', N'RG', N'Ragusa', N'088', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (140, N'IT', N'SR', N'Siracusa', N'089', N'19', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (141, N'IT', N'SS', N'Sassari', N'090', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (142, N'IT', N'NU', N'Nuoro', N'091', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (143, N'IT', N'CA', N'Cagliari', N'092', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (144, N'IT', N'PN', N'Pordenone', N'093', N'06', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (145, N'IT', N'IS', N'Isernia', N'094', N'14', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (146, N'IT', N'OR', N'Oristano', N'095', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (147, N'IT', N'BI', N'Biella', N'096', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (148, N'IT', N'LC', N'Lecco', N'097', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (149, N'IT', N'LO', N'Lodi', N'098', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (150, N'IT', N'RN', N'Rimini', N'099', N'08', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (151, N'IT', N'PO', N'Prato', N'100', N'09', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (152, N'IT', N'KR', N'Crotone', N'101', N'18', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (153, N'IT', N'VV', N'Vibo Valentia', N'102', N'18', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (154, N'IT', N'VB', N'Verbano-Cusio-Ossola', N'103', N'01', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (155, N'IT', N'OT', N'Olbia-Tempio', N'104', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (156, N'IT', N'OG', N'Ogliastra', N'105', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (157, N'IT', N'VS', N'Medio Campidano', N'106', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (158, N'IT', N'CI', N'Carbonia-Iglesias', N'107', N'20', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (159, N'IT', N'MB', N'Monza e della Brianza', N'108', N'03', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (160, N'IT', N'FM', N'Fermo', N'109', N'11', NULL)
INSERT [dbo].[#__geoZones] ([id], [countryCode], [code], [name], [custom1], [custom2], [custom3]) VALUES (161, N'IT', N'BT', N'Barletta-Andria-Trani', N'110', N'16', NULL)
SET IDENTITY_INSERT [dbo].[#__geoZones] OFF
SET IDENTITY_INSERT [dbo].[#__items] ON 

INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (1, N'PigeonCms.Item', 2, 1, 1, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, NULL, NULL, NULL, N'titolo-news-3', 0, 0, 0, N'', 0, 1, N'o-col--33', N'', 23)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (2, N'PigeonCms.Item', 2, 1, 2, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 1, 0, N'', 0, NULL, NULL, NULL, N'titolo-news-4', 0, 0, 0, N'', 0, 2, N'o-col--33', N'', 24)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (3, N'PigeonCms.Item', 2, 1, 3, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'news-5', 0, 0, 0, N'', 0, 3, N'o-col--50', N'', 26)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (4, N'PigeonCms.Item', 2, 1, 4, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 1, 43, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'news-6', 0, 0, 0, N'', 0, 4, N'o-col--50 o-teaser-single--double-height', N'', 27)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (5, N'PigeonCms.Item', 4, 1, 5, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'blog-post-1', 0, 0, 0, N'', 0, 5, N'o-blog-single--purple', N'', 28)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (6, N'PigeonCms.Item', 4, 1, 6, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'blog-post-2', 0, 0, 0, N'', 0, 6, N'o-blog-single--pink', N'', 29)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (7, N'PigeonCms.Item', 4, 1, 7, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'blog-post-3', 0, 0, 0, N'', 0, 7, N'o-blog-single--green', N'', 30)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (8, N'PigeonCms.Item', 1, 1, 8, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'generic-error', 0, 0, 0, N'', 0, 8, N'', N'', 31)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (9, N'PigeonCms.Item', 1, 1, 9, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-06-12T00:00:00.000' AS DateTime), CAST(N'2016-06-12T00:00:00.000' AS DateTime), NULL, N'dashboard', 0, 0, 0, N'', 0, 9, N'', N'', 32)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (10, N'PigeonCms.Item', 1, 1, 10, N'', NULL, N'admin', NULL, N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'', N'', N'', N'', N'', 0, 0, N'', 0, CAST(N'2016-07-12T00:00:00.000' AS DateTime), CAST(N'2016-07-12T00:00:00.000' AS DateTime), NULL, N'page-not-found', 0, 0, 0, N'', 0, 10, N'', N'', 35)
INSERT [dbo].[#__items] ([id], [itemType], [categoryId], [enabled], [ordering], [defaultImageName], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [CustomBool1], [CustomBool2], [CustomBool3], [CustomBool4], [CustomDate1], [CustomDate2], [CustomDate3], [CustomDate4], [CustomDecimal1], [CustomDecimal2], [CustomDecimal3], [CustomDecimal4], [CustomInt1], [CustomInt2], [CustomInt3], [CustomInt4], [CustomString1], [CustomString2], [CustomString3], [CustomString4], [ItemParams], [accessType], [permissionId], [accessCode], [accessLevel], [itemDate], [validFrom], [validTo], [alias], [commentsGroupId], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [threadId], [cssClass], [extId], [seoId]) VALUES (11, N'PigeonCms.HelloWorldItem', 2, 1, 11, N'', CAST(N'2017-06-21T13:00:54.930' AS DateTime), N'admin', CAST(N'2017-06-21T14:27:07.353' AS DateTime), N'admin', 0, 0, 0, 0, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, N'{"TextSimple":"simple test","TextSimpleLocalized":{"it-IT":"simp ita","en-US":"simp en"},"TextHtml":"<p>html text sample sample lorem</p>","TextHtmlLocalized":{"it-IT":"<p>html ita</p>","en-US":"<p>html eng</p>"},"SelectColor":"2","Image1":"~/public/gallery/items/11/myfolder/alias1-image1.jpg","File1":null,"Flag1":false,"Number1":0,"Blocks":[{"type":"Header","data":{"Title":{"it-IT":"<p>tit <strong>bold</strong></p>","en-US":""},"TitleStyle":"gradient","Image":"~/public/gallery/items/11/movie1_header_large.jpg","Abstract":{"it-IT":"<p>abs</p>","en-US":""},"Blocks":null,"MapAttributeValue":"CustomString1"}}],"MapAttributeValue":"CustomString1"}', N'{"OtherSimpleText":"","Blocks":null,"MapAttributeValue":"CustomString2"}', N'', N'', N'', 0, 0, N'', 0, CAST(N'2017-06-21T00:00:00.000' AS DateTime), CAST(N'2017-06-21T00:00:00.000' AS DateTime), NULL, N'alias1', 0, 0, 0, N'', 0, 11, N'', N'', 12)
SET IDENTITY_INSERT [dbo].[#__items] OFF
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 1, N'', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.</p>
<hr class="system-readmore" />
<p>Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>
<p>&nbsp;</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 2, N'', N'')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 3, N'news-5', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.&nbsp;</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 4, N'news 6 (admin only)', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.&nbsp;</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 5, N'blog post 1', N'')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 6, N'blog post 2', N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem voluptatum iure ducimus ipsa voluptate reprehenderit eum minima accusantium iste adipisci eaque, labore, animi hic porro voluptates, unde sapiente aliquid nihil!</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 7, N'', N'')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 8, N'Error occurred', N'<p>Ops! An error occurred</p>
<p>The resource you requested caused an error.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 9, N'User dashboard', N'')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 10, N'Page not found', N'<p>404 - Page not found :(</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'en-US', 11, N'tit en', N'<p><em>desc eng</em></p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 1, N'titolo NEWS 33', N'<p>LOREM&nbsp;ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.</p>
<hr class="system-readmore" />
<p>Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem.</p>
<p>Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula.</p>
<hr class="system-pagebreak" />
<p>Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien.</p>
<hr class="system-pagebreak" />
<p>Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>
<p>&nbsp;</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 2, N'titolo news 4', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.&nbsp;</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 3, N'news-5', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.&nbsp;</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 4, N'news 6 (admin only)', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.&nbsp;</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 5, N'blog post 1', N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem voluptatum iure ducimus ipsa voluptate reprehenderit eum minima accusantium iste adipisci eaque, labore, animi hic porro voluptates, unde sapiente aliquid nihil!</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 6, N'blog post 2', N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem voluptatum iure ducimus ipsa voluptate reprehenderit eum minima accusantium iste adipisci eaque, labore, animi hic porro voluptates, unde sapiente aliquid nihil!</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 7, N'blog post 3', N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
<hr class="system-readmore" />
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem voluptatum iure ducimus ipsa voluptate reprehenderit eum minima accusantium iste adipisci eaque, labore, animi hic porro voluptates, unde sapiente aliquid nihil!</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 8, N'Errore', N'<p>Ops! Si &egrave; verificato un errore.<br /> La risorsa richiesta ha generato un errore.</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 9, N'User dashboard', N'')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 10, N'Pagina non trovata', N'<p>404 - Page not found :(</p>')
INSERT [dbo].[#__items_Culture] ([cultureName], [itemId], [title], [description]) VALUES (N'it-IT', 11, N'tit ita', N'<p>desc ita</p>')
SET IDENTITY_INSERT [dbo].[#__labels] ON 

INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (3, N'it-IT', N'PigeonCms.EmailContactForm', N'LblInfoAdulti', N'adulti', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (4, N'en-US', N'PigeonCms.MenuAdmin', N'LblMenuType', N'Menu type', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (6, N'it-IT', N'PigeonCms.MenuAdmin', N'LblMenuType', N'Tipo menu', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (7, N'en-US', N'PigeonCms.MenuAdmin', N'LblContentType', N'Content type', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (8, N'it-IT', N'PigeonCms.MenuAdmin', N'LblContentType', N'Tipo contenuto', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (9, N'en-US', N'PigeonCms.Menuadmin', N'LblName', N'Name', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (10, N'it-IT', N'PigeonCms.Menuadmin', N'LblName', N'Nome', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (11, N'en-US', N'PigeonCms.Menuadmin', N'LblTitle', N'Title', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (12, N'it-IT', N'PigeonCms.Menuadmin', N'LblTitle', N'Titolo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (13, N'en-US', N'PigeonCms.Menuadmin', N'LblAlias', N'Alias', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (15, N'en-US', N'PigeonCms.Menuadmin', N'LblRoute', N'Route', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (17, N'en-US', N'PigeonCms.Menuadmin', N'LblLink', N'Link', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (18, N'en-US', N'PigeonCms.Menuadmin', N'LblRedirectTo', N'Redirect to', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (19, N'it-IT', N'PigeonCms.Menuadmin', N'LblRedirectTo', N'Trasferisci a', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (20, N'en-US', N'PigeonCms.Menuadmin', N'LblParentItem', N'Parent item', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (21, N'it-IT', N'PigeonCms.Menuadmin', N'LblParentItem', N'Elemento padre', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (22, N'en-US', N'PigeonCms.Menuadmin', N'ModuleTitle', N'Menu entries', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (23, N'it-IT', N'PigeonCms.Menuadmin', N'ModuleTitle', N'Voci menu', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (24, N'en-US', N'PigeonCms.Menuadmin', N'ModuleDescription', N'Menu items admin area', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (25, N'en-US', N'PigeonCms.StaticPage', N'PageNameLabel', N'Static content', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (26, N'it-IT', N'PigeonCms.StaticPage', N'PageNameLabel', N'Contenuto statico', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (27, N'en-US', N'PigeonCms.StaticPage', N'PageNameDescription', N'Name of the static page to display. If the content does not yet exist create it using [contents > static page] menu.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (28, N'it-IT', N'PigeonCms.StaticPage', N'PageNameDescription', N'Nome della pagina statica da visualizzare. Se il contenuto non esiste ancora crearlo dal menu [contenuti > pagine statiche].', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (29, N'en-US', N'PigeonCms.VideoPlayer', N'FileLabel', N'File', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (30, N'en-US', N'PigeonCms.VideoPlayer', N'FileDescription', N'Source file path or url', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (31, N'it-IT', N'PigeonCms.VideoPlayer', N'FileDescription', N'Percorso e indirizzo del file sorgente', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (32, N'en-US', N'PigeonCms.VideoPlayer', N'WidthLabel', N'Width', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (33, N'it-IT', N'PigeonCms.VideoPlayer', N'WidthLabel', N'Larghezza', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (34, N'en-US', N'PigeonCms.VideoPlayer', N'WidthDescription', N'Video width', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (35, N'it-IT', N'PigeonCms.VideoPlayer', N'WidthDescription', N'Larghezza del video', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (36, N'en-US', N'PigeonCms.VideoPlayer', N'HeightLabel', N'Height', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (37, N'it-IT', N'PigeonCms.VideoPlayer', N'HeightLabel', N'Altezza', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (38, N'en-US', N'PigeonCms.VideoPlayer', N'HeightDescription', N'Video Height', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (39, N'it-IT', N'PigeonCms.VideoPlayer', N'HeightDescription', N'Altezza del video', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (40, N'en-US', N'PigeonCms.VideoPlayer', N'ModuleTitle', N'Video player', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (41, N'it-IT', N'PigeonCms.VideoPlayer', N'ModuleTitle', N'Visualizzatore video', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (42, N'en-US', N'PigeonCms.Menuadmin', N'LblDetails', N'Details', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (44, N'it-IT', N'PigeonCms.Menuadmin', N'LblDetails', N'Dettagli', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (45, N'en-US', N'PigeonCms.Menuadmin', N'LblOptions', N'Options', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (46, N'it-IT', N'PigeonCms.Menuadmin', N'LblOptions', N'Opzioni', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (47, N'en-US', N'PigeonCms.Menuadmin', N'LblVisible', N'Visible', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (48, N'it-IT', N'PigeonCms.Menuadmin', N'LblVisible', N'Visibile', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (49, N'en-US', N'PigeonCms.Menuadmin', N'LblPublished', N'Published', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (50, N'it-IT', N'PigeonCms.Menuadmin', N'LblPublished', N'Pubblicato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (51, N'en-US', N'PigeonCms.Menuadmin', N'LblOverrideTitle', N'Override page title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (52, N'it-IT', N'PigeonCms.Menuadmin', N'LblOverrideTitle', N'Sovrascrivi titolo finestra', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (53, N'en-US', N'PigeonCms.Menuadmin', N'LblRecordInfo', N'Record info', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (54, N'it-IT', N'PigeonCms.Menuadmin', N'LblRecordInfo', N'Informazioni record', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (55, N'it-IT', N'PigeonCms.VideoPlayer', N'FileLabel', N'File', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (56, N'en-US', N'PigeonCms.Menuadmin', N'LblSecurity', N'Security', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (58, N'it-IT', N'PigeonCms.Menuadmin', N'LblSecurity', N'Sicurezza', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (59, N'en-US', N'PigeonCms.Menuadmin', N'LblViews', N'Views', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (60, N'it-IT', N'PigeonCms.Menuadmin', N'LblViews', N'Visualizzazioni', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (61, N'en-US', N'PigeonCms.Menuadmin', N'LblParameters', N'Parameters', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (62, N'it-IT', N'PigeonCms.Menuadmin', N'LblParameters', N'Parametri', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (63, N'it-IT', N'PigeonCms.OfflineAdmin', N'LblTitle', N'Titolo messaggio', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (64, N'en-US', N'PigeonCms.OfflineAdmin', N'LblTitle', N'Message title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (65, N'it-IT', N'PigeonCms.OfflineAdmin', N'LblMessage', N'Messaggio', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (66, N'en-US', N'PigeonCms.OfflineAdmin', N'LblMessage', N'Message', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (67, N'it-IT', N'PigeonCms.OfflineAdmin', N'LblOffline', N'Sito offline', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (68, N'en-US', N'PigeonCms.OfflineAdmin', N'LblOffline', N'Site offline', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (69, N'it-IT', N'PigeonCms.OfflineAdmin', N'ModuleTitle', N'Gestione sito offline', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (70, N'en-US', N'PigeonCms.OfflineAdmin', N'ModuleTitle', N'Offline admin', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (71, N'it-IT', N'PigeonCms.OfflineAdmin', N'LitOfflineWarning', N'ATTENZIONE. Stai mettendo il sito offline.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (72, N'en-US', N'PigeonCms.OfflineAdmin', N'LitOfflineWarning', N'WARNING. You are going to put website offline.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (73, N'en-US', N'PigeonCms.TopMenu', N'MenuLevelLabel', N'Menu level', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (74, N'it-IT', N'PigeonCms.TopMenu', N'MenuLevelLabel', N'Livello menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (75, N'en-US', N'PigeonCms.TopMenu', N'ShowChildLabel', N'Show childs', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (76, N'it-IT', N'PigeonCms.TopMenu', N'ShowChildLabel', N'Visualizza sottomenu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (77, N'en-US', N'PigeonCms.TopMenu', N'ListClassLabel', N'List css class', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (78, N'it-IT', N'PigeonCms.TopMenu', N'ListClassLabel', N'Classe css lista', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (79, N'en-US', N'PigeonCms.TopMenu', N'ItemSelectedClassLabel', N'Selected item css class', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (80, N'it-IT', N'PigeonCms.TopMenu', N'ItemSelectedClassLabel', N'Classe Css Elemento selezionato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (81, N'en-US', N'PigeonCms.TopMenu', N'ItemLastClassLabel', N'Last item css class', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (82, N'it-IT', N'PigeonCms.TopMenu', N'ItemLastClassLabel', N'Classe css ultimo elemento', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (83, N'en-US', N'PigeonCms.TopMenu', N'ShowPagePostFixLabel', N'Show page extension', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (84, N'it-IT', N'PigeonCms.TopMenu', N'ShowPagePostFixLabel', N'Visualizza estensione pagina', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (85, N'en-US', N'PigeonCms.TopMenu', N'ShowPagePostFixDescription', N'Show page .aspx extension', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (86, N'it-IT', N'PigeonCms.TopMenu', N'ShowPagePostFixDescription', N'Visualizza l''estensione .aspx della pagina', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (87, N'en-US', N'PigeonCms.TopMenu', N'MenuIdLabel', N'Css menu Id', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (88, N'it-IT', N'PigeonCms.TopMenu', N'MenuIdLabel', N'Css ID del menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (89, N'en-US', N'PigeonCms.TopMenu', N'MenuIdDescription', N'Css ID used for current menu.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (90, N'it-IT', N'PigeonCms.TopMenu', N'MenuIdDescription', N'ID css associato al menu corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (93, N'en-US', N'PigeonCms.ItemsAdmin', N'HomepageLabel', N'Show in homepage', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (94, N'it-IT', N'PigeonCms.ItemsAdmin', N'HomepageLabel', N'Visualizza in homepage', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (95, N'en-US', N'PigeonCms.MenuAdmin', N'LblTitleWindow', N'Window''s title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (96, N'it-IT', N'PigeonCms.MenuAdmin', N'LblTitleWindow', N'Titolo finestra', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (107, N'en-US', N'PigeonCms.ItemsAdmin', N'LblValidFrom', N'Valid from', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (109, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblValidFrom', N'Valido dal', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (110, N'en-US', N'PigeonCms.ItemsAdmin', N'LblValidTo', N'Valid to', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (111, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblValidTo', N'Valido fino al', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (114, N'en-US', N'PigeonCms.ItemsAdmin', N'LblTitle', N'Title', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (115, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblTitle', N'Titolo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (116, N'en-US', N'PigeonCms.ItemsAdmin', N'LblDescription', N'Description', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (117, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblDescription', N'Descrizione', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (118, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblCategory', N'Categoria', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (119, N'en-US', N'PigeonCms.ItemsAdmin', N'LblCategory', N'Category', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (120, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblEnabled', N'Abilitato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (121, N'en-US', N'PigeonCms.ItemsAdmin', N'LblEnabled', N'Enabled', N'', 2, 1, NULL)
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (122, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblItemType', N'Tipo elemento', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (123, N'en-US', N'PigeonCms.ItemsAdmin', N'LblItemType', N'Item type', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (124, N'it-IT', N'PigeonCms.ItemsAdmin', N'ModuleTitle', N'Gestione elementi', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (125, N'en-US', N'PigeonCms.ItemsAdmin', N'ModuleTitle', N'Items manager', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (126, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblItemDate', N'Data elemento', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (127, N'en-US', N'PigeonCms.ItemsAdmin', N'LblItemDate', N'Item date', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (128, N'en-US', N'PigeonCms.EmailContactForm', N'LblGenericError', N'An error occured', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (129, N'it-IT', N'PigeonCms.EmailContactForm', N'LblGenericError', N'Si ? veirificato un errore', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (130, N'en-US', N'PigeonCms.EmailContactForm', N'LblGenericSucces', N'Operation completed', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (132, N'it-IT', N'PigeonCms.EmailContactForm', N'LblGenericSuccess', N'Operazione completata', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (133, N'it-IT', N'PigeonCms.FilesManager', N'FileExtensionsLabel', N'Estensioni file consentite', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (134, N'en-US', N'PigeonCms.FilesManager', N'FileExtensionsLabel', N'Allowed files extensions', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (135, N'it-IT', N'PigeonCms.FilesManager', N'FileExtensionsDescription', N'esempio: jpg;gif;doc', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (136, N'en-US', N'PigeonCms.FilesManager', N'FileExtensionsDescription', N'example: jpg;gif;doc', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (137, N'it-IT', N'PigeonCms.FilesManager', N'FileSizeLabel', N'Dimensione max file (KB)', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (138, N'en-US', N'PigeonCms.FilesManager', N'FileSizeLabel', N'Max file size (KB)', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (139, N'it-IT', N'PigeonCms.FilesManager', N'FileNameTypeLabel', N'Nome file', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (140, N'en-US', N'PigeonCms.FilesManager', N'FileNameTypeLabel', N'File name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (141, N'it-IT', N'PigeonCms.FilesManager', N'FileNameTypeDescription', N'Specifica come come sar? composto il file di destinazione', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (142, N'en-US', N'PigeonCms.FilesManager', N'FileNameTypeDescription', N'Specifies uploaded file name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (143, N'it-IT', N'PigeonCms.FilesManager', N'FilePrefixLabel', N'Prefisso file caricato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (144, N'en-US', N'PigeonCms.FilesManager', N'FilePrefixLabel', N'Uploaded file prefix', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (145, N'it-IT', N'PigeonCms.FilesManager', N'FilePrefixDescription', N'Applicato solo se non viene scelto di mantenere il nome file originale', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (146, N'en-US', N'PigeonCms.FilesManager', N'FilePrefixDescription', N'It will be applied only if you choose not to mantain original file name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (147, N'it-IT', N'PigeonCms.FilesManager', N'FilePathLabel', N'Path di caricamento files', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (148, N'en-US', N'PigeonCms.FilesManager', N'FilePathLabel', N'Uploaded files path', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (149, N'it-IT', N'PigeonCms.FilesManager', N'FilePathDescription', N'Path dove verrranno caricati i files (es. ~/Public/Files)', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (150, N'en-US', N'PigeonCms.FilesManager', N'FilePathDescription', N'Destination path for uploaded files (ex. ~/Public/Gallery)', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (151, N'it-IT', N'PigeonCms.FilesManager', N'UploadFieldsLabel', N'Max numero di campi upload', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (152, N'en-US', N'PigeonCms.FilesManager', N'UploadFieldsLabel', N'Max number of upload fields', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (153, N'it-IT', N'PigeonCms.FilesManager', N'UploadFieldsDescription', N'Numero massimo di files che si possono caricare contemporaneamente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (154, N'en-US', N'PigeonCms.FilesManager', N'UploadFieldsDescription', N'Max files to upload concurrently', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (155, N'it-IT', N'PigeonCms.MenuAdmin', N'LblMenuTypeDescription', N'Tipo di menu tra quelli creati al quale appartiene la voce corrente.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (156, N'en-US', N'PigeonCms.MenuAdmin', N'LblMenuTypeDescription', N'Menu type owner of current menu entry', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (157, N'it-IT', N'PigeonCms.MenuAdmin', N'LblContentTypeDescription', N'Tipo di contenuto visualizzato dalla voce di menu corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (158, N'en-US', N'PigeonCms.MenuAdmin', N'LblContentTypeDescription', N'Content type show by current menu entry', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (159, N'it-IT', N'PigeonCms.MenuAdmin', N'LblNameDescription', N'Nome mnemonico assegnato alla voce menu. Utilizzato solo per riconoscere la voce nella lista.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (160, N'en-US', N'PigeonCms.MenuAdmin', N'LblNameDescription', N'Mnemonic name assigned to the menu item. Used only to recognize the entry in the list.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (161, N'it-IT', N'PigeonCms.MenuAdmin', N'LblTitleDescription', N'Titolo della voce di menu. Apparir? come link nel menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (162, N'en-US', N'PigeonCms.MenuAdmin', N'LblTitleDescription', N'Title of the menu item. It will be the text of the link in menu items', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (163, N'it-IT', N'PigeonCms.MenuAdmin', N'LblTitleWindowDescription', N'Titolo della finestra del browser', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (164, N'en-US', N'PigeonCms.MenuAdmin', N'LblTitleWindowDescription', N'Title of the browser window', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (165, N'it-IT', N'PigeonCms.MenuAdmin', N'LblAliasDescription', N'Nome della pagina nell url della risorsa corrente. Esempio: mia-pagina', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (166, N'en-US', N'PigeonCms.MenuAdmin', N'LblAliasDescription', N'Page name in the url of the current resource. Example: my-page', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (167, N'it-IT', N'PigeonCms.MenuAdmin', N'LblRouteDescription', N'Percorso virtuale di accesso alla risorsa. Scegliere un percorso significativo per la voce corrente.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (168, N'en-US', N'PigeonCms.MenuAdmin', N'LblRouteDescription', N'Virtual path to access current resource. Choose a location significant to the current entry.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (169, N'it-IT', N'PigeonCms.MenuAdmin', N'LblLinkDescription', N'Collegamento ad una risorsa locale o esterna (sole se tipomenu link o javascript). Es.: http://www.google.it, mypage.aspx, ~/pages/mypage.aspx, alert()', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (170, N'en-US', N'PigeonCms.MenuAdmin', N'LblLinkDescription', N'Link to local or external resource (only for menutype link or javascript). Example: http://www.google.it, mypage.aspx, ~/pages/mypage.aspx, alert()', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (171, N'it-IT', N'PigeonCms.MenuAdmin', N'LblRedirectToDescription', N'Solo per tipo menu=alias. Scegliere la risorsa menu alla quale essere reindirizzato quando si accede alla voce di menu corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (172, N'en-US', N'PigeonCms.MenuAdmin', N'LblRedirectToDescription', N'Only for menutype=alias. Choose the menu resource to be redirected to when you click current menu item.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (173, N'it-IT', N'PigeonCms.MenuAdmin', N'LblParentItemDescription', N'Scegli la voce di menu padre alla quale la voce corrente dovr? appartenere per creare un sottomenu. ', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (174, N'en-US', N'PigeonCms.MenuAdmin', N'LblParentItemDescription', N'Choose parent menu entry for current item to create a submenu.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (175, N'it-IT', N'PigeonCms.MenuAdmin', N'LblCssClass', N'Classe css', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (176, N'en-US', N'PigeonCms.MenuAdmin', N'LblCssClass', N'Css class', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (177, N'it-IT', N'PigeonCms.MenuAdmin', N'LblCssClassDescription', N'Classe css personalizzata per la voce di menu corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (178, N'en-US', N'PigeonCms.MenuAdmin', N'LblCssClassDescription', N'Custom css class for current menu entry', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (179, N'it-IT', N'PigeonCms.MenuAdmin', N'LblTheme', N'Tema pagina', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (180, N'en-US', N'PigeonCms.MenuAdmin', N'LblTheme', N'Theme', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (181, N'it-IT', N'PigeonCms.MenuAdmin', N'LblThemeDescription', N'Tema di visualizzazione per la pagina corrente. Se non specificato verr? utilizzato quello di default per la Route scelta o quello di default del sito web.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (182, N'en-US', N'PigeonCms.MenuAdmin', N'LblThemeDescription', N'Display theme for the current page. If not specified, the default will be used for Route choice or the default Web site one.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (183, N'it-IT', N'PigeonCms.MenuAdmin', N'LblMasterpageDescription', N'Layout grafico per la pagina corrente. Se non specificato verr? utilizzato quello di default per la Route scelta o quello di default del sito web.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (184, N'en-US', N'PigeonCms.MenuAdmin', N'LblMasterpageDescription', N'Graphical layout for the current page. If not specified, the default will be used for Route choice or the default Web site one.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (185, N'it-IT', N'PigeonCms.MenuAdmin', N'LblVisibleDescription', N'Visualizza la voce di menu.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (186, N'en-US', N'PigeonCms.MenuAdmin', N'LblVisibleDescription', N'Show or not menu entry', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (187, N'it-IT', N'PigeonCms.MenuAdmin', N'LblPublishedDescription', N'Pubblica o meno il contenuto. La voce pu? essere non visibile ma pubblicata. In questo modo non sar? visibile a menu ma sar? comunque raggiungibile tramite il suo url.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (188, N'en-US', N'PigeonCms.MenuAdmin', N'LblPublishedDescription', N'Publish or not the content. The entry could be published but not visible, in this way you will not see the menu entry but it will be accessible by using its URL.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (189, N'it-IT', N'PigeonCms.MenuAdmin', N'LblOverrideTitleDescription', N'Se selezionato imposta il titolo della finestra del browser con il testo della casella Titolo Finestra', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (190, N'en-US', N'PigeonCms.MenuAdmin', N'LblOverrideTitleDescription', N'If selected sets the title of the browser window with the Window Title text box', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (191, N'it-IT', N'PigeonCms.MenuAdmin', N'LblShowModuleTitle', N'Visualizza titolo modulo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (192, N'en-US', N'PigeonCms.MenuAdmin', N'LblShowModuleTitle', N'Show module title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (193, N'it-IT', N'PigeonCms.MenuAdmin', N'LblShowModuleTitleDescription', N'Visualizza il titolo del modulo associato al menu corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (194, N'en-US', N'PigeonCms.MenuAdmin', N'LblShowModuleTitleDescription', N'Displays the title of the module associated with the current menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (195, N'it-IT', N'PigeonCms.PermissionsControl', N'LblSecurity', N'Sicurezza', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (196, N'en-US', N'PigeonCms.PermissionsControl', N'LblSecurity', N'Security', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (197, N'it-IT', N'PigeonCms.PermissionsControl', N'LblAccessType', N'Tipo di accesso', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (198, N'en-US', N'PigeonCms.PermissionsControl', N'LblAccessType', N'Access type', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (199, N'it-IT', N'PigeonCms.PermissionsControl', N'LblAccessTypeDescription', N'Tipo di accesso alla risorsa. [Public] visibile ad ogni utente. [Registered] visibile solo agli utenti autenticati che soddisfano il [codice di accesso] ed il [livello di accesso] quando impostati.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (200, N'en-US', N'PigeonCms.PermissionsControl', N'LblAccessTypeDescription', N'Resource access type. [Public] visible for all users. [Registered] visible only for logged in users only authenticated users that meet the [access code] and [access level] when set.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (201, N'it-IT', N'PigeonCms.PermissionsControl', N'LblRolesAllowed', N'Ruoli consentiti', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (202, N'en-US', N'PigeonCms.PermissionsControl', N'LblRolesAllowed', N'Allowed roles', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (203, N'it-IT', N'PigeonCms.PermissionsControl', N'LblRolesAllowedDescription', N'Ruoli utente che possono accedere alla risorsa. Solo per tipo accesso (registrato). Tenere premuto CTRL per effettuare una selezione multipla.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (204, N'en-US', N'PigeonCms.PermissionsControl', N'LblRolesAllowedDescription', N'User roles that can access the resource. Only for access type (registered). Hold down CTRL to make a multiple selection.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (205, N'it-IT', N'PigeonCms.PermissionsControl', N'LblAccessCode', N'Codice accesso', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (206, N'en-US', N'PigeonCms.PermissionsControl', N'LblAccessCode', N'Access code', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (207, N'it-IT', N'PigeonCms.PermissionsControl', N'LblAccessCodeDescription', N'Codice di accesso utente necessario per accedere alla risorsa. Se vuoto non verr? considerato.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (208, N'en-US', N'PigeonCms.PermissionsControl', N'LblAccessCodeDescription', N'User access code required to access the resource. If empty will not be considered.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (209, N'it-IT', N'PigeonCms.PermissionsControl', N'LblAccessLevel', N'Livello di accesso', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (210, N'en-US', N'PigeonCms.PermissionsControl', N'LblAccessLevel', N'Acces level', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (211, N'it-IT', N'PigeonCms.PermissionsControl', N'LblAccessLevelDescription', N'Minimo livello di accesso utente necessario per accedere alla risorsa. Se vuoto non verr? considerato.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (212, N'en-US', N'PigeonCms.PermissionsControl', N'LblAccessLevelDescription', N'Minimum user access level required to access the resource. If empty will not be considered.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (213, N'it-IT', N'PigeonCms.ModuleParams', N'LblUseCache', N'Usa la cache', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (214, N'en-US', N'PigeonCms.ModuleParams', N'LblUseCache', N'Use cache', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (215, N'it-IT', N'PigeonCms.ModuleParams', N'LblUseCacheDescription', N'Consente al modulo di caricare i dati dalla cache del server (solo se supportato dal modulo). Use global mantiene le impostazioni globali del sito. ', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (216, N'en-US', N'PigeonCms.ModuleParams', N'LblUseCacheDescription', N'Allows the module to load data from the web server cache (if supported by the module). Use global maintains the site default settings. ', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (217, N'it-IT', N'PigeonCms.ModuleParams', N'LblUseLog', N'Registra log', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (218, N'en-US', N'PigeonCms.ModuleParams', N'LblUseLog', N'Use log', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (219, N'it-IT', N'PigeonCms.ModuleParams', N'LblUseLogDescription', N'Registra gli eventi significativi del modulo nel registro di log (solo se supportato dal modulo). Use global mantiene le impostazioni globali del sito. ATTENZIONE: se abilitato aumenta il carico di lavoro del server.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (220, N'en-US', N'PigeonCms.ModuleParams', N'LblUseLogDescription', N'Save significant events of the module in the log list (only if supported by the module). Use global mantains site default settings. WARNING: it increases the workload of the server when enabled.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (221, N'it-IT', N'PigeonCms.ModuleParams', N'LblCssFile', N'File css', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (222, N'en-US', N'PigeonCms.ModuleParams', N'LblCssFile', N'Css file', N'', 2, 1, NULL)
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (223, N'it-IT', N'PigeonCms.ModuleParams', N'LblCssFileDescription', N'Nome del file css da caricare (es. miofile.css). Il file deve essere nella cartella della visualizzazione scelta per il modulo corrente.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (224, N'en-US', N'PigeonCms.ModuleParams', N'LblCssFileDescription', N'Name of css file to upload (ex. myfile.css). The file must be in the folder of the selected view for the current module.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (225, N'it-IT', N'PigeonCms.ModuleParams', N'LblCssClass', N'Classe css', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (226, N'en-US', N'PigeonCms.ModuleParams', N'LblCssClass', N'Css class', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (227, N'it-IT', N'PigeonCms.ModuleParams', N'LblCssClassDescription', N'Classe css personalizzata per il modulo corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (228, N'en-US', N'PigeonCms.ModuleParams', N'LblCssClassDescription', N'Custom css class for current module', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (229, N'it-IT', N'PigeonCms.MenuAdmin', N'LblViewsDescription', N'Visualizzazione del modulo corrente. Se il modulo prevede diversi tipi di visualizzazione sceglierlo dalla lista.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (230, N'en-US', N'PigeonCms.MenuAdmin', N'LblViewsDescription', N'Type of view of the current module. If the form provides different viewing options choose one from the list.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (231, N'it-IT', N'PigeonCms.ModulesAdmin', N'ModuleTitle', N'Gestione moduli', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (232, N'en-US', N'PigeonCms.ModulesAdmin', N'ModuleTitle', N'Modules management', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (233, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblCreated', N'Creazione', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (234, N'en-US', N'PigeonCms.ModulesAdmin', N'LblCreated', N'Created', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (235, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblLastUpdate', N'Ultimo aggiornamento', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (236, N'en-US', N'PigeonCms.ModulesAdmin', N'LblLastUpdate', N'Last update', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (237, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblDetails', N'Dettagli', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (238, N'en-US', N'PigeonCms.ModulesAdmin', N'LblDetails', N'Details', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (239, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblModuleType', N'Tipo modulo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (240, N'en-US', N'PigeonCms.ModulesAdmin', N'LblModuleType', N'Module type', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (241, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblModuleTypeDescription', N'Tipo di contenuto del modulo corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (242, N'en-US', N'PigeonCms.ModulesAdmin', N'LblModuleTypeDescription', N'Content type shown by current module', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (243, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblTitle', N'Titolo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (244, N'en-US', N'PigeonCms.ModulesAdmin', N'LblTitle', N'Title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (245, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblTitleDescription', N'Titolo del modulo. Verr? visualizzato se abilitata l opzione [visualizza titolo]', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (246, N'en-US', N'PigeonCms.ModulesAdmin', N'LblTitleDescription', N'Module title. It will be shown when [show title] is checked', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (247, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblShowTitle', N'Visualizza titolo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (248, N'en-US', N'PigeonCms.ModulesAdmin', N'LblShowTitle', N'Show title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (249, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblShowTitleDescription', N'Visualizza il titolo del modulo nella posizione prevista', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (250, N'en-US', N'PigeonCms.ModulesAdmin', N'LblShowTitleDescription', N'Shows module title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (251, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblPublished', N'Pubblicato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (252, N'en-US', N'PigeonCms.ModulesAdmin', N'LblPublished', N'Published', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (253, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblPublishedDescription', N'Pubblica o meno il modulo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (254, N'en-US', N'PigeonCms.ModulesAdmin', N'LblPublishedDescription', N'Publish or not the current module', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (255, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblPosition', N'Posizione', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (256, N'en-US', N'PigeonCms.ModulesAdmin', N'LblPosition', N'Position', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (257, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblPositionDescription', N'Posizione del modulo nel layout della pagina corrente. Se tale [blocco template] non fosse presente nel layout il modulo non verr? visualizzato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (258, N'en-US', N'PigeonCms.ModulesAdmin', N'LblPositionDescription', N'Module position in the layout of currente page. If current layout does not contains that [template block] module will be not shown.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (259, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblOrder', N'Ordine', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (260, N'en-US', N'PigeonCms.ModulesAdmin', N'LblOrder', N'Order', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (261, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblOrderDescription', N'Ordine di visualizzazione del modulo all interno della corrente posizione', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (262, N'en-US', N'PigeonCms.ModulesAdmin', N'LblOrderDescription', N'Display order inside the current position', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (263, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblContent', N'Contenuto', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (264, N'en-US', N'PigeonCms.ModulesAdmin', N'LblContent', N'Content', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (265, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblContentDescription', N'Attualmente non utilizzato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (266, N'en-US', N'PigeonCms.ModulesAdmin', N'LblContentDescription', N'Actually not used', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (267, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenuEntries', N'Voci menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (268, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenuEntries', N'Menu entries', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (269, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenus', N'Visualizza in', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (270, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenus', N'Show in', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (271, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenusDescription', N'Specifica in quali pagine visualizzare il modulo corrente', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (272, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenusDescription', N'Specify which pages display the current module', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (273, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenuAll', N'Tutte', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (274, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenuAll', N'All', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (275, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenuNone', N'Nessuna', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (276, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenuNone', N'None', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (277, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenuSelection', N'Singole voci di menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (278, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenuSelection', N'Select menu items', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (279, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblMenuEntriesDescription', N'Scegli le singole voci di menu nelle quali verr? visualizzato il modulo, solo se [visualizza in]=singole voci di menu. Tenere premuto CTRL per selezione multipla. Scegliendo il nome del menu (es, [mainmenu]) il module verr? visualizzata in tutte le sue voci.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (280, N'en-US', N'PigeonCms.ModulesAdmin', N'LblMenuEntriesDescription', N'Choose individual menu items in which the module will appear, only if [menus] = [select menu items]. Hold CTRL for multiple selection. Choosing the menu name (eg, [mainmenu]) the module will be displayed in all its entries.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (281, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblViews', N'Visualizzazioni', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (282, N'en-US', N'PigeonCms.ModulesAdmin', N'LblViews', N'Views', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (283, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblViewsDescription', N'Visualizzazione del modulo corrente. Se il modulo prevede diversi tipi di visualizzazione sceglierlo dalla lista.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (284, N'en-US', N'PigeonCms.ModulesAdmin', N'LblViewsDescription', N'Type of view of the current module. If the form provides different viewing options choose one from the list.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (285, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblCreated', N'Creazione', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (286, N'en-US', N'PigeonCms.ItemsAdmin', N'LblCreated', N'Created', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (287, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblLastUpdate', N'Ultimo aggiornamento', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (288, N'en-US', N'PigeonCms.ItemsAdmin', N'LblLastUpdate', N'Last update', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (289, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblFields', N'Campi personalizzati', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (290, N'en-US', N'PigeonCms.ItemsAdmin', N'LblFields', N'Custom fields', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (291, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblParameters', N'Parametri', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (292, N'en-US', N'PigeonCms.ItemsAdmin', N'LblParameters', N'Parameters', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (293, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'ModuleTitle', N'Contenuti statici', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (294, N'en-US', N'PigeonCms.StaticPagesAdmin', N'ModuleTitle', N'Static contents', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (295, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblName', N'Nome', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (296, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblName', N'Name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (297, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblNameDescription', N'Nome mnemonico assegnato al contenuto. Utilizzato solo per riconoscere la voce nella lista.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (298, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblNameDescription', N'Mnemonic name assigned to the content. Used only to recognize the entry in the list.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (299, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblTitle', N'Titolo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (300, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblTitle', N'Title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (301, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblTitleDescription', N'Titolo della pagina. Verr? visualizzato se abilitata l opzione [visualizza titolo]', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (302, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblTitleDescription', N'Page title. It will be shown when [show title] is checked', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (303, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblShowTitle', N'Visualizza titolo', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (304, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblShowTitle', N'Show title', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (305, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblShowTitleDescription', N'Quando abilitato visualizza il titolo della pagina', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (306, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblShowTitleDescription', N'When checked it shows the title of the page', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (307, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblVisible', N'Visibile', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (308, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblVisible', N'Visible', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (309, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblVisibleDescription', N'Visualizza o meno il contenuto, anche se presente in pi? voci di menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (310, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblVisibleDescription', N'Shows or not the static content, also if contained in many menu entries', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (311, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblContent', N'Contenuto', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (312, N'en-US', N'PigeonCms.StaticPagesAdmin', N'LblContent', N'Content', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (313, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblItemTypeDescription', N'Tipo di item selezionato. In base al tipo scelto saranno disponibili campi personalizzati e parametri diversi.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (314, N'en-US', N'PigeonCms.ItemsAdmin', N'LblItemTypeDescription', N'Selected item type. Depending on it will be aavailable differen custom fields and parameters.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (315, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblCategoryDescription', N'Categoria di appartenenza dell item corrente. ', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (316, N'en-US', N'PigeonCms.ItemsAdmin', N'LblCategoryDescription', N'Current of current item.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (317, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblEnabledDescription', N'Abilita o meno l item corrent', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (318, N'en-US', N'PigeonCms.ItemsAdmin', N'LblEnabledDescription', N'Enable or not the current item', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (319, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblTitleDescription', N'Titolo dell elemento. Verr? visualizzato a seconda della visualizzazione scelta nel modulo di visualizzazione [PigeonCms.Item]', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (320, N'en-US', N'PigeonCms.ItemsAdmin', N'LblTitleDescription', N'Title of the item. It will be shown depending on the choosen view in the module [PigeonCms.Item] ', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (321, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblItemDateDescription', N'Data associata all elemento. Viene impostata in automatico la data di creazione.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (322, N'en-US', N'PigeonCms.ItemsAdmin', N'LblItemDateDescription', N'Date associated to the element. By default it is set to current date when created.', N'', 2, 1, NULL)
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (323, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblValidFromDescription', N'Data di inizio validit? dell elemento. Impostata automaticamente alla data corrente in fase di creazione. Se si imposta una data futura l elemento non verr? visualizzato fino a tale data.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (324, N'en-US', N'PigeonCms.ItemsAdmin', N'LblValidFromDescription', N'Effective Date of the element. Automatically set to the current date when element is created. If you set a future date the item will not appear until that date.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (325, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblValidToDescription', N'Data di fine validit? dell elemento. Lasciare vuoto per non associare una scedenza.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (326, N'en-US', N'PigeonCms.ItemsAdmin', N'LblValidToDescription', N'Expiring date of the element. Element never expires when blank.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (327, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblDescriptionDescription', N'Descrizione dell elemento (in pi? lingue quando abilitate).', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (328, N'en-US', N'PigeonCms.ItemsAdmin', N'LblDescriptionDescription', N'Description of the item (in more languages when enabled)', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (329, N'it-IT', N'PigeonCms.EmailContactForm', N'LblName', N'nome', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (330, N'en-US', N'PigeonCms.EmailContactForm', N'LblName', N'name and surname', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (331, N'it-IT', N'PigeonCms.EmailContactForm', N'LblCompanyName', N'azienda', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (332, N'en-US', N'PigeonCms.EmailContactForm', N'LblCompanyName', N'company name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (333, N'it-IT', N'PigeonCms.EmailContactForm', N'LblCity', N'citt?', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (334, N'en-US', N'PigeonCms.EmailContactForm', N'LblCity', N'city', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (335, N'it-IT', N'PigeonCms.EmailContactForm', N'LblInfoEmail', N'e-mail', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (336, N'en-US', N'PigeonCms.EmailContactForm', N'LblInfoEmail', N'e-mail', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (337, N'it-IT', N'PigeonCms.EmailContactForm', N'LblPhone', N'telefono', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (338, N'en-US', N'PigeonCms.EmailContactForm', N'LblPhone', N'phone', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (339, N'it-IT', N'PigeonCms.EmailContactForm', N'LblMessage', N'messaggio', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (340, N'en-US', N'PigeonCms.EmailContactForm', N'LblMessage', N'message', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (341, N'it-IT', N'PigeonCms.EmailContactForm', N'LblPrivacyText', N'Informativa ai sensi dell?art. 13 del d.lgs. n. 196/2003.', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (342, N'en-US', N'PigeonCms.EmailContactForm', N'LblPrivacyText', N'privacy text message', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (343, N'it-IT', N'PigeonCms.MenuAdmin', N'LblChange', N'cambia', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (344, N'en-US', N'PigeonCms.MenuAdmin', N'LblChange', N'change', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (345, N'it-IT', N'PigeonCms.MenuAdmin', N'LblChangeDescription', N'Consente di cambiare il tipo di contenuto della voce corrente. ATTENZIONE: gli eventuali parametri associati al modulo corrente andranno persi', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (346, N'en-US', N'PigeonCms.MenuAdmin', N'LblChangeDescription', N'Change the content type of the current entry. WARNING: Any parameters associated with the current module will be lost', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (347, N'it-IT', N'PigeonCms.ItemsSearch', N'LblSearchText', N'', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (348, N'en-US', N'PigeonCms.ItemsSearch', N'LblSearchText', N'', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (349, N'it-IT', N'PigeonCms.ItemsSearch', N'LblSearchLink', N'cerca', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (350, N'en-US', N'PigeonCms.ItemsSearch', N'LblSearchLink', N'search', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (351, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'ModuleTitle', N'Segnaposto', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (352, N'en-US', N'PigeonCms.PlaceholdersAdmin', N'ModuleTitle', N'Placeholders', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (353, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'LblName', N'Nome', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (354, N'en-US', N'PigeonCms.PlaceholdersAdmin', N'LblName', N'Name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (355, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'LblContent', N'Contenuto', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (356, N'en-US', N'PigeonCms.PlaceholdersAdmin', N'LblContent', N'Content', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (357, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'LblVisible', N'Visibile', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (358, N'en-US', N'PigeonCms.PlaceholdersAdmin', N'LblVisible', N'Visible', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (359, N'en-US', N'PigeonCms.Items', N'LblMoreInfo', N'more info', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (360, N'it-IT', N'PigeonCms.Items', N'LblMoreInfo', N'leggi tutto', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (361, N'en-US', N'PigeonCms.Items', N'LblPrint', N'Print', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (362, N'it-IT', N'PigeonCms.Items', N'LblPrint', N'Stampa', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (365, N'it-IT', N'PigeonCms.Placeholder', N'NameLabel', N'Nome', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (366, N'en-US', N'PigeonCms.Placeholder', N'NameLabel', N'Name', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (367, N'it-IT', N'PigeonCms.EmailContactForm', N'ShowPrivacyCheckLabel', N'Visualizza casella privacy', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (368, N'en-US', N'PigeonCms.EmailContactForm', N'ShowPrivacyCheckLabel', N'Show privacy check', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (369, N'it-IT', N'PigeonCms.EmailContactForm', N'PrivacyTextLabel', N'Informativa privacy', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (370, N'en-US', N'PigeonCms.EmailContactForm', N'PrivacyTextLabel', N'Privacy text', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (371, N'it-IT', N'PigeonCms.TopMenu', N'MenuTypeLabel', N'Tipo menu', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (372, N'en-US', N'PigeonCms.TopMenu', N'MenuTypeLabel', N'Menu type', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (373, N'en-US', N'PigeonCms.EmailContactForm', N'LblCaptchaText', N'enter the code shown', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (374, N'it-IT', N'PigeonCms.EmailContactForm', N'LblCaptchaText', N'inserisci il codice visualizzato', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (375, N'it-IT', N'PigeonCms.FileUpload', N'all', N'tutti', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (376, N'en-US', N'PigeonCms.FileUpload', N'all', N'all', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (377, N'it-IT', N'PigeonCms.FileUpload', N'Allowedfiles', N'Files consentiti', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (378, N'en-US', N'PigeonCms.FileUpload', N'Allowedfiles', N'Allowed files', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (379, N'it-IT', N'PigeonCms.FileUpload', N'MaxSize', N'Dimensione massima', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (380, N'en-US', N'PigeonCms.FileUpload', N'MaxSize', N'Max size', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (381, N'it-IT', N'PigeonCms.FilesManager', N'Select', N'Seleziona', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (382, N'en-US', N'PigeonCms.FilesManager', N'Select', N'Select', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (383, N'it-IT', N'PigeonCms.FilesManager', N'Preview', N'Anteprima', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (384, N'en-US', N'PigeonCms.FilesManager', N'Preview', N'Preview', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (385, N'en-US', N'PigeonCms.ContentEditorControl', N'ReadMore', N'Read more', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (386, N'it-IT', N'PigeonCms.ContentEditorControl', N'ReadMore', N'Leggi tutto', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (387, N'en-US', N'PigeonCms.ContentEditorControl', N'Pagebreak', N'Page break', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (388, N'it-IT', N'PigeonCms.ContentEditorControl', N'Pagebreak', N'Salto pagina', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (389, N'en-US', N'PigeonCms.ContentEditorControl', N'File', N'File', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (390, N'it-IT', N'PigeonCms.ContentEditorControl', N'File', N'File', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (392, N'en-US', N'PigeonCms.FilesUpload', N'Preview', N'Preview', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (393, N'it-IT', N'PigeonCms.FilesUpload', N'Preview', N'Anteprima', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (394, N'en-US', N'PigeonCms.FilesUpload', N'Select', N'Select', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (395, N'it-IT', N'PigeonCms.FilesUpload', N'Select', N'Seleziona', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (402, N'en-US', N'PigeonCms.SectionsAdmin', N'LblTitle', N'Title', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (403, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblTitle', N'Titolo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (404, N'en-US', N'PigeonCms.SectionsAdmin', N'LblDescription', N'Description', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (405, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblDescription', N'Descrizione', N'', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (406, N'en-US', N'PigeonCms.SectionsAdmin', N'LblEnabled', N'Enabled', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (407, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblEnabled', N'Abilitato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (408, N'en-US', N'PigeonCms.SectionsAdmin', N'LblLimits', N'Limits', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (409, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblLimits', N'Limiti', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (410, N'en-US', N'PigeonCms.SectionsAdmin', N'LblMaxItems', N'Max items', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (411, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblMaxItems', N'Numero max items', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (412, N'en-US', N'PigeonCms.SectionsAdmin', N'LblMaxAttachSizeKB', N'Max size for attachments (KB)', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (413, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblMaxAttachSizeKB', N'Dimensione massima allegati (KB)', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (414, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblPasswordControl', N'Ripeti password', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (415, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblOldPassword', N'Vecchia password', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (416, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblEnabled', N'Abilitato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (417, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblComment', N'Note', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (418, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblSex', N'Sesso', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (419, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblCompanyName', N'Nome azienda', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (420, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblVat', N'Partita IVA', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (421, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblSsn', N'Codice fiscale', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (422, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblFirstName', N'Nome', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (423, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblSecondName', N'Cognome', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (424, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblAddress1', N'Indirizzo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (425, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblAddress2', N'Indirizzo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (426, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblCity', N'Citt&agrave;', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (427, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblState', N'Provincia', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (428, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblZipCode', N'Cap', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (429, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblNation', N'Nazione', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (430, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblTel1', N'Telefono', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (431, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblMobile1', N'Cellulare', NULL, 2, 1, NULL)
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (432, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblWebsite1', N'Sito web', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (433, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblPasswordNotMatching', N'le password non corrispondono', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (434, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblInvalidEmail', N'email non valida', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (435, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblInvalidPassword', N'password non valida', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (458, N'en-US', N'PigeonCms.MemberEditorControl', N'LblPasswordControl', N'Repeat password', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (459, N'en-US', N'PigeonCms.MemberEditorControl', N'LblOldPassword', N'Old password', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (460, N'en-US', N'PigeonCms.MemberEditorControl', N'LblEnabled', N'Enabled', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (461, N'en-US', N'PigeonCms.MemberEditorControl', N'LblComment', N'Notes', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (462, N'en-US', N'PigeonCms.MemberEditorControl', N'LblSex', N'Gender', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (463, N'en-US', N'PigeonCms.MemberEditorControl', N'LblCompanyName', N'Company name', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (464, N'en-US', N'PigeonCms.MemberEditorControl', N'LblVat', N'Vat', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (465, N'en-US', N'PigeonCms.MemberEditorControl', N'LblSsn', N'Ssn', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (466, N'en-US', N'PigeonCms.MemberEditorControl', N'LblFirstName', N'First name', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (467, N'en-US', N'PigeonCms.MemberEditorControl', N'LblSecondName', N'Second name', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (468, N'en-US', N'PigeonCms.MemberEditorControl', N'LblAddress1', N'Address', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (469, N'en-US', N'PigeonCms.MemberEditorControl', N'LblAddress2', N'Address', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (470, N'en-US', N'PigeonCms.MemberEditorControl', N'LblCity', N'Citty;', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (471, N'en-US', N'PigeonCms.MemberEditorControl', N'LblState', N'State', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (472, N'en-US', N'PigeonCms.MemberEditorControl', N'LblZipCode', N'Zip', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (473, N'en-US', N'PigeonCms.MemberEditorControl', N'LblNation', N'Nation', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (474, N'en-US', N'PigeonCms.MemberEditorControl', N'LblTel1', N'Telephone', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (475, N'en-US', N'PigeonCms.MemberEditorControl', N'LblMobile1', N'Mobile phone', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (476, N'en-US', N'PigeonCms.MemberEditorControl', N'LblWebsite1', N'Website', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (477, N'en-US', N'PigeonCms.MemberEditorControl', N'LblPasswordNotMatching', N'passwords do not match', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (478, N'en-US', N'PigeonCms.MemberEditorControl', N'LblInvalidEmail', N'invalid email', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (479, N'en-US', N'PigeonCms.MemberEditorControl', N'LblInvalidPassword', N'invalid password', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (480, N'en-US', N'PigeonCms.MemberEditorControl', N'LblManadatoryFields', N'please fill mandatory fields', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (481, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblManadatoryFields', N'compila i campi obbligatori', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (482, N'en-US', N'PigeonCms.MemberEditorControl', N'LblCaptchaText', N'enter the code shows', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (483, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblCaptchaText', N'inserisci il codice visualizzato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (484, N'en-US', N'PigeonCms.MembersAdmin', N'LblCaptchaText', N'enter the code shown', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (485, N'it-IT', N'PigeonCms.MembersAdmin', N'LblCaptchaText', N'inserisci il codice visualizzato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (486, N'en-US', N'DroidCatalogue.Orders', N'LblFillCompanyOrName', N'please fill company name or your full name', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (487, N'it-IT', N'DroidCatalogue.Orders', N'LblFillCompanyOrName', N'inserisci il nome azienda o il tuo nome completo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (488, N'en-US', N'DroidCatalogue.Orders', N'LblFillAddress', N'please fill address field', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (489, N'it-IT', N'DroidCatalogue.Orders', N'LblFillAddress', N'inserisci il campo indirizzo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (490, N'en-US', N'DroidCatalogue.Orders', N'LblFillCity', N'please fill city field', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (491, N'it-IT', N'DroidCatalogue.Orders', N'LblFillCity', N'inserisci il campo citt?', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (492, N'en-US', N'DroidCatalogue.Orders', N'LblFillState', N'please fill state field', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (493, N'it-IT', N'DroidCatalogue.Orders', N'LblFillState', N'inserisci il campo provincia', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (494, N'en-US', N'DroidCatalogue.Orders', N'LblFillZip', N'please fill zip field', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (495, N'it-IT', N'DroidCatalogue.Orders', N'LblFillZip', N'inserisci il campo Cap', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (496, N'en-US', N'DroidCatalogue.Orders', N'LblFillVatOrSsn', N'please fill Vat or Ssn field', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (497, N'it-IT', N'DroidCatalogue.Orders', N'LblFillVatOrSsn', N'inserisci Partita IVA o codice fiscale', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (498, N'en-US', N'DroidCatalogue.Orders', N'LblItems', N'items', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (499, N'it-IT', N'DroidCatalogue.Orders', N'LblItems', N'articoli', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (500, N'en-US', N'DroidCatalogue.Orders', N'LblDiskSpace', N'disk space', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (501, N'it-IT', N'DroidCatalogue.Orders', N'LblDiskSpace', N'spazio su disco', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (502, N'en-US', N'DroidCatalogue.Orders', N'LblClients', N'clients', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (503, N'it-IT', N'DroidCatalogue.Orders', N'LblClients', N'attivazioni', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (504, N'en-US', N'DroidCatalogue.Orders', N'LblSetup', N'setup', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (505, N'it-IT', N'DroidCatalogue.Orders', N'LblSetup', N'setup', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (506, N'en-US', N'DroidCatalogue.Orders', N'LblFreeOnline', N'free online', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (507, N'it-IT', N'DroidCatalogue.Orders', N'LblFreeOnline', N'gratis online', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (508, N'en-US', N'DroidCatalogue.Orders', N'LblAds', N'ads', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (509, N'it-IT', N'DroidCatalogue.Orders', N'LblAds', N'pubblicit&agrave;', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (510, N'en-US', N'DroidCatalogue.Orders', N'LblYes', N'yes', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (511, N'it-IT', N'DroidCatalogue.Orders', N'LblYes', N's&igrave;', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (512, N'en-US', N'DroidCatalogue.Orders', N'LblNoAds', N'no ads', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (513, N'it-IT', N'DroidCatalogue.Orders', N'LblNoAds', N'nessuna pubblicit&agrave', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (514, N'en-US', N'DroidCatalogue.Orders', N'LblMonthlyFee', N'monthly fee', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (515, N'it-IT', N'DroidCatalogue.Orders', N'LblMonthlyFee', N'canone mensile', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (516, N'en-US', N'DroidCatalogue.Orders', N'LblFree', N'free', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (517, N'it-IT', N'DroidCatalogue.Orders', N'LblFree', N'gratis', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (518, N'en-US', N'DroidCatalogue.Orders', N'LblUnlimited', N'unlimited', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (519, N'it-IT', N'DroidCatalogue.Orders', N'LblUnlimited', N'illimitate', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (520, N'en-US', N'DroidCatalogue.Orders', N'LblChoosePlan', N'Choose plan', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (521, N'it-IT', N'DroidCatalogue.Orders', N'LblChoosePlan', N'Scelta piano', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (522, N'en-US', N'DroidCatalogue.Orders', N'LblCheckout', N'Checkout', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (523, N'it-IT', N'DroidCatalogue.Orders', N'LblCheckout', N'Conferma', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (524, N'en-US', N'DroidCatalogue.Orders', N'LblFinish', N'Finish', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (525, N'it-IT', N'DroidCatalogue.Orders', N'LblFinish', N'Fine', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (526, N'en-US', N'DroidCatalogue.Orders', N'LblOrderSummary', N'Order summary', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (527, N'it-IT', N'DroidCatalogue.Orders', N'LblOrderSummary', N'Riepilogo ordine', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (528, N'en-US', N'DroidCatalogue.Orders', N'LblBillingSummary', N'User and billing summary', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (529, N'it-IT', N'DroidCatalogue.Orders', N'LblBillingSummary', N'Riepilogo dati utente e pagamento', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (530, N'en-US', N'PigeonCms.ItemsAdmin', N'ChooseSection', N'Choose a section before', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (531, N'it-IT', N'PigeonCms.ItemsAdmin', N'ChooseSection', N'Scegli una sezione', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (532, N'en-US', N'PigeonCms.ItemsAdmin', N'ChooseCategory', N'Choose a category before', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (533, N'it-IT', N'PigeonCms.ItemsAdmin', N'ChooseCategory', N'Scegli una categoria', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (534, N'it-IT', N'PigeonCms.ItemsAdmin', N'NewTicket', N'Nuovo ticket', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (535, N'it-IT', N'PigeonCms.ItemsAdmin', N'PriorityFilter', N'--priorit?--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (536, N'it-IT', N'PigeonCms.ItemsAdmin', N'Close', N'chiudi', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (537, N'it-IT', N'PigeonCms.ItemsAdmin', N'Reopen', N'riapri', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (538, N'it-IT', N'PigeonCms.ItemsAdmin', N'Lock', N'blocca', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (539, N'it-IT', N'PigeonCms.ItemsAdmin', N'Actions', N'--azioni--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (540, N'it-IT', N'PigeonCms.ItemsAdmin', N'StatusFilter', N'--stato--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (541, N'it-IT', N'PigeonCms.ItemsAdmin', N'CategoryFilter', N'--categoria--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (542, N'it-IT', N'PigeonCms.ItemsAdmin', N'Always', N'tutto', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (543, N'it-IT', N'PigeonCms.ItemsAdmin', N'Today', N'oggi', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (544, N'it-IT', N'PigeonCms.ItemsAdmin', N'Last week', N'ultima settimana', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (545, N'it-IT', N'PigeonCms.ItemsAdmin', N'Last month', N'ultimo mese', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (546, N'it-IT', N'PigeonCms.ItemsAdmin', N'Subject', N'Oggetto', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (547, N'it-IT', N'PigeonCms.ItemsAdmin', N'Reply', N'Rispondi', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (548, N'it-IT', N'PigeonCms.ItemsAdmin', N'BackToList', N'Torna alla lista', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (549, N'it-IT', N'PigeonCms.ItemsAdmin', N'MyTickets', N'solo mie', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (550, N'it-IT', N'PigeonCms.ItemsAdmin', N'AssignTo', N'--assegna--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (551, N'it-IT', N'PigeonCms.ItemsAdmin', N'Status', N'Stato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (552, N'it-IT', N'PigeonCms.ItemsAdmin', N'AssignedTo', N'Assegnato a', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (553, N'it-IT', N'PigeonCms.ItemsAdmin', N'Low', N'Bassa', NULL, 2, 1, NULL)
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (554, N'it-IT', N'PigeonCms.ItemsAdmin', N'Medium', N'Media', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (555, N'it-IT', N'PigeonCms.ItemsAdmin', N'High', N'Alta', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (556, N'it-IT', N'PigeonCms.ItemsAdmin', N'Open', N'Aperto', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (557, N'it-IT', N'PigeonCms.ItemsAdmin', N'WorkInProgress', N'In lavorazione', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (558, N'it-IT', N'PigeonCms.ItemsAdmin', N'Closed', N'Chiuso', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (559, N'it-IT', N'PigeonCms.ItemsAdmin', N'Locked', N'Bloccato', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (560, N'it-IT', N'PigeonCms.ItemsAdmin', N'Working', N'in lavorazione', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (561, N'it-IT', N'PigeonCms.ItemsAdmin', N'<subject>', N'<oggetto>', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (562, N'it-IT', N'PigeonCms.ItemsAdmin', N'Priority', N'Priorit?', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (563, N'it-IT', N'PigeonCms.ItemsAdmin', N'Operator', N'Operatore', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (564, N'it-IT', N'PigeonCms.ItemsAdmin', N'DateInserted', N'Inserito il', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (565, N'it-IT', N'PigeonCms.ItemsAdmin', N'LastActivity', N'Ultima attivit?', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (566, N'it-IT', N'PigeonCms.ItemsAdmin', N'Text', N'Testo', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (567, N'it-IT', N'PigeonCms.ItemsAdmin', N'MessageTicketTitle', N'Ticket "[[ItemTitle]]" ([[ItemId]]) [[Extra]]', N'allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (568, N'it-IT', N'PigeonCms.ItemsAdmin', N'MessageTicketDescription', N'[[ItemDescription]]', N'allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (569, N'it-IT', N'PigeonCms.ItemsAdmin', N'CustomerFilter', N'--cliente--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (570, N'it-IT', N'PigeonCms.ItemsAdmin', N'Customer', N'Cliente', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (571, N'it-IT', N'PigeonCms.ItemsAdmin', N'Attachment', N'Allegati', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (572, N'it-IT', N'PigeonCms.ItemsAdmin', N'AttachFiles', N'Allega files', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (573, N'it-IT', N'PigeonCms.ItemsAdmin', N'SendEmail', N'Invia email', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (574, N'it-IT', N'PigeonCms.ItemsAdmin', N'OperatorFilter', N'--operatore--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (575, N'it-IT', N'PigeonCms.ItemsAdmin', N'UserInsertedFilter', N'--inserito da--', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (576, N'it-IT', N'PigeonCms.ItemsAdmin', N'SaveAndClose', N'Salva e chiudi ticket', NULL, 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (577, N'it-IT', N'PigeonCms.LoginForm', N'', N'user', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (578, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (579, N'it-IT', N'PigeonCms.MenuAdmin', N'Main', N'Main', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (580, N'it-IT', N'PigeonCms.MenuAdmin', N'Options', N'Options', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (581, N'it-IT', N'PigeonCms.MenuAdmin', N'Security', N'Security', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (582, N'it-IT', N'PigeonCms.MenuAdmin', N'Parameters', N'Parameters', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (583, N'it-IT', N'PigeonCms.MenuAdmin', N'LblAlias', N'Alias', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (584, N'it-IT', N'PigeonCms.MenuAdmin', N'LblRoute', N'Route', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (585, N'it-IT', N'PigeonCms.MenuAdmin', N'LblUseSsl', N'Use SSL', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (586, N'it-IT', N'PigeonCms.MenuAdmin', N'LblLink', N'Link', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (587, N'it-IT', N'PigeonCms.MenuAdmin', N'LblMasterpage', N'Masterpage', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (588, N'it-IT', N'PigeonCms.PermissionsControl', N'LblRead', N'Read', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (589, N'it-IT', N'PigeonCms.PermissionsControl', N'LblWrite', N'Write', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (590, N'it-IT', N'PigeonCms.PermissionsControl', N'LblPermissionId', N'ID', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (591, N'it-IT', N'PigeonCms.ModuleParams', N'LblSystemMessagesTo', N'System messages to', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (592, N'it-IT', N'PigeonCms.ModuleParams', N'LblDirectEditMode', N'Direct edit mode', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (593, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblUsername', N'Username', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (594, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblEmail', N'E-mail', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (595, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblPassword', N'Password', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (596, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblAllowMessages', N'Allow messages', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (597, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblAllowEmails', N'Allow emails', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (598, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblAccessCode', N'Access code', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (599, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblAccessLevel', N'Access level', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (600, N'it-IT', N'PigeonCms.MembersAdmin', N'LblFilters', N'Filters', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (601, N'it-IT', N'PigeonCms.ModulesAdmin', N'Main', N'Main', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (602, N'it-IT', N'PigeonCms.ModulesAdmin', N'Menu', N'Menu', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (603, N'it-IT', N'PigeonCms.ModulesAdmin', N'Options', N'Options', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (604, N'it-IT', N'PigeonCms.ModulesAdmin', N'Security', N'Security', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (605, N'it-IT', N'PigeonCms.ModulesAdmin', N'Parameters', N'Parameters', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (606, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblChange', N'change', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (607, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblRecordId', N'ID', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (608, N'it-IT', N'PigeonCms.MembersAdmin', N'LblUpdateUser', N'Update user', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (609, N'it-IT', N'PigeonCms.FileUpload', N'Folder', N'Folder', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (610, N'it-IT', N'PigeonCms.UpdatesAdmin', N'LblInstall', N'Install', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (611, N'it-IT', N'PigeonCms.UpdatesAdmin', N'LblModules', N'Modules', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (612, N'it-IT', N'PigeonCms.UpdatesAdmin', N'LblTemplates', N'Templates', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (613, N'it-IT', N'PigeonCms.UpdatesAdmin', N'LblSql', N'Sql', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (614, N'it-IT', N'PigeonCms.LogsAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (615, N'it-IT', N'PigeonCms.LogsAdmin', N'LblRecordId', N'ID', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (616, N'it-IT', N'PigeonCms.LogsAdmin', N'LblCreated', N'Created', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (617, N'it-IT', N'PigeonCms.LogsAdmin', N'LblType', N'Type', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (618, N'it-IT', N'PigeonCms.LogsAdmin', N'LblModuleType', N'Module', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (619, N'it-IT', N'PigeonCms.LogsAdmin', N'LblView', N'View', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (620, N'it-IT', N'PigeonCms.LogsAdmin', N'LblIp', N'Ip', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (621, N'it-IT', N'PigeonCms.LogsAdmin', N'LblSessionId', N'Session ID', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (622, N'it-IT', N'PigeonCms.LogsAdmin', N'LblUser', N'User', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (623, N'it-IT', N'PigeonCms.LogsAdmin', N'LblUrl', N'Url', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (624, N'it-IT', N'PigeonCms.LogsAdmin', N'LblDescription', N'Description', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (625, N'it-IT', N'PigeonCms.OfflineAdmin', N'LblPageTemplate', N'Page template', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (626, N'it-IT', N'PigeonCms.AttributesAdmin', N'Name', N'Name', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (627, N'it-IT', N'PigeonCms.AttributesAdmin', N'FieldType', N'Field type', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (628, N'it-IT', N'PigeonCms.AttributesAdmin', N'MinValue', N'Min value', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (629, N'it-IT', N'PigeonCms.AttributesAdmin', N'MaxValue', N'Max value', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (630, N'it-IT', N'PigeonCms.AttributesAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (631, N'it-IT', N'PigeonCms.AttributesAdmin', N'AttributeValues', N'Attribute values', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (632, N'it-IT', N'PigeonCms.AttributesAdmin', N'Value', N'Value', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (633, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblRecordInfo', N'Record info', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (634, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblRecordId', N'ID', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (635, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblCreated', N'Created', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (636, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblLastUpdate', N'Last update', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (637, N'it-IT', N'PigeonCms.LoginForm', N'user', N'user', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (638, N'it-IT', N'PigeonCms.LoginForm', N'password', N'password', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (639, N'it-IT', N'PigeonCms.LoginForm', N'Username', N'Username', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (640, N'it-IT', N'PigeonCms.MembersAdmin', N'LblChangePassword', N'Change password', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (641, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (642, N'it-IT', N'PigeonCms.SectionsAdmin', N'Main', N'Main', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (643, N'it-IT', N'PigeonCms.SectionsAdmin', N'Security', N'Security', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (644, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblCssClass', N'Css class', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (645, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (646, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblSection', N'Section', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (647, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblCssClass', N'Css class', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (648, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblEnabled', N'Enabled', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (649, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblTitle', N'Title', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (650, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblDescription', N'Description', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (651, N'it-IT', N'PigeonCms.StaticPagesAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (652, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (653, N'it-IT', N'PigeonCms.ItemsAdmin', N'Main', N'Main', N'SYSTEM', 2, 1, NULL)
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (654, N'it-IT', N'PigeonCms.ItemsAdmin', N'Security', N'Security', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (655, N'it-IT', N'PigeonCms.ItemsAdmin', N'Parameters', N'Parameters', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (656, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblAlias', N'Alias', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (657, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblCssClass', N'Css class', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (658, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblRecordId', N'ID', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (659, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblOrderId', N'Order Id', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (660, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (661, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblResourceSet', N'Resource set', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (662, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblResourceId', N'Resource id', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (663, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblTextMode', N'Text mode', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (664, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblValue', N'Value', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (665, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblComment', N'Comment', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (666, N'it-IT', N'PigeonCms.FilesManager', N'Size', N'Size', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (667, N'it-IT', N'PigeonCms.FilesManager', N'Meta', N'Meta data', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (668, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (669, N'it-IT', N'PigeonCms.UpdatesAdmin', N'LblFilters', N'filters', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (670, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (671, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'LblName', N'Name', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (672, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'LblTitle', N'Title', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (673, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'LblValue', N'Value', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (674, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'LblInfo', N'Additional info', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (675, N'it-IT', N'PigeonCms.LoginForm', N'LblInvalidLogin', N'Invalid username or password.', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (676, N'it-IT', N'PigeonCms.MembersAdmin', N'LblNewUser', N'New user', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (677, N'it-IT', N'PigeonCms.MembersAdmin', N'LblChangeRoles', N'Change roles', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (678, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblRoles', N'Roles', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (679, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblRolesInUser', N'User roles', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (680, N'it-IT', N'PigeonCms.MembersAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (681, N'it-IT', N'PigeonCms.RolesAdmin', N'LblNewRole', N'New role', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (682, N'it-IT', N'PigeonCms.RolesAdmin', N'LblUsersInRole', N'Users in role', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (683, N'it-IT', N'PigeonCms.RolesAdmin', N'InsRoleName', N'Insert role name', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (684, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'Name', N'Name', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (685, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'Title', N'Title', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (686, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (687, N'it-IT', N'PigeonCms.CulturesAdmin', N'CultureCode', N'Culture code', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (688, N'it-IT', N'PigeonCms.CulturesAdmin', N'DisaplyName', N'Display name', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (689, N'it-IT', N'PigeonCms.CulturesAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (690, N'it-IT', N'PigeonCms.WebConfigAdmin', N'Key', N'Key', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (691, N'it-IT', N'PigeonCms.WebConfigAdmin', N'Value', N'Value', N'SYSTEM', 2, 1, NULL)
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (692, N'it-IT', N'AQ_default', N'Sample5', N'Label value calling method in code behind', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (693, N'it-IT', N'AQ_menu', N'Home', N'Labels', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (694, N'it-IT', N'AQ_menu', N'Images', N'Images', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (695, N'it-IT', N'AQ_menu', N'Cache', N'Cache', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (696, N'it-IT', N'AQ_menu', N'List', N'List items', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (697, N'it-IT', N'AQ_menu', N'PrivateArea', N'Private area', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (698, N'it-IT', N'AQ_default', N'Title', N'Labels resources', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (699, N'it-IT', N'AQ_default', N'Sample1', N'Label value calling method in html', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (700, N'it-IT', N'AQ_default', N'Sample2', N'
Label using server control with TextMode="Text"
', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (701, N'it-IT', N'AQ_default', N'Sample3', N'
Label using server control with TextMode="BasicHtml" <em>(simple editor)</em>
', N'SYSTEM', 1, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (702, N'it-IT', N'AQ_default', N'Sample4', N'
Label using server control with TextMode="Html" <em>(advanced editor)</em><br />
This is a <a href="#">link</a>
', N'SYSTEM', 0, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (703, N'it-IT', N'PigeonCms.ItemsAdmin', N'Related', N'Related', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (704, N'it-IT', N'PigeonCms.ItemsAdmin', N'Attributes', N'Attributes', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (705, N'it-IT', N'PigeonCms.ItemsAdmin', N'Variants', N'Variants', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (706, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblCustomVlaue', N'Valore Custom', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (707, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblMeasureUnit', N'Unit? di misura', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (708, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (709, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblTxtCode', N'Codice Coupon', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (710, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblEnabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (711, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblAmount', N'Importo', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (712, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblIsPercentage', N'Valore in Percentuale', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (713, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblMinOrderAmount', N'Importo Minimo', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (714, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblValidFrom', N'Valid from', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (715, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblValidTo', N'Valid to', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (716, N'it-IT', N'PigeonCms.CouponsAdmin', N'LblMaxUses', N'Numero d''usi', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (717, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (718, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblAllowCustomValue', N'Use Custom Values', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (719, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblItemType', N'Select ItemType', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (720, N'it-IT', N'PigeonCms.AttributesAdmin', N'LblName', N'Name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (721, N'it-IT', N'AQ_cache', N'Title', N'Cache example', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (722, N'it-IT', N'AQ_list', N'Title', N'Items list example', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (723, N'it-IT', N'AQ_images', N'Title', N'Images resources', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (724, N'it-IT', N'AQ_images', N'ImageRoadRunner', N'/assets/img/roadrunner.gif', N'SYSTEM', 3, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (725, N'it-IT', N'AQ_images', N'ImageCoyote', N'/assets/img/coyote.jpg', N'SYSTEM', 3, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (726, N'it-IT', N'AQ_images', N'ImageTnt', N'/assets/img/tnt.png', N'SYSTEM', 3, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (727, N'it-IT', N'AQ_private', N'Title', N'Private area', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (728, N'it-IT', N'AQ_login', N'Title', N'Login', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (729, N'it-IT', N'PigeonCms.Debug', N'debug-test', N'debug-test-value', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (730, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblResourceParams', N'Resource params', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (731, N'it-IT', N'PigeonCms.LoginForm', N'RememberMe', N'Remember me', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (732, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'NewSettingsAdded', N'new settings added', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (733, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'SettingsRefreshed', N'Settings refreshed', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (734, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Section', N'Section', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (735, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblExtId', N'External Id', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (736, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblItemType', N'ItemType', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (737, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblChange', N'change', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (738, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblExtId', N'External Id', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (739, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblParentItem', N'Parent category', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (740, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblSection', N'Section', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (741, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblExtId', N'External Id', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (742, N'it-IT', N'PigeonCms.LabelsAdmin', N'AllForExport', N'All for export', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (743, N'it-IT', N'PigeonCms.LabelsAdmin', N'FilterAllLabels', N'All labels', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (744, N'it-IT', N'PigeonCms.LabelsAdmin', N'FilterMissingValues', N'Only with missing values', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (745, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblResource', N'Resource', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (746, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblMissingValues', N'Missing values', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (747, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblValuesStartsWith', N'Only values that starts with', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (748, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblValuesContains', N'Only values that contains', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (749, N'it-IT', N'SITE_default', N'TopReasons', N'Top ten reasons to choose the new CMS', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (750, N'it-IT', N'SITE_default', N'KeyFeatures', N'... just with few rude key features', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (751, N'it-IT', N'SITE_default', N'DiscoverPillars', N'Discover the pillars', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (752, N'it-IT', N'Page1', N'Image1', N'/assets/images/elements-img1.jpg', N'SYSTEM', 3, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (753, N'it-IT', N'PigeonCms.MenuAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (754, N'it-IT', N'PigeonCms.MenuAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (755, N'it-IT', N'PigeonCms.MenuAdmin', N'NameCol', N'Name/Alias/Route', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (756, N'it-IT', N'PigeonCms.MenuAdmin', N'CssCol', N'Css/Theme/Master', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (757, N'it-IT', N'PigeonCms.MenuAdmin', N'ModuleCol', N'Module', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (758, N'it-IT', N'PigeonCms.MenuAdmin', N'Vis-Pub-Ssl', N'Vis/Pub/Ssl', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (759, N'it-IT', N'PigeonCms.MenuAdmin', N'Permissions', N'Permissions', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (760, N'it-IT', N'PigeonCms.MenuAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (761, N'it-IT', N'PigeonCms.MenuAdmin', N'visible', N'visible', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (762, N'it-IT', N'PigeonCms.MenuAdmin', N'published', N'published', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (763, N'it-IT', N'PigeonCms.MenuAdmin', N'copy', N'copy', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (764, N'it-IT', N'PigeonCms.MenuAdmin', N'move', N'move', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (765, N'it-IT', N'PigeonCms.MenuAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (766, N'it-IT', N'PigeonCms.MenuAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (767, N'it-IT', N'PigeonCms.MenuAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (768, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'Key', N'Key', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (769, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'Value', N'Value', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (770, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (771, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (772, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (773, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (774, N'it-IT', N'PigeonCms.AppSettingsAdmin', N'LblKeySet', N'KeySet', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (775, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (776, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (777, N'it-IT', N'PigeonCms.RoutesAdmin', N'Name-Patter', N'Name / Pattern', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (778, N'it-IT', N'PigeonCms.RoutesAdmin', N'Theme-MasterPage', N'Theme/Masterpage', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (779, N'it-IT', N'PigeonCms.RoutesAdmin', N'PageHandler', N'Page Handler', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (780, N'it-IT', N'PigeonCms.RoutesAdmin', N'Published', N'Published', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (781, N'it-IT', N'PigeonCms.RoutesAdmin', N'UseSsl', N'UseSsl', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (782, N'it-IT', N'PigeonCms.RoutesAdmin', N'Core', N'Core', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (783, N'it-IT', N'PigeonCms.RoutesAdmin', N'ID', N'ID', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (784, N'it-IT', N'PigeonCms.RoutesAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (785, N'it-IT', N'PigeonCms.RoutesAdmin', N'move', N'move', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (786, N'it-IT', N'PigeonCms.RoutesAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (787, N'it-IT', N'PigeonCms.RoutesAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (788, N'it-IT', N'PigeonCms.RoutesAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (789, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblName', N'Name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (790, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblPattern', N'Pattern', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (791, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblTheme', N'Theme', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (792, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblMasterpage', N'Masterpage', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (793, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblAssemblyPath', N'Assembly path', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (794, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblHandlerName', N'Handler name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (795, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblEnabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (796, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblUseSsl', N'Use SSL', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (797, N'it-IT', N'PigeonCms.RoutesAdmin', N'LblIsCore', N'Core', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (798, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (799, N'it-IT', N'PigeonCms.LabelsAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (800, N'it-IT', N'PigeonCms.LabelsAdmin', N'ResourceSetId', N'Resource Set / Id', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (801, N'it-IT', N'PigeonCms.LabelsAdmin', N'TextMode', N'Text mode', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (802, N'it-IT', N'PigeonCms.LabelsAdmin', N'Values', N'Values', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (803, N'it-IT', N'PigeonCms.MenuAdmin', N'Seo', N'Seo', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (804, N'it-IT', N'PigeonCms.SeoControl', N'MetaNoIndex', N'META NoIndex', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (805, N'it-IT', N'PigeonCms.SeoControl', N'LblNoIndex', N'NoIndex', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (806, N'it-IT', N'PigeonCms.SeoControl', N'MetaNoFollow', N'META NoFollow', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (807, N'it-IT', N'PigeonCms.SeoControl', N'LblNoFollow', N'NoFollow', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (808, N'it-IT', N'PigeonCms.SeoControl', N'LblTitle', N'Title', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (809, N'it-IT', N'PigeonCms.SeoControl', N'LblDescription', N'Description', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (810, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'MenuType', N'MenuType', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (811, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'Title', N'Title', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (812, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (813, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (814, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (815, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (816, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'Name', N'Name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (817, N'it-IT', N'PigeonCms.MenuTypesAdmin', N'Description', N'Description', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (818, N'it-IT', N'PigeonCms.ModulesAdmin', N'PagesAll', N'All pages', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (819, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (820, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (821, N'it-IT', N'PigeonCms.ModulesAdmin', N'Title', N'Title', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (822, N'it-IT', N'PigeonCms.ModulesAdmin', N'BlockMenu', N'Block/Menu', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (823, N'it-IT', N'PigeonCms.ModulesAdmin', N'ModuleCol', N'Module', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (824, N'it-IT', N'PigeonCms.ModulesAdmin', N'Published', N'Published', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (825, N'it-IT', N'PigeonCms.ModulesAdmin', N'Permissions', N'Permissions', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (826, N'it-IT', N'PigeonCms.ModulesAdmin', N'content', N'content', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (827, N'it-IT', N'PigeonCms.ModulesAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (828, N'it-IT', N'PigeonCms.ModulesAdmin', N'copy', N'copy', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (829, N'it-IT', N'PigeonCms.ModulesAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (830, N'it-IT', N'PigeonCms.ModulesAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (831, N'it-IT', N'PigeonCms.ModulesAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (832, N'it-IT', N'PigeonCms.ModulesAdmin', N'LblName', N'Module Name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (833, N'it-IT', N'PigeonCms.LabelsAdmin', N'SelectReourceSet', N'Select resource', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (834, N'it-IT', N'PigeonCms.LabelsAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (835, N'it-IT', N'PigeonCms.LabelsAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (836, N'it-IT', N'PigeonCms.LabelsAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (837, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (838, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (839, N'it-IT', N'PigeonCms.SectionsAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (840, N'it-IT', N'PigeonCms.SectionsAdmin', N'Title', N'Title', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (841, N'it-IT', N'PigeonCms.SectionsAdmin', N'Images', N'Images', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (842, N'it-IT', N'PigeonCms.SectionsAdmin', N'Files', N'Files', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (843, N'it-IT', N'PigeonCms.SectionsAdmin', N'Permissions', N'Permissions', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (844, N'it-IT', N'PigeonCms.SectionsAdmin', N'Quota', N'Quota', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (845, N'it-IT', N'PigeonCms.SectionsAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (846, N'it-IT', N'PigeonCms.SectionsAdmin', N'documents', N'documents', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (847, N'it-IT', N'PigeonCms.SectionsAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (848, N'it-IT', N'PigeonCms.SectionsAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (849, N'it-IT', N'PigeonCms.SectionsAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (850, N'it-IT', N'PigeonCms.SectionsAdmin', N'LblEnabledLabel', N'Enable section', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (851, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (852, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (853, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Title', N'Title', N'SYSTEM', 2, 1, N'')
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (854, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Info', N'Info', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (855, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (856, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Images', N'Images', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (857, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Files', N'Files', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (858, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Permissions', N'Permissions', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (859, N'it-IT', N'PigeonCms.CategoriesAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (860, N'it-IT', N'PigeonCms.CategoriesAdmin', N'documents', N'documents', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (861, N'it-IT', N'PigeonCms.CategoriesAdmin', N'move', N'move', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (862, N'it-IT', N'PigeonCms.CategoriesAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (863, N'it-IT', N'PigeonCms.CategoriesAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (864, N'it-IT', N'PigeonCms.CategoriesAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (865, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Main', N'Main', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (866, N'it-IT', N'PigeonCms.CategoriesAdmin', N'Security', N'Security', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (867, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblEnabledLabel', N'Abilita elemento', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (868, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblRecordId', N'ID', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (869, N'it-IT', N'PigeonCms.CategoriesAdmin', N'LblOrderId', N'Order Id', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (870, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (871, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (872, N'it-IT', N'PigeonCms.ItemsAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (873, N'it-IT', N'PigeonCms.ItemsAdmin', N'Title', N'Title', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (874, N'it-IT', N'PigeonCms.ItemsAdmin', N'Category', N'Category', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (875, N'it-IT', N'PigeonCms.ItemsAdmin', N'Info', N'Info', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (876, N'it-IT', N'PigeonCms.ItemsAdmin', N'Images', N'Images', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (877, N'it-IT', N'PigeonCms.ItemsAdmin', N'Files', N'Files', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (878, N'it-IT', N'PigeonCms.ItemsAdmin', N'Permissions', N'Permissions', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (879, N'it-IT', N'PigeonCms.ItemsAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (880, N'it-IT', N'PigeonCms.ItemsAdmin', N'documents', N'documents', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (881, N'it-IT', N'PigeonCms.ItemsAdmin', N'move', N'move', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (882, N'it-IT', N'PigeonCms.ItemsAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (883, N'it-IT', N'PigeonCms.ItemsAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (884, N'it-IT', N'PigeonCms.ItemsAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (885, N'it-IT', N'PigeonCms.ItemsAdmin', N'Seo', N'Seo', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (886, N'it-IT', N'PigeonCms.ItemsAdmin', N'LblEnabledLabel', N'Abilita elemento', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (887, N'it-IT', N'PigeonCms.WebConfigAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (888, N'it-IT', N'PigeonCms.WebConfigAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (889, N'it-IT', N'PigeonCms.WebConfigAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (890, N'it-IT', N'PigeonCms.MemberEditorControl', N'LblApproved', N'Approved', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (891, N'it-IT', N'PigeonCms.MembersAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (892, N'it-IT', N'PigeonCms.MembersAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (893, N'it-IT', N'PigeonCms.MembersAdmin', N'Username', N'Username', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (894, N'it-IT', N'PigeonCms.MembersAdmin', N'Email', N'Email', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (895, N'it-IT', N'PigeonCms.MembersAdmin', N'Roles', N'Roles', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (896, N'it-IT', N'PigeonCms.MembersAdmin', N'ChangePwd', N'Change password', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (897, N'it-IT', N'PigeonCms.MembersAdmin', N'ChangeRoles', N'Change roles', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (898, N'it-IT', N'PigeonCms.MembersAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (899, N'it-IT', N'PigeonCms.MembersAdmin', N'Approved', N'Approved', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (900, N'it-IT', N'PigeonCms.MembersAdmin', N'IsCore', N'Core', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (901, N'it-IT', N'PigeonCms.MembersAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (902, N'it-IT', N'PigeonCms.MembersAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (903, N'it-IT', N'PigeonCms.MembersAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (904, N'it-IT', N'PigeonCms.RolesAdmin', N'Role', N'Role', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (905, N'it-IT', N'PigeonCms.RolesAdmin', N'Users', N'Users', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (906, N'it-IT', N'PigeonCms.RolesAdmin', N'UsersCount', N'# Users', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (907, N'it-IT', N'PigeonCms.RolesAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (908, N'it-IT', N'PigeonCms.RolesAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (909, N'it-IT', N'PigeonCms.RolesAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (910, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (911, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (912, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (913, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (914, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (915, N'it-IT', N'PigeonCms.CulturesAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (916, N'it-IT', N'PigeonCms.CulturesAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (917, N'it-IT', N'PigeonCms.CulturesAdmin', N'ShortCode', N'Short Code', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (918, N'it-IT', N'PigeonCms.CulturesAdmin', N'DisplayName', N'Display Name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (919, N'it-IT', N'PigeonCms.CulturesAdmin', N'edit', N'edit', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (920, N'it-IT', N'PigeonCms.CulturesAdmin', N'move', N'move', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (921, N'it-IT', N'PigeonCms.CulturesAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (922, N'it-IT', N'PigeonCms.CulturesAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (923, N'it-IT', N'PigeonCms.CulturesAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (924, N'it-IT', N'PigeonCms.LogsAdmin', N'LblClose', N'Close', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (925, N'it-IT', N'PigeonCms.LogsAdmin', N'LblOpen', N'Open', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (926, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'Name', N'Name', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (927, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'ContentPreview', N'Content preview', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (928, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'Enabled', N'Enabled', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (929, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (930, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (931, N'it-IT', N'PigeonCms.PlaceholdersAdmin', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (932, N'it-IT', N'PigeonCms.FilesManagerModern', N'MaxSize', N'Max file size', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (933, N'it-IT', N'PigeonCms.FilesManagerModern', N'Folder', N'Folder', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (934, N'it-IT', N'PigeonCms.FilesManagerModern', N'Allowedfiles', N'Allowed files', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (935, N'it-IT', N'PigeonCms.FilesManagerModern', N'click or drag', N'clicca qui o trascina', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (936, N'it-IT', N'PigeonCms.FilesManagerModern', N'delete', N'delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (937, N'it-IT', N'PigeonCms.FilesManagerModern', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (938, N'it-IT', N'PigeonCms.FilesManagerModern', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (939, N'it-IT', N'PigeonCms.TemplateBlocksAdmin', N'LblDetails', N'Details', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (940, N'it-IT', N'PigeonCms.MenuAdmin', N'content', N'content', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (941, N'it-IT', N'PigeonCms.MemberEditorControl', N'LitNickname', N'Nickname', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (942, N'it-IT', N'PigeonCms.MembersAdmin', N'UsernameEmail', N'Username/email', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (943, N'it-IT', N'PigeonCms.MembersAdmin', N'MetaInfo', N'Meta info', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (944, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.Acme.TntItem', N'Acme.TntItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (945, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.HelloWorldItem', N'PigeonCms.HelloWorldItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (946, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.Item', N'PigeonCms.Item', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (947, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.NewsItem', N'PigeonCms.NewsItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (948, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.RedirectItem', N'PigeonCms.RedirectItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (949, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.RoomItem', N'PigeonCms.RoomItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (950, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.Shop.ProductItem', N'PigeonCms.Shop.ProductItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (951, N'it-IT', N'PigeonCms.ItemsAdmin', N'ItemTypeName.PigeonCms.TicketItem', N'PigeonCms.TicketItem', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (952, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_tab-FirstTab', N'FirstTab', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (953, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-TextSimple', N'Simple text', N'SYSTEM', 2, 1, N'')
GO
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (954, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-TextSimpleLocalized', N'Simple text localized', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (955, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-TextHtml', N'Html text', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (956, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-TextHtmlLocalized', N'Html text localized', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (957, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-SelectColor', N'Select color', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (958, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-Image1', N'Sample image', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (959, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'UploadFile_FileTooBig', N'File exceed size limits', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (960, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'UploadFile_FileNotAllowed', N'File type is not allowed', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (961, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'UploadFile_MaxFileSize', N'Dim. max file', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (962, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'UploadFile_FileTypes', N'File type', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (963, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-File1', N'Sample file', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (964, N'it-IT', N'PigeonCms.FileUploadModernControl', N'UploadFile_FileTooBig', N'File exceed size limits', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (965, N'it-IT', N'PigeonCms.FileUploadModernControl', N'UploadFile_FileNotAllowed', N'File type is not allowed', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (966, N'it-IT', N'PigeonCms.FileUploadModernControl', N'UploadFile_MaxFileSize', N'Dim. max file', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (967, N'it-IT', N'PigeonCms.FileUploadModernControl', N'UploadFile_FileTypes', N'File type', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (968, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_tab-SecondTab', N'SecondTab', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (969, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-Flag1', N'Flag 1', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (970, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp2-OtherSimpleText', N'Other simple text', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (971, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_tab-Composer', N'Composer', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (972, N'it-IT', N'PigeonCms.ItemsAdmin', N'AutoLayout.PigeonCms.HelloWorldItem_CustomProp1-Blocks', N'Page composer', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (973, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'delete_image_title', N'Confirm delete', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (974, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'delete_image', N'Delete image?', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (975, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'cancel', N'cancel', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (976, N'it-IT', N'PigeonCms.ImageUploadModernControl', N'confirm', N'confirm', N'SYSTEM', 2, 1, N'')
INSERT [dbo].[#__labels] ([id], [cultureName], [resourceSet], [resourceId], [value], [comment], [textMode], [isLocalized], [resourceParams]) VALUES (977, N'it-IT', N'PigeonCms.LoginForm', N'Oauth_Someone_Exception', N'Someone provider error.', N'SYSTEM', 2, 1, N'')
SET IDENTITY_INSERT [dbo].[#__labels] OFF

SET IDENTITY_INSERT [dbo].[#__memberUsers] ON 

INSERT [dbo].[#__memberUsers] ([id], [username], [applicationName], [email], [comment], [password], [passwordQuestion], [passwordAnswer], [isApproved], [lastActivityDate], [lastLoginDate], [lastPasswordChangedDate], [creationDate], [isOnLine], [isLockedOut], [lastLockedOutDate], [failedPasswordAttemptCount], [failedPasswordAttemptWindowStart], [failedPasswordAnswerAttemptCount], [failedPasswordAnswerAttemptWindowStart], [enabled], [accessCode], [accessLevel], [isCore], [sex], [companyName], [vat], [ssn], [firstName], [secondName], [address1], [address2], [city], [state], [zipCode], [nation], [tel1], [mobile1], [website1], [allowMessages], [allowEmails], [validationCode], [nickName]) VALUES (1, N'admin', N'PigeonCms', N'', N'', N'admin123', N'', N'', 1, NULL, CAST(N'2017-06-21T12:53:11.217' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, 0, NULL, 1, N'', 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, N'', N'admin')
INSERT [dbo].[#__memberUsers] ([id], [username], [applicationName], [email], [comment], [password], [passwordQuestion], [passwordAnswer], [isApproved], [lastActivityDate], [lastLoginDate], [lastPasswordChangedDate], [creationDate], [isOnLine], [isLockedOut], [lastLockedOutDate], [failedPasswordAttemptCount], [failedPasswordAttemptWindowStart], [failedPasswordAnswerAttemptCount], [failedPasswordAnswerAttemptWindowStart], [enabled], [accessCode], [accessLevel], [isCore], [sex], [companyName], [vat], [ssn], [firstName], [secondName], [address1], [address2], [city], [state], [zipCode], [nation], [tel1], [mobile1], [website1], [allowMessages], [allowEmails], [validationCode], [nickName]) VALUES (2, N'manager', N'PigeonCms', N'', N'', N'manager', N'', N'', 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, 0, NULL, 1, N'', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, N'', N'manager')
SET IDENTITY_INSERT [dbo].[#__memberUsers] OFF
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (2, N'mainmenu', N'Default', N'default', N'', 0, 1, 0, 4, 10, 0, 0, 0, N'PigeonModernBlank', N'', N'fa fa-key fa-fw', 0, 1, 0, N'', 0, 1, 0, 0, N'', 0, 2, 1)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (31, N'adminmenu', N'Site', N'', N'#', 1, 1, 0, 0, 1, 1, 0, 0, N'', N'', N'fa fa-wrench fa-fw', 1, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 13)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (32, N'adminmenu', N'sections admin', N'sections', N'', 0, 1, 33, 54, 4, 1, 1, 0, N'', N'', N'', 1, 6, 20, N'', 0, 1, 0, 0, N'', 0, 2, 14)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (33, N'adminmenu', N'Contents', N'', N'#', 1, 1, 0, 0, 2, 1, 1, 0, N'', N'', N'fa fa-edit fa-fw', 1, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 11)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (34, N'adminmenu', N'menu admin', N'menu', N'', 0, 1, 36, 55, 13, 1, 1, 0, N'', N'', N'', 1, 6, 27, N'', 0, 1, 0, 0, N'', 0, 2, 6)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (35, N'adminmenu', N'Preview', N'', N'~/', 1, 1, 31, 0, 20, 1, 0, 0, N'', N'', N'', 1, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 14)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (36, N'adminmenu', N'Menu', N'', N'#', 1, 1, 0, 0, 10, 1, 1, 0, N'', N'', N'fa fa-sitemap fa-fw', 1, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 3)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (37, N'adminmenu', N'modules admin', N'modules', N'', 0, 1, 36, 56, 11, 1, 1, 0, N'', N'', N'', 1, 6, 25, N'', 0, 1, 0, 0, N'', 0, 2, 4)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (38, N'adminmenu', N'menutypes admin', N'menutypes', N'', 0, 1, 36, 57, 12, 1, 1, 0, N'', N'', N'', 1, 6, 26, N'', 0, 1, 0, 0, N'', 0, 2, 5)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (39, N'adminmenu', N'categories admin', N'categories', N'', 0, 1, 33, 58, 5, 1, 1, 0, N'', N'', N'', 1, 6, 21, N'', 0, 1, 0, 0, N'', 0, 2, 13)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (40, N'adminmenu', N'items admin', N'items', N'', 0, 1, 33, 59, 7, 1, 1, 0, N'', N'', N'', 1, 6, 22, N'', 0, 1, 0, 0, N'', 0, 2, 10)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (42, N'adminmenu', N'placeholders admin', N'placeholders', N'', 0, 1, 33, 61, 8, 1, 1, 0, N'', N'', N'', 1, 6, 24, N'', 0, 1, 0, 0, N'', 0, 2, 39)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (43, N'adminmenu', N'logout', N'', N'~/Default.aspx?act=logout', 1, 0, 31, 0, 18, 1, 1, 0, N'', N'', N'', 0, 6, 0, N'', 0, 0, 0, 0, N'', 0, 2, 7)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (44, N'adminmenu', N'members admin', N'members', N'', 0, 1, 31, 62, 35, 1, 1, 0, N'', N'', N'', 1, 6, 12, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (45, N'adminmenu', N'roles admin', N'roles', N'', 0, 1, 31, 63, 36, 1, 1, 0, N'', N'', N'', 1, 6, 11, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (46, N'adminmenu', N'templateblocks admin', N'templateblocks', N'', 0, 1, 31, 64, 37, 1, 1, 0, N'', N'', N'', 1, 6, 13, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (47, N'adminmenu', N'routes admin', N'routes', N'', 0, 1, 31, 65, 38, 1, 1, 0, N'', N'', N'', 1, 6, 14, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (48, N'adminmenu', N'cultures admin', N'cultures', N'', 0, 1, 31, 66, 39, 1, 1, 0, N'', N'', N'', 1, 6, 15, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (49, N'adminmenu', N'appsettings admin', N'appsettings', N'', 0, 1, 31, 67, 40, 1, 1, 0, N'', N'', N'', 1, 6, 16, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (50, N'adminmenu', N'webconfig admin', N'webconfig', N'', 0, 1, 31, 68, 41, 1, 1, 0, N'', N'', N'', 1, 6, 17, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (51, N'adminmenu', N'updates admin', N'updates', N'', 0, 0, 31, 69, 42, 1, 1, 0, N'', N'', N'', 0, 6, 18, N'', 0, 1, 0, 0, N'', 0, 2, 5)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (57, N'adminmenu', N'labels admin', N'labels', N'', 0, 1, 33, 74, 9, 1, 1, 0, N'', N'', N'', 1, 6, 19, N'', 0, 1, 0, 0, N'', 0, 2, 40)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (58, N'adminmenu', N'logs', N'logs', N'', 0, 1, 31, 75, 43, 1, 1, 0, N'', N'', N'', 1, 6, 29, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (59, N'adminmenu', N'offline', N'offline', N'', 0, 0, 31, 76, 48, 1, 1, 0, N'', N'', N'', 0, 6, 30, N'', 0, 0, 0, 0, N'', 0, 2, 6)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (60, N'mainmenu', N'Offline admin', N'offlineadmin', N'', 3, 1, 0, 0, 55, 0, 1, 2, N'', N'', N'', 0, 1, 0, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (65, N'adminPopups', N'labels admin popup', N'labels-admin-popup', N'', 0, 1, 0, 83, 57, 1, 1, 0, N'PigeonModernBlank', N'', N'', 0, 6, 41, N'', 0, 1, 0, 0, N'', 0, 2, 8)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (67, N'adminPopups', N'PigeonCms-ItemsAdmin-popup', N'pigeoncms-itemsadmin-popup', N'', 0, 1, 0, 85, 59, 1, 1, 0, N'PigeonModernBlank', N'', N'', 0, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (68, N'adminPopups', N'PigeonCms-PlaceholdersAdmin-popup', N'pigeoncms-placeholdersadmin-popup', N'', 0, 1, 0, 86, 60, 1, 1, 0, N'PigeonModernBlank', N'', N'', 0, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, NULL)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (69, N'adminmenu', N'ReadMe', N'', N'https://github.com/picce/pigeoncms/blob/master/README.md', 1, 1, 31, 0, 19, 1, 1, 0, N'', N'', N'', 1, 1, 0, N'', 0, 1, 0, 0, N'', 0, 2, 41)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (72, N'adminmenu', N'attributes admin', N'attributes-admin', N'', 0, 0, 33, 90, 3, 1, 0, 0, N'', N'', N'', 0, 6, 34, N'', 0, 1, 0, 0, N'', 0, 2, 2)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (73, N'adminmenu', N'Shop', N'', N'#', 1, 0, 0, 0, 14, 1, 1, 0, N'', N'', N'fa fa-shopping-cart fa-fw', 0, 6, 35, N'', 0, 0, 0, 0, N'', 0, 2, 7)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (74, N'adminmenu', N'Products', N'products', N'', 0, 1, 73, 91, 15, 1, 0, 0, N'', N'', N'', 1, 6, 36, N'', 0, 0, 0, 0, N'', 0, 2, 8)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (75, N'adminmenu', N'Products attributes', N'products-attributes', N'', 0, 1, 73, 92, 16, 1, 0, 0, N'', N'', N'', 1, 6, 37, N'', 0, 0, 0, 0, N'', 0, 2, 9)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (76, N'adminmenu', N'Coupons', N'coupons', N'', 0, 1, 73, 93, 17, 1, 0, 0, N'', N'', N'', 1, 1, 38, N'', 0, 0, 0, 0, N'', 0, 2, 10)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (77, N'managerMenu', N'Website labels', N'website-labels', N'', 0, 1, 0, 95, 2, 0, 1, 0, N'', N'', N'fa fa-edit fa-fw', 1, 8, 0, N'', 0, 0, 0, 0, N'', 0, 2, 2)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (78, N'managerMenu', N'Admin area', N'', N'~/admin/menu.aspx', 1, 1, 0, 0, 3, 1, 0, 0, N'', N'', N'fa fa-key', 1, 1, 40, N'', 0, 0, 0, 0, N'', 0, 2, 3)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (79, N'usermenu', N'Logout', N'', N'/pages/default.aspx?act=logout', 1, 1, 0, 0, 71, 1, 1, 0, N'', N'', N'', 1, 1, 0, N'', 0, 1, 0, 0, N'', 0, 2, 4)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (80, N'adminPopups', N'items images upload', N'items-images-upload', N'', 0, 1, 0, 97, 72, 1, 0, 0, N'PigeonModernBlank', N'', N'', 0, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 9)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (81, N'adminPopups', N'items files upload', N'items-files-upload', N'', 0, 1, 0, 98, 73, 1, 1, 0, N'PigeonModernBlank', N'', N'', 0, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 11)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (82, N'adminPopups', N'Documents upload', N'documents-upload', N'', 0, 1, 0, 99, 74, 1, 1, 0, N'PigeonModernBlank', N'', N'', 0, 6, 0, N'', 0, 1, 0, 0, N'', 0, 2, 12)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (83, N'frontmenu', N'home', N'', N'/', 1, 1, 0, 0, 75, 0, 1, 0, N'', N'', N'', 1, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 15)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (84, N'frontmenu', N'elements', N'', N'/contents/elements', 1, 1, 0, 0, 76, 0, 1, 0, N'', N'', N'', 1, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 16)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (85, N'frontmenu', N'news', N'-copy', N'/contents/news', 1, 1, 0, 0, 77, 0, 1, 0, N'', N'', N'', 1, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 17)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (86, N'frontmenu', N'reserved area', N'-copy-copy', N'/contents/reserved-area', 1, 1, 0, 0, 78, 0, 1, 0, N'', N'', N'', 1, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 19)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (87, N'frontmenu', N'manager dashboard', N'', N'/manager/website-labels.aspx', 1, 1, 0, 0, 79, 1, 1, 0, N'', N'', N'', 1, 1, 42, N'', 0, 0, 0, 0, N'', 0, 2, 20)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (88, N'mainmenu', N'Error page', N'error', N'', 0, 1, 0, 101, 80, 0, 0, 0, N'', N'', N'', 0, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 33)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (89, N'mainmenu', N'pageNotFound', N'pagenotfound', N'', 0, 1, 0, 102, 81, 0, 1, 0, N'', N'', N'', 0, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 34)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (90, N'managerMenu', N'Manager dashboard', N'dashboard', N'', 0, 1, 0, 103, 1, 0, 0, 0, N'', N'', N'fa fa-dashboard', 1, 8, 0, N'', 0, 0, 0, 0, N'', 0, 2, 37)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (91, N'adminmenu', N'items composer', N'items-composer', N'', 0, 1, 33, 104, 6, 1, 1, 0, N'', N'', N'', 1, 6, 44, N'', 0, 0, 0, 0, N'', 0, 2, 1)
INSERT [dbo].[#__menu] ([id], [menuType], [name], [alias], [link], [contentType], [published], [parentId], [moduleId], [ordering], [accessType], [overridePageTitle], [referMenuId], [currMasterPage], [currTheme], [cssClass], [visible], [routeId], [permissionId], [accessCode], [accessLevel], [isCore], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [useSsl], [seoId]) VALUES (92, N'mainmenu', N'login-someone', N'login-someone', N'', 0, 1, 0, 105, 82, 0, 0, 0, N'PigeonModernBlank', N'', N'fa fa-key fa-fw', 0, 1, 0, N'', 0, 0, 0, 0, N'', 0, 2, 15)
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 2, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 31, N'Site', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 32, N'Sections', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 33, N'Contents', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 34, N'Menu entries', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 35, N'Preview', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 36, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 37, N'Modules', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 38, N'Menu types', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 39, N'Categories', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 40, N'Items', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 42, N'Placeholders', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 43, N'Logout', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 44, N'Members', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 45, N'Roles', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 46, N'Template blocks ', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 47, N'Routing', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 48, N'Cultures', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 49, N'Settings', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 50, N'Web.config', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 51, N'Updates', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 57, N'Labels Pigeon', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 58, N'Logs', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 59, N'Site offline', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 60, N'Offline admin access', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 65, N'Labels', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 67, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 68, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 69, N'ReadMe', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 72, N'Form attributes', N'attributes admin')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 73, N'Shop', N'Shop')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 74, N'Products', N'Products')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 75, N'Products attributes', N'Products attributes')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 76, N'Coupons', N'Coupons')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 77, N'Website labels', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 78, N'Admin area', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 79, N'Esci', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 80, N'Items images upload', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 81, N'Items files upload', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 82, N'Documents upload', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 83, N'home', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 84, N'elements', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 85, N'news', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 86, N'reserved area', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 87, N'manager dashboard', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 88, N'Error page', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 89, N'Page not fount', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 90, N'Manager dashboard', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 91, N'items composer', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'en-US', 92, N'Login with someone', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 2, N'Default', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 31, N'Sito', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 32, N'Sezioni', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 33, N'Contenuti', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 34, N'Voci menu', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 35, N'Anteprima', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 36, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 37, N'Moduli', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 38, N'Tipi menu', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 39, N'Categorie', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 40, N'Elementi', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 42, N'Segnaposto', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 43, N'Disconnetti', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 44, N'Utenti', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 45, N'Ruoli utente', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 46, N'Blocchi template', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 47, N'Routing', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 48, N'Lingue', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 49, N'Impostazioni', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 50, N'Web.config', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 51, N'Aggiornamenti', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 57, N'Etichette Pigeon', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 58, N'Logs', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 59, N'Sito offline', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 60, N'Accesso offline admin', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 65, N'Gestione etichette', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 67, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 68, N'', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 69, N'ReadMe', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 72, N'Attributi form', N'attributes admin')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 73, N'Shop', N'Shop')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 74, N'Prodotti', N'Prodotti')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 75, N'Attributi prodotti', N'Attributi prodotti')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 76, N'Coupons', N'Coupons')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 77, N'Etichette sito', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 78, N'Admin area', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 79, N'Logout', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 80, N'Upload immagini elementi', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 81, N'Upload files elementi', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 82, N'Upload allegati', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 83, N'home', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 84, N'elementi', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 85, N'news', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 86, N'reserved area', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 87, N'manager dashboard', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 88, N'Error page', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 89, N'Page not fount', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 90, N'Manager dashboard', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 91, N'items composer', N'')
INSERT [dbo].[#__menu_Culture] ([cultureName], [menuId], [title], [titleWindow]) VALUES (N'it-IT', 92, N'Login with someone', N'')
GO
SET IDENTITY_INSERT [dbo].[#__menuTypes] ON 

INSERT [dbo].[#__menuTypes] ([id], [menuType], [title], [description]) VALUES (1, N'mainmenu', N'Main menu (usually front site menu)', N'usually front site menu)')
INSERT [dbo].[#__menuTypes] ([id], [menuType], [title], [description]) VALUES (2, N'adminmenu', N'Admin menu (default backend admin menu)', N'default backend admin menu')
INSERT [dbo].[#__menuTypes] ([id], [menuType], [title], [description]) VALUES (3, N'adminPopups', N'adminPopups (popup or fancy menu entries)', N'popup or fancy menu entries (example: FilesManager instances)')
INSERT [dbo].[#__menuTypes] ([id], [menuType], [title], [description]) VALUES (4, N'managerMenu', N'managerMenu (default menu for manager dashboard)', N'')
INSERT [dbo].[#__menuTypes] ([id], [menuType], [title], [description]) VALUES (5, N'usermenu', N'usermenu (user context menu)', N'user context menu (example: logout). Used in masterpage')
INSERT [dbo].[#__menuTypes] ([id], [menuType], [title], [description]) VALUES (6, N'frontmenu', N'frontmenu', N'menu for frontend website')
SET IDENTITY_INSERT [dbo].[#__menuTypes] OFF
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (1, N'Debug', N'', 1, N'Debug', 1, N'Debug', N'PigeonCms', NULL, N'nicola', NULL, N'admin', 1, 0, N'HideText:=1', 1, 0, N'Debug.ascx', 31, N'', 0, N'', N'', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (2, N'Company address', N'', 0, N'Top', 1, N'Placeholder', N'PigeonCms', NULL, N'nicola', NULL, N'admin', 0, 0, N'Name:=CompanyAddress', 0, 2, N'Placeholder.ascx', 0, N'', 0, N'', N'', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (4, N'Default', N'', 0, N'content', 1, N'LoginForm', N'PigeonCms', NULL, N'admin', CAST(N'2016-07-12T10:29:51.330' AS DateTime), N'admin', 0, 0, N'RedirectUrl:=~/manager/dashboard.aspx|', 0, 3, N'LoginPigeonModernAdmin.ascx', 0, N'', 0, N'', N'', 2, 1, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (7, N'Manager menu', N'', 1, N'Toolbar', 1, N'TopMenu', N'PigeonCms', NULL, N'nicola', CAST(N'2016-01-12T18:35:37.613' AS DateTime), N'admin', 0, 0, N'MenuType:=managerMenu|MenuId:=listMenuRoot|ItemSelectedClass:=selected|ItemLastClass:=last|MenuLevel:=0|ShowChild:=1|', 1, 2, N'PigeonModernAdminMenu.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (9, N'Google Analytics', N'', 0, N'Footer', 0, N'Placeholder', N'PigeonCms', NULL, N'nicola', NULL, N'admin', 0, 0, N'Name:=Analytics', 0, 2, N'Placeholder.ascx', 0, N'', 0, N'', N'', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (10, N'Site credits', N'', 0, N'Footer', 1, N'Placeholder', N'PigeonCms', NULL, N'nicola', NULL, N'admin', 0, 0, N'Name:=credits', 0, 0, N'Placeholder.ascx', 0, N'', 0, N'', N'', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (53, N'Admin menu', N'', 1, N'Toolbar', 1, N'TopMenu', N'PigeonCms', NULL, N'nicola', NULL, N'admin', 1, 0, N'MenuType:=adminmenu|MenuId:=listMenuRoot|ItemSelectedClass:=selected|ItemLastClass:=last|MenuLevel:=0|ShowChild:=1|', 1, 2, N'SbAdminMenu.ascx', 10, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (54, N'sections admin', N'', 0, N'content', 1, N'SectionsAdmin', N'PigeonCms', NULL, N'admin', CAST(N'2016-07-12T10:33:20.867' AS DateTime), N'admin', 0, 1, N'TargetImagesUpload:=0|TargetFilesUpload:=0|ListPageSize:=20|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (55, N'Menu admin', N'', 0, N'content', 1, N'MenuAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'AllowEditContentUrl:=1|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (56, N'Modules', N'', 0, N'content', 1, N'ModulesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (57, N'menutypes', N'', 0, N'content', 1, N'MenuTypesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (58, N'categories admin', N'', 0, N'content', 1, N'CategoriesAdmin', N'PigeonCms', NULL, N'admin', CAST(N'2016-07-12T10:33:33.683' AS DateTime), N'admin', 0, 1, N'TargetImagesUpload:=0|TargetFilesUpload:=0|SectionId:=0|ListPageSize:=20|ShowSecurity:=1|ShowOnlyDefaultCulture:=0|ShowItemsCount:=1|AllowOrdering:=1|AllowEdit:=1|AllowDelete:=1|AllowNew:=1|AllowSelection:=1|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (59, N'items admin', N'', 0, N'content', 1, N'ItemsAdmin', N'PigeonCms', NULL, N'admin', CAST(N'2016-07-12T10:33:43.607' AS DateTime), N'admin', 0, 1, N'TargetImagesUpload:=80|TargetFilesUpload:=81|TargetDocsUpload:=82|SectionId:=0|CategoryId:=0|ItemType:=|ListPageSize:=20|ShowSecurity:=1|ShowSeo:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|ShowItemsCount:=1|AllowOrdering:=1|AllowDelete:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (61, N'placeholders admin', N'', 0, N'content', 1, N'PlaceholdersAdmin', N'PigeonCms', NULL, N'admin', CAST(N'2016-07-12T10:33:52.913' AS DateTime), N'admin', 0, 1, N'ListPageSize:=20|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (62, N'members admin', N'', 0, N'content', 1, N'MembersAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'MemberEditorMode:=0|LoginAfterCreate:=0|NeedApprovation:=0|NewRoleAsUser:=0|NewUserSuffix:=|DefaultRoles:=|DefaultAccessCode:=|DefaultAccessLevel:=|RedirectUrl:=|SendEmailNotificationToUser:=0|SendEmailNotificationToAdmin:=0|AdminNotificationEmail:=admin@yourdomain.com|NotificationEmailPageName:=|ShowFieldSex:=0|ShowFieldCompanyName:=0|ShowFieldVat:=0|ShowFieldSsn:=0|ShowFieldFirstName:=0|ShowFieldSecondName:=0|ShowFieldAddress1:=0|ShowFieldAddress2:=0|ShowFieldCity:=0|ShowFieldState:=0|ShowFieldZipCode:=0|ShowFieldNation:=0|ShowFieldTel1:=0|ShowFieldMobile1:=0|ShowFieldWebsite1:=0|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (63, N'roles admin', N'', 0, N'content', 1, N'RolesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (64, N'templateblocks admin', N'', 0, N'content', 1, N'TemplateBlocksAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (65, N'routes admin', N'', 0, N'content', 1, N'RoutesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (66, N'cultures admin', N'', 0, N'content', 1, N'CulturesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (67, N'appsettings admin', N'', 0, N'content', 1, N'AppSettingsAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (68, N'webconfig admin', N'', 0, N'content', 1, N'WebConfigAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (69, N'updates admin', N'', 0, N'content', 1, N'UpdatesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'TargetLabelsPopup:=65', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (70, N'Immobilia Zone admin', N'', 0, N'content', 1, N'ZoneAdmin', N'Immobilia', NULL, N'nicola', NULL, N'nicola', 0, 0, N'', 0, 3, N'Default.ascx', 0, N'', 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (71, N'Immobilia categorie admin', N'', 0, N'content', 1, N'CategorieAdmin', N'Immobilia', NULL, N'nicola', NULL, N'nicola', 0, 0, N'', 0, 3, N'Default.ascx', 0, N'', 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (72, N'Immobilia immobili admin', N'', 0, N'content', 1, N'ImmobiliAdmin', N'Immobilia', NULL, N'nicola', NULL, N'nicola', 0, 0, N'', 0, 3, N'Default.ascx', 0, N'', 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (74, N'labels admin', N'', 0, N'content', 1, N'LabelsAdmin', N'PigeonCms', NULL, N'admin', CAST(N'2016-07-12T10:34:01.583' AS DateTime), N'admin', 0, 1, N'ModuleFullName:=|ModuleFullNamePart:=|ListPageSize:=50|TargetImagesUpload:=0|DefaultResourceFolder:=~/public/res|AllowNew:=0|AllowDel:=0|AllowTextModeEdit:=0|AllowParamsEdit:=0|AllowAdminMode:=1|ShowOnlyEnabledCultures:=0|AllowImportExport:=0|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (75, N'logs', N'', 0, N'content', 1, N'LogsAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (76, N'offline', N'', 0, N'content', 1, N'OfflineAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (82, N'top submenu', N'', 0, N'Toolbar2', 1, N'TopMenu', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 0, N'MenuType:=|MenuId:=none|ItemSelectedClass:=selected|ItemLastClass:=last|ShowPagePostFix:=1|MenuLevel:=1|ShowChild:=0', 0, 2, N'TopMenu.ascx', 0, N'', 0, N'', N'submenulist', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (83, N'labels admin popup', N'', 0, N'content', 1, N'LabelsAdmin', N'PigeonCms', NULL, N'admin', CAST(N'2016-06-12T10:21:57.623' AS DateTime), N'admin', 0, 1, N'ModuleFullName:=|ModuleFullNamePart:=|ListPageSize:=50|TargetImagesUpload:=0|DefaultResourceFolder:=~/public/res|AllowNew:=0|AllowDel:=0|AllowTextModeEdit:=0|AllowParamsEdit:=0|AllowAdminMode:=1|ShowOnlyEnabledCultures:=0|AllowImportExport:=0|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (85, N'items admin popup', N'', 0, N'content', 1, N'ItemsAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'TargetImagesUpload:=61|TargetFilesUpload:=62|TargetDocsUpload:=0|SectionId:=0|CategoryId:=0|MandatorySectionFilter:=0|ItemType:=|ShowSecurity:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|AllowOrdering:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 1, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (86, N'placeholders admin popup', N'', 0, N'content', 1, N'PlaceholdersAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 1, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 1, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (88, NULL, N'', 2, N'Pathway', 1, N'Breadcrumbs', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 0, N'MenuType:=mainmenu', 0, 2, N'Breadcrumbs.ascx', 0, N'', 0, N'', N'', 2, 2, 0, NULL, NULL, NULL, 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (90, NULL, N'', 4, N'content', 1, N'AttributesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 0, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (91, NULL, N'', 5, N'content', 1, N'ItemsAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'TargetImagesUpload:=0|TargetFilesUpload:=0|TargetDocsUpload:=0|SectionId:=0|CategoryId:=0|MandatorySectionFilter:=0|ItemType:=|ShowSecurity:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|AllowOrdering:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|', 0, 3, N'ShopProduct.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (92, NULL, N'', 6, N'content', 1, N'AttributesAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 0, 3, N'default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (93, NULL, N'', 7, N'content', 1, N'CouponsAdmin', N'PigeonCms', NULL, N'admin', NULL, N'admin', 0, 1, N'|', 0, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (94, N'', N'', 8, N'Toolbar', 0, N'TopMenu', N'PigeonCms', CAST(N'2016-01-12T18:11:00.523' AS DateTime), N'admin', CAST(N'2016-01-12T18:11:00.523' AS DateTime), N'admin', 0, 0, N'MenuType:=managerMenu|ListClass:=menulist|ItemSelectedClass:=selected|ItemLastClass:=last', 0, 0, N'default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (95, N'Website labels', N'', 9, N'content', 1, N'LabelsAdmin', N'PigeonCms', CAST(N'2016-01-12T18:12:40.070' AS DateTime), N'admin', CAST(N'2016-01-12T18:26:50.563' AS DateTime), N'admin', 0, 0, N'ModuleFullName:=|ModuleFullNamePart:=SITE_|ListPageSize:=50|TargetImagesUpload:=0|DefaultResourceFolder:=~/public/res|AllowNew:=0|AllowDel:=0|AllowTextModeEdit:=0|AllowParamsEdit:=0|AllowAdminMode:=1|ShowOnlyEnabledCultures:=0|AllowImportExport:=0|', 0, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (96, N'', N'', 10, N'Toolbar', 0, N'TopMenu', N'PigeonCms', CAST(N'2016-01-12T18:21:43.523' AS DateTime), N'admin', CAST(N'2016-01-12T18:21:43.523' AS DateTime), N'admin', 0, 0, N'MenuType:=usermenu|ListClass:=menulist|ItemSelectedClass:=selected|ItemLastClass:=last', 0, 0, N'default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (97, N'items images upload', N'', 11, N'content', 1, N'FilesManagerModern', N'PigeonCms', CAST(N'2016-06-12T11:09:14.980' AS DateTime), N'admin', CAST(N'2016-06-12T11:09:14.980' AS DateTime), N'admin', 0, 1, N'AllowFilesUpload:=1|AllowFilesSelection:=0|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=0|AllowFoldersNavigation:=0|FileExtensions:=jpg;jpeg;gif;png|FileSize:=1024|FileNameType:=0|FilePrefix:=file_|ForcedFilename:=|FileFolderType:=items-images|FilePath:=|NumOfFilesAllowed:=0|ShowWorkingPath:=1|CustomWidth:=|CustomHeight:=|', 0, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (98, N'items files upload', N'', 12, N'content', 1, N'FilesManagerModern', N'PigeonCms', CAST(N'2016-06-12T11:30:50.690' AS DateTime), N'admin', CAST(N'2016-06-12T11:30:50.690' AS DateTime), N'admin', 0, 0, N'AllowFilesUpload:=1|AllowFilesSelection:=0|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=0|AllowFoldersNavigation:=0|FileExtensions:=pdf|FileSize:=512|FileNameType:=0|FilePrefix:=file_|ForcedFilename:=|FileFolderType:=items-files|FilePath:=|NumOfFilesAllowed:=0|ShowWorkingPath:=1|CustomWidth:=|CustomHeight:=|', 0, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (99, N'Documents upload', N'', 13, N'content', 1, N'FilesManagerModern', N'PigeonCms', CAST(N'2016-06-12T11:35:39.737' AS DateTime), N'admin', CAST(N'2016-06-12T11:38:40.997' AS DateTime), N'admin', 0, 0, N'AllowFilesUpload:=1|AllowFilesSelection:=1|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=1|AllowFoldersNavigation:=1|FileExtensions:=jpg;jpeg;gif;png;pdf;|FileSize:=1024|FileNameType:=0|FilePrefix:=file_|ForcedFilename:=|FileFolderType:=documents|FilePath:=|NumOfFilesAllowed:=0|ShowWorkingPath:=1|CustomWidth:=|CustomHeight:=|', 0, 3, N'Default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (100, N'', N'', 14, N'Toolbar', 0, N'TopMenu', N'PigeonCms', CAST(N'2016-06-12T11:56:40.733' AS DateTime), N'admin', CAST(N'2016-06-12T11:56:40.733' AS DateTime), N'admin', 0, 0, N'MenuType:=frontmenu|ListClass:=menulist|ItemSelectedClass:=selected|ItemLastClass:=last', 0, 0, N'default.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (101, N'Error page', N'', 15, N'content', 1, N'Item', N'PigeonCms', CAST(N'2016-07-12T10:03:15.373' AS DateTime), N'admin', CAST(N'2016-07-12T10:09:07.153' AS DateTime), N'admin', 0, 1, N'ItemId:=8|StaticFilesTracking:=2|ShowFiles:=1|ShowImages:=1|PreviewSize:=s|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|', 0, 3, N'Item.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (102, N'pageNotFound', N'', 16, N'content', 1, N'Item', N'PigeonCms', CAST(N'2016-07-12T10:11:27.240' AS DateTime), N'admin', CAST(N'2016-07-12T10:14:29.717' AS DateTime), N'admin', 0, 0, N'ItemId:=10|StaticFilesTracking:=2|ShowFiles:=1|ShowImages:=1|PreviewSize:=s|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|', 0, 3, N'Item.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (103, N'Manager dashboard', N'', 17, N'content', 1, N'Item', N'PigeonCms', CAST(N'2016-07-12T10:17:51.493' AS DateTime), N'admin', CAST(N'2016-07-12T10:28:44.400' AS DateTime), N'admin', 0, 1, N'ItemId:=9|StaticFilesTracking:=2|ShowFiles:=1|ShowImages:=1|PreviewSize:=s|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|', 0, 3, N'ManagerDashboard.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (104, N'items composer', N'', 18, N'content', 1, N'ItemsAdmin', N'PigeonCms', CAST(N'2017-06-21T12:56:27.780' AS DateTime), N'admin', CAST(N'2017-06-21T12:58:26.477' AS DateTime), N'admin', 0, 1, N'TargetImagesUpload:=80|TargetFilesUpload:=81|TargetDocsUpload:=82|SectionId:=0|CategoryId:=0|ItemType:=|ItemTypes:=|ListPageSize:=50|ShowSecurity:=1|ShowSeo:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|ShowItemsCount:=1|AllowOrdering:=1|AllowDelete:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|', 0, 3, N'AutoLayout.ascx', 0, N'', 0, N'', N'', 2, 2, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules] ([id], [title], [content], [ordering], [templateBlockName], [published], [moduleName], [moduleNamespace], [dateInserted], [userInserted], [dateUpdated], [userUpdated], [accessType], [showTitle], [moduleParams], [isCore], [menuSelection], [currView], [permissionId], [accessCode], [accessLevel], [cssFile], [cssClass], [useCache], [useLog], [directEditMode], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [systemMessagesTo]) VALUES (105, N'login-someone', N'', 19, N'content', 1, N'LoginForm', N'PigeonCms', CAST(N'2017-06-21T14:42:03.007' AS DateTime), N'admin', CAST(N'2017-06-21T14:46:29.110' AS DateTime), N'admin', 0, 0, N'RedirectUrl:=~/manager/dashboard.aspx|AppClientId:=|AppClientSecret:=|AppCallbackUri:=http://localhost:56718/pages/login-someone|EnableUserRegistration:=1|DefaultRoles:=someone', 0, 3, N'Someone.Login.ascx', 0, N'', 0, N'', N'', 2, 1, 0, 0, 0, N'', 0, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 1, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 4, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 7, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 53, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 54, N'Sections')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 55, N'Menu entries')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 56, N'Modules')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 57, N'Menu types')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 58, N'Categories')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 59, N'Items')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 61, N'Placeholders')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 62, N'Members')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 63, N'Roles')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 64, N'Template blocks ')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 65, N'Routing')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 66, N'Cultures')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 67, N'Settings')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 68, N'Web.config')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 69, N'Updates')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 74, N'Labels Pigeon')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 75, N'Logs')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 76, N'Site offline')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 83, N'Labels')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 85, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 86, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 88, N'breadcrumbs')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 90, N'Form attributes')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 91, N'Products')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 92, N'Products attributes')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 93, N'Coupons')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 95, N'Website labels')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 97, N'Items images upload')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 98, N'Items files upload')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 99, N'Documents upload')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 101, N'Error page')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 102, N'Page not fount')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 103, N'Manager dashboard')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 104, N'items composer')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'en-US', 105, N'Login with Someone')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 1, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 4, N'Default')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 7, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 53, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 54, N'Sezioni')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 55, N'Voci menu')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 56, N'Moduli')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 57, N'Tipi menu')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 58, N'Categorie')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 59, N'Elementi')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 61, N'Segnaposto')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 62, N'Utenti')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 63, N'Ruoli utente')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 64, N'Blocchi template')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 65, N'Routing')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 66, N'Lingue')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 67, N'Impostazioni')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 68, N'Web.config')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 69, N'Aggiornamenti')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 74, N'Etichette Pigeon')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 75, N'Logs')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 76, N'Sito offline')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 83, N'Gestione etichette')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 85, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 86, N'')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 88, N'breadcrumbs')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 90, N'Attributi form')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 91, N'Prodotti')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 92, N'Attributi prodotti')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 93, N'Coupons')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 95, N'Etichette sito')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 97, N'Upload immagini elementi')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 98, N'Upload files elementi')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 99, N'Upload allegati')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 101, N'Error page')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 102, N'Page not fount')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 103, N'Manager dashboard')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 104, N'items composer')
INSERT [dbo].[#__modules_Culture] ([cultureName], [moduleId], [title]) VALUES (N'it-IT', 105, N'Login with Someone')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (2, N'mainmenu')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (7, N'managerMenu')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (9, N'mainmenu')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (53, N'adminmenu')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (77, N'adminmenu')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (82, N'mainmenu')
INSERT [dbo].[#__modulesMenuTypes] ([moduleId], [menuType]) VALUES (88, N'mainmenu')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (11, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (12, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (13, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (14, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (15, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (16, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (17, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (18, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (19, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (20, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (21, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (22, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (24, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (25, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (26, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (27, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (29, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (30, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (31, N'debug')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (32, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (32, N'backend')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (32, N'manager')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (33, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (34, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (35, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (36, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (37, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (38, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (39, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (40, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (41, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (42, N'manager')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (43, N'admin')
INSERT [dbo].[#__permissions] ([id], [rolename]) VALUES (44, N'admin')
INSERT [dbo].[#__placeholders] ([name], [content], [visible]) VALUES (N'Analytics', N'<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src=''" + gaJsHost + "google-analytics.com/ga.js'' type=''text/javascript''%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try {
var pageTracker = _gat._getTracker("analytics code here");
pageTracker._trackPageview();
} catch(err) {}</script>', 1)
INSERT [dbo].[#__placeholders] ([name], [content], [visible]) VALUES (N'CompanyAddress', N'your address 69, city, nation', 1)
INSERT [dbo].[#__placeholders] ([name], [content], [visible]) VALUES (N'credits', N'powered by <a href="https://github.com/picce/pigeoncms" target="_blank">pigeoncms</a>', 1)
INSERT [dbo].[#__roles] ([rolename], [applicationName]) VALUES (N'admin', N'PigeonCms')
INSERT [dbo].[#__roles] ([rolename], [applicationName]) VALUES (N'backend', N'PigeonCms')
INSERT [dbo].[#__roles] ([rolename], [applicationName]) VALUES (N'debug', N'PigeonCms')
INSERT [dbo].[#__roles] ([rolename], [applicationName]) VALUES (N'manager', N'PigeonCms')
INSERT [dbo].[#__roles] ([rolename], [applicationName]) VALUES (N'public', N'PigeonCms')
INSERT [dbo].[#__routeParams] ([routeId], [paramKey], [paramValue], [paramConstraint], [paramDataType]) VALUES (10, N'action', N'sample', NULL, NULL)
INSERT [dbo].[#__routeParams] ([routeId], [paramKey], [paramValue], [paramConstraint], [paramDataType]) VALUES (10, N'controller', N'home', NULL, NULL)
INSERT [dbo].[#__routeParams] ([routeId], [paramKey], [paramValue], [paramConstraint], [paramDataType]) VALUES (12, N'action', N'NotFound', NULL, NULL)
INSERT [dbo].[#__routeParams] ([routeId], [paramKey], [paramValue], [paramConstraint], [paramDataType]) VALUES (12, N'controller', N'Error', NULL, NULL)
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (1, N'pages', N'pages/{pagename}', 1, 1, NULL, NULL, 1, NULL, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (2, N'catalog', N'catalog/{pagename}/{*pathinfo}', 1, 2, N'', N'', 0, 0, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (3, N'products list by category name', N'products/list/{pagename}/{*categoryname}', 1, 4, N'', N'', 0, NULL, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (4, N'product detail by name', N'products/{pagename}/{itemname}/{*pathinfo}', 1, 6, N'', N'', 0, NULL, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (5, N'product detail by id', N'products/{pagename}/id/{itemid}/{*pathinfo}', 1, 5, N'', N'', 0, NULL, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (6, N'admin area', N'admin/{pagename}', 1, 7, N'PigeonModern', N'SbAdmin', 1, 0, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (7, N'products list by category id', N'products/list/{pagename}/id/{*categoryid}', 1, 3, N'', N'', 0, NULL, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (8, N'manager', N'manager/{pagename}', 1, 8, N'', N'', 0, 0, N'', N'')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (9, N'site_default', N'', 0, 20, N'', N'', 0, 0, N'~/bin/YourMvcLib.dll', N'YourMvcLib.Helpers.CustomMvcHandler')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (10, N'site_page_sample', N'sample', 0, 30, N'', N'', 0, 0, N'~/bin/YourMvcLib.dll', N'YourMvcLib.Helpers.CustomMvcHandler')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (11, N'site_controller', N'{controller}/{action}/{*id}', 0, 40, N'', N'', 0, 0, N'~/bin/YourMvcLib.dll', N'YourMvcLib.Helpers.CustomMvcHandler')
INSERT [dbo].[#__routes] ([id], [name], [pattern], [published], [ordering], [currMasterPage], [currTheme], [isCore], [useSsl], [assemblyPath], [handlerName]) VALUES (12, N'site_catchall', N'{*url}', 0, 50, N'', N'', 0, 0, N'~/bin/YourMvcLib.dll', N'YourMvcLib.Helpers.CustomMvcHandler')
SET IDENTITY_INSERT [dbo].[#__sections] ON 

INSERT [dbo].[#__sections] ([id], [enabled], [accessType], [permissionId], [accessCode], [accessLevel], [writeAccessType], [writePermissionId], [writeAccessCode], [writeAccessLevel], [maxItems], [maxAttachSizeKB], [cssClass], [itemType], [extId], [seoId]) VALUES (1, 1, 0, 0, N'', 0, 1, 39, N'', 0, 0, 0, N'', N'', N'', NULL)
SET IDENTITY_INSERT [dbo].[#__sections] OFF
INSERT [dbo].[#__sections_Culture] ([cultureName], [sectionId], [title], [description]) VALUES (N'en-US', 1, N'Contents', N'')
INSERT [dbo].[#__sections_Culture] ([cultureName], [sectionId], [title], [description]) VALUES (N'it-IT', 1, N'Contenuti', N'')
SET IDENTITY_INSERT [dbo].[#__seo] ON 

INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (1, N'menus', CAST(N'2017-06-21T14:40:06.537' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (2, N'menus', CAST(N'2017-06-21T14:40:06.520' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (3, N'menus', CAST(N'2017-06-21T14:40:06.563' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (4, N'menus', CAST(N'2017-06-21T14:40:06.567' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (5, N'menus', CAST(N'2017-06-21T14:40:06.573' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (6, N'menus', CAST(N'2017-06-21T14:40:06.577' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (7, N'menus', CAST(N'2017-06-21T14:40:06.603' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (8, N'menus', CAST(N'2017-06-21T14:40:06.590' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (9, N'menus', CAST(N'2017-06-21T14:40:06.597' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (10, N'menus', CAST(N'2017-06-21T14:40:06.600' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (11, N'menus', CAST(N'2017-06-21T14:40:06.510' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (12, N'items', CAST(N'2017-06-21T14:27:07.353' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (13, N'menus', CAST(N'2017-06-21T14:40:06.530' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (14, N'menus', CAST(N'2017-06-21T14:40:06.613' AS DateTime), N'admin', 0, 0)
INSERT [dbo].[#__seo] ([id], [resourceSet], [dateUpdated], [userUpdated], [noIndex], [noFollow]) VALUES (15, N'menus', CAST(N'2017-06-21T14:46:29.097' AS DateTime), N'admin', 0, 0)
SET IDENTITY_INSERT [dbo].[#__seo] OFF
INSERT [dbo].[#__seo_Culture] ([cultureName], [seoId], [title], [description]) VALUES (N'en-US', 1, N'', N'')
INSERT [dbo].[#__seo_Culture] ([cultureName], [seoId], [title], [description]) VALUES (N'en-US', 12, N'', N'')
INSERT [dbo].[#__seo_Culture] ([cultureName], [seoId], [title], [description]) VALUES (N'en-US', 15, N'', N'')
INSERT [dbo].[#__seo_Culture] ([cultureName], [seoId], [title], [description]) VALUES (N'it-IT', 1, N'', N'')
INSERT [dbo].[#__seo_Culture] ([cultureName], [seoId], [title], [description]) VALUES (N'it-IT', 12, N'', N'')
INSERT [dbo].[#__seo_Culture] ([cultureName], [seoId], [title], [description]) VALUES (N'it-IT', 15, N'', N'')
INSERT [dbo].[#__staticPages] ([pageName], [visible], [showPageTitle]) VALUES (N'error', 1, 1)
INSERT [dbo].[#__staticPages] ([pageName], [visible], [showPageTitle]) VALUES (N'home', 1, 1)
INSERT [dbo].[#__staticPages] ([pageName], [visible], [showPageTitle]) VALUES (N'pageNotFound', 1, 1)
INSERT [dbo].[#__staticPages_Culture] ([cultureName], [pageName], [pageTitle], [pageContent]) VALUES (N'en-US', N'error', N'Ops! An error occurred.', N'<p>The resource you requested caused an error.<br />Go back to&nbsp;<a href="/pages/default.aspx">homepage</a>.</p>')
INSERT [dbo].[#__staticPages_Culture] ([cultureName], [pageName], [pageTitle], [pageContent]) VALUES (N'en-US', N'home', N'Home', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.<br /> <br /> Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros...</p>')
INSERT [dbo].[#__staticPages_Culture] ([cultureName], [pageName], [pageTitle], [pageContent]) VALUES (N'en-US', N'pageNotFound', N'Page not found', N'<p>The page you are looking for does not exists or is not available.<br /> Try to find it from website <a href="/pages/default.aspx">homepage</a>.</p>')
INSERT [dbo].[#__staticPages_Culture] ([cultureName], [pageName], [pageTitle], [pageContent]) VALUES (N'it-IT', N'error', N'Ops! Si ? verificato un errore.', N'<p>La risorsa richiesta ha generato un errore.<br />Torna alla&nbsp;<a href="/admin/&quot;/pages/default.aspx&quot;">homepage</a>.</p>')
INSERT [dbo].[#__staticPages_Culture] ([cultureName], [pageName], [pageTitle], [pageContent]) VALUES (N'it-IT', N'home', N'Home', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.<br /> <br /> Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>')
INSERT [dbo].[#__staticPages_Culture] ([cultureName], [pageName], [pageTitle], [pageContent]) VALUES (N'it-IT', N'pageNotFound', N'Pagina non trovata', N'<p>La pagina richiesta non esiste o non &egrave; disponibile.<br /> Prova ad accedere alla risorsa desiderata dalla <a href="/pages/default.aspx">homepage</a> del sito.</p>')
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Advert1', N'', 1, 6)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Advert2', N'', 1, 9)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Advert3', N'', 1, 10)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Banner1', N'for banners', 1, 7)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Banner2', N'for banners', 1, 11)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Banner3', N'for banners', 1, 12)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Bottom', N'bottom of the page', 1, 13)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Cpanel', N'*** not used', 0, 14)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Debug', N'debug info for dev, usually not visible to users', 1, 15)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Footer', N'page footer', 1, 8)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Header', N'page header', 1, 16)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Icon', N'*** not used', 0, 17)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Inset', N'*** not used', 0, 18)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Left', N'left column', 1, 1)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Legals', N'Legals info', 1, 2)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Mainbody', N'main body of the page', 1, 3)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Newsflash', N'news in brief', 1, 5)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Pathway', N'(breadcrumb)', 1, 19)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Right', N'right column', 1, 20)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Toolbar', N'menu bar', 1, 21)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Toolbar2', N'menu bar ', 1, 32)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'Top', N'top of the page', 1, 22)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User1', N'custom area', 1, 23)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User2', N'custom area', 1, 24)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User3', N'custom area', 1, 25)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User4', N'custom area', 1, 26)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User5', N'custom area', 1, 27)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User6', N'custom area', 1, 28)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User7', N'custom area', 1, 29)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User8', N'custom area', 1, 30)
INSERT [dbo].[#__templateBlocks] ([name], [title], [enabled], [orderId]) VALUES (N'User9', N'custom area', 1, 31)
INSERT [dbo].[#__usersInRoles] ([username], [rolename], [applicationName]) VALUES (N'admin', N'admin', N'PigeonCms')
INSERT [dbo].[#__usersInRoles] ([username], [rolename], [applicationName]) VALUES (N'admin', N'backend', N'PigeonCms')
INSERT [dbo].[#__usersInRoles] ([username], [rolename], [applicationName]) VALUES (N'admin', N'debug', N'PigeonCms')
INSERT [dbo].[#__usersInRoles] ([username], [rolename], [applicationName]) VALUES (N'manager', N'backend', N'PigeonCms')
INSERT [dbo].[#__usersInRoles] ([username], [rolename], [applicationName]) VALUES (N'manager', N'manager', N'PigeonCms')
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__labels]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__labels]') AND name = N'IX_#__labels')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__labels] ON [dbo].[#__labels]
(
	[cultureName] ASC,
	[resourceSet] ASC,
	[resourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__memberUsers]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__memberUsers]') AND name = N'IX_#__memberUsers')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__memberUsers] ON [dbo].[#__memberUsers]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__memberUsers_Meta]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__memberUsers_Meta]') AND name = N'IX_#__memberUsers_Meta')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__memberUsers_Meta] ON [dbo].[#__memberUsers_Meta]
(
	[username] ASC,
	[metaKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__menuTypes]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__menuTypes]') AND name = N'IX_#__menuTypes')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__menuTypes] ON [dbo].[#__menuTypes]
(
	[menuType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__routes_name]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__routes]') AND name = N'IX_#__routes_name')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__routes_name] ON [dbo].[#__routes]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__routes_pattern]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__routes]') AND name = N'IX_#__routes_pattern')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__routes_pattern] ON [dbo].[#__routes]
(
	[pattern] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_#__shop_coupons]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_coupons]') AND name = N'IX_#__shop_coupons')
CREATE UNIQUE NONCLUSTERED INDEX [IX_#__shop_coupons] ON [dbo].[#__shop_coupons]
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IXssn_#__shop_customers]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_customers]') AND name = N'IXssn_#__shop_customers')
CREATE NONCLUSTERED INDEX [IXssn_#__shop_customers] ON [dbo].[#__shop_customers]
(
	[ssn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IXvat_#__shop_customers]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_customers]') AND name = N'IXvat_#__shop_customers')
CREATE NONCLUSTERED INDEX [IXvat_#__shop_customers] ON [dbo].[#__shop_customers]
(
	[vat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IXOrderRef_#__shop_orderHeader]    Script Date: 21/06/2017 15:38:50 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[#__shop_orderHeader]') AND name = N'IXOrderRef_#__shop_orderHeader')
CREATE UNIQUE NONCLUSTERED INDEX [IXOrderRef_#__shop_orderHeader] ON [dbo].[#__shop_orderHeader]
(
	[orderRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
