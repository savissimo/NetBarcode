using NetBarcode.Graphics;
using NetBarcode.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace NetBarcode
{
    public enum Type
    {
        Code11,
        Code128,
        Code39,
        Code93,
        EAN13,
        EAN8
    }

    public class Barcode
    {
        private readonly string _data;
        private readonly Type _type = Type.Code128;
        private List<Bar> _encodedData;
        private readonly Color _foregroundColor = Color.Black;
        private readonly Color _backgroundColor = Color.White;
        private int _width = 300;
        private int _height = 150;
        private readonly bool _autoSize = true;
        private readonly bool _showLabel = false;
        private readonly Font _labelFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        private readonly LabelPosition _labelPosition = LabelPosition.BottomCenter;
        private readonly AlignmentPosition _alignmentPosition = AlignmentPosition.Center;
		private IBarcode m_barcode = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode"/> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        public Barcode(string data)
        {
            _data = data;
            _type = Type.Code128;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        public Barcode(string data, Type type)
        {
            _data = data;
            _type = type;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        public Barcode(string data, bool showLabel)
        {
            _data = data;
            _showLabel = showLabel;

            InitializeType();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, Font labelFont)
        {
            _data = data;
            _showLabel = showLabel;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        public Barcode(string data, Type type, bool showLabel)
        {
            _data = data;
            _type = type;
            _showLabel = showLabel;

            InitializeType();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, Type type, bool showLabel, Font labelFont)
        {
            _data = data;
            _type = type;
            _showLabel = showLabel;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        public Barcode(string data, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _width = width;
            _height = height;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        public Barcode(string data, bool showLabel, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;

            InitializeType();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;

            InitializeType();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;

            InitializeType();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="backgroundColor">Color of the background. Defaults to white.</param>
        /// <param name="foregroundColor">Color of the foreground. Defaults to black.</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Color backgroundColor, Color foregroundColor)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="backgroundColor">Color of the background. Defaults to white.</param>
        /// <param name="foregroundColor">Color of the foreground. Defaults to black.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Color backgroundColor, Color foregroundColor, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;
            _labelFont = labelFont;

            InitializeType();
        }

        private void InitializeType()
        {
            m_barcode = null;

            switch (_type)
            {
                case Type.Code128:
                    m_barcode = new Code128(_data);
                    break;
                case Type.Code11:
                    m_barcode = new Code11(_data);
                    break;
                case Type.Code39:
                    m_barcode = new Code39(_data);
                    break;
                case Type.Code93:
                    m_barcode = new Code93(_data);
                    break;
                case Type.EAN8:
                    m_barcode = new EAN8(_data);
                    break;
                case Type.EAN13:
                    m_barcode = new EAN13(_data);
                    break;
                default:
                    m_barcode = new Code128(_data);
                    break;
            }

            _encodedData = m_barcode?.GetEncoding() ?? new List<Bar>();
        }

        /// <summary>
        /// Saves the image to a file.
        /// </summary>
        /// <param name="path">The file path for the image.</param>
        /// <param name="imageFormat">The image format. Defaults to Jpeg.</param>
        public void SaveImageFile(string path, ImageFormat imageFormat = null)
        {
            var image = GenerateImage();

            image.Save(path, imageFormat ?? ImageFormat.Jpeg);
        }

        /// <summary>
        /// Gets the image in PNG format as a Base64 encoded string.
        /// </summary>
        public string GetBase64Image()
        {
            var image = GenerateImage();

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Gets the image in PNG format as a byte array.
        /// </summary>
        public byte[] GetByteArray()
        {
            var image = GenerateImage();

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Gets the image as a byte array.
        /// </summary>
        /// <param name="imageFormat">The image format. Defaults to PNG.</param>
        /// <returns></returns>
        public byte[] GetByteArray(ImageFormat imageFormat)
        {
            var image = GenerateImage();

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageFormat);
                return memoryStream.ToArray();
            }
        }

        private Image GenerateImage()
        {
			IRenderer renderer = m_barcode.Renderer ?? new DefaultRenderer();
			if (_autoSize)
			{
				renderer.SetAutoSize();
			}
			else
			{
				renderer.SetSize(_width, _height);
			}
			renderer.SetLabelSettings(_showLabel, _labelPosition, _labelFont);
			return renderer.Render(m_barcode);
        }
    }
}