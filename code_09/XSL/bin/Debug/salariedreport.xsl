<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
   <xsl:template match="/">
      <html>
         <head>
            <title>EntegraTech Salaried Employees</title>
         </head>
         <body bgcolor="#C0C0C0">
            <h1 align="left">EntegraTech Salaried Employees</h1>
            <p></p>
            <xsl:apply-templates />
         </body>
      </html>
   </xsl:template>
   
   <xsl:template match="Salaried">
      <h2 align="left">Seattle Office Salaried Employees</h2>
      <table border="0" width="100%">
         <xsl:apply-templates select="Employee[Location='98103']" />
      </table>
      <p></p>
      <h2 align="left">Portland Office Salaried Employees</h2>
      <table border="0" width="100%">
         <xsl:apply-templates select="Employee[Location='97206']" />
      </table>
   </xsl:template>
   
   <xsl:template match="Employee[Location='98103']">
      <tr>
         <td width="28%">
            <xsl:value-of select="Name" />
         </td>
         <td width="72%">
            $<xsl:value-of select="Wage" />
         </td>
      </tr>
   </xsl:template>
   
   <xsl:template match="Employee[Location='97206']">
      <tr>
         <td width="28%">
            <xsl:value-of select="Name" />
         </td>
         <td width="72%">
            $<xsl:value-of select="Wage" />
         </td>
      </tr>
   </xsl:template>
</xsl:stylesheet>
