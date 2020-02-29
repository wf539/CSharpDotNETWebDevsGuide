<%@ OutputCache Duration="1" VaryByParam="none" %>
<%@ Page language="c#" Codebehind="XMLPoll.aspx.cs" AutoEventWireup="false" Inherits="XMLPoll_wa.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="xmlpoll.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="wrapper">
				<table class="poll" borderColor="silver" cellSpacing="0" cellPadding="0" border="1">
					<tr>
						<th>
							Programers Poll</th>
					</tr>
					<tr>
						<td class="topic">
							My Focus is on:
						</td>
					</tr>
					<tr>
						<td>
							<asp:radiobutton id="focus_Desktop" runat="server" GroupName="focus"></asp:radiobutton>
							Desktop
						</td>
					</tr>
					<tr>
						<td>
							<asp:radiobutton id="focus_web" runat="server" GroupName="focus"></asp:radiobutton>
							Web
						</td>
					</tr>
					<tr>
						<td>
							<asp:radiobutton id="focus_mobile" runat="server" GroupName="focus"></asp:radiobutton>
							Mobile
						</td>
					</tr>
					<tr>
						<td class="topic">
							My Primary Dev Tool is:
						</td>
					</tr>
					<tr>
						<td>
							<asp:radiobutton id="IDE_VSNET" runat="server" GroupName="IDE"></asp:radiobutton>
							VSNET
						</td>
					</tr>
					<tr>
						<td>
							<asp:radiobutton id="IDE_XMLSpy" runat="server" GroupName="IDE"></asp:radiobutton>
							XMLSpy
						</td>
					</tr>
					<tr>
						<td>
							<asp:RadioButton id="IDE_other" GroupName="IDE" runat="server"></asp:RadioButton>
							other
						</td>
					</tr>
				</table>
				<br>
				<div align="right" style="WIDTH:100%">
					<asp:Button id="vote" runat="server" Text="Vote"></asp:Button>
					&nbsp;
				</div>
				<br>
				<asp:Xml ID="stats" Runat="server" />
			</div>
		</form>
	</body>
</HTML>
