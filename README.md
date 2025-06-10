# Example
```CSharp
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
    ""created_at"":1748492990477
}";
string mainKey = "0e6be1f1e21ffc33590b888fd4dc81b19713e570e805d4e5df80a493c9571a05";
string guestId = "v1%3A174849298500261196";
string encrypted = XPForwardedFor.GenerateXP.Encrypt(baseData, mainKey, guestId);
string decrypted =  XPForwardedFor.GenerateXP.Decrypt(encrypted , mainKey , guestId);

Console.WriteLine(encrypted);

Console.WriteLine(decrypted);
```
