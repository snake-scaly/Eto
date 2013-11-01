using System;
using Eto.Forms;
using Eto.Drawing;
using sw = System.Windows;
using swc = System.Windows.Controls;
using msc = Microsoft.Samples.CustomControls;

namespace Eto.Platform.Wpf.Forms
{
	public class ColorDialogHandler : WidgetHandler<msc.ColorPickerDialog, ColorDialog>, IColorDialog
	{
		public ColorDialogHandler ()
		{
			this.Control = new msc.ColorPickerDialog {
				ShowAlpha = false
			};
		}

		public Color Color {
			get { return Control.SelectedColor.ToEto (); }
			set { Control.StartingColor = value.ToWpf (); }
		}

		public DialogResult ShowDialog (Window parent)
		{
			if (parent != null)
			{
				var owner = parent.ControlObject as sw.Window;
				Control.Owner = owner;
				Control.WindowStartupLocation = sw.WindowStartupLocation.CenterOwner;
			}
			var result = Control.ShowDialog();
			if (result == true)
			{
				Widget.OnColorChanged(EventArgs.Empty);
				return DialogResult.Ok;
			}
			return DialogResult.Cancel;
		}
	}
}

