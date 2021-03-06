// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
namespace DotNetNuke.Web.UI.WebControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Obsolete("Telerik support will be removed in DNN Platform 10.0.0.  You will need to find an alternative solution")]
    public class DnnFormToggleButtonItem : DnnFormItemBase
    {
        // private DnnRadButton _checkBox;
        private CheckBox _checkBox;

        public DnnFormToggleButtonItem()
        {
            this.Mode = CheckBoxMode.TrueFalse;
        }

        public enum CheckBoxMode
        {
            TrueFalse = 0,
            YN = 1,
            YesNo = 2,
        }

        public CheckBoxMode Mode { get; set; }

        protected override WebControl CreateControlInternal(Control container)
        {
            // _checkBox = new DnnRadButton {ID = ID + "_CheckBox", ButtonType = RadButtonType.ToggleButton, ToggleType = ButtonToggleType.CheckBox, AutoPostBack = false};
            this._checkBox = new CheckBox { ID = this.ID + "_CheckBox", AutoPostBack = false };

            this._checkBox.CheckedChanged += this.CheckedChanged;
            container.Controls.Add(this._checkBox);

            // Load from ControlState
            if (!this._checkBox.Page.IsPostBack)
            {
            }

            switch (this.Mode)
            {
                case CheckBoxMode.YN:
                case CheckBoxMode.YesNo:
                    var stringValue = this.Value as string;
                    if (stringValue != null)
                    {
                        this._checkBox.Checked = stringValue.StartsWith("Y", StringComparison.InvariantCultureIgnoreCase);
                    }

                    break;
                default:
                    this._checkBox.Checked = Convert.ToBoolean(this.Value);
                    break;
            }

            return this._checkBox;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.FormMode = DnnFormMode.Short;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            string newValue;
            switch (this.Mode)
            {
                case CheckBoxMode.YN:
                    newValue = this._checkBox.Checked ? "Y" : "N";
                    break;
                case CheckBoxMode.YesNo:
                    newValue = this._checkBox.Checked ? "Yes" : "No";
                    break;
                default:
                    newValue = this._checkBox.Checked ? "true" : "false";
                    break;
            }

            this.UpdateDataSource(this.Value, newValue, this.DataField);
        }
    }
}
