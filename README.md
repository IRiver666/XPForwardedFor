# Example
```CSharp
string baseData = "your-data";
string mainKey = "your-main-key";
string guestId = "guest-id";
string encrypted = XPForwardedFor.GenerateXP.Encrypt(baseData, mainKey, guestId);
Console.WriteLine(encrypted);
```
