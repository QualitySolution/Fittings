using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class ProjectMap : ClassMap<Project>
	{
		public ProjectMap ()
		{
			Table ("project");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.Customer).Column ("customer");
			Map (x => x.ProjectName).Column ("project_name");

			HasMany(x => x.Projects).KeyColumn("project_id").Inverse().Cascade.AllDeleteOrphan().LazyLoad();
		}
	}
}