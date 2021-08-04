using System.Text.Json;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Response
{
    /// <summary>
    /// Unit test for <see cref="ProfileResponse"/>.
    /// </summary>
    public class ProfileResponseTest
    {
        [Theory]
        [InlineData(@"{""name"":""textures"",""value"":""ewogICJ0aW1lc3RhbXAiIDogMTYyMzgwMzY2MjIxNywKICAicHJvZmlsZUlkIiA6ICJjYjI2NzFkNTkwYjg0ZGZlOWIxYzczNjgzZDQ1MWQxYSIsCiAgInByb2ZpbGVOYW1lIiA6ICJQb3RhdG9NYXN0ZXIxMDEiLAogICJ0ZXh0dXJlcyIgOiB7CiAgICAiU0tJTiIgOiB7CiAgICAgICJ1cmwiIDogImh0dHA6Ly90ZXh0dXJlcy5taW5lY3JhZnQubmV0L3RleHR1cmUvODE2MmY1Yjk3ZDAxNWE5ZDZiMWM1ZTc4M2FmNDg3MDFiYzgyYjU1YTRmNTliNWU2ZTA1NzQ5ZDA2YjhlZjgiCiAgICB9CiAgfQp9""}",
                    "http://textures.minecraft.net/texture/8162f5b97d015a9d6b1c5e783af48701bc82b55a4f59b5e6e05749d06b8ef8", null, false)]
        [InlineData(@"{""name"":""textures"",""value"":""ewogICJ0aW1lc3RhbXAiIDogMTYyMzgwNDA0NDIyNCwKICAicHJvZmlsZUlkIiA6ICIwNjlhNzlmNDQ0ZTk0NzI2YTViZWZjYTkwZTM4YWFmNSIsCiAgInByb2ZpbGVOYW1lIiA6ICJOb3RjaCIsCiAgInRleHR1cmVzIiA6IHsKICAgICJTS0lOIiA6IHsKICAgICAgInVybCIgOiAiaHR0cDovL3RleHR1cmVzLm1pbmVjcmFmdC5uZXQvdGV4dHVyZS8yOTIwMDlhNDkyNWI1OGYwMmM3N2RhZGMzZWNlZjA3ZWE0Yzc0NzJmNjRlMGZkYzMyY2U1NTIyNDg5MzYyNjgwIgogICAgfQogIH0KfQ==""}",
                    "http://textures.minecraft.net/texture/292009a4925b58f02c77dadc3ecef07ea4c7472f64e0fdc32ce5522489362680", null, false)]
        [InlineData(@"{""name"":""textures"",""value"":""ewogICJ0aW1lc3RhbXAiIDogMTYyMzgwNDIyMDcyMywKICAicHJvZmlsZUlkIiA6ICI2MTY5OWIyZWQzMjc0YTAxOWYxZTBlYThjM2YwNmJjNiIsCiAgInByb2ZpbGVOYW1lIiA6ICJEaW5uZXJib25lIiwKICAidGV4dHVyZXMiIDogewogICAgIlNLSU4iIDogewogICAgICAidXJsIiA6ICJodHRwOi8vdGV4dHVyZXMubWluZWNyYWZ0Lm5ldC90ZXh0dXJlLzUwYzQxMGZhZDhkOWQ4ODI1YWQ1NmIwZTQ0M2UyNzc3YTZiNDZiZmEyMGRhY2QxZDJmNTVlZGM3MWZiZWIwNmQiCiAgICB9LAogICAgIkNBUEUiIDogewogICAgICAidXJsIiA6ICJodHRwOi8vdGV4dHVyZXMubWluZWNyYWZ0Lm5ldC90ZXh0dXJlLzU3ODZmZTk5YmUzNzdkZmI2ODU4ODU5ZjkyNmM0ZGJjOTk1NzUxZTkxY2VlMzczNDY4YzVmYmY0ODY1ZTcxNTEiCiAgICB9CiAgfQp9""}",
                    "http://textures.minecraft.net/texture/50c410fad8d9d8825ad56b0e443e2777a6b46bfa20dacd1d2f55edc71fbeb06d", "http://textures.minecraft.net/texture/5786fe99be377dfb6858859f926c4dbc995751e91cee373468c5fbf4865e7151", false)]
        [InlineData(@"{""name"":""textures"",""value"":""ewogICJ0aW1lc3RhbXAiIDogMTYyMzgwNTA0MjQ5NywKICAicHJvZmlsZUlkIiA6ICI3OTIxNzUwOGEwNDg0YTc4YWJiYTUyYzY2MDQ5ZjAyOCIsCiAgInByb2ZpbGVOYW1lIiA6ICJfUXVlZW5LYXRobGVlbl8iLAogICJ0ZXh0dXJlcyIgOiB7CiAgICAiU0tJTiIgOiB7CiAgICAgICJ1cmwiIDogImh0dHA6Ly90ZXh0dXJlcy5taW5lY3JhZnQubmV0L3RleHR1cmUvMmE1ZjhlMzUxMTZhZDBhZDZjZmY5ZmYxZWFlZWFmMGM2N2FjYzliMmE2NmRiZjFmODdhMThjYmEzNGFiZjVkZCIsCiAgICAgICJtZXRhZGF0YSIgOiB7CiAgICAgICAgIm1vZGVsIiA6ICJzbGltIgogICAgICB9CiAgICB9CiAgfQp9""}",
                    "http://textures.minecraft.net/texture/2a5f8e35116ad0ad6cff9ff1eaeeaf0c67acc9b2a66dbf1f87a18cba34abf5dd", null, true)]
        public void TexturePropertyConstructor_Sets_FromBase64String(string json, string skin, string cape, bool slim)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act
            var texture = new ProfileResponse.TextureProperty(jsonDoc.RootElement);

            // assert
            Assert.Equal(skin, texture.SkinUrl);
            Assert.Equal(cape, texture.CapeUrl);
            Assert.Equal(slim, texture.SlimSkin);
        }
    }
}
