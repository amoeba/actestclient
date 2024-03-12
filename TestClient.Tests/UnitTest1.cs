namespace TestClient.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			Span<byte> bytes = new Span<byte>();
			var res = TestClient.Crypto.Checksum.GetMagicNumber(bytes, 32, true);

			Assert.Equal((uint)32, res);
		}
	}
}
