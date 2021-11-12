# Mojang API
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/PotatoMaster101/mojapi/blob/main/LICENCE)
[![.NET](https://github.com/PotatoMaster101/mojapi/actions/workflows/dotnet.yml/badge.svg)](https://github.com/PotatoMaster101/mojapi/actions/workflows/dotnet.yml)
[![Nuget](https://img.shields.io/nuget/v/PotatoMaster101.Mojapi)](https://www.nuget.org/packages/PotatoMaster101.Mojapi/)

**Deprecation notice: This project will be archived once [PotatoMaster101/MojSharp](https://github.com/PotatoMaster101/MojSharp) is completed.**

An asynchronous [Mojang API](https://wiki.vg/Mojang_API) wrapper in C# targeting `.NET 5` and above.

## Build
Build with [`dotnet`](https://dotnet.microsoft.com/download):
```
$ dotnet build -c Release
```

## Test
Run unit tests on local PC:
```
$ dotnet test -c Release
```

Run unit tests in docker:
```
$ docker build -t mojapi .
```

## Quick Start
### I want to authenticate!
```cs
var auth = await new AuthenticationEndpoint(new Credentials("username", "password")).Request();
Console.WriteLine(auth.Token.AccessToken);
Console.WriteLine(auth.Token.ClientToken);
```

### Who the hell is this guy? Give me his past names!
```cs
var player = await new SingleUuidEndpoint("player").Request();
var history = await new NameHistoryEndpoint(player.Player.Uuid).Request();

foreach (var (name, timestamp) in history.History)
    Console.WriteLine($"{name}, {timestamp}");
```

### Give me this guy's skin! I like it!
```cs
var player = await new SingleUuidEndpoint("player").Request();
var profile = await new ProfileEndpoint(player.Player.Uuid).Request();
Console.WriteLine(profile.Texture.SkinUrl);
```

### Let me change my skin!
Change skin to a local file:
```cs
var skin = new Skin("some/minecraft/skin.png", Skin.SlimStyle);     // or Skin.ClassicStyle
var upload = await new UploadSkinEndpoint("access token", skin).Request();
Console.WriteLine(upload.ChangedSkin.Url);
```

Change skin to a network image:
```cs
var skin = new Skin("https://some/online/skin.png", Skin.SlimStyle);    // or Skin.ClassicStyle
var upload = await new ChangeSkinEndpoint("access token", skin).Request();
Console.WriteLine(upload.ChangedSkin.Url);
```

### More examples!
More sample projects can be found under `examples/`
