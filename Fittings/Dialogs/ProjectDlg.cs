using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fittings.Domain;
using Fittings.ViewModel;
using Gamma.GtkWidgets;
using QSCurrency.CBR;
using QSOrmProject;
using QSProjectsLib;
using Gtk;

namespace Fittings
{
	public partial class ProjectDlg : OrmGtkDialogBase<Project>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();
		ProjectItem editingItem;

		CurrencyMode currencyMode;

		public ProjectDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Project> ();
			ConfigureDlg ();
		}

		public ProjectDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Project> (id);
			ConfigureDlg ();
		}

		public ProjectDlg (Project sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			customerEntry.Binding.AddBinding (Entity, e => e.Customer, w => w.Text).InitializeFromSource(); 
			projectNameEntry.Binding.AddBinding (Entity, e => e.ProjectName, w => w.Text).InitializeFromSource();

			var ConductorItemsList = UoW.GetAll<Conductor> ().ToList();

			projectTreeView.ColumnsConfig = ColumnsConfigFactory.Create <ProjectItem> ()
				.AddColumn ("№ п/п").AddNumericRenderer (x => x.SequenceNumber).Background("Lavender")
				.AddColumn ("Позиция ТРП").AddTextRenderer (x => x.TrpPositions).Editable()
				.AddColumn ("Тип").AddTextRenderer (x => x.Fitting.Name.NameRus).Background("White Smoke")
				.AddColumn ("Кол-во").AddNumericRenderer (x => x.Amount).Editing (new Gtk.Adjustment(0, 0, 10000000, 1, 100, 100))
				.AddColumn ("Диаметр").AddTextRenderer (x => x.Fitting.DiameterText).Background("White Smoke")
				.AddColumn ("Давление").AddTextRenderer (x => x.Fitting.PressureText).Background("White Smoke")
				.AddColumn ("Тип соединения").AddTextRenderer (x => x.Fitting.ConnectionType.NameRus).Background("White Smoke")
				.AddColumn ("Материал").AddTextRenderer (x => x.Fitting.BodyMaterial.NameRus).Background("White Smoke")
				.AddColumn ("Проводимая среда").AddComboRenderer (x => x.Conductor)
				.SetDisplayFunc(x => (x as Conductor).NameRus).FillItems<Conductor>(ConductorItemsList).Editing()
				//.AddColumn ("Группа").AddTextRenderer (x => x.PrGroup).Editable() 
				.AddColumn ("Расположение").AddTextRenderer (x => x.Location).Editable().EditingStartedEvent(OnLocationEditingStarted)
				.AddColumn ("Температура")
				.AddNumericRenderer (x => x.TemperatureMin).Editing (new Gtk.Adjustment(0, -273, 2000, 1, 100, 100)).WidthChars(5)
					.AddTextRenderer (x =>("—"))
				.AddNumericRenderer (x => x.TemperatureMax).Editing (new Gtk.Adjustment(0, -273, 2000, 1, 100, 100)).WidthChars(5)
				.AddColumn("Цена")
				.AddNumericRenderer (x => x.FittingPrice).Editing (new Gtk.Adjustment (0, 0, 10000000, 1, 100, 100)).Digits (2).WidthChars(10)
				.AddEnumRenderer (x => x.PriceCurrency).Editing()
				.AddColumn("Цена в валюте").AddTextRenderer(x => MoneyToCurrency(x.PriceCurrency, x.FittingPrice))//.Background("White Smoke")
				.AddColumn("Поставщик").AddTextRenderer(x => x.SelectedPriceItem != null ? x.SelectedPriceItem.Price.Provider.Name : String.Empty)
				.AddColumn("Из прайса").AddTextRenderer(x => x.SelectedPriceItem != null ? x.SelectedPriceItem.Price.Date.ToShortDateString() : String.Empty)
				.AddColumn("Сумма").AddTextRenderer(x => MoneyToCurrency(x.PriceCurrency, x.Amount * x.FittingPrice))
				.AddColumn ("Комментарий").AddTextRenderer (x => x.Comment).Editable()
				.Finish();

			projectTreeView.Selection.Changed += ProjectTreeView_Selection_Changed;
			projectTreeView.ItemsDataSource = Entity.ObservableProjectRows;
			projectTreeView.EnableGridLines = TreeViewGridLines.Both;

			comboCurrencyMode.ItemsEnum = typeof(CurrencyMode);
		}

		void OnLocationEditingStarted(object sender, Gtk.EditingStartedArgs args)
		{
			if (Entity.ProjectRows.Count == 0)
				return;
			
			var entry = args.Editable as Entry;
			var completion = new EntryCompletion();
			var list = new ListStore(typeof(string));
			foreach(var txt in Entity.ProjectRows.Where(x => !String.IsNullOrWhiteSpace(x.Location)).Select(x => x.Location.Trim()).Distinct())
			{
				list.AppendValues(txt);
			}
			completion.Model = list;
			completion.TextColumn = 0;
			entry.Completion = completion;
		}

		void ProjectTreeView_Selection_Changed (object sender, EventArgs e)
		{
			buttonEdit1.Sensitive = buttonRemove.Sensitive = buttonSelectPrice.Sensitive 
				= projectTreeView.Selection.CountSelectedRows() > 0 ;
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Project> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем информацию о проекте...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}

		protected void OnButtonAddClicked (object sender, EventArgs e)
		{
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.MultiSelect;
			dlg.ObjectSelected += Dlg_ObjectSelected1;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_ObjectSelected1 (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var fittings = UoW.GetById<Fitting> (e.GetNodes<FittingVMNode> ().Select (x => x.Id).ToArray());
			foreach (var item in e.GetNodes<FittingVMNode>()) {
				Entity.AddItem (fittings.First (x => x.Id == item.Id));
			}
		}

		protected void OnButtonEdit1Clicked (object sender, EventArgs e)
		{
			editingItem = projectTreeView.GetSelectedObject<ProjectItem> ();
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += Dlg_EditObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_EditObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			editingItem.Fitting = UoW.GetById<Fitting> (e.ObjectId);
		}

		protected void OnButtonRemoveClicked (object sender, EventArgs e)
		{
			Entity.ObservableProjectRows.Remove (projectTreeView.GetSelectedObject<ProjectItem> ());
		}

		protected void OnButtonSelectPriceClicked(object sender, EventArgs e)
		{
			var priceEditingItem = projectTreeView.GetSelectedObject<ProjectItem> ();
			var dlg = new ReferenceRepresentation (new FittingPricesVM (priceEditingItem.Fitting));
			dlg.Tag = priceEditingItem;
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += Dlg_ItemPriceSelected;;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_ItemPriceSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var dlg = sender as ReferenceRepresentation;
			var priceEditingItem = dlg.Tag as ProjectItem;
			var priceItem = UoW.GetById<PriceItem>(e.ObjectId);
			priceEditingItem.FittingPrice = priceItem.Cost;
			priceEditingItem.PriceCurrency = priceItem.Currency;
			priceEditingItem.SelectedPriceItem = priceItem;
		}

		protected void OnButtonUpdatePricesClicked(object sender, EventArgs e)
		{
			var newPricesList = new Dictionary<Price, int>();
			int effectedRows = 0;

			foreach(var item in Entity.ProjectRows)
			{
				PriceItem newPriceItem = Repository.PriceRepository.GetLastPriceItem(UoW, item.Fitting, item.SelectedPriceItem?.Price.Provider);
				if (newPriceItem == null)
					continue;
				if(!DomainHelper.EqualDomainObjects(newPriceItem, item.SelectedPriceItem))
				{
					effectedRows++;
					item.SelectedPriceItem = newPriceItem;
					item.FittingPrice = newPriceItem.Cost;
					item.PriceCurrency = newPriceItem.Currency;
					if (newPricesList.ContainsKey(newPriceItem.Price))
						newPricesList[newPriceItem.Price]++;
					else
						newPricesList.Add(newPriceItem.Price, 1);
				}
			}

			if(effectedRows == 0)
				MessageDialogWorks.RunInfoDialog("Новые цены не найдены!");
			else
				MessageDialogWorks.RunInfoDialog(String.Format("Были обновлены цены в {0}.\n" +
					"Новые цены были получены из {1}:\n" +
					"{2}",
					RusNumber.FormatCase(effectedRows, "{0} строке", "{0} строках", "{0} строках"),
					RusNumber.FormatCase(newPricesList.Count, "{0} прайса", "{0} прайсов", "{0} прайсов"),
					String.Join("\n", newPricesList.Select(x => 
						String.Format("{0} ({1:d}) - {2}", x.Key.Provider.Name, x.Key.Date, x.Value)
					))
				));
		}

		string MoneyToCurrency(PriceСurrency currency, decimal money)
		{
			if (money == 0)
				return String.Empty;

			string toCurrency;
			string fromCurrency = currency.ToString();
			if (currencyMode == CurrencyMode.Edit)
				toCurrency = currency.ToString();
			else
				toCurrency = currencyMode.ToString();

			if (fromCurrency.CompareTo(toCurrency) != 0)
			{
				var converted = CurrencyConverter.Convert(money, fromCurrency, toCurrency);
				if (converted == null)
					return String.Empty;
				money = converted.Value;
			}

			return String.Format("{0:N2} {1}",
				money, toCurrency);
		}

		protected void OnComboCurrencyModeChanged(object sender, EventArgs e)
		{
			currencyMode = (CurrencyMode)comboCurrencyMode.SelectedItem;
			var priceEditColumn = projectTreeView.Columns.First(x => x.Title == "Цена");
			var priceColumn = projectTreeView.Columns.First(x => x.Title == "Цена в валюте");

			switch(currencyMode)
			{
				case CurrencyMode.Edit:
					labelCurrencyInfo.LabelProp = String.Empty;
					priceEditColumn.Visible = true;
					priceColumn.Visible = false;
					break;
				case CurrencyMode.EUR:
					labelCurrencyInfo.LabelProp = String.Format("1 EUR = {0:N2} USD\n1 EUR = {1:N2} RUB",
						CurrencyConverter.Convert(1, "EUR", "USD"),
						CurrencyConverter.Convert(1, "EUR", "RUB")
					);
					priceEditColumn.Visible = false;
					priceColumn.Visible = true;
					break;
				case CurrencyMode.RUB:
					labelCurrencyInfo.LabelProp = String.Format("100 RUB = {0:N2} USD\n100 RUB = {1:N2} EUR",
						CurrencyConverter.Convert(100, "RUB", "USD"),
						CurrencyConverter.Convert(100, "RUB", "EUR")
					);
					priceEditColumn.Visible = false;
					priceColumn.Visible = true;
					break;
				case CurrencyMode.USD:
					labelCurrencyInfo.LabelProp = String.Format("1 USD = {0:N2} EUR\n1 USD = {1:N2} RUB",
						CurrencyConverter.Convert(1, "USD", "EUR"),
						CurrencyConverter.Convert(1, "USD", "RUB")
					);
					priceEditColumn.Visible = false;
					priceColumn.Visible = true;
					break;
			}
			projectTreeView.QueueDraw();
		}

		enum CurrencyMode {
			[Display(Name = "Как в прайсе")]
			Edit,
			[Display(Name = "В долларах")]
			USD,
			[Display(Name = "В рублях")]
			RUB,
			[Display(Name = "В евро")]
			EUR
		}
	}
}

