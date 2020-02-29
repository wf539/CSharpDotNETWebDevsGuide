<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WebUserControl1.ascx.cs" Inherits="simpleMail.WebUserControl1"%>
<TABLE cellSpacing="1" cellPadding="1" width="300" border="0">
	<TR>
		<TD>
			From:
		</TD>
		<TD colspan="2">
			<asp:TextBox id="txtFrom" runat="server" AutoPostBack="False">
			</asp:TextBox>
		</TD>
	</TR>
	<TR>
		<TD>
			To:
		</TD>
		<TD colspan="2">
			<asp:TextBox id="txtTo" runat="server" AutoPostBack="False">
			</asp:TextBox>
		</TD>
	</TR>
	<TR>
		<TD>
			CC:
		</TD>
		<TD colspan="2">
			<asp:TextBox id="txtCC" runat="server" AutoPostBack="False">
			</asp:TextBox>
		</TD>
	</TR>
	<TR>
		<TD colspan="3">
		</TD>
	</TR>
	<TR>
		<TD colspan="3">
			Subject:
		</TD>
	</TR>
	<TR>
		<TD colspan="3">
			<asp:TextBox id="txtSubject" AutoPostBack="False" runat="server" Width="288px" Height="24px">
			</asp:TextBox>
		</TD>
	</TR>
	<TR>
		<TD colspan="3">
			Message:
		</TD>
	</TR>
	<TR>
		<TD colspan="3">
			<asp:TextBox id="txtMessage" AutoPostBack="False" Rows="10" runat="server" Width="313px" Height="160px">
			</asp:TextBox>
		</TD>
	</TR>
	<tr>
		<td colspan="3">
			<asp:Button id="btnEmail" runat="server" Text="Send Email">
			</asp:Button>
		</td>
	</tr>
</TABLE>
</div>
<DIV style="Z-INDEX: 101; LEFT: 352px; WIDTH: 264px; POSITION: absolute; TOP: 24px; HEIGHT: 400px">
	<TABLE style="WIDTH: 300px; HEIGHT: 80px" cellSpacing="1" cellPadding="1" width="300" border="0">
		<TR>
			<TD>
				<asp:RequiredFieldValidator id="rfvTxtfrom" runat="server" ControlToValidate="txtFrom" Display="Dynamic" Font-Name="Verdana" Font-Size="10pt">
					* please provide an email address
				</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator id="revTxtfrom" runat="server" ControlToValidate="txtFrom" ValidationExpression="^.*\@.*\..*$" Display="static" Font-Name="verdana" Font-Size="10pt">
					Please enter a valid e-mail address
				</asp:RegularExpressionValidator>
			</TD>
		</TR>
		<TR>
			<TD>
				<asp:RequiredFieldValidator id="rfvTxtto" runat="server" ControlToValidate="txtTo" Display="Dynamic" Font-Name="Verdana" Font-Size="10pt">
					* please provide an email address
				</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator id="revTxtto" runat="server" ControlToValidate="txtTo" ValidationExpression="^.*\@.*\..*$" Display="static" Font-Name="verdana" Font-Size="10pt">
					Please enter a valid e-mail address
				</asp:RegularExpressionValidator>
			</TD>
		</TR>
		<TR>
			<TD>
				<asp:RegularExpressionValidator id="revTxtcc" runat="server" ControlToValidate="txtCC" ValidationExpression="^.*\@.*\..*$" Display="static" Font-Name="verdana" Font-Size="10pt">
					Please enter a valid e-mail address
				</asp:RegularExpressionValidator>
			</TD>
		</TR>
	</TABLE>
</DIV>
