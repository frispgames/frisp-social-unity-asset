using UnityEngine;
using System.Collections;

public class ExampleShare : MonoBehaviour {
	
	public void shareScreenShot() {
		StartCoroutine (PostScreenshot());
	}
	
	private IEnumerator PostScreenshot() {    
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
		
		#if UNITY_IPHONE && !UNITY_EDITOR
			FrispAppleSocial.Instance().ShareImage("Testing", tex);
		#endif
		
		#if UNITY_ANDROID
			FrispAndroidSocial.Instance().ShareImage("Title", "Testing", tex);
		#endif
		
		Destroy(tex);
	}
}