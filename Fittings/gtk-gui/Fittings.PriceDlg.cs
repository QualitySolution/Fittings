
// This file has been generated by the GUI designer. Do not modify.
namespace Fittings
{
	public partial class PriceDlg
	{
		private global::Gtk.VBox vbox3;
		
		private global::Gtk.HBox hbox7;
		
		private global::Gtk.Button buttonSave;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Table table4;
		
		private global::Gamma.Widgets.yDatePicker datepicker;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gamma.GtkWidgets.yTextView commentTextview;
		
		private global::Gtk.Label label13;
		
		private global::Gtk.Label label14;
		
		private global::Gtk.Label label15;
		
		private global::Gamma.Widgets.yEntryReference providerReference;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::Gamma.GtkWidgets.yTreeView ytreeview2;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Fittings.PriceDlg
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Fittings.PriceDlg";
			// Container child Fittings.PriceDlg.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox ();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button ();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString ("Сохранить");
			this.hbox7.Add (this.buttonSave);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.buttonSave]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Отменить");
			this.hbox7.Add (this.buttonCancel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.buttonCancel]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.vbox3.Add (this.hbox7);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox7]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.table4 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
			this.table4.Name = "table4";
			this.table4.RowSpacing = ((uint)(6));
			this.table4.ColumnSpacing = ((uint)(6));
			// Container child table4.Gtk.Table+TableChild
			this.datepicker = new global::Gamma.Widgets.yDatePicker ();
			this.datepicker.Events = ((global::Gdk.EventMask)(256));
			this.datepicker.Name = "datepicker";
			this.datepicker.WithTime = false;
			this.datepicker.Date = new global::System.DateTime (0);
			this.datepicker.IsEditable = true;
			this.datepicker.AutoSeparation = false;
			this.table4.Add (this.datepicker);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table4 [this.datepicker]));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.commentTextview = new global::Gamma.GtkWidgets.yTextView ();
			this.commentTextview.CanFocus = true;
			this.commentTextview.Name = "commentTextview";
			this.GtkScrolledWindow.Add (this.commentTextview);
			this.table4.Add (this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table4 [this.GtkScrolledWindow]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label13 = new global::Gtk.Label ();
			this.label13.Name = "label13";
			this.label13.Xalign = 1F;
			this.label13.LabelProp = global::Mono.Unix.Catalog.GetString ("Дата:");
			this.table4.Add (this.label13);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table4 [this.label13]));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label14 = new global::Gtk.Label ();
			this.label14.Name = "label14";
			this.label14.Xalign = 1F;
			this.label14.LabelProp = global::Mono.Unix.Catalog.GetString ("Поставщик:");
			this.table4.Add (this.label14);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table4 [this.label14]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label15 = new global::Gtk.Label ();
			this.label15.Name = "label15";
			this.label15.LabelProp = global::Mono.Unix.Catalog.GetString ("Комментарий:");
			this.table4.Add (this.label15);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table4 [this.label15]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.providerReference = new global::Gamma.Widgets.yEntryReference ();
			this.providerReference.Events = ((global::Gdk.EventMask)(256));
			this.providerReference.Name = "providerReference";
			this.table4.Add (this.providerReference);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table4 [this.providerReference]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.table4);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.table4]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.ytreeview2 = new global::Gamma.GtkWidgets.yTreeView ();
			this.ytreeview2.CanFocus = true;
			this.ytreeview2.Name = "ytreeview2";
			this.GtkScrolledWindow1.Add (this.ytreeview2);
			this.vbox3.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.GtkScrolledWindow1]));
			w13.Position = 2;
			this.Add (this.vbox3);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonSave.Clicked += new global::System.EventHandler (this.OnButtonSaveClicked);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
		}
	}
}
