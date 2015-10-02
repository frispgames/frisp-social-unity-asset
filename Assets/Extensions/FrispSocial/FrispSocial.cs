using UnityEngine;
using System.Collections;

public class FrispSocial : MonoBehaviour {

	private static readonly FrispSocial _singleton = new FrispSocial ();

	private FrispSocial() {}
	
	public static FrispSocial Instance() {
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
		
		#if UNITY_IPHONE && !UNITY_EDITOR
		FrispAppleSocial.Instance().ShareImage(message, tex);
		#endif
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		FrispAndroidSocial.Instance().ShareImage(title, message, tex);
		#endif
		
		Destroy(tex);
	}
}