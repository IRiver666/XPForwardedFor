string baseData = @"{"navigator_properties":{"hasBeenActive":"true","userAgent":"youruseragent","webdriver":"false"},"created_at":timestamp_milliseconds}";
string mainKey = "0e6be1f1e21ffc33590b888fd4dc81b19713e570e805d4e5df80a493c9571a05";
string guestId = "v1%3A174849298500261196";
string encrypted = XPForwardedFor.GenerateXP.Encrypt(baseData, mainKey, guestId);
string decrypted =  XPForwardedFor.GenerateXP.Decrypt(encrypted , mainKey , guestId);

Console.WriteLine(encrypted);

Console.WriteLine(decrypted);
