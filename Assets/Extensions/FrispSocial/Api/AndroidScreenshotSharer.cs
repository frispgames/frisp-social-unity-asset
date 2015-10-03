using UnityEngine;
using System;
using System.Collections;

namespace FrispSocial.Api {
	public class AndroidScreenshotSharer : MonoBehaviour {
		private static readonly AndroidScreenshotSharer _singleton = new AndroidScreenshotSharer ();

		private AndroidScreenshotSharer() {}

		public static AndroidScreenshotSharer Instance() {
			return _singleton;
		}

		public void ShareImage(String title, String text, Texture2D image) {
			#if UNITY_ANDROID && !UNITY_EDITOR
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
			#endif
			#if UNITY_EDITOR
				print("Mock share of Android screenshot");
			#endif
		}
	}
}