using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sce.Atf.Controls;
using Sce.Atf.Controls.Timelines.Direct2D;
using WeifenLuo.WinFormsUI.Docking;
using EditorCore;
using Sce.Atf;

namespace EditorWinForms.Documents.ObjectAnim
{
    public partial class ObjectAnimView : DockContent
    {
        UrhoBackend.UrControl Urho3D;
        TreeControl paletteTree;
        Sce.Atf.Controls.Timelines.Direct2D.D2dTimelineControl timeline;
        D2dSplitManipulator splitManipulator;

        public ObjectAnimView(string path, Timelines.ObjectAnimTimelineDocument doc)
        {
            InitializeComponent();
            if (path != null)
                Text = System.IO.Path.GetFileName(path);
            else
                Text = "New Object Animation".Localize();
            paletteTree = new TreeControl();
            paletteTree.Dock = DockStyle.Fill;
            subSplitter.Panel1.Controls.Add(paletteTree);

            timeline = new Sce.Atf.Controls.Timelines.Direct2D.D2dTimelineControl(doc);
            timeline.Dock = DockStyle.Fill;
            subSplitter.Panel2.Controls.Add(timeline);
            new D2dSelectionManipulator(timeline);
            new D2dMoveManipulator(timeline);
            new D2dScaleManipulator(timeline);
            splitManipulator = new D2dSplitManipulator(timeline);
            D2dSnapManipulator snapManipulator = new D2dSnapManipulator(timeline);
            D2dScrubberManipulator scrubberManipulator = new D2dScrubberManipulator(timeline);

            //// Allow the snap manipulator to snap objects to the scrubber.
            snapManipulator.Scrubber = scrubberManipulator;

            Urho3D = new UrhoBackend.UrControl("Data/Scripts/ObjectAnimEditor.as");
            Urho3D.Dock = DockStyle.Fill;
            Urho3D.SubscribeCallback(this);
            topSplitter.Panel1.Controls.Add(Urho3D);
        }
    }
}
