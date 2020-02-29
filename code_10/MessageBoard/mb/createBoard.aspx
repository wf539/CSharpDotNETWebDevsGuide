<%@ Page language="c#" Codebehind="createBoard.aspx.cs" AutoEventWireup="false" Inherits="mb.createBoard" %>
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
		<asp:Label id="clientScriptHandle" runat="server"></asp:Label>
		<form id="createBoard" method="post" runat="server">
			<TABLE style="WIDTH: 300px; HEIGHT: 225px" cellSpacing="1" cellPadding="1" width="300" border="0">
				<TR>
					<TD>
						<FONT face="Arial">Group Title</FONT>
					</TD>
					<TD>
						<asp:TextBox id="txtTitle" runat="server"></asp:TextBox>
					</TD>
				</TR>
				<TR>
					<TD>
						<FONT face="Arial">Group Topic</FONT>
					</TD>
					<TD>
						<asp:TextBox id="txtGroup" runat="server"></asp:TextBox>
					</TD>
				</TR>
				<TR>
					<TD colspan="2">
						<P align="center">
							<asp:Button id="Button1" runat="server" Text="New Group"></asp:Button>
						</P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
