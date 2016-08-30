using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "пользователи",
		Nominative = "пользователь")]
	public class AttachedFile: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		byte[] file;

		public virtual byte[] File {
			get { return file; }
			set { SetField (ref file, value, () => File); }
		}

		Fitting fitting;

		public virtual Fitting Fitting {
			get { return fitting; }
			set { SetField (ref fitting, value, () => Fitting); }
		}

		string fileName;

		public virtual string FileName {
			get { return fileName; }
			set { SetField (ref fileName, value, () => FileName); }
		}

		#endregion

		public AttachedFile ()
		{
			
		}
	}
}

