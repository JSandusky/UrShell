using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.Timelines
{
    class EffectKey : Sce.Atf.Controls.Timelines.IKey
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

    class EffectInterval : Sce.Atf.Controls.Timelines.IInterval
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

    class EffectTrack : Sce.Atf.Controls.Timelines.ITrack
    {

        public Sce.Atf.Controls.Timelines.IInterval CreateInterval()
        {
            throw new NotImplementedException();
        }

        public Sce.Atf.Controls.Timelines.IKey CreateKey()
        {
            throw new NotImplementedException();
        }

        public Sce.Atf.Controls.Timelines.IGroup Group
        {
            get { throw new NotImplementedException(); }
        }

        public IList<Sce.Atf.Controls.Timelines.IInterval> Intervals
        {
            get { throw new NotImplementedException(); }
        }

        public IList<Sce.Atf.Controls.Timelines.IKey> Keys
        {
            get { throw new NotImplementedException(); }
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
    }

    class EffectGroup : Sce.Atf.Controls.Timelines.IGroup
    {

        public Sce.Atf.Controls.Timelines.ITrack CreateTrack()
        {
            throw new NotImplementedException();
        }

        public bool Expanded
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

        public Sce.Atf.Controls.Timelines.ITimeline Timeline
        {
            get { throw new NotImplementedException(); }
        }

        public IList<Sce.Atf.Controls.Timelines.ITrack> Tracks
        {
            get { throw new NotImplementedException(); }
        }
    }

    class EffectTimeline : Sce.Atf.Controls.Timelines.ITimeline
    {
        public Sce.Atf.Controls.Timelines.IGroup CreateGroup()
        {
            throw new NotImplementedException();
        }

        public Sce.Atf.Controls.Timelines.IMarker CreateMarker()
        {
            throw new NotImplementedException();
        }

        public IList<Sce.Atf.Controls.Timelines.IGroup> Groups
        {
            get { throw new NotImplementedException(); }
        }

        public IList<Sce.Atf.Controls.Timelines.IMarker> Markers
        {
            get { throw new NotImplementedException(); }
        }
    }
}
