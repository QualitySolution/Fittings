using System.Collections.Generic;
using QSOrmProject.Deletion;


namespace Fittings
{
	partial class MainClass
	{
		public static void ConfigureDeletion ()
		{
			logger.Info ("Настройка параметров удаления...");



			//Для тетирования
			#if DEBUG


			DeleteConfig.DeletionCheck ();
			#endif

			logger.Info ("Ок");
		}
	}
}
