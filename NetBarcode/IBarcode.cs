using NetBarcode.Graphics;
using System.Collections.Generic;

namespace NetBarcode
{
    internal interface IBarcode
    {
		string Data { get; }
        List<Bar> GetEncoding();
		IRenderer Renderer { get; }
    }
}