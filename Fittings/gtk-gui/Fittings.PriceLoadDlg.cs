
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
		
		private global::Gtk.HBox hbox3;
		
		private global::Gtk.VBox vbox3;
		
		private global::Gtk.Table table2;
		
		private global::Gamma.Widgets.yEnumComboBox comboCurrency;
		
		private global::Gtk.Label label5;
		
		private global::Gtk.ProgressBar progressParsing;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::Gamma.GtkWidgets.yTreeView ytreeviewParsing;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Button buttonMultiEdit;
		
		private global::Gtk.Button buttonManualSet;
		
		private global::Gtk.Button buttonResolveMultiFound;
		
		private global::Gtk.VSeparator vseparator1;
		
		private global::Fittings.MultiEditXLSRows multiedit;
		
		private global::Gtk.Label label6;
		
		private global::Gtk.HBox hbox9;
		
		private global::Gtk.Button buttonPrev;
		
		private global::Gtk.Button buttonNext;
		
		private global::Gtk.Button buttonCancel1;

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
			this.notebook1.CurrentPage = 1;
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
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Шаг 1");
			this.notebook1.SetTabLabel (this.vbox2, this.label1);
			this.label1.ShowAll ();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.table2 = new global::Gtk.Table (((uint)(1)), ((uint)(3)), false);
			this.table2.Name = "table2";
			this.table2.RowSpacing = ((uint)(6));
			this.table2.ColumnSpacing = ((uint)(6));
			// Container child table2.Gtk.Table+TableChild
			this.comboCurrency = new global::Gamma.Widgets.yEnumComboBox ();
			this.comboCurrency.Name = "comboCurrency";
			this.comboCurrency.ShowSpecialStateAll = false;
			this.comboCurrency.ShowSpecialStateNot = false;
			this.comboCurrency.UseShortTitle = false;
			this.comboCurrency.DefaultFirst = true;
			this.table2.Add (this.comboCurrency);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table2 [this.comboCurrency]));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Валюта прайса:");
			this.table2.Add (this.label5);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table2 [this.label5]));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.table2);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.table2]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.progressParsing = new global::Gtk.ProgressBar ();
			this.progressParsing.Name = "progressParsing";
			this.vbox3.Add (this.progressParsing);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.progressParsing]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.ytreeviewParsing = new global::Gamma.GtkWidgets.yTreeView ();
			this.ytreeviewParsing.CanFocus = true;
			this.ytreeviewParsing.Name = "ytreeviewParsing";
			this.GtkScrolledWindow1.Add (this.ytreeviewParsing);
			this.vbox3.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.GtkScrolledWindow1]));
			w15.Position = 2;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonMultiEdit = new global::Gtk.Button ();
			this.buttonMultiEdit.CanFocus = true;
			this.buttonMultiEdit.Name = "buttonMultiEdit";
			this.buttonMultiEdit.UseUnderline = true;
			this.buttonMultiEdit.Label = global::Mono.Unix.Catalog.GetString ("Изменить создаваемую арматуру");
			this.hbox2.Add (this.buttonMultiEdit);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonMultiEdit]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonManualSet = new global::Gtk.Button ();
			this.buttonManualSet.CanFocus = true;
			this.buttonManualSet.Name = "buttonManualSet";
			this.buttonManualSet.UseUnderline = true;
			this.buttonManualSet.Label = global::Mono.Unix.Catalog.GetString ("Установить принудительно");
			this.hbox2.Add (this.buttonManualSet);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonManualSet]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonResolveMultiFound = new global::Gtk.Button ();
			this.buttonResolveMultiFound.CanFocus = true;
			this.buttonResolveMultiFound.Name = "buttonResolveMultiFound";
			this.buttonResolveMultiFound.UseUnderline = true;
			this.buttonResolveMultiFound.Label = global::Mono.Unix.Catalog.GetString ("Решить конфликт");
			this.hbox2.Add (this.buttonResolveMultiFound);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonResolveMultiFound]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox2]));
			w19.Position = 3;
			w19.Expand = false;
			w19.Fill = false;
			this.hbox3.Add (this.vbox3);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.vbox3]));
			w20.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator ();
			this.vseparator1.Name = "vseparator1";
			this.hbox3.Add (this.vseparator1);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.vseparator1]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.multiedit = new global::Fittings.MultiEditXLSRows ();
			this.multiedit.Events = ((global::Gdk.EventMask)(256));
			this.multiedit.Name = "multiedit";
			this.hbox3.Add (this.multiedit);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.multiedit]));
			w22.Position = 2;
			w22.Expand = false;
			w22.Fill = false;
			this.notebook1.Add (this.hbox3);
			global::Gtk.Notebook.NotebookChild w23 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.hbox3]));
			w23.Position = 1;
			// Notebook tab
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Шаг 2");
			this.notebook1.SetTabLabel (this.hbox3, this.label6);
			this.label6.ShowAll ();
			this.vbox1.Add (this.notebook1);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.notebook1]));
			w24.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox9 = new global::Gtk.HBox ();
			this.hbox9.Name = "hbox9";
			this.hbox9.Spacing = 6;
			// Container child hbox9.Gtk.Box+BoxChild
			this.buttonPrev = new global::Gtk.Button ();
			this.buttonPrev.Sensitive = false;
			this.buttonPrev.CanFocus = true;
			this.buttonPrev.Name = "buttonPrev";
			this.buttonPrev.UseStock = true;
			this.buttonPrev.UseUnderline = true;
			this.buttonPrev.Label = "gtk-go-back";
			this.hbox9.Add (this.buttonPrev);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.buttonPrev]));
			w25.Position = 0;
			// Container child hbox9.Gtk.Box+BoxChild
			this.buttonNext = new global::Gtk.Button ();
			this.buttonNext.CanFocus = true;
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.UseStock = true;
			this.buttonNext.UseUnderline = true;
			this.buttonNext.Label = "gtk-go-forward";
			this.hbox9.Add (this.buttonNext);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.buttonNext]));
			w26.Position = 1;
			// Container child hbox9.Gtk.Box+BoxChild
			this.buttonCancel1 = new global::Gtk.Button ();
			this.buttonCancel1.CanFocus = true;
			this.buttonCancel1.Name = "buttonCancel1";
			this.buttonCancel1.UseStock = true;
			this.buttonCancel1.UseUnderline = true;
			this.buttonCancel1.Label = "gtk-cancel";
			this.hbox9.Add (this.buttonCancel1);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.buttonCancel1]));
			w27.Position = 2;
			w27.Expand = false;
			w27.Fill = false;
			this.vbox1.Add (this.hbox9);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox9]));
			w28.Position = 1;
			w28.Expand = false;
			w28.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.multiedit.Hide ();
			this.Hide ();
			this.yspinSkipRows.ValueChanged += new global::System.EventHandler (this.OnYspinSkipRowsValueChanged);
			this.comboSheet.Changed += new global::System.EventHandler (this.OnComboSheetChanged);
			this.ytreeviewParsing.RowActivated += new global::Gtk.RowActivatedHandler (this.OnYtreeviewParsingRowActivated);
			this.buttonMultiEdit.Clicked += new global::System.EventHandler (this.OnButtonMultiEditClicked);
			this.buttonManualSet.Clicked += new global::System.EventHandler (this.OnButtonManualSetClicked);
			this.buttonResolveMultiFound.Clicked += new global::System.EventHandler (this.OnButtonResolveMultiFoundClicked);
			this.buttonPrev.Clicked += new global::System.EventHandler (this.OnButtonPrevClicked);
			this.buttonNext.Clicked += new global::System.EventHandler (this.OnButtonNextClicked);
			this.buttonCancel1.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
		}
	}
}
