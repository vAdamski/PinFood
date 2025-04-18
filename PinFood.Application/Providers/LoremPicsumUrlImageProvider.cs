namespace PinFood.Application.Providers;

public static class LoremPicsumUrlImageProvider
{
	public static string GenerateImageUrl(int width, int height)
	{
		return $"https://picsum.photos/{width}/{height}";
	}
	
	public static List<string> GenerateImageUrls(int count, int width, int height)
	{
		var urls = new List<string>();
		for (int i = 0; i < count; i++)
		{
			urls.Add(GenerateImageUrl(width, height));
		}
		return urls;
	}
}