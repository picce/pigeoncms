<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" encoding="UTF-8"/>
    <xsl:template match="/">
        <html>
            <head>
                <title>
                    <xsl:value-of select="offline/title" />
                </title>
                <style type="text/css">
                    body{
                        font-family: arial;
                    }
                    pre{ font-family: arial; }
                </style>
            </head>

            <body>
                <div id="container">
                    <div id="pageHeader">
                        <h1>
                            <xsl:value-of select="offline/title" />
                        </h1>
                    </div>

                    <div id="pageContent">
                        <h2>
                            <pre>
                                <xsl:value-of select="offline/message" />
                            </pre>
                        </h2>
                    </div>


                    <div id="pageFooter">
                        powered by <a href="http://www.pigeoncms.com" target="_blank">pigeoncms</a>
                    </div>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>