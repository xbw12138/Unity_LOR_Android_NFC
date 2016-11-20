var graphic = TextureGUI(); //(28,23);
var graphic1=TextureGUI();
var GUIColor:Color;
var kai1=1;
function OnGUI() {
    GUI.color = GUIColor;
    if(kai1==1)
    {
        if (graphic1.texture) {
            GUI.DrawTexture(Rect(graphic1.offset.x,graphic1.offset.y,
                            Screen.width,Screen.height),
                            graphic1.texture,ScaleMode.StretchToFill,true);
        }
        StartCoroutine(WaitAndPrint(0.1F));
    }
    if(kai1==0)
    {
        if (graphic.texture) {
            GUI.DrawTexture(Rect(graphic.offset.x,graphic.offset.y,
                            Screen.width,Screen.height),
                            graphic.texture,ScaleMode.StretchToFill,true);
        }
        StartCoroutine(WaitAndPrint1(0.1F));
    }
   
   
}
function WaitAndPrint (waitTime : float){
            yield  WaitForSeconds(waitTime);
            kai1=0;
        }    
    function WaitAndPrint1 (waitTime : float){
        yield  WaitForSeconds(waitTime);
        kai1=1;
    }   
function AlphaUp(change:float) {
    GUIColor.a += change;
}

    function setStartColor(color:Color) {
        GUIColor = color;
    }


        function setDelay(delay:float) {
            if (GUIColor.a > .5) {
                GUIColor.a += delay;
            } else {
                GUIColor.a -= delay;
            }
        }

            function AlphaDown(change:float) {
                GUIColor.a -= change;
            }

