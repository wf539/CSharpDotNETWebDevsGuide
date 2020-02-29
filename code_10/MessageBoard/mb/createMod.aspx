<%@ Page language="c#" Codebehind="createMod.aspx.cs" AutoEventWireup="false" Inherits="mb.createMod" %>
<%@ OutputCache Duration="1" VaryByParam="none"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript (ECMAScript)">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="createMod" method="post" runat="server">
			<TABLE style="WIDTH: 300px; HEIGHT: 225px" cellSpacing="1" cellPadding="1" width="300" border="0">
				<TR>
					<TD>
						<FONT face="Arial">Email:</FONT>
					</TD>
					<TD>
						<asp:TextBox id="txtEmail" runat="server"></asp:TextBox>
					</TD>
				</TR>
				<TR>
					<TD>
						<FONT face="Arial">Password: </FONT>
					</TD>
					<TD>
						<asp:TextBox id="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
					</TD>
				</TR>
				<TR>
					<TD colspan="2">
						<P align="center">
							<asp:Button id="Button1" runat="server" Text="Create Moderator"></asp:Button>
						</P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
