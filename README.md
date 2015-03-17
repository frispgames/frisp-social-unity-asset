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
### Troubleshooting
#### Android:
* INSTALL_FAILED_CONTAINER_ERROR: When exporting the project to Android Studio change:
``` android:installLocation="preferExternal" ```
to:
``` android:installLocation="auto" ```
on the following line:
```
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.frispgames.frispsocialunityasset" android:versionName="1.0" android:versionCode="1" android:installLocation="preferExternal">
```
* Permission issues: Make sure the following line is in your AndroidManifest.xml this should be under ```Plugins/Android``` if not you can find one under ```Temp/StagingArea``` after building for android. Add this into your ```Plugins/Android``` and add the below line:
```
  <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
```

