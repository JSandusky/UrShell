using EditorCore.DragDrop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.DragDrop
{
    public class TextDragMatcher : DragMatcher
    {
        public TextDragMatcher() : base(typeof(System.Object), typeof(string))
        {

        }

        public override bool CanDoDrop(object srcInst, object destInst)
        {
            System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(destInst);
            if (converter != null)
            {
                return converter.CanConvertFrom(srcInst.GetType());
            }
            return false;
        }

        public override void DoDrop(object srcInst, object destInst, object srcHolder, object destHolder)
        {
            System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(destInst);
            if (converter != null)
            {
                object newObject = converter.ConvertFrom(srcInst);
            }
        }
    }
}
