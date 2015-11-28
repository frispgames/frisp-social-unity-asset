#import "FGSocial.h"
#define IPAD UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad

@implementation FGSocial

  - (void) share:(NSString *)text  media:(NSString *) media {
    UIActivityViewController *socialViewController;
    
    // Create image from image data
    NSData *imageData = [[NSData alloc] initWithBase64EncodedString:media options: 0];
    UIImage *image = [[UIImage alloc] initWithData:imageData];
    
    socialViewController = [[UIActivityViewController alloc] initWithActivityItems:@[text, image] applicationActivities:nil];
    
    UIViewController *rootViewController = [[[[UIApplication sharedApplication]delegate] window] rootViewController];
    if (IPAD) {
      CGRect midScreen = CGRectMake(rootViewController.view.frame.size.width/2, rootViewController.view.frame.size.height/4, 0, 0);
      UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:socialViewController];
      [popup presentPopoverFromRect:midScreen inView:rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
    } else {
      [rootViewController presentViewController:socialViewController animated:YES completion:nil];
    }
  }

extern "C" {
  void _Share(char* text, char* encodedMedia) {
    NSString *status = [NSString stringWithUTF8String: text];
    NSString *media = [NSString stringWithUTF8String: encodedMedia];
    
    [[[FGSocial alloc] init] share:status media:media];
  }
}

@end