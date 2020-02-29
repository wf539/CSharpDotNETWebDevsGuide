<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	<!-- this file is used to display the message and message responses
		 in a collapsable tree view 
	-->
	<xsl:template match="/">
		<div style="position:absolute;top:70px;">
			<div class="directions" >
				single-click to select an item or expand a message tree<br />
				double-click to read a message or message response.			
			</div>
			<xsl:apply-templates />
		</div>
		
	</xsl:template>
	<!-- ========================================= -->
	<!-- matches only messages that have responses -->
	<!-- ========================================= -->
	<xsl:template match="m[r/@rsgid]">
		<xsl:if test="position() > 0 ">
			<br />
		</xsl:if>
		<a href="#" class="unselectedItem">
			<xsl:attribute name="title">select this item</xsl:attribute>
			<xsl:attribute name="onclick">selectMessage('m',img_<xsl:value-of select="@msgid" />,rspGroup_<xsl:value-of select="@msgid" />,msg_<xsl:value-of select="@msgid" />);return false</xsl:attribute>
			<xsl:attribute name="ondblclick">showHideText(mtext_<xsl:value-of select="@msgid" />);return false</xsl:attribute>
			<img src="images/plus.gif" border="0">
				<xsl:attribute name="id">img_<xsl:value-of select="@msgid" /></xsl:attribute>
			</img>
		</a>
		<a href="#" class="unselectedItem" style="font-size:10pt">
			<xsl:attribute name="id">msg_<xsl:value-of select="@msgid" /></xsl:attribute>
			<xsl:attribute name="name">msg_<xsl:value-of select="@msgid" /></xsl:attribute>
			<xsl:attribute name="alt">
				<xsl:value-of select="@msgid" />
			</xsl:attribute>
			<xsl:attribute name="title">select this item</xsl:attribute>
			<xsl:attribute name="onclick">selectMessage('m',img_<xsl:value-of select="@msgid" />,rspGroup_<xsl:value-of select="@msgid" />, this);return false</xsl:attribute>
			<xsl:attribute name="ondblclick">showHideText(mtext_<xsl:value-of select="@msgid" />);return false</xsl:attribute>
			<b><xsl:value-of select="@subject" /></b> &#160;&#160;<span class="timestamp">[ posted:<xsl:value-of select="@msgdate" /> ]</span>
		</a>
		<div class="message-text" style="display:none">
			<xsl:attribute name="id">mtext_<xsl:value-of select="@msgid" /></xsl:attribute>
			<xsl:value-of select="@message" />
		</div>
		<div style="display:none;position:relative; left:15px;">
			<!-- ========================================= -->
			<!--           matches responses			   -->
			<!-- ========================================= -->
			<xsl:attribute name="id">rspGroup_<xsl:value-of select="@msgid" /></xsl:attribute>
			<xsl:for-each select="r">
				<a href="#" class="unselectedItem" style="font-size:9pt">
					<xsl:attribute name="id">rsp_<xsl:value-of select="@rsgid" /></xsl:attribute>
					<xsl:attribute name="name">rsp_<xsl:value-of select="@rsgid" /></xsl:attribute>
					<xsl:attribute name="alt">
						<xsl:value-of select="@rsgid" />
					</xsl:attribute>
					<xsl:attribute name="onclick">selectMessage('r',rspImg<xsl:value-of select="@rsgid" />,null,this);return false</xsl:attribute>
					<xsl:attribute name="ondblclick">showHideText(rtext_<xsl:value-of select="@rsgid" />);return false</xsl:attribute>
					<img src="images/rightArrow.gif" border="0">
						<xsl:attribute name="id">rspImg<xsl:value-of select="@rsgid" /></xsl:attribute>
					</img>&#160;Response: <span class="timestamp">[ posted:<xsl:value-of select="@rsgdate" /> ]</span>
				</a>
				<div class="message-text" style="display:none">
					<xsl:attribute name="id">rtext_<xsl:value-of select="@rsgid" /></xsl:attribute>
					<xsl:value-of select="@response" />
				</div>
				<br />
			</xsl:for-each>
		</div>
	</xsl:template>
	<!-- ============================================ -->
	<!-- matches only messages that have no responses -->
	<!-- ============================================ -->
	<xsl:template match="m">
		<div class="message-text">
			<a href="#" class="unselectedItem"  style="font-size:10pt">
				<xsl:attribute name="id">msg_<xsl:value-of select="@msgid" /></xsl:attribute>
				<xsl:attribute name="name">msg_<xsl:value-of select="@msgid" /></xsl:attribute>
				<xsl:attribute name="alt">
					<xsl:value-of select="@msgid" />
				</xsl:attribute>
				<xsl:attribute name="title">select this item</xsl:attribute>
				<xsl:attribute name="onclick">selectMessage('m',null,null,this);return false</xsl:attribute>
				<xsl:attribute name="ondblclick">showHideText(mtext_<xsl:value-of select="@msgid" />);return false</xsl:attribute>
				<b><xsl:value-of select="@subject" /></b> &#160;&#160;<span class="timestamp">[ posted:<xsl:value-of select="@msgdate" /> ]</span>
			</a>
			<div style="display:none">
				<xsl:attribute name="id">mtext_<xsl:value-of select="@msgid" /></xsl:attribute>
				<xsl:value-of select="@message" />
			</div>
		</div>
	</xsl:template>
</xsl:stylesheet>
