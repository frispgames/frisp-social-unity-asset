#import "FGSocial.h"

@implementation FGSocial

  - (void) share:(NSString *)text  media:(NSString *) media {
    // Create image from image data
    NSData *imageData = [[NSData alloc] initWithBase64EncodedString:media options: 0];
    UIImage *image = [[UIImage alloc] initWithData:imageData];

    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
      UIActivityViewController *socialViewController = [[UIActivityViewController alloc] initWithActivityItems:@[text, image] applicationActivities:nil];

      UIViewController *viewController = UnityGetGLViewController();

      // required by iOS 8.
      if([socialViewController respondsToSelector:@selector(popoverPresentationController)]) {
        socialViewController.popoverPresentationController.sourceView = viewController.view;
      }

      dispatch_async(dispatch_get_main_queue(), ^{
        [viewController presentViewController:socialViewController animated:YES completion:nil];
      });
    });
  }

extern "C" {
  void _Share(char* text, char* encodedMedia) {
    NSString *status = [NSString stringWithUTF8String: text];
    NSString *media = [NSString stringWithUTF8String: encodedMedia];

    [[[FGSocial alloc] init] share:status media:media];
  }
}

@end