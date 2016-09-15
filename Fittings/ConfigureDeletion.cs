using System.Collections.Generic;
using QSOrmProject.Deletion;
using Fittings.Domain;


namespace Fittings
{
	partial class MainClass
	{
		public static void ConfigureDeletion ()
		{
			logger.Info ("Настройка параметров удаления...");

			DeleteConfig.AddHibernateDeleteInfo<AttachedFile>();

			DeleteConfig.AddHibernateDeleteInfo<BodyMaterial>()
				.AddDeleteDependence<Fitting>(x => x.BodyMaterial);
			
			DeleteConfig.AddHibernateDeleteInfo<Conductor>()
				.AddDeleteDependence<ProjectItem>(x => x.Conductor);
			
			DeleteConfig.AddHibernateDeleteInfo<ConnectionType>()
				.AddDeleteDependence<Fitting>(x => x.ConnectionType);
			
			DeleteConfig.AddHibernateDeleteInfo<Diameter>()
				.AddDeleteDependence<Fitting>(x => x.Diameter);
			
			DeleteConfig.AddHibernateDeleteInfo<Fitting>()
				.AddDeleteDependence<AttachedFile>(x => x.Fitting)
				.AddDeleteDependence<ProjectItem>(x => x.Fitting)
				.AddDeleteDependence<PriceItem>(x => x.Fitting);

			DeleteConfig.AddHibernateDeleteInfo<FittingType>()
				.AddDeleteDependence<Fitting>(x => x.Name);
			
			DeleteConfig.AddHibernateDeleteInfo<Pressure>()
				.AddDeleteDependence<Fitting>(x => x.Pressure);
			
			DeleteConfig.AddHibernateDeleteInfo<PriceItem>()
				.AddClearDependence<ProjectItem>(x => x.SelectedPriceItem);
			
			DeleteConfig.AddHibernateDeleteInfo<Price>()
				.AddDeleteDependence<PriceItem>(x => x.Price);
			
			DeleteConfig.AddHibernateDeleteInfo<ProjectItem>();

			DeleteConfig.AddHibernateDeleteInfo<Project>()
				.AddDeleteDependence<ProjectItem>(x => x.Project);
			
			DeleteConfig.AddHibernateDeleteInfo<Provider>()
				.AddDeleteDependence<Price>(x => x.Provider);
			
			DeleteConfig.AddHibernateDeleteInfo<User>();

			//Для тетирования
			#if DEBUG
			DeleteConfig.DeletionCheck ();
			#endif

			logger.Info ("Ок");
		}
	}
}
