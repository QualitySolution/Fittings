
// This file has been generated by the GUI designer. Do not modify.
namespace Fittings
{
	public partial class PriceLoadDlg
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Notebook notebook1;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.Table table1;
		
		private global::Gamma.Widgets.yListComboBox comboSheet;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.Label labelSetColumnsInfo;
		
		private global::Gamma.GtkWidgets.ySpinButton yspinSkipRows;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gamma.GtkWidgets.yTreeView ytreeviewSetColumns;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Button buttonPrev;
		
		private global::Gtk.Button buttonNext;
		
		private global::Gtk.Button buttonCancel;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Fittings.PriceLoadDlg
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Fittings.PriceLoadDlg";
			// Container child Fittings.PriceLoadDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			this.notebook1.ShowBorder = false;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.comboSheet = new global::Gamma.Widgets.yListComboBox ();
			this.comboSheet.Name = "comboSheet";
			this.comboSheet.AddIfNotExist = false;
			this.comboSheet.DefaultFirst = true;
			this.table1.Add (this.comboSheet);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.comboSheet]));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Выберите лист:");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Пропустить строки:");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w3.LeftAttach = ((uint)(2));
			w3.RightAttach = ((uint)(3));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelSetColumnsInfo = new global::Gtk.Label ();
			this.labelSetColumnsInfo.Name = "labelSetColumnsInfo";
			this.labelSetColumnsInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
			this.table1.Add (this.labelSetColumnsInfo);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelSetColumnsInfo]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.RightAttach = ((uint)(4));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yspinSkipRows = new global::Gamma.GtkWidgets.ySpinButton (0, 100, 1);
			this.yspinSkipRows.CanFocus = true;
			this.yspinSkipRows.Name = "yspinSkipRows";
			this.yspinSkipRows.Adjustment.PageIncrement = 10;
			this.yspinSkipRows.ClimbRate = 1;
			this.yspinSkipRows.Numeric = true;
			this.yspinSkipRows.ValueAsDecimal = 0m;
			this.yspinSkipRows.ValueAsInt = 0;
			this.table1.Add (this.yspinSkipRows);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.yspinSkipRows]));
			w5.LeftAttach = ((uint)(3));
			w5.RightAttach = ((uint)(4));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add (this.table1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.table1]));
			w6.Position = 0;
			w6.Expand = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytreeviewSetColumns = new global::Gamma.GtkWidgets.yTreeView ();
			this.ytreeviewSetColumns.CanFocus = true;
			this.ytreeviewSetColumns.Name = "ytreeviewSetColumns";
			this.GtkScrolledWindow.Add (this.ytreeviewSetColumns);
			this.vbox2.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
			w8.Position = 1;
			this.notebook1.Add (this.vbox2);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("page1");
			this.notebook1.SetTabLabel (this.vbox2, this.label1);
			this.label1.ShowAll ();
			this.vbox1.Add (this.notebook1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.notebook1]));
			w10.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonPrev = new global::Gtk.Button ();
			this.buttonPrev.Sensitive = false;
			this.buttonPrev.CanFocus = true;
			this.buttonPrev.Name = "buttonPrev";
			this.buttonPrev.UseStock = true;
			this.buttonPrev.UseUnderline = true;
			this.buttonPrev.Label = "gtk-go-back";
			this.hbox1.Add (this.buttonPrev);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonPrev]));
			w11.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonNext = new global::Gtk.Button ();
			this.buttonNext.CanFocus = true;
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.UseStock = true;
			this.buttonNext.UseUnderline = true;
			this.buttonNext.Label = "gtk-go-forward";
			this.hbox1.Add (this.buttonNext);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonNext]));
			w12.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.hbox1.Add (this.buttonCancel);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonCancel]));
			w13.Position = 2;
			w13.Expand = false;
			w13.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.yspinSkipRows.ValueChanged += new global::System.EventHandler (this.OnYspinSkipRowsValueChanged);
			this.comboSheet.Changed += new global::System.EventHandler (this.OnComboSheetChanged);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
		}
	}
}
