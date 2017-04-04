//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Sce.Atf;
using Sce.Atf.Controls;
using System.Runtime.InteropServices;

namespace EditorCore.Controls.PropertyGrid
{
    /// <summary>
    /// Control for editing a numeric tuple (vector) of arbitrary dimension</summary>
    public class VectorControl : Control
    {
        /// <summary>
        /// Constructor specifying 3 single-precision components named "x", "y", and "z"</summary>
        public VectorControl()
            : this(typeof(UrhoBackend.Vector3), new[] { "x", "y", "z" })
        {
        }

        /// <summary>
        /// Constructor for a given numeric type, component names, and dimension. By default, </summary>
        /// <param name="numericType">Numeric type of tuple coordinates</param>
        /// <param name="names">Array of tuple coordinate names; vector length (dimension) is array length</param>
        /// <remarks>All numeric types, except Decimal, are supported</remarks>
        public VectorControl(Type numericType, string[] names)
        {
            m_labelColors = new Color[]
            {
                Color.FromArgb(200,40,0),
                Color.FromArgb(100,160,0),
                Color.FromArgb(40,120,240),
                Color.FromArgb(20,20,20),
            };

            Define(numericType, names);
        }





        /// <summary>
        /// Gets or sets whether axis labels should be hidden. The labels are visible by default.</summary>
        public bool HideAxisLabel
        {
            get;
            set;
        }

        /// <summary>
        /// Sets background colors used for drawing labels</summary>
        /// <param name="color">The array of colors to be used for drawing the background text color
        /// for the component labels. By default, 'x' has a red background, 'y' green, and 'z' blue.
        /// The actual text of the label is always white. The minimum length of 'color' is 1. If there
        /// are more components than colors in the array, the chosen index will cycle back to the
        /// beginning of the array.</param>
        public void SetLabelBackColors(Color[] color)
        {
            if (color == null || color.Length == 0)
                throw new ArgumentException("color");
            m_labelColors = color;

        }

        /// <summary>
        /// Sets the tuple coordinate names and the dimension of the tuple</summary>
        /// <param name="numericType">Numeric type of tuple coordinates</param>
        /// <param name="names">Array of tuple coordinate names; vector length (dimension) is array length</param>
        /// <remarks>All numeric types, except Decimal, are supported</remarks>
        public void Define(Type numericType, string[] names)
        {
            if (!typeof(UrhoBackend.MathVector).IsAssignableFrom(numericType))
            {
                throw new ArgumentException("Unsupported numeric type");
            }

            if (names == null || names.Length == 0)
                throw new ArgumentException("Must have at least 1 coordinate in the tuple");

            DoubleBuffered = true;
            m_numericType = numericType;

            while (Controls.Count > 0)
                Controls[0].Dispose();

            if (s_lblFormat == null)
            {
                s_lblFormat = new StringFormat();
                s_lblFormat.Alignment = StringAlignment.Center;
                s_lblFormat.LineAlignment = StringAlignment.Near;
                s_lblFormat.Trimming = StringTrimming.Character;

            }
            m_labelWidth = new int[names.Length];
            m_labelWidth[0] = -1; // to indicate uninitialized state.
            SuspendLayout();


            // custom tab handling.
            TabStop = false;
            for (int i = 0; i < names.Length; i++)
            {
                var textBox = new NumericTextBox(typeof(float));
                textBox.BorderStyle = BorderStyle.None;
                textBox.TabStop = false;
                textBox.Name = names[i];
                textBox.ScaleFactor = m_scaleFactor;
                textBox.ValueEdited += textBox_ValueEdited;
                Controls.Add(textBox);

            }

            ResumeLayout();
        }

        /// <summary>
        /// Gets or sets the scale factor, which is used to scale the value for presentation to the
        /// user. The inverse factor is used to scale the user entered value back.</summary>
        public double ScaleFactor
        {
            get { return m_scaleFactor; }
            set
            {
                if (value == 0)
                    throw new ArgumentException("value must be non-zero");

                m_scaleFactor = value;
                foreach (NumericTextBox textBox in Controls)
                    textBox.ScaleFactor = m_scaleFactor;
            }
        }

        /// <summary>
        /// Gets or sets tuple value as array</summary>
        public UrhoBackend.MathVector Value
        {
            get
            {
                if (m_numericType == typeof(UrhoBackend.Vector2))
                    return GetValue<UrhoBackend.Vector2>();
                if (m_numericType == typeof(UrhoBackend.Vector3))
                    return GetValue<UrhoBackend.Vector3>();
                if (m_numericType == typeof(UrhoBackend.Vector4))
                    return GetValue<UrhoBackend.Vector4>();
                if (m_numericType == typeof(UrhoBackend.Color))
                    return GetValue<UrhoBackend.Color>();
                if (m_numericType == typeof(UrhoBackend.Rect))
                    return GetValue<UrhoBackend.Rect>();
                if (m_numericType == typeof(UrhoBackend.IntRect))
                    return GetValue<UrhoBackend.IntRect>();
                if (m_numericType == typeof(UrhoBackend.IntVector2))
                    return GetValue<UrhoBackend.IntVector2>();
                // can't get here
                return null;
            }
            set
            {
                UrhoBackend.MathVector vector = value as UrhoBackend.MathVector;
                if (value == null)
                    throw new ArgumentException("value must be Vector2, Vector3 or Vector4");


                m_lastChange = vector.Clone();
                if (!m_editing)
                    m_lastEdit = m_lastChange;

                for (int i = 0; i < vector.Length; i++)
                {
                    NumericTextBox textBox = Controls[i] as NumericTextBox;
                    object obj = vector.Get(i);
                    textBox.Value = obj;
                }
            }
        }

        /// <summary>
        /// Gets the last changed value</summary>
        public UrhoBackend.MathVector LastChange
        {
            get { return m_lastChange; }
        }

        /// <summary>
        /// Gets the last edited value</summary>
        public UrhoBackend.MathVector LastEdit
        {
            get { return m_lastEdit; }
        }

        /// <summary>
        /// Event that is raised after value is changed</summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Event that is raised after the value is edited</summary>
        public event EventHandler ValueEdited;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize"></see> event</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            UpdateHeight();
            int cellSize = (Width / Controls.Count);

            SuspendLayout();

            if (HideAxisLabel)
            {
                int x = 0;
                foreach (NumericTextBox control in Controls)
                {
                    control.BorderStyle = BorderStyle.FixedSingle;
                    control.Bounds = new Rectangle(x, 0, cellSize, control.Height);
                    x += cellSize;
                }
            }
            else
            {
                if (m_labelWidth[0] == -1)
                    ComputeLabelWidth();
                const int margin = 3;
                int x = 0, c = 0;
                foreach (Control control in Controls)
                {
                    int labelWidth = m_labelWidth[c++];
                    control.Bounds = new Rectangle(x + labelWidth + margin, 0, cellSize - labelWidth - margin, control.Height);
                    x += cellSize;
                }
            }
            ResumeLayout(true);
            base.OnResize(e);
        }

        /// <summary>
        /// Raises Paint event and draws control</summary>
        /// <param name="e">Paint event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (HideAxisLabel)
                return;

            int cellSize = (Width / Controls.Count);
            int x = 0;

            int height = Controls[0].Height + 1;
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                int c = 0;
                foreach (Control control in Controls)
                {
                    brush.Color = m_labelColors[c % m_labelColors.Length];
                    Rectangle rect = new Rectangle(x, 1, m_labelWidth[c], height);
                    e.Graphics.FillRectangle(brush, rect);

                    e.Graphics.DrawString(control.Name, Font, Brushes.White,
                        rect, s_lblFormat);
                    x += cellSize;
                    c++;
                }
            }
        }
        /// <summary>
        /// Raises the <see cref="E:Sce.Atf.Controls.NumericTupleControl.ValueChanged"/> event</summary>
        /// <param name="e">Event args</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged.Raise(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Sce.Atf.Controls.NumericTupleControl.ValueEdited"/> event</summary>
        /// <param name="e">Event args</param>
        protected virtual void OnValueEdited(EventArgs e)
        {
            ValueEdited.Raise(this, e);
        }

        /// <summary>
        /// Raises FontChanged event and updates control</summary>
        /// <param name="e">Event arguments</param>
        protected override void OnFontChanged(EventArgs e)
        {
            // copied from PropertyView's OnFontChanged().
            // Call the base first so that FontHeight is updated before we call UpdateRowHeight().
            base.OnFontChanged(e);

            UpdateHeight();
            ComputeLabelWidth();
            PerformLayout();
            Invalidate();
        }

        /// <summary>
        /// Processes a dialog key</summary>
        /// <param name="keyData">One of the System.Windows.Forms.Keys values that represents the key to process</param>
        /// <returns>True iff the key was processed by the control</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            NumericTextBox focusTextBox = null;
            foreach (NumericTextBox ctrl in Controls)
            {
                if (ctrl.Focused)
                {
                    focusTextBox = ctrl;
                    break;
                }
            }

            int index = focusTextBox == null ? -1 : Controls.IndexOf(focusTextBox);
            if (keyData == Keys.Tab || keyData == Keys.Enter)
            {
                // if on last NumericTextBox then don't process tab
                NumericTextBox last = (NumericTextBox)Controls[Controls.Count - 1];
                if (focusTextBox != last)
                {
                    Controls[index + 1].Focus();
                    return true;
                }
            }
            else if (keyData == (Keys.Tab | Keys.Shift))
            {
                NumericTextBox first = (NumericTextBox)Controls[0];
                if (focusTextBox != first && index != -1)
                {
                    Controls[index - 1].Focus();
                    return true;

                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void UpdateHeight()
        {
            if (Controls.Count > 0)
                Height = Controls[0].Height;
        }

        /// <summary>
        /// Gets the component that changed</summary>
        /// <remarks>Only valid during OnValueChanged or OnValueEdited methods</remarks>
        public int Component
        {
            get { return m_component; }
        }

        private void textBox_ValueEdited(object sender, EventArgs e)
        {
            NumericTextBox textbox = (NumericTextBox)sender;
            m_component = Controls.IndexOf(textbox);
            UrhoBackend.MathVector value = Value as UrhoBackend.MathVector;
            m_editing = true;

            OnValueChanged(EventArgs.Empty);

            m_lastChange = value;
            if (!ContainsFocus && m_editing)
            {
                OnValueEdited(EventArgs.Empty);
                m_lastEdit = value;
                m_editing = false;
            }
        }

        private T GetValue<T>() where T : UrhoBackend.MathVector, new()
        {
            T result = new T();
            for (int i = 0; i < result.Length; i++)
                result.Set(i, (float)((NumericTextBox)Controls[i]).Value);
            return result;
        }

        private void ComputeLabelWidth()
        {
            int c = 0;
            foreach (NumericTextBox textBox in Controls)
            {
                m_labelWidth[c++] = TextRenderer.MeasureText(textBox.Name, Font).Width;
            }

        }

        public Type m_numericType;
        private double m_scaleFactor = 1.0;
        private UrhoBackend.MathVector m_lastChange;
        private UrhoBackend.MathVector m_lastEdit;
        private int m_component;
        private int[] m_labelWidth;
        private Color[] m_labelColors;
        private bool m_editing;
        private static StringFormat s_lblFormat;
    }
}
