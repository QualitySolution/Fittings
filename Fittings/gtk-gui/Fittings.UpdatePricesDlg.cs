
// This file has been generated by the GUI designer. Do not modify.
namespace Fittings
{
	public partial class UpdatePricesDlg
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Notebook notebook1;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.Table table1;
		
		private global::Gamma.Widgets.yEnumComboBox comboCurrency;
		
		private global::Gamma.Widgets.yListComboBox comboSheet;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.Label label5;
		
		private global::Gtk.Label labelSetColumnsInfo;
		
		private global::Gamma.GtkWidgets.ySpinButton yspinSkipRows;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gamma.GtkWidgets.yTreeView ytreeviewSetColumns;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.HBox hbox3;
		
		private global::Gtk.VBox vbox3;
		
		private global::Gtk.ProgressBar progressParsing;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::Gamma.GtkWidgets.yTreeView ytreeviewParsing;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Button buttonSelectAll;
		
		private global::Gtk.Button buttonUnSelect;
		
		private global::Gtk.VSeparator vseparator2;
		
		private global::Gtk.Label label4;
		
		private global::Gamma.Widgets.yListComboBox comboProvider;
		
		private global::Gtk.Button buttonResolveMultiFound;
		
		private global::Gtk.VSeparator vseparator1;
		
		private global::Gtk.Label label6;
		
		private global::Gtk.Table table4;
		
		private global::Gtk.Button buttonFinish;
		
		private global::Gtk.CheckButton checkOpenAfterSave;
		
		private global::Gtk.Label label11;
		
		private global::Gtk.Label label12;
		
		private global::Gtk.Label label13;
		
		private global::Gtk.Label label14;
		
		private global::Gtk.Label label15;
		
		private global::Gtk.Label label16;
		
		private global::Gtk.Label label17;
		
		private global::Gtk.Label label18;
		
		private global::Gtk.Label label8;
		
		private global::Gtk.Label label9;
		
		private global::Gtk.Label labelCurrency;
		
		private global::Gtk.Label labelFind;
		
		private global::Gtk.Label labelManualSet;
		
		private global::Gtk.Label labelPriceNotChanged;
		
		private global::Gtk.Label labelTotal;
		
		private global::Gtk.Label labelWillChangePrice;
		
		private global::Gtk.ProgressBar progressFinal;
		
		private global::Gtk.Label label7;
		
		private global::Gtk.HBox hbox9;
		
		private global::Gtk.Button buttonPrev;
		
		private global::Gtk.Button buttonNext;
		
		private global::Gtk.Button buttonCancel1;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Fittings.UpdatePricesDlg
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Fittings.UpdatePricesDlg";
			// Container child Fittings.UpdatePricesDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 2;
			this.notebook1.ShowBorder = false;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(6)), false);
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.comboCurrency = new global::Gamma.Widgets.yEnumComboBox ();
			this.comboCurrency.Name = "comboCurrency";
			this.comboCurrency.ShowSpecialStateAll = false;
			this.comboCurrency.ShowSpecialStateNot = false;
			this.comboCurrency.UseShortTitle = false;
			this.comboCurrency.DefaultFirst = true;
			this.table1.Add (this.comboCurrency);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.comboCurrency]));
			w1.LeftAttach = ((uint)(5));
			w1.RightAttach = ((uint)(6));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.comboSheet = new global::Gamma.Widgets.yListComboBox ();
			this.comboSheet.Name = "comboSheet";
			this.comboSheet.AddIfNotExist = false;
			this.comboSheet.DefaultFirst = true;
			this.table1.Add (this.comboSheet);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.comboSheet]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Выберите лист:");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Пропустить строки:");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w4.LeftAttach = ((uint)(2));
			w4.RightAttach = ((uint)(3));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Валюта прайса:");
			this.table1.Add (this.label5);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.label5]));
			w5.LeftAttach = ((uint)(4));
			w5.RightAttach = ((uint)(5));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelSetColumnsInfo = new global::Gtk.Label ();
			this.labelSetColumnsInfo.Name = "labelSetColumnsInfo";
			this.labelSetColumnsInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
			this.table1.Add (this.labelSetColumnsInfo);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelSetColumnsInfo]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.RightAttach = ((uint)(4));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
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
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.yspinSkipRows]));
			w7.LeftAttach = ((uint)(3));
			w7.RightAttach = ((uint)(4));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add (this.table1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.table1]));
			w8.Position = 0;
			w8.Expand = false;
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
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
			w10.Position = 1;
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
			this.progressParsing = new global::Gtk.ProgressBar ();
			this.progressParsing.Name = "progressParsing";
			this.vbox3.Add (this.progressParsing);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.progressParsing]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
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
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.GtkScrolledWindow1]));
			w14.Position = 1;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonSelectAll = new global::Gtk.Button ();
			this.buttonSelectAll.CanFocus = true;
			this.buttonSelectAll.Name = "buttonSelectAll";
			this.buttonSelectAll.UseUnderline = true;
			this.buttonSelectAll.Label = global::Mono.Unix.Catalog.GetString ("Отметить выделеное");
			global::Gtk.Image w15 = new global::Gtk.Image ();
			w15.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("Fittings.icons.buttons.checkbox-checked-symbolic.png");
			this.buttonSelectAll.Image = w15;
			this.hbox2.Add (this.buttonSelectAll);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonSelectAll]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonUnSelect = new global::Gtk.Button ();
			this.buttonUnSelect.CanFocus = true;
			this.buttonUnSelect.Name = "buttonUnSelect";
			this.buttonUnSelect.UseUnderline = true;
			this.buttonUnSelect.Label = global::Mono.Unix.Catalog.GetString ("Снять выделенное");
			global::Gtk.Image w17 = new global::Gtk.Image ();
			w17.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("Fittings.icons.buttons.checkbox-symbolic.png");
			this.buttonUnSelect.Image = w17;
			this.hbox2.Add (this.buttonUnSelect);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonUnSelect]));
			w18.Position = 1;
			w18.Expand = false;
			w18.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vseparator2 = new global::Gtk.VSeparator ();
			this.vseparator2.Name = "vseparator2";
			this.hbox2.Add (this.vseparator2);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.vseparator2]));
			w19.Position = 2;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Цена от");
			this.hbox2.Add (this.label4);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label4]));
			w20.Position = 3;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.comboProvider = new global::Gamma.Widgets.yListComboBox ();
			this.comboProvider.Name = "comboProvider";
			this.comboProvider.AddIfNotExist = false;
			this.comboProvider.DefaultFirst = false;
			this.hbox2.Add (this.comboProvider);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.comboProvider]));
			w21.Position = 4;
			w21.Expand = false;
			w21.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonResolveMultiFound = new global::Gtk.Button ();
			this.buttonResolveMultiFound.CanFocus = true;
			this.buttonResolveMultiFound.Name = "buttonResolveMultiFound";
			this.buttonResolveMultiFound.UseUnderline = true;
			this.buttonResolveMultiFound.Label = global::Mono.Unix.Catalog.GetString ("Установить");
			this.hbox2.Add (this.buttonResolveMultiFound);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonResolveMultiFound]));
			w22.Position = 5;
			w22.Expand = false;
			w22.Fill = false;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox2]));
			w23.Position = 2;
			w23.Expand = false;
			w23.Fill = false;
			this.hbox3.Add (this.vbox3);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.vbox3]));
			w24.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator ();
			this.vseparator1.Name = "vseparator1";
			this.hbox3.Add (this.vseparator1);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.vseparator1]));
			w25.Position = 1;
			w25.Expand = false;
			w25.Fill = false;
			this.notebook1.Add (this.hbox3);
			global::Gtk.Notebook.NotebookChild w26 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.hbox3]));
			w26.Position = 1;
			// Notebook tab
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Шаг 2");
			this.notebook1.SetTabLabel (this.hbox3, this.label6);
			this.label6.ShowAll ();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.table4 = new global::Gtk.Table (((uint)(11)), ((uint)(4)), false);
			this.table4.Name = "table4";
			this.table4.RowSpacing = ((uint)(6));
			this.table4.ColumnSpacing = ((uint)(6));
			// Container child table4.Gtk.Table+TableChild
			this.buttonFinish = new global::Gtk.Button ();
			this.buttonFinish.CanFocus = true;
			this.buttonFinish.Name = "buttonFinish";
			this.buttonFinish.UseUnderline = true;
			this.buttonFinish.Label = global::Mono.Unix.Catalog.GetString ("Сохранить изменения в файл");
			global::Gtk.Image w27 = new global::Gtk.Image ();
			w27.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save-as", global::Gtk.IconSize.Button);
			this.buttonFinish.Image = w27;
			this.table4.Add (this.buttonFinish);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table4 [this.buttonFinish]));
			w28.TopAttach = ((uint)(9));
			w28.BottomAttach = ((uint)(10));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(3));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.checkOpenAfterSave = new global::Gtk.CheckButton ();
			this.checkOpenAfterSave.CanFocus = true;
			this.checkOpenAfterSave.Name = "checkOpenAfterSave";
			this.checkOpenAfterSave.Label = global::Mono.Unix.Catalog.GetString ("Открыть после сохранения");
			this.checkOpenAfterSave.Active = true;
			this.checkOpenAfterSave.DrawIndicator = true;
			this.checkOpenAfterSave.UseUnderline = true;
			this.table4.Add (this.checkOpenAfterSave);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table4 [this.checkOpenAfterSave]));
			w29.TopAttach = ((uint)(8));
			w29.BottomAttach = ((uint)(9));
			w29.LeftAttach = ((uint)(1));
			w29.RightAttach = ((uint)(3));
			w29.XOptions = ((global::Gtk.AttachOptions)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label11 = new global::Gtk.Label ();
			this.label11.Name = "label11";
			this.table4.Add (this.label11);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table4 [this.label11]));
			w30.TopAttach = ((uint)(1));
			w30.BottomAttach = ((uint)(2));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label12 = new global::Gtk.Label ();
			this.label12.Name = "label12";
			this.table4.Add (this.label12);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.table4 [this.label12]));
			w31.TopAttach = ((uint)(1));
			w31.BottomAttach = ((uint)(2));
			w31.LeftAttach = ((uint)(3));
			w31.RightAttach = ((uint)(4));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label13 = new global::Gtk.Label ();
			this.label13.Name = "label13";
			this.table4.Add (this.label13);
			global::Gtk.Table.TableChild w32 = ((global::Gtk.Table.TableChild)(this.table4 [this.label13]));
			w32.LeftAttach = ((uint)(1));
			w32.RightAttach = ((uint)(2));
			w32.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label14 = new global::Gtk.Label ();
			this.label14.Name = "label14";
			this.table4.Add (this.label14);
			global::Gtk.Table.TableChild w33 = ((global::Gtk.Table.TableChild)(this.table4 [this.label14]));
			w33.TopAttach = ((uint)(10));
			w33.BottomAttach = ((uint)(11));
			w33.LeftAttach = ((uint)(1));
			w33.RightAttach = ((uint)(2));
			w33.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label15 = new global::Gtk.Label ();
			this.label15.Name = "label15";
			this.label15.Xalign = 1F;
			this.label15.LabelProp = global::Mono.Unix.Catalog.GetString ("Всего обработано строк файла:");
			this.table4.Add (this.label15);
			global::Gtk.Table.TableChild w34 = ((global::Gtk.Table.TableChild)(this.table4 [this.label15]));
			w34.TopAttach = ((uint)(1));
			w34.BottomAttach = ((uint)(2));
			w34.LeftAttach = ((uint)(1));
			w34.RightAttach = ((uint)(2));
			w34.XOptions = ((global::Gtk.AttachOptions)(4));
			w34.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label16 = new global::Gtk.Label ();
			this.label16.Name = "label16";
			this.label16.Xalign = 1F;
			this.label16.LabelProp = global::Mono.Unix.Catalog.GetString ("Из них найдено автоматически:");
			this.table4.Add (this.label16);
			global::Gtk.Table.TableChild w35 = ((global::Gtk.Table.TableChild)(this.table4 [this.label16]));
			w35.TopAttach = ((uint)(2));
			w35.BottomAttach = ((uint)(3));
			w35.LeftAttach = ((uint)(1));
			w35.RightAttach = ((uint)(2));
			w35.XOptions = ((global::Gtk.AttachOptions)(4));
			w35.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label17 = new global::Gtk.Label ();
			this.label17.Name = "label17";
			this.label17.Xalign = 1F;
			this.label17.LabelProp = global::Mono.Unix.Catalog.GetString ("Вручну выбрано цен:");
			this.table4.Add (this.label17);
			global::Gtk.Table.TableChild w36 = ((global::Gtk.Table.TableChild)(this.table4 [this.label17]));
			w36.TopAttach = ((uint)(3));
			w36.BottomAttach = ((uint)(4));
			w36.LeftAttach = ((uint)(1));
			w36.RightAttach = ((uint)(2));
			w36.XOptions = ((global::Gtk.AttachOptions)(4));
			w36.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label18 = new global::Gtk.Label ();
			this.label18.Name = "label18";
			this.label18.Xalign = 1F;
			this.label18.LabelProp = global::Mono.Unix.Catalog.GetString ("Цена не изменилась в:");
			this.table4.Add (this.label18);
			global::Gtk.Table.TableChild w37 = ((global::Gtk.Table.TableChild)(this.table4 [this.label18]));
			w37.TopAttach = ((uint)(4));
			w37.BottomAttach = ((uint)(5));
			w37.LeftAttach = ((uint)(1));
			w37.RightAttach = ((uint)(2));
			w37.XOptions = ((global::Gtk.AttachOptions)(4));
			w37.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.Xalign = 1F;
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Все цены в валюте:");
			this.table4.Add (this.label8);
			global::Gtk.Table.TableChild w38 = ((global::Gtk.Table.TableChild)(this.table4 [this.label8]));
			w38.TopAttach = ((uint)(6));
			w38.BottomAttach = ((uint)(7));
			w38.LeftAttach = ((uint)(1));
			w38.RightAttach = ((uint)(2));
			w38.XOptions = ((global::Gtk.AttachOptions)(4));
			w38.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label ();
			this.label9.Name = "label9";
			this.label9.Xalign = 1F;
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString ("Выбрано строк для обновление в файле:");
			this.table4.Add (this.label9);
			global::Gtk.Table.TableChild w39 = ((global::Gtk.Table.TableChild)(this.table4 [this.label9]));
			w39.TopAttach = ((uint)(5));
			w39.BottomAttach = ((uint)(6));
			w39.LeftAttach = ((uint)(1));
			w39.RightAttach = ((uint)(2));
			w39.XOptions = ((global::Gtk.AttachOptions)(4));
			w39.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.labelCurrency = new global::Gtk.Label ();
			this.labelCurrency.Name = "labelCurrency";
			this.labelCurrency.LabelProp = global::Mono.Unix.Catalog.GetString ("label7");
			this.table4.Add (this.labelCurrency);
			global::Gtk.Table.TableChild w40 = ((global::Gtk.Table.TableChild)(this.table4 [this.labelCurrency]));
			w40.TopAttach = ((uint)(6));
			w40.BottomAttach = ((uint)(7));
			w40.LeftAttach = ((uint)(2));
			w40.RightAttach = ((uint)(3));
			w40.XOptions = ((global::Gtk.AttachOptions)(4));
			w40.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.labelFind = new global::Gtk.Label ();
			this.labelFind.Name = "labelFind";
			this.labelFind.LabelProp = global::Mono.Unix.Catalog.GetString ("label2");
			this.table4.Add (this.labelFind);
			global::Gtk.Table.TableChild w41 = ((global::Gtk.Table.TableChild)(this.table4 [this.labelFind]));
			w41.TopAttach = ((uint)(2));
			w41.BottomAttach = ((uint)(3));
			w41.LeftAttach = ((uint)(2));
			w41.RightAttach = ((uint)(3));
			w41.XOptions = ((global::Gtk.AttachOptions)(4));
			w41.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.labelManualSet = new global::Gtk.Label ();
			this.labelManualSet.Name = "labelManualSet";
			this.labelManualSet.LabelProp = global::Mono.Unix.Catalog.GetString ("label3");
			this.table4.Add (this.labelManualSet);
			global::Gtk.Table.TableChild w42 = ((global::Gtk.Table.TableChild)(this.table4 [this.labelManualSet]));
			w42.TopAttach = ((uint)(3));
			w42.BottomAttach = ((uint)(4));
			w42.LeftAttach = ((uint)(2));
			w42.RightAttach = ((uint)(3));
			w42.XOptions = ((global::Gtk.AttachOptions)(4));
			w42.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.labelPriceNotChanged = new global::Gtk.Label ();
			this.labelPriceNotChanged.Name = "labelPriceNotChanged";
			this.labelPriceNotChanged.LabelProp = global::Mono.Unix.Catalog.GetString ("label4");
			this.table4.Add (this.labelPriceNotChanged);
			global::Gtk.Table.TableChild w43 = ((global::Gtk.Table.TableChild)(this.table4 [this.labelPriceNotChanged]));
			w43.TopAttach = ((uint)(4));
			w43.BottomAttach = ((uint)(5));
			w43.LeftAttach = ((uint)(2));
			w43.RightAttach = ((uint)(3));
			w43.XOptions = ((global::Gtk.AttachOptions)(4));
			w43.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.labelTotal = new global::Gtk.Label ();
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
			this.table4.Add (this.labelTotal);
			global::Gtk.Table.TableChild w44 = ((global::Gtk.Table.TableChild)(this.table4 [this.labelTotal]));
			w44.TopAttach = ((uint)(1));
			w44.BottomAttach = ((uint)(2));
			w44.LeftAttach = ((uint)(2));
			w44.RightAttach = ((uint)(3));
			w44.XOptions = ((global::Gtk.AttachOptions)(4));
			w44.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.labelWillChangePrice = new global::Gtk.Label ();
			this.labelWillChangePrice.Name = "labelWillChangePrice";
			this.labelWillChangePrice.LabelProp = global::Mono.Unix.Catalog.GetString ("label9");
			this.table4.Add (this.labelWillChangePrice);
			global::Gtk.Table.TableChild w45 = ((global::Gtk.Table.TableChild)(this.table4 [this.labelWillChangePrice]));
			w45.TopAttach = ((uint)(5));
			w45.BottomAttach = ((uint)(6));
			w45.LeftAttach = ((uint)(2));
			w45.RightAttach = ((uint)(3));
			w45.XOptions = ((global::Gtk.AttachOptions)(4));
			w45.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.progressFinal = new global::Gtk.ProgressBar ();
			this.progressFinal.Name = "progressFinal";
			this.table4.Add (this.progressFinal);
			global::Gtk.Table.TableChild w46 = ((global::Gtk.Table.TableChild)(this.table4 [this.progressFinal]));
			w46.TopAttach = ((uint)(7));
			w46.BottomAttach = ((uint)(8));
			w46.LeftAttach = ((uint)(1));
			w46.RightAttach = ((uint)(3));
			w46.XOptions = ((global::Gtk.AttachOptions)(4));
			w46.YOptions = ((global::Gtk.AttachOptions)(4));
			this.notebook1.Add (this.table4);
			global::Gtk.Notebook.NotebookChild w47 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.table4]));
			w47.Position = 2;
			// Notebook tab
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Шаг 3");
			this.notebook1.SetTabLabel (this.table4, this.label7);
			this.label7.ShowAll ();
			this.vbox1.Add (this.notebook1);
			global::Gtk.Box.BoxChild w48 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.notebook1]));
			w48.Position = 0;
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
			global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.buttonPrev]));
			w49.Position = 0;
			// Container child hbox9.Gtk.Box+BoxChild
			this.buttonNext = new global::Gtk.Button ();
			this.buttonNext.CanFocus = true;
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.UseStock = true;
			this.buttonNext.UseUnderline = true;
			this.buttonNext.Label = "gtk-go-forward";
			this.hbox9.Add (this.buttonNext);
			global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.buttonNext]));
			w50.Position = 1;
			// Container child hbox9.Gtk.Box+BoxChild
			this.buttonCancel1 = new global::Gtk.Button ();
			this.buttonCancel1.CanFocus = true;
			this.buttonCancel1.Name = "buttonCancel1";
			this.buttonCancel1.UseStock = true;
			this.buttonCancel1.UseUnderline = true;
			this.buttonCancel1.Label = "gtk-cancel";
			this.hbox9.Add (this.buttonCancel1);
			global::Gtk.Box.BoxChild w51 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.buttonCancel1]));
			w51.Position = 2;
			w51.Expand = false;
			w51.Fill = false;
			this.vbox1.Add (this.hbox9);
			global::Gtk.Box.BoxChild w52 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox9]));
			w52.Position = 1;
			w52.Expand = false;
			w52.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.yspinSkipRows.ValueChanged += new global::System.EventHandler (this.OnYspinSkipRowsValueChanged);
			this.comboSheet.Changed += new global::System.EventHandler (this.OnComboSheetChanged);
			this.buttonSelectAll.Clicked += new global::System.EventHandler (this.OnButtonSelectAllClicked);
			this.buttonUnSelect.Clicked += new global::System.EventHandler (this.OnButtonUnSelectClicked);
			this.comboProvider.Changed += new global::System.EventHandler (this.OnComboProviderChanged);
			this.buttonResolveMultiFound.Clicked += new global::System.EventHandler (this.OnButtonResolveMultiFoundClicked);
			this.buttonFinish.Clicked += new global::System.EventHandler (this.OnButtonFinishClicked);
			this.buttonPrev.Clicked += new global::System.EventHandler (this.OnButtonPrevClicked);
			this.buttonNext.Clicked += new global::System.EventHandler (this.OnButtonNextClicked);
			this.buttonCancel1.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
		}
	}
}
