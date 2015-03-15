# Frisp Social Unity Asset
Unity asset for native sharing a screenshot on Android and iPhone.

### Development

I have set the repository up as a unity project. When adding features you will need to open the contents of the repository in unity. For any new features add a new UI Button and connect the feature up to that button and name it accordingly.

If you would like to make any changes or update the android layer of the asset you can do so by changing the contents of the frisp-social android lbrary. You will then need to replace ```frisp-social.jar``` inside of this repo with the one generated from working on frisp-social android lbrary.

### Setting up for your own project

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
