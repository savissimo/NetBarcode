using System.Drawing;

namespace NetBarcode.Graphics
{
	interface IRenderer
	{
		Image Render(IBarcode i_barcode);

		void SetAutoSize();
		void SetSize(int i_width, int i_height, bool i_fillSpace = false);
		void SetLabelSettings(bool i_showLabel, LabelPosition i_labelPosition = LabelPosition.Standard, Font i_labelFont = null);
		void SetImageSettings(AlignmentPosition i_alignmentPosition = AlignmentPosition.Center, Color? i_foregroundColor = null, Color? i_backgroundColor = null);
	}
}
