
package cn.sharesdk.unity3d;

import android.app.Application;
import cn.sharesdk.unity3d.ShareSDKUtils;

public class ShareSDKApplication extends Application {
	public ShareSDKApplication() {
	}

	public void onCreate() {
		super.onCreate();
		ShareSDKUtils.prepare(this.getApplicationContext());
	}
}
