/*last update 20150203*/
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
INSERT INTO #__appSettings (keyName,keyTitle,keyValue,keyInfo)  VALUES( 'SmtpServer','smtp host address','smtp.yourdomain.com','')
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
INSERT INTO #__items (id,itemType,categoryId,enabled,ordering,defaultImageName,dateInserted,userInserted,dateUpdated,userUpdated,CustomBool1,CustomBool2,CustomBool3,CustomDate1,CustomDate2,CustomDate3,CustomDecimal1,CustomDecimal2,CustomDecimal3,CustomInt1,CustomInt2,CustomInt3,CustomString1,CustomString2,CustomString3,ItemParams,accessType,permissionId,accessCode,accessLevel,itemDate,validFrom,validTo,alias,commentsGroupId,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,threadId,cssClass)  VALUES( '1','PigeonCms.Item','1',1,'1','',NULL,'admin',NULL,'admin',0,0,0,NULL,NULL,NULL,'0','0','0','0','0','0','','','','','0','0','','0',NULL,NULL,NULL,'item1','0',NULL,NULL,NULL,NULL,'1',NULL)
GO
INSERT INTO #__items (id,itemType,categoryId,enabled,ordering,defaultImageName,dateInserted,userInserted,dateUpdated,userUpdated,CustomBool1,CustomBool2,CustomBool3,CustomDate1,CustomDate2,CustomDate3,CustomDecimal1,CustomDecimal2,CustomDecimal3,CustomInt1,CustomInt2,CustomInt3,CustomString1,CustomString2,CustomString3,ItemParams,accessType,permissionId,accessCode,accessLevel,itemDate,validFrom,validTo,alias,commentsGroupId,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,threadId,cssClass)  VALUES( '2','PigeonCms.Item','1',1,'2','',NULL,'admin',NULL,'admin',0,0,0,NULL,NULL,NULL,'0','0','0','0','0','0','','','','','1','0','','0',NULL,NULL,NULL,'item2','0',NULL,NULL,NULL,NULL,'2',NULL)
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'en-US','1','','<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.</p>
<hr class="system-readmore" />
<p>Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem. Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula. Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'en-US','2','','<p>This is second news</p>
<p>
<hr class="system-readmore" />
Second news text</p>')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'it-IT','1','Item1','<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu urna volutpat tellus blandit euismod. Vestibulum pretium iaculis ligula. Nullam condimentum tempus erat. Morbi bibendum tristique risus, et gravida magna consectetur id. Proin libero dui, mollis quis fringilla eleifend, accumsan eget lacus. Aliquam in venenatis leo. Mauris semper imperdiet purus gravida consequat. In quis lacus at libero ullamcorper egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a porttitor augue. Praesent at ligula non risus ullamcorper tincidunt vel ac eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi lectus lacus, mattis vitae dictum at, tincidunt eu urna. In tellus nisi, adipiscing sit amet vestibulum sed, commodo eget dolor. Praesent non nisl elit, rhoncus posuere neque.</p>
<hr class="system-readmore" />
<p>Integer tincidunt lectus turpis. Phasellus pharetra varius velit in interdum. Donec venenatis, arcu rhoncus posuere facilisis, mauris mauris egestas ipsum, ac aliquam purus tellus nec lorem.
<hr class="system-pagebreak" />
Sed porta hendrerit orci, non elementum mauris bibendum sit amet. Maecenas libero purus, luctus id egestas a, sollicitudin nec nulla. Aliquam mattis sapien et velit commodo ultricies. Ut nec sem mauris, non pellentesque lacus. Aenean fermentum laoreet vehicula.
<hr class="system-pagebreak" />
Etiam ullamcorper lacus et dui interdum non tempus sem scelerisque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas nunc tortor, tempor sed iaculis sed, volutpat in eros. Quisque pulvinar rhoncus adipiscing. Aliquam imperdiet, nulla non luctus rhoncus, nunc mi lacinia mi, sit amet congue turpis ipsum sed sapien. Mauris viverra, tellus vitae consectetur iaculis, lacus nibh porta dui, nec sodales nisi ipsum quis quam. Curabitur eleifend, tellus sed ultrices iaculis, neque erat accumsan neque, id egestas nunc erat tempor arcu. Sed leo neque, tristique ac auctor vel, consectetur ac lectus. Nulla accumsan, lacus sit amet adipiscing sagittis, velit urna imperdiet tortor, commodo vulputate augue lectus blandit eros.</p>
<p>&nbsp;</p>')
GO
INSERT INTO #__items_Culture (cultureName,itemId,title,description)  VALUES( 'it-IT','2','Item2','<p>Questa &egrave; la seconda news</p>
<p>
<hr class="system-readmore" />
Testo della seconda news</p>')
GO
SET IDENTITY_INSERT #__labels ON
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '3','it-IT','PigeonCms.EmailContactForm','LblInfoAdulti','adulti',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '4','en-US','PigeonCms.MenuAdmin','LblMenuType','Menu type',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '6','it-IT','PigeonCms.MenuAdmin','LblMenuType','Tipo menu',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '7','en-US','PigeonCms.MenuAdmin','LblContentType','Content type',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '8','it-IT','PigeonCms.MenuAdmin','LblContentType','Tipo contenuto',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '9','en-US','PigeonCms.Menuadmin','LblName','Name',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '10','it-IT','PigeonCms.Menuadmin','LblName','Nome',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '11','en-US','PigeonCms.Menuadmin','LblTitle','Title',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '12','it-IT','PigeonCms.Menuadmin','LblTitle','Titolo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '13','en-US','PigeonCms.Menuadmin','LblAlias','Alias',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '15','en-US','PigeonCms.Menuadmin','LblRoute','Route',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '17','en-US','PigeonCms.Menuadmin','LblLink','Link',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '18','en-US','PigeonCms.Menuadmin','LblRedirectTo','Redirect to',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '19','it-IT','PigeonCms.Menuadmin','LblRedirectTo','Trasferisci a',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '20','en-US','PigeonCms.Menuadmin','LblParentItem','Parent item',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '21','it-IT','PigeonCms.Menuadmin','LblParentItem','Elemento padre',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '22','en-US','PigeonCms.Menuadmin','ModuleTitle','Menu entries',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '23','it-IT','PigeonCms.Menuadmin','ModuleTitle','Voci menu',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '24','en-US','PigeonCms.Menuadmin','ModuleDescription','Menu items admin area',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '25','en-US','PigeonCms.StaticPage','PageNameLabel','Static content',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '26','it-IT','PigeonCms.StaticPage','PageNameLabel','Contenuto statico',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '27','en-US','PigeonCms.StaticPage','PageNameDescription','Name of the static page to display. If the content does not yet exist create it using [contents > static page] menu.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '28','it-IT','PigeonCms.StaticPage','PageNameDescription','Nome della pagina statica da visualizzare. Se il contenuto non esiste ancora crearlo dal menu [contenuti > pagine statiche].','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '29','en-US','PigeonCms.VideoPlayer','FileLabel','File',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '30','en-US','PigeonCms.VideoPlayer','FileDescription','Source file path or url',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '31','it-IT','PigeonCms.VideoPlayer','FileDescription','Percorso e indirizzo del file sorgente',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '32','en-US','PigeonCms.VideoPlayer','WidthLabel','Width',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '33','it-IT','PigeonCms.VideoPlayer','WidthLabel','Larghezza',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '34','en-US','PigeonCms.VideoPlayer','WidthDescription','Video width',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '35','it-IT','PigeonCms.VideoPlayer','WidthDescription','Larghezza del video',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '36','en-US','PigeonCms.VideoPlayer','HeightLabel','Height',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '37','it-IT','PigeonCms.VideoPlayer','HeightLabel','Altezza',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '38','en-US','PigeonCms.VideoPlayer','HeightDescription','Video Height',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '39','it-IT','PigeonCms.VideoPlayer','HeightDescription','Altezza del video',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '40','en-US','PigeonCms.VideoPlayer','ModuleTitle','Video player',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '41','it-IT','PigeonCms.VideoPlayer','ModuleTitle','Visualizzatore video',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '42','en-US','PigeonCms.Menuadmin','LblDetails','Details',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '44','it-IT','PigeonCms.Menuadmin','LblDetails','Dettagli',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '45','en-US','PigeonCms.Menuadmin','LblOptions','Options',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '46','it-IT','PigeonCms.Menuadmin','LblOptions','Opzioni',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '47','en-US','PigeonCms.Menuadmin','LblVisible','Visible',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '48','it-IT','PigeonCms.Menuadmin','LblVisible','Visibile',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '49','en-US','PigeonCms.Menuadmin','LblPublished','Published',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '50','it-IT','PigeonCms.Menuadmin','LblPublished','Pubblicato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '51','en-US','PigeonCms.Menuadmin','LblOverrideTitle','Override page title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '52','it-IT','PigeonCms.Menuadmin','LblOverrideTitle','Sovrascrivi titolo finestra','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '53','en-US','PigeonCms.Menuadmin','LblRecordInfo','Record info',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '54','it-IT','PigeonCms.Menuadmin','LblRecordInfo','Informazioni record',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '55','it-IT','PigeonCms.VideoPlayer','FileLabel','File',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '56','en-US','PigeonCms.Menuadmin','LblSecurity','Security',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '58','it-IT','PigeonCms.Menuadmin','LblSecurity','Sicurezza',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '59','en-US','PigeonCms.Menuadmin','LblViews','Views',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '60','it-IT','PigeonCms.Menuadmin','LblViews','Visualizzazioni',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '61','en-US','PigeonCms.Menuadmin','LblParameters','Parameters',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '62','it-IT','PigeonCms.Menuadmin','LblParameters','Parametri',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '63','it-IT','PigeonCms.OfflineAdmin','LblTitle','Titolo messaggio','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '64','en-US','PigeonCms.OfflineAdmin','LblTitle','Message title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '65','it-IT','PigeonCms.OfflineAdmin','LblMessage','Messaggio','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '66','en-US','PigeonCms.OfflineAdmin','LblMessage','Message','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '67','it-IT','PigeonCms.OfflineAdmin','LblOffline','Sito offline','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '68','en-US','PigeonCms.OfflineAdmin','LblOffline','Site offline','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '69','it-IT','PigeonCms.OfflineAdmin','ModuleTitle','Gestione sito offline','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '70','en-US','PigeonCms.OfflineAdmin','ModuleTitle','Offline admin','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '71','it-IT','PigeonCms.OfflineAdmin','LitOfflineWarning','ATTENZIONE. Stai mettendo il sito offline.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '72','en-US','PigeonCms.OfflineAdmin','LitOfflineWarning','WARNING. You are going to put website offline.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '73','en-US','PigeonCms.TopMenu','MenuLevelLabel','Menu level','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '74','it-IT','PigeonCms.TopMenu','MenuLevelLabel','Livello menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '75','en-US','PigeonCms.TopMenu','ShowChildLabel','Show childs','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '76','it-IT','PigeonCms.TopMenu','ShowChildLabel','Visualizza sottomenu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '77','en-US','PigeonCms.TopMenu','ListClassLabel','List css class','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '78','it-IT','PigeonCms.TopMenu','ListClassLabel','Classe css lista','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '79','en-US','PigeonCms.TopMenu','ItemSelectedClassLabel','Selected item css class','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '80','it-IT','PigeonCms.TopMenu','ItemSelectedClassLabel','Classe Css Elemento selezionato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '81','en-US','PigeonCms.TopMenu','ItemLastClassLabel','Last item css class','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '82','it-IT','PigeonCms.TopMenu','ItemLastClassLabel','Classe css ultimo elemento','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '83','en-US','PigeonCms.TopMenu','ShowPagePostFixLabel','Show page extension','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '84','it-IT','PigeonCms.TopMenu','ShowPagePostFixLabel','Visualizza estensione pagina','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '85','en-US','PigeonCms.TopMenu','ShowPagePostFixDescription','Show page .aspx extension','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '86','it-IT','PigeonCms.TopMenu','ShowPagePostFixDescription','Visualizza l''estensione .aspx della pagina','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '87','en-US','PigeonCms.TopMenu','MenuIdLabel','Css menu Id','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '88','it-IT','PigeonCms.TopMenu','MenuIdLabel','Css ID del menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '89','en-US','PigeonCms.TopMenu','MenuIdDescription','Css ID used for current menu.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '90','it-IT','PigeonCms.TopMenu','MenuIdDescription','ID css associato al menu corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '93','en-US','PigeonCms.ItemsAdmin','HomepageLabel','Show in homepage','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '94','it-IT','PigeonCms.ItemsAdmin','HomepageLabel','Visualizza in homepage','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '95','en-US','PigeonCms.MenuAdmin','LblTitleWindow','Window''s title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '96','it-IT','PigeonCms.MenuAdmin','LblTitleWindow','Titolo finestra','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '107','en-US','PigeonCms.ItemsAdmin','LblValidFrom','Valid from',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '109','it-IT','PigeonCms.ItemsAdmin','LblValidFrom','Valido dal',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '110','en-US','PigeonCms.ItemsAdmin','LblValidTo','Valid to','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '111','it-IT','PigeonCms.ItemsAdmin','LblValidTo','Valido fino al','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '114','en-US','PigeonCms.ItemsAdmin','LblTitle','Title',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '115','it-IT','PigeonCms.ItemsAdmin','LblTitle','Titolo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '116','en-US','PigeonCms.ItemsAdmin','LblDescription','Description',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '117','it-IT','PigeonCms.ItemsAdmin','LblDescription','Descrizione',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '118','it-IT','PigeonCms.ItemsAdmin','LblCategory','Categoria','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '119','en-US','PigeonCms.ItemsAdmin','LblCategory','Category','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '120','it-IT','PigeonCms.ItemsAdmin','LblEnabled','Abilitato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '121','en-US','PigeonCms.ItemsAdmin','LblEnabled','Enabled','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '122','it-IT','PigeonCms.ItemsAdmin','LblItemType','Tipo elemento','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '123','en-US','PigeonCms.ItemsAdmin','LblItemType','Item type','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '124','it-IT','PigeonCms.ItemsAdmin','ModuleTitle','Gestione elementi','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '125','en-US','PigeonCms.ItemsAdmin','ModuleTitle','Items manager','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '126','it-IT','PigeonCms.ItemsAdmin','LblItemDate','Data elemento','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '127','en-US','PigeonCms.ItemsAdmin','LblItemDate','Item date','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '128','en-US','PigeonCms.EmailContactForm','LblGenericError','An error occured',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '129','it-IT','PigeonCms.EmailContactForm','LblGenericError','Si è veirificato un errore',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '130','en-US','PigeonCms.EmailContactForm','LblGenericSucces','Operation completed',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '132','it-IT','PigeonCms.EmailContactForm','LblGenericSuccess','Operazione completata',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '133','it-IT','PigeonCms.FilesManager','FileExtensionsLabel','Estensioni file consentite','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '134','en-US','PigeonCms.FilesManager','FileExtensionsLabel','Allowed files extensions','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '135','it-IT','PigeonCms.FilesManager','FileExtensionsDescription','esempio: jpg;gif;doc','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '136','en-US','PigeonCms.FilesManager','FileExtensionsDescription','example: jpg;gif;doc','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '137','it-IT','PigeonCms.FilesManager','FileSizeLabel','Dimensione max file (KB)','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '138','en-US','PigeonCms.FilesManager','FileSizeLabel','Max file size (KB)','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '139','it-IT','PigeonCms.FilesManager','FileNameTypeLabel','Nome file','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '140','en-US','PigeonCms.FilesManager','FileNameTypeLabel','File name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '141','it-IT','PigeonCms.FilesManager','FileNameTypeDescription','Specifica come come sarà composto il file di destinazione','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '142','en-US','PigeonCms.FilesManager','FileNameTypeDescription','Specifies uploaded file name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '143','it-IT','PigeonCms.FilesManager','FilePrefixLabel','Prefisso file caricato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '144','en-US','PigeonCms.FilesManager','FilePrefixLabel','Uploaded file prefix','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '145','it-IT','PigeonCms.FilesManager','FilePrefixDescription','Applicato solo se non viene scelto di mantenere il nome file originale','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '146','en-US','PigeonCms.FilesManager','FilePrefixDescription','It will be applied only if you choose not to mantain original file name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '147','it-IT','PigeonCms.FilesManager','FilePathLabel','Path di caricamento files','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '148','en-US','PigeonCms.FilesManager','FilePathLabel','Uploaded files path','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '149','it-IT','PigeonCms.FilesManager','FilePathDescription','Path dove verrranno caricati i files (es. ~/Public/Files)','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '150','en-US','PigeonCms.FilesManager','FilePathDescription','Destination path for uploaded files (ex. ~/Public/Gallery)','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '151','it-IT','PigeonCms.FilesManager','UploadFieldsLabel','Max numero di campi upload','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '152','en-US','PigeonCms.FilesManager','UploadFieldsLabel','Max number of upload fields','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '153','it-IT','PigeonCms.FilesManager','UploadFieldsDescription','Numero massimo di files che si possono caricare contemporaneamente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '154','en-US','PigeonCms.FilesManager','UploadFieldsDescription','Max files to upload concurrently','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '155','it-IT','PigeonCms.MenuAdmin','LblMenuTypeDescription','Tipo di menu tra quelli creati al quale appartiene la voce corrente.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '156','en-US','PigeonCms.MenuAdmin','LblMenuTypeDescription','Menu type owner of current menu entry','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '157','it-IT','PigeonCms.MenuAdmin','LblContentTypeDescription','Tipo di contenuto visualizzato dalla voce di menu corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '158','en-US','PigeonCms.MenuAdmin','LblContentTypeDescription','Content type show by current menu entry','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '159','it-IT','PigeonCms.MenuAdmin','LblNameDescription','Nome mnemonico assegnato alla voce menu. Utilizzato solo per riconoscere la voce nella lista.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '160','en-US','PigeonCms.MenuAdmin','LblNameDescription','Mnemonic name assigned to the menu item. Used only to recognize the entry in the list.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '161','it-IT','PigeonCms.MenuAdmin','LblTitleDescription','Titolo della voce di menu. Apparirà come link nel menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '162','en-US','PigeonCms.MenuAdmin','LblTitleDescription','Title of the menu item. It will be the text of the link in menu items','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '163','it-IT','PigeonCms.MenuAdmin','LblTitleWindowDescription','Titolo della finestra del browser','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '164','en-US','PigeonCms.MenuAdmin','LblTitleWindowDescription','Title of the browser window','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '165','it-IT','PigeonCms.MenuAdmin','LblAliasDescription','Nome della pagina nell url della risorsa corrente. Esempio: mia-pagina','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '166','en-US','PigeonCms.MenuAdmin','LblAliasDescription','Page name in the url of the current resource. Example: my-page','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '167','it-IT','PigeonCms.MenuAdmin','LblRouteDescription','Percorso virtuale di accesso alla risorsa. Scegliere un percorso significativo per la voce corrente.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '168','en-US','PigeonCms.MenuAdmin','LblRouteDescription','Virtual path to access current resource. Choose a location significant to the current entry.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '169','it-IT','PigeonCms.MenuAdmin','LblLinkDescription','Collegamento ad una risorsa locale o esterna (sole se tipomenu link o javascript). Es.: http://www.google.it, mypage.aspx, ~/pages/mypage.aspx, alert()','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '170','en-US','PigeonCms.MenuAdmin','LblLinkDescription','Link to local or external resource (only for menutype link or javascript). Example: http://www.google.it, mypage.aspx, ~/pages/mypage.aspx, alert()','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '171','it-IT','PigeonCms.MenuAdmin','LblRedirectToDescription','Solo per tipo menu=alias. Scegliere la risorsa menu alla quale essere reindirizzato quando si accede alla voce di menu corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '172','en-US','PigeonCms.MenuAdmin','LblRedirectToDescription','Only for menutype=alias. Choose the menu resource to be redirected to when you click current menu item.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '173','it-IT','PigeonCms.MenuAdmin','LblParentItemDescription','Scegli la voce di menu padre alla quale la voce corrente dovrà appartenere per creare un sottomenu. ','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '174','en-US','PigeonCms.MenuAdmin','LblParentItemDescription','Choose parent menu entry for current item to create a submenu.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '175','it-IT','PigeonCms.MenuAdmin','LblCssClass','Classe css','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '176','en-US','PigeonCms.MenuAdmin','LblCssClass','Css class','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '177','it-IT','PigeonCms.MenuAdmin','LblCssClassDescription','Classe css personalizzata per la voce di menu corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '178','en-US','PigeonCms.MenuAdmin','LblCssClassDescription','Custom css class for current menu entry','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '179','it-IT','PigeonCms.MenuAdmin','LblTheme','Tema pagina','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '180','en-US','PigeonCms.MenuAdmin','LblTheme','Theme','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '181','it-IT','PigeonCms.MenuAdmin','LblThemeDescription','Tema di visualizzazione per la pagina corrente. Se non specificato verrà utilizzato quello di default per la Route scelta o quello di default del sito web.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '182','en-US','PigeonCms.MenuAdmin','LblThemeDescription','Display theme for the current page. If not specified, the default will be used for Route choice or the default Web site one.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '183','it-IT','PigeonCms.MenuAdmin','LblMasterpageDescription','Layout grafico per la pagina corrente. Se non specificato verrà utilizzato quello di default per la Route scelta o quello di default del sito web.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '184','en-US','PigeonCms.MenuAdmin','LblMasterpageDescription','Graphical layout for the current page. If not specified, the default will be used for Route choice or the default Web site one.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '185','it-IT','PigeonCms.MenuAdmin','LblVisibleDescription','Visualizza la voce di menu.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '186','en-US','PigeonCms.MenuAdmin','LblVisibleDescription','Show or not menu entry','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '187','it-IT','PigeonCms.MenuAdmin','LblPublishedDescription','Pubblica o meno il contenuto. La voce può essere non visibile ma pubblicata. In questo modo non sarà visibile a menu ma sarà comunque raggiungibile tramite il suo url.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '188','en-US','PigeonCms.MenuAdmin','LblPublishedDescription','Publish or not the content. The entry could be published but not visible, in this way you will not see the menu entry but it will be accessible by using its URL.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '189','it-IT','PigeonCms.MenuAdmin','LblOverrideTitleDescription','Se selezionato imposta il titolo della finestra del browser con il testo della casella Titolo Finestra','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '190','en-US','PigeonCms.MenuAdmin','LblOverrideTitleDescription','If selected sets the title of the browser window with the Window Title text box','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '191','it-IT','PigeonCms.MenuAdmin','LblShowModuleTitle','Visualizza titolo modulo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '192','en-US','PigeonCms.MenuAdmin','LblShowModuleTitle','Show module title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '193','it-IT','PigeonCms.MenuAdmin','LblShowModuleTitleDescription','Visualizza il titolo del modulo associato al menu corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '194','en-US','PigeonCms.MenuAdmin','LblShowModuleTitleDescription','Displays the title of the module associated with the current menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '195','it-IT','PigeonCms.PermissionsControl','LblSecurity','Sicurezza','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '196','en-US','PigeonCms.PermissionsControl','LblSecurity','Security','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '197','it-IT','PigeonCms.PermissionsControl','LblAccessType','Tipo di accesso','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '198','en-US','PigeonCms.PermissionsControl','LblAccessType','Access type','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '199','it-IT','PigeonCms.PermissionsControl','LblAccessTypeDescription','Tipo di accesso alla risorsa. [Public] visibile ad ogni utente. [Registered] visibile solo agli utenti autenticati che soddisfano il [codice di accesso] ed il [livello di accesso] quando impostati.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '200','en-US','PigeonCms.PermissionsControl','LblAccessTypeDescription','Resource access type. [Public] visible for all users. [Registered] visible only for logged in users only authenticated users that meet the [access code] and [access level] when set.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '201','it-IT','PigeonCms.PermissionsControl','LblRolesAllowed','Ruoli consentiti','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '202','en-US','PigeonCms.PermissionsControl','LblRolesAllowed','Allowed roles','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '203','it-IT','PigeonCms.PermissionsControl','LblRolesAllowedDescription','Ruoli utente che possono accedere alla risorsa. Solo per tipo accesso (registrato). Tenere premuto CTRL per effettuare una selezione multipla.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '204','en-US','PigeonCms.PermissionsControl','LblRolesAllowedDescription','User roles that can access the resource. Only for access type (registered). Hold down CTRL to make a multiple selection.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '205','it-IT','PigeonCms.PermissionsControl','LblAccessCode','Codice accesso','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '206','en-US','PigeonCms.PermissionsControl','LblAccessCode','Access code','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '207','it-IT','PigeonCms.PermissionsControl','LblAccessCodeDescription','Codice di accesso utente necessario per accedere alla risorsa. Se vuoto non verrà considerato.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '208','en-US','PigeonCms.PermissionsControl','LblAccessCodeDescription','User access code required to access the resource. If empty will not be considered.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '209','it-IT','PigeonCms.PermissionsControl','LblAccessLevel','Livello di accesso','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '210','en-US','PigeonCms.PermissionsControl','LblAccessLevel','Acces level','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '211','it-IT','PigeonCms.PermissionsControl','LblAccessLevelDescription','Minimo livello di accesso utente necessario per accedere alla risorsa. Se vuoto non verrà considerato.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '212','en-US','PigeonCms.PermissionsControl','LblAccessLevelDescription','Minimum user access level required to access the resource. If empty will not be considered.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '213','it-IT','PigeonCms.ModuleParams','LblUseCache','Usa la cache','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '214','en-US','PigeonCms.ModuleParams','LblUseCache','Use cache','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '215','it-IT','PigeonCms.ModuleParams','LblUseCacheDescription','Consente al modulo di caricare i dati dalla cache del server (solo se supportato dal modulo). Use global mantiene le impostazioni globali del sito. ','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '216','en-US','PigeonCms.ModuleParams','LblUseCacheDescription','Allows the module to load data from the web server cache (if supported by the module). Use global maintains the site default settings. ','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '217','it-IT','PigeonCms.ModuleParams','LblUseLog','Registra log','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '218','en-US','PigeonCms.ModuleParams','LblUseLog','Use log','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '219','it-IT','PigeonCms.ModuleParams','LblUseLogDescription','Registra gli eventi significativi del modulo nel registro di log (solo se supportato dal modulo). Use global mantiene le impostazioni globali del sito. ATTENZIONE: se abilitato aumenta il carico di lavoro del server.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '220','en-US','PigeonCms.ModuleParams','LblUseLogDescription','Save significant events of the module in the log list (only if supported by the module). Use global mantains site default settings. WARNING: it increases the workload of the server when enabled.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '221','it-IT','PigeonCms.ModuleParams','LblCssFile','File css','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '222','en-US','PigeonCms.ModuleParams','LblCssFile','Css file','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '223','it-IT','PigeonCms.ModuleParams','LblCssFileDescription','Nome del file css da caricare (es. miofile.css). Il file deve essere nella cartella della visualizzazione scelta per il modulo corrente.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '224','en-US','PigeonCms.ModuleParams','LblCssFileDescription','Name of css file to upload (ex. myfile.css). The file must be in the folder of the selected view for the current module.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '225','it-IT','PigeonCms.ModuleParams','LblCssClass','Classe css','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '226','en-US','PigeonCms.ModuleParams','LblCssClass','Css class','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '227','it-IT','PigeonCms.ModuleParams','LblCssClassDescription','Classe css personalizzata per il modulo corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '228','en-US','PigeonCms.ModuleParams','LblCssClassDescription','Custom css class for current module','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '229','it-IT','PigeonCms.MenuAdmin','LblViewsDescription','Visualizzazione del modulo corrente. Se il modulo prevede diversi tipi di visualizzazione sceglierlo dalla lista.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '230','en-US','PigeonCms.MenuAdmin','LblViewsDescription','Type of view of the current module. If the form provides different viewing options choose one from the list.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '231','it-IT','PigeonCms.ModulesAdmin','ModuleTitle','Gestione moduli','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '232','en-US','PigeonCms.ModulesAdmin','ModuleTitle','Modules management','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '233','it-IT','PigeonCms.ModulesAdmin','LblCreated','Creazione','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '234','en-US','PigeonCms.ModulesAdmin','LblCreated','Created','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '235','it-IT','PigeonCms.ModulesAdmin','LblLastUpdate','Ultimo aggiornamento','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '236','en-US','PigeonCms.ModulesAdmin','LblLastUpdate','Last update','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '237','it-IT','PigeonCms.ModulesAdmin','LblDetails','Dettagli','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '238','en-US','PigeonCms.ModulesAdmin','LblDetails','Details','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '239','it-IT','PigeonCms.ModulesAdmin','LblModuleType','Tipo modulo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '240','en-US','PigeonCms.ModulesAdmin','LblModuleType','Module type','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '241','it-IT','PigeonCms.ModulesAdmin','LblModuleTypeDescription','Tipo di contenuto del modulo corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '242','en-US','PigeonCms.ModulesAdmin','LblModuleTypeDescription','Content type shown by current module','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '243','it-IT','PigeonCms.ModulesAdmin','LblTitle','Titolo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '244','en-US','PigeonCms.ModulesAdmin','LblTitle','Title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '245','it-IT','PigeonCms.ModulesAdmin','LblTitleDescription','Titolo del modulo. Verrà visualizzato se abilitata l opzione [visualizza titolo]','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '246','en-US','PigeonCms.ModulesAdmin','LblTitleDescription','Module title. It will be shown when [show title] is checked','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '247','it-IT','PigeonCms.ModulesAdmin','LblShowTitle','Visualizza titolo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '248','en-US','PigeonCms.ModulesAdmin','LblShowTitle','Show title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '249','it-IT','PigeonCms.ModulesAdmin','LblShowTitleDescription','Visualizza il titolo del modulo nella posizione prevista','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '250','en-US','PigeonCms.ModulesAdmin','LblShowTitleDescription','Shows module title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '251','it-IT','PigeonCms.ModulesAdmin','LblPublished','Pubblicato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '252','en-US','PigeonCms.ModulesAdmin','LblPublished','Published','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '253','it-IT','PigeonCms.ModulesAdmin','LblPublishedDescription','Pubblica o meno il modulo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '254','en-US','PigeonCms.ModulesAdmin','LblPublishedDescription','Publish or not the current module','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '255','it-IT','PigeonCms.ModulesAdmin','LblPosition','Posizione','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '256','en-US','PigeonCms.ModulesAdmin','LblPosition','Position','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '257','it-IT','PigeonCms.ModulesAdmin','LblPositionDescription','Posizione del modulo nel layout della pagina corrente. Se tale [blocco template] non fosse presente nel layout il modulo non verrà visualizzato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '258','en-US','PigeonCms.ModulesAdmin','LblPositionDescription','Module position in the layout of currente page. If current layout does not contains that [template block] module will be not shown.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '259','it-IT','PigeonCms.ModulesAdmin','LblOrder','Ordine','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '260','en-US','PigeonCms.ModulesAdmin','LblOrder','Order','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '261','it-IT','PigeonCms.ModulesAdmin','LblOrderDescription','Ordine di visualizzazione del modulo all interno della corrente posizione','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '262','en-US','PigeonCms.ModulesAdmin','LblOrderDescription','Display order inside the current position','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '263','it-IT','PigeonCms.ModulesAdmin','LblContent','Contenuto','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '264','en-US','PigeonCms.ModulesAdmin','LblContent','Content','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '265','it-IT','PigeonCms.ModulesAdmin','LblContentDescription','Attualmente non utilizzato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '266','en-US','PigeonCms.ModulesAdmin','LblContentDescription','Actually not used','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '267','it-IT','PigeonCms.ModulesAdmin','LblMenuEntries','Voci menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '268','en-US','PigeonCms.ModulesAdmin','LblMenuEntries','Menu entries','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '269','it-IT','PigeonCms.ModulesAdmin','LblMenus','Visualizza in','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '270','en-US','PigeonCms.ModulesAdmin','LblMenus','Show in','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '271','it-IT','PigeonCms.ModulesAdmin','LblMenusDescription','Specifica in quali pagine visualizzare il modulo corrente','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '272','en-US','PigeonCms.ModulesAdmin','LblMenusDescription','Specify which pages display the current module','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '273','it-IT','PigeonCms.ModulesAdmin','LblMenuAll','Tutte','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '274','en-US','PigeonCms.ModulesAdmin','LblMenuAll','All','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '275','it-IT','PigeonCms.ModulesAdmin','LblMenuNone','Nessuna','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '276','en-US','PigeonCms.ModulesAdmin','LblMenuNone','None','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '277','it-IT','PigeonCms.ModulesAdmin','LblMenuSelection','Singole voci di menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '278','en-US','PigeonCms.ModulesAdmin','LblMenuSelection','Select menu items','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '279','it-IT','PigeonCms.ModulesAdmin','LblMenuEntriesDescription','Scegli le singole voci di menu nelle quali verrà visualizzato il modulo, solo se [visualizza in]=singole voci di menu. Tenere premuto CTRL per selezione multipla. Scegliendo il nome del menu (es, [mainmenu]) il module verrà visualizzata in tutte le sue voci.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '280','en-US','PigeonCms.ModulesAdmin','LblMenuEntriesDescription','Choose individual menu items in which the module will appear, only if [menus] = [select menu items]. Hold CTRL for multiple selection. Choosing the menu name (eg, [mainmenu]) the module will be displayed in all its entries.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '281','it-IT','PigeonCms.ModulesAdmin','LblViews','Visualizzazioni','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '282','en-US','PigeonCms.ModulesAdmin','LblViews','Views','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '283','it-IT','PigeonCms.ModulesAdmin','LblViewsDescription','Visualizzazione del modulo corrente. Se il modulo prevede diversi tipi di visualizzazione sceglierlo dalla lista.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '284','en-US','PigeonCms.ModulesAdmin','LblViewsDescription','Type of view of the current module. If the form provides different viewing options choose one from the list.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '285','it-IT','PigeonCms.ItemsAdmin','LblCreated','Creazione','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '286','en-US','PigeonCms.ItemsAdmin','LblCreated','Created','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '287','it-IT','PigeonCms.ItemsAdmin','LblLastUpdate','Ultimo aggiornamento','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '288','en-US','PigeonCms.ItemsAdmin','LblLastUpdate','Last update','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '289','it-IT','PigeonCms.ItemsAdmin','LblFields','Campi personalizzati','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '290','en-US','PigeonCms.ItemsAdmin','LblFields','Custom fields','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '291','it-IT','PigeonCms.ItemsAdmin','LblParameters','Parametri','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '292','en-US','PigeonCms.ItemsAdmin','LblParameters','Parameters','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '293','it-IT','PigeonCms.StaticPagesAdmin','ModuleTitle','Contenuti statici','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '294','en-US','PigeonCms.StaticPagesAdmin','ModuleTitle','Static contents','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '295','it-IT','PigeonCms.StaticPagesAdmin','LblName','Nome','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '296','en-US','PigeonCms.StaticPagesAdmin','LblName','Name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '297','it-IT','PigeonCms.StaticPagesAdmin','LblNameDescription','Nome mnemonico assegnato al contenuto. Utilizzato solo per riconoscere la voce nella lista.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '298','en-US','PigeonCms.StaticPagesAdmin','LblNameDescription','Mnemonic name assigned to the content. Used only to recognize the entry in the list.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '299','it-IT','PigeonCms.StaticPagesAdmin','LblTitle','Titolo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '300','en-US','PigeonCms.StaticPagesAdmin','LblTitle','Title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '301','it-IT','PigeonCms.StaticPagesAdmin','LblTitleDescription','Titolo della pagina. Verrà visualizzato se abilitata l opzione [visualizza titolo]','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '302','en-US','PigeonCms.StaticPagesAdmin','LblTitleDescription','Page title. It will be shown when [show title] is checked','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '303','it-IT','PigeonCms.StaticPagesAdmin','LblShowTitle','Visualizza titolo','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '304','en-US','PigeonCms.StaticPagesAdmin','LblShowTitle','Show title','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '305','it-IT','PigeonCms.StaticPagesAdmin','LblShowTitleDescription','Quando abilitato visualizza il titolo della pagina','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '306','en-US','PigeonCms.StaticPagesAdmin','LblShowTitleDescription','When checked it shows the title of the page','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '307','it-IT','PigeonCms.StaticPagesAdmin','LblVisible','Visibile','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '308','en-US','PigeonCms.StaticPagesAdmin','LblVisible','Visible','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '309','it-IT','PigeonCms.StaticPagesAdmin','LblVisibleDescription','Visualizza o meno il contenuto, anche se presente in più voci di menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '310','en-US','PigeonCms.StaticPagesAdmin','LblVisibleDescription','Shows or not the static content, also if contained in many menu entries','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '311','it-IT','PigeonCms.StaticPagesAdmin','LblContent','Contenuto','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '312','en-US','PigeonCms.StaticPagesAdmin','LblContent','Content','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '313','it-IT','PigeonCms.ItemsAdmin','LblItemTypeDescription','Tipo di item selezionato. In base al tipo scelto saranno disponibili campi personalizzati e parametri diversi.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '314','en-US','PigeonCms.ItemsAdmin','LblItemTypeDescription','Selected item type. Depending on it will be aavailable differen custom fields and parameters.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '315','it-IT','PigeonCms.ItemsAdmin','LblCategoryDescription','Categoria di appartenenza dell item corrente. ','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '316','en-US','PigeonCms.ItemsAdmin','LblCategoryDescription','Current of current item.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '317','it-IT','PigeonCms.ItemsAdmin','LblEnabledDescription','Abilita o meno l item corrent','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '318','en-US','PigeonCms.ItemsAdmin','LblEnabledDescription','Enable or not the current item','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '319','it-IT','PigeonCms.ItemsAdmin','LblTitleDescription','Titolo dell elemento. Verrà visualizzato a seconda della visualizzazione scelta nel modulo di visualizzazione [PigeonCms.Item]','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '320','en-US','PigeonCms.ItemsAdmin','LblTitleDescription','Title of the item. It will be shown depending on the choosen view in the module [PigeonCms.Item] ','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '321','it-IT','PigeonCms.ItemsAdmin','LblItemDateDescription','Data associata all elemento. Viene impostata in automatico la data di creazione.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '322','en-US','PigeonCms.ItemsAdmin','LblItemDateDescription','Date associated to the element. By default it is set to current date when created.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '323','it-IT','PigeonCms.ItemsAdmin','LblValidFromDescription','Data di inizio validità dell elemento. Impostata automaticamente alla data corrente in fase di creazione. Se si imposta una data futura l elemento non verrà visualizzato fino a tale data.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '324','en-US','PigeonCms.ItemsAdmin','LblValidFromDescription','Effective Date of the element. Automatically set to the current date when element is created. If you set a future date the item will not appear until that date.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '325','it-IT','PigeonCms.ItemsAdmin','LblValidToDescription','Data di fine validità dell elemento. Lasciare vuoto per non associare una scedenza.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '326','en-US','PigeonCms.ItemsAdmin','LblValidToDescription','Expiring date of the element. Element never expires when blank.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '327','it-IT','PigeonCms.ItemsAdmin','LblDescriptionDescription','Descrizione dell elemento (in più lingue quando abilitate).','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '328','en-US','PigeonCms.ItemsAdmin','LblDescriptionDescription','Description of the item (in more languages when enabled)','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '329','it-IT','PigeonCms.EmailContactForm','LblName','nome','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '330','en-US','PigeonCms.EmailContactForm','LblName','name and surname','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '331','it-IT','PigeonCms.EmailContactForm','LblCompanyName','azienda','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '332','en-US','PigeonCms.EmailContactForm','LblCompanyName','company name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '333','it-IT','PigeonCms.EmailContactForm','LblCity','città','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '334','en-US','PigeonCms.EmailContactForm','LblCity','city','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '335','it-IT','PigeonCms.EmailContactForm','LblInfoEmail','e-mail','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '336','en-US','PigeonCms.EmailContactForm','LblInfoEmail','e-mail','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '337','it-IT','PigeonCms.EmailContactForm','LblPhone','telefono','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '338','en-US','PigeonCms.EmailContactForm','LblPhone','phone','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '339','it-IT','PigeonCms.EmailContactForm','LblMessage','messaggio','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '340','en-US','PigeonCms.EmailContactForm','LblMessage','message','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '341','it-IT','PigeonCms.EmailContactForm','LblPrivacyText','Informativa ai sensi dell’art. 13 del d.lgs. n. 196/2003.','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '342','en-US','PigeonCms.EmailContactForm','LblPrivacyText','privacy text message','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '343','it-IT','PigeonCms.MenuAdmin','LblChange','cambia','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '344','en-US','PigeonCms.MenuAdmin','LblChange','change','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '345','it-IT','PigeonCms.MenuAdmin','LblChangeDescription','Consente di cambiare il tipo di contenuto della voce corrente. ATTENZIONE: gli eventuali parametri associati al modulo corrente andranno persi','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '346','en-US','PigeonCms.MenuAdmin','LblChangeDescription','Change the content type of the current entry. WARNING: Any parameters associated with the current module will be lost','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '347','it-IT','PigeonCms.ItemsSearch','LblSearchText','','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '348','en-US','PigeonCms.ItemsSearch','LblSearchText','','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '349','it-IT','PigeonCms.ItemsSearch','LblSearchLink','cerca','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '350','en-US','PigeonCms.ItemsSearch','LblSearchLink','search','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '351','it-IT','PigeonCms.PlaceholdersAdmin','ModuleTitle','Segnaposto','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '352','en-US','PigeonCms.PlaceholdersAdmin','ModuleTitle','Placeholders','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '353','it-IT','PigeonCms.PlaceholdersAdmin','LblName','Nome','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '354','en-US','PigeonCms.PlaceholdersAdmin','LblName','Name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '355','it-IT','PigeonCms.PlaceholdersAdmin','LblContent','Contenuto','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '356','en-US','PigeonCms.PlaceholdersAdmin','LblContent','Content','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '357','it-IT','PigeonCms.PlaceholdersAdmin','LblVisible','Visibile','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '358','en-US','PigeonCms.PlaceholdersAdmin','LblVisible','Visible','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '359','en-US','PigeonCms.Items','LblMoreInfo','more info','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '360','it-IT','PigeonCms.Items','LblMoreInfo','leggi tutto','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '361','en-US','PigeonCms.Items','LblPrint','Print','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '362','it-IT','PigeonCms.Items','LblPrint','Stampa','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '365','it-IT','PigeonCms.Placeholder','NameLabel','Nome','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '366','en-US','PigeonCms.Placeholder','NameLabel','Name','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '367','it-IT','PigeonCms.EmailContactForm','ShowPrivacyCheckLabel','Visualizza casella privacy','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '368','en-US','PigeonCms.EmailContactForm','ShowPrivacyCheckLabel','Show privacy check','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '369','it-IT','PigeonCms.EmailContactForm','PrivacyTextLabel','Informativa privacy','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '370','en-US','PigeonCms.EmailContactForm','PrivacyTextLabel','Privacy text','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '371','it-IT','PigeonCms.TopMenu','MenuTypeLabel','Tipo menu','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '372','en-US','PigeonCms.TopMenu','MenuTypeLabel','Menu type','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '373','en-US','PigeonCms.EmailContactForm','LblCaptchaText','enter the code shown','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '374','it-IT','PigeonCms.EmailContactForm','LblCaptchaText','inserisci il codice visualizzato','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '375','it-IT','PigeonCms.FileUpload','all','tutti','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '376','en-US','PigeonCms.FileUpload','all','all','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '377','it-IT','PigeonCms.FileUpload','Allowedfiles','Files consentiti','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '378','en-US','PigeonCms.FileUpload','Allowedfiles','Allowed files','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '379','it-IT','PigeonCms.FileUpload','MaxSize','Dimensione massima','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '380','en-US','PigeonCms.FileUpload','MaxSize','Max size','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '381','it-IT','PigeonCms.FilesManager','Select','Seleziona','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '382','en-US','PigeonCms.FilesManager','Select','Select','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '383','it-IT','PigeonCms.FilesManager','Preview','Anteprima','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '384','en-US','PigeonCms.FilesManager','Preview','Preview','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '385','en-US','PigeonCms.ContentEditorControl','ReadMore','Read more',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '386','it-IT','PigeonCms.ContentEditorControl','ReadMore','Leggi tutto',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '387','en-US','PigeonCms.ContentEditorControl','Pagebreak','Page break','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '388','it-IT','PigeonCms.ContentEditorControl','Pagebreak','Salto pagina','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '389','en-US','PigeonCms.ContentEditorControl','File','File',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '390','it-IT','PigeonCms.ContentEditorControl','File','File',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '392','en-US','PigeonCms.FilesUpload','Preview','Preview',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '393','it-IT','PigeonCms.FilesUpload','Preview','Anteprima',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '394','en-US','PigeonCms.FilesUpload','Select','Select',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '395','it-IT','PigeonCms.FilesUpload','Select','Seleziona',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '402','en-US','PigeonCms.SectionsAdmin','LblTitle','Title',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '403','it-IT','PigeonCms.SectionsAdmin','LblTitle','Titolo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '404','en-US','PigeonCms.SectionsAdmin','LblDescription','Description','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '405','it-IT','PigeonCms.SectionsAdmin','LblDescription','Descrizione','','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '406','en-US','PigeonCms.SectionsAdmin','LblEnabled','Enabled',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '407','it-IT','PigeonCms.SectionsAdmin','LblEnabled','Abilitato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '408','en-US','PigeonCms.SectionsAdmin','LblLimits','Limits',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '409','it-IT','PigeonCms.SectionsAdmin','LblLimits','Limiti',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '410','en-US','PigeonCms.SectionsAdmin','LblMaxItems','Max items',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '411','it-IT','PigeonCms.SectionsAdmin','LblMaxItems','Numero max items',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '412','en-US','PigeonCms.SectionsAdmin','LblMaxAttachSizeKB','Max size for attachments (KB)',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '413','it-IT','PigeonCms.SectionsAdmin','LblMaxAttachSizeKB','Dimensione massima allegati (KB)',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '414','it-IT','PigeonCms.MemberEditorControl','LblPasswordControl','Ripeti password',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '415','it-IT','PigeonCms.MemberEditorControl','LblOldPassword','Vecchia password',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '416','it-IT','PigeonCms.MemberEditorControl','LblEnabled','Abilitato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '417','it-IT','PigeonCms.MemberEditorControl','LblComment','Note',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '418','it-IT','PigeonCms.MemberEditorControl','LblSex','Sesso',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '419','it-IT','PigeonCms.MemberEditorControl','LblCompanyName','Nome azienda',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '420','it-IT','PigeonCms.MemberEditorControl','LblVat','Partita IVA',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '421','it-IT','PigeonCms.MemberEditorControl','LblSsn','Codice fiscale',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '422','it-IT','PigeonCms.MemberEditorControl','LblFirstName','Nome',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '423','it-IT','PigeonCms.MemberEditorControl','LblSecondName','Cognome',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '424','it-IT','PigeonCms.MemberEditorControl','LblAddress1','Indirizzo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '425','it-IT','PigeonCms.MemberEditorControl','LblAddress2','Indirizzo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '426','it-IT','PigeonCms.MemberEditorControl','LblCity','Citt&agrave;',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '427','it-IT','PigeonCms.MemberEditorControl','LblState','Provincia',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '428','it-IT','PigeonCms.MemberEditorControl','LblZipCode','Cap',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '429','it-IT','PigeonCms.MemberEditorControl','LblNation','Nazione',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '430','it-IT','PigeonCms.MemberEditorControl','LblTel1','Telefono',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '431','it-IT','PigeonCms.MemberEditorControl','LblMobile1','Cellulare',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '432','it-IT','PigeonCms.MemberEditorControl','LblWebsite1','Sito web',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '433','it-IT','PigeonCms.MemberEditorControl','LblPasswordNotMatching','le password non corrispondono',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '434','it-IT','PigeonCms.MemberEditorControl','LblInvalidEmail','email non valida',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '435','it-IT','PigeonCms.MemberEditorControl','LblInvalidPassword','password non valida',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '458','en-US','PigeonCms.MemberEditorControl','LblPasswordControl','Repeat password',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '459','en-US','PigeonCms.MemberEditorControl','LblOldPassword','Old password',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '460','en-US','PigeonCms.MemberEditorControl','LblEnabled','Enabled',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '461','en-US','PigeonCms.MemberEditorControl','LblComment','Notes',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '462','en-US','PigeonCms.MemberEditorControl','LblSex','Gender',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '463','en-US','PigeonCms.MemberEditorControl','LblCompanyName','Company name',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '464','en-US','PigeonCms.MemberEditorControl','LblVat','Vat',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '465','en-US','PigeonCms.MemberEditorControl','LblSsn','Ssn',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '466','en-US','PigeonCms.MemberEditorControl','LblFirstName','First name',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '467','en-US','PigeonCms.MemberEditorControl','LblSecondName','Second name',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '468','en-US','PigeonCms.MemberEditorControl','LblAddress1','Address',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '469','en-US','PigeonCms.MemberEditorControl','LblAddress2','Address',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '470','en-US','PigeonCms.MemberEditorControl','LblCity','Citty;',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '471','en-US','PigeonCms.MemberEditorControl','LblState','State',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '472','en-US','PigeonCms.MemberEditorControl','LblZipCode','Zip',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '473','en-US','PigeonCms.MemberEditorControl','LblNation','Nation',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '474','en-US','PigeonCms.MemberEditorControl','LblTel1','Telephone',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '475','en-US','PigeonCms.MemberEditorControl','LblMobile1','Mobile phone',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '476','en-US','PigeonCms.MemberEditorControl','LblWebsite1','Website',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '477','en-US','PigeonCms.MemberEditorControl','LblPasswordNotMatching','passwords do not match',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '478','en-US','PigeonCms.MemberEditorControl','LblInvalidEmail','invalid email',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '479','en-US','PigeonCms.MemberEditorControl','LblInvalidPassword','invalid password',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '480','en-US','PigeonCms.MemberEditorControl','LblManadatoryFields','please fill mandatory fields',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '481','it-IT','PigeonCms.MemberEditorControl','LblManadatoryFields','compila i campi obbligatori',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '482','en-US','PigeonCms.MemberEditorControl','LblCaptchaText','enter the code shows',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '483','it-IT','PigeonCms.MemberEditorControl','LblCaptchaText','inserisci il codice visualizzato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '484','en-US','PigeonCms.MembersAdmin','LblCaptchaText','enter the code shown',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '485','it-IT','PigeonCms.MembersAdmin','LblCaptchaText','inserisci il codice visualizzato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '486','en-US','DroidCatalogue.Orders','LblFillCompanyOrName','please fill company name or your full name',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '487','it-IT','DroidCatalogue.Orders','LblFillCompanyOrName','inserisci il nome azienda o il tuo nome completo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '488','en-US','DroidCatalogue.Orders','LblFillAddress','please fill address field',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '489','it-IT','DroidCatalogue.Orders','LblFillAddress','inserisci il campo indirizzo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '490','en-US','DroidCatalogue.Orders','LblFillCity','please fill city field',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '491','it-IT','DroidCatalogue.Orders','LblFillCity','inserisci il campo città',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '492','en-US','DroidCatalogue.Orders','LblFillState','please fill state field',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '493','it-IT','DroidCatalogue.Orders','LblFillState','inserisci il campo provincia',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '494','en-US','DroidCatalogue.Orders','LblFillZip','please fill zip field',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '495','it-IT','DroidCatalogue.Orders','LblFillZip','inserisci il campo Cap',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '496','en-US','DroidCatalogue.Orders','LblFillVatOrSsn','please fill Vat or Ssn field',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '497','it-IT','DroidCatalogue.Orders','LblFillVatOrSsn','inserisci Partita IVA o codice fiscale',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '498','en-US','DroidCatalogue.Orders','LblItems','items',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '499','it-IT','DroidCatalogue.Orders','LblItems','articoli',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '500','en-US','DroidCatalogue.Orders','LblDiskSpace','disk space',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '501','it-IT','DroidCatalogue.Orders','LblDiskSpace','spazio su disco',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '502','en-US','DroidCatalogue.Orders','LblClients','clients',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '503','it-IT','DroidCatalogue.Orders','LblClients','attivazioni',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '504','en-US','DroidCatalogue.Orders','LblSetup','setup',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '505','it-IT','DroidCatalogue.Orders','LblSetup','setup',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '506','en-US','DroidCatalogue.Orders','LblFreeOnline','free online',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '507','it-IT','DroidCatalogue.Orders','LblFreeOnline','gratis online',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '508','en-US','DroidCatalogue.Orders','LblAds','ads',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '509','it-IT','DroidCatalogue.Orders','LblAds','pubblicit&agrave;',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '510','en-US','DroidCatalogue.Orders','LblYes','yes',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '511','it-IT','DroidCatalogue.Orders','LblYes','s&igrave;',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '512','en-US','DroidCatalogue.Orders','LblNoAds','no ads',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '513','it-IT','DroidCatalogue.Orders','LblNoAds','nessuna pubblicit&agrave',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '514','en-US','DroidCatalogue.Orders','LblMonthlyFee','monthly fee',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '515','it-IT','DroidCatalogue.Orders','LblMonthlyFee','canone mensile',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '516','en-US','DroidCatalogue.Orders','LblFree','free',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '517','it-IT','DroidCatalogue.Orders','LblFree','gratis',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '518','en-US','DroidCatalogue.Orders','LblUnlimited','unlimited',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '519','it-IT','DroidCatalogue.Orders','LblUnlimited','illimitate',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '520','en-US','DroidCatalogue.Orders','LblChoosePlan','Choose plan',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '521','it-IT','DroidCatalogue.Orders','LblChoosePlan','Scelta piano',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '522','en-US','DroidCatalogue.Orders','LblCheckout','Checkout',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '523','it-IT','DroidCatalogue.Orders','LblCheckout','Conferma',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '524','en-US','DroidCatalogue.Orders','LblFinish','Finish',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '525','it-IT','DroidCatalogue.Orders','LblFinish','Fine',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '526','en-US','DroidCatalogue.Orders','LblOrderSummary','Order summary',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '527','it-IT','DroidCatalogue.Orders','LblOrderSummary','Riepilogo ordine',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '528','en-US','DroidCatalogue.Orders','LblBillingSummary','User and billing summary',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '529','it-IT','DroidCatalogue.Orders','LblBillingSummary','Riepilogo dati utente e pagamento',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '530','en-US','PigeonCms.ItemsAdmin','ChooseSection','Choose a section before',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '531','it-IT','PigeonCms.ItemsAdmin','ChooseSection','Scegli una sezione',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '532','en-US','PigeonCms.ItemsAdmin','ChooseCategory','Choose a category before',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '533','it-IT','PigeonCms.ItemsAdmin','ChooseCategory','Scegli una categoria',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '534','it-IT','PigeonCms.ItemsAdmin','NewTicket','Nuovo ticket',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '535','it-IT','PigeonCms.ItemsAdmin','PriorityFilter','--priorità--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '536','it-IT','PigeonCms.ItemsAdmin','Close','chiudi',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '537','it-IT','PigeonCms.ItemsAdmin','Reopen','riapri',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '538','it-IT','PigeonCms.ItemsAdmin','Lock','blocca',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '539','it-IT','PigeonCms.ItemsAdmin','Actions','--azioni--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '540','it-IT','PigeonCms.ItemsAdmin','StatusFilter','--stato--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '541','it-IT','PigeonCms.ItemsAdmin','CategoryFilter','--categoria--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '542','it-IT','PigeonCms.ItemsAdmin','Always','tutto',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '543','it-IT','PigeonCms.ItemsAdmin','Today','oggi',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '544','it-IT','PigeonCms.ItemsAdmin','Last week','ultima settimana',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '545','it-IT','PigeonCms.ItemsAdmin','Last month','ultimo mese',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '546','it-IT','PigeonCms.ItemsAdmin','Subject','Oggetto',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '547','it-IT','PigeonCms.ItemsAdmin','Reply','Rispondi',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '548','it-IT','PigeonCms.ItemsAdmin','BackToList','Torna alla lista',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '549','it-IT','PigeonCms.ItemsAdmin','MyTickets','solo mie',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '550','it-IT','PigeonCms.ItemsAdmin','AssignTo','--assegna--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '551','it-IT','PigeonCms.ItemsAdmin','Status','Stato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '552','it-IT','PigeonCms.ItemsAdmin','AssignedTo','Assegnato a',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '553','it-IT','PigeonCms.ItemsAdmin','Low','Bassa',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '554','it-IT','PigeonCms.ItemsAdmin','Medium','Media',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '555','it-IT','PigeonCms.ItemsAdmin','High','Alta',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '556','it-IT','PigeonCms.ItemsAdmin','Open','Aperto',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '557','it-IT','PigeonCms.ItemsAdmin','WorkInProgress','In lavorazione',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '558','it-IT','PigeonCms.ItemsAdmin','Closed','Chiuso',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '559','it-IT','PigeonCms.ItemsAdmin','Locked','Bloccato',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '560','it-IT','PigeonCms.ItemsAdmin','Working','in lavorazione',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '561','it-IT','PigeonCms.ItemsAdmin','<subject>','<oggetto>',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '562','it-IT','PigeonCms.ItemsAdmin','Priority','Priorità',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '563','it-IT','PigeonCms.ItemsAdmin','Operator','Operatore',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '564','it-IT','PigeonCms.ItemsAdmin','DateInserted','Inserito il',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '565','it-IT','PigeonCms.ItemsAdmin','LastActivity','Ultima attività',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '566','it-IT','PigeonCms.ItemsAdmin','Text','Testo',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '567','it-IT','PigeonCms.ItemsAdmin','MessageTicketTitle','Ticket "[[ItemTitle]]" ([[ItemId]]) [[Extra]]','allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '568','it-IT','PigeonCms.ItemsAdmin','MessageTicketDescription','[[ItemDescription]]','allowed placeholders:  [[ItemId]], [[ItemTitle]], [[ItemDescription]], [[ItemUserUpdated]], [[ItemDateUpdated]], [[Extra]]','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '569','it-IT','PigeonCms.ItemsAdmin','CustomerFilter','--cliente--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '570','it-IT','PigeonCms.ItemsAdmin','Customer','Cliente',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '571','it-IT','PigeonCms.ItemsAdmin','Attachment','Allegati',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '572','it-IT','PigeonCms.ItemsAdmin','AttachFiles','Allega files',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '573','it-IT','PigeonCms.ItemsAdmin','SendEmail','Invia email',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '574','it-IT','PigeonCms.ItemsAdmin','OperatorFilter','--operatore--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '575','it-IT','PigeonCms.ItemsAdmin','UserInsertedFilter','--inserito da--',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '576','it-IT','PigeonCms.ItemsAdmin','SaveAndClose','Salva e chiudi ticket',NULL,'2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '577','it-IT','PigeonCms.LoginForm','','user','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '578','it-IT','PigeonCms.RoutesAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '579','it-IT','PigeonCms.MenuAdmin','Main','Main','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '580','it-IT','PigeonCms.MenuAdmin','Options','Options','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '581','it-IT','PigeonCms.MenuAdmin','Security','Security','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '582','it-IT','PigeonCms.MenuAdmin','Parameters','Parameters','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '583','it-IT','PigeonCms.MenuAdmin','LblAlias','Alias','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '584','it-IT','PigeonCms.MenuAdmin','LblRoute','Route','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '585','it-IT','PigeonCms.MenuAdmin','LblUseSsl','Use SSL','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '586','it-IT','PigeonCms.MenuAdmin','LblLink','Link','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '587','it-IT','PigeonCms.MenuAdmin','LblMasterpage','Masterpage','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '588','it-IT','PigeonCms.PermissionsControl','LblRead','Read','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '589','it-IT','PigeonCms.PermissionsControl','LblWrite','Write','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '590','it-IT','PigeonCms.PermissionsControl','LblPermissionId','ID','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '591','it-IT','PigeonCms.ModuleParams','LblSystemMessagesTo','System messages to','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '592','it-IT','PigeonCms.ModuleParams','LblDirectEditMode','Direct edit mode','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '593','it-IT','PigeonCms.MemberEditorControl','LblUsername','Username','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '594','it-IT','PigeonCms.MemberEditorControl','LblEmail','E-mail','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '595','it-IT','PigeonCms.MemberEditorControl','LblPassword','Password','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '596','it-IT','PigeonCms.MemberEditorControl','LblAllowMessages','Allow messages','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '597','it-IT','PigeonCms.MemberEditorControl','LblAllowEmails','Allow emails','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '598','it-IT','PigeonCms.MemberEditorControl','LblAccessCode','Access code','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '599','it-IT','PigeonCms.MemberEditorControl','LblAccessLevel','Access level','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '600','it-IT','PigeonCms.MembersAdmin','LblFilters','Filters','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '601','it-IT','PigeonCms.ModulesAdmin','Main','Main','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '602','it-IT','PigeonCms.ModulesAdmin','Menu','Menu','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '603','it-IT','PigeonCms.ModulesAdmin','Options','Options','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '604','it-IT','PigeonCms.ModulesAdmin','Security','Security','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '605','it-IT','PigeonCms.ModulesAdmin','Parameters','Parameters','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '606','it-IT','PigeonCms.ModulesAdmin','LblChange','change','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '607','it-IT','PigeonCms.ModulesAdmin','LblRecordId','ID','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '608','it-IT','PigeonCms.MembersAdmin','LblUpdateUser','Update user','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '609','it-IT','PigeonCms.FileUpload','Folder','Folder','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '610','it-IT','PigeonCms.UpdatesAdmin','LblInstall','Install','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '611','it-IT','PigeonCms.UpdatesAdmin','LblModules','Modules','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '612','it-IT','PigeonCms.UpdatesAdmin','LblTemplates','Templates','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '613','it-IT','PigeonCms.UpdatesAdmin','LblSql','Sql','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '614','it-IT','PigeonCms.LogsAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '615','it-IT','PigeonCms.LogsAdmin','LblRecordId','ID','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '616','it-IT','PigeonCms.LogsAdmin','LblCreated','Created','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '617','it-IT','PigeonCms.LogsAdmin','LblType','Type','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '618','it-IT','PigeonCms.LogsAdmin','LblModuleType','Module','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '619','it-IT','PigeonCms.LogsAdmin','LblView','View','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '620','it-IT','PigeonCms.LogsAdmin','LblIp','Ip','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '621','it-IT','PigeonCms.LogsAdmin','LblSessionId','Session ID','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '622','it-IT','PigeonCms.LogsAdmin','LblUser','User','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '623','it-IT','PigeonCms.LogsAdmin','LblUrl','Url','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '624','it-IT','PigeonCms.LogsAdmin','LblDescription','Description','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '625','it-IT','PigeonCms.OfflineAdmin','LblPageTemplate','Page template','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '626','it-IT','PigeonCms.AttributesAdmin','Name','Name','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '627','it-IT','PigeonCms.AttributesAdmin','FieldType','Field type','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '628','it-IT','PigeonCms.AttributesAdmin','MinValue','Min value','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '629','it-IT','PigeonCms.AttributesAdmin','MaxValue','Max value','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '630','it-IT','PigeonCms.AttributesAdmin','Enabled','Enabled','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '631','it-IT','PigeonCms.AttributesAdmin','AttributeValues','Attribute values','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '632','it-IT','PigeonCms.AttributesAdmin','Value','Value','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '633','it-IT','PigeonCms.AttributesAdmin','LblRecordInfo','Record info','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '634','it-IT','PigeonCms.AttributesAdmin','LblRecordId','ID','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '635','it-IT','PigeonCms.AttributesAdmin','LblCreated','Created','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '636','it-IT','PigeonCms.AttributesAdmin','LblLastUpdate','Last update','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '637','it-IT','PigeonCms.LoginForm','user','user','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '638','it-IT','PigeonCms.LoginForm','password','password','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '639','it-IT','PigeonCms.LoginForm','Username','Username','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '640','it-IT','PigeonCms.MembersAdmin','LblChangePassword','Change password','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '641','it-IT','PigeonCms.SectionsAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '642','it-IT','PigeonCms.SectionsAdmin','Main','Main','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '643','it-IT','PigeonCms.SectionsAdmin','Security','Security','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '644','it-IT','PigeonCms.SectionsAdmin','LblCssClass','Css class','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '645','it-IT','PigeonCms.CategoriesAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '646','it-IT','PigeonCms.CategoriesAdmin','LblSection','Section','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '647','it-IT','PigeonCms.CategoriesAdmin','LblCssClass','Css class','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '648','it-IT','PigeonCms.CategoriesAdmin','LblEnabled','Enabled','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '649','it-IT','PigeonCms.CategoriesAdmin','LblTitle','Title','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '650','it-IT','PigeonCms.CategoriesAdmin','LblDescription','Description','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '651','it-IT','PigeonCms.StaticPagesAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '652','it-IT','PigeonCms.ItemsAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '653','it-IT','PigeonCms.ItemsAdmin','Main','Main','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '654','it-IT','PigeonCms.ItemsAdmin','Security','Security','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '655','it-IT','PigeonCms.ItemsAdmin','Parameters','Parameters','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '656','it-IT','PigeonCms.ItemsAdmin','LblAlias','Alias','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '657','it-IT','PigeonCms.ItemsAdmin','LblCssClass','Css class','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '658','it-IT','PigeonCms.ItemsAdmin','LblRecordId','ID','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '659','it-IT','PigeonCms.ItemsAdmin','LblOrderId','Order Id','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '660','it-IT','PigeonCms.LabelsAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '661','it-IT','PigeonCms.LabelsAdmin','LblResourceSet','Resource set','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '662','it-IT','PigeonCms.LabelsAdmin','LblResourceId','Resource id','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '663','it-IT','PigeonCms.LabelsAdmin','LblTextMode','Text mode','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '664','it-IT','PigeonCms.LabelsAdmin','LblValue','Value','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '665','it-IT','PigeonCms.LabelsAdmin','LblComment','Comment','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '666','it-IT','PigeonCms.FilesManager','Size','Size','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '667','it-IT','PigeonCms.FilesManager','Meta','Meta data','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '668','it-IT','PigeonCms.PlaceholdersAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '669','it-IT','PigeonCms.UpdatesAdmin','LblFilters','filters','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '670','it-IT','PigeonCms.AppSettingsAdmin','LblDetails','Details','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '671','it-IT','PigeonCms.AppSettingsAdmin','LblName','Name','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '672','it-IT','PigeonCms.AppSettingsAdmin','LblTitle','Title','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '673','it-IT','PigeonCms.AppSettingsAdmin','LblValue','Value','SYSTEM','2',1)
GO
INSERT INTO #__labels (id,cultureName,resourceSet,resourceId,value,comment,textMode,isLocalized)  VALUES( '674','it-IT','PigeonCms.AppSettingsAdmin','LblInfo','Additional info','SYSTEM','2',1)
GO
SET IDENTITY_INSERT #__labels OFF
GO
SET IDENTITY_INSERT #__memberUsers ON
GO
INSERT INTO #__memberUsers (id,username,applicationName,email,comment,password,passwordQuestion,passwordAnswer,isApproved,lastActivityDate,lastLoginDate,lastPasswordChangedDate,creationDate,isOnLine,isLockedOut,lastLockedOutDate,failedPasswordAttemptCount,failedPasswordAttemptWindowStart,failedPasswordAnswerAttemptCount,failedPasswordAnswerAttemptWindowStart,enabled,accessCode,accessLevel,isCore,sex,companyName,vat,ssn,firstName,secondName,address1,address2,city,state,zipCode,nation,tel1,mobile1,website1,allowMessages,allowEmails)  VALUES( '1','admin','PigeonCms','','','admin','','',1,NULL,NULL,NULL,NULL,NULL,0,NULL,'0',NULL,'0',NULL,1,'','0',1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,0)
GO
INSERT INTO #__memberUsers (id,username,applicationName,email,comment,password,passwordQuestion,passwordAnswer,isApproved,lastActivityDate,lastLoginDate,lastPasswordChangedDate,creationDate,isOnLine,isLockedOut,lastLockedOutDate,failedPasswordAttemptCount,failedPasswordAttemptWindowStart,failedPasswordAnswerAttemptCount,failedPasswordAnswerAttemptWindowStart,enabled,accessCode,accessLevel,isCore,sex,companyName,vat,ssn,firstName,secondName,address1,address2,city,state,zipCode,nation,tel1,mobile1,website1,allowMessages,allowEmails)  VALUES( '2','manager','PigeonCms','','','manager','','',1,NULL,NULL,NULL,NULL,NULL,0,NULL,'0',NULL,'0',NULL,1,'','0',0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,0)
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
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','57','Labels','')
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
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','69','Help','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','70','Error page','Error page')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','71','Files upload','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'en-US','72','Form attributes','attributes admin')
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
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','57','Etichette','')
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
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','69','Guida','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','70','Error page','Error page')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','71','Caricamento files','')
GO
INSERT INTO #__menu_Culture (cultureName,menuId,title,titleWindow)  VALUES( 'it-IT','72','Attributi form','attributes admin')
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
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '74','labels admin','','0','content',1,'LabelsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'ModuleFullName:=|ModuleFullNamePart:=|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '75','logs','','0','content',1,'LogsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '76','offline','','0','content',1,'OfflineAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',1,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '78','items images','','0','content',1,'FilesManager','PigeonCms',NULL,'admin',NULL,'admin','0',1,'FileExtensions:=jpg;jpeg;gif|FileSize:=300|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Gallery|UploadFields:=3|NumOfFilesAllowed:=0|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|SuccessText:=File uploaded|ErrorText:=Error uploading file(s)',1,'3','Default.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '79','Items files','','0','content',1,'FilesManager','PigeonCms',NULL,'admin',NULL,'admin','0',1,'FileExtensions:=pdf|FileSize:=500|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Files|UploadFields:=3|NumOfFilesAllowed:=0|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|SuccessText:=File uploaded|ErrorText:=Error uploading file(s)',1,'3','Default.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '82','top submenu','','0','Toolbar2',1,'TopMenu','PigeonCms',NULL,'admin',NULL,'admin','0',0,'MenuType:=|MenuId:=none|ItemSelectedClass:=selected|ItemLastClass:=last|ShowPagePostFix:=1|MenuLevel:=1|ShowChild:=0',0,'2','TopMenu.ascx','0','','0','','submenulist','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '83','labels admin popup','','0','content',1,'LabelsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'ModuleFullName:=',1,'3','Default.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '84','static pages admin popup','','0','content',1,'StaticPagesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'PageName:=|TargetDocsUpload:=71',1,'3','Default.ascx','0','','0','','','2','2',1,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '85','items admin popup','','0','content',1,'ItemsAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'TargetImagesUpload:=61|TargetFilesUpload:=62',1,'3','Default.ascx','0','','0','','','2','2',1,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '86','placeholders admin popup','','0','content',1,'PlaceholdersAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'',1,'3','Default.ascx','0','','0','','','2','2',1,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '87',NULL,'','1','content',1,'StaticPage','PigeonCms',NULL,'admin',NULL,'admin','0',0,'PageName:=error',0,'3','StaticPage.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '88',NULL,'','2','Pathway',1,'Breadcrumbs','PigeonCms',NULL,'admin',NULL,'admin','0',0,'MenuType:=mainmenu',0,'2','Breadcrumbs.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '89',NULL,'','3','content',1,'FilesManager','PigeonCms',NULL,'admin',NULL,'admin','0',1,'AllowFilesUpload:=1|AllowFilesSelection:=1|AllowFilesEdit:=1|AllowFilesDel:=1|AllowNewFolder:=1|AllowFoldersNavigation:=1|TypeParamRequired:=0|FileExtensions:=pdf;doc;docx;xls;xlsx;jpg;jpeg;gif;png;zip|FileSize:=1024|FileNameType:=0|FilePrefix:=file_|FilePath:=~/Public/Docs|UploadFields:=3|NumOfFilesAllowed:=0|CustomWidth:=|CustomHeight:=|HeaderText:=|FooterText:=|SuccessText:=File(s) uploaded|ErrorText:=Error uploading file(s)',0,'3','Default.ascx','0','','0','','','2','2',0,NULL,NULL,NULL,'0','')
GO
INSERT INTO #__modules (id,title,content,ordering,templateBlockName,published,moduleName,moduleNamespace,dateInserted,userInserted,dateUpdated,userUpdated,accessType,showTitle,moduleParams,isCore,menuSelection,currView,permissionId,accessCode,accessLevel,cssFile,cssClass,useCache,useLog,directEditMode,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,systemMessagesTo)  VALUES( '90',NULL,'','4','content',1,'AttributesAdmin','PigeonCms',NULL,'admin',NULL,'admin','0',1,'|',0,'3','Default.ascx','0','','0','','','2','2',0,'0','0','','0','')
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
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','74','Labels')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','75','Logs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','76','Site offline')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','83','Labels')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','84','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','85','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','87','Error page')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','88','breadcrumbs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','89','Files upload')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'en-US','90','Form attributes')
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
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','74','Etichette')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','75','Logs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','76','Sito offline')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','83','Gestione etichette')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','84','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','85','')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','87','Error page')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','88','breadcrumbs')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','89','Caricamento files')
GO
INSERT INTO #__modules_Culture (cultureName,moduleId,title)  VALUES( 'it-IT','90','Attributi form')
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
INSERT INTO #__sections (id,enabled,accessType,permissionId,accessCode,accessLevel,writeAccessType,writePermissionId,writeAccessCode,writeAccessLevel,maxItems,maxAttachSizeKB,cssClass)  VALUES( '1',1,'0','0','','0','1','33','','0','0','0',NULL)
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
INSERT INTO #__templateBlocks (name,title,enabled,orderId)  VALUES( 'Advert1',NULL,1,'6')
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
