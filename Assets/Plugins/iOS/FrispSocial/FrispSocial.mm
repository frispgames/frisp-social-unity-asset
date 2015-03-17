#import "FrispSocial.h"

@implementation FrispSocial

 static FrispSocial * _instance = [[FrispSocial alloc] init];

+ (id) instance {
    return _instance;
}

- (void) share:(NSString *)text  media:(NSString *)media {
  UIActivityViewController *socialViewController;

  // Create image from image data
  NSData *imageData = [[NSData alloc] initWithBase64EncodedString:media options: 0];
  UIImage *image = [[UIImage alloc] initWithData:imageData];

  socialViewController = [[UIActivityViewController alloc] initWithActivityItems:@[text, image] applicationActivities:nil];

  UIViewController *rootViewController =  UnityGetGLViewController();

  [rootViewController presentViewController:socialViewController animated:YES completion:nil];
}

extern "C" { 
  void _Share(char* text, char* encodedMedia) {
    NSString *status = [NSString stringWithUTF8String: text];
    NSString *media = [NSString stringWithUTF8String: encodedMedia];
    
    [[FrispSocial instance] share:status media:media];
  }
}

@end 