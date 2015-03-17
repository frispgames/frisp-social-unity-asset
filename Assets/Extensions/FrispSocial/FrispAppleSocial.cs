using UnityEngine;
using System;
using System.Collections;
#if UNITY_IPHONE && !UNITY_EDITOR

using System.Runtime.InteropServices;

public class FrispAppleSocial : MonoBehaviour  {
  [DllImport ("__Internal")]
  private static extern void _Share (string text, string image);

  private static readonly FrispAppleSocial _singleton = new FrispAppleSocial ();

  private FrispAppleSocial() {}

  public static FrispAppleSocial Instance() {
	return _singleton;
  }

  public void ShareImage(String text, Texture2D image) {
    var imageInBytes = image.EncodeToPNG();
    var base64Image = System.Convert.ToBase64String (imageInBytes);
    _Share(text, base64Image);
  }
}
#endif