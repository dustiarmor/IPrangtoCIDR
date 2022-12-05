# IPrangtoCIDR 
Thank to https://blog.ip2location.com/knowledge-base/how-to-convert-ip-address-range-into-cidr/

1. vs studio F5 run test
2. input ip rang to left richtextBox, such like :

   `1.1.1.1,2.2.2.2`
   
    `1.2.1.1,1.2.1.8`
    
    `...`
3. press Button
#

```
public static List<string> iprange2cidr(string ipStart, string ipEnd)
{
    long start = ip2long(ipStart);
    long end = ip2long(ipEnd);
    var result = new List<string>();
 
    while (end >= start)
    {
        byte maxSize = 32;
        while (maxSize > 0)
        {
            long mask = iMask(maxSize - 1);
            long maskBase = start & mask;
 
            if (maskBase != start)
            {
                break;
            }
 
            maxSize--;
        }
        double x = Math.Log(end - start + 1) / Math.Log(2);
        byte maxDiff = (byte)(32 - Math.Floor(x));
        if (maxSize < maxDiff)
        {
            maxSize = maxDiff;
        }
        string ip = long2ip(start);
        result.Add(ip + "/" + maxSize);
        start += (long)Math.Pow(2, (32 - maxSize));
    }
    return result;
}
```
``` 
public static List<string> iprange2cidr(int ipStart, int ipEnd)
{
    long start = ipStart;
    long end = ipEnd;
    var result = new List<string>();
 
    while (end >= start)
    {
        byte maxSize = 32;
        while (maxSize > 0)
        {
            long mask = iMask(maxSize - 1);
            long maskBase = start & mask;
 
            if (maskBase != start)
            {
                break;
            }
 
            maxSize--;
        }
        double x = Math.Log(end - start + 1) / Math.Log(2);
        byte maxDiff = (byte)(32 - Math.Floor(x));
        if (maxSize < maxDiff)
        {
            maxSize = maxDiff;
        }
        string ip = long2ip(start);
        result.Add(ip + "/" + maxSize);
        start += (long)Math.Pow(2, (32 - maxSize));
    }
    return result;
}

```
```
private static long iMask(int s)
{
    return (long)(Math.Pow(2, 32) - Math.Pow(2, (32 - s)));
}
 ```
 ```
private static string long2ip(long ipAddress)
{
    System.Net.IPAddress ip;
    if (System.Net.IPAddress.TryParse(ipAddress.ToString(), out ip))
    {
        return ip.ToString();
    }
    return "";
}
 ```
 ```
private static long ip2long(string ipAddress)
{
    System.Net.IPAddress ip;
    if (System.Net.IPAddress.TryParse(ipAddress, out ip))
    {
        return (((long)ip.GetAddressBytes()[0] << 24) | ((long)ip.GetAddressBytes()[1] << 16) | ((long)ip.GetAddressBytes()[2] << 8) | ip.GetAddressBytes()[3]);
    }
    return -1;
}
```



<a href="https://www.buymeacoffee.com/dustiarmorb" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>
