
package cn.sharesdk.unity3d;

import android.content.Context;
import android.os.Message;
import android.os.Handler.Callback;
import android.text.TextUtils;
import cn.sharesdk.framework.Platform;
import cn.sharesdk.framework.PlatformActionListener;
import cn.sharesdk.framework.ShareSDK;
import cn.sharesdk.framework.Platform.ShareParams;
import cn.sharesdk.onekeyshare.OnekeyShare;
import cn.sharesdk.onekeyshare.OnekeyShareTheme;
import com.mob.tools.utils.Hashon;
import com.mob.tools.utils.UIHandler;
import com.unity3d.player.UnityPlayer;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map.Entry;

public class ShareSDKUtils {
	private static boolean DEBUG = true;
	private static final int MSG_INITSDK = 1;
	private static final int MSG_STOPSDK = 2;
	private static final int MSG_AUTHORIZE = 3;
	private static final int MSG_SHOW_USER = 4;
	private static final int MSG_SHARE = 5;
	private static final int MSG_ONEKEY_SAHRE = 6;
	private static Context context;
	private static Callback uiCallback;
	private static PlatformActionListener paListener;
	private static String gameObject;
	private static String callback;

	public ShareSDKUtils() {
	}

	/** @deprecated */
	public static void prepare() {
		prepare((Context)null);
	}

	public static void prepare(Context appContext) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.prepare");
		}

		if(context == null) {
			context = appContext != null?appContext.getApplicationContext():UnityPlayer.currentActivity.getApplicationContext();
		}

		if(uiCallback == null) {
			uiCallback = new Callback() {
				public boolean handleMessage(Message msg) {
					return ShareSDKUtils.handleMessage(msg);
				}
			};
		}

		if(paListener == null) {
			paListener = new PlatformActionListener() {
				public void onError(Platform platform, int action, Throwable t) {
					String resp = ShareSDKUtils.javaActionResToCS(platform, action, t);
					UnityPlayer.UnitySendMessage(ShareSDKUtils.gameObject, ShareSDKUtils.callback, resp);
				}

				public void onComplete(Platform platform, int action, HashMap<String, Object> res) {
					String resp = ShareSDKUtils.javaActionResToCS(platform, action, res);
					UnityPlayer.UnitySendMessage(ShareSDKUtils.gameObject, ShareSDKUtils.callback, resp);
				}

				public void onCancel(Platform platform, int action) {
					String resp = ShareSDKUtils.javaActionResToCS(platform, action);
					UnityPlayer.UnitySendMessage(ShareSDKUtils.gameObject, ShareSDKUtils.callback, resp);
				}
			};
		}

	}

	public static void setGameObject(String gameObject, String callback) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.setGameObject");
		}

		gameObject = gameObject;
		callback = callback;
	}

	public static void initSDK(String appKey) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.initSDK");
		}

		Message msg = new Message();
		msg.what = 1;
		msg.obj = appKey;
		UIHandler.sendMessage(msg, uiCallback);
	}

	public static void stopSDK() {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.stopSDK");
		}

		UIHandler.sendEmptyMessage(2, uiCallback);
	}

	public static void setPlatformConfig(int platform, String configs) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.setPlatformConfig");
		}

		Hashon hashon = new Hashon();
		HashMap devInfo = hashon.fromJson(configs);
		String p = ShareSDK.platformIdToName(platform);
		ShareSDK.setPlatformDevInfo(p, devInfo);
	}

	public static void authorize(int platform) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.authorize");
		}

		Message msg = new Message();
		msg.what = 3;
		msg.arg1 = platform;
		UIHandler.sendMessage(msg, uiCallback);
	}

	public static void removeAccount(int platform) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.removeAccount");
		}

		String name = ShareSDK.platformIdToName(platform);
		Platform plat = ShareSDK.getPlatform(context, name);
		plat.removeAccount(true);
	}

	public static boolean isValid(int platform) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.isValid");
		}

		String name = ShareSDK.platformIdToName(platform);
		Platform plat = ShareSDK.getPlatform(context, name);
		return plat.isValid();
	}

	public static void showUser(int platform) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.showUser");
		}

		Message msg = new Message();
		msg.what = 4;
		msg.arg1 = platform;
		UIHandler.sendMessage(msg, uiCallback);
	}

	public static void share(int platform, String content) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.share");
		}

		Message msg = new Message();
		msg.what = 5;
		msg.arg1 = platform;
		msg.obj = content;
		UIHandler.sendMessage(msg, uiCallback);
	}

	public static void onekeyShare(int platform, String content) {
		if(DEBUG) {
			System.out.println("ShareSDKUtils.OnekeyShare");
		}

		Message msg = new Message();
		msg.what = 6;
		msg.arg1 = platform;
		msg.obj = content;
		UIHandler.sendMessage(msg, uiCallback);
	}

	public static boolean handleMessage(Message msg) {
		int platform;
		String content;
		Platform hashon2;
		switch(msg.what) {
		case 1:
			if(msg.obj != null) {
				String platform1 = (String)msg.obj;
				ShareSDK.initSDK(context, platform1);
			} else {
				ShareSDK.initSDK(context);
			}
			break;
		case 2:
			ShareSDK.stopSDK(context);
			break;
		case 3:
			platform = msg.arg1;
			content = ShareSDK.platformIdToName(platform);
			hashon2 = ShareSDK.getPlatform(context, content);
			hashon2.setPlatformActionListener(paListener);
			hashon2.authorize();
			break;
		case 4:
			platform = msg.arg1;
			content = ShareSDK.platformIdToName(platform);
			hashon2 = ShareSDK.getPlatform(context, content);
			hashon2.setPlatformActionListener(paListener);
			hashon2.showUser((String)null);
			break;
		case 5:
			platform = msg.arg1;
			content = (String)msg.obj;
			String hashon1 = ShareSDK.platformIdToName(platform);
			Platform map1 = ShareSDK.getPlatform(context, hashon1);
			map1.setPlatformActionListener(paListener);

			try {
				Hashon oks1 = new Hashon();
				ShareParams theme1 = hashmapToShareParams(map1, oks1.fromJson(content));
				map1.share(theme1);
			} catch (Throwable var7) {
				paListener.onError(map1, 9, var7);
			}
			break;
		case 6:
			platform = msg.arg1;
			content = (String)msg.obj;
			Hashon hashon = new Hashon();
			HashMap map = CSMapToJavaMap(hashon.fromJson(content));
			OnekeyShare oks = new OnekeyShare();
			String theme;
			if(platform > 0) {
				theme = ShareSDK.platformIdToName(platform);
				oks.setPlatform(theme);
			}

			if(map.containsKey("text")) {
				oks.setText((String)map.get("text"));
			}

			if(map.containsKey("imagePath")) {
				oks.setImagePath((String)map.get("imagePath"));
			}

			if(map.containsKey("imageUrl")) {
				oks.setImageUrl((String)map.get("imageUrl"));
			}

			if(map.containsKey("title")) {
				oks.setTitle((String)map.get("title"));
			}

			if(map.containsKey("comment")) {
				oks.setComment((String)map.get("comment"));
			}

			if(map.containsKey("url")) {
				oks.setUrl((String)map.get("url"));
				oks.setTitleUrl((String)map.get("url"));
			}

			if(map.containsKey("site")) {
				oks.setSite((String)map.get("site"));
			}

			if(map.containsKey("siteUrl")) {
				oks.setSiteUrl((String)map.get("siteUrl"));
			}

			theme = (String)map.get("shareTheme");
			if(OnekeyShareTheme.SKYBLUE.toString().toLowerCase().equals(theme)) {
				oks.setTheme(OnekeyShareTheme.SKYBLUE);
			}

			if(map.containsKey("disableSSOWhenAuthorize") && ((Boolean)map.get("disableSSOWhenAuthorize")).booleanValue()) {
				oks.disableSSOWhenAuthorize();
			}

			oks.setCallback(paListener);
			oks.show(context);
		}

		return false;
	}

	private static String javaActionResToCS(Platform platform, int action, Throwable t) {
		int platformId = ShareSDK.platformNameToId(platform.getName());
		HashMap map = new HashMap();
		map.put("platform", Integer.valueOf(platformId));
		map.put("action", Integer.valueOf(action));
		map.put("status", Integer.valueOf(2));
		map.put("res", throwableToMap(t));
		Hashon hashon = new Hashon();
		return hashon.fromHashMap(map);
	}

	private static String javaActionResToCS(Platform platform, int action, HashMap<String, Object> res) {
		int platformId = ShareSDK.platformNameToId(platform.getName());
		HashMap map = new HashMap();
		map.put("platform", Integer.valueOf(platformId));
		map.put("action", Integer.valueOf(action));
		map.put("status", Integer.valueOf(1));
		map.put("res", res);
		Hashon hashon = new Hashon();
		return hashon.fromHashMap(map);
	}

	private static String javaActionResToCS(Platform platform, int action) {
		int platformId = ShareSDK.platformNameToId(platform.getName());
		HashMap map = new HashMap();
		map.put("platform", Integer.valueOf(platformId));
		map.put("action", Integer.valueOf(action));
		map.put("status", Integer.valueOf(3));
		Hashon hashon = new Hashon();
		return hashon.fromHashMap(map);
	}

	private static HashMap<String, Object> throwableToMap(Throwable t) {
		HashMap map = new HashMap();
		map.put("msg", t.getMessage());
		ArrayList traces = new ArrayList();
		StackTraceElement[] var6;
		int var5 = (var6 = t.getStackTrace()).length;

		for(int var4 = 0; var4 < var5; ++var4) {
			StackTraceElement cause = var6[var4];
			HashMap element = new HashMap();
			element.put("cls", cause.getClassName());
			element.put("method", cause.getMethodName());
			element.put("file", cause.getFileName());
			element.put("line", Integer.valueOf(cause.getLineNumber()));
			traces.add(element);
		}

		map.put("stack", traces);
		Throwable var8 = t.getCause();
		if(var8 != null) {
			map.put("cause", throwableToMap(var8));
		}

		return map;
	}

	private static ShareParams hashmapToShareParams(Platform plat, HashMap<String, Object> content) throws Throwable {
		String className = plat.getClass().getName() + "$ShareParams";
		Class cls = Class.forName(className);
		if(cls == null) {
			return null;
		} else {
			Object sp = cls.newInstance();
			if(sp == null) {
				return null;
			} else {
				HashMap data = CSMapToJavaMap(content);
				Iterator var7 = data.entrySet().iterator();

				while(var7.hasNext()) {
					Entry ent = (Entry)var7.next();

					try {
						Field fld = cls.getField((String)ent.getKey());
						if(fld != null) {
							fld.setAccessible(true);
							fld.set(sp, ent.getValue());
						}
					} catch (Throwable var9) {
						;
					}
				}

				return (ShareParams)sp;
			}
		}
	}

	private static HashMap<String, Object> CSMapToJavaMap(HashMap<String, Object> content) {
		HashMap map = new HashMap();
		map.put("text", content.get("content"));
		String image = (String)content.get("image");
		if(image != null && image.startsWith("/")) {
			map.put("imagePath", image);
		} else if(!TextUtils.isEmpty(image)) {
			map.put("imageUrl", image);
		}

		map.put("title", content.get("title"));
		map.put("comment", content.get("description"));
		map.put("url", content.get("url"));
		map.put("titleUrl", content.get("url"));
		String type = (String)content.get("type");
		if(type != null) {
			int e = iosTypeToAndroidType(Integer.parseInt(type));
			map.put("shareType", Integer.valueOf(e));
		}

		map.put("filePath", content.get("file"));
		map.put("siteUrl", content.get("siteUrl"));
		map.put("site", content.get("site"));
		map.put("musicUrl", content.get("musicUrl"));
		map.put("extInfo", content.get("extInfo"));
		map.put("shareTheme", content.get("shareTheme"));

		try {
			boolean e1 = Boolean.parseBoolean(String.valueOf(content.get("disableSSOWhenAuthorize")));
			map.put("disableSSOWhenAuthorize", Boolean.valueOf(e1));
		} catch (Exception var5) {
			var5.printStackTrace();
			map.put("disableSSOWhenAuthorize", Boolean.valueOf(false));
		}

		return map;
	}

	private static int iosTypeToAndroidType(int type) {
		switch(type) {
		case 1:
			return 2;
		case 2:
			return 4;
		case 3:
			return 5;
		case 4:
			return 6;
		case 5:
			return 7;
		case 6:
		case 7:
			return 9;
		case 8:
			return 8;
		default:
			return 1;
		}
	}
}
