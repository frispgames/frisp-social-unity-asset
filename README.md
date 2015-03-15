# Frisp Social Unity Asset
Unity asset for native sharing a screenshot on Android and iPhone.

### Development

I have set the repository up as a unity project. When adding features you will need to open the contents of the repository in unity. For any new features add a new UI Button and connect the feature up to that button and name it accordingly.

If you would like to make any changes or update the android layer of the asset you can do so by changing the contents of the [android-social-library](https://github.com/frispgames/android-social-library). You will then need to replace ```frisp-social.jar``` inside of this repo with the one generated from working on [android-social-library](https://github.com/frispgames/android-social-library).

### Installing into your own project

* Download the [asset](https://github.com/frispgames/frisp-social-unity-asset/blob/master/package/frisp-social.unitypackage) and import it into your unity project.
* Create a class that takes a screenshot of the game and then use the API provided with the asset to share it. See the below class as an example:
```CSharp
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
			FrispAppleSocial.ShareImage("Testing", tex);
		#endif
		
		#if UNITY_ANDROID
			FrispAndroidSocial.instance().ShareImage("Title", "Testing", tex);
		#endif
		
		Destroy(tex);
	}
}
```
* Add the following to Plugins/Android/AndroidManifest.xml inside your project:
If you don't already have an AndroidManifest.xml use this [one](https://github.com/frispgames/android-social-library/blob/master/AndroidManifest.xml)
```XML
	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE"></uses-permission>
	<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE"></uses-permission>
```
### Troubleshooting
#### Android:
* INSTALL_FAILED_CONTAINER_ERROR: This comes up all the time for me. What you need to do is unistall the application on the android phone if it is already installed and then erase the SD card on the emulator. You can do this by going to Settings->Storage->Erase SD card
