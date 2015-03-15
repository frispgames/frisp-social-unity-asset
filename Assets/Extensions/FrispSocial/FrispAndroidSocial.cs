using UnityEngine;
using System;
using System.Collections;
#if UNITY_ANDROID

public class FrispAndroidSocial {
	static FrispAndroidSocial singleton = null;

	public static FrispAndroidSocial instance() {
		if (singleton == null) {
			singleton = new FrispAndroidSocial();
		}
		return singleton;
	}

	public void ShareImage(String title, String text, Texture2D image) {
		byte[] imageInBytes = image.EncodeToPNG();
		string base64Image = System.Convert.ToBase64String (imageInBytes);

		using (AndroidJavaClass socialClass = new AndroidJavaClass ("com.frispgames.frispsocial.FrispSocial")) {
			socialClass.CallStatic ("shareImage", new String[]{title, text, base64Image});
		}
	}
}
#endif