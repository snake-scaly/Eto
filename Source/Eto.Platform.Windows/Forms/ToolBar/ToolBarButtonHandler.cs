using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;
using Eto.Forms;

namespace Eto.Platform.Windows
{

	public class ToolBarButtonHandler : ToolBarItemHandler<SWF.ToolStripButton, ToolBarButton>, IToolBarButton
	{
		readonly SWF.ToolStripButton control;

		public ToolBarButtonHandler()
		{
			control = new SWF.ToolStripButton();
			control.Tag = this;
			control.Click += control_Click;
			Control = control;
		}

		void control_Click(object sender, EventArgs e)
		{
			Widget.OnClick(EventArgs.Empty);
		}

		#region IToolBarButton Members

		public override bool Enabled
		{
			get { return control.Enabled; }
			set { control.Enabled = value; }
		}
		

		#endregion

		public override void CreateControl(ToolBarHandler handler)
		{
			handler.Control.Items.Add(control);
		}


		public override void InvokeButton()
		{
			Widget.OnClick(EventArgs.Empty);
		}

	}


}
