using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Media
{
    // From user: CodeCaster
    // http://stackoverflow.com/questions/13893517/extract-a-portion-of-an-image-in-net
    public class ImageCropper
    {
        private Bitmap source_;
        private int cardsPerRow_;
        private int rowCount_;
        private int cardCount_;
        private int cardWidth_;
        private int cardHeight_;
        private int padding_;

        public ImageCropper(Bitmap source)
        {
            source_ = source;
        }

        public Bitmap Get(Rectangle r)
        {
            return source_.Clone(r, source_.PixelFormat);
        }

        public ImageCropper(Bitmap source, int rowCount, int cardsPerRow, int padding)
        {
            source_ = source;
            padding_ = padding;
            cardsPerRow_ = cardsPerRow;
            rowCount_ = rowCount;
            cardCount_ = cardsPerRow * rowCount_;
            cardWidth_ = source.Width / cardsPerRow_ - rowCount * padding;
            cardHeight_ = source.Height / rowCount_ - cardsPerRow * padding;
        }

        public Bitmap[,] CropImages()
        {
            var cards = new Bitmap[rowCount_, cardsPerRow_];
            for (int y = 0; y < rowCount_; y++)
            {
                for (int x = 0; x < cardsPerRow_; x++)
                    cards[y, x] = CropImage(x, y);
            }
            return cards;
        }

        private Bitmap CropImage(int x, int y)
        {
            var rect = new Rectangle(x * cardWidth_ + x * padding_, y * cardHeight_ + y * padding_, cardWidth_, cardHeight_);
            return source_.Clone(rect, source_.PixelFormat);
        }
    }
}
