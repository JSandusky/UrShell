using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrhoBackend;

namespace EditorWinForms.Documents.Timelines
{
    public class ModelTimelineKey : Sce.Atf.Controls.Timelines.IKey
    {
        public AnimKeyframe key {get;set;}
        public AnimTrack track { get; set; }

        public ModelTimelineKey(AnimTrack track) {
            this.track = track;
        }

        public ModelTimelineKey(AnimTrack track, AnimKeyframe key)
        {
            this.key = key;
            this.track = track;
        }

        public Sce.Atf.Controls.Timelines.ITrack Track { get; set; }

        public System.Drawing.Color Color { get; set; }

        public float Length
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Start
        {
            get
            {
                return key.Time;
            }
            set
            {
                key.Time = value;
            }
        }
    }

    public class ModelTimelineInterval : Sce.Atf.Controls.Timelines.IInterval
    {

        public Sce.Atf.Controls.Timelines.ITrack Track
        {
            get { throw new NotImplementedException(); }
        }

        public System.Drawing.Color Color
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Length
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Start
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public class ModelTimelineTrack : Sce.Atf.Controls.Timelines.ITrack
    {
        AnimTrack track;
        List<Sce.Atf.Controls.Timelines.IKey> keys;

        public ModelTimelineTrack() {
            keys = new List<Sce.Atf.Controls.Timelines.IKey>();
        }

        public ModelTimelineTrack(AnimTrack track)
        {
            keys = new List<Sce.Atf.Controls.Timelines.IKey>();
            this.track = track;
            foreach (AnimKeyframe key in track.Keys)
            {
                keys.Add(new ModelTimelineKey(track, key));
            }
        }

        public Sce.Atf.Controls.Timelines.IInterval CreateInterval()
        {
            return null;
        }

        public Sce.Atf.Controls.Timelines.IKey CreateKey()
        {
            return new ModelTimelineKey(track) { key = new AnimKeyframe { Time = 0.0f }, 
                Track = this };
        }

        public Sce.Atf.Controls.Timelines.IGroup Group { get; set; }

        public IList<Sce.Atf.Controls.Timelines.IInterval> Intervals
        {
            get { return null; }
        }

        public IList<Sce.Atf.Controls.Timelines.IKey> Keys
        {
            get { return keys; }
        }

        public string Name
        {
            get
            {
                return track.Name;
            }
            set
            {
                track.Name = value;
            }
        }
    }

    public class ModelTimelineGroup : Sce.Atf.Controls.Timelines.IGroup
    {
        Animation animation;
        String animationFileName;
        List<Sce.Atf.Controls.Timelines.ITrack> tracks;

        public ModelTimelineGroup(String fileName, Animation anim)
        {
            animation = anim;
            animationFileName = fileName;
            tracks = new List<Sce.Atf.Controls.Timelines.ITrack>();
            foreach (AnimTrack track in animation.Tracks)
                tracks.Add(new ModelTimelineTrack(track) { Group = this });
        }

        public Sce.Atf.Controls.Timelines.ITrack CreateTrack()
        {
            return new ModelTimelineTrack { Group = this, Name = "" };
        }

        public bool Expanded { get; set; }

        public string Name
        {
            get { return animationFileName; }
            set { animationFileName = value; }
        }

        public Sce.Atf.Controls.Timelines.ITimeline Timeline { get; set; }

        public IList<Sce.Atf.Controls.Timelines.ITrack> Tracks
        {
            get { return tracks; }
        }
    }

    public class ModelTimelineMarker : Sce.Atf.Controls.Timelines.IMarker
    {

        public Sce.Atf.Controls.Timelines.ITimeline Timeline
        {
            get { throw new NotImplementedException(); }
        }

        public System.Drawing.Color Color
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Length
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Start
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public class ModelTimeline : Sce.Atf.Controls.Timelines.ITimeline
    {
        Animation animation;
        String fileName;
        List<Sce.Atf.Controls.Timelines.IGroup> groups;

        public ModelTimeline(String fileName, Animation animation)
        {
            this.fileName = fileName;
            this.animation = animation;
            groups = new List<Sce.Atf.Controls.Timelines.IGroup>();
            groups.Add(new ModelTimelineGroup(fileName, animation));
        }

        public Sce.Atf.Controls.Timelines.IGroup CreateGroup()
        {
            return null;
        }

        public Sce.Atf.Controls.Timelines.IMarker CreateMarker()
        {
            return null;
        }

        public IList<Sce.Atf.Controls.Timelines.IGroup> Groups
        {
            get { return groups; }
        }

        public IList<Sce.Atf.Controls.Timelines.IMarker> Markers
        {
            get { return null; }
        }
    }
}
