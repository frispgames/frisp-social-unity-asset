@interface FrispSocial : NSObject 
+ (id) instance;
- (void) share:(NSString*)text media: (NSString*) media;
@end