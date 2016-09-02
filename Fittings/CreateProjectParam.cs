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
				OrmObjectMapping<Conductor>.Create().Dialog<ConductorDlg>().DefaultTableView().SearchColumn("Русское название", x => x.NameRus).SearchColumn("Английское название", x => x.NameEng).End(),
				OrmObjectMapping<Fittings.Domain.ConnectionType>.Create().Dialog<ConnectionTypeDlg>().DefaultTableView().SearchColumn("Русское название", x => x.NameRus).SearchColumn("Английское название", x => x.NameEng).End(),
				OrmObjectMapping<FittingType>.Create().Dialog<FittingTypeDlg>().DefaultTableView().SearchColumn("Русское название", x => x.NameRus).SearchColumn("Английское название", x => x.NameEng).End(),
				OrmObjectMapping<Pressure>.Create().Dialog<PressureDlg>().DefaultTableView().SearchColumn("PN", x => x.Pn).SearchColumn("class", x => x.Pclass).End(),
				OrmObjectMapping<Diameter>.Create().Dialog<DiameterDlg>().DefaultTableView().SearchColumn("Дюймы", x => x.Inch).SearchColumn("Миллиметры", x => x.Mm).End(),
				OrmObjectMapping<Fitting>.Create().Dialog<FittingDlg>().DefaultTableView().SearchColumn("Код", x => x.Id.ToString()).SearchColumn("Артикул", x => x.Code).End(),
				OrmObjectMapping<Project>.Create().Dialog<ProjectDlg>().DefaultTableView().SearchColumn("Заказчик", x => x.Customer).SearchColumn("Название проекта", x => x.ProjectName).End(),
				OrmObjectMapping<Price>.Create().Dialog<PriceDlg>().DefaultTableView().SearchColumn("Дата", x => x.Date.ToString()).SearchColumn("Комментарий", x => x.Comment).End(),
			};

		}
	}
}
