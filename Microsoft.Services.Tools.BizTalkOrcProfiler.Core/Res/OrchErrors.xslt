<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	

<xsl:param name="OrchName" />
<xsl:param name="ErrorCount" />
 	      
<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<html>
			<head>
				<link href="CommentReport.css" type="text/css" rel="stylesheet"></link>
				<script language="javaScript">

					function ShowItem( itemName )
					{
						eval( 'var itemStyle = ' + itemName + '.style' )

						if( itemStyle.display=="none")
							itemStyle.display="block";
						else
							itemStyle.display="none";
					}

				</script>
			</head>
			
			<body>
			<SPAN CLASS="StartOfFile">Errors Encountered During Orchestration Execution</SPAN>

            <table class="TableData">
				<tr>
					<td width="10"></td>
					<td valign="top"><IMG SRC='../Orchestration.jpg' VALIGN="center"/></td>
					<td valign="center" CLASS="PageTitle">Orchestration Error Info : <xsl:value-of select="$OrchName"/></td>
				</tr>
				<tr>
					<td width="10"></td>
					<td class="TableTitle" colspan="2"><br/><br/><nobr>Top <xsl:value-of select="$ErrorCount"/> Errors</nobr></td>
				</tr>
			</table>

			<br/>
			
            <table class="TableData" width="90%">
				<tr>
					<td width="10"></td>
					<td valign="top">
						<ul>						
							<xsl:for-each select="//Error">
								
								<li type="square"><span class="TableTitle" style="cursor: hand;"><xsl:text> </xsl:text><u>											
									<xsl:element name="a">
										<xsl:attribute name="onClick">ShowItem( 'Item<xsl:number/>' );</xsl:attribute>
										<xsl:value-of select="Messsage"/>
									</xsl:element>
									
									</u></span>	
									
									<xsl:element name="div">
										<xsl:attribute name="id">Item<xsl:number/></xsl:attribute>
										<xsl:attribute name="style">display: none;</xsl:attribute>
											<ul><span class="TableData"><xsl:value-of select="StackTrace"/></span></ul>
									</xsl:element>
								</li>
								<br/><br/>		
							</xsl:for-each>
						</ul>
					</td>
				</tr>
			</table>
						
			</body>
		</html>
	</xsl:template>
	
	<xsl:template match="text()">
	</xsl:template>

</xsl:stylesheet>
