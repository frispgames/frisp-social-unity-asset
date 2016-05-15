using UnityEngine;
using System.Collections;

namespace FrispGames.Social {

	public class ScreenshotSharer : MonoBehaviour {

		private static readonly ScreenshotSharer _singleton = new ScreenshotSharer ();

		private ScreenshotSharer() {}

		public static ScreenshotSharer Instance() {
			return _singleton;
		}

		public IEnumerator PostScreenshot(string title, string message) {
			yield return new WaitForEndOfFrame();
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
			// Read screen contents into the texture
			tex.ReadPixels(new Rect(0, 0, width, height), 0, 0 );
			tex.Apply();

			if (Application.platform == RuntimePlatform.Android) {
				Api.AndroidScreenshotSharer.Instance ().ShareImage (title, message, tex);
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				Api.AppleScreenshotSharer.Instance().ShareImage(message,tex);
			}

			Destroy(tex);
		}
	}
}