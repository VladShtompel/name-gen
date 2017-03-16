using System;
using Gtk;

namespace NameGenerator 
{
	public partial class MainWindow: Gtk.Window
	{
		private NameGen gen = new NameGen ();

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			label7.Visible = false;
			combobox4.Active = 1;
			combobox5.Active = 4;
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnButton3Clicked (object sender, EventArgs e)
		{
			int lbox = Convert.ToInt32 (combobox4.ActiveText);
			int hbox = Convert.ToInt32 (combobox5.ActiveText);

			if (lbox > hbox) {
				label7.Visible = true;
			} else {
				label7.Visible = false;
				gen.min_l = lbox;
				gen.max_l = hbox;
			}
			txtbox1.Text = gen.Generate ();
		}


	}
}