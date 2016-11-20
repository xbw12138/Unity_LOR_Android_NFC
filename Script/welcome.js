var renderOverlay : DisplayTextureFullScreen;


function Start() {

    renderOverlay = GetComponent(DisplayTextureFullScreen);
    renderOverlay.setStartColor(Color.white);
    renderOverlay.setDelay(3.0);
}

function Update () {

    if (renderOverlay.GUIColor.a >0) {
        renderOverlay.AlphaDown(Time.deltaTime);       
    }
     
 else     {
        Application.LoadLevel("MENU");//我想说，我好聪明，这样实现了场景的切换；；；哇哦
    }
    
   
}
