<%@ OutputCache Duration="1" VaryByParam="none"%>
<%@ Page language="c#" Codebehind="boardlist.aspx.cs" AutoEventWireup="false" Inherits="mb.boardlist" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="messageboard.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="clientfunctions.js"></script>
	</HEAD>
	<body bgcolor="dimgray">
		<form id="MBList" method="post" runat="server">
			<!-- this control will display the Message Board list -->
			<asp:xml id="xList" runat="server"></asp:xml>
			<!-- the following is for the login interface -->
			<div id="loginbox" name="loginbox" style="WIDTH:250px">
				<table>
					<tr>
						<td>
							<A class="inactiveFunction" id="admin" onclick="toggleLoginDisplay(this, loginbox, moderatorLoginBlock)" href="#">
								<div align="left">
									New Message Board /Admin
								</div>
							</A>
						</td>
					</tr>
					<tr>
						<td>
							<div id="moderatorLoginBlock" name="moderatorLoginBlock" style="DISPLAY:none">
								<table>
									<tr>
										<td>
											login:
										</td>
										<td align="right">
											<asp:RequiredFieldValidator id="reqLogin" ControlToValidate="moderatorLogin" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
											<asp:TextBox id="moderatorLogin" runat="server"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td>
											password
										</td>
										<td align="right">
											<asp:RequiredFieldValidator id="reqPassword" ControlToValidate="moderatorPassword" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
											<asp:TextBox id="moderatorPassword" TextMode="Password" runat="server"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td colspan="2" align="right">
											<asp:Button id="login" runat="server" Text="login"></asp:Button>
										</td>
									</tr>
									<tr>
										<td colspan="2">
											<asp:RegularExpressionValidator id="rexLogin" ControlToValidate="moderatorLogin" runat="server" ErrorMessage="invalid login" ValidationExpression="^.*\@.*\..*$"></asp:RegularExpressionValidator>
											<br>
											<asp:CheckBox id="adminCheckBox" runat="server"></asp:CheckBox>
											administrator?
											<br>
											<asp:Label id="adminLabel" runat="server"></asp:Label>
											<br>
											don't have a login? <a href="createMod.aspx" onclick="toggleLoginDisplay(admin, loginbox, moderatorLoginBlock)" target="main">
												get one here!</a>
										</td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
				</table>
			</div>
			<asp:Label id="scriptHandle" runat="server"></asp:Label>
			<asp:Label id="CreateNew" runat="server"></asp:Label>
		</form>
	</body>
</HTML>
