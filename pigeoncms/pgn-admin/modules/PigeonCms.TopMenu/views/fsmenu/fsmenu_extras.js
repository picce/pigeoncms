/*

Here are some extras for the script that didn't make it into the standard .JS file.
I use some of these on my site, so feel free to add the effects to yours. Included are:

 Menu Hide On Click: Hides all menus when the document is clicked.
      Menu Floating: Scrolls the menu with the page.
      Title Display: Shows your menu link TITLE="" attributes in a separate display area.
 Menu Repositioning: Stops menus from displaying offscreen.
  Select Box Hiding: Stops boxes covering over menus in Internet Explorer.
        Link Fading: Fades between over/out colours for links in the menu.
       Current Page: Applies a CSS style to the item representing the current page.

*/







// MENU HIDE ON CLICK: Hides all menus when the document is clicked.
// To activate:
//  1) Paste this into your script.
//  2) If you are using multiple menu objects, call hideAll for each one.

addEvent(document, 'click',  function() {
 listMenu.hideAll();
});







// MENU FLOATING: This will scroll a menu with the page.
// To activate:
//  1) Wrap each menu with a tag like this: <div id="abcdef"> <MENU DATA GOES HERE> </div>
//     That should either be in a column by itself, or have POSITION:ABSOLUTE set in its CSS.
//  2) Add the ID of the DIVs wrapping each menu to the fsmScrollHandler() function below.
//  3) Paste the script below at the end of fsmenu.js

// If you have good CSS knowledge, consider implementing a position:fixed solution in supported
// browsers. This is a general, JS-only floating function designed to work with most layouts.

function fsmScrollHandler()
{
 floatElement('abcdef');
 // ADD OTHER PAGE ELEMENTS CONTAINING MENUS HERE.
};

function floatElement(containerID)
{
 var container = getRef(containerID);
 if (!container) return;
 container.style.paddingTop = (typeof window.pageYOffset == 'number' ? window.pageYOffset :
  (document.body ? document.body.scrollTop || document.documentElement.scrollTop : 0)) + 'px';
 window.status = container.style.paddingTop;
};
if (''+window.onscroll=='undefined') setInterval('fsmScrollHandler()', 500);
else addEvent(window, 'scroll', fsmScrollHandler);







// TITLE DISPLAY: Shows your link TITLE="" attributes in the page itself.
// To activate:
//  1) Include a target element like this in your page: <div id="listMenuTitles"></div>
//     This is the element that will contain the titles.
//  2) Add title display lines like this to the function below:
//     titleDisplay('id-of-menu-containing-links', 'target-element-id');
//  3) Paste this script into your document or the fsmenu.js file.

function titleDisplaySetup()
{
 titleDisplay('listMenuRoot', 'listMenuTitles');
 // ADD DIFFERENT TITLE AREAS HERE! Each must have a 'target' area in your page.
 //titleDisplay('otherMenuRoot', 'otherMenuTitles');
};
addEvent(window, 'load', titleDisplaySetup);

function titleDisplay(menuID, target)
{
 var nav = getRef(menuID);
 addEvent(nav, 'mouseover', new Function('e',
  'e=e||window.event; var lt=getRef("' + target + '"); if (lt) {' +
  'while (lt.firstChild) lt.removeChild(lt.firstChild);' +
   'e=e.target||e.srcElement; while(e && (!e.title&&!e.title_orig)) e=e.parentNode;' +
   'if (e && e.getAttribute) {' +
    'var t = e.getAttribute("title");' +
    'if (t) { e.title_orig = t; e.setAttribute("title", "") }' +
    'lt.appendChild(document.createTextNode(e.title_orig));' +
  '}}'));
 addEvent(nav, 'mouseout', new Function('e',
  'var lt=getRef("' + target + '"); if (lt) while (lt.firstChild) lt.removeChild(lt.firstChild)'));
}






// MENU REPOSITIONING: This will stop menus showing outside visible screen boundaries.
// To activate:
//  1) Paste this after you create your "new FSMenu" object.
//  2) Make sure the last line contains the correct menu object name, and
//     duplicate for each of the menu objects to which you want this to apply.

page.winW=function()
 { with (this) return Math.max(minW, MS?win.document[db].clientWidth:win.innerWidth) };
page.winH=function()
 { with (this) return Math.max(minH, MS?win.document[db].clientHeight:win.innerHeight) };
page.scrollX=function()
 { with (this) return MS?win.document[db].scrollLeft:win.pageXOffset };
page.scrollY=function()
 { with (this) return MS?win.document[db].scrollTop:win.pageYOffset };

function repositionMenus(mN) { with (this)
{
 var menu = this.menus[mN].lyr;

 // Showing before measuring corrects MSIE bug.
 menu.sty.display = 'block';
 // Reset to and/or store original margins.
 if (!menu._fsm_origML) menu._fsm_origML = menu.ref.currentStyle ?
  menu.ref.currentStyle.marginLeft : (menu.sty.marginLeft || 'auto');
 if (!menu._fsm_origMT) menu._fsm_origMT = menu.ref.currentStyle ?
  menu.ref.currentStyle.marginTop : (menu.sty.marginTop || 'auto');
 menu.sty.marginLeft = menu._fsm_origML;
 menu.sty.marginTop = menu._fsm_origMT;

 // Calculate absolute position within document.
 var menuX = 0, menuY = 0,
  menuW = menu.ref.offsetWidth, menuH = menu.ref.offsetHeight,
  vpL = page.scrollX(), vpR = vpL + page.winW() - 16,
  vpT = page.scrollY(), vpB = vpT + page.winH() - 16;
 var tmp = menu.ref;
 while (tmp)
 {
  menuX += tmp.offsetLeft;
  menuY += tmp.offsetTop;
  tmp = tmp.offsetParent;
 }

 // Compare position to viewport, reposition accordingly.
 var mgL = 0, mgT = 0;
 if (menuX + menuW > vpR) mgL = vpR - menuX - menuW;
 if (menuX + mgL < vpL) mgL = vpL - menuX;
 if (menuY + menuH > vpB) mgT = vpB - menuY - menuH;
 if (menuY + mgT < vpT) mgT = vpT - menuY;

 if (mgL) menu.sty.marginLeft = mgL + 'px';
 if (mgT) menu.sty.marginTop = mgT + 'px';
}};

// Set this to process menu show events for a given object.
addEvent(listMenu, 'show', repositionMenus, true);







// SELECT BOX / IFRAME HIDING: This will help mixing menus and forms/frames/Flash/etc.
// Pick one (not both) of the below two methods. To use either, copy and paste beneath
// your menu data and duplicate the last addEvent lines to apply to each of your menus.


// Method one.

FSMenu.prototype.toggleElements = function(show)
{
 // CONFIGURATION: Here's a list of tags that will be hidden by menus. Modify to fit your site.
 var tags = ['select', 'iframe'];

 if (!isDOM) return;
 if (!show) for (var m in this.menus) if (this.menus[m].visible) return;
 for (var t in tags)
 {
  var elms = document.getElementsByTagName(tags[t]);
  for (var e = 0; e < elms.length; e++) elms[e].style.visibility = show ? 'visible' : 'hidden';
 }
};
addEvent(listMenu, 'show', function() { this.toggleElements(0) }, 1);
addEvent(listMenu, 'hide', function() { this.toggleElements(1) }, 1);


// Here's a second method. This only works in IE 5.5+ on Windows, but it doesn't make
// select boxes appear and disappear (menus cleanly cover them).

FSMenu.prototype.ieSelBoxFixShow = function(mN) { with (this)
{
 var m = menus[mN];
 if (!isIE || !window.createPopup) return;
 if (navigator.userAgent.match(/MSIE ([\d\.]+)/) && parseFloat(RegExp.$1) > 6.5)
  return;
 // Create a new transparent IFRAME if needed, and insert under the menu.
 if (!m.ifr)
 {
  m.ifr = document.createElement('iframe');
  m.ifr.src = 'about:blank';
  with (m.ifr.style)
  {
   position = 'absolute';
   border = 'none';
   filter = 'progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0)';
  }
  m.lyr.ref.parentNode.insertBefore(m.ifr, m.lyr.ref);
 }
 // Position and show it on each call.
 with (m.ifr.style)
 {
  left = m.lyr.ref.offsetLeft + 'px';
  top = m.lyr.ref.offsetTop + 'px';
  width = m.lyr.ref.offsetWidth + 'px';
  height = m.lyr.ref.offsetHeight + 'px';
  visibility = 'visible';
 }
}};
FSMenu.prototype.ieSelBoxFixHide = function(mN) { with (this)
{
 if (!isIE || !window.createPopup) return;
 var m = menus[mN];
 if (m.ifr) m.ifr.style.visibility = 'hidden';
}};

addEvent(listMenu, 'show', function(mN) { this.ieSelBoxFixShow(mN) }, 1);
addEvent(listMenu, 'hide', function(mN) { this.ieSelBoxFixHide(mN) }, 1);







// LINK FADING: Fades between over/out colours for links in the menu.
// To activate:
//  1) Paste the FSMenu.prototype function into the script anywhere.
//     You might want to put them into fsmenu.js, or around your menu data.
//  2) Paste the "Activation" section beneath your menu data, and ensure that
//     you follow the instructions within it.

FSMenu.prototype.setLinkFading = function(linkClasses, linkSpeed) { with (this)
{
 // Store the link classes in the menu object.
 this.linkClasses = linkClasses || {};
 this.linkSpeed = linkSpeed || 100;
 
 // Find the root menu.
 var mRoot = null;
 for (var m in menus) if (menus[m].isRoot) mRoot = menus[m];
 if (!mRoot) return;

 // Find all link tags and add ID/timers/counters/mouse handlers...
 var links = mRoot.lyr.ref.getElementsByTagName('a'), i = links.length;
 while (i--)
 {
  if (!links[i].id) links[i].id = myName + '-linkfade-' + i;
  links[i].__lf_timer = 0;
  links[i].__lf_count = 0;
  addEvent(links[i], 'mouseover', new Function(myName + '.linkFade("' + links[i].id + '", 1)'));
  addEvent(links[i], 'mouseout', new Function(myName + '.linkFade("' + links[i].id + '", 0)'));
 }
}};

FSMenu.prototype.linkFade = function(link, doShow) { with (this)
{
 // Repeatedly called to animate a link colour in and out.

 link = document.getElementById(link);
 clearTimeout(link.__lf_timer);

// If we're hiding, delay until the link is no longer highlighted.
 var fadeOK = doShow || !link.className || !cssLitClass ||
  (link.className.indexOf(cssLitClass) == -1);

 if (fadeOK)
 {
  var linkClass = linkClasses[link.className || 'standard'] || linkClasses.standard;
  var dim = linkClass.dim, lit = linkClass.lit;

  // Increment the fading counter in the proper direction and speed.
  link.__lf_count = Math.max(0, Math.min(link.__lf_count+(2*doShow-1)*linkSpeed, 100));
 
  // Since Konqueror as of v3.1 doesn't support Number.toString(radix), we need a hack.
  // What the heck were they thinking/smoking when they omitted that? :P
  var col = '#', nc, hexD = '0123456789ABCDEF';
  // Loop through dim/lit arrays, to calculate 3 new hex pairs (0xRR, 0xGG and 0xBB).
  for (var i=0; i<3; i++)
  {
   // Make a new hex pair based on weighted averages of the out/over array indices.
   nc = parseInt(dim[i] + (lit[i]-dim[i])*(link.__lf_count/100));
   col += hexD.charAt(Math.floor(nc/16)).toString() + hexD.charAt(nc%16);
  }
  // Assign to the background of the link.
  link.style.backgroundColor = col;
 }

 // Repeat in 50ms if we're delaying the fade or the fade isn't done yet.
 if (!fadeOK || (link.__lf_count % 100 > 0)) link.__lf_timer = setTimeout(this.myName +
  '.linkFade("' + link.id + '",' + doShow + ')', 50);
 }
};


// Activation: This must be pasted beneath your activateMenu() call.
addEvent(window, 'load', function() {
 // You must call menuObjectName.setLinkFading for each of your menu objects.
 // Pass an associative array {} that contains a list of classnames.
 // You must include a 'standard' class which applies to menu items that have no
 // other classname set like <a class="special"> in the HTML.
 // You can also optionally specify a "highlighted" class that will apply
 // to lit items (or whatever your menu cssLitClass is).
 // You can also style other classes individually -- here I am applying
 // different styles to <a class="special"> as an example.
 // Finally, pass a "speed" parameter to setLinkFading.
 
 // Each class is formatted like so with colour values 0-255:
 // 'classname': { dim: [RR, GG, BB], lit: [RR, GG, BB] }

 listMenu.setLinkFading({
  'standard': { dim: [240,240,248], lit: [64,80,192] },
  'highlighted': { dim: [240,240,248], lit: [64,80,192] },
  'special': { dim: [240,240,248], lit: [200,0,0] },
  'special highlighted': { dim: [240,240,248], lit: [200,200,0] }
 }, 10);

});







// CURRENT PAGE: Paste this anywhere. Include the ID of your menu <UL>, and call
// it once onload for each menu object you create.
// Note: You will probably have to edit this to make it work. The key line is
// the one that tests location.pathname (which is like '/folder/file.html'
// without any '?querystring' or '#bookmark'). I am trying to find the longest
// matching item and highlight that and all its parent links.
// Note 2: Add a rule like this:
//   .current-page { font-weight: bold }
// or similar to your stylesheet of course :).

function activePageHighlight(elm)
{
 if (typeof elm == 'string') elm = document.getElementById(elm);
 if (!elm) return;
 var links = elm.getElementsByTagName('a'), chosen = null;

 for (var i = 0; i < links.length; i++)
 {
  if (links.item(i).href.indexOf(location.pathname) > -1)
   if (!chosen || links[i].href.length > chosen.href.length) chosen = links[i];
 }

 while (chosen && chosen.className != 'menulist')
 {
  if (chosen.nodeName.toLowerCase() == 'li')
  {
   chosen.getElementsByTagName('a').item(0).className = 'current-page';
  }
  chosen = chosen.parentNode;
 }
};
// Activation: Include the ID for your menu in here.
addEvent(window, 'load', new Function('activePageHighlight("listMenuRoot")'));
