using System;
using QSOrmProject;

namespace Fittings.Domain
{
	public abstract class MultiLanguageReferenceBase: PropertyChangedBase, IDomainObject, IMultiLanguageReference
	{
		public virtual int Id { get; set; }

		string name_rus;

		public virtual string NameRus {
			get { return name_rus; }
			set { SetField (ref name_rus, value, () => NameRus); }
		}

		string name_eng;

		public virtual string NameEng {
			get { return name_eng; }
			set { SetField (ref name_eng, value, () => NameEng); }
		}


		public MultiLanguageReferenceBase ()
		{
		}
	}
}

