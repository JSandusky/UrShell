using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrhoBackend;

namespace EditorCore.RenderView
{
    public class OrbitZoomRender : UrhoBackend.UrControl
    {
        IDocument belongsTo_;

        public OrbitZoomRender(IDocument belongingTo, string script) : base(script)
        {
            //belongsTo_ = belongingTo;
            //DocumentManager.GetInst().DocumentChanged += OrbitZoomRender_DocumentChanged;
        }

        void OrbitZoomRender_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            //SetForceFocus(args.New == belongsTo_);
        }

        UrhoBackend.Vector2 lastPosition = new UrhoBackend.Vector2();
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.Focus();
            Capture = true;
            lastPosition.x = e.X;
            lastPosition.y = e.Y;
        
            VariantMap eventData = new VariantMap();
            SendEvent("CaptureStarted", eventData);
        
            eventData.Set("Button", new Variant((int)e.Button));
            SendEvent("MouseButtonDown", eventData);
        
            base.OnMouseDown(e);
        }
        
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            Capture = false;
            this.Unfocus();
        
            VariantMap eventData = new VariantMap();
            SendEvent("CaptureEnded", eventData);
            eventData.Set("Button", new Variant((int)e.Button));
            SendEvent("MouseButtonUp", eventData);
        
            base.OnMouseUp(e);
        }
        
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (Capture)
            {
                UrhoBackend.Vector2 delta = new UrhoBackend.Vector2(e.X, e.Y);
                delta.op_SubtractionAssignment(lastPosition);
        
                UrhoBackend.VariantMap eventData = new UrhoBackend.VariantMap();
                eventData.Set("X", new UrhoBackend.Variant(e.X));
                eventData.Set("Y", new UrhoBackend.Variant(e.Y));
                eventData.Set("DX", new UrhoBackend.Variant((int)delta.x));
                eventData.Set("DY", new UrhoBackend.Variant((int)delta.y));
                
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    eventData.Set("Buttons", new UrhoBackend.Variant((int)0));
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    eventData.Set("Buttons", new UrhoBackend.Variant((int)1));
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                    eventData.Set("Buttons", new UrhoBackend.Variant((int)2));
                
                SendEvent("MouseMove", eventData);
        
                lastPosition.x = e.X;
                lastPosition.y = e.Y;
            }
            base.OnMouseMove(e);
        }
        
        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            VariantMap eventData = new VariantMap();
            
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                eventData.Set("Buttons", new UrhoBackend.Variant((int)0));
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                eventData.Set("Buttons", new UrhoBackend.Variant((int)1));
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                eventData.Set("Buttons", new UrhoBackend.Variant((int)2));
        
            eventData.Set("Wheel", new Variant(e.Delta));
            SendEvent("MouseWheel", eventData);
            base.OnMouseWheel(e);
        }
        
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (Capture)
            {
                VariantMap eventData = new VariantMap();
                eventData.Set("Raw", new Variant((uint)e.KeyCode));
                eventData.Set("ScanCode", new Variant(e.KeyValue));
                eventData.Set("Key", new Variant(e.KeyValue));
                eventData.Set("Repeat", new Variant(false));
                SendEvent("KeyDown", eventData);
            } else
                base.OnKeyDown(e);
        }
        
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            if (Capture)
            {
                VariantMap eventData = new VariantMap();
                eventData.Set("Raw", new Variant((uint)e.KeyCode));
                eventData.Set("ScanCode", new Variant(e.KeyValue));
                eventData.Set("Key", new Variant(e.KeyValue));
                SendEvent("KeyUp", eventData);
            } else
                base.OnKeyUp(e);
        }
    }
}
