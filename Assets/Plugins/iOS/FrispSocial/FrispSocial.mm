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
  
  if ([socialViewController respondsToSelector:@selector(popoverPresentationController)]) {
    // IOS8 iPad
    UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:socialViewController];
    [popup presentPopoverFromRect:CGRectMake(rootViewController.view.frame.size.width/2, rootViewController.view.frame.size.height/4, 0, 0)inView:rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
  } else {
        [rootViewController presentViewController:socialViewController animated:YES completion:nil];
  }
}

extern "C" { 
  void _Share(char* text, char* encodedMedia) {
    NSString *status = [NSString stringWithUTF8String: text];
    NSString *media = [NSString stringWithUTF8String: encodedMedia];
    
    [[FrispSocial instance] share:status media:media];
  }
}

@end 