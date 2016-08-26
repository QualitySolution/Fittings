using System;
using Gtk;
using QSProjectsLib;

namespace Fittings
{
	partial class MainClass
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();
		public static MainWindow MainWin;

		public static void Main (string[] args)
		{
			Application.Init ();
			QSMain.SubscribeToUnhadledExceptions ();
			QSMain.GuiThread = System.Threading.Thread.CurrentThread;
			CreateProjectParam ();
			// Создаем окно входа
			Login LoginDialog = new Login ();
			LoginDialog.Logo = Gdk.Pixbuf.LoadFromResource ("Fittings.icons.flow_logo.png");
			LoginDialog.SetDefaultNames ("Fittings");
			LoginDialog.DefaultLogin = "user";
			LoginDialog.DefaultServer = "localhost";
			LoginDialog.DemoServer = "demo.qsolution.ru";
			LoginDialog.DemoMessage = "Для подключения к демострационному серверу используйте следующие настройки:\n" +
				"\n" +
				"<b>Сервер:</b> demo.qsolution.ru\n" +
				"<b>Пользователь:</b> demo\n" +
				"<b>Пароль:</b> demo\n" +
				"\n" +
				"Для установки собственного сервера обратитесь к документации.";
			LoginDialog.UpdateFromGConf ();

			ResponseType LoginResult;
			LoginResult = (ResponseType)LoginDialog.Run ();
			if (LoginResult == ResponseType.DeleteEvent || LoginResult == ResponseType.Cancel)
				return;

			LoginDialog.Destroy ();

			//Настройка базы
			CreateBaseConfig ();

			//Настрока удаления
			ConfigureDeletion ();

			//Запускаем программу
			MainWin = new MainWindow ();
			QSMain.ErrorDlgParrent = MainWin;
			if (QSMain.User.Login == "root")
				return;
			MainWin.Show ();
			Application.Run ();
		}
	}
}
