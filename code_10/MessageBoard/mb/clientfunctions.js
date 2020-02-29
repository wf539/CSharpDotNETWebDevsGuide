function toggleLoginDisplay( e, box, block){
	toggleElementDisplay( block )
	if( box.className !="selected" ){
		box.className ="selected";
	}else{
		box.className ="";
	}	
	if( e.className =="activeFunction"){
		e.className = "inactiveFunction"
	}else{
		e.className = "activeFunction"
	}			
}

function toggleElementDisplay( block ){
	if( block.style.display !="none" ){
		block.style.display ="none"
	}else{
		block.style.display ="block"
	}
}

function expandCollapseBranch( eImage, Iexpand, Icollapse, eSublist ){
	if( eSublist.style.display != "none"){
		eSublist.style.display ="none";
		eImage.src = Iexpand.src;
	}else{
		eSublist.style.display ="block";
		eImage.src = Icollapse.src;
	}
}




