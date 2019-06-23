<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	
 	      
<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<html>
			<HEAD>
				<link href="../CommentReport.css" type="text/css" rel="stylesheet"></link>
			</HEAD>
			
			<body>
			<SPAN CLASS="StartOfFile">Coverage Report</SPAN>

            <table class="TableData">
				<tr>
					<td width="10"></td>
					<td valign="top"></td>
					<td valign="center" CLASS="PageTitle">Orchestration Profiler Coverage Report</td>
				</tr>
			</table>
		
			<!-- Overview -->
			<BR/><BR/>
			
            <table class="TableData">
				<tr>
					<td width="10"></td>
					<td valign="top"></td>
					<td valign="center" CLASS="TableTitle"><xsl:apply-templates mode="summary"/></td>
				</tr>
				<tr>
					<td width="10"></td>
					<td valign="top"></td>
					<td valign="center" CLASS="TableTitle">
					
						<br/>
						<br/>
						<span class="TableTitle">Orchestrations Ranked By Overall Coverage</span>
						<br/>
						<br/>
						
						<table class="TableData" width="100" align="left" border="1" cellspacing="0" cellpadding="2">
						<tr>
							<td align="left" class="TableTitle" bgcolor="#c0c0c0">
								<nobr>Level of Coverage (%)</nobr>
							</td>
							<td align="left" class="TableTitle" bgcolor="#c0c0c0">
								<nobr>Orchestration Name</nobr>
							</td>
						</tr>
						
						<xsl:apply-templates select="CoverageReport/Orchestrations/Orchestration[OverallCoverage != '0']" mode="list">
							<xsl:sort select="OverallCoverage" order="ascending" />
						</xsl:apply-templates>
						
						</table>					
					
					</td>
				</tr>			
				<tr>
					<td width="10"></td>
					<td valign="top"></td>
					<td valign="center" CLASS="TableTitle">
					
						<br/>
						<br/>
						<span class="TableTitle">Orchestrations With No Coverage</span>
						<br/>
						<br/>
						
						<table class="TableData" width="100" align="left" border="1" cellspacing="0" cellpadding="2">
						<tr>
							<td align="left" class="TableTitle" bgcolor="#c0c0c0">
								<nobr>Level of Coverage (%)</nobr>
							</td>
							<td align="left" class="TableTitle" bgcolor="#c0c0c0">
								<nobr>Orchestration Name</nobr>
							</td>
						</tr>
						
						<xsl:apply-templates select="CoverageReport/Orchestrations/Orchestration[OverallCoverage = '0']" mode="list">
							<xsl:sort select="Name" />
						</xsl:apply-templates>
						
						</table>					
					
					</td>
				</tr>
			</table>
									
			</body>
		</html>
	</xsl:template>
	
	<xsl:template match="CoverageReport/Orchestrations/Orchestration" mode="list">
	
		<tr>
			<td>
				<table width="100%" bgcolor="red" cellpadding="0" cellspacing="0">
					<tr>
						<xsl:element name="td">
							<xsl:attribute name="width"><xsl:value-of select="OverallCoverage" />%</xsl:attribute>
							<xsl:attribute name="bgcolor">green</xsl:attribute>
							<xsl:attribute name="class">TableData</xsl:attribute>
							<nobr>
								<xsl:if test="OverallCoverage != '0'">
									<xsl:value-of select="OverallCoverage" />
								</xsl:if>
							</nobr>
						</xsl:element>
						<xsl:element name="td">
							<xsl:attribute name="width">100%</xsl:attribute>
							<xsl:attribute name="bgcolor">red</xsl:attribute>
							<xsl:attribute name="class">TableData</xsl:attribute>
							<nobr>
								<xsl:if test="OverallCoverage = '0'"><xsl:text>0</xsl:text></xsl:if>
							</nobr>
						</xsl:element>
					</tr>
				</table>
			</td>
			<td>
				<xsl:element name="A">
					<xsl:attribute name="HREF">Orchestration/<xsl:value-of select="Id" />.html</xsl:attribute>
					<xsl:attribute name="CLASS">TableData</xsl:attribute>
					<xsl:value-of select="Name" />
				</xsl:element>
			</td>
		</tr>
		
	</xsl:template>
	
	<xsl:template match="CoverageReport/Summary" mode="summary">
	
		<span class="TableTitle">Number of orchestrations profiled: <xsl:value-of select="numOrchestrations" /></span>
		<br/>
		<br/>
		
		<table class="TableData" width="100" align="left" border="1" cellspacing="0" cellpadding="2">
		<tr>
			<td align="left" class="TableTitle" bgcolor="#c0c0c0">
				<nobr>Level of Coverage</nobr>
			</td>
			<td align="left" class="TableTitle" bgcolor="#c0c0c0">
				<nobr>Number of Orchestrations</nobr>
			</td>
		</tr>
		<tr>
			<td align="left">
				<nobr>Under 20%</nobr>
			</td>
			<td align="left">
				<nobr><xsl:value-of select="countUnder20" /></nobr>
			</td>
		</tr>
		<tr>
			<td align="left">
				<nobr>20% - 40%</nobr>
			</td>
			<td align="left">
				<nobr><xsl:value-of select="count20to40" /></nobr>
			</td>
		</tr>
		<tr>
			<td align="left">
				<nobr>40 - 60%</nobr>
			</td>
			<td align="left">
				<nobr><xsl:value-of select="count40to60" /></nobr>
			</td>
		</tr>
		<tr>
			<td align="left">
				<nobr>60% - 80%</nobr>
			</td>
			<td align="left">
				<nobr><xsl:value-of select="count60to80" /></nobr>
			</td>
		</tr>
		<tr>
			<td align="left">
				<nobr>80% - 100%</nobr>
			</td>
			<td align="left">
				<nobr><xsl:value-of select="count80to100" /></nobr>
			</td>
		</tr>
		</table>
			
	</xsl:template>
	
	<xsl:template match="CoverageReport/Orchestrations/Orchestration">
			
			<table class="TableData" width="100" align="center" border="1">
			<tr>
				<td colspan="4" class="TableTitle" align="left">
					
					<nobr>
			        <xsl:element name="IMG">
						<xsl:attribute name="WIDTH">50</xsl:attribute>
						<xsl:attribute name="HEIGHT">50</xsl:attribute>
						<xsl:attribute name="SRC"><xsl:value-of select="ImgFileOverall" /></xsl:attribute>							
				    </xsl:element>		
				    			
			        <xsl:element name="IMG">
						<xsl:attribute name="WIDTH">50</xsl:attribute>
						<xsl:attribute name="HEIGHT">50</xsl:attribute>
						<xsl:attribute name="SRC"><xsl:value-of select="ImgFileSuccess" /></xsl:attribute>							
				    </xsl:element>		
				
					</nobr>
				</td>
				<td>
					<nobr><xsl:value-of select="Name" /></nobr>
				</td>
			</tr>
			</table>
			
	</xsl:template>
	
	<xsl:template match="text()">
	</xsl:template>
	
	<xsl:template match="text()" mode="summary">
	</xsl:template>
	
	<xsl:template match="text()" mode="list">
	</xsl:template>

</xsl:stylesheet>
