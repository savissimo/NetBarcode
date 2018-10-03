using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace NetBarcode.Graphics
{
	class DefaultRenderer : IRenderer
	{
		private bool m_autosize = true;
		private int m_width = 0;
		private int m_height = 0;
		private bool m_fillSpace = false;
		private bool m_showLabel = true;
		private LabelPosition m_labelPosition = LabelPosition.Standard;
		private Font m_labelFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
		private AlignmentPosition m_alignmentPosition = AlignmentPosition.Center;
		private Color m_foregroundColor = Color.Black;
		private Color m_backgroundColor = Color.White;

		public DefaultRenderer()
		{
		}

		public void SetAutoSize()
		{
			m_autosize = true;
		}

		public void SetSize(int i_width, int i_height, bool i_fillSpace = false)
		{
			m_autosize = false;
			m_width = i_width;
			m_height = i_height;
			m_fillSpace = i_fillSpace;
		}

		public void SetLabelSettings(bool i_showLabel, LabelPosition i_labelPosition = LabelPosition.Standard, Font i_labelFont = null)
		{
			m_showLabel = i_showLabel;
			m_labelPosition = i_labelPosition;
			if (i_labelFont != null)
			{
				m_labelFont = i_labelFont;
			}
		}

		public void SetImageSettings(AlignmentPosition i_alignmentPosition = AlignmentPosition.Center, Color? i_foregroundColor = null, Color? i_backgroundColor = null)
		{
			m_alignmentPosition = i_alignmentPosition;
			if (i_foregroundColor != null)
			{
				m_foregroundColor = i_foregroundColor.Value;
			}
			if (i_backgroundColor != null)
			{
				m_backgroundColor = i_backgroundColor.Value;
			}
		}

		public Image Render(IBarcode i_barcode)
		{
			const int barWidth = 2;
			const int aspectRatio = 2;

			int width = m_width;
			int height = m_height;

			List<Bar> encodedData = i_barcode.GetEncoding();

			if (m_autosize)
			{
				width = barWidth * encodedData.Count;

				height = width / aspectRatio;
			}

			var topLabelAdjustment = 0;

			if (m_showLabel)
			{
				// Shift drawing down if top label.
				if ((m_labelPosition & (LabelPosition.TopCenter | LabelPosition.TopLeft | LabelPosition.TopRight)) > 0)
					topLabelAdjustment = m_labelFont.Height;

				height -= m_labelFont.Height;
			}

			var bitmap = new Bitmap(width, height);
			var iBarWidth = width / encodedData.Count;
			var shiftAdjustment = 0;
			var iBarWidthModifier = 1;

			switch (m_alignmentPosition)
			{
				case AlignmentPosition.Center:
					shiftAdjustment = (width % encodedData.Count) / 2;
					break;
				case AlignmentPosition.Left:
					shiftAdjustment = 0;
					break;
				case AlignmentPosition.Right:
					shiftAdjustment = (width % encodedData.Count);
					break;
				default:
					shiftAdjustment = (width % encodedData.Count) / 2;
					break;
			}

			if (iBarWidth <= 0)
				throw new Exception(
					"EGENERATE_IMAGE-2: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)"
					);

			//draw image
			var pos = 0;
			var halfBarWidth = (int)(iBarWidth * 0.5);

			using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
			{
				//clears the image and colors the entire background
				graphics.Clear(m_backgroundColor);

				//lines are fBarWidth wide so draw the appropriate color line vertically
				using (var backpen = new Pen(m_backgroundColor, iBarWidth / iBarWidthModifier))
				{
					using (var pen = new Pen(m_foregroundColor, iBarWidth / iBarWidthModifier))
					{
						while (pos < encodedData.Count)
						{
							Bar bar = encodedData[pos];
							if (bar.Value == 1)
							{
								graphics.DrawLine(pen,
									new Point(pos * iBarWidth + shiftAdjustment + halfBarWidth, topLabelAdjustment),
									new Point(pos * iBarWidth + shiftAdjustment + halfBarWidth,
										height + topLabelAdjustment));
							}
							pos++;
						}
					}
				}
			}

			var image = (Image)bitmap;

			if (m_showLabel)
			{
				image = InsertLabel(image, i_barcode.Data);
			}
			return image;
		}

		private Image InsertLabel(Image i_image, string i_data)
		{
			try
			{
				using (var g = System.Drawing.Graphics.FromImage(i_image))
				{
					g.DrawImage(i_image, 0, 0);

					g.SmoothingMode = SmoothingMode.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.CompositingQuality = CompositingQuality.HighQuality;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

					var stringFormat = new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Near
					};

					var labelY = 0;

					switch (m_labelPosition)
					{
						case LabelPosition.BottomCenter:
							labelY = i_image.Height - (m_labelFont.Height);
							stringFormat.Alignment = StringAlignment.Center;
							break;
						case LabelPosition.BottomLeft:
							labelY = i_image.Height - (m_labelFont.Height);
							stringFormat.Alignment = StringAlignment.Near;
							break;
						case LabelPosition.BottomRight:
							labelY = i_image.Height - (m_labelFont.Height);
							stringFormat.Alignment = StringAlignment.Far;
							break;
						case LabelPosition.TopCenter:
							labelY = 0;
							stringFormat.Alignment = StringAlignment.Center;
							break;
						case LabelPosition.TopLeft:
							labelY = 0;
							stringFormat.Alignment = StringAlignment.Near;
							break;
						case LabelPosition.TopRight:
							labelY = 0;
							stringFormat.Alignment = StringAlignment.Far;
							break;
					}

					//color a background color box at the bottom of the barcode to hold the string of data
					g.FillRectangle(new SolidBrush(m_backgroundColor),
                        new RectangleF(0, labelY, i_image.Width, m_labelFont.Height));

					//draw datastring under the barcode image
					g.DrawString(i_data, m_labelFont, new SolidBrush(m_foregroundColor),
						new RectangleF(0, labelY, i_image.Width, m_labelFont.Height), stringFormat);

					g.Save();
				}
				return i_image;
			}
			catch (Exception ex)
			{
				throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
			}
		}
	}
}
