<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	

<xsl:param name="ImgFile" />
<xsl:param name="OrchId" />
<xsl:param name="OrchName" />
<xsl:param name="AsmName" />
<xsl:param name="ImgFileOverall" />
<xsl:param name="ImgFileSuccess" />
<xsl:param name="PcOverall" />
<xsl:param name="PcSuccess" />

<xsl:param name="WriteFurtherInfo" />
<xsl:param name="AvgDurationMillis" />
<xsl:param name="MaxDurationMillis" />
<xsl:param name="MinDurationMillis" />
<xsl:param name="NumCompleted" />
<xsl:param name="NumStarted" />
<xsl:param name="NumTerminated" />
<xsl:param name="ErrorCount" />
 	      
<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<html>
			<HEAD>
				<link href="../CommentReport.css" type="text/css" rel="stylesheet"></link>
			</HEAD>
			
			<body>
			<SPAN CLASS="StartOfFile">Orchestration Overview</SPAN>
									
			<table class="TableData">
				<tr>
					<td width="10"></td>
					<td valign="top"><IMG SRC='../Orchestration.jpg' VALIGN="center"/></td>
					<td valign="center" CLASS="PageTitle">Orchestration : <xsl:value-of select="$OrchName"/>
					<span class="Copyright"><br/>Assembly: <xsl:value-of select="$AsmName"/><br/><br/></span></td>
				</tr>
			</table>
			<BR/><BR/>

			<table class="TableData" border="1" align="center" width="65%" cellspacing="0" cellpadding="0">
				<tr>
					<td align="center">
					
						<!-- Charts -->
						<table class="TableData" width="100%" align="center" bgcolor="#c0c0c0">
						<tr>
							<td colspan="4" class="TableTitle" align="center"><nobr><u>Coverage Summary</u></nobr></td>
						</tr>
						</table>
						<BR/>			
						
						<table class="TableData" width="100" align="center">
						<tr>
							<td align="center">				
								<xsl:element name="IMG">
									<xsl:attribute name="SRC"><xsl:value-of select="$ImgFileOverall" /></xsl:attribute>							
								</xsl:element>					    
							</td>
							<td>
								<xsl:value-of select="$PcOverall" /> % Overall Coverage
							</td>
                            <!--
							<td align="center">				
								<xsl:element name="IMG">
									<xsl:attribute name="SRC"><xsl:value-of select="$ImgFileSuccess" /></xsl:attribute>							
								</xsl:element>					    
							</td>
							<td>
								<xsl:value-of select="$PcSuccess" /> % of 'hit' shapes completed successfully every time they were invoked
							</td>
                            -->
						</tr>
						</table>
									
						
						<table class="TableData" width="100" align="center">
						<tr>
							<td colspan="1" class="TableTitle" align="right"><nobr>Active Instances: </nobr></td>
							<td colspan="1" class="TableData" align="left"><xsl:value-of select="$NumStarted"/></td>
						</tr>
						<tr>
							<td colspan="1" class="TableTitle" align="right"><nobr>Completed Instances: </nobr></td>
							<td colspan="1" class="TableData" align="left"><xsl:value-of select="$NumCompleted"/></td>
						</tr>
						<tr>
							<td colspan="1" class="TableTitle" align="right"><nobr>Terminated Instances: </nobr></td>
							<td colspan="1" class="TableData" align="left"><xsl:value-of select="$NumTerminated"/></td>
						</tr>
						<tr>
							<td colspan="1" class="TableTitle" align="right"><nobr>Minimum Duration (ms): </nobr></td>
							<td colspan="1" class="TableData" align="left"><xsl:value-of select="$MinDurationMillis"/></td>
						</tr>
						<tr>
							<td colspan="1" class="TableTitle" align="right"><nobr>Maximum Duration (ms): </nobr></td>
							<td colspan="1" class="TableData" align="left"><xsl:value-of select="$MaxDurationMillis"/></td>
						</tr>
						<tr>
							<td colspan="1" class="TableTitle" align="right"><nobr>Average Duration (ms): </nobr></td>
							<td colspan="1" class="TableData" align="left"><xsl:value-of select="$AvgDurationMillis"/></td>
						</tr>
						</table>
						
						<xsl:if test="string-length($WriteFurtherInfo)>0">
							<BR/>
							<table class="TableData" width="100" align="center">
							<tr>
								<td colspan="4" class="TableTitle" align="center"><nobr><u>
									<xsl:element name="A">
										<xsl:attribute name="href"><xsl:value-of select="$OrchId" />ShapeDetail.html</xsl:attribute>Detailed Shape Info</xsl:element></u></nobr></td>
							</tr>
							<tr>
								<td colspan="4" class="TableTitle" align="center"><nobr><u>
									<xsl:element name="A">
										<xsl:attribute name="href"><xsl:value-of select="$OrchId" />ShapeDetailL20.html</xsl:attribute>Top 20 longest running shapes</xsl:element></u></nobr></td>
							</tr>
							<tr>
								<td colspan="4" class="TableTitle" align="center"><nobr><u>
									<xsl:element name="A">
										<xsl:attribute name="href"><xsl:value-of select="$OrchId" />ShapeDetailLS20.html</xsl:attribute>Top 20 least successful shapes</xsl:element></u></nobr></td>
							</tr>
                                <xsl:if test="$ErrorCount>0">
                                    <tr>
                                        <td colspan="4" class="TableTitle" align="center">
                                            <nobr>
                                                <u>
                                                    <xsl:element name="A">
                                                        <xsl:attribute name="href">
                                                            <xsl:value-of select="$OrchId" />Errors.html
                                                        </xsl:attribute>Top <xsl:value-of select="$ErrorCount"/> Errors
                                                    </xsl:element>
                                                </u>
                                            </nobr>
                                        </td>
                                    </tr>
                                </xsl:if>
							</table>
						</xsl:if>
						<BR/>
						
					</td>
				</tr>
			</table>						
	
			<!-- Image -->
			<xsl:if test="string-length($ImgFile)>0">
			    <BR/><BR/>
			    <table class="TableData" width="100%">
			    <tr>
			        <td align="center">
					
			            <xsl:element name="IMG">
							<xsl:attribute name="SRC"><xsl:value-of select="$ImgFile" /></xsl:attribute>							
				        </xsl:element>	
				        
			        </td>
			    </tr>
			    </table>
			</xsl:if>
						
			</body>
		</html>
	</xsl:template>
	
	<xsl:template match="text()">
	</xsl:template>

</xsl:stylesheet>
