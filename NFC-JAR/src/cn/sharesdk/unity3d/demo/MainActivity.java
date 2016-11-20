

package cn.sharesdk.onekeyshare;

import android.app.NotificationManager;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Message;
import android.os.Handler.Callback;
import android.text.TextUtils;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Toast;
import cn.sharesdk.framework.CustomPlatform;
import cn.sharesdk.framework.Platform;
import cn.sharesdk.framework.PlatformActionListener;
import cn.sharesdk.framework.ShareSDK;
import cn.sharesdk.onekeyshare.CustomerLogo;
import cn.sharesdk.onekeyshare.OnekeyShareTheme;
import cn.sharesdk.onekeyshare.PlatformListFakeActivity;
import cn.sharesdk.onekeyshare.ShareContentCustomizeCallback;
import cn.sharesdk.onekeyshare.ShareCore;
import cn.sharesdk.onekeyshare.ThemeShareCallback;
import cn.sharesdk.onekeyshare.PlatformListFakeActivity.OnShareButtonClickListener;
import com.mob.tools.utils.BitmapHelper;
import com.mob.tools.utils.R;
import com.mob.tools.utils.UIHandler;
import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map.Entry;

public class OnekeyShare implements PlatformActionListener, Callback {
	private static final int MSG_TOAST = 1;
	private static final int MSG_ACTION_CCALLBACK = 2;
	private static final int MSG_CANCEL_NOTIFY = 3;
	private HashMap<String, Object> shareParamsMap = new HashMap();
	private ArrayList<CustomerLogo> customers = new ArrayList();
	private boolean silent;
	private PlatformActionListener callback = this;
	private ShareContentCustomizeCallback customizeCallback;
	private boolean dialogMode = false;
	private boolean disableSSO;
	private HashMap<String, String> hiddenPlatforms = new HashMap();
	private View bgView;
	private OnekeyShareTheme theme;
	private Context context;
	private OnShareButtonClickListener onShareButtonClickListener;

	public OnekeyShare() {
	}

	public void show(Context context) {
		ShareSDK.initSDK(context);
		this.context = context;
		ShareSDK.logDemoEvent(1, (Platform)null);
		if(this.shareParamsMap.containsKey("platform")) {
			String platformListFakeActivity = String.valueOf(this.shareParamsMap.get("platform"));
			Platform name = ShareSDK.getPlatform(platformListFakeActivity);
			if(this.silent || ShareCore.isUseClientToShare(platformListFakeActivity) || name instanceof CustomPlatform) {
				HashMap platform1 = new HashMap();
				platform1.put(ShareSDK.getPlatform(platformListFakeActivity), this.shareParamsMap);
				this.share(platform1);
				return;
			}
		}

		PlatformListFakeActivity platformListFakeActivity1;
		try {
			if(OnekeyShareTheme.SKYBLUE == this.theme) {
				platformListFakeActivity1 = (PlatformListFakeActivity)Class.forName("cn.sharesdk.onekeyshare.theme.skyblue.PlatformListPage").newInstance();
			} else {
				platformListFakeActivity1 = (PlatformListFakeActivity)Class.forName("cn.sharesdk.onekeyshare.theme.classic.PlatformListPage").newInstance();
			}
		} catch (Exception var5) {
			var5.printStackTrace();
			return;
		}

		platformListFakeActivity1.setDialogMode(this.dialogMode);
		platformListFakeActivity1.setShareParamsMap(this.shareParamsMap);
		platformListFakeActivity1.setSilent(this.silent);
		platformListFakeActivity1.setCustomerLogos(this.customers);
		platformListFakeActivity1.setBackgroundView(this.bgView);
		platformListFakeActivity1.setHiddenPlatforms(this.hiddenPlatforms);
		platformListFakeActivity1.setOnShareButtonClickListener(this.onShareButtonClickListener);
		platformListFakeActivity1.setThemeShareCallback(new ThemeShareCallback() {
			public void doShare(HashMap<Platform, HashMap<String, Object>> shareData) {
				OnekeyShare.this.share(shareData);
			}
		});
		if(this.shareParamsMap.containsKey("platform")) {
			String name1 = String.valueOf(this.shareParamsMap.get("platform"));
			Platform platform = ShareSDK.getPlatform(name1);
			platformListFakeActivity1.showEditPage(context, platform);
		} else {
			platformListFakeActivity1.show(context, (Intent)null);
		}
	}

	public void setTheme(OnekeyShareTheme theme) {
		this.theme = theme;
	}

	public void setAddress(String address) {
		this.shareParamsMap.put("address", address);
	}

	public void setTitle(String title) {
		this.shareParamsMap.put("title", title);
	}

	public void setTitleUrl(String titleUrl) {
		this.shareParamsMap.put("titleUrl", titleUrl);
	}

	public void setText(String text) {
		this.shareParamsMap.put("text", text);
	}

	public String getText() {
		return this.shareParamsMap.containsKey("text")?String.valueOf(this.shareParamsMap.get("text")):null;
	}

	public void setImagePath(String imagePath) {
		if(!TextUtils.isEmpty(imagePath)) {
			this.shareParamsMap.put("imagePath", imagePath);
		}

	}

	public void setImageUrl(String imageUrl) {
		if(!TextUtils.isEmpty(imageUrl)) {
			this.shareParamsMap.put("imageUrl", imageUrl);
		}

	}

	public void setUrl(String url) {
		this.shareParamsMap.put("url", url);
	}

	public void setFilePath(String filePath) {
		this.shareParamsMap.put("filePath", filePath);
	}

	public void setComment(String comment) {
		this.shareParamsMap.put("comment", comment);
	}

	public void setSite(String site) {
		this.shareParamsMap.put("site", site);
	}

	public void setSiteUrl(String siteUrl) {
		this.shareParamsMap.put("siteUrl", siteUrl);
	}

	public void setVenueName(String venueName) {
		this.shareParamsMap.put("venueName", venueName);
	}

	public void setVenueDescription(String venueDescription) {
		this.shareParamsMap.put("venueDescription", venueDescription);
	}

	public void setLatitude(float latitude) {
		this.shareParamsMap.put("latitude", Float.valueOf(latitude));
	}

	public void setLongitude(float longitude) {
		this.shareParamsMap.put("longitude", Float.valueOf(longitude));
	}

	public void setSilent(boolean silent) {
		this.silent = silent;
	}

	public void setPlatform(String platform) {
		this.shareParamsMap.put("platform", platform);
	}

	public void setInstallUrl(String installurl) {
		this.shareParamsMap.put("installurl", installurl);
	}

	public void setExecuteUrl(String executeurl) {
		this.shareParamsMap.put("executeurl", executeurl);
	}

	public void setMusicUrl(String musicUrl) {
		this.shareParamsMap.put("musicUrl", musicUrl);
	}

	public void setCallback(PlatformActionListener callback) {
		this.callback = callback;
	}

	public PlatformActionListener getCallback() {
		return this.callback;
	}

	public void setShareContentCustomizeCallback(ShareContentCustomizeCallback callback) {
		this.customizeCallback = callback;
	}

	public ShareContentCustomizeCallback getShareContentCustomizeCallback() {
		return this.customizeCallback;
	}

	public void setCustomerLogo(Bitmap enableLogo, Bitmap disableLogo, String label, OnClickListener ocListener) {
		CustomerLogo cl = new CustomerLogo();
		cl.label = label;
		cl.enableLogo = enableLogo;
		cl.disableLogo = disableLogo;
		cl.listener = ocListener;
		this.customers.add(cl);
	}

	public void disableSSOWhenAuthorize() {
		this.disableSSO = true;
	}

	public void setDialogMode() {
		this.dialogMode = true;
		this.shareParamsMap.put("dialogMode", Boolean.valueOf(this.dialogMode));
	}

	public void addHiddenPlatform(String platform) {
		this.hiddenPlatforms.put(platform, platform);
	}

	public void setViewToShare(View viewToShare) {
		try {
			Bitmap e = BitmapHelper.captureView(viewToShare, viewToShare.getWidth(), viewToShare.getHeight());
			this.shareParamsMap.put("viewToShare", e);
		} catch (Throwable var3) {
			var3.printStackTrace();
		}

	}

	public void setImageArray(String[] imageArray) {
		this.shareParamsMap.put("imageArray", imageArray);
	}

	public void setEditPageBackground(View bgView) {
		this.bgView = bgView;
	}

	public void setOnShareButtonClickListener(OnShareButtonClickListener onShareButtonClickListener) {
		this.onShareButtonClickListener = onShareButtonClickListener;
	}

	public void share(HashMap<Platform, HashMap<String, Object>> shareData) {
		boolean started = false;
		Iterator var4 = shareData.entrySet().iterator();

		while(true) {
			while(var4.hasNext()) {
				Entry ent = (Entry)var4.next();
				Platform plat = (Platform)ent.getKey();
				plat.SSOSetting(this.disableSSO);
				String name = plat.getName();
				boolean isGooglePlus = "GooglePlus".equals(name);
				if(isGooglePlus && !plat.isClientValid()) {
					Message isAlipay1 = new Message();
					isAlipay1.what = 1;
					int isKakaoTalk2 = R.getStringRes(this.context, "google_plus_client_inavailable");
					isAlipay1.obj = this.context.getString(isKakaoTalk2);
					UIHandler.sendMessage(isAlipay1, this);
				} else {
					boolean isAlipay = "Alipay".equals(name);
					if(isAlipay && !plat.isClientValid()) {
						Message isKakaoTalk1 = new Message();
						isKakaoTalk1.what = 1;
						int isKakaoStory2 = R.getStringRes(this.context, "alipay_client_inavailable");
						isKakaoTalk1.obj = this.context.getString(isKakaoStory2);
						UIHandler.sendMessage(isKakaoTalk1, this);
					} else {
						boolean isKakaoTalk = "KakaoTalk".equals(name);
						if(isKakaoTalk && !plat.isClientValid()) {
							Message isKakaoStory1 = new Message();
							isKakaoStory1.what = 1;
							int isLine2 = R.getStringRes(this.context, "kakaotalk_client_inavailable");
							isKakaoStory1.obj = this.context.getString(isLine2);
							UIHandler.sendMessage(isKakaoStory1, this);
						} else {
							boolean isKakaoStory = "KakaoStory".equals(name);
							if(isKakaoStory && !plat.isClientValid()) {
								Message isLine1 = new Message();
								isLine1.what = 1;
								int isWhatsApp2 = R.getStringRes(this.context, "kakaostory_client_inavailable");
								isLine1.obj = this.context.getString(isWhatsApp2);
								UIHandler.sendMessage(isLine1, this);
							} else {
								boolean isLine = "Line".equals(name);
								if(isLine && !plat.isClientValid()) {
									Message isWhatsApp1 = new Message();
									isWhatsApp1.what = 1;
									int isPinterest2 = R.getStringRes(this.context, "line_client_inavailable");
									isWhatsApp1.obj = this.context.getString(isPinterest2);
									UIHandler.sendMessage(isWhatsApp1, this);
								} else {
									boolean isWhatsApp = "WhatsApp".equals(name);
									if(isWhatsApp && !plat.isClientValid()) {
										Message isPinterest1 = new Message();
										isPinterest1.what = 1;
										int isLaiwang2 = R.getStringRes(this.context, "whatsapp_client_inavailable");
										isPinterest1.obj = this.context.getString(isLaiwang2);
										UIHandler.sendMessage(isPinterest1, this);
									} else {
										boolean isPinterest = "Pinterest".equals(name);
										Message isLaiwang1;
										int isLaiwangMoments1;
										if(isPinterest && !plat.isClientValid()) {
											isLaiwang1 = new Message();
											isLaiwang1.what = 1;
											isLaiwangMoments1 = R.getStringRes(this.context, "pinterest_client_inavailable");
											isLaiwang1.obj = this.context.getString(isLaiwangMoments1);
											UIHandler.sendMessage(isLaiwang1, this);
										} else if("Instagram".equals(name) && !plat.isClientValid()) {
											isLaiwang1 = new Message();
											isLaiwang1.what = 1;
											isLaiwangMoments1 = R.getStringRes(this.context, "instagram_client_inavailable");
											isLaiwang1.obj = this.context.getString(isLaiwangMoments1);
											UIHandler.sendMessage(isLaiwang1, this);
										} else {
											boolean isLaiwang = "Laiwang".equals(name);
											boolean isLaiwangMoments = "LaiwangMoments".equals(name);
											if((isLaiwang || isLaiwangMoments) && !plat.isClientValid()) {
												Message isYixin1 = new Message();
												isYixin1.what = 1;
												int data2 = R.getStringRes(this.context, "laiwang_client_inavailable");
												isYixin1.obj = this.context.getString(data2);
												UIHandler.sendMessage(isYixin1, this);
											} else {
												boolean isYixin = "YixinMoments".equals(name) || "Yixin".equals(name);
												if(isYixin && !plat.isClientValid()) {
													Message data1 = new Message();
													data1.what = 1;
													int shareType1 = R.getStringRes(this.context, "yixin_client_inavailable");
													data1.obj = this.context.getString(shareType1);
													UIHandler.sendMessage(data1, this);
												} else {
													HashMap data = (HashMap)ent.getValue();
													byte shareType = 1;
													String imagePath = String.valueOf(data.get("imagePath"));
													if(imagePath != null && (new File(imagePath)).exists()) {
														shareType = 2;
														if(imagePath.endsWith(".gif")) {
															shareType = 9;
														} else if(data.containsKey("url") && !TextUtils.isEmpty(data.get("url").toString())) {
															shareType = 4;
															if(data.containsKey("musicUrl") && !TextUtils.isEmpty(data.get("musicUrl").toString())) {
																shareType = 5;
															}
														}
													} else {
														Bitmap shareCore = (Bitmap)data.get("viewToShare");
														if(shareCore != null && !shareCore.isRecycled()) {
															shareType = 2;
															if(data.containsKey("url") && !TextUtils.isEmpty(data.get("url").toString())) {
																shareType = 4;
																if(data.containsKey("musicUrl") && !TextUtils.isEmpty(data.get("musicUrl").toString())) {
																	shareType = 5;
																}
															}
														} else {
															Object imageUrl = data.get("imageUrl");
															if(imageUrl != null && !TextUtils.isEmpty(String.valueOf(imageUrl))) {
																shareType = 2;
																if(String.valueOf(imageUrl).endsWith(".gif")) {
																	shareType = 9;
																} else if(data.containsKey("url") && !TextUtils.isEmpty(data.get("url").toString())) {
																	shareType = 4;
																	if(data.containsKey("musicUrl") && !TextUtils.isEmpty(data.get("musicUrl").toString())) {
																		shareType = 5;
																	}
																}
															}
														}
													}

													data.put("shareType", Integer.valueOf(shareType));
													if(!started) {
														started = true;
														int shareCore1 = R.getStringRes(this.context, "sharing");
														if(shareCore1 > 0) {
															this.showNotification(this.context.getString(shareCore1));
														}
													}

													plat.setPlatformActionListener(this.callback);
													ShareCore shareCore2 = new ShareCore();
													shareCore2.setShareContentCustomizeCallback(this.customizeCallback);
													shareCore2.share(plat, data);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			return;
		}
	}

	public void onComplete(Platform platform, int action, HashMap<String, Object> res) {
		Message msg = new Message();
		msg.what = 2;
		msg.arg1 = 1;
		msg.arg2 = action;
		msg.obj = platform;
		UIHandler.sendMessage(msg, this);
	}

	public void onError(Platform platform, int action, Throwable t) {
		t.printStackTrace();
		Message msg = new Message();
		msg.what = 2;
		msg.arg1 = 2;
		msg.arg2 = action;
		msg.obj = t;
		UIHandler.sendMessage(msg, this);
		ShareSDK.logDemoEvent(4, platform);
	}

	public void onCancel(Platform platform, int action) {
		Message msg = new Message();
		msg.what = 2;
		msg.arg1 = 3;
		msg.arg2 = action;
		msg.obj = platform;
		UIHandler.sendMessage(msg, this);
		ShareSDK.logDemoEvent(5, platform);
	}

	public boolean handleMessage(Message msg) {
		String nm2;
		switch(msg.what) {
		case 1:
			nm2 = String.valueOf(msg.obj);
			Toast.makeText(this.context, nm2, 0).show();
			break;
		case 2:
			int nm1;
			switch(msg.arg1) {
			case 1:
				nm1 = R.getStringRes(this.context, "share_completed");
				if(nm1 > 0) {
					this.showNotification(this.context.getString(nm1));
				}

				return false;
			case 2:
				nm2 = msg.obj.getClass().getSimpleName();
				int resId;
				if(!"WechatClientNotExistException".equals(nm2) && !"WechatTimelineNotSupportedException".equals(nm2) && !"WechatFavoriteNotSupportedException".equals(nm2)) {
					if("GooglePlusClientNotExistException".equals(nm2)) {
						resId = R.getStringRes(this.context, "google_plus_client_inavailable");
						if(resId > 0) {
							this.showNotification(this.context.getString(resId));
							return false;
						}

						return false;
					} else if("QQClientNotExistException".equals(nm2)) {
						resId = R.getStringRes(this.context, "qq_client_inavailable");
						if(resId > 0) {
							this.showNotification(this.context.getString(resId));
							return false;
						}

						return false;
					} else if(!"YixinClientNotExistException".equals(nm2) && !"YixinTimelineNotSupportedException".equals(nm2)) {
						if("KakaoTalkClientNotExistException".equals(nm2)) {
							resId = R.getStringRes(this.context, "kakaotalk_client_inavailable");
							if(resId > 0) {
								this.showNotification(this.context.getString(resId));
								return false;
							}

							return false;
						} else if("KakaoStoryClientNotExistException".equals(nm2)) {
							resId = R.getStringRes(this.context, "kakaostory_client_inavailable");
							if(resId > 0) {
								this.showNotification(this.context.getString(resId));
								return false;
							}

							return false;
						} else {
							if("WhatsAppClientNotExistException".equals(nm2)) {
								resId = R.getStringRes(this.context, "whatsapp_client_inavailable");
								if(resId > 0) {
									this.showNotification(this.context.getString(resId));
									return false;
								}
							} else {
								resId = R.getStringRes(this.context, "share_failed");
								if(resId > 0) {
									this.showNotification(this.context.getString(resId));
									return false;
								}
							}

							return false;
						}
					} else {
						resId = R.getStringRes(this.context, "yixin_client_inavailable");
						if(resId > 0) {
							this.showNotification(this.context.getString(resId));
							return false;
						}

						return false;
					}
				} else {
					resId = R.getStringRes(this.context, "wechat_client_inavailable");
					if(resId > 0) {
						this.showNotification(this.context.getString(resId));
						return false;
					}

					return false;
				}
			case 3:
				nm1 = R.getStringRes(this.context, "share_canceled");
				if(nm1 > 0) {
					this.showNotification(this.context.getString(nm1));
				}

				return false;
			default:
				return false;
			}
		case 3:
			NotificationManager nm = (NotificationManager)msg.obj;
			if(nm != null) {
				nm.cancel(msg.arg1);
			}
		}

		return false;
	}

	private void showNotification(String text) {
		Toast.makeText(this.context, text, 0).show();
	}

	public void setShareFromQQAuthSupport(boolean shareFromQQLogin) {
		this.shareParamsMap.put("isShareTencentWeibo", Boolean.valueOf(shareFromQQLogin));
	}
}
