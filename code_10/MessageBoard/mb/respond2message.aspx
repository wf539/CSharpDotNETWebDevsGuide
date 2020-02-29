<%@ Page language="c#" Codebehind="respond2message.aspx.cs" AutoEventWireup="false" Inherits="mb.respond2message" %>
<%@ OutputCache Duration="1" VaryByParam="none"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<title>post</title>
	</HEAD>
	<body>
		<form id="respond2message" method="post" runat="server">
			<asp:label id="ClientScriptHandle" runat="server"></asp:label>
			<table>
				<tr>
					<td align="right">
						your name:
					</td>
					<td align="left" colSpan="2">
						<asp:textbox id="name" runat="server"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="right">
						email:
					</td>
					<td align="left">
						<asp:textbox id="email" runat="server"></asp:textbox>
					</td>
					<td>
						<asp:regularexpressionvalidator id="rexEmail" runat="server" ErrorMessage="Invalid Email" ValidationExpression="^.*\@.*\..*$" ControlToValidate="email"></asp:regularexpressionvalidator>
					</td>
				</tr>
				<tr>
					<td align="right">
						Subject:
					</td>
					<td align="left" colspan="2">
						<asp:textbox id="subject" runat="server"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="right">
						response:
					</td>
					<td colSpan="2">
						&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<asp:textbox id="responseMessage" runat="server" TextMode="MultiLine" Rows="10" Columns="40"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="3">
						<br>
						<asp:button id="sendResponse" runat="server" Text="send"></asp:button>
						&nbsp; <input onclick="window.close()" type="button" value="cancel">
					</td>
				</tr>
			</table>
			<input type="hidden" id="msgID" runat="server" name="msgID">
		</form>
	</body>
</HTML>
