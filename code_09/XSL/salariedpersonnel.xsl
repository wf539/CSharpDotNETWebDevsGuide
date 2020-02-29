
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
    <Salaried>
        <xsl:apply-templates/>
    </Salaried>
    </xsl:template>

    <xsl:template match="Employees">
      <xsl:apply-templates select="Employee[Salaried='true']"/>
    </xsl:template>

    <xsl:template match="Employee[Salaried='true']">
        <Employee>
          <Name>
            <xsl:value-of select="FirstName"/><xsl:text> </xsl:text>
            <xsl:value-of select="MiddleInit"/><xsl:text>. </xsl:text>
            <xsl:value-of select="LastName"/>
          </Name>
          <Wage>
            <xsl:value-of select="Wage"/>
          </Wage>
          <Location>
            <xsl:value-of select="Location/Zip"/>  
          </Location>
        </Employee>
    </xsl:template>

</xsl:stylesheet>

