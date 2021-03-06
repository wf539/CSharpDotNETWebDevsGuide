﻿<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	<xsl:template match="/">
		<div class="wrapper">
			<!-- this table displays the poll -->
			<table class="poll" borderColor="silver" cellSpacing="0" cellPadding="0" border="1">
				<tr>
					<th>
						<xsl:value-of select="//title" />
					</th>
				</tr>
				<xsl:for-each select="//question">
					<tr>
						<td class="topic">
							<xsl:value-of select="text" />
						</td>
					</tr>
					<xsl:for-each select="option">
						<tr>
							<td>
								<input type="radio">
									<xsl:attribute name="name">q<xsl:value-of select="../@id" /></xsl:attribute>
									<xsl:attribute name="id">q<xsl:value-of select="../@id" /></xsl:attribute>
									<xsl:attribute name="value">
										<xsl:value-of select="." />
									</xsl:attribute>
								</input>
								<xsl:value-of select="." />
							</td>
						</tr>
					</xsl:for-each>
				</xsl:for-each>
			</table>
			<br />
			<div align="right" style="WIDTH:100%">
				<input type="submit" value="vote" />&#160;
			</div>
			<br />
			<!-- this section calculates displays the statistics table -->
			<table class="poll" borderColor="silver" cellSpacing="0" cellPadding="0" border="1">
				<tr>
					<th colspan="2">Statistics</th>
				</tr>
				<xsl:for-each select="//question">
					<xsl:variable name="Qid" select="@id" />
					<tr>
						<td colspan="2" class="topic">
							<xsl:value-of select="text" />
						</td>
					</tr>
					<xsl:for-each select="option">
						<xsl:variable name="OP" select="." />
						<!-- count the number of selections that match this option -->
						<!-- divided by -->
						<!-- count the number of questions where the ID matches this question id -->
						<xsl:variable name="percent" select="count(//response[@selection = $OP]) div count(//response[@question-id=$Qid])" />
						<tr>
							<td>
								<xsl:value-of select="." />
							</td>
							<td>
								<xsl:choose>
									<xsl:when test="count(//response[@selection = $OP]) = 0">
										<!-- if there is no response to a poll item show a "-" centered -->
										<xsl:attribute name="align">center</xsl:attribute>
										<xsl:text>-</xsl:text>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number( $percent, '#.0%')" />
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
					</xsl:for-each>
				</xsl:for-each>
				<tr>
					<td colspan="2">
						Total Respondents:&#160;<xsl:value-of select="count( //data )" />
					</td>
				</tr>
			</table>
		</div>
	</xsl:template>
</xsl:stylesheet>
