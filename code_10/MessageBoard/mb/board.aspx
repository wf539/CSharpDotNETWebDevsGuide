<%@ OutputCache Duration="1" VaryByParam="none"%>
<%@ Page language="c#" Codebehind="board.aspx.cs" AutoEventWireup="false" Inherits="mb.xBoard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript (ECMAScript)">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="messageboard.css">
		<script language="javascript" src="clientfunctions.js"></script>
		<script language="javascript">
		<!--
			//cache images
			var rightArrow = new Image;
				rightArrow.src = "images/rightArrow.gif";
			var downArrow = new Image;
				downArrow.src = "images/downArrow.gif";
			var plus = new Image;
				plus.src = "images/plus.gif";
			var minus = new Image;
				minus.src = "images/minus.gif";
				
			function selectMessage( type, eImage, eGroup, e ){
				if(( type=="m") && (eGroup != null)) {
					//toggle responses
					expandCollapseBranch( eImage, plus, minus, eGroup);
				}
				setSelected( e, type )
			}
			
			function showHideText( e ){
				if( e.style.display != "none" ){
					e.style.display="none";
				}else{
					e.style.display="block";
				}
			}
			
			function setSelected( e, type){
				var currentType = board.itemType.value;
				if(!currentType){
					//nothing has been selected yet
				}else{
					if( currentType =="m"){
						name="msg_";
					}else if( currentType =="r"){
						name="rsp_";
					}
					var currentSelection = eval(name + board.selection.value );
					if(!currentSelection){
						//nothing has been selected yet
					}else{
						//update UI for previous selection
						currentSelection.className="unselectedItem";
					}
				}
				board.itemType.value = type;
				board.selection.value = e.alt;	//store new current selection
				e.className="selectedItem";		//update UI for new selection		
				board.msgID.value = e.id;					
			}
			
			function respond(){
				msgid = board.selection.value;
				page = "respond2message.aspx?msgID=" + msgid; 
				window.open(page,'win','width=380,height=400,scroll=no,title=no,status=no');
			}
			
			function post(){
				window.open('respond2message.aspx?msg=new','win','width=380,height=400,scroll=no,title=no,status=no');
			}
			
		//-->
		</script>
	</HEAD>
	<body>
		<form id="board" method="post" runat="server">
			<table border="0" width="100%" height="100%">
				<tr>
					<td valign="top">
						<asp:Label id="groupTitle" runat="server"></asp:Label>
						<input type="button" value="post new message" onclick="post()" style='width:115px'>
						<asp:Label id="reponseButtonWrapper" runat="server"></asp:Label>
						<asp:Button id="deleteMessage" Visible="False" runat="server" Text="delete selected item" style='width:135px'></asp:Button>
					</td>
				</tr>
				<tr>
					<td valign="top">
						<asp:Xml id="xMsgBoard" runat="server"></asp:Xml>
					</td>
				</tr>
			</table>
			<input type="hidden" id="selection" runat="server" name="selection"> <input type="hidden" id="itemType" runat="server" name="itemType">
			<input type="hidden" id="msgID" runat="server" name="msgID"> <input type="hidden" id="responseMessage" runat="server" name="responseMessage">
			<input type="hidden" id="responderName" runat="server" name="responderName"> <input type="hidden" id="responderEmail" runat="server" name="responderEmail">
			<input type="hidden" id="ClientPost" runat="server" name="ClientPost">
		</form>
	</body>
</HTML>
