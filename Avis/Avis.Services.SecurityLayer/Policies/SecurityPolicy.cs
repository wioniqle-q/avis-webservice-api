using Microsoft.AspNetCore.Builder;

namespace Avis.Services.SecurityLayer.Policies;

public abstract class SecurityPolicy
{
    public static HeaderPolicyCollection HeaderPolicyCollection()
    {
        var headerPolicyCollection = new HeaderPolicyCollection();
        
        headerPolicyCollection.AddFrameOptionsDeny();
        headerPolicyCollection.AddXssProtectionBlock();
        headerPolicyCollection.AddContentTypeOptionsNoSniff();
        headerPolicyCollection.AddReferrerPolicyNoReferrer();
        headerPolicyCollection.AddStrictTransportSecurityMaxAge();
        headerPolicyCollection.AddReferrerPolicyOriginWhenCrossOrigin();
        headerPolicyCollection.RemoveServerHeader();
        
        headerPolicyCollection.AddCrossOriginOpenerPolicy(builder => builder.SameOrigin());
        headerPolicyCollection.AddCrossOriginEmbedderPolicy(builder => builder.RequireCorp());
        headerPolicyCollection.AddCrossOriginResourcePolicy(builder => builder.SameOrigin());
        
        headerPolicyCollection.AddContentSecurityPolicy(options =>
        {
            options.AddObjectSrc().None();
            options.AddBlockAllMixedContent();
            options.AddImgSrc().Self().From("data:");
            options.AddFormAction().Self();
            options.AddFontSrc().Self();
            options.AddStyleSrc().Self().UnsafeInline();
            options.AddScriptSrc().Self().UnsafeInline();
            options.AddBaseUri().Self();
            options.AddFrameAncestors().None();
            options.AddUpgradeInsecureRequests();
            options.AddObjectSrc().None();
            options.AddBlockAllMixedContent();
        });
        
        headerPolicyCollection.AddPermissionsPolicy(options =>
        {
            options.AddAccelerometer().None();
            options.AddAmbientLightSensor().None();
            options.AddAutoplay().None();
            options.AddCamera().None();
            options.AddEncryptedMedia().None();
            options.AddFullscreen().All();
            options.AddGeolocation().None();
            options.AddGyroscope().None();
            options.AddMagnetometer().None();
            options.AddMicrophone().None();
            options.AddMidi().None();
            options.AddPayment().None();
            options.AddPictureInPicture().None();
            options.AddSpeaker().None();
            options.AddSyncXHR().None();
            options.AddUsb().None();
            options.AddVR().None();
        });

        return headerPolicyCollection;
    }
}
