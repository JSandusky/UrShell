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
    /// Edits a list of ResourceRefs.</summary>
    public class ResourceRefListEditor : IPropertyEditor
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
            m_boolControl = new ResourceRefListControl(context);
            SkinService.ApplyActiveSkin(m_boolControl);
            return m_boolControl;
        }

        #endregion

        private class ResourceRefListControl : Control
        {
            public ResourceRefListControl(PropertyEditorControlContext context)
            {
                m_context = context;
                UrhoBackend.ResourceRefList Ref = context.GetValue() as UrhoBackend.ResourceRefList;

                m_textBoxes = new List<TextBox>();
                m_browseButtons = new List<Button>();
                if (Ref != null)
                {
                    for (int i = 0; i < Ref.Size(); ++i)
                    {
                        TextBox tb = new TextBox();
                        tb.Width = 160;
                        tb.TextChanged += checkBox_CheckedChanged;
                        tb.Tag = i;
                        m_textBoxes.Add(tb);

                        Button btn = new Button();
                        btn.Text = "...";
                        btn.Width = 32;
                        btn.Height = 32;
                        btn.Tag = tb;
                        btn.Click += btn_Click;
                        m_browseButtons.Add(btn);

                        Controls.Add(tb);
                        Controls.Add(btn);
                        Height += tb.Height;
                    }
                }

                RefreshValue();
            }

            void btn_Click(object sender, EventArgs e)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ((TextBox)((Button)sender).Tag).Text = dlg.FileName;
                }
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
                int top = 0;
                for (int i = 0; i < m_textBoxes.Count; ++i)
                {
                    m_textBoxes[i].Location = new Point(m_topAndLeftMargin, top);
                    m_browseButtons[i].Location = new Point(m_topAndLeftMargin + m_textBoxes[i].Width, top);
                    top += m_textBoxes[i].Height;
                }
                this.Height = top;
                base.OnSizeChanged(e);
            }

            private void checkBox_CheckedChanged(object sender, EventArgs e)
            {
                if (!m_refreshing)
                {
                    List<string> values = new List<string>();
                    foreach (TextBox cb in m_textBoxes)
                    {
                        values.Add(cb.Text);
                    }
                    UrhoBackend.ResourceRefList Ref = m_context.GetValue() as UrhoBackend.ResourceRefList;
                    Ref.FromList(values);
                    m_context.SetValue(Ref);
                }
            }

            private void RefreshValue()
            {
                try
                {
                    m_refreshing = true;
                    object value = m_context.GetValue();
                    UrhoBackend.ResourceRefList Ref = m_context.GetValue() as UrhoBackend.ResourceRefList;
                    if (Ref == null)
                        return;
                    for (int i = 0; i < Ref.Size(); ++i)
                    {
                        if (i > m_textBoxes.Count - 1)
                            m_textBoxes.Add(new TextBox());
                        TextBox cb = m_textBoxes[i];
                        if (value == null)
                            cb.Enabled = false;
                        else
                        {
                            cb.Text = Ref.Get(i).GetName();
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
            private readonly List<TextBox> m_textBoxes;
            private readonly List<Button> m_browseButtons;
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

        private ResourceRefListControl m_boolControl;
    }
}
