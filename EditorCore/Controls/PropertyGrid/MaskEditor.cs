//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Drawing;
using System.Windows.Forms;
using Sce.Atf.Applications;
using Sce.Atf.Controls.PropertyEditing;
using System.Collections.Generic;

namespace EditorCore.Controls.PropertyGrid
{
    /// <summary>
    /// Displays multiple checkboxes for editing a bitfield.</summary>
    public class MaskEditor : IPropertyEditor
    {
        #region IPropertyEditor Implementation

        /// <summary>
        /// Obtains a control to edit a given property. Changes to the selection set
        /// cause this method to be called again (and passed a new 'context'),
        /// unless ICacheablePropertyControl is implemented on the control. For
        /// performance reasons, it is highly recommended that the control implement
        /// the ICacheablePropertyControl interface.</summary>
        /// <param name="context">Context for property editing control</param>
        /// <returns>Control to edit the given context</returns>
        public Control GetEditingControl(PropertyEditorControlContext context)
        {
            m_boolControl = new MaskControl(context);
            SkinService.ApplyActiveSkin(m_boolControl);
            return m_boolControl;
        }

        #endregion

        private class MaskControl : Control, ICacheablePropertyControl
        {
            public MaskControl(PropertyEditorControlContext context)
            {
                m_context = context;

                m_flip = new Button();
                m_flip.Text = "Flip";
                m_checkBoxes = new List<CheckBox>();
                int bit = 1;
                for (int i = 0; i < 8; ++i)
                {
                    CheckBox cb = new CheckBox();
                    cb.Size = cb.PreferredSize;
                    cb.CheckAlign = ContentAlignment.MiddleLeft;
                    cb.CheckedChanged += checkBox_CheckedChanged;
                    m_checkBoxes.Add(cb);
                    Controls.Add(cb);
                    cb.Tag = bit;
                    bit = bit << 1;
                    Height = cb.Height + m_topAndLeftMargin;
                }
                m_flip.Click += (sender, args) => {
                    foreach (CheckBox cb in m_checkBoxes)
                        cb.Checked = !cb.Checked;
                };
                Controls.Add(m_flip);
                RefreshValue();
            }

            public override void Refresh()
            {
                RefreshValue();

                base.Refresh();
            }

            #region ICacheablePropertyControl
            /// <summary>
            /// Gets true iff this control can be used indefinitely, regardless of whether the associated
            /// PropertyEditorControlContext's SelectedObjects property changes, i.e., the selection changes. 
            /// This property must be constant for the life of this control.</summary>
            public bool Cacheable
            {
                get { return true; }
            }
            #endregion

            protected override void OnSizeChanged(EventArgs e)
            {
                int left = 0;
                for (int i = 0; i < m_checkBoxes.Count; ++i)
                {
                    m_checkBoxes[i].Location = new Point(m_topAndLeftMargin + left, (Height - m_checkBoxes[i].Height) / 2 + 1);
                    left += m_checkBoxes[i].Width;
                }
                m_flip.Location = new Point(m_topAndLeftMargin + left, (Height - m_flip.Height) / 2 + 1);
                base.OnSizeChanged(e);
            }

            private void checkBox_CheckedChanged(object sender, EventArgs e)
            {
                if (!m_refreshing)
                {
                    int value = 0;
                    foreach (CheckBox cb in m_checkBoxes)
                    {
                        if (cb.Checked)
                            value = value | (int)cb.Tag;
                    }
                    m_context.SetValue(value);
                }
            }

            private void RefreshValue()
            {
                try
                {
                    m_refreshing = true;
                    object value = m_context.GetValue();
                    foreach (CheckBox cb in m_checkBoxes)
                    {
                        if (value == null)
                            cb.Enabled = false;
                        else
                        {
                            int iValue = (int)value;
                            cb.Checked = (iValue & (int)(cb.Tag)) != 0;
                            cb.Enabled = (!m_context.IsReadOnly) && !DisableEditing;
                        }
                    }
                }
                finally
                {
                    m_refreshing = false;
                }
            }

            /// <summary>
            /// Gets or sets whether editing is disabled.
            /// DisableEditing can be used to lock out editing on this control, whether or not the context it was created with is read only.</summary>
            public bool DisableEditing { get; set; }

            private readonly PropertyEditorControlContext m_context;
            private readonly List<CheckBox> m_checkBoxes;
            private readonly Button m_flip;
            private bool m_refreshing;
            private const int m_topAndLeftMargin = 2;
        }

        /// <summary>
        /// Gets or sets whether editing is disabled. 
        /// DisableEditing can be used to lock out editing on this editor, whether or not the context it was created with is read only.</summary>
        public bool DisableEditing
        {
            get { return (m_boolControl != null) ? m_boolControl.DisableEditing : false; }
            set { if (m_boolControl != null) m_boolControl.DisableEditing = value; }
        }

        private MaskControl m_boolControl;
    }
}
