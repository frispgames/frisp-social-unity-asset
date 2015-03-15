using UnityEngine;
using System;
using System.Collections;
#if UNITY_IPHONE && !UNITY_EDITOR

using System.Runtime.InteropServices;

public class FrispAppleSocial : MonoBehaviour  {
  [DllImport ("__Internal")]
  private static extern void _Share (string text, string image);

  public static void ShareImage(String text, Texture2D image) {
    byte[] imageInBytes = image.EncodeToPNG();
    string base64Image = System.Convert.ToBase64String (imageInBytes);
    _Share(text, base64Image);
  }
}
#endif