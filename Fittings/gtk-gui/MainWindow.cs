
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action Action;
	
	private global::Gtk.Action ActionPasswordChange;
	
	private global::Gtk.Action quitAction;
	
	private global::Gtk.Action UsersAction;
	
	private global::Gtk.Action Action4;
	
	private global::Gtk.Action aboutAction;
	
	private global::Gtk.Action Action2;
	
	private global::Gtk.Action ActionFittings;
	
	private global::Gtk.Action ActionProvider;
	
	private global::Gtk.Action ActionFittingType;
	
	private global::Gtk.Action ActionBodyMaterial;
	
	private global::Gtk.Action ActionConnectionType;
	
	private global::Gtk.Action ActionConductor;
	
	private global::Gtk.Action ActionPressure;
	
	private global::Gtk.Action ActionDiameter;
	
	private global::Gtk.Action Action3;
	
	private global::Gtk.Action ActionProject;
	
	private global::Gtk.Action ActionPrice;
	
	private global::Gtk.Action ChekUpdateAction;
	
	private global::Gtk.Action Action5;
	
	private global::Gtk.Action ActionChangelog;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.MenuBar menubar1;
	
	private global::QSTDI.TdiNotebook tdiMain;
	
	private global::Gtk.Statusbar statusbar1;
	
	private global::Gtk.Label labelStatus;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.Action = new global::Gtk.Action ("Action", global::Mono.Unix.Catalog.GetString ("База"), null, null);
		this.Action.ShortLabel = global::Mono.Unix.Catalog.GetString ("База");
		w1.Add (this.Action, null);
		this.ActionPasswordChange = new global::Gtk.Action ("ActionPasswordChange", global::Mono.Unix.Catalog.GetString ("Изменить пароль"), null, null);
		this.ActionPasswordChange.ShortLabel = global::Mono.Unix.Catalog.GetString ("Изменить пароль");
		w1.Add (this.ActionPasswordChange, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("Выход"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Выход");
		w1.Add (this.quitAction, null);
		this.UsersAction = new global::Gtk.Action ("UsersAction", global::Mono.Unix.Catalog.GetString ("Пользователи"), null, null);
		this.UsersAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Пользователи");
		w1.Add (this.UsersAction, null);
		this.Action4 = new global::Gtk.Action ("Action4", global::Mono.Unix.Catalog.GetString ("Справка"), null, null);
		this.Action4.ShortLabel = global::Mono.Unix.Catalog.GetString ("Справка");
		w1.Add (this.Action4, null);
		this.aboutAction = new global::Gtk.Action ("aboutAction", global::Mono.Unix.Catalog.GetString ("_О программе"), null, "gtk-about");
		this.aboutAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_О программе");
		w1.Add (this.aboutAction, null);
		this.Action2 = new global::Gtk.Action ("Action2", global::Mono.Unix.Catalog.GetString ("Справочники"), null, null);
		this.Action2.ShortLabel = global::Mono.Unix.Catalog.GetString ("Справочники");
		w1.Add (this.Action2, null);
		this.ActionFittings = new global::Gtk.Action ("ActionFittings", global::Mono.Unix.Catalog.GetString ("Арматура"), null, null);
		this.ActionFittings.ShortLabel = global::Mono.Unix.Catalog.GetString ("Арматура");
		w1.Add (this.ActionFittings, null);
		this.ActionProvider = new global::Gtk.Action ("ActionProvider", global::Mono.Unix.Catalog.GetString ("Поставщики"), null, null);
		this.ActionProvider.ShortLabel = global::Mono.Unix.Catalog.GetString ("Поставщики");
		w1.Add (this.ActionProvider, null);
		this.ActionFittingType = new global::Gtk.Action ("ActionFittingType", global::Mono.Unix.Catalog.GetString ("Виды арматуры"), null, null);
		this.ActionFittingType.ShortLabel = global::Mono.Unix.Catalog.GetString ("Виды арматуры");
		w1.Add (this.ActionFittingType, null);
		this.ActionBodyMaterial = new global::Gtk.Action ("ActionBodyMaterial", global::Mono.Unix.Catalog.GetString ("Материал корпуса"), null, null);
		this.ActionBodyMaterial.ShortLabel = global::Mono.Unix.Catalog.GetString ("Материал корпуса");
		w1.Add (this.ActionBodyMaterial, null);
		this.ActionConnectionType = new global::Gtk.Action ("ActionConnectionType", global::Mono.Unix.Catalog.GetString ("Виды соединений"), null, null);
		this.ActionConnectionType.ShortLabel = global::Mono.Unix.Catalog.GetString ("Виды соединений");
		w1.Add (this.ActionConnectionType, null);
		this.ActionConductor = new global::Gtk.Action ("ActionConductor", global::Mono.Unix.Catalog.GetString ("Проводимая среда"), null, null);
		this.ActionConductor.ShortLabel = global::Mono.Unix.Catalog.GetString ("Проводимая среда");
		w1.Add (this.ActionConductor, null);
		this.ActionPressure = new global::Gtk.Action ("ActionPressure", global::Mono.Unix.Catalog.GetString ("Давление"), null, null);
		this.ActionPressure.ShortLabel = global::Mono.Unix.Catalog.GetString ("Давление");
		w1.Add (this.ActionPressure, null);
		this.ActionDiameter = new global::Gtk.Action ("ActionDiameter", global::Mono.Unix.Catalog.GetString ("Диаметр"), null, null);
		this.ActionDiameter.ShortLabel = global::Mono.Unix.Catalog.GetString ("Диаметр");
		w1.Add (this.ActionDiameter, null);
		this.Action3 = new global::Gtk.Action ("Action3", global::Mono.Unix.Catalog.GetString ("Основные данные"), null, null);
		this.Action3.ShortLabel = global::Mono.Unix.Catalog.GetString ("Дополнительно");
		w1.Add (this.Action3, null);
		this.ActionProject = new global::Gtk.Action ("ActionProject", global::Mono.Unix.Catalog.GetString ("Проекты"), null, null);
		this.ActionProject.ShortLabel = global::Mono.Unix.Catalog.GetString ("Проекты");
		w1.Add (this.ActionProject, null);
		this.ActionPrice = new global::Gtk.Action ("ActionPrice", global::Mono.Unix.Catalog.GetString ("Прайс"), null, null);
		this.ActionPrice.ShortLabel = global::Mono.Unix.Catalog.GetString ("Прайс");
		w1.Add (this.ActionPrice, null);
		this.ChekUpdateAction = new global::Gtk.Action ("ChekUpdateAction", global::Mono.Unix.Catalog.GetString ("Проверить обновления..."), null, "gtk-go-down");
		this.ChekUpdateAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Проверить обновления...");
		w1.Add (this.ChekUpdateAction, null);
		this.Action5 = new global::Gtk.Action ("Action5", global::Mono.Unix.Catalog.GetString ("Обновить цены в файле"), global::Mono.Unix.Catalog.GetString ("Помощник обновления цен в файле проекта."), null);
		this.Action5.HideIfEmpty = false;
		this.Action5.ShortLabel = global::Mono.Unix.Catalog.GetString ("Обновить цены в файле");
		w1.Add (this.Action5, null);
		this.ActionChangelog = new global::Gtk.Action ("ActionChangelog", global::Mono.Unix.Catalog.GetString ("История версий"), null, null);
		this.ActionChangelog.ShortLabel = global::Mono.Unix.Catalog.GetString ("История версий");
		w1.Add (this.ActionChangelog, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.Icon = global::Gdk.Pixbuf.LoadFromResource ("Fittings.icons.header-logo.ico");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='Action' action='Action'><menuitem name='ActionPasswordChange' action='ActionPasswordChange'/><menuitem name='UsersAction' action='UsersAction'/><separator/><menuitem name='quitAction' action='quitAction'/></menu><menu name='Action3' action='Action3'><menuitem name='ActionProject' action='ActionProject'/><menuitem name='ActionPrice' action='ActionPrice'/></menu><menuitem name='Action5' action='Action5'/><menu name='Action2' action='Action2'><menuitem name='ActionFittings' action='ActionFittings'/><menuitem name='ActionProvider' action='ActionProvider'/><separator/><menuitem name='ActionFittingType' action='ActionFittingType'/><menuitem name='ActionBodyMaterial' action='ActionBodyMaterial'/><menuitem name='ActionConnectionType' action='ActionConnectionType'/><menuitem name='ActionConductor' action='ActionConductor'/><separator/><menuitem name='ActionPressure' action='ActionPressure'/><menuitem name='ActionDiameter' action='ActionDiameter'/></menu><menu name='Action4' action='Action4'><menuitem name='ActionChangelog' action='ActionChangelog'/><menuitem name='ChekUpdateAction' action='ChekUpdateAction'/><separator/><menuitem name='aboutAction' action='aboutAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.tdiMain = new global::QSTDI.TdiNotebook ();
		this.tdiMain.Name = "tdiMain";
		this.tdiMain.CurrentPage = 0;
		this.tdiMain.ShowTabs = false;
		this.vbox1.Add (this.tdiMain);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.tdiMain]));
		w3.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar1 = new global::Gtk.Statusbar ();
		this.statusbar1.Name = "statusbar1";
		this.statusbar1.Spacing = 6;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.labelStatus = new global::Gtk.Label ();
		this.labelStatus.Name = "labelStatus";
		this.labelStatus.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
		this.statusbar1.Add (this.labelStatus);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.statusbar1 [this.labelStatus]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		this.vbox1.Add (this.statusbar1);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbar1]));
		w5.Position = 2;
		w5.Expand = false;
		w5.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 799;
		this.DefaultHeight = 300;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.ActionPasswordChange.Activated += new global::System.EventHandler (this.OnActionPasswordChangeActivated);
		this.quitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
		this.UsersAction.Activated += new global::System.EventHandler (this.OnUsersActionActivated);
		this.aboutAction.Activated += new global::System.EventHandler (this.OnAboutActionActivated);
		this.ActionFittings.Activated += new global::System.EventHandler (this.OnActionFittingsActivated);
		this.ActionProvider.Activated += new global::System.EventHandler (this.OnActionProviderActivated);
		this.ActionFittingType.Activated += new global::System.EventHandler (this.OnActionFittingTypeActivated);
		this.ActionBodyMaterial.Activated += new global::System.EventHandler (this.OnActionBodyMaterialActivated);
		this.ActionConnectionType.Activated += new global::System.EventHandler (this.OnActionConnectionTypeActivated);
		this.ActionConductor.Activated += new global::System.EventHandler (this.OnActionConductorActivated);
		this.ActionPressure.Activated += new global::System.EventHandler (this.OnActionPressureActivated);
		this.ActionDiameter.Activated += new global::System.EventHandler (this.OnActionDiameterActivated);
		this.ActionProject.Activated += new global::System.EventHandler (this.OnActionProjectActivated);
		this.ActionPrice.Activated += new global::System.EventHandler (this.OnActionPriceActivated);
		this.ChekUpdateAction.Activated += new global::System.EventHandler (this.OnChekUpdateActionActivated);
		this.Action5.Activated += new global::System.EventHandler (this.OnActionUpdatePricesActivated);
		this.ActionChangelog.Activated += new global::System.EventHandler (this.OnActionChangelogActivated);
	}
}
