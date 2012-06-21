using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.interfaces;

namespace Rob.Umbraco.DataTypes.ProtectedProperty
{
    public class DisabledPropertyDataEditor : PlaceHolder, IDataEditor
    {
        private string _message;
        public DisabledPropertyDataEditor(string message)
        {
            _message = message;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnLoad(e);
            Controls.Add(new LiteralControl(_message));
            Controls.Add(GetHidePanel());
        }

        public Control Editor
        {
            get
            {
                return this;
            }
        }

        public void Save()
        {
            
        }

        public bool ShowLabel
        {
            get { return true; }
        }

        public bool TreatAsRichTextEditor
        {
            get { return false; }
        }

        private Panel GetHidePanel()
        {
            Panel panel = new Panel();
            panel.Width = Unit.Percentage(100.0);
            panel.Height = Unit.Percentage(100.0);
            panel.Style.Add("z-index", "100");
            panel.Style.Add("background", "#FFFFFF url(images/propertyBackground.gif) repeat-x scroll center top");
            panel.Style.Add("position", "absolute");
            panel.Style.Add("top", "0");
            panel.Style.Add("left", "0");
            panel.Style.Add("filter", "alpha(opacity=70)");
            panel.Style.Add("-moz-opacity", "0.7");
            panel.Style.Add("opacity", "0.7");
            return panel;
        }
    }
}
