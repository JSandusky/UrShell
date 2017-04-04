using EditorCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.DragDrop
{
    public abstract class DragMatcher
    {
        public Type SrcType { get; private set; }
        public Type DestType { get; private set; }

        public DragMatcher(Type src, Type dest)
        {
            SrcType = src;
            DestType = dest;
        }

        public virtual bool CanDrag(object srcObject)
        {
            return true;
        }

        public virtual bool CanDoDrop(object srcInst, object destInst) 
        {
            return true;
        }

        public abstract void DoDrop(object srcInst, object destInst, object srcHolder, object destHolder);
    }

    public abstract class TemplateDragMatch<S, D> : DragMatcher
    {
        public TemplateDragMatch() : base(typeof(S), typeof(D)) { }

        public override bool CanDoDrop(object srcInst, object destInst)
        {
            return CanDoDrop((S)srcInst, (D)destInst);
        }
        public override void DoDrop(object srcInst, object destInst, object srcHolder, object destHolder)
        {
            DoDrop((S)srcInst, (D)destInst, srcHolder, destHolder);
        }

        public abstract bool CanDoDrop(S srcInst, D destInst);
        public abstract void DoDrop(S srcInst, D destInst, object srcHolder, object destHolder);
    }

    [IProgramInitializer("Drag and Drop")]
    public class DragMatchCollection : List<DragMatcher>
    {
        static DragMatchCollection master_;
        public static DragMatchCollection GetMaster() { return master_; }

        public bool HandlesSource(Type t)
        {
            foreach (DragMatcher matcher in this)
            {
                if (matcher.SrcType == t)
                    return true;
            }
            return false;
        }

        public DragMatcher GetBestFor(Type src, Type dest)
        {
            List<DragMatcher> handlesBoth = new List<DragMatcher>();
            foreach (DragMatcher matcher in this)
            {
                if (matcher.SrcType == src && matcher.DestType == dest)
                {
                    return matcher;
                }
                else if (matcher.SrcType.IsAssignableFrom(src) && matcher.DestType.IsAssignableFrom(dest))
                    handlesBoth.Add(matcher);
            }
            if (handlesBoth.Count > 1)
            {
                handlesBoth.Sort((lhs, rhs) =>
                {
                    int leftDist = TypeUtils.GetDistance(src, lhs.SrcType) + TypeUtils.GetDistance(dest, lhs.DestType);
                    int rightDist = TypeUtils.GetDistance(src, rhs.SrcType) + TypeUtils.GetDistance(dest, rhs.DestType);
                    if (leftDist < rightDist)
                        return -1;
                    else if (rightDist < leftDist)
                        return 1;
                    return 0;
                });
                return handlesBoth[0];
            }
            else if (handlesBoth.Count == 1)
                return handlesBoth[0];
            return null;
        }

        

        static void ProgramInitialized()
        {
            master_ = new DragMatchCollection();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeof(DragMatcher).IsAssignableFrom(t) && !t.IsAbstract)
                    master_.Add(Activator.CreateInstance(t) as DragMatcher);
            }
        }
    }
}
