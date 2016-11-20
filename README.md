# Unity_LOR_Android_NFC

一年之后才觉得当时自己挺厉害的，真是一个庞大的工程，自己如何耐得住寂寞的

游戏下载链接
Last release: [Download](http://android.myapp.com/myapp/detail.htm?apkName=com.XBW.nfcgame)

Unity游戏工程比较大，上传Github有点不太合适，这里把NFC接口放出来吧，Unity的NFC接口。
Unity没有提供NFC功能，所以需要Android原生开发。

只需要eclipse导入NFC-JAR 工程，打包jar。
就可以了，Android studio的也可以，自己试试吧。

Unity里如何使用

```
//NFC写入模式
using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")){
	using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")){
			//调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
			jo.Call("StartActivity0", name1);
	}
}

```

```
//NFC读取模式
using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")){
	using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")){
			//调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
			jo.Call("StartActivity1", name);
	}
}
```   

```
//NFC写入回调接口
void reNFC(string str){}
```             

```
//NFC感应接口
void NFC(string str){}
```


