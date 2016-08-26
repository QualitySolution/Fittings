using System;
using System.Collections.Generic;
//using Gamma.Binding;
//using Gamma.Utilities;
using QSOrmProject;
using QSOrmProject.DomainMapping;
using QSProjectsLib;

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
	/*		OrmMain.ConfigureOrm (QSMain.ConnectionString, new System.Reflection.Assembly[] {
				System.Reflection.Assembly.GetAssembly (typeof(Vodovoz.HMap.OrganizationMap)),
				System.Reflection.Assembly.GetAssembly (typeof(QSBanks.QSBanksMain)),
				System.Reflection.Assembly.GetAssembly (typeof(QSContacts.QSContactsMain))
			});
			OrmMain.ClassMappingList = new List<IOrmObjectMapping> {
				//Простые справочники
				OrmObjectMapping<CullingCategory>.Create().DefaultTableView().SearchColumn("Название", x => x.Name).End(),

			};
			OrmMain.ClassMappingList.AddRange (QSBanks.QSBanksMain.GetModuleMaping ());
			OrmMain.ClassMappingList.AddRange (QSContactsMain.GetModuleMaping ());

			//Настройка ParentReference
			ParentReferenceConfig.AddActions (new ParentReferenceActions<Organization, QSBanks.Account> {
				AddNewChild = (o, a) => o.AddAccount (a)
			});
			ParentReferenceConfig.AddActions (new ParentReferenceActions<Counterparty, QSBanks.Account> {
				AddNewChild = (c, a) => c.AddAccount (a)
			});
			ParentReferenceConfig.AddActions (new ParentReferenceActions<Employee, QSBanks.Account> {
				AddNewChild = (c, a) => c.AddAccount (a)
			}); */
		}
	}
}
