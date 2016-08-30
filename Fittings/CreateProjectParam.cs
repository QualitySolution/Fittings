using System;
using System.Collections.Generic;
//using Gamma.Binding;
//using Gamma.Utilities;
using QSOrmProject;
using QSOrmProject.DomainMapping;
using QSProjectsLib;
using Fittings.Domain;

namespace Fittings
{
	partial class MainClass
	{
		static void CreateProjectParam ()
		{
			QSMain.ProjectPermission = new Dictionary<string, UserPermission> ();
		}

		static void CreateBaseConfig ()
		{
			logger.Info ("Настройка параметров базы...");

			// Настройка ORM
			var db = FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
				.ConnectionString (QSMain.ConnectionString)
				.ShowSql ()
				.FormatSql ();

			OrmMain.ConfigureOrm (db, new System.Reflection.Assembly[] {
				System.Reflection.Assembly.GetAssembly (typeof(MainClass)),
			});
			OrmMain.ClassMappingList = new List<IOrmObjectMapping> {
				//Простые справочники
				OrmObjectMapping<User>.Create().DefaultTableView().SearchColumn("Название", x => x.Name).End(),
				OrmObjectMapping<Provider>.Create().DefaultTableView().SearchColumn("Название", x => x.Name).End(),
				OrmObjectMapping<BodyMaterial>.Create().Dialog<BodyMaterialDlg>().DefaultTableView().SearchColumn("Русское название", x => x.NameRus).SearchColumn("Английское название", x => x.NameEng).End(),

			};

		}
	}
}
