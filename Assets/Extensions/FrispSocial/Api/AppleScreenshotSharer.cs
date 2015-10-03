using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace FrispSocial.Api {
	public class AppleScreenshotSharer : MonoBehaviour  {
		[DllImport ("__Internal")]
		private static extern void _Share (string text, string image);

		private static readonly AppleScreenshotSharer _singleton = new AppleScreenshotSharer ();

		private AppleScreenshotSharer() {}

		public static AppleScreenshotSharer Instance() {
			return _singleton;
		}

		public void ShareImage(String text, Texture2D image) {
			#if UNITY_IPHONE && !UNITY_EDITOR
			    var imageInBytes = image.EncodeToPNG();
			    var base64Image = System.Convert.ToBase64String (imageInBytes);
			    _Share(text, base64Image);
			#endif
			#if UNITY_EDITOR
				print("Mock share of iPhone screenshot");
			#endif
		}
	}
}