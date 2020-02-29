<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	<!-- this file is used to display the message and message responses
		 in a collapsable tree view 
		 audience: general users
	-->
	<xsl:template match="/">
		<table>
			<tr>
				<td>
					<xsl:for-each select="//group">
						<xsl:sort select="title" />
						<xsl:sort select="topic" />
						<a target="main" class="inactiveFunction">
							<xsl:attribute name="href">board.aspx?board=<xsl:value-of select="groupid" />&amp;mod=<xsl:value-of select="moderatorid" />&amp;title=<xsl:value-of select="title" />: <xsl:value-of select="topic" /></xsl:attribute>
							<xsl:attribute name="title">view this message board</xsl:attribute>
							<xsl:value-of select="title" />: <xsl:value-of select="topic" />
						</a>
						<br />
					</xsl:for-each>
				</td>
			</tr>
		</table>
	</xsl:template>
</xsl:stylesheet>
