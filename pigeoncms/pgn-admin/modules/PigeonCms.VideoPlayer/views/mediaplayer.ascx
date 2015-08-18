<%@ Import Namespace="PigeonCms" %>
<%@ Control EnableViewState="false" Language="C#" AutoEventWireup="true" CodeFile="mediaplayer.ascx.cs" Inherits="PigeonCms_VideoPlayer_mediaplayer" %>

<!--left: 17px; width: 320px; top: 36px; height: 304px;-->
<object width='<%=base.Width %>' height='<%=base.Height %>' 
classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" 
style="" id="WMP7">
<param value='<%=base.File %>' name="URL" />
<param value="1" name="rate" />
<param value="0" name="balance" />
<param value="0.4669999" name="currentPosition" />
<param value="" name="defaultFrame" />
<param value="1" name="playCount" />
<param value="-1" name="autoStart" />
<param value="0" name="currentMarker" />
<param value="-1" name="invokeURLs" />
<param value="" name="baseURL" />
<param value="50" name="volume" />
<param value="0" name="mute" />
<param value="full" name="uiMode" />
<param value="0" name="stretchToFit" />
<param value="0" name="windowlessVideo" />
<param value="-1" name="enabled" />
<param value="-1" name="enableContextMenu" />
<param value="0" name="fullScreen" />
<param value="" name="SAMIStyle" />
<param value="" name="SAMILang" />
<param value="" name="SAMIFilename" />
<param value="" name="captioningID" />
<param value="0" name="enableErrorDialogs" />
<param value="8467" name="_cx" />
<param value="8043" name="_cy" /> </object>