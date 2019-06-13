using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AIT.WebUIComponent.Utilities
{
    public class UnicodeFontFactory : FontFactoryImp
    {
        private readonly BaseFont _baseFont;

        public UnicodeFontFactory(string fontPath)
        {
            _baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        }

        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
        {
            return new Font(_baseFont, size, style, color);
        }
    }
}