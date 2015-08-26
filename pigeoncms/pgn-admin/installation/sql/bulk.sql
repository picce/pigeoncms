INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'CurrentMasterPage','Current Masterpage','sbadmin','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'CurrentTheme','Current Theme','sbadmin','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'DocsPrivatePath','Virtual Path for files upload/download in admin area','~/Private/Docs/','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'EmailSender','emails sender address','email@domain.com','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'GMapsApiKey','Google maps API Key','AAAAAAA_BBBB_CCCCC_DDDDD','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'MetaDescription','','PigeonCms - ASP.NET content management system','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'MetaKeywords','','PigeonCms, ASP.NET, cms, content management system, csharp, open source','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'MetaSiteTitle','Default website pages title','{Pigeoncms}','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'PgnVersion','PigeonCms last version data','2.0.0','@date: 20150203
@version: 2.0.0
@contributors: picce
@wn: deploy on github https://github.com/picce/pigeoncms
-new admin theme SbAdmin2 for all admin modules
-added lib PigeonCms.Core.Engine to use PigeonCms.Core inside classic asp.net website
-class fields in items, categories and sections
-sessionId in logs
-more flexible labels admin (textmode) and auto insert default values on page load
-start dev of PigeonCms.Shop
-removed old app_code files
-bugs fix
-started test with Dapper as ORM
')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'PhotoSize_Large','large images width','250','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'PhotoSize_Medium','medium images width','105','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'PhotoSize_Small','small images width','90','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'PhotoSize_Xlarge','extra large images width','350','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'SmtpServer','smtp host address','127.0.0.1','')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'SmtpUser','smtp user','',NULL)
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'StaticFilesTracking','Google analytics static files tracking','false','Enable google analytics tracking for static files such pdf, images etc.. Works only in modules that implement this functionality. Values=true|false')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'UseCache','Global use of Cache','true','true | false')
GO
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'UseLog','Global use of LogProvider','false','true | false')
GO
INSERT INTO #__categories (id,sectionId,parentId,enabled,ordering,defaultImageName,accessType,permissionId,accessCode,accessLevel,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,cssClass)  VALUES( '1','1','0',1,'1','',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
GO
INSERT INTO #__categories_Culture (cultureName,categoryId,title,description)  VALUES( 'en-US','1','PigeonCms','news about pigeoncms')
GO
INSERT INTO #__categories_Culture (cultureName,categoryId,title,description)  VALUES( 'it-IT','1','PigeonCms','news su pigeoncms')
GO
INSERT INTO #__cultures (cultureCode,displayName,enabled,ordering)  VALUES( 'de-DE','Deutsch',0,'3')
GO
INSERT INTO #__cultures (cultureCode,displayName,enabled,ordering)  VALUES( 'en-US','English',1,'1')
GO
INSERT INTO #__cultures (cultureCode,displayName,enabled,ordering)  VALUES( 'es-ES','Español',0,'2')
GO
INSERT INTO #__cultures (cultureCode,displayName,enabled,ordering)  VALUES( 'it-IT','Italiano',1,'0')
GO
INSERT INTO #__cultures (cultureCode,displayName,enabled,ordering)  VALUES( 'sl-SI','Slovenski',0,'4')
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AD','AND','EU','Andorra','020',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AE','ARE','AS','United Arab Emirates','784',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AF','AFG','AS','Afghanistan','004',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AG','ATG','NA','Antigua and Barbuda','028',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AI','AIA','NA','Anguilla','660',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AL','ALB','EU','Albania','008',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AM','ARM','AS','Armenia','051',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AO','AGO','AF','Angola','024',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AQ','ATA','AN','Antarctica','010',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AR','ARG','SA','Argentina','032',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AS','ASM','OC','American Samoa','016',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AT','AUT','EU','Austria','040',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AU','AUS','OC','Australia','036',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AW','ABW','NA','Aruba','533',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AX','ALA','EU','Åland','248',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'AZ','AZE','AS','Azerbaijan','031',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BA','BIH','EU','Bosnia and Herzegovina','070',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BB','BRB','NA','Barbados','052',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BD','BGD','AS','Bangladesh','050',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BE','BEL','EU','Belgium','056',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BF','BFA','AF','Burkina Faso','854',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BG','BGR','EU','Bulgaria','100',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BH','BHR','AS','Bahrain','048',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BI','BDI','AF','Burundi','108',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BJ','BEN','AF','Benin','204',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BL','BLM','NA','Saint Barthélemy','652',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BM','BMU','NA','Bermuda','060',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BN','BRN','AS','Brunei','096',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BO','BOL','SA','Bolivia','068',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BQ','BES','NA','Bonaire','535',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BR','BRA','SA','Brazil','076',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BS','BHS','NA','Bahamas','044',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BT','BTN','AS','Bhutan','064',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BV','BVT','AN','Bouvet Island','074',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BW','BWA','AF','Botswana','072',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BY','BLR','EU','Belarus','112',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'BZ','BLZ','NA','Belize','084',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CA','CAN','NA','Canada','124',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CC','CCK','AS','Cocos [Keeling] Islands','166',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CD','COD','AF','Democratic Republic of the Congo','180',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CF','CAF','AF','Central African Republic','140',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CG','COG','AF','Republic of the Congo','178',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CH','CHE','EU','Switzerland','756',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CI','CIV','AF','Ivory Coast','384',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CK','COK','OC','Cook Islands','184',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CL','CHL','SA','Chile','152',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CM','CMR','AF','Cameroon','120',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CN','CHN','AS','China','156',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CO','COL','SA','Colombia','170',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CR','CRI','NA','Costa Rica','188',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CU','CUB','NA','Cuba','192',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CV','CPV','AF','Cape Verde','132',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CW','CUW','NA','Curacao','531',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CX','CXR','AS','Christmas Island','162',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CY','CYP','EU','Cyprus','196',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'CZ','CZE','EU','Czech Republic','203',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'DE','DEU','EU','Germany','276',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'DJ','DJI','AF','Djibouti','262',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'DK','DNK','EU','Denmark','208',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'DM','DMA','NA','Dominica','212',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'DO','DOM','NA','Dominican Republic','214',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'DZ','DZA','AF','Algeria','012',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'EC','ECU','SA','Ecuador','218',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'EE','EST','EU','Estonia','233',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'EG','EGY','AF','Egypt','818',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'EH','ESH','AF','Western Sahara','732',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ER','ERI','AF','Eritrea','232',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ES','ESP','EU','Spain','724',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ET','ETH','AF','Ethiopia','231',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'FI','FIN','EU','Finland','246',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'FJ','FJI','OC','Fiji','242',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'FK','FLK','SA','Falkland Islands','238',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'FM','FSM','OC','Micronesia','583',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'FO','FRO','EU','Faroe Islands','234',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'FR','FRA','EU','France','250',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GA','GAB','AF','Gabon','266',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GB','GBR','EU','United Kingdom','826',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GD','GRD','NA','Grenada','308',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GE','GEO','AS','Georgia','268',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GF','GUF','SA','French Guiana','254',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GG','GGY','EU','Guernsey','831',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GH','GHA','AF','Ghana','288',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GI','GIB','EU','Gibraltar','292',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GL','GRL','NA','Greenland','304',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GM','GMB','AF','Gambia','270',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GN','GIN','AF','Guinea','324',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GP','GLP','NA','Guadeloupe','312',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GQ','GNQ','AF','Equatorial Guinea','226',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GR','GRC','EU','Greece','300',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GS','SGS','AN','South Georgia and the South Sandwich Islands','239',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GT','GTM','NA','Guatemala','320',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GU','GUM','OC','Guam','316',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GW','GNB','AF','Guinea-Bissau','624',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'GY','GUY','SA','Guyana','328',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'HK','HKG','AS','Hong Kong','344',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'HM','HMD','AN','Heard Island and McDonald Islands','334',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'HN','HND','NA','Honduras','340',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'HR','HRV','EU','Croatia','191',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'HT','HTI','NA','Haiti','332',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'HU','HUN','EU','Hungary','348',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ID','IDN','AS','Indonesia','360',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IE','IRL','EU','Ireland','372',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IL','ISR','AS','Israel','376',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IM','IMN','EU','Isle of Man','833',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IN','IND','AS','India','356',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IO','IOT','AS','British Indian Ocean Territory','086',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IQ','IRQ','AS','Iraq','368',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IR','IRN','AS','Iran','364',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IS','ISL','EU','Iceland','352',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'IT','ITA','EU','Italy','380',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'JE','JEY','EU','Jersey','832',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'JM','JAM','NA','Jamaica','388',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'JO','JOR','AS','Jordan','400',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'JP','JPN','AS','Japan','392',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KE','KEN','AF','Kenya','404',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KG','KGZ','AS','Kyrgyzstan','417',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KH','KHM','AS','Cambodia','116',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KI','KIR','OC','Kiribati','296',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KM','COM','AF','Comoros','174',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KN','KNA','NA','Saint Kitts and Nevis','659',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KP','PRK','AS','North Korea','408',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KR','KOR','AS','South Korea','410',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KW','KWT','AS','Kuwait','414',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KY','CYM','NA','Cayman Islands','136',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'KZ','KAZ','AS','Kazakhstan','398',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LA','LAO','AS','Laos','418',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LB','LBN','AS','Lebanon','422',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LC','LCA','NA','Saint Lucia','662',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LI','LIE','EU','Liechtenstein','438',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LK','LKA','AS','Sri Lanka','144',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LR','LBR','AF','Liberia','430',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LS','LSO','AF','Lesotho','426',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LT','LTU','EU','Lithuania','440',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LU','LUX','EU','Luxembourg','442',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LV','LVA','EU','Latvia','428',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'LY','LBY','AF','Libya','434',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MA','MAR','AF','Morocco','504',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MC','MCO','EU','Monaco','492',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MD','MDA','EU','Moldova','498',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ME','MNE','EU','Montenegro','499',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MF','MAF','NA','Saint Martin','663',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MG','MDG','AF','Madagascar','450',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MH','MHL','OC','Marshall Islands','584',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MK','MKD','EU','Macedonia','807',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ML','MLI','AF','Mali','466',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MM','MMR','AS','Myanmar [Burma]','104',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MN','MNG','AS','Mongolia','496',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MO','MAC','AS','Macao','446',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MP','MNP','OC','Northern Mariana Islands','580',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MQ','MTQ','NA','Martinique','474',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MR','MRT','AF','Mauritania','478',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MS','MSR','NA','Montserrat','500',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MT','MLT','EU','Malta','470',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MU','MUS','AF','Mauritius','480',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MV','MDV','AS','Maldives','462',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MW','MWI','AF','Malawi','454',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MX','MEX','NA','Mexico','484',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MY','MYS','AS','Malaysia','458',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'MZ','MOZ','AF','Mozambique','508',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NA','NAM','AF','Namibia','516',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NC','NCL','OC','New Caledonia','540',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NE','NER','AF','Niger','562',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NF','NFK','OC','Norfolk Island','574',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NG','NGA','AF','Nigeria','566',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NI','NIC','NA','Nicaragua','558',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NL','NLD','EU','Netherlands','528',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NO','NOR','EU','Norway','578',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NP','NPL','AS','Nepal','524',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NR','NRU','OC','Nauru','520',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NU','NIU','OC','Niue','570',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'NZ','NZL','OC','New Zealand','554',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'OM','OMN','AS','Oman','512',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PA','PAN','NA','Panama','591',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PE','PER','SA','Peru','604',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PF','PYF','OC','French Polynesia','258',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PG','PNG','OC','Papua New Guinea','598',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PH','PHL','AS','Philippines','608',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PK','PAK','AS','Pakistan','586',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PL','POL','EU','Poland','616',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PM','SPM','NA','Saint Pierre and Miquelon','666',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PN','PCN','OC','Pitcairn Islands','612',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PR','PRI','NA','Puerto Rico','630',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PS','PSE','AS','Palestine','275',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PT','PRT','EU','Portugal','620',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PW','PLW','OC','Palau','585',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'PY','PRY','SA','Paraguay','600',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'QA','QAT','AS','Qatar','634',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'RE','REU','AF','Réunion','638',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'RO','ROU','EU','Romania','642',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'RS','SRB','EU','Serbia','688',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'RU','RUS','EU','Russia','643',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'RW','RWA','AF','Rwanda','646',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SA','SAU','AS','Saudi Arabia','682',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SB','SLB','OC','Solomon Islands','090',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SC','SYC','AF','Seychelles','690',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SD','SDN','AF','Sudan','729',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SE','SWE','EU','Sweden','752',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SG','SGP','AS','Singapore','702',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SH','SHN','AF','Saint Helena','654',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SI','SVN','EU','Slovenia','705',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SJ','SJM','EU','Svalbard and Jan Mayen','744',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SK','SVK','EU','Slovakia','703',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SL','SLE','AF','Sierra Leone','694',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SM','SMR','EU','San Marino','674',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SN','SEN','AF','Senegal','686',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SO','SOM','AF','Somalia','706',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SR','SUR','SA','Suriname','740',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SS','SSD','AF','South Sudan','728',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ST','STP','AF','São Tomé and Príncipe','678',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SV','SLV','NA','El Salvador','222',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SX','SXM','NA','Sint Maarten','534',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SY','SYR','AS','Syria','760',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'SZ','SWZ','AF','Swaziland','748',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TC','TCA','NA','Turks and Caicos Islands','796',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TD','TCD','AF','Chad','148',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TF','ATF','AN','French Southern Territories','260',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TG','TGO','AF','Togo','768',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TH','THA','AS','Thailand','764',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TJ','TJK','AS','Tajikistan','762',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TK','TKL','OC','Tokelau','772',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TL','TLS','OC','East Timor','626',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TM','TKM','AS','Turkmenistan','795',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TN','TUN','AF','Tunisia','788',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TO','TON','OC','Tonga','776',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TR','TUR','AS','Turkey','792',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TT','TTO','NA','Trinidad and Tobago','780',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TV','TUV','OC','Tuvalu','798',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TW','TWN','AS','Taiwan','158',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'TZ','TZA','AF','Tanzania','834',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'UA','UKR','EU','Ukraine','804',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'UG','UGA','AF','Uganda','800',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'UM','UMI','OC','U.S. Minor Outlying Islands','581',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'US','USA','NA','United States','840',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'UY','URY','SA','Uruguay','858',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'UZ','UZB','AS','Uzbekistan','860',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VA','VAT','EU','Vatican City','336',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VC','VCT','NA','Saint Vincent and the Grenadines','670',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VE','VEN','SA','Venezuela','862',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VG','VGB','NA','British Virgin Islands','092',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VI','VIR','NA','U.S. Virgin Islands','850',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VN','VNM','AS','Vietnam','704',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'VU','VUT','OC','Vanuatu','548',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'WF','WLF','OC','Wallis and Futuna','876',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'WS','WSM','OC','Samoa','882',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'XK','XKX','EU','Kosovo','0',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'YE','YEM','AS','Yemen','887',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'YT','MYT','AF','Mayotte','175',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ZA','ZAF','AF','South Africa','710',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ZM','ZMB','AF','Zambia','894',NULL,NULL)
GO
INSERT INTO #__geoCountries (code,iso3,continent,name,custom1,custom2,custom3)  VALUES( 'ZW','ZWE','AF','Zimbabwe','716',NULL,NULL)
GO

SET IDENTITY_INSERT #__geoZones ON
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '1','US','AL','Alabama',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '2','US','AK','Alaska',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '3','US','AZ','Arizona',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '4','US','AR','Arkansas',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '5','US','CA','California',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '6','US','CO','Colorado',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '7','US','CT','Connecticut',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '8','US','DE','Delaware',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '9','US','DC','District of Columbia',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '10','US','FL','Florida',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '11','US','GA','Georgia',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '12','US','HI','Hawaii',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '13','US','ID','Idaho',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '14','US','IL','Illinois',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '15','US','IN','Indiana',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '16','US','IA','Iowa',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '17','US','KS','Kansas',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '18','US','KY','Kentucky',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '19','US','LA','Louisiana',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '20','US','ME','Maine',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '21','US','MD','Maryland',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '22','US','MA','Massachusetts',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '23','US','MI','Michigan',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '24','US','MN','Minnesota',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '25','US','MS','Mississippi',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '26','US','MO','Missouri',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '27','US','MT','Montana',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '28','US','NE','Nebraska',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '29','US','NV','Nevada',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '30','US','NH','New Hampshire',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '31','US','NJ','New Jersey',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '32','US','NM','New Mexico',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '33','US','NY','New York',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '34','US','NC','North Carolina',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '35','US','ND','North Dakota',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '36','US','OH','Ohio',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '37','US','OK','Oklahoma',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '38','US','OR','Oregon',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '39','US','PA','Pennsylvania',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '40','US','RI','Rhode Island',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '41','US','SC','South Carolina',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '42','US','SD','South Dakota',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '43','US','TN','Tennessee',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '44','US','TX','Texas',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '45','US','UT','Utah',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '46','US','VT','Vermont',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '47','US','VA','Virginia',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '48','US','WA','Washington',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '49','US','WV','West Virginia',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '50','US','WI','Wisconsin',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '51','US','WY','Wyoming',NULL,NULL,NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '52','IT','TO','Torino','001','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '53','IT','VC','Vercelli','002','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '54','IT','NO','Novara','003','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '55','IT','CN','Cuneo','004','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '56','IT','AT','Asti','005','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '57','IT','AL','Alessandria','006','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '58','IT','AO','Valle d''Aosta/Vallée d''Aoste','007','02',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '59','IT','IM','Imperia','008','07',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '60','IT','SV','Savona','009','07',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '61','IT','GE','Genova','010','07',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '62','IT','SP','La Spezia','011','07',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '63','IT','VA','Varese','012','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '64','IT','CO','Como','013','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '65','IT','SO','Sondrio','014','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '66','IT','MI','Milano','015','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '67','IT','BG','Bergamo','016','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '68','IT','BS','Brescia','017','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '69','IT','PV','Pavia','018','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '70','IT','CR','Cremona','019','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '71','IT','MN','Mantova','020','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '72','IT','BZ','Bolzano/Bozen','021','04',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '73','IT','TN','Trento','022','04',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '74','IT','VR','Verona','023','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '75','IT','VI','Vicenza','024','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '76','IT','BL','Belluno','025','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '77','IT','TV','Treviso','026','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '78','IT','VE','Venezia','027','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '79','IT','PD','Padova','028','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '80','IT','RO','Rovigo','029','05',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '81','IT','UD','Udine','030','06',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '82','IT','GO','Gorizia','031','06',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '83','IT','TS','Trieste','032','06',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '84','IT','PC','Piacenza','033','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '85','IT','PR','Parma','034','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '86','IT','RE','Reggio nell''Emilia','035','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '87','IT','MO','Modena','036','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '88','IT','BO','Bologna','037','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '89','IT','FE','Ferrara','038','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '90','IT','RA','Ravenna','039','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '91','IT','FC','Forlì-Cesena','040','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '92','IT','PU','Pesaro e Urbino','041','11',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '93','IT','AN','Ancona','042','11',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '94','IT','MC','Macerata','043','11',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '95','IT','AP','Ascoli Piceno','044','11',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '96','IT','MS','Massa-Carrara','045','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '97','IT','LU','Lucca','046','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '98','IT','PT','Pistoia','047','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '99','IT','FI','Firenze','048','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '100','IT','LI','Livorno','049','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '101','IT','PI','Pisa','050','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '102','IT','AR','Arezzo','051','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '103','IT','SI','Siena','052','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '104','IT','GR','Grosseto','053','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '105','IT','PG','Perugia','054','10',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '106','IT','TR','Terni','055','10',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '107','IT','VT','Viterbo','056','12',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '108','IT','RI','Rieti','057','12',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '109','IT','RM','Roma','058','12',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '110','IT','LT','Latina','059','12',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '111','IT','FR','Frosinone','060','12',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '112','IT','CE','Caserta','061','15',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '113','IT','BN','Benevento','062','15',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '114','IT','NA','Napoli','063','15',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '115','IT','AV','Avellino','064','15',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '116','IT','SA','Salerno','065','15',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '117','IT','AQ','L''Aquila','066','13',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '118','IT','TE','Teramo','067','13',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '119','IT','PE','Pescara','068','13',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '120','IT','CH','Chieti','069','13',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '121','IT','CB','Campobasso','070','14',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '122','IT','FG','Foggia','071','16',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '123','IT','BA','Bari','072','16',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '124','IT','TA','Taranto','073','16',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '125','IT','BR','Brindisi','074','16',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '126','IT','LE','Lecce','075','16',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '127','IT','PZ','Potenza','076','17',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '128','IT','MT','Matera','077','17',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '129','IT','CS','Cosenza','078','18',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '130','IT','CZ','Catanzaro','079','18',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '131','IT','RC','Reggio di Calabria','080','18',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '132','IT','TP','Trapani','081','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '133','IT','PA','Palermo','082','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '134','IT','ME','Messina','083','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '135','IT','AG','Agrigento','084','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '136','IT','CL','Caltanissetta','085','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '137','IT','EN','Enna','086','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '138','IT','CT','Catania','087','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '139','IT','RG','Ragusa','088','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '140','IT','SR','Siracusa','089','19',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '141','IT','SS','Sassari','090','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '142','IT','NU','Nuoro','091','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '143','IT','CA','Cagliari','092','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '144','IT','PN','Pordenone','093','06',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '145','IT','IS','Isernia','094','14',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '146','IT','OR','Oristano','095','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '147','IT','BI','Biella','096','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '148','IT','LC','Lecco','097','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '149','IT','LO','Lodi','098','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '150','IT','RN','Rimini','099','08',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '151','IT','PO','Prato','100','09',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '152','IT','KR','Crotone','101','18',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '153','IT','VV','Vibo Valentia','102','18',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '154','IT','VB','Verbano-Cusio-Ossola','103','01',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '155','IT','OT','Olbia-Tempio','104','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '156','IT','OG','Ogliastra','105','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '157','IT','VS','Medio Campidano','106','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '158','IT','CI','Carbonia-Iglesias','107','20',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '159','IT','MB','Monza e della Brianza','108','03',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '160','IT','FM','Fermo','109','11',NULL)
GO
INSERT INTO #__geoZones (id,countryCode,code,name,custom1,custom2,custom3)  VALUES( '161','IT','BT','Barletta-Andria-Trani','110','16',NULL)
GO
SET IDENTITY_INSERT #__geoZones OFF
GO

INSERT INTO #__items (id,itemType,categoryId,enabled,ordering,defaultImageName,dateInserted,userInserted,dateUpdated,userUpdated,CustomBool1,CustomBool2,CustomBool3,CustomDate1,CustomDate2,CustomDate3,CustomDecimal1,CustomDecimal2,CustomDecimal3,CustomInt1,CustomInt2,CustomInt3,CustomString1,CustomString2,CustomString3,ItemParams,accessType,permissionId,accessCode,accessLevel,itemDate,validFrom,validTo,alias,commentsGroupId,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,threadId,cssClass)  VALUES( '1','PigeonCms.Item','1',1,'1','',NULL,'admin',NULL,'admin',0,0,0,NULL,NULL,NULL,'0','0','0','0','0','0','','','','','0','0','','0',NULL,NULL,NULL,'item1','0','0','0','','0','1','')
GO
INSERT INTO #__items (id,itemType,categoryId,enabled,ordering,defaultImageName,dateInserted,userInserted,dateUpdated,userUpdated,CustomBool1,CustomBool2,CustomBool3,CustomDate1,CustomDate2,CustomDate3,CustomDecimal1,CustomDecimal2,CustomDecimal3,CustomInt1,CustomInt2,CustomInt3,CustomString1,CustomString2,CustomString3,ItemParams,accessType,permissionId,accessCode,accessLevel,itemDate,validFrom,validTo,alias,commentsGroupId,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,threadId,cssClass)  VALUES( '2','PigeonCms.Item','1',1,'2','',NULL,'admin',NULL,'admin',0,0,0,NULL,NULL,NULL,'0','0','0','0','0','0','','','','','1','0','','0',NULL,NULL,NULL,'item2','0',NULL,NULL,NULL,NULL,'2',NULL)
GO
INSERT INTO #__items (id,itemType,categoryId,enabled,ordering,defaultImageName,dateInserted,userInserted,dateUpdated,userUpdated,CustomBool1,CustomBool2,CustomBool3,CustomDate1,CustomDate2,CustomDate3,CustomDecimal1,CustomDecimal2,CustomDecimal3,CustomInt1,CustomInt2,CustomInt3,CustomString1,CustomString2,CustomString3,ItemParams,accessType,permissionId,accessCode,accessLevel,itemDate,validFrom,validTo,alias,commentsGroupId,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,threadId,cssClass)  VALUES( '3','PigeonCms.Shop.ProductItem','0',0,'3','',NULL,'admin',NULL,'admin',1,0,0,NULL,NULL,NULL,'0','0','0','0','0','0','','','','','0','0','','0',NULL,NULL,NULL,'','0','0','0','','0','3','')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'en-US','1','','<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.</p>
<hr class="system-readmore" />
<p>Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>
<p>&nbsp;</p>')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'en-US','2','','<p>This is second news</p>
<p>
<hr class="system-readmore" />
Second news text</p>')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'it-IT','1','Item1','<p>LOREM&nbsp;ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.</p>
<hr class="system-readmore" />
<p>Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem.</p>
<p>Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula.</p>
<hr class="system-pagebreak" />
<p>Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien.</p>
<hr class="system-pagebreak" />
<p>Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>
<p>&nbsp;</p>')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'it-IT','2','Item2','<p>Questa &egrave; la seconda news</p>
<p>
<hr class="system-readmore" />
Testo della seconda news</p>')
GO
SET IDENTITY_INSERT #__labels ON
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '3','it-IT','PigeonCms.EmailContactForm','LblInfoAdulti','adulti',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '4','en-US','PigeonCms.MenuAdmin','LblMenuType','Menu type',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '6','it-IT','PigeonCms.MenuAdmin','LblMenuType','Tipo menu',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '7','en-US','PigeonCms.MenuAdmin','LblContentType','Content type',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '8','it-IT','PigeonCms.MenuAdmin','LblContentType','Tipo contenuto',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '9','en-US','PigeonCms.Menuadmin','LblName','Name',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '10','it-IT','PigeonCms.Menuadmin','LblName','Nome',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '11','en-US','PigeonCms.Menuadmin','LblTitle','Title',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '12','it-IT','PigeonCms.Menuadmin','LblTitle','Titolo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '13','en-US','PigeonCms.Menuadmin','LblAlias','Alias',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '15','en-US','PigeonCms.Menuadmin','LblRoute','Route',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '17','en-US','PigeonCms.Menuadmin','LblLink','Link',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '18','en-US','PigeonCms.Menuadmin','LblRedirectTo','Redirect to',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '19','it-IT','PigeonCms.Menuadmin','LblRedirectTo','Trasferisci a',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '20','en-US','PigeonCms.Menuadmin','LblParentItem','Parent item',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '21','it-IT','PigeonCms.Menuadmin','LblParentItem','Elemento padre',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '22','en-US','PigeonCms.Menuadmin','ModuleTitle','Menu entries',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '23','it-IT','PigeonCms.Menuadmin','ModuleTitle','Voci menu',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '24','en-US','PigeonCms.Menuadmin','ModuleDescription','Menu items admin area',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '25','en-US','PigeonCms.StaticPage','PageNameLabel','Static content',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '26','it-IT','PigeonCms.StaticPage','PageNameLabel','Contenuto statico',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '27','en-US','PigeonCms.StaticPage','PageNameDescription','Name of the static page to display. If the content does not yet exist create it using [contents > static page] menu.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '28','it-IT','PigeonCms.StaticPage','PageNameDescription','Nome della pagina statica da visualizzare. Se il contenuto non esiste ancora crearlo dal menu [contenuti > pagine statiche].','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '29','en-US','PigeonCms.VideoPlayer','FileLabel','File',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '30','en-US','PigeonCms.VideoPlayer','FileDescription','Source file path or url',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '31','it-IT','PigeonCms.VideoPlayer','FileDescription','Percorso e indirizzo del file sorgente',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '32','en-US','PigeonCms.VideoPlayer','WidthLabel','Width',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '33','it-IT','PigeonCms.VideoPlayer','WidthLabel','Larghezza',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '34','en-US','PigeonCms.VideoPlayer','WidthDescription','Video width',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '35','it-IT','PigeonCms.VideoPlayer','WidthDescription','Larghezza del video',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '36','en-US','PigeonCms.VideoPlayer','HeightLabel','Height',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '37','it-IT','PigeonCms.VideoPlayer','HeightLabel','Altezza',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '38','en-US','PigeonCms.VideoPlayer','HeightDescription','Video Height',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '39','it-IT','PigeonCms.VideoPlayer','HeightDescription','Altezza del video',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '40','en-US','PigeonCms.VideoPlayer','ModuleTitle','Video player',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '41','it-IT','PigeonCms.VideoPlayer','ModuleTitle','Visualizzatore video',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '42','en-US','PigeonCms.Menuadmin','LblDetails','Details',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '44','it-IT','PigeonCms.Menuadmin','LblDetails','Dettagli',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '45','en-US','PigeonCms.Menuadmin','LblOptions','Options',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '46','it-IT','PigeonCms.Menuadmin','LblOptions','Opzioni',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '47','en-US','PigeonCms.Menuadmin','LblVisible','Visible',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '48','it-IT','PigeonCms.Menuadmin','LblVisible','Visibile',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '49','en-US','PigeonCms.Menuadmin','LblPublished','Published',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '50','it-IT','PigeonCms.Menuadmin','LblPublished','Pubblicato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '51','en-US','PigeonCms.Menuadmin','LblOverrideTitle','Override page title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '52','it-IT','PigeonCms.Menuadmin','LblOverrideTitle','Sovrascrivi titolo finestra','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '53','en-US','PigeonCms.Menuadmin','LblRecordInfo','Record info',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '54','it-IT','PigeonCms.Menuadmin','LblRecordInfo','Informazioni record',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '55','it-IT','PigeonCms.VideoPlayer','FileLabel','File',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '56','en-US','PigeonCms.Menuadmin','LblSecurity','Security',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '58','it-IT','PigeonCms.Menuadmin','LblSecurity','Sicurezza',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '59','en-US','PigeonCms.Menuadmin','LblViews','Views',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '60','it-IT','PigeonCms.Menuadmin','LblViews','Visualizzazioni',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '61','en-US','PigeonCms.Menuadmin','LblParameters','Parameters',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '62','it-IT','PigeonCms.Menuadmin','LblParameters','Parametri',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '63','it-IT','PigeonCms.OfflineAdmin','LblTitle','Titolo messaggio','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '64','en-US','PigeonCms.OfflineAdmin','LblTitle','Message title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '65','it-IT','PigeonCms.OfflineAdmin','LblMessage','Messaggio','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '66','en-US','PigeonCms.OfflineAdmin','LblMessage','Message','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '67','it-IT','PigeonCms.OfflineAdmin','LblOffline','Sito offline','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '68','en-US','PigeonCms.OfflineAdmin','LblOffline','Site offline','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '69','it-IT','PigeonCms.OfflineAdmin','ModuleTitle','Gestione sito offline','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '70','en-US','PigeonCms.OfflineAdmin','ModuleTitle','Offline admin','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '71','it-IT','PigeonCms.OfflineAdmin','LitOfflineWarning','ATTENZIONE. Stai mettendo il sito offline.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '72','en-US','PigeonCms.OfflineAdmin','LitOfflineWarning','WARNING. You are going to put website offline.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '73','en-US','PigeonCms.TopMenu','MenuLevelLabel','Menu level','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '74','it-IT','PigeonCms.TopMenu','MenuLevelLabel','Livello menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '75','en-US','PigeonCms.TopMenu','ShowChildLabel','Show childs','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '76','it-IT','PigeonCms.TopMenu','ShowChildLabel','Visualizza sottomenu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '77','en-US','PigeonCms.TopMenu','ListClassLabel','List css class','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '78','it-IT','PigeonCms.TopMenu','ListClassLabel','Classe css lista','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '79','en-US','PigeonCms.TopMenu','ItemSelectedClassLabel','Selected item css class','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '80','it-IT','PigeonCms.TopMenu','ItemSelectedClassLabel','Classe Css Elemento selezionato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '81','en-US','PigeonCms.TopMenu','ItemLastClassLabel','Last item css class','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '82','it-IT','PigeonCms.TopMenu','ItemLastClassLabel','Classe css ultimo elemento','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '83','en-US','PigeonCms.TopMenu','ShowPagePostFixLabel','Show page extension','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '84','it-IT','PigeonCms.TopMenu','ShowPagePostFixLabel','Visualizza estensione pagina','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '85','en-US','PigeonCms.TopMenu','ShowPagePostFixDescription','Show page .aspx extension','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '86','it-IT','PigeonCms.TopMenu','ShowPagePostFixDescription','Visualizza l''estensione .aspx della pagina','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '87','en-US','PigeonCms.TopMenu','MenuIdLabel','Css menu Id','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '88','it-IT','PigeonCms.TopMenu','MenuIdLabel','Css ID del menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '89','en-US','PigeonCms.TopMenu','MenuIdDescription','Css ID used for current menu.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '90','it-IT','PigeonCms.TopMenu','MenuIdDescription','ID css associato al menu corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '93','en-US','PigeonCms.ItemsAdmin','HomepageLabel','Show in homepage','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '94','it-IT','PigeonCms.ItemsAdmin','HomepageLabel','Visualizza in homepage','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '95','en-US','PigeonCms.MenuAdmin','LblTitleWindow','Window''s title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '96','it-IT','PigeonCms.MenuAdmin','LblTitleWindow','Titolo finestra','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '107','en-US','PigeonCms.ItemsAdmin','LblValidFrom','Valid from',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '109','it-IT','PigeonCms.ItemsAdmin','LblValidFrom','Valido dal',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '110','en-US','PigeonCms.ItemsAdmin','LblValidTo','Valid to','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '111','it-IT','PigeonCms.ItemsAdmin','LblValidTo','Valido fino al','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '114','en-US','PigeonCms.ItemsAdmin','LblTitle','Title',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '115','it-IT','PigeonCms.ItemsAdmin','LblTitle','Titolo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '116','en-US','PigeonCms.ItemsAdmin','LblDescription','Description',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '117','it-IT','PigeonCms.ItemsAdmin','LblDescription','Descrizione',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '118','it-IT','PigeonCms.ItemsAdmin','LblCategory','Categoria','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '119','en-US','PigeonCms.ItemsAdmin','LblCategory','Category','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '120','it-IT','PigeonCms.ItemsAdmin','LblEnabled','Abilitato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '121','en-US','PigeonCms.ItemsAdmin','LblEnabled','Enabled','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '122','it-IT','PigeonCms.ItemsAdmin','LblItemType','Tipo elemento','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '123','en-US','PigeonCms.ItemsAdmin','LblItemType','Item type','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '124','it-IT','PigeonCms.ItemsAdmin','ModuleTitle','Gestione elementi','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '125','en-US','PigeonCms.ItemsAdmin','ModuleTitle','Items manager','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '126','it-IT','PigeonCms.ItemsAdmin','LblItemDate','Data elemento','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '127','en-US','PigeonCms.ItemsAdmin','LblItemDate','Item date','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '128','en-US','PigeonCms.EmailContactForm','LblGenericError','An error occured',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '129','it-IT','PigeonCms.EmailContactForm','LblGenericError','Si è veirificato un errore',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '130','en-US','PigeonCms.EmailContactForm','LblGenericSucces','Operation completed',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '132','it-IT','PigeonCms.EmailContactForm','LblGenericSuccess','Operazione completata',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '133','it-IT','PigeonCms.FilesManager','FileExtensionsLabel','Estensioni file consentite','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '134','en-US','PigeonCms.FilesManager','FileExtensionsLabel','Allowed files extensions','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '135','it-IT','PigeonCms.FilesManager','FileExtensionsDescription','esempio: jpg;gif;doc','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '136','en-US','PigeonCms.FilesManager','FileExtensionsDescription','example: jpg;gif;doc','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '137','it-IT','PigeonCms.FilesManager','FileSizeLabel','Dimensione max file (KB)','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '138','en-US','PigeonCms.FilesManager','FileSizeLabel','Max file size (KB)','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '139','it-IT','PigeonCms.FilesManager','FileNameTypeLabel','Nome file','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '140','en-US','PigeonCms.FilesManager','FileNameTypeLabel','File name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '141','it-IT','PigeonCms.FilesManager','FileNameTypeDescription','Specifica come come sarà composto il file di destinazione','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '142','en-US','PigeonCms.FilesManager','FileNameTypeDescription','Specifies uploaded file name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '143','it-IT','PigeonCms.FilesManager','FilePrefixLabel','Prefisso file caricato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '144','en-US','PigeonCms.FilesManager','FilePrefixLabel','Uploaded file prefix','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '145','it-IT','PigeonCms.FilesManager','FilePrefixDescription','Applicato solo se non viene scelto di mantenere il nome file originale','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '146','en-US','PigeonCms.FilesManager','FilePrefixDescription','It will be applied only if you choose not to mantain original file name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '147','it-IT','PigeonCms.FilesManager','FilePathLabel','Path di caricamento files','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '148','en-US','PigeonCms.FilesManager','FilePathLabel','Uploaded files path','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '149','it-IT','PigeonCms.FilesManager','FilePathDescription','Path dove verrranno caricati i files (es. ~/Public/Files)','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '150','en-US','PigeonCms.FilesManager','FilePathDescription','Destination path for uploaded files (ex. ~/Public/Gallery)','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '151','it-IT','PigeonCms.FilesManager','UploadFieldsLabel','Max numero di campi upload','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '152','en-US','PigeonCms.FilesManager','UploadFieldsLabel','Max number of upload fields','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '153','it-IT','PigeonCms.FilesManager','UploadFieldsDescription','Numero massimo di files che si possono caricare contemporaneamente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '154','en-US','PigeonCms.FilesManager','UploadFieldsDescription','Max files to upload concurrently','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '155','it-IT','PigeonCms.MenuAdmin','LblMenuTypeDescription','Tipo di menu tra quelli creati al quale appartiene la voce corrente.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '156','en-US','PigeonCms.MenuAdmin','LblMenuTypeDescription','Menu type owner of current menu entry','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '157','it-IT','PigeonCms.MenuAdmin','LblContentTypeDescription','Tipo di contenuto visualizzato dalla voce di menu corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '158','en-US','PigeonCms.MenuAdmin','LblContentTypeDescription','Content type show by current menu entry','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '159','it-IT','PigeonCms.MenuAdmin','LblNameDescription','Nome mnemonico assegnato alla voce menu. Utilizzato solo per riconoscere la voce nella lista.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '160','en-US','PigeonCms.MenuAdmin','LblNameDescription','Mnemonic name assigned to the menu item. Used only to recognize the entry in the list.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '161','it-IT','PigeonCms.MenuAdmin','LblTitleDescription','Titolo della voce di menu. Apparirà come link nel menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '162','en-US','PigeonCms.MenuAdmin','LblTitleDescription','Title of the menu item. It will be the text of the link in menu items','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '163','it-IT','PigeonCms.MenuAdmin','LblTitleWindowDescription','Titolo della finestra del browser','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '164','en-US','PigeonCms.MenuAdmin','LblTitleWindowDescription','Title of the browser window','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '165','it-IT','PigeonCms.MenuAdmin','LblAliasDescription','Nome della pagina nell url della risorsa corrente. Esempio: mia-pagina','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '166','en-US','PigeonCms.MenuAdmin','LblAliasDescription','Page name in the url of the current resource. Example: my-page','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '167','it-IT','PigeonCms.MenuAdmin','LblRouteDescription','Percorso virtuale di accesso alla risorsa. Scegliere un percorso significativo per la voce corrente.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '168','en-US','PigeonCms.MenuAdmin','LblRouteDescription','Virtual path to access current resource. Choose a location significant to the current entry.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '169','it-IT','PigeonCms.MenuAdmin','LblLinkDescription','Collegamento ad una risorsa locale o esterna (sole se tipomenu link o javascript). Es.: http://www.google.it, mypage.aspx, ~/pages/mypage.aspx, alert()','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '170','en-US','PigeonCms.MenuAdmin','LblLinkDescription','Link to local or external resource (only for menutype link or javascript). Example: http://www.google.it, mypage.aspx, ~/pages/mypage.aspx, alert()','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '171','it-IT','PigeonCms.MenuAdmin','LblRedirectToDescription','Solo per tipo menu=alias. Scegliere la risorsa menu alla quale essere reindirizzato quando si accede alla voce di menu corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '172','en-US','PigeonCms.MenuAdmin','LblRedirectToDescription','Only for menutype=alias. Choose the menu resource to be redirected to when you click current menu item.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '173','it-IT','PigeonCms.MenuAdmin','LblParentItemDescription','Scegli la voce di menu padre alla quale la voce corrente dovrà appartenere per creare un sottomenu. ','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '174','en-US','PigeonCms.MenuAdmin','LblParentItemDescription','Choose parent menu entry for current item to create a submenu.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '175','it-IT','PigeonCms.MenuAdmin','LblCssClass','Classe css','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '176','en-US','PigeonCms.MenuAdmin','LblCssClass','Css class','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '177','it-IT','PigeonCms.MenuAdmin','LblCssClassDescription','Classe css personalizzata per la voce di menu corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '178','en-US','PigeonCms.MenuAdmin','LblCssClassDescription','Custom css class for current menu entry','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '179','it-IT','PigeonCms.MenuAdmin','LblTheme','Tema pagina','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '180','en-US','PigeonCms.MenuAdmin','LblTheme','Theme','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '181','it-IT','PigeonCms.MenuAdmin','LblThemeDescription','Tema di visualizzazione per la pagina corrente. Se non specificato verrà utilizzato quello di default per la Route scelta o quello di default del sito web.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '182','en-US','PigeonCms.MenuAdmin','LblThemeDescription','Display theme for the current page. If not specified, the default will be used for Route choice or the default Web site one.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '183','it-IT','PigeonCms.MenuAdmin','LblMasterpageDescription','Layout grafico per la pagina corrente. Se non specificato verrà utilizzato quello di default per la Route scelta o quello di default del sito web.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '184','en-US','PigeonCms.MenuAdmin','LblMasterpageDescription','Graphical layout for the current page. If not specified, the default will be used for Route choice or the default Web site one.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '185','it-IT','PigeonCms.MenuAdmin','LblVisibleDescription','Visualizza la voce di menu.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '186','en-US','PigeonCms.MenuAdmin','LblVisibleDescription','Show or not menu entry','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '187','it-IT','PigeonCms.MenuAdmin','LblPublishedDescription','Pubblica o meno il contenuto. La voce può essere non visibile ma pubblicata. In questo modo non sarà visibile a menu ma sarà comunque raggiungibile tramite il suo url.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '188','en-US','PigeonCms.MenuAdmin','LblPublishedDescription','Publish or not the content. The entry could be published but not visible, in this way you will not see the menu entry but it will be accessible by using its URL.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '189','it-IT','PigeonCms.MenuAdmin','LblOverrideTitleDescription','Se selezionato imposta il titolo della finestra del browser con il testo della casella Titolo Finestra','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '190','en-US','PigeonCms.MenuAdmin','LblOverrideTitleDescription','If selected sets the title of the browser window with the Window Title text box','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '191','it-IT','PigeonCms.MenuAdmin','LblShowModuleTitle','Visualizza titolo modulo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '192','en-US','PigeonCms.MenuAdmin','LblShowModuleTitle','Show module title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '193','it-IT','PigeonCms.MenuAdmin','LblShowModuleTitleDescription','Visualizza il titolo del modulo associato al menu corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '194','en-US','PigeonCms.MenuAdmin','LblShowModuleTitleDescription','Displays the title of the module associated with the current menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '195','it-IT','PigeonCms.PermissionsControl','LblSecurity','Sicurezza','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '196','en-US','PigeonCms.PermissionsControl','LblSecurity','Security','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '197','it-IT','PigeonCms.PermissionsControl','LblAccessType','Tipo di accesso','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '198','en-US','PigeonCms.PermissionsControl','LblAccessType','Access type','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '199','it-IT','PigeonCms.PermissionsControl','LblAccessTypeDescription','Tipo di accesso alla risorsa. [Public] visibile ad ogni utente. [Registered] visibile solo agli utenti autenticati che soddisfano il [codice di accesso] ed il [livello di accesso] quando impostati.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '200','en-US','PigeonCms.PermissionsControl','LblAccessTypeDescription','Resource access type. [Public] visible for all users. [Registered] visible only for logged in users only authenticated users that meet the [access code] and [access level] when set.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '201','it-IT','PigeonCms.PermissionsControl','LblRolesAllowed','Ruoli consentiti','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '202','en-US','PigeonCms.PermissionsControl','LblRolesAllowed','Allowed roles','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '203','it-IT','PigeonCms.PermissionsControl','LblRolesAllowedDescription','Ruoli utente che possono accedere alla risorsa. Solo per tipo accesso (registrato). Tenere premuto CTRL per effettuare una selezione multipla.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '204','en-US','PigeonCms.PermissionsControl','LblRolesAllowedDescription','User roles that can access the resource. Only for access type (registered). Hold down CTRL to make a multiple selection.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '205','it-IT','PigeonCms.PermissionsControl','LblAccessCode','Codice accesso','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '206','en-US','PigeonCms.PermissionsControl','LblAccessCode','Access code','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '207','it-IT','PigeonCms.PermissionsControl','LblAccessCodeDescription','Codice di accesso utente necessario per accedere alla risorsa. Se vuoto non verrà considerato.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '208','en-US','PigeonCms.PermissionsControl','LblAccessCodeDescription','User access code required to access the resource. If empty will not be considered.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '209','it-IT','PigeonCms.PermissionsControl','LblAccessLevel','Livello di accesso','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '210','en-US','PigeonCms.PermissionsControl','LblAccessLevel','Acces level','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '211','it-IT','PigeonCms.PermissionsControl','LblAccessLevelDescription','Minimo livello di accesso utente necessario per accedere alla risorsa. Se vuoto non verrà considerato.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '212','en-US','PigeonCms.PermissionsControl','LblAccessLevelDescription','Minimum user access level required to access the resource. If empty will not be considered.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '213','it-IT','PigeonCms.ModuleParams','LblUseCache','Usa la cache','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '214','en-US','PigeonCms.ModuleParams','LblUseCache','Use cache','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '215','it-IT','PigeonCms.ModuleParams','LblUseCacheDescription','Consente al modulo di caricare i dati dalla cache del server (solo se supportato dal modulo). Use global mantiene le impostazioni globali del sito. ','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '216','en-US','PigeonCms.ModuleParams','LblUseCacheDescription','Allows the module to load data from the web server cache (if supported by the module). Use global maintains the site default settings. ','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '217','it-IT','PigeonCms.ModuleParams','LblUseLog','Registra log','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '218','en-US','PigeonCms.ModuleParams','LblUseLog','Use log','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '219','it-IT','PigeonCms.ModuleParams','LblUseLogDescription','Registra gli eventi significativi del modulo nel registro di log (solo se supportato dal modulo). Use global mantiene le impostazioni globali del sito. ATTENZIONE: se abilitato aumenta il carico di lavoro del server.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '220','en-US','PigeonCms.ModuleParams','LblUseLogDescription','Save significant events of the module in the log list (only if supported by the module). Use global mantains site default settings. WARNING: it increases the workload of the server when enabled.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '221','it-IT','PigeonCms.ModuleParams','LblCssFile','File css','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '222','en-US','PigeonCms.ModuleParams','LblCssFile','Css file','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '223','it-IT','PigeonCms.ModuleParams','LblCssFileDescription','Nome del file css da caricare (es. miofile.css). Il file deve essere nella cartella della visualizzazione scelta per il modulo corrente.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '224','en-US','PigeonCms.ModuleParams','LblCssFileDescription','Name of css file to upload (ex. myfile.css). The file must be in the folder of the selected view for the current module.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '225','it-IT','PigeonCms.ModuleParams','LblCssClass','Classe css','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '226','en-US','PigeonCms.ModuleParams','LblCssClass','Css class','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '227','it-IT','PigeonCms.ModuleParams','LblCssClassDescription','Classe css personalizzata per il modulo corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '228','en-US','PigeonCms.ModuleParams','LblCssClassDescription','Custom css class for current module','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '229','it-IT','PigeonCms.MenuAdmin','LblViewsDescription','Visualizzazione del modulo corrente. Se il modulo prevede diversi tipi di visualizzazione sceglierlo dalla lista.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '230','en-US','PigeonCms.MenuAdmin','LblViewsDescription','Type of view of the current module. If the form provides different viewing options choose one from the list.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '231','it-IT','PigeonCms.ModulesAdmin','ModuleTitle','Gestione moduli','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '232','en-US','PigeonCms.ModulesAdmin','ModuleTitle','Modules management','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '233','it-IT','PigeonCms.ModulesAdmin','LblCreated','Creazione','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '234','en-US','PigeonCms.ModulesAdmin','LblCreated','Created','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '235','it-IT','PigeonCms.ModulesAdmin','LblLastUpdate','Ultimo aggiornamento','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '236','en-US','PigeonCms.ModulesAdmin','LblLastUpdate','Last update','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '237','it-IT','PigeonCms.ModulesAdmin','LblDetails','Dettagli','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '238','en-US','PigeonCms.ModulesAdmin','LblDetails','Details','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '239','it-IT','PigeonCms.ModulesAdmin','LblModuleType','Tipo modulo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '240','en-US','PigeonCms.ModulesAdmin','LblModuleType','Module type','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '241','it-IT','PigeonCms.ModulesAdmin','LblModuleTypeDescription','Tipo di contenuto del modulo corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '242','en-US','PigeonCms.ModulesAdmin','LblModuleTypeDescription','Content type shown by current module','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '243','it-IT','PigeonCms.ModulesAdmin','LblTitle','Titolo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '244','en-US','PigeonCms.ModulesAdmin','LblTitle','Title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '245','it-IT','PigeonCms.ModulesAdmin','LblTitleDescription','Titolo del modulo. Verrà visualizzato se abilitata l opzione [visualizza titolo]','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '246','en-US','PigeonCms.ModulesAdmin','LblTitleDescription','Module title. It will be shown when [show title] is checked','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '247','it-IT','PigeonCms.ModulesAdmin','LblShowTitle','Visualizza titolo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '248','en-US','PigeonCms.ModulesAdmin','LblShowTitle','Show title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '249','it-IT','PigeonCms.ModulesAdmin','LblShowTitleDescription','Visualizza il titolo del modulo nella posizione prevista','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '250','en-US','PigeonCms.ModulesAdmin','LblShowTitleDescription','Shows module title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '251','it-IT','PigeonCms.ModulesAdmin','LblPublished','Pubblicato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '252','en-US','PigeonCms.ModulesAdmin','LblPublished','Published','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '253','it-IT','PigeonCms.ModulesAdmin','LblPublishedDescription','Pubblica o meno il modulo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '254','en-US','PigeonCms.ModulesAdmin','LblPublishedDescription','Publish or not the current module','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '255','it-IT','PigeonCms.ModulesAdmin','LblPosition','Posizione','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '256','en-US','PigeonCms.ModulesAdmin','LblPosition','Position','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '257','it-IT','PigeonCms.ModulesAdmin','LblPositionDescription','Posizione del modulo nel layout della pagina corrente. Se tale [blocco template] non fosse presente nel layout il modulo non verrà visualizzato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '258','en-US','PigeonCms.ModulesAdmin','LblPositionDescription','Module position in the layout of currente page. If current layout does not contains that [template block] module will be not shown.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '259','it-IT','PigeonCms.ModulesAdmin','LblOrder','Ordine','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '260','en-US','PigeonCms.ModulesAdmin','LblOrder','Order','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '261','it-IT','PigeonCms.ModulesAdmin','LblOrderDescription','Ordine di visualizzazione del modulo all interno della corrente posizione','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '262','en-US','PigeonCms.ModulesAdmin','LblOrderDescription','Display order inside the current position','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '263','it-IT','PigeonCms.ModulesAdmin','LblContent','Contenuto','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '264','en-US','PigeonCms.ModulesAdmin','LblContent','Content','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '265','it-IT','PigeonCms.ModulesAdmin','LblContentDescription','Attualmente non utilizzato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '266','en-US','PigeonCms.ModulesAdmin','LblContentDescription','Actually not used','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '267','it-IT','PigeonCms.ModulesAdmin','LblMenuEntries','Voci menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '268','en-US','PigeonCms.ModulesAdmin','LblMenuEntries','Menu entries','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '269','it-IT','PigeonCms.ModulesAdmin','LblMenus','Visualizza in','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '270','en-US','PigeonCms.ModulesAdmin','LblMenus','Show in','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '271','it-IT','PigeonCms.ModulesAdmin','LblMenusDescription','Specifica in quali pagine visualizzare il modulo corrente','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '272','en-US','PigeonCms.ModulesAdmin','LblMenusDescription','Specify which pages display the current module','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '273','it-IT','PigeonCms.ModulesAdmin','LblMenuAll','Tutte','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '274','en-US','PigeonCms.ModulesAdmin','LblMenuAll','All','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '275','it-IT','PigeonCms.ModulesAdmin','LblMenuNone','Nessuna','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '276','en-US','PigeonCms.ModulesAdmin','LblMenuNone','None','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '277','it-IT','PigeonCms.ModulesAdmin','LblMenuSelection','Singole voci di menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '278','en-US','PigeonCms.ModulesAdmin','LblMenuSelection','Select menu items','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '279','it-IT','PigeonCms.ModulesAdmin','LblMenuEntriesDescription','Scegli le singole voci di menu nelle quali verrà visualizzato il modulo, solo se [visualizza in]=singole voci di menu. Tenere premuto CTRL per selezione multipla. Scegliendo il nome del menu (es, [mainmenu]) il module verrà visualizzata in tutte le sue voci.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '280','en-US','PigeonCms.ModulesAdmin','LblMenuEntriesDescription','Choose individual menu items in which the module will appear, only if [menus] = [select menu items]. Hold CTRL for multiple selection. Choosing the menu name (eg, [mainmenu]) the module will be displayed in all its entries.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '281','it-IT','PigeonCms.ModulesAdmin','LblViews','Visualizzazioni','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '282','en-US','PigeonCms.ModulesAdmin','LblViews','Views','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '283','it-IT','PigeonCms.ModulesAdmin','LblViewsDescription','Visualizzazione del modulo corrente. Se il modulo prevede diversi tipi di visualizzazione sceglierlo dalla lista.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '284','en-US','PigeonCms.ModulesAdmin','LblViewsDescription','Type of view of the current module. If the form provides different viewing options choose one from the list.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '285','it-IT','PigeonCms.ItemsAdmin','LblCreated','Creazione','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '286','en-US','PigeonCms.ItemsAdmin','LblCreated','Created','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '287','it-IT','PigeonCms.ItemsAdmin','LblLastUpdate','Ultimo aggiornamento','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '288','en-US','PigeonCms.ItemsAdmin','LblLastUpdate','Last update','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '289','it-IT','PigeonCms.ItemsAdmin','LblFields','Campi personalizzati','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '290','en-US','PigeonCms.ItemsAdmin','LblFields','Custom fields','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '291','it-IT','PigeonCms.ItemsAdmin','LblParameters','Parametri','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '292','en-US','PigeonCms.ItemsAdmin','LblParameters','Parameters','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '293','it-IT','PigeonCms.StaticPagesAdmin','ModuleTitle','Contenuti statici','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '294','en-US','PigeonCms.StaticPagesAdmin','ModuleTitle','Static contents','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '295','it-IT','PigeonCms.StaticPagesAdmin','LblName','Nome','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '296','en-US','PigeonCms.StaticPagesAdmin','LblName','Name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '297','it-IT','PigeonCms.StaticPagesAdmin','LblNameDescription','Nome mnemonico assegnato al contenuto. Utilizzato solo per riconoscere la voce nella lista.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '298','en-US','PigeonCms.StaticPagesAdmin','LblNameDescription','Mnemonic name assigned to the content. Used only to recognize the entry in the list.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '299','it-IT','PigeonCms.StaticPagesAdmin','LblTitle','Titolo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '300','en-US','PigeonCms.StaticPagesAdmin','LblTitle','Title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '301','it-IT','PigeonCms.StaticPagesAdmin','LblTitleDescription','Titolo della pagina. Verrà visualizzato se abilitata l opzione [visualizza titolo]','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '302','en-US','PigeonCms.StaticPagesAdmin','LblTitleDescription','Page title. It will be shown when [show title] is checked','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '303','it-IT','PigeonCms.StaticPagesAdmin','LblShowTitle','Visualizza titolo','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '304','en-US','PigeonCms.StaticPagesAdmin','LblShowTitle','Show title','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '305','it-IT','PigeonCms.StaticPagesAdmin','LblShowTitleDescription','Quando abilitato visualizza il titolo della pagina','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '306','en-US','PigeonCms.StaticPagesAdmin','LblShowTitleDescription','When checked it shows the title of the page','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '307','it-IT','PigeonCms.StaticPagesAdmin','LblVisible','Visibile','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '308','en-US','PigeonCms.StaticPagesAdmin','LblVisible','Visible','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '309','it-IT','PigeonCms.StaticPagesAdmin','LblVisibleDescription','Visualizza o meno il contenuto, anche se presente in più voci di menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '310','en-US','PigeonCms.StaticPagesAdmin','LblVisibleDescription','Shows or not the static content, also if contained in many menu entries','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '311','it-IT','PigeonCms.StaticPagesAdmin','LblContent','Contenuto','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '312','en-US','PigeonCms.StaticPagesAdmin','LblContent','Content','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '313','it-IT','PigeonCms.ItemsAdmin','LblItemTypeDescription','Tipo di item selezionato. In base al tipo scelto saranno disponibili campi personalizzati e parametri diversi.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '314','en-US','PigeonCms.ItemsAdmin','LblItemTypeDescription','Selected item type. Depending on it will be aavailable differen custom fields and parameters.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '315','it-IT','PigeonCms.ItemsAdmin','LblCategoryDescription','Categoria di appartenenza dell item corrente. ','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '316','en-US','PigeonCms.ItemsAdmin','LblCategoryDescription','Current of current item.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '317','it-IT','PigeonCms.ItemsAdmin','LblEnabledDescription','Abilita o meno l item corrent','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '318','en-US','PigeonCms.ItemsAdmin','LblEnabledDescription','Enable or not the current item','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '319','it-IT','PigeonCms.ItemsAdmin','LblTitleDescription','Titolo dell elemento. Verrà visualizzato a seconda della visualizzazione scelta nel modulo di visualizzazione [PigeonCms.Item]','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '320','en-US','PigeonCms.ItemsAdmin','LblTitleDescription','Title of the item. It will be shown depending on the choosen view in the module [PigeonCms.Item] ','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '321','it-IT','PigeonCms.ItemsAdmin','LblItemDateDescription','Data associata all elemento. Viene impostata in automatico la data di creazione.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '322','en-US','PigeonCms.ItemsAdmin','LblItemDateDescription','Date associated to the element. By default it is set to current date when created.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '323','it-IT','PigeonCms.ItemsAdmin','LblValidFromDescription','Data di inizio validità dell elemento. Impostata automaticamente alla data corrente in fase di creazione. Se si imposta una data futura l elemento non verrà visualizzato fino a tale data.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '324','en-US','PigeonCms.ItemsAdmin','LblValidFromDescription','Effective Date of the element. Automatically set to the current date when element is created. If you set a future date the item will not appear until that date.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '325','it-IT','PigeonCms.ItemsAdmin','LblValidToDescription','Data di fine validità dell elemento. Lasciare vuoto per non associare una scedenza.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '326','en-US','PigeonCms.ItemsAdmin','LblValidToDescription','Expiring date of the element. Element never expires when blank.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '327','it-IT','PigeonCms.ItemsAdmin','LblDescriptionDescription','Descrizione dell elemento (in più lingue quando abilitate).','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '328','en-US','PigeonCms.ItemsAdmin','LblDescriptionDescription','Description of the item (in more languages when enabled)','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '329','it-IT','PigeonCms.EmailContactForm','LblName','nome','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '330','en-US','PigeonCms.EmailContactForm','LblName','name and surname','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '331','it-IT','PigeonCms.EmailContactForm','LblCompanyName','azienda','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '332','en-US','PigeonCms.EmailContactForm','LblCompanyName','company name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '333','it-IT','PigeonCms.EmailContactForm','LblCity','città','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '334','en-US','PigeonCms.EmailContactForm','LblCity','city','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '335','it-IT','PigeonCms.EmailContactForm','LblInfoEmail','e-mail','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '336','en-US','PigeonCms.EmailContactForm','LblInfoEmail','e-mail','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '337','it-IT','PigeonCms.EmailContactForm','LblPhone','telefono','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '338','en-US','PigeonCms.EmailContactForm','LblPhone','phone','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '339','it-IT','PigeonCms.EmailContactForm','LblMessage','messaggio','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '340','en-US','PigeonCms.EmailContactForm','LblMessage','message','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '341','it-IT','PigeonCms.EmailContactForm','LblPrivacyText','Informativa ai sensi dell’art. 13 del d.lgs. n. 196/2003.','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '342','en-US','PigeonCms.EmailContactForm','LblPrivacyText','privacy text message','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '343','it-IT','PigeonCms.MenuAdmin','LblChange','cambia','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '344','en-US','PigeonCms.MenuAdmin','LblChange','change','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '345','it-IT','PigeonCms.MenuAdmin','LblChangeDescription','Consente di cambiare il tipo di contenuto della voce corrente. ATTENZIONE: gli eventuali parametri associati al modulo corrente andranno persi','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '346','en-US','PigeonCms.MenuAdmin','LblChangeDescription','Change the content type of the current entry. WARNING: Any parameters associated with the current module will be lost','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '347','it-IT','PigeonCms.ItemsSearch','LblSearchText','','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '348','en-US','PigeonCms.ItemsSearch','LblSearchText','','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '349','it-IT','PigeonCms.ItemsSearch','LblSearchLink','cerca','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '350','en-US','PigeonCms.ItemsSearch','LblSearchLink','search','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '351','it-IT','PigeonCms.PlaceholdersAdmin','ModuleTitle','Segnaposto','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '352','en-US','PigeonCms.PlaceholdersAdmin','ModuleTitle','Placeholders','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '353','it-IT','PigeonCms.PlaceholdersAdmin','LblName','Nome','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '354','en-US','PigeonCms.PlaceholdersAdmin','LblName','Name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '355','it-IT','PigeonCms.PlaceholdersAdmin','LblContent','Contenuto','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '356','en-US','PigeonCms.PlaceholdersAdmin','LblContent','Content','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '357','it-IT','PigeonCms.PlaceholdersAdmin','LblVisible','Visibile','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '358','en-US','PigeonCms.PlaceholdersAdmin','LblVisible','Visible','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '359','en-US','PigeonCms.Items','LblMoreInfo','more info','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '360','it-IT','PigeonCms.Items','LblMoreInfo','leggi tutto','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '361','en-US','PigeonCms.Items','LblPrint','Print','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '362','it-IT','PigeonCms.Items','LblPrint','Stampa','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '365','it-IT','PigeonCms.Placeholder','NameLabel','Nome','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '366','en-US','PigeonCms.Placeholder','NameLabel','Name','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '367','it-IT','PigeonCms.EmailContactForm','ShowPrivacyCheckLabel','Visualizza casella privacy','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '368','en-US','PigeonCms.EmailContactForm','ShowPrivacyCheckLabel','Show privacy check','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '369','it-IT','PigeonCms.EmailContactForm','PrivacyTextLabel','Informativa privacy','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '370','en-US','PigeonCms.EmailContactForm','PrivacyTextLabel','Privacy text','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '371','it-IT','PigeonCms.TopMenu','MenuTypeLabel','Tipo menu','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '372','en-US','PigeonCms.TopMenu','MenuTypeLabel','Menu type','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '373','en-US','PigeonCms.EmailContactForm','LblCaptchaText','enter the code shown','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '374','it-IT','PigeonCms.EmailContactForm','LblCaptchaText','inserisci il codice visualizzato','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '375','it-IT','PigeonCms.FileUpload','all','tutti','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '376','en-US','PigeonCms.FileUpload','all','all','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '377','it-IT','PigeonCms.FileUpload','Allowedfiles','Files consentiti','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '378','en-US','PigeonCms.FileUpload','Allowedfiles','Allowed files','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '379','it-IT','PigeonCms.FileUpload','MaxSize','Dimensione massima','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '380','en-US','PigeonCms.FileUpload','MaxSize','Max size','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '381','it-IT','PigeonCms.FilesManager','Select','Seleziona','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '382','en-US','PigeonCms.FilesManager','Select','Select','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '383','it-IT','PigeonCms.FilesManager','Preview','Anteprima','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '384','en-US','PigeonCms.FilesManager','Preview','Preview','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '385','en-US','PigeonCms.ContentEditorControl','ReadMore','Read more',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '386','it-IT','PigeonCms.ContentEditorControl','ReadMore','Leggi tutto',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '387','en-US','PigeonCms.ContentEditorControl','Pagebreak','Page break','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '388','it-IT','PigeonCms.ContentEditorControl','Pagebreak','Salto pagina','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '389','en-US','PigeonCms.ContentEditorControl','File','File',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '390','it-IT','PigeonCms.ContentEditorControl','File','File',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '392','en-US','PigeonCms.FilesUpload','Preview','Preview',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '393','it-IT','PigeonCms.FilesUpload','Preview','Anteprima',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '394','en-US','PigeonCms.FilesUpload','Select','Select',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '395','it-IT','PigeonCms.FilesUpload','Select','Seleziona',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '402','en-US','PigeonCms.SectionsAdmin','LblTitle','Title',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '403','it-IT','PigeonCms.SectionsAdmin','LblTitle','Titolo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '404','en-US','PigeonCms.SectionsAdmin','LblDescription','Description','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '405','it-IT','PigeonCms.SectionsAdmin','LblDescription','Descrizione','','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '406','en-US','PigeonCms.SectionsAdmin','LblEnabled','Enabled',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '407','it-IT','PigeonCms.SectionsAdmin','LblEnabled','Abilitato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '408','en-US','PigeonCms.SectionsAdmin','LblLimits','Limits',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '409','it-IT','PigeonCms.SectionsAdmin','LblLimits','Limiti',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '410','en-US','PigeonCms.SectionsAdmin','LblMaxItems','Max items',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '411','it-IT','PigeonCms.SectionsAdmin','LblMaxItems','Numero max items',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '412','en-US','PigeonCms.SectionsAdmin','LblMaxAttachSizeKB','Max size for attachments (KB)',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '413','it-IT','PigeonCms.SectionsAdmin','LblMaxAttachSizeKB','Dimensione massima allegati (KB)',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '414','it-IT','PigeonCms.MemberEditorControl','LblPasswordControl','Ripeti password',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '415','it-IT','PigeonCms.MemberEditorControl','LblOldPassword','Vecchia password',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '416','it-IT','PigeonCms.MemberEditorControl','LblEnabled','Abilitato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '417','it-IT','PigeonCms.MemberEditorControl','LblComment','Note',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '418','it-IT','PigeonCms.MemberEditorControl','LblSex','Sesso',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '419','it-IT','PigeonCms.MemberEditorControl','LblCompanyName','Nome azienda',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '420','it-IT','PigeonCms.MemberEditorControl','LblVat','Partita IVA',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '421','it-IT','PigeonCms.MemberEditorControl','LblSsn','Codice fiscale',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '422','it-IT','PigeonCms.MemberEditorControl','LblFirstName','Nome',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '423','it-IT','PigeonCms.MemberEditorControl','LblSecondName','Cognome',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '424','it-IT','PigeonCms.MemberEditorControl','LblAddress1','Indirizzo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '425','it-IT','PigeonCms.MemberEditorControl','LblAddress2','Indirizzo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '426','it-IT','PigeonCms.MemberEditorControl','LblCity','Citt&agrave;',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '427','it-IT','PigeonCms.MemberEditorControl','LblState','Provincia',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '428','it-IT','PigeonCms.MemberEditorControl','LblZipCode','Cap',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '429','it-IT','PigeonCms.MemberEditorControl','LblNation','Nazione',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '430','it-IT','PigeonCms.MemberEditorControl','LblTel1','Telefono',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '431','it-IT','PigeonCms.MemberEditorControl','LblMobile1','Cellulare',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '432','it-IT','PigeonCms.MemberEditorControl','LblWebsite1','Sito web',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '433','it-IT','PigeonCms.MemberEditorControl','LblPasswordNotMatching','le password non corrispondono',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '434','it-IT','PigeonCms.MemberEditorControl','LblInvalidEmail','email non valida',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '435','it-IT','PigeonCms.MemberEditorControl','LblInvalidPassword','password non valida',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '458','en-US','PigeonCms.MemberEditorControl','LblPasswordControl','Repeat password',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '459','en-US','PigeonCms.MemberEditorControl','LblOldPassword','Old password',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '460','en-US','PigeonCms.MemberEditorControl','LblEnabled','Enabled',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '461','en-US','PigeonCms.MemberEditorControl','LblComment','Notes',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '462','en-US','PigeonCms.MemberEditorControl','LblSex','Gender',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '463','en-US','PigeonCms.MemberEditorControl','LblCompanyName','Company name',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '464','en-US','PigeonCms.MemberEditorControl','LblVat','Vat',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '465','en-US','PigeonCms.MemberEditorControl','LblSsn','Ssn',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '466','en-US','PigeonCms.MemberEditorControl','LblFirstName','First name',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '467','en-US','PigeonCms.MemberEditorControl','LblSecondName','Second name',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '468','en-US','PigeonCms.MemberEditorControl','LblAddress1','Address',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '469','en-US','PigeonCms.MemberEditorControl','LblAddress2','Address',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '470','en-US','PigeonCms.MemberEditorControl','LblCity','Citty;',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '471','en-US','PigeonCms.MemberEditorControl','LblState','State',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '472','en-US','PigeonCms.MemberEditorControl','LblZipCode','Zip',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '473','en-US','PigeonCms.MemberEditorControl','LblNation','Nation',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '474','en-US','PigeonCms.MemberEditorControl','LblTel1','Telephone',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '475','en-US','PigeonCms.MemberEditorControl','LblMobile1','Mobile phone',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '476','en-US','PigeonCms.MemberEditorControl','LblWebsite1','Website',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '477','en-US','PigeonCms.MemberEditorControl','LblPasswordNotMatching','passwords do not match',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '478','en-US','PigeonCms.MemberEditorControl','LblInvalidEmail','invalid email',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '479','en-US','PigeonCms.MemberEditorControl','LblInvalidPassword','invalid password',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '480','en-US','PigeonCms.MemberEditorControl','LblManadatoryFields','please fill mandatory fields',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '481','it-IT','PigeonCms.MemberEditorControl','LblManadatoryFields','compila i campi obbligatori',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '482','en-US','PigeonCms.MemberEditorControl','LblCaptchaText','enter the code shows',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '483','it-IT','PigeonCms.MemberEditorControl','LblCaptchaText','inserisci il codice visualizzato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '484','en-US','PigeonCms.MembersAdmin','LblCaptchaText','enter the code shown',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '485','it-IT','PigeonCms.MembersAdmin','LblCaptchaText','inserisci il codice visualizzato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '486','en-US','DroidCatalogue.Orders','LblFillCompanyOrName','please fill company name or your full name',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '487','it-IT','DroidCatalogue.Orders','LblFillCompanyOrName','inserisci il nome azienda o il tuo nome completo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '488','en-US','DroidCatalogue.Orders','LblFillAddress','please fill address field',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '489','it-IT','DroidCatalogue.Orders','LblFillAddress','inserisci il campo indirizzo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '490','en-US','DroidCatalogue.Orders','LblFillCity','please fill city field',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '491','it-IT','DroidCatalogue.Orders','LblFillCity','inserisci il campo città',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '492','en-US','DroidCatalogue.Orders','LblFillState','please fill state field',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '493','it-IT','DroidCatalogue.Orders','LblFillState','inserisci il campo provincia',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '494','en-US','DroidCatalogue.Orders','LblFillZip','please fill zip field',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '495','it-IT','DroidCatalogue.Orders','LblFillZip','inserisci il campo Cap',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '496','en-US','DroidCatalogue.Orders','LblFillVatOrSsn','please fill Vat or Ssn field',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '497','it-IT','DroidCatalogue.Orders','LblFillVatOrSsn','inserisci Partita IVA o codice fiscale',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '498','en-US','DroidCatalogue.Orders','LblItems','items',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '499','it-IT','DroidCatalogue.Orders','LblItems','articoli',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '500','en-US','DroidCatalogue.Orders','LblDiskSpace','disk space',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '501','it-IT','DroidCatalogue.Orders','LblDiskSpace','spazio su disco',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '502','en-US','DroidCatalogue.Orders','LblClients','clients',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '503','it-IT','DroidCatalogue.Orders','LblClients','attivazioni',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '504','en-US','DroidCatalogue.Orders','LblSetup','setup',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '505','it-IT','DroidCatalogue.Orders','LblSetup','setup',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '506','en-US','DroidCatalogue.Orders','LblFreeOnline','free online',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '507','it-IT','DroidCatalogue.Orders','LblFreeOnline','gratis online',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '508','en-US','DroidCatalogue.Orders','LblAds','ads',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '509','it-IT','DroidCatalogue.Orders','LblAds','pubblicit&agrave;',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '510','en-US','DroidCatalogue.Orders','LblYes','yes',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '511','it-IT','DroidCatalogue.Orders','LblYes','s&igrave;',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '512','en-US','DroidCatalogue.Orders','LblNoAds','no ads',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '513','it-IT','DroidCatalogue.Orders','LblNoAds','nessuna pubblicit&agrave',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '514','en-US','DroidCatalogue.Orders','LblMonthlyFee','monthly fee',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '515','it-IT','DroidCatalogue.Orders','LblMonthlyFee','canone mensile',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '516','en-US','DroidCatalogue.Orders','LblFree','free',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '517','it-IT','DroidCatalogue.Orders','LblFree','gratis',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '518','en-US','DroidCatalogue.Orders','LblUnlimited','unlimited',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '519','it-IT','DroidCatalogue.Orders','LblUnlimited','illimitate',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '520','en-US','DroidCatalogue.Orders','LblChoosePlan','Choose plan',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '521','it-IT','DroidCatalogue.Orders','LblChoosePlan','Scelta piano',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '522','en-US','DroidCatalogue.Orders','LblCheckout','Checkout',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '523','it-IT','DroidCatalogue.Orders','LblCheckout','Conferma',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '524','en-US','DroidCatalogue.Orders','LblFinish','Finish',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '525','it-IT','DroidCatalogue.Orders','LblFinish','Fine',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '526','en-US','DroidCatalogue.Orders','LblOrderSummary','Order summary',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '527','it-IT','DroidCatalogue.Orders','LblOrderSummary','Riepilogo ordine',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '528','en-US','DroidCatalogue.Orders','LblBillingSummary','User and billing summary',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '529','it-IT','DroidCatalogue.Orders','LblBillingSummary','Riepilogo dati utente e pagamento',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '530','en-US','PigeonCms.ItemsAdmin','ChooseSection','Choose a section before',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '531','it-IT','PigeonCms.ItemsAdmin','ChooseSection','Scegli una sezione',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '532','en-US','PigeonCms.ItemsAdmin','ChooseCategory','Choose a category before',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '533','it-IT','PigeonCms.ItemsAdmin','ChooseCategory','Scegli una categoria',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '534','it-IT','PigeonCms.ItemsAdmin','NewTicket','Nuovo ticket',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '535','it-IT','PigeonCms.ItemsAdmin','PriorityFilter','--priorità--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '536','it-IT','PigeonCms.ItemsAdmin','Close','chiudi',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '537','it-IT','PigeonCms.ItemsAdmin','Reopen','riapri',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '538','it-IT','PigeonCms.ItemsAdmin','Lock','blocca',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '539','it-IT','PigeonCms.ItemsAdmin','Actions','--azioni--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '540','it-IT','PigeonCms.ItemsAdmin','StatusFilter','--stato--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '541','it-IT','PigeonCms.ItemsAdmin','CategoryFilter','--categoria--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '542','it-IT','PigeonCms.ItemsAdmin','Always','tutto',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '543','it-IT','PigeonCms.ItemsAdmin','Today','oggi',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '544','it-IT','PigeonCms.ItemsAdmin','Last week','ultima settimana',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '545','it-IT','PigeonCms.ItemsAdmin','Last month','ultimo mese',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '546','it-IT','PigeonCms.ItemsAdmin','Subject','Oggetto',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '547','it-IT','PigeonCms.ItemsAdmin','Reply','Rispondi',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '548','it-IT','PigeonCms.ItemsAdmin','BackToList','Torna alla lista',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '549','it-IT','PigeonCms.ItemsAdmin','MyTickets','solo mie',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '550','it-IT','PigeonCms.ItemsAdmin','AssignTo','--assegna--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '551','it-IT','PigeonCms.ItemsAdmin','Status','Stato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '552','it-IT','PigeonCms.ItemsAdmin','AssignedTo','Assegnato a',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '553','it-IT','PigeonCms.ItemsAdmin','Low','Bassa',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '554','it-IT','PigeonCms.ItemsAdmin','Medium','Media',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '555','it-IT','PigeonCms.ItemsAdmin','High','Alta',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '556','it-IT','PigeonCms.ItemsAdmin','Open','Aperto',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '557','it-IT','PigeonCms.ItemsAdmin','WorkInProgress','In lavorazione',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '558','it-IT','PigeonCms.ItemsAdmin','Closed','Chiuso',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '559','it-IT','PigeonCms.ItemsAdmin','Locked','Bloccato',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '560','it-IT','PigeonCms.ItemsAdmin','Working','in lavorazione',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '561','it-IT','PigeonCms.ItemsAdmin','<subject>','<oggetto>',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '562','it-IT','PigeonCms.ItemsAdmin','Priority','Priorità',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '563','it-IT','PigeonCms.ItemsAdmin','Operator','Operatore',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '564','it-IT','PigeonCms.ItemsAdmin','DateInserted','Inserito il',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '565','it-IT','PigeonCms.ItemsAdmin','LastActivity','Ultima attività',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '566','it-IT','PigeonCms.ItemsAdmin','Text','Testo',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '567','it-IT','PigeonCms.ItemsAdmin','MessageTicketTitle','Ticket "[[ItemTitle]]" ([[ItemId]]) [[Extra]]','allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '568','it-IT','PigeonCms.ItemsAdmin','MessageTicketDescription','[[ItemDescription]]','allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '569','it-IT','PigeonCms.ItemsAdmin','CustomerFilter','--cliente--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '570','it-IT','PigeonCms.ItemsAdmin','Customer','Cliente',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '571','it-IT','PigeonCms.ItemsAdmin','Attachment','Allegati',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '572','it-IT','PigeonCms.ItemsAdmin','AttachFiles','Allega files',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '573','it-IT','PigeonCms.ItemsAdmin','SendEmail','Invia email',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '574','it-IT','PigeonCms.ItemsAdmin','OperatorFilter','--operatore--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '575','it-IT','PigeonCms.ItemsAdmin','UserInsertedFilter','--inserito da--',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '576','it-IT','PigeonCms.ItemsAdmin','SaveAndClose','Salva e chiudi ticket',NULL,'2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '577','it-IT','PigeonCms.LoginForm','','user','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '578','it-IT','PigeonCms.RoutesAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '579','it-IT','PigeonCms.MenuAdmin','Main','Main','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '580','it-IT','PigeonCms.MenuAdmin','Options','Options','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '581','it-IT','PigeonCms.MenuAdmin','Security','Security','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '582','it-IT','PigeonCms.MenuAdmin','Parameters','Parameters','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '583','it-IT','PigeonCms.MenuAdmin','LblAlias','Alias','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '584','it-IT','PigeonCms.MenuAdmin','LblRoute','Route','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '585','it-IT','PigeonCms.MenuAdmin','LblUseSsl','Use SSL','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '586','it-IT','PigeonCms.MenuAdmin','LblLink','Link','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '587','it-IT','PigeonCms.MenuAdmin','LblMasterpage','Masterpage','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '588','it-IT','PigeonCms.PermissionsControl','LblRead','Read','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '589','it-IT','PigeonCms.PermissionsControl','LblWrite','Write','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '590','it-IT','PigeonCms.PermissionsControl','LblPermissionId','ID','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '591','it-IT','PigeonCms.ModuleParams','LblSystemMessagesTo','System messages to','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '592','it-IT','PigeonCms.ModuleParams','LblDirectEditMode','Direct edit mode','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '593','it-IT','PigeonCms.MemberEditorControl','LblUsername','Username','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '594','it-IT','PigeonCms.MemberEditorControl','LblEmail','E-mail','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '595','it-IT','PigeonCms.MemberEditorControl','LblPassword','Password','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '596','it-IT','PigeonCms.MemberEditorControl','LblAllowMessages','Allow messages','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '597','it-IT','PigeonCms.MemberEditorControl','LblAllowEmails','Allow emails','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '598','it-IT','PigeonCms.MemberEditorControl','LblAccessCode','Access code','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '599','it-IT','PigeonCms.MemberEditorControl','LblAccessLevel','Access level','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '600','it-IT','PigeonCms.MembersAdmin','LblFilters','Filters','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '601','it-IT','PigeonCms.ModulesAdmin','Main','Main','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '602','it-IT','PigeonCms.ModulesAdmin','Menu','Menu','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '603','it-IT','PigeonCms.ModulesAdmin','Options','Options','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '604','it-IT','PigeonCms.ModulesAdmin','Security','Security','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '605','it-IT','PigeonCms.ModulesAdmin','Parameters','Parameters','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '606','it-IT','PigeonCms.ModulesAdmin','LblChange','change','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '607','it-IT','PigeonCms.ModulesAdmin','LblRecordId','ID','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '608','it-IT','PigeonCms.MembersAdmin','LblUpdateUser','Update user','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '609','it-IT','PigeonCms.FileUpload','Folder','Folder','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '610','it-IT','PigeonCms.UpdatesAdmin','LblInstall','Install','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '611','it-IT','PigeonCms.UpdatesAdmin','LblModules','Modules','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '612','it-IT','PigeonCms.UpdatesAdmin','LblTemplates','Templates','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '613','it-IT','PigeonCms.UpdatesAdmin','LblSql','Sql','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '614','it-IT','PigeonCms.LogsAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '615','it-IT','PigeonCms.LogsAdmin','LblRecordId','ID','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '616','it-IT','PigeonCms.LogsAdmin','LblCreated','Created','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '617','it-IT','PigeonCms.LogsAdmin','LblType','Type','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '618','it-IT','PigeonCms.LogsAdmin','LblModuleType','Module','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '619','it-IT','PigeonCms.LogsAdmin','LblView','View','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '620','it-IT','PigeonCms.LogsAdmin','LblIp','Ip','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '621','it-IT','PigeonCms.LogsAdmin','LblSessionId','Session ID','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '622','it-IT','PigeonCms.LogsAdmin','LblUser','User','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '623','it-IT','PigeonCms.LogsAdmin','LblUrl','Url','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '624','it-IT','PigeonCms.LogsAdmin','LblDescription','Description','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '625','it-IT','PigeonCms.OfflineAdmin','LblPageTemplate','Page template','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '626','it-IT','PigeonCms.AttributesAdmin','Name','Name','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '627','it-IT','PigeonCms.AttributesAdmin','FieldType','Field type','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '628','it-IT','PigeonCms.AttributesAdmin','MinValue','Min value','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '629','it-IT','PigeonCms.AttributesAdmin','MaxValue','Max value','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '630','it-IT','PigeonCms.AttributesAdmin','Enabled','Enabled','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '631','it-IT','PigeonCms.AttributesAdmin','AttributeValues','Attribute values','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '632','it-IT','PigeonCms.AttributesAdmin','Value','Value','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '633','it-IT','PigeonCms.AttributesAdmin','LblRecordInfo','Record info','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '634','it-IT','PigeonCms.AttributesAdmin','LblRecordId','ID','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '635','it-IT','PigeonCms.AttributesAdmin','LblCreated','Created','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '636','it-IT','PigeonCms.AttributesAdmin','LblLastUpdate','Last update','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '637','it-IT','PigeonCms.LoginForm','user','user','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '638','it-IT','PigeonCms.LoginForm','password','password','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '639','it-IT','PigeonCms.LoginForm','Username','Username','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '640','it-IT','PigeonCms.MembersAdmin','LblChangePassword','Change password','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '641','it-IT','PigeonCms.SectionsAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '642','it-IT','PigeonCms.SectionsAdmin','Main','Main','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '643','it-IT','PigeonCms.SectionsAdmin','Security','Security','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '644','it-IT','PigeonCms.SectionsAdmin','LblCssClass','Css class','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '645','it-IT','PigeonCms.CategoriesAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '646','it-IT','PigeonCms.CategoriesAdmin','LblSection','Section','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '647','it-IT','PigeonCms.CategoriesAdmin','LblCssClass','Css class','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '648','it-IT','PigeonCms.CategoriesAdmin','LblEnabled','Enabled','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '649','it-IT','PigeonCms.CategoriesAdmin','LblTitle','Title','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '650','it-IT','PigeonCms.CategoriesAdmin','LblDescription','Description','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '651','it-IT','PigeonCms.StaticPagesAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '652','it-IT','PigeonCms.ItemsAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '653','it-IT','PigeonCms.ItemsAdmin','Main','Main','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '654','it-IT','PigeonCms.ItemsAdmin','Security','Security','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '655','it-IT','PigeonCms.ItemsAdmin','Parameters','Parameters','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '656','it-IT','PigeonCms.ItemsAdmin','LblAlias','Alias','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '657','it-IT','PigeonCms.ItemsAdmin','LblCssClass','Css class','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '658','it-IT','PigeonCms.ItemsAdmin','LblRecordId','ID','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '659','it-IT','PigeonCms.ItemsAdmin','LblOrderId','Order Id','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '660','it-IT','PigeonCms.LabelsAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '661','it-IT','PigeonCms.LabelsAdmin','LblResourceSet','Resource set','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '662','it-IT','PigeonCms.LabelsAdmin','LblResourceId','Resource id','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '663','it-IT','PigeonCms.LabelsAdmin','LblTextMode','Text mode','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '664','it-IT','PigeonCms.LabelsAdmin','LblValue','Value','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '665','it-IT','PigeonCms.LabelsAdmin','LblComment','Comment','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '666','it-IT','PigeonCms.FilesManager','Size','Size','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '667','it-IT','PigeonCms.FilesManager','Meta','Meta data','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '668','it-IT','PigeonCms.PlaceholdersAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '669','it-IT','PigeonCms.UpdatesAdmin','LblFilters','filters','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '670','it-IT','PigeonCms.AppSettingsAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '671','it-IT','PigeonCms.AppSettingsAdmin','LblName','Name','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '672','it-IT','PigeonCms.AppSettingsAdmin','LblTitle','Title','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '673','it-IT','PigeonCms.AppSettingsAdmin','LblValue','Value','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '674','it-IT','PigeonCms.AppSettingsAdmin','LblInfo','Additional info','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '675','it-IT','PigeonCms.LoginForm','LblInvalidLogin','Invalid username or password.','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '676','it-IT','PigeonCms.MembersAdmin','LblNewUser','New user','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '677','it-IT','PigeonCms.MembersAdmin','LblChangeRoles','Change roles','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '678','it-IT','PigeonCms.MemberEditorControl','LblRoles','Roles','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '679','it-IT','PigeonCms.MemberEditorControl','LblRolesInUser','User roles','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '680','it-IT','PigeonCms.MembersAdmin','LblDetails','Details','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '681','it-IT','PigeonCms.RolesAdmin','LblNewRole','New role','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '682','it-IT','PigeonCms.RolesAdmin','LblUsersInRole','Users in role','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '683','it-IT','PigeonCms.RolesAdmin','InsRoleName','Insert role name','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '684','it-IT','PigeonCms.TemplateBlocksAdmin','Name','Name','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '685','it-IT','PigeonCms.TemplateBlocksAdmin','Title','Title','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '686','it-IT','PigeonCms.TemplateBlocksAdmin','Enabled','Enabled','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '687','it-IT','PigeonCms.CulturesAdmin','CultureCode','Culture code','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '688','it-IT','PigeonCms.CulturesAdmin','DisaplyName','Display name','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '689','it-IT','PigeonCms.CulturesAdmin','Enabled','Enabled','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '690','it-IT','PigeonCms.WebConfigAdmin','Key','Key','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '691','it-IT','PigeonCms.WebConfigAdmin','Value','Value','SYSTEM','2',1,NULL)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '692','it-IT','AQ_default','Sample5','Label value calling method in code behind','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '693','it-IT','AQ_menu','Home','Labels','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '694','it-IT','AQ_menu','Images','Images','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '695','it-IT','AQ_menu','Cache','Cache','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '696','it-IT','AQ_menu','List','List items','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '697','it-IT','AQ_menu','PrivateArea','Private area','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '698','it-IT','AQ_default','Title','Labels resources','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '699','it-IT','AQ_default','Sample1','Label value calling method in html','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '700','it-IT','AQ_default','Sample2','
            Label using server control with TextMode="Text"
        ','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '701','it-IT','AQ_default','Sample3','
            Label using server control with TextMode="BasicHtml" <em>(simple editor)</em>
        ','SYSTEM','1',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '702','it-IT','AQ_default','Sample4','
            Label using server control with TextMode="Html" <em>(advanced editor)</em><br />
            This is a <a href="#">link</a>
        ','SYSTEM','0',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '703','it-IT','PigeonCms.ItemsAdmin','Related','Related','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '704','it-IT','PigeonCms.ItemsAdmin','Attributes','Attributes','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '705','it-IT','PigeonCms.ItemsAdmin','Variants','Variants','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '706','it-IT','PigeonCms.AttributesAdmin','LblCustomVlaue','Valore Custom','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '707','it-IT','PigeonCms.AttributesAdmin','LblMeasureUnit','Unità di misura','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '708','it-IT','PigeonCms.CouponsAdmin','LblDetails','Details','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '709','it-IT','PigeonCms.CouponsAdmin','LblTxtCode','Codice Coupon','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '710','it-IT','PigeonCms.CouponsAdmin','LblEnabled','Enabled','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '711','it-IT','PigeonCms.CouponsAdmin','LblAmount','Importo','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '712','it-IT','PigeonCms.CouponsAdmin','LblIsPercentage','Valore in Percentuale','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '713','it-IT','PigeonCms.CouponsAdmin','LblMinOrderAmount','Importo Minimo','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '714','it-IT','PigeonCms.CouponsAdmin','LblValidFrom','Valid from','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '715','it-IT','PigeonCms.CouponsAdmin','LblValidTo','Valid to','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '716','it-IT','PigeonCms.CouponsAdmin','LblMaxUses','Numero d''usi','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '717','it-IT','PigeonCms.AttributesAdmin','LblDetails','Details','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '718','it-IT','PigeonCms.AttributesAdmin','LblAllowCustomValue','Use Custom Values','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '719','it-IT','PigeonCms.AttributesAdmin','LblItemType','Select ItemType','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '720','it-IT','PigeonCms.AttributesAdmin','LblName','Name','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '721','it-IT','AQ_cache','Title','Cache example','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '722','it-IT','AQ_list','Title','Items list example','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '723','it-IT','AQ_images','Title','Images resources','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '724','it-IT','AQ_images','ImageRoadRunner','/assets/img/roadrunner.gif','SYSTEM','3',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '725','it-IT','AQ_images','ImageCoyote','/assets/img/coyote.jpg','SYSTEM','3',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '726','it-IT','AQ_images','ImageTnt','/assets/img/tnt.png','SYSTEM','3',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '727','it-IT','AQ_private','Title','Private area','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '728','it-IT','AQ_login','Title','Login','SYSTEM','2',1,'')
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized,resourceParams)  VALUES( '729','it-IT','PigeonCms.Debug','debug-test','debug-test-value','SYSTEM','2',1,'')
GO
SET IDENTITY_INSERT #__labels OFF
GO
SET IDENTITY_INSERT #__memberUsers ON
GO
INSERT INTO #__memberUsers (id,username,applicationName,email,comment,password,passwordQuestion,passwordAnswer,isApproved,lastActivityDate,lastLoginDate,lastPasswordChangedDate,creationDate,isOnLine,isLockedOut,lastLockedOutDate,failedPasswordAttemptCount,failedPasswordAttemptWindowStart,failedPasswordAnswerAttemptCount,failedPasswordAnswerAttemptWindowStart,enabled,accessCode,accessLevel,isCore,sex,companyName,vat,ssn,firstName,secondName,address1,address2,city,state,zipCode,nation,tel1,mobile1,website1,allowMessages,allowEmails,validationCode)  VALUES( '1','admin','PigeonCms','','','admin','','',1,NULL,NULL,NULL,NULL,NULL,0,NULL,'0',NULL,'0',NULL,1,'','0',1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,0,'')
GO
INSERT INTO #__memberUsers (id,username,applicationName,email,comment,password,passwordQuestion,passwordAnswer,isApproved,lastActivityDate,lastLoginDate,lastPasswordChangedDate,creationDate,isOnLine,isLockedOut,lastLockedOutDate,failedPasswordAttemptCount,failedPasswordAttemptWindowStart,failedPasswordAnswerAttemptCount,failedPasswordAnswerAttemptWindowStart,enabled,accessCode,accessLevel,isCore,sex,companyName,vat,ssn,firstName,secondName,address1,address2,city,state,zipCode,nation,tel1,mobile1,website1,allowMessages,allowEmails,validationCode)  VALUES( '2','manager','PigeonCms','','','manager','','',1,NULL,NULL,NULL,NULL,NULL,0,NULL,'0',NULL,'0',NULL,1,'','0',0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,0,'')
GO
SET IDENTITY_INSERT #__memberUsers OFF
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '2','mainmenu','Login','login','','0',1,'0','4','10','0',0,'0','SbAdminBlank','','fa fa-key fa-fw',1,'1','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '3','mainmenu','Default page','default','','0',1,'0','5','2','0',0,'0','','','fa fa-files-o fa-fw',1,'1','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '4','mainmenu','Page not found','pageNotFound','','0',1,'0','6','51','0',1,'0','','','',0,'1','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '31','adminmenu','Site','','#','1',1,'0','0','23','1',0,'0','','','fa fa-wrench fa-fw',1,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '32','adminmenu','sections admin','sections','','0',1,'33','54','25','1',1,'0','','','',1,'6','20','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '33','adminmenu','Contents','','#','1',1,'0','0','24','1',1,'0','','','fa fa-edit fa-fw',1,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '34','adminmenu','menu admin','menu','','0',1,'36','55','30','1',1,'0','','','',1,'6','27','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '35','adminmenu','Preview','','~/','1',1,'31','0','28','1',0,'0','','','',1,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '36','adminmenu','Menu','','#','1',1,'0','0','31','1',1,'0','','','fa fa-sitemap fa-fw',1,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '37','adminmenu','modules admin','modules','','0',1,'36','56','27','1',1,'0','','','',1,'6','25','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '38','adminmenu','menutypes admin','menutypes','','0',1,'36','57','29','1',1,'0','','','',1,'6','26','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '39','adminmenu','categories admin','categories','','0',1,'33','58','26','1',1,'0','','','',1,'6','21','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '40','adminmenu','items admin','items','','0',1,'33','59','32','1',1,'0','','','',1,'6','22','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '41','adminmenu','static pages','staticpages','','0',1,'33','60','33','1',1,'0','','','',1,'6','23','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '42','adminmenu','placeholders admin','placeholders','','0',1,'33','61','34','1',1,'0','','','',1,'6','24','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '43','adminmenu','logout','','~/Default.aspx?act=logout','1',1,'31','0','49','1',1,'0','','','',1,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '44','adminmenu','members admin','members','','0',1,'31','62','35','1',1,'0','','','',1,'6','12','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '45','adminmenu','roles admin','roles','','0',1,'31','63','36','1',1,'0','','','',1,'6','11','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '46','adminmenu','templateblocks admin','templateblocks','','0',1,'31','64','37','1',1,'0','','','',1,'6','13','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '47','adminmenu','routes admin','routes','','0',1,'31','65','38','1',1,'0','','','',1,'6','14','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '48','adminmenu','cultures admin','cultures','','0',1,'31','66','39','1',1,'0','','','',1,'6','15','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '49','adminmenu','appsettings admin','appsettings','','0',1,'31','67','40','1',1,'0','','','',1,'6','16','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '50','adminmenu','webconfig admin','webconfig','','0',1,'31','68','41','1',1,'0','','','',1,'6','17','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '51','adminmenu','updates admin','updates','','0',1,'31','69','42','1',1,'0','','','',1,'6','18','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '57','adminmenu','labels admin','labels','','0',1,'33','74','52','1',1,'0','','','',1,'6','19','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '58','adminmenu','logs','logs','','0',1,'31','75','43','1',1,'0','','','',1,'6','29','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '59','adminmenu','offline','offline','','0',1,'31','76','48','1',1,'0','','','',1,'6','30','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '60','mainmenu','Offline admin','offlineadmin','','3',1,'0','0','55','0',1,'2','','','',0,'1','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '61','adminPopups','items images','images-upload','','0',1,'0','78','53','1',0,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '62','adminPopups','Items files','files-uploads','','0',1,'0','79','54','1',0,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '65','adminPopups','labels admin popup','labels-admin-popup','','0',1,'0','83','57','1',1,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '66','adminPopups','PigeonCms-StaticPageAdmin-popup','pigeoncms-staticpageadmin-popup','','0',1,'0','84','58','1',1,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '67','adminPopups','PigeonCms-ItemsAdmin-popup','pigeoncms-itemsadmin-popup','','0',1,'0','85','59','1',1,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '68','adminPopups','PigeonCms-PlaceholdersAdmin-popup','pigeoncms-placeholdersadmin-popup','','0',1,'0','86','60','1',1,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '69','adminmenu','Help','','http://www.pigeoncms.com/docs/admin-area.aspx','1',1,'0','0','61','1',1,'0','','','',0,'1','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '70','mainmenu','Error page','error','','0',1,'0','87','62','0',0,'0','','','',0,'1','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '71','adminPopups','documents upload','docs-upload','','0',1,'0','89','63','1',0,'0','SbAdminBlank','','',0,'6','0','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '72','adminmenu','attributes admin','attributes-admin','','0',0,'33','90','64','1',0,'0','','','',0,'6','34','','0',1,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '73','adminmenu','Shop','','#','1',1,'0','0','65','1',1,'0','','','fa fa-shopping-cart fa-fw',1,'6','35','','0',0,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '74','adminmenu','Products','products','','0',1,'73','91','66','1',0,'0','','','',1,'6','36','','0',0,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '75','adminmenu','Products attributes','products-attributes','','0',1,'73','92','67','1',0,'0','','','',1,'6','37','','0',0,'0','0','','0','2')
GO
INSERT INTO #__menu (id,menuType,name,alias,link,contentType,published,parentId,moduleId,ordering,accessType,overridePageTitle,referMenuId,currMasterPage,currTheme,cssClass,visible,routeId,permissionId,accessCode,accessLevel,isCore,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,useSsl)  VALUES( '76','adminmenu','Coupons','coupons','','0',1,'73','93','68','1',0,'0','','','',1,'1','38','','0',0,'0','0','','0','2')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','2','Login','Login')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','3','Homepage','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','4','Page not found','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','31','Site','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','32','Sections','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','33','Contents','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','34','Menu entries','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','35','Preview',NULL)
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','36','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','37','Modules','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','38','Menu types','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','39','Categories','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','40','Items','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','41','Static pages','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','42','Placeholders','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','43','Logout',NULL)
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','44','Members','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','45','Roles','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','46','Template blocks ','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','47','Routing','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','48','Cultures','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','49','Settings','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','50','Web.config','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','51','Updates','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','57','Labels Pigeon','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','58','Logs','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','59','Site offline','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','60','Offline admin access','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','61','Items Images upload','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','62','Items attachements upload','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','65','Labels','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','66','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','67','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','68','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','69','Help','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','70','Error page','Error page')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','71','Files upload','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','72','Form attributes','attributes admin')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','73','Shop','Shop')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','74','Products','Products')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','75','Products attributes','Products attributes')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','76','Coupons','Coupons')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','2','Login','Login')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','3','Homepage','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','4','Pagina non trovata','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','31','Sito','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','32','Sezioni','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','33','Contenuti','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','34','Voci menu','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','35','Anteprima',NULL)
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','36','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','37','Moduli','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','38','Tipi menu','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','39','Categorie','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','40','Elementi','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','41','Pagine statiche','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','42','Segnaposto','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','43','Disconnetti',NULL)
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','44','Utenti','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','45','Ruoli utente','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','46','Blocchi template','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','47','Routing','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','48','Lingue','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','49','Impostazioni','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','50','Web.config','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','51','Aggiornamenti','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','57','Etichette Pigeon','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','58','Logs','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','59','Sito offline','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','60','Accesso offline admin','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','61','Upload immagini elementi','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','62','Upload allegati elementi','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','65','Gestione etichette','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','66','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','67','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','68','','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','69','Guida','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','70','Error page','Error page')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','71','Caricamento files','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','72','Attributi form','attributes admin')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','73','Shop','Shop')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','74','Prodotti','Prodotti')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','75','Attributi prodotti','Attributi prodotti')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','76','Coupons','Coupons')
GO
SET IDENTITY_INSERT #__menuTypes ON
GO
INSERT INTO #__menuTypes (id,menuType,title,description)  VALUES( '1','mainmenu','Main menu','website main menu')
GO
INSERT INTO #__menuTypes (id,menuType,title,description)  VALUES( '2','adminmenu','Admin menu','website admin menu')
GO
INSERT INTO #__menuTypes (id,menuType,title,description)  VALUES( '3','adminPopups','admin','admin menu for popup functions')
GO
SET IDENTITY_INSERT #__menuTypes OFF
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '1','Debug','','1','Debug',1,'Debug','PigeonCms',NULL,'nicola',NULL,'admin','1',0,'HideText:=1',1,'0','Debug.ascx','31','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '2','Company address','','0','Top',1,'Placeholder','PigeonCms',NULL,'nicola',NULL,'admin','0',0,'Name:=CompanyAddress',0,'2','Placeholder.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '4','Admin','','0','content',1,'LoginForm','PigeonCms',NULL,'admin',NULL,'admin','0',0,'RedirectUrl:=~/admin/menu.aspx|',0,'3','LoginSbAdmin.ascx','0','','0','','','2','1',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '5','Default page','','0','content',1,'StaticPage','PigeonCms',NULL,'admin',NULL,'admin','0',0,'PageName:=home|',0,'3','StaticPage.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '6','Page not found','','0','content',1,'StaticPage','PigeonCms',NULL,'admin',NULL,'admin','0',0,'PageName:=pageNotFound|cache:=1',0,'3','StaticPage.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '7','Main menu','','1','Toolbar',1,'TopMenu','PigeonCms',NULL,'nicola',NULL,'admin','0',0,'MenuType:=mainmenu|MenuId:=listMenuRoot|ItemSelectedClass:=selected|ItemLastClass:=last|MenuLevel:=0|ShowChild:=1|',1,'2','SbAdminMenu.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '9','Google Analytics','','0','Footer',0,'Placeholder','PigeonCms',NULL,'nicola',NULL,'admin','0',0,'Name:=Analytics',0,'2','Placeholder.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '10','Site credits','','0','Footer',1,'Placeholder','PigeonCms',NULL,'nicola',NULL,'admin','0',0,'Name:=credits',0,'0','Placeholder.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '53','Admin menu','','1','Toolbar',1,'TopMenu','PigeonCms',NULL,'nicola',NULL,'admin','1',0,'MenuType:=adminmenu|MenuId:=listMenuRoot|ItemSelectedClass:=selected|ItemLastClass:=last|MenuLevel:=0|ShowChild:=1|',1,'2','SbAdminMenu.ascx','10','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '54','Sections admin','','0','content',1,'SectionsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetImagesUpload:=61|TargetFilesUpload:=62|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '55','Menu admin','','0','content',1,'MenuAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'AllowEditContentUrl:=1|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '56','Modules','','0','content',1,'ModulesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '57','menutypes','','0','content',1,'MenuTypesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '58','categories','','0','content',1,'CategoriesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetImagesUpload:=61|TargetFilesUpload:=62|SectionId:=0|MandatorySectionFilter:=0|ShowSecurity:=1|ShowOnlyDefaultCulture:=0|ShowEnabledFilter:=1|AllowOrdering:=1|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '59','items admin','','0','content',1,'ItemsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetImagesUpload:=61|TargetFilesUpload:=62|TargetDocsUpload:=71|SectionId:=0|CategoryId:=0|MandatorySectionFilter:=0|ItemType:=|ShowSecurity:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|AllowOrdering:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '60','static pages','','0','content',1,'StaticPagesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'PageName:=|TargetDocsUpload:=71',1,'3','Default.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '61','placeholders admin','','0','content',1,'PlaceholdersAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '62','members admin','','0','content',1,'MembersAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'MemberEditorMode:=0|LoginAfterCreate:=0|NeedApprovation:=0|NewRoleAsUser:=0|NewUserSuffix:=|DefaultRoles:=|DefaultAccessCode:=|DefaultAccessLevel:=|RedirectUrl:=|SendEmailNotificationToUser:=0|SendEmailNotificationToAdmin:=0|AdminNotificationEmail:=admin@yourdomain.com|NotificationEmailPageName:=|ShowFieldSex:=0|ShowFieldCompanyName:=0|ShowFieldVat:=0|ShowFieldSsn:=0|ShowFieldFirstName:=0|ShowFieldSecondName:=0|ShowFieldAddress1:=0|ShowFieldAddress2:=0|ShowFieldCity:=0|ShowFieldState:=0|ShowFieldZipCode:=0|ShowFieldNation:=0|ShowFieldTel1:=0|ShowFieldMobile1:=0|ShowFieldWebsite1:=0|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '63','roles admin','','0','content',1,'RolesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '64','templateblocks admin','','0','content',1,'TemplateBlocksAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '65','routes admin','','0','content',1,'RoutesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '66','cultures admin','','0','content',1,'CulturesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '67','appsettings admin','','0','content',1,'AppSettingsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '68','webconfig admin','','0','content',1,'WebConfigAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '69','updates admin','','0','content',1,'UpdatesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetLabelsPopup:=65',1,'3','Default.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '70','Immobilia Zone admin','','0','content',1,'ZoneAdmin','Immobilia',NULL,'nicola',NULL,'nicola','0',0,'',0,'3','Default.ascx','0','','0',NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '71','Immobilia categorie admin','','0','content',1,'CategorieAdmin','Immobilia',NULL,'nicola',NULL,'nicola','0',0,'',0,'3','Default.ascx','0','','0',NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '72','Immobilia immobili admin','','0','content',1,'ImmobiliAdmin','Immobilia',NULL,'nicola',NULL,'nicola','0',0,'',0,'3','Default.ascx','0','','0',NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '74','labels admin','','0','content',1,'LabelsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'ModuleFullName:=|ModuleFullNamePart:=|TargetImagesUpload:=0|DefaultResourceFolder:=~/public/res|AllowNew:=0|AllowDel:=0|AllowTextModeEdit:=0|AllowParamsEdit:=0|AllowAdminMode:=1|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '75','logs','','0','content',1,'LogsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '76','offline','','0','content',1,'OfflineAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '78','items images','','0','content',1,'FilesManager','PigeonCms',NULL,'admin',NULL,'admin','0',1,'AllowFilesUpload:=1|AllowFilesSelection:=0|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=0|AllowFoldersNavigation:=0|TypeParamRequired:=1|AllowTemporaryFiles:=0|FileExtensions:=jpg;jpeg;gif|FileSize:=300|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Gallery|ForcedFilename:=|UploadFields:=3|NumOfFilesAllowed:=0|ShowWorkingPath:=1|CustomWidth:=|CustomHeight:=|ContentBeforePage:=|ContentAfterPage:=|HeaderText:=|FooterText:=|SuccessText:=File uploaded|ErrorText:=Error uploading file(s)|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '79','Items files','','0','content',1,'FilesManager','PigeonCms',NULL,'admin',NULL,'admin','0',1,'AllowFilesUpload:=1|AllowFilesSelection:=0|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=0|AllowFoldersNavigation:=0|TypeParamRequired:=1|AllowTemporaryFiles:=0|FileExtensions:=pdf|FileSize:=500|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Files|ForcedFilename:=|UploadFields:=3|NumOfFilesAllowed:=0|ShowWorkingPath:=1|CustomWidth:=|CustomHeight:=|ContentBeforePage:=|ContentAfterPage:=|HeaderText:=|FooterText:=|SuccessText:=File uploaded|ErrorText:=Error uploading file(s)|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '82','top submenu','','0','Toolbar2',1,'TopMenu','PigeonCms',NULL,'admin',NULL,'admin','0',0,'MenuType:=|MenuId:=none|ItemSelectedClass:=selected|ItemLastClass:=last|ShowPagePostFix:=1|MenuLevel:=1|ShowChild:=0',0,'2','TopMenu.ascx','0','','0','','submenulist','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '83','labels admin popup','','0','content',1,'LabelsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'ModuleFullName:=|ModuleFullNamePart:=|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '84','static pages admin popup','','0','content',1,'StaticPagesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'PageName:=|TargetDocsUpload:=71|',1,'3','Default.ascx','0','','0','','','2','2',1,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '85','items admin popup','','0','content',1,'ItemsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetImagesUpload:=61|TargetFilesUpload:=62|TargetDocsUpload:=0|SectionId:=0|CategoryId:=0|MandatorySectionFilter:=0|ItemType:=|ShowSecurity:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|AllowOrdering:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|',1,'3','Default.ascx','0','','0','','','2','2',1,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '86','placeholders admin popup','','0','content',1,'PlaceholdersAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',1,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '87',NULL,'','1','content',1,'StaticPage','PigeonCms',NULL,'admin',NULL,'admin','0',0,'PageName:=error',0,'3','StaticPage.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '88',NULL,'','2','Pathway',1,'Breadcrumbs','PigeonCms',NULL,'admin',NULL,'admin','0',0,'MenuType:=mainmenu',0,'2','Breadcrumbs.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '89',NULL,'','3','content',1,'FilesManager','PigeonCms',NULL,'admin',NULL,'admin','0',1,'AllowFilesUpload:=1|AllowFilesSelection:=1|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=1|AllowFoldersNavigation:=1|TypeParamRequired:=0|AllowTemporaryFiles:=0|FileExtensions:=pdf;doc;docx;xls;xlsx;jpg;jpeg;gif;png;zip|FileSize:=1024|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Docs|ForcedFilename:=|UploadFields:=3|NumOfFilesAllowed:=0|ShowWorkingPath:=1|CustomWidth:=|CustomHeight:=|ContentBeforePage:=|ContentAfterPage:=|HeaderText:=|FooterText:=|SuccessText:=File(s) uploaded|ErrorText:=Error uploading file(s)|',0,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '90',NULL,'','4','content',1,'AttributesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',0,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '91',NULL,'','5','content',1,'ItemsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetImagesUpload:=0|TargetFilesUpload:=0|TargetDocsUpload:=0|SectionId:=0|CategoryId:=0|MandatorySectionFilter:=0|ItemType:=|ShowSecurity:=1|ShowFieldsPanel:=1|ShowParamsPanel:=1|ShowAlias:=1|ShowType:=1|ShowSectionColumn:=1|ShowEnabledFilter:=1|ShowDates:=1|AllowOrdering:=1|HtmlEditorType:=0|ShowEditorFileButton:=1|ShowEditorPageBreakButton:=1|ShowEditorReadMoreButton:=1|ShowOnlyDefaultCulture:=0|',0,'3','ShopProduct.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '92',NULL,'','6','content',1,'AttributesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',0,'3','default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '93',NULL,'','7','content',1,'CouponsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',0,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','1','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','4','Login')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','5','Homepage')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','7','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','53','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','54','Sections')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','55','Menu entries')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','56','Modules')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','57','Menu types')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','58','Categories')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','59','Items')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','60','Static pages')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','61','Placeholders')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','62','Members')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','63','Roles')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','64','Template blocks ')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','65','Routing')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','66','Cultures')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','67','Settings')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','68','Web.config')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','69','Updates')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','74','Labels Pigeon')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','75','Logs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','76','Site offline')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','78','Items Images upload')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','79','Items attachements upload')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','83','Labels')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','84','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','85','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','86','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','87','Error page')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','88','breadcrumbs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','89','Files upload')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','90','Form attributes')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','91','Products')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','92','Products attributes')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','93','Coupons')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','1','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','4','Login')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','5','Homepage')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','7','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','53','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','54','Sezioni')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','55','Voci menu')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','56','Moduli')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','57','Tipi menu')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','58','Categorie')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','59','Elementi')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','60','Pagine statiche')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','61','Segnaposto')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','62','Utenti')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','63','Ruoli utente')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','64','Blocchi template')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','65','Routing')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','66','Lingue')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','67','Impostazioni')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','68','Web.config')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','69','Aggiornamenti')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','74','Etichette Pigeon')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','75','Logs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','76','Sito offline')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','78','Upload immagini elementi')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','79','Upload allegati elementi')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','83','Gestione etichette')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','84','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','85','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','86','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','87','Error page')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','88','breadcrumbs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','89','Caricamento files')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','90','Attributi form')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','91','Prodotti')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','92','Attributi prodotti')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','93','Coupons')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '2','mainmenu')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '7','mainmenu')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '9','mainmenu')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '53','adminmenu')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '77','adminmenu')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '82','mainmenu')
GO
INSERT INTO #__modulesMenuTypes (moduleId,menuType)  VALUES( '88','mainmenu')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '11','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '12','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '13','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '14','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '15','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '16','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '17','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '18','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '19','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '19','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '20','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '20','backend')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '20','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '21','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '21','backend')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '21','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '22','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '22','backend')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '22','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '23','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '23','backend')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '23','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '24','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '24','backend')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '24','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '25','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '26','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '27','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '29','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '30','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '31','debug')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '32','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '32','backend')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '32','manager')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '33','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '34','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '35','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '36','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '37','admin')
GO
INSERT INTO #__permissions (id,rolename)  VALUES( '38','admin')
GO
INSERT INTO #__placeholders (name,content,visible)  VALUES( 'Analytics','<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src=''" + gaJsHost + "google-analytics.com/ga.js'' type=''text/javascript''%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try {
var pageTracker = _gat._getTracker("analytics code here");
pageTracker._trackPageview();
} catch(err) {}</script>',1)
GO
INSERT INTO #__placeholders (name,content,visible)  VALUES( 'CompanyAddress','your address 69, city, nation',1)
GO
INSERT INTO #__placeholders (name,content,visible)  VALUES( 'credits','powered by <a href="https://github.com/picce/pigeoncms" target="_blank">pigeoncms</a>',1)
GO
INSERT INTO #__roles (rolename,applicationName)  VALUES( 'admin','PigeonCms')
GO
INSERT INTO #__roles (rolename,applicationName)  VALUES( 'backend','PigeonCms')
GO
INSERT INTO #__roles (rolename,applicationName)  VALUES( 'debug','PigeonCms')
GO
INSERT INTO #__roles (rolename,applicationName)  VALUES( 'manager','PigeonCms')
GO
INSERT INTO #__roles (rolename,applicationName)  VALUES( 'public','PigeonCms')
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '1','pages','pages/{pagename}',1,'1',NULL,NULL,1,NULL)
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '2','catalog','catalog/{pagename}/{*pathinfo}',1,'2','','',0,0)
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '3','products list by category name','products/list/{pagename}/{*categoryname}',1,'4','','',0,NULL)
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '4','product detail by name','products/{pagename}/{itemname}/{*pathinfo}',1,'6','','',0,NULL)
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '5','product detail by id','products/{pagename}/id/{itemid}/{*pathinfo}',1,'5','','',0,NULL)
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '6','admin area','admin/{pagename}',1,'7','SbAdmin','SbAdmin',1,0)
GO
INSERT INTO #__routes (id,name,pattern,published,ordering,currMasterPage,currTheme,isCore,useSsl)  VALUES( '7','products list by category id','products/list/{pagename}/id/{*categoryid}',1,'3','','',0,NULL)
GO
INSERT INTO #__sections_Culture (cultureName,sectionId,title,description)  VALUES( 'en-US','1','News','news list
')
GO
INSERT INTO #__sections_Culture (cultureName,sectionId,title,description)  VALUES( 'it-IT','1','News','elenco news')
GO
INSERT INTO #__staticpages (pageName,visible,showPageTitle)  VALUES( 'error',1,1)
GO
INSERT INTO #__staticpages (pageName,visible,showPageTitle)  VALUES( 'home',1,1)
GO
INSERT INTO #__staticpages (pageName,visible,showPageTitle)  VALUES( 'pageNotFound',1,1)
GO
INSERT INTO #__staticPages_Culture (cultureName,pageName,pageTitle,pageContent)  VALUES( 'en-US','error','Ops! An error occurred.','<p>The resource you requested caused an error.<br />Go back to&nbsp;<a href="/pages/default.aspx">homepage</a>.</p>')
GO
INSERT INTO #__staticPages_Culture (cultureName,pageName,pageTitle,pageContent)  VALUES( 'en-US','home','Home','<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.<br /> <br /> Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros...</p>')
GO
INSERT INTO #__staticPages_Culture (cultureName,pageName,pageTitle,pageContent)  VALUES( 'en-US','pageNotFound','Page not found','<p>The page you are looking for does not exists or is not available.<br /> Try to find it from website <a href="/pages/default.aspx">homepage</a>.</p>')
GO
INSERT INTO #__staticPages_Culture (cultureName,pageName,pageTitle,pageContent)  VALUES( 'it-IT','error','Ops! Si è verificato un errore.','<p>La risorsa richiesta ha generato un errore.<br />Torna alla&nbsp;<a href="/admin/&quot;/pages/default.aspx&quot;">homepage</a>.</p>')
GO
INSERT INTO #__staticPages_Culture (cultureName,pageName,pageTitle,pageContent)  VALUES( 'it-IT','home','Home','<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.<br /> <br /> Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>')
GO
INSERT INTO #__staticPages_Culture (cultureName,pageName,pageTitle,pageContent)  VALUES( 'it-IT','pageNotFound','Pagina non trovata','<p>La pagina richiesta non esiste o non &egrave; disponibile.<br /> Prova ad accedere alla risorsa desiderata dalla <a href="/pages/default.aspx">homepage</a> del sito.</p>')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Advert1','',1,'6')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Advert2','',1,'9')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Advert3','',1,'10')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Banner1','for banners',1,'7')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Banner2','for banners',1,'11')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Banner3','for banners',1,'12')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Bottom','bottom of the page',1,'13')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Cpanel','*** not used',0,'14')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Debug','debug info for dev, usually not visible to users',1,'15')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Footer','page footer',1,'8')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Header','page header',1,'16')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Icon','*** not used',0,'17')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Inset','*** not used',0,'18')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Left','left column',1,'1')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Legals','Legals info',1,'2')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Mainbody','main body of the page',1,'3')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Newsflash','news in brief',1,'5')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Pathway','(breadcrumb)',1,'19')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Right','right column',1,'20')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Toolbar','menu bar',1,'21')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Toolbar2','menu bar ',1,'32')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Top','top of the page',1,'22')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User1','custom area',1,'23')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User2','custom area',1,'24')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User3','custom area',1,'25')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User4','custom area',1,'26')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User5','custom area',1,'27')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User6','custom area',1,'28')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User7','custom area',1,'29')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User8','custom area',1,'30')
GO
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'User9','custom area',1,'31')
GO
INSERT INTO #__usersInRoles (username,rolename,applicationName)  VALUES( 'admin','admin','PigeonCms')
GO
INSERT INTO #__usersInRoles (username,rolename,applicationName)  VALUES( 'admin','backend','PigeonCms')
GO
INSERT INTO #__usersInRoles (username,rolename,applicationName)  VALUES( 'admin','debug','PigeonCms')
GO
INSERT INTO #__usersInRoles (username,rolename,applicationName)  VALUES( 'manager','backend','PigeonCms')
GO
INSERT INTO #__usersInRoles (username,rolename,applicationName)  VALUES( 'manager','manager','PigeonCms')
GO
