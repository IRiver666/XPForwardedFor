// See https://aka.ms/new-console-template for more information
string baseData = @"{
    ""webgl_fingerprint"":"""",
    ""canvas_fingerprint"":"""",
    ""navigator_properties"":{
        ""hasBeenActive"":""true"",
        ""userAgent"":"""",
        ""webdriver"":""false""
    },
    ""codec_fingerprint"":"""",
    ""audio_fingerprint"":"""",
    ""audio_properties"":null,
    ""created_at"":0
}";
string mainKey = "your-main-key";
string guestId = "guest-id";
string encrypted = XPForwardedFor.GenerateXP.Encrypt(baseData, mainKey, guestId);
Console.WriteLine(encrypted);
