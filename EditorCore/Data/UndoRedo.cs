using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Data
{
    public interface UndoRedo
    {
        /// <summary>
        /// Take us to the relevant UI, returns true if we didn't change anything
        /// </summary>
        bool GoTo();
        /// <summary>
        /// Undo the action
        /// </summary>
        void Undo();
        /// <summary>
        /// Redo the action
        /// </summary>
        void Redo();
    }

    public class UndoRedoStack
    {
        Stack<UndoRedo> undoStack_;
        Stack<UndoRedo> redoStack_;

        public UndoRedoStack()
        {
            undoStack_ = new Stack<UndoRedo>();
            redoStack_ = new Stack<UndoRedo>();
        }

        public void Add(UndoRedo undoable)
        {
            undoStack_.Push(undoable);
            redoStack_.Clear();
        }

        public void Undo()
        {
            if (undoStack_.Count > 0)
            {
                if (undoStack_.Peek().GoTo())
                {
                    undoStack_.Peek().Undo();
                    redoStack_.Push(undoStack_.Pop());
                }
            }
        }

        public void Redo()
        {
            if (redoStack_.Count > 0)
            {
                if (redoStack_.Peek().GoTo())
                {
                    redoStack_.Peek().GoTo();
                    undoStack_.Push(redoStack_.Pop());
                }
            }
        }
    }
}
