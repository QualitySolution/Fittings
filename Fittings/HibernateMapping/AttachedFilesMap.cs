using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class AttachedFilesMap : ClassMap<AttachedFile>
	{
		public AttachedFilesMap ()
		{
			Table ("files");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.File).Column ("file");
			References (x => x.Fitting).Column ("fitting_id");
			Map (x => x.FileName).Column ("file_name");
		}
	}
}