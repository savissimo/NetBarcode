namespace NetBarcode.Graphics
{
	public enum BarSize
	{
		Regular, 
		Long
	}

	public class Bar
	{
		public int Value;
		public BarSize Size;

		public Bar(int i_value, BarSize i_size)
		{
			Value = i_value;
			Size = i_size;
		}
	}
}
