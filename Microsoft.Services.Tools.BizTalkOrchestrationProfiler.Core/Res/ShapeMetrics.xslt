<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	

<xsl:param name="OrchName" />
 	      
<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<html>
			<HEAD>
				<link href="../CommentReport.css" type="text/css" rel="stylesheet"></link>
			</HEAD>
			
			<body>
			<SPAN CLASS="StartOfFile">Orchestration Shape Metrics</SPAN>

            <table class="TableData">
				<tr>
					<td width="10"></td>
					<td valign="top"><IMG SRC='../Orchestration.jpg' VALIGN="center"/></td>
					<td valign="center" CLASS="PageTitle">Orchestration Shape Metrics : <xsl:value-of select="$OrchName"/></td>
				</tr>
			</table>

			<br/><br/>
			
            <table class="TableData" width="90%">
				<tr CLASS="TableTitle">
					<td width="10"></td>
					<td><nobr>Shape Id</nobr></td>
					<td><nobr>Shape Name</nobr></td>
					<td><nobr>Entry Count</nobr></td>
					<td><nobr>Exit Count</nobr></td>
					<td><nobr>Success Rate (%)</nobr></td>
					<td><nobr>MinDuration (ms)</nobr></td>
					<td><nobr>MaxDuration (ms)</nobr></td>
					<td><nobr>AvgDuration (ms)</nobr></td>
				</tr>
				
				<xsl:for-each select="CoverageShapes/CoverageShape">
					<xsl:if test="string-length(Text)>0">
						<tr>
							<td width="10"></td>
							<td><xsl:value-of select="position()"/></td>
							<td><xsl:value-of select="Text"/></td>
							<td><xsl:value-of select="EntryCount"/></td>
							<td><xsl:value-of select="ExitCount"/></td>
							<td><xsl:value-of select="SuccessRate"/></td>
							<td><xsl:value-of select="MinDurationMillis"/></td>
							<td><xsl:value-of select="MaxDurationMillis"/></td>
							<td><xsl:value-of select="AvgDurationMillis"/></td>
						</tr>
					</xsl:if>
				</xsl:for-each>
			</table>
						
			</body>
		</html>
	</xsl:template>
	
	<xsl:template match="text()">
	</xsl:template>

</xsl:stylesheet>
