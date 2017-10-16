// JavaScript Document




  function setTab(name,cursel,n){
   for(i=1;i<=n;i++){
    var menu=document.getElementById(name+i);
    var con=document.getElementById("con_"+name+"_"+i);
    menu.className=i==cursel?"hover":"";
    con.style.display=i==cursel?"block":"none";
   }
  }

    function setTabout(name,cursel){

  var menu=document.getElementById(name+cursel);
    menu.className="";
    var con=document.getElementById("con_"+name+"_"+cursel);

    con.style.display="none"; 
  }