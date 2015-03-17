using UnityEngine;
using System;
using System.Collections;

#if UNITY_ANDROID && !UNITY_EDITOR
public class FrispAndroidSocial {
	private static readonly FrispAndroidSocial _singleton = new FrispAndroidSocial ();

	private FrispAndroidSocial() {}

	public static FrispAndroidSocial Instance() {
		return _singleton;
	}

	public void ShareImage(String title, String text, Texture2D image) {
		var imageInBytes = image.EncodeToPNG();
		var base64Image = System.Convert.ToBase64String (imageInBytes);
		var socialClass = new AndroidJavaClass (
			"com.frispgames.frispsocial.FrispSocial"
		);

		using (socialClass) {
			socialClass.CallStatic (
				"shareImage", new String[]{title, text, base64Image}
			);
		}
	}
}
#endif