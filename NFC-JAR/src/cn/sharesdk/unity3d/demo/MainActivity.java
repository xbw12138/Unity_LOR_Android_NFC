package cn.sharesdk.unity3d.demo;

import android.annotation.SuppressLint;
import android.app.PendingIntent;
import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.DialogInterface.OnClickListener;
import android.content.IntentFilter.MalformedMimeTypeException;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.nfc.tech.Ndef;
import android.nfc.tech.NdefFormatable;
import android.os.Bundle;
import android.os.Parcelable;
import android.widget.Toast;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import java.io.IOException;
import java.nio.charset.Charset;
import org.json.JSONException;
import org.json.JSONObject;

public class MainActivity extends UnityPlayerActivity {
	NfcAdapter mNfcAdapter;
	PendingIntent mNfcPendingIntent;
	IntentFilter[] mReadTagFilters;
	IntentFilter[] mWriteTagFilters;
	private boolean mWriteMode = false;
	String mName;
	String reNFC = "N";

	public MainActivity() {
	}

	@SuppressLint({"NewApi"})
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		this.mNfcAdapter = NfcAdapter.getDefaultAdapter(this);
		if(this.mNfcAdapter == null) {
			Toast.makeText(this, "Your device does not support NFC. Cannot run demo.", 1).show();
			this.finish();
		} else {
			this.checkNfcEnabled();
			this.mNfcPendingIntent = PendingIntent.getActivity(this, 0, (new Intent(this, this.getClass())).addFlags(536870912), 0);
			IntentFilter ndefDetected = new IntentFilter("android.nfc.action.NDEF_DISCOVERED");

			try {
				ndefDetected.addDataType("application/xbw.game.playground.nfc");
			} catch (MalformedMimeTypeException var4) {
				throw new RuntimeException("Could not add MIME type.", var4);
			}

			IntentFilter tagDetected = new IntentFilter("android.nfc.action.TAG_DISCOVERED");
			this.mWriteTagFilters = new IntentFilter[]{tagDetected};
			this.mReadTagFilters = new IntentFilter[]{ndefDetected, tagDetected};
		}
	}

	@SuppressLint({"NewApi"})
	protected void onResume() {
		super.onResume();
		this.checkNfcEnabled();
		if(this.getIntent().getAction() != null) {
			this.getIntent().getAction().equals("android.nfc.action.NDEF_DISCOVERED");
		}

		this.mNfcAdapter.enableForegroundDispatch(this, this.mNfcPendingIntent, this.mReadTagFilters, (String[][])null);
	}

	@SuppressLint({"NewApi"})
	protected void onPause() {
		super.onPause();
		this.mNfcAdapter.disableForegroundDispatch(this);
	}

	protected void onNewIntent(Intent intent) {
		if(!this.mWriteMode) {
			if(intent.getAction().equals("android.nfc.action.NDEF_DISCOVERED")) {
				NdefMessage[] detectedTag = this.getNdefMessagesFromIntent(intent);
				this.confirmDisplayedContentOverwrite(detectedTag[0]);
			} else if(intent.getAction().equals("android.nfc.action.TAG_DISCOVERED")) {
				Toast.makeText(this, "This NFC tag currently has no inventory NDEF data.", 1).show();
			}
		} else if(intent.getAction().equals("android.nfc.action.TAG_DISCOVERED")) {
			Tag detectedTag1 = (Tag)intent.getParcelableExtra("android.nfc.extra.TAG");
			this.writeTag(this.createNdefFromJson(), detectedTag1);
		}

	}

	@SuppressLint({"NewApi"})
	NdefMessage[] getNdefMessagesFromIntent(Intent intent) {
		NdefMessage[] msgs = null;
		String action = intent.getAction();
		if(!action.equals("android.nfc.action.TAG_DISCOVERED") && !action.equals("android.nfc.action.NDEF_DISCOVERED")) {
			this.finish();
		} else {
			Parcelable[] rawMsgs = intent.getParcelableArrayExtra("android.nfc.extra.NDEF_MESSAGES");
			if(rawMsgs != null) {
				msgs = new NdefMessage[rawMsgs.length];

				for(int empty = 0; empty < rawMsgs.length; ++empty) {
					msgs[empty] = (NdefMessage)rawMsgs[empty];
				}
			} else {
				byte[] var8 = new byte[0];
				NdefRecord record = new NdefRecord((short)5, var8, var8, var8);
				NdefMessage msg = new NdefMessage(new NdefRecord[]{record});
				msgs = new NdefMessage[]{msg};
			}
		}

		return msgs;
	}

	@SuppressLint({"NewApi"})
	private void confirmDisplayedContentOverwrite(NdefMessage msg) {
		String payload = new String(msg.getRecords()[0].getPayload());
		this.setTextFieldValues(payload);
	}

	private void setTextFieldValues(String jsonString) {
		JSONObject inventory = null;
		String name = "";

		try {
			inventory = new JSONObject(jsonString);
			name = inventory.getString("name");
		} catch (JSONException var5) {
			;
		}

		UnityPlayer.UnitySendMessage("Player", "NFC", name);
	}

	public void StartActivity0(String name) {
		this.mName = name;
		this.enableTagWriteMode();
	}

	public void StartActivity1(String name) {
		this.enableTagReadMode();
	}

	@SuppressLint({"NewApi"})
	private NdefMessage createNdefFromJson() {
		JSONObject computerSpecs = new JSONObject();

		try {
			computerSpecs.put("name", this.mName);
		} catch (JSONException var9) {
			;
		}

		String mimeType = "application/xbw.game.playground.nfc";
		byte[] mimeBytes = mimeType.getBytes(Charset.forName("UTF-8"));
		String data = computerSpecs.toString();
		byte[] dataBytes = data.getBytes(Charset.forName("UTF-8"));
		byte[] id = new byte[0];
		NdefRecord record = new NdefRecord((short)2, mimeBytes, id, dataBytes);
		NdefMessage m = new NdefMessage(new NdefRecord[]{record});
		return m;
	}

	@SuppressLint({"NewApi"})
	private void enableTagWriteMode() {
		this.mWriteMode = true;
		this.mNfcAdapter.enableForegroundDispatch(this, this.mNfcPendingIntent, this.mWriteTagFilters, (String[][])null);
	}

	@SuppressLint({"NewApi"})
	private void enableTagReadMode() {
		this.mWriteMode = false;
		this.mNfcAdapter.enableForegroundDispatch(this, this.mNfcPendingIntent, this.mReadTagFilters, (String[][])null);
	}

	@SuppressLint({"NewApi"})
	boolean writeTag(NdefMessage message, Tag tag) {
		int size = message.toByteArray().length;

		try {
			Ndef e = Ndef.get(tag);
			if(e != null) {
				e.connect();
				if(!e.isWritable()) {
					Toast.makeText(this, "Cannot write to this tag. This tag is read-only.", 1).show();
					return false;
				} else if(e.getMaxSize() < size) {
					Toast.makeText(this, "Cannot write to this tag. Message size (" + size + " bytes) exceeds this tag\'s capacity of " + e.getMaxSize() + " bytes.", 1).show();
					return false;
				} else {
					e.writeNdefMessage(message);
					UnityPlayer.UnitySendMessage("Player", "reNFC", this.reNFC);
					Toast.makeText(this, "A pre-formatted tag was successfully updated.", 1).show();
					return true;
				}
			} else {
				NdefFormatable format = NdefFormatable.get(tag);
				if(format != null) {
					try {
						format.connect();
						format.format(message);
						Toast.makeText(this, "This tag was successfully formatted and updated.", 1).show();
						return true;
					} catch (IOException var7) {
						Toast.makeText(this, "Cannot write to this tag due to I/O Exception.", 1).show();
						return false;
					}
				} else {
					Toast.makeText(this, "Cannot write to this tag. This tag does not support NDEF.", 1).show();
					return false;
				}
			}
		} catch (Exception var8) {
			Toast.makeText(this, "Cannot write to this tag due to an Exception.", 1).show();
			return false;
		}
	}

	@SuppressLint({"NewApi"})
	private void checkNfcEnabled() {
		Boolean nfcEnabled = Boolean.valueOf(this.mNfcAdapter.isEnabled());
		if(!nfcEnabled.booleanValue()) {
			(new Builder(this)).setTitle("NFC目前关闭!").setMessage("请点击进入设置开启NFC功能").setCancelable(false).setPositiveButton("开启NFC", new OnClickListener() {
				@SuppressLint({"NewApi"})
				public void onClick(DialogInterface dialog, int id) {
					MainActivity.this.startActivity(new Intent("android.settings.NFC_SETTINGS"));
				}
			}).create().show();
		}

	}
}
