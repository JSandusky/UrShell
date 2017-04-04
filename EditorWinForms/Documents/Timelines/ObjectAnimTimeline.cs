using EditorWinForms.Controls;
using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EditorWinForms.Documents.Timelines
{
    public class ObjectAnimKey : Sce.Atf.Controls.Timelines.IKey
    {
        public ObjectAnimKey(ObjectAnimTrack track)
        {
            Track = track;
            Color = System.Drawing.Color.White;
        }

        public Sce.Atf.Controls.Timelines.ITrack Track { get; set; }
        public System.Drawing.Color Color { get; set; }
        public float Length { get; set; }
        public string Name { get; set; }

        float start_;
        public float Start
        {
            get
            {
                return start_;
            }
            set
            {
                start_ = value;
            }
        }
    }

    public class ObjectAnimInterval : Sce.Atf.Controls.Timelines.IInterval
    {
        public ObjectAnimInterval(ObjectAnimTrack track)
        {
            Track = track;
            Color = System.Drawing.Color.Yellow;
        }

        public Sce.Atf.Controls.Timelines.ITrack Track { get; set; }
        public System.Drawing.Color Color { get; set; }
        public float Length { get; set; }
        public string Name { get; set; }
        public float Start { get; set; }
    }

    public class ObjectAnimTrack : Sce.Atf.Controls.Timelines.ITrack
    {
        public ObjectAnimTrack(ObjectAnimGroup group)
        {
            Group = group;
            Keys = new List<Sce.Atf.Controls.Timelines.IKey>();
            Intervals = new List<Sce.Atf.Controls.Timelines.IInterval>();
        }

        public Sce.Atf.Controls.Timelines.IInterval CreateInterval()
        {
            return new ObjectAnimInterval(this);
        }

        public Sce.Atf.Controls.Timelines.IKey CreateKey()
        {
            return new ObjectAnimKey(this);
        }

        public Sce.Atf.Controls.Timelines.IGroup Group
        {
            get;
            set;
        }

        public IList<Sce.Atf.Controls.Timelines.IInterval> Intervals
        {
            get;
            private set;
        }

        public IList<Sce.Atf.Controls.Timelines.IKey> Keys
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public class ObjectAnimGroup : Sce.Atf.Controls.Timelines.IGroup
    {
        ObjectAnimTimeline timeline;
        public ObjectAnimGroup(ObjectAnimTimeline timeline)
        {
            this.timeline = timeline;
            Tracks = new List<Sce.Atf.Controls.Timelines.ITrack>();
        }

        public Sce.Atf.Controls.Timelines.ITrack CreateTrack()
        {
            return new ObjectAnimTrack(this);
        }

        public bool Expanded
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Sce.Atf.Controls.Timelines.ITimeline Timeline
        {
            get;
            set;
        }

        public IList<Sce.Atf.Controls.Timelines.ITrack> Tracks
        {
            get;
            private set;
        }
    }

    public class ObjectAnimMarker : Sce.Atf.Controls.Timelines.IMarker
    {
        public ObjectAnimMarker(ObjectAnimTimeline timeline)
        {
            Timeline = timeline;
            Color = System.Drawing.Color.Purple;
        }
        public Sce.Atf.Controls.Timelines.ITimeline Timeline { get; set; }

        public System.Drawing.Color Color { get; set; }

        public float Length { get; set; }

        public string Name { get; set; }

        public float Start { get; set; }
    }

    public class ObjectAnimTimeline : Sce.Atf.Controls.Timelines.ITimeline
    {
        public ObjectAnimTimeline(XmlDocument document)
        {
            Groups = new List<Sce.Atf.Controls.Timelines.IGroup>();
            Markers = new List<Sce.Atf.Controls.Timelines.IMarker>();
            Groups.Add(CreateGroup());
            Groups[0].Name = "Test";
            Groups[0].Tracks.Add(Groups[0].CreateTrack());
            Groups[0].Tracks[0].Name = "Test Track";
            Groups[0].Tracks[0].Keys.Add(Groups[0].Tracks[0].CreateKey());
            Groups[0].Tracks[0].Keys[0].Start = 3.0f;
            Groups[0].Tracks[0].Keys[0].Name = "Test Key";
        }

        public Sce.Atf.Controls.Timelines.IGroup CreateGroup()
        {
            return new ObjectAnimGroup(this);
        }

        public Sce.Atf.Controls.Timelines.IMarker CreateMarker()
        {
            return new ObjectAnimMarker(this);
        }

        public IList<Sce.Atf.Controls.Timelines.IGroup> Groups
        {
            get;
            set;
        }

        public IList<Sce.Atf.Controls.Timelines.IMarker> Markers
        {
            get;
            set;
        }
    }

    public class ObjectAnimTimelineDocument : EditorCore.Controls.AtfSelectionHandler, Sce.Atf.Controls.Timelines.ITimelineDocument, IObservableContext
    {
        public ObjectAnimTimelineDocument(XmlDocument doc)
        {
            Timeline = new ObjectAnimTimeline(doc);
        }
        public Sce.Atf.Controls.Timelines.ITimeline Timeline
        {
            get;
            set;
        }

        bool dirty_;
        public bool Dirty
        {
            get { return dirty_; }
            set
            {
                if (dirty_ != value)
                {
                    dirty_ = value;
                    if (DirtyChanged != null)
                        DirtyChanged(this, null);
                }
            }
        }

        public event EventHandler DirtyChanged;

        public bool IsReadOnly { get { return false; } }

        public string Type
        {
            get { throw new NotImplementedException(); }
        }

        public Uri Uri
        {
            get;
            set;
        }

        public event EventHandler<Sce.Atf.UriChangedEventArgs> UriChanged;

        public event EventHandler<Sce.Atf.ItemChangedEventArgs<object>> ItemChanged;

        public event EventHandler<Sce.Atf.ItemInsertedEventArgs<object>> ItemInserted;

        public event EventHandler<Sce.Atf.ItemRemovedEventArgs<object>> ItemRemoved;

        public event EventHandler Reloaded;

        public object GetAdapter(Type type)
        {
            return this;
        }
    }
}
