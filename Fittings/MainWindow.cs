using System;
using Fittings;
using Fittings.Domain;
using Fittings.ViewModel;
using Gtk;
using QSOrmProject;
using QSProjectsLib;
using QSSupportLib;
using QSTDI;
using QSUpdater;

public partial class MainWindow: Gtk.Window
{
	private static Gtk.Clipboard clipboard = Gtk.Clipboard.Get (Gdk.Atom.Intern ("CLIPBOARD", false));

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		this.KeyReleaseEvent += HandleKeyReleaseEvent;
		//Передаем лебл
		QSMain.StatusBarLabel = labelStatus;
		this.Title = MainSupport.GetTitle ();
		QSMain.MakeNewStatusTargetForNlog ();

		MainSupport.LoadBaseParameters ();

		MainUpdater.RunCheckVersion (true, true, true);
		QSMain.CheckServer (this); // Проверяем настройки сервера

		if (QSMain.User.Login == "root") {
			string Message = "Вы зашли в программу под администратором базы данных. У вас есть только возможность создавать других пользователей.";
			MessageDialog md = new MessageDialog (this, DialogFlags.DestroyWithParent,
				MessageType.Info, 
				ButtonsType.Ok,
				Message);
			md.Run ();
			md.Destroy ();
			Users WinUser = new Users ();
			WinUser.Show ();
			WinUser.Run ();
			WinUser.Destroy ();
			return;
		}

		UsersAction.Sensitive = QSMain.User.Admin;
	}

	#region Обходной путь для 

	void HandleKeyReleaseEvent (object o, KeyReleaseEventArgs args)
	{
		int platform = (int)Environment.OSVersion.Platform;
		int version = (int)Environment.OSVersion.Version.Major;
		Gdk.ModifierType modifier;

		//Kind of MacOSX
		if ((platform == 4 || platform == 6 || platform == 128) && version > 8)
			modifier = Gdk.ModifierType.MetaMask | Gdk.ModifierType.Mod1Mask;
		//Kind of Windows or Unix
		else
			modifier = Gdk.ModifierType.ControlMask;

		//CTRL+C	
		if ((args.Event.Key == Gdk.Key.Cyrillic_es || args.Event.Key == Gdk.Key.Cyrillic_ES) && args.Event.State.HasFlag(modifier)) {
			Widget w = (o as MainWindow).Focus;
			CopyToClipboard (w);
		}//CTRL+X
		else if ((args.Event.Key == Gdk.Key.Cyrillic_che || args.Event.Key == Gdk.Key.Cyrillic_CHE) && args.Event.State.HasFlag(modifier)) {
			Widget w = (o as MainWindow).Focus;
			CutToClipboard (w);
		}//CTRL+V
		else if ((args.Event.Key == Gdk.Key.Cyrillic_em || args.Event.Key == Gdk.Key.Cyrillic_EM) && args.Event.State.HasFlag(modifier)) {
			Widget w = (o as MainWindow).Focus;
			PasteFromClipboard (w);
		}//CTRL+S || CTRL+ENTER
		else if ((args.Event.Key == Gdk.Key.S
			|| args.Event.Key == Gdk.Key.s
			|| args.Event.Key == Gdk.Key.Cyrillic_yeru
			|| args.Event.Key == Gdk.Key.Cyrillic_YERU
			|| args.Event.Key == Gdk.Key.Return) && args.Event.State.HasFlag(modifier)) {
			var w = tdiMain.CurrentPageWidget;
			if (w is QSTDI.TabVBox) {
				var tab = (w as QSTDI.TabVBox).Tab;
				if (tab is QSTDI.TdiSliderTab) {
					var dialog = (tab as QSTDI.TdiSliderTab).ActiveDialog;
					dialog.SaveAndClose ();
				}
				if(tab is ITdiDialog)
				{
					(tab as ITdiDialog).SaveAndClose();
				}
			}
		}
	}

	void CopyToClipboard (Widget w)
	{
		int start, end;

		if (w is Editable)
			(w as Editable).CopyClipboard ();
		else if (w is TextView)
			(w as TextView).Buffer.CopyClipboard (clipboard);
		else if (w is Label) {
			(w as Label).GetSelectionBounds (out start, out end);
			if (start != end)
				clipboard.Text = (w as Label).Text.Substring (start, end - start);
		}
	}

	void CutToClipboard (Widget w)
	{
		int start, end;

		if (w is Editable)
			(w as Editable).CutClipboard ();
		else if (w is TextView)
			(w as TextView).Buffer.CutClipboard (clipboard, true);
		else if (w is Label) {
			(w as Label).GetSelectionBounds (out start, out end);
			if (start != end)
				clipboard.Text = (w as Label).Text.Substring (start, end - start);
		}
	}

	void PasteFromClipboard (Widget w)
	{
		if (w is Editable)
			(w as Editable).PasteClipboard ();
		else if (w is TextView)
			(w as TextView).Buffer.PasteClipboard (clipboard);
	}

	#endregion

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		if (tdiMain.CloseAllTabs ()) {
			a.RetVal = false;
			Application.Quit ();
		} else {
			a.RetVal = true;
		}
	}
		
	protected void OnActionPasswordChangeActivated (object sender, EventArgs e)
	{
		QSMain.User.ChangeUserPassword (this);
	}

	protected void OnUsersActionActivated (object sender, EventArgs e)
	{
		Users winUsers = new Users ();
		winUsers.Show ();
		winUsers.Run ();
		winUsers.Destroy ();
	}
		
	protected void OnQuitActionActivated (object sender, EventArgs e)
	{
		if (tdiMain.CloseAllTabs ()) {
			Application.Quit ();
		}
	}

	protected void OnAboutActionActivated (object sender, EventArgs e)
	{
		QSMain.RunAboutDialog ();
	}

	protected void OnActionProviderActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Provider>(),
			() => new OrmReference(typeof(Provider))
		);
	}

	protected void OnActionBodyMaterialActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<BodyMaterial>(),
			() => new OrmReference(typeof(BodyMaterial))
		);
	}

	protected void OnActionConductorActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Conductor>(),
			() => new OrmReference(typeof(Conductor))
		);
	}

	protected void OnActionConnectionTypeActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Fittings.Domain.ConnectionType>(),
			() => new OrmReference(typeof(Fittings.Domain.ConnectionType))
		);
	}

	protected void OnActionFittingTypeActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<FittingType>(),
			() => new OrmReference(typeof(FittingType))
		);
	}

	protected void OnActionPressureActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Pressure>(),
			() => new OrmReference(typeof(Pressure))
		);
	}

	protected void OnActionDiameterActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Diameter>(),
			() => new OrmReference(typeof(Diameter))
		);
	}

	protected void OnActionFittingsActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			ReferenceRepresentation.GenerateHashName<FittingsVM>(),
			() => new ReferenceRepresentation(new FittingsVM())
		);
	}

	protected void OnActionProjectActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Project>(),
			() => new OrmReference(typeof(Project))
		);
	}
	protected void OnActionPriceActivated (object sender, EventArgs e)
	{
		tdiMain.OpenTab(
			OrmReference.GenerateHashName<Price>(),
			() => new OrmReference(typeof(Price))
		);
	}

	protected void OnChekUpdateActionActivated(object sender, EventArgs e)
	{
		CheckUpdate.StartCheckUpdateThread (UpdaterFlags.ShowAnyway);
	}

	protected void OnActionUpdatePricesActivated(object sender, EventArgs e)
	{
		var dlg = new UpdatePricesDlg();
		tdiMain.AddTab(dlg);
	}

	protected void OnActionChangelogActivated(object sender, EventArgs e)
	{
		QSMain.RunChangeLogDlg (this);
	}
}
