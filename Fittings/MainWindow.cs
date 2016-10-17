using System;
using Fittings.Domain;
using Fittings.ViewModel;
using Gtk;
using QSOrmProject;
using QSProjectsLib;
using QSSupportLib;
using QSUpdater;
using Fittings;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
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

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
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
		Application.Quit ();
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
