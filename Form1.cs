using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DictConverter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			AddLexemes();
			//AddMetadata();
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				this.textBoxFile.Text = dlg.FileName;
			}

			if (String.IsNullOrEmpty(this.textBoxFile.Text))
			{
				this.AcceptButton = this.buttonBrowse;
			}
			else
			{
				this.AcceptButton = this.buttonConvert;
			}
		}

		private void buttonConvert_Click(object sender, EventArgs e)
		{
			AddLexemes();
			//AddMetadata();
			//AddContributors();
			//AddPos();
		}

		private int GetEthnicGroupIdFromString(string egString)
		{
			int id = 0;

			egString = egString.ToLower();

			if (egString.Contains("risino"))
			{
				id = 1;
			}
			else if (egString.Contains("dipo"))
			{
				id = 2;
			}
			else if (egString.Contains("zoriyo"))
			{
				id = 3;
			}
			else if (egString.Contains("dodimo"))
			{
				id = 4;
			}
			else if (egString.Contains("kula"))
			{
				id = 5;
			}
			else if (egString.Contains("cuhun"))
			{
				id = 6;
			}
			else if (egString.Contains("kumbasi"))
			{
				id = 7;
			}
			else if (egString.Contains("hausa"))
			{
				id = 8;
			}
			else if (egString == "va'di")
			{
				id = 10;
			}
			else if (egString == "lela")
			{
				id = 11;
			}
			else if (egString == "duka")
			{
				id = 12;
			}
			else if (egString == "fakai")
			{
				id = 13;
			}
			else if (egString == "jju")
			{
				id = 17;
			}
			else if (egString == "british")
			{
				id = 18;
			}
			else if (egString == "canadian")
			{
				id = 19;
			}
			else if (egString == "us")
			{
				id = 20;
			}
			else if (egString == "tarok")
			{
				id = 21;
			}
			else if (egString.Contains("cipu"))
			{
				//default to risino
				id = 1;
			}

			return id;
		}

		private List<int> GetLanguagesFromString(string lgString)
		{
			lgString = lgString.ToLower();

			List<int> languages = new List<int>();
			int id = 0;

			//tokenise by comma
			string[] lgs = lgString.Split(',');

			foreach (string lg in lgs)
			{
				id = 0;
				lg.Trim();
				if (!String.IsNullOrEmpty(lg))
				{
					if (lg.Contains("risino"))
					{
						id = 1;
					}
					else if (lg.Contains("dipo"))
					{
						id = 2;
					}
					else if (lg.Contains("zoriyo"))
					{
						id = 3;
					}
					else if (lg.Contains("dodimo"))
					{
						id = 4;
					}
					else if (lg.Contains("kula"))
					{
						id = 5;
					}
					else if (lg.Contains("cuhun"))
					{
						id = 6;
					}
					else if (lg.Contains("kumbasi"))
					{
						id = 7;
					}
					else if (lg.Contains("hausa"))
					{
						id = 8;
					}
					else if (lg == "va'di" || lg == "tsuvaɗi")
					{
						id = 10;
					}
					else if (lg == "lela")
					{
						id = 11;
					}
					else if (lg == "duka")
					{
						id = 12;
					}
					else if (lg == "fakai")
					{
						id = 13;
					}
					else if (lg == "fulfulde")
					{
						id = 14;
					}
					else if (lg == "yoruba")
					{
						id = 15;
					}
					else if (lg == "arabic")
					{
						id = 16;
					}
					else if (lg == "jju")
					{
						id = 17;
					}
					else if (lg == "british")
					{
						id = 18;
					}
					else if (lg == "canadian")
					{
						id = 19;
					}
					else if (lg == "us")
					{
						id = 20;
					}
					else if (lg == "tarok")
					{
						id = 21;
					}
					else if (lg == "english")
					{
						id = 18;
					}
					else if (lg == "damakawa")
					{
						id = 22;
					}
					else if (lg.Contains("cipu"))
					{
						//default to risino
						id = 1;
					}

					languages.Add(id);
				}
			}

			return languages;
		}

		private int CalcFluency(Contributor con, ContributorLanguage l)
		{
			if (con.IsCicipu() && l.IsCicipu())
			{
				return 1; //native
			}
			else if (con.IsCicipu() && l.IsHausa())
			{
				return 2; //fluent
			}
			else
			{
				return 3; //some
			}
		}

		private void AddContributors()
		{
			string fileName = this.textBoxFile.Text;

			XDocument xdoc = XDocument.Load(fileName);
			var idGroups = from idGroup in xdoc.Descendants("idGroup")
						   select idGroup;

			List<Contributor> cons = new List<Contributor>();

			foreach (XElement idGroup in idGroups)
			{
				Contributor con = new Contributor();

				con.ToolboxInitials = idGroup.Element("id").Value;
				con.Name = idGroup.Element("nm").Value;
				con.Birthplace = idGroup.SafeElement("bp").Value;
				con.ParentalDetails = idGroup.SafeElement("pd").Value;
				con.CurrentResidence = idGroup.SafeElement("rs").Value;
				con.TimeAtCurrentResidence = idGroup.SafeElement("tr").Value;
				con.ChildhoodResidence = idGroup.SafeElement("cr").Value;
				con.Comment = idGroup.SafeElement("cm").Value;
				con.LevelEducation = idGroup.SafeElement("le").Value;
				con.Occupation = idGroup.SafeElement("oc").Value;
				con.AccessRights = idGroup.SafeElement("rt").Value;

				//sex
				string sexString = idGroup.Element("sx").Value;
				int sex = 0;

				if (sexString == "Female")
				{
					sex = 2;
				}
				else
				{
					sex = 1;
				}

				con.SexId = sex;

				//ethnic group
				string egString = idGroup.Element("eg").Value;
				con.EthnicGroupId = GetEthnicGroupIdFromString(egString);

				if (!con.IsCicipu())
				{
					//for the moment, let's just added native speakers
					continue;
				}

				//languages
				string lgString = idGroup.Element("lg").Value;
				lgString = lgString.Replace("?", "");

				List<int> languages = GetLanguagesFromString(lgString);

				foreach (int langId in languages)
				{
					if (langId > 0)
					{
						ContributorLanguage l = new ContributorLanguage();
						l.LanguageId = langId;

						//approximate fluency
						l.FluencyId = CalcFluency(con, l);

						con.ContributorLanguages.Add(l);
					}
				}

				//dob
				string dob = idGroup.SafeElement("db").Value;
				dob = dob.Replace("?", "");
				dob = dob.Left(4);

				if (!String.IsNullOrEmpty(dob))
				{
					con.DOB = new DateTime(Int32.Parse(dob), 1, 1);
				}

				//image file
				string imageFile = idGroup.SafeElement("fnGroup").SafeElement("fn").Value;
				string imageComment = idGroup.SafeElement("fnGroup").SafeElement("np").Value;

				ContributorImage conImage = new ContributorImage();
				conImage.Comment = imageComment;

				Image image = new Image();
				image.Filename = imageFile;

				conImage.Image = image;

				con.ContributorImages.Add(conImage);

				cons.Add(con);
			}

			using (DictionaryEntities db = new DictionaryEntities())
			{
				foreach (Contributor con in cons)
				{
					db.Contributors.Add(con);
				}

				try
				{
					db.SaveChanges();
				}
				catch (DbEntityValidationException e)
				{
					foreach (var eve in e.EntityValidationErrors)
					{
						Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
							eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
								ve.PropertyName, ve.ErrorMessage);
						}
					}
					throw;
				}
			}
		}

		private void AddPos()
		{
			string fileName = this.textBoxFile.Text;

			XDocument xdoc = XDocument.Load(fileName);
			var idGroups = from idGroup in xdoc.Descendants("abGroup")
						   select idGroup;

			List<PartsOfSpeech> poses = new List<PartsOfSpeech>();

			foreach (XElement idGroup in idGroups)
			{
				PartsOfSpeech pos = new PartsOfSpeech();

				pos.Abbreviation = idGroup.Element("ab").Value;
				pos.Name = idGroup.Element("fn").Value;

				poses.Add(pos);
			}

			using (DictionaryEntities db = new DictionaryEntities())
			{
				foreach (PartsOfSpeech pos in poses)
				{
					db.PartsOfSpeeches.Add(pos);
				}

				try
				{
					db.SaveChanges();
				}
				catch (DbEntityValidationException e)
				{
					foreach (var eve in e.EntityValidationErrors)
					{
						Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
							eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
								ve.PropertyName, ve.ErrorMessage);
						}
					}
					throw;
				}
			}
		}

		private void AddMetadata()
		{
			//string fileName = this.textBoxFile.Text;
			string fileName = @"C:\Users\Stuart\Documents\Visual Studio 2015\Projects\Exports\Metadata.xml";

			XDocument xdoc = XDocument.Load(fileName);
			var idGroups = from idGroup in xdoc.Descendants("idGroup")
						   select idGroup;

			List<SpeechEvent> events = new List<SpeechEvent>();

			foreach (XElement idGroup in idGroups)
			{
				SpeechEvent sEvent = new SpeechEvent();

				sEvent.ToolboxCode = idGroup.Element("id").Value;
				sEvent.Title = idGroup.Element("ti").Value;

				string eventTypeString = idGroup.Element("ty").Value;
				sEvent.EventTypeId = CalcEventTypeId(eventTypeString);

				//language
				string lgString = "";

				XElement elem = idGroup.Element("lg");
				if (elem == null)
				{
					continue;
				}

				lgString = elem.Value;

				lgString = lgString.Replace("?", "");

				List<int> languages = GetLanguagesFromString(lgString);

				if (languages.Count == 0)
				{
					continue;
				}

				int languageId = languages[0];

				if (!IsCicipu(languageId))
				{
					continue;
				}

				sEvent.LanguageId = languageId;

				//get the contributor
				XElement conElem = idGroup.Element("con");

				if (conElem != null)
				{
					string contribString = conElem.Value;
					List<int> cons = ExtractContribs(contribString);

					foreach (int con in cons)
					{
						SpeechEventContributor contrib = new SpeechEventContributor();
						contrib.ContributorId = con;

						if (contrib.ContributorId == 0)
						{
							break;
						}

						sEvent.SpeechEventContributors.Add(contrib);
					}

				}
				else
				{
					System.Diagnostics.Debug.Write("");
				}

				events.Add(sEvent);
			}

			using (DictionaryEntities db = new DictionaryEntities())
			{


				foreach (SpeechEvent ev in events)
				{
					db.SpeechEvents.Add(ev);
				}

				//contributors



				try
				{
					db.SaveChanges();
				}
				catch (DbEntityValidationException e)
				{
					foreach (var eve in e.EntityValidationErrors)
					{
						Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
							eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
								ve.PropertyName, ve.ErrorMessage);
						}
					}
					throw;
				}
			}
		}

		private List<int> ExtractContribs(string contribString)
		{
			List<int> contribs = new List<int>();

			//tokenise
			string[] cons = contribString.Split(';');

			for (int index = 0; index < cons.Count(); index++)
			{
				string con = cons[index];
				con = con.Trim();

				using (DictionaryEntities db = new DictionaryEntities())
				{
					var contrib = db.Contributors
					.Where(c => c.ToolboxInitials == con)
					.FirstOrDefault();

					if (contrib != null)
					{
						contribs.Add(contrib.Id);
					}
				}
			}

			return contribs;
		}


		private int CalcEventTypeId(string eventType)
		{
			int id = 0;

			eventType = eventType.ToLower();

			if (eventType == "commentary")
			{
				id = 1;
			}
			else if (eventType == "discussion")
			{
				id = 2;
			}
			else if (eventType == "elicitation")
			{
				id = 3;
			}
			else if (eventType == "observed")
			{
				id = 4;
			}
			else if (eventType == "sms")
			{
				id = 5;
			}
			else if (eventType == "staged")
			{
				id = 6;
			}
			else if (eventType == "stimulation")
			{
				id = 7;
			}
			else if (eventType == "written")
			{
				id = 8;
			}
			else if (eventType == "reference")
			{
				id = 9;
			}
			else if (eventType == "translation")
			{
				id = 10;
			}
			else
			{
				throw new ApplicationException("Unexpected event type " + eventType);
			}

			return id;
		}

		public static bool IsCicipu(int languageId)
		{
			if (languageId <= 7)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool IsHausa(int languageId)
		{
			if (languageId == 8)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		

		private void AddLexemes()
		{
			//string fileName = this.textBoxFile.Text;
			string fileName = @"C:\Users\Stuart\Documents\Visual Studio 2015\Projects\Exports\Dictionary.xml";

			XDocument xdoc = XDocument.Load(fileName);
			var idGroups = from idGroup in xdoc.Descendants("lxGroup")
						   select idGroup;

			List<Lexeme> lexemes = new List<Lexeme>();

			foreach (XElement lxGroup in idGroups)
			{
				//lexeme is simple
				Lexeme lex = new Lexeme();
				lex.Lexeme1 = lxGroup.Element("lx").Value;

				XElement homElem = lxGroup.Element("hm");
				if (homElem != null)
				{
					lex.HomonymNumber = Int32.Parse(homElem.Value);
				}

				//always at least one lexeme entry - do the headword first
				LexemeEntry headWord = ConstructLexemeEntry(lex.Lexeme1, lxGroup, true);
				lex.LexemeEntries1.Add(headWord);

				headWord.TypeId = 1; //headword

				int entryOrder = 1;
				headWord.Order = entryOrder; //headword order is always 1

				//now add lexem entries for the sub-entries
				var seGroups = lxGroup.Elements("seGroup");
				foreach(XElement seGroup in seGroups)
				{
					LexemeEntry subEntry = ConstructLexemeEntry(lex.Lexeme1, seGroup, false);
					lex.LexemeEntries1.Add(subEntry);

					subEntry.TypeId = 2; //sub-entry
					subEntry.Order = ++entryOrder;
				}
				
				lexemes.Add(lex);

				//remove tone marks
				Utils.MakeOrthographic(lexemes);
			}
			
			using (DictionaryEntities db = new DictionaryEntities())
			{
				foreach (Lexeme lex in lexemes)
				{
					db.Lexemes.Add(lex);
				}
				
				using (var txn = db.Database.BeginTransaction())
				{
					try
					{
						//this will generate the lexeme IDs
						db.SaveChanges();

						//now we can do main entries
						foreach (Lexeme lex in lexemes)
						{
							foreach (LexemeEntry entry in lex.LexemeEntries1)
							{
								int mainEntryId = 0;

								//get the lexeme ID using the temp variable that was stored earlier
								if (!String.IsNullOrEmpty(entry.TempMainEntry))
								{
									mainEntryId = FindLexemeId(entry.TempMainEntry, db, true);
								}
								
								if (mainEntryId == 0)
								{
									//just skip
									continue;
								}

								entry.MainEntryId = mainEntryId;
							}
						}

						//save the main entries
						db.SaveChanges();

						//now we can do cross-references (requires main entries to be saved first)
						foreach (Lexeme lex in lexemes)
						{
							foreach (LexemeEntry entry in lex.LexemeEntries1)
							{
								foreach (Sens sense in entry.Senses)
								{
									int order = 1;
									foreach (string cf in sense.TempXReferences)
									{
										SenseXReference xref = new SenseXReference();
										xref.Order = order;

										//get the lexeme entry ID using the cf field
										int lexemeEntryId = FindLexemeEntryId(cf, db); 
										if (lexemeEntryId == 0)
										{
											//just skip
											continue;
										}

										xref.XReferenceId = lexemeEntryId;

										sense.SenseXReferences.Add(xref);

										order++;
									}
								}
							}
						}

						//debug help
						foreach (Lexeme lex in db.Lexemes)
						{
							foreach (LexemeEntry entry in lex.LexemeEntries1)
							{
								foreach (Sens sense in entry.Senses)
								{
									foreach(SenseXReference reference in sense.SenseXReferences)
									{
										

										int id = reference.XReferenceId;

										Debug.Assert(id != 0);

										//check id exists in DB
										var dummy = (from LexemeEntry le in db.LexemeEntries
													 where le.Id == id
													 select le).ToList();

										int count = dummy.Count();

										Debug.Assert(count == 1);
									}
								}
							}
						}

						//save the cross-references
						db.SaveChanges();

						txn.Commit();
					}
					catch (DbEntityValidationException e)
					{
						foreach (var eve in e.EntityValidationErrors)
						{
							Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
								eve.Entry.Entity.GetType().Name, eve.Entry.State);
							foreach (var ve in eve.ValidationErrors)
							{
								Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
									ve.PropertyName, ve.ErrorMessage);
							}
						}

						txn.Rollback();
						throw;
					}
					catch (Exception ex)
					{
						txn.Rollback();
						throw;
					}
				}
			}
		}

		private LexemeEntry ConstructLexemeEntry(string lexeme, XElement lxGroup, bool isHeadword)
		{
			LexemeEntry entry = new LexemeEntry();

			//field for citation depends on whether this is a headword or not
			if(isHeadword)
			{
				//if there is a citation, use it
				XElement citationElem = lxGroup.Element("lc");
				if(citationElem != null)
				{
					entry.Citation = citationElem.Value;
				}
				else
				{
					//if not, use the lx
					entry.Citation = lexeme;
				}
			}
			else
			{
				entry.Citation = lxGroup.Element("se").Value;
			}

			XElement phonElem = lxGroup.Element("ph");
			if(phonElem != null)
			{
				entry.Phonetic = phonElem.Value;
			}

			XElement toneElem = lxGroup.Element("tl");
			if (toneElem != null)
			{
				entry.TonePattern = toneElem.Value;
			}

			//loanword comment
			XElement bwElem = lxGroup.Element("bw");
			if (bwElem != null)
			{
				//web pages needs to handle the fi:
				entry.LoanwordComment = bwElem.Value;
			}

			//defer main entries (because we need to make sure everything is saved before we add these to the database
			XElement mnElem = lxGroup.Element("mn");
			if (mnElem != null)
			{
				entry.TempMainEntry = mnElem.Value;
			}

			XElement psGroup = lxGroup.Element("psGroup");

			//how many snGroup elements are there?
			var snGroups = from snGroup in psGroup.Descendants("snGroup")
						   select snGroup;

			if (snGroups.Count() == 0)
			{
				//the sense info is directly under psGroup
				Sens sense = CreateSense(psGroup, 1);
				entry.Senses.Add(sense);
			}
			else
			{
				int order = 1;

				foreach (XElement snGroup in snGroups)
				{
					//the sense info is directly under snGroup
					Sens sense = CreateSense(snGroup, order);

					entry.Senses.Add(sense);

					order++;
				}
			}

			//literally
			XElement elem = psGroup.Element("lt");
			if (elem != null)
			{
				entry.Literally = elem.Value;
			}

			//plural form
			elem = psGroup.Element("pl");
			if (elem != null)
			{
				entry.PluralForm = elem.Value;
			}

			//singular form
			elem = psGroup.Element("sg");
			if (elem != null)
			{
				entry.SingularForm = elem.Value;
			}

			//gender
			XElement pdlGroup = psGroup.Elements("pdlGroup").Where(x => x.Element("pdl").Value == "Gen").SingleOrDefault();
			if (pdlGroup != null)
			{
				elem = pdlGroup.Element("pdv");
				if (elem != null)
				{
					entry.Gender = elem.Value.Trim().Replace(".", "");
				}
			}

			//verbal comment
			pdlGroup = psGroup.Elements("pdlGroup").Where(x => (x.Element("pdl").Value == "Imperative")
			|| (x.Element("pdl").Value == "Pluractional")).SingleOrDefault();
			if (pdlGroup != null)
			{
				elem = pdlGroup.Element("pdv");
				if (elem != null)
				{
					//escape English text with with fi:
					entry.VerbalComment = String.Format("fi:{0}: {1}", pdlGroup.Element("pdl").Value, elem.Value);
				}
			}
			
			//part of speech
			elem = psGroup.Element("ps");
			entry.PartOfSpeechId = GetPartOfSpeechId(elem.Value);

			return entry;
		}

		private int GetPartOfSpeechId(string text)
		{
			using (DictionaryEntities db = new DictionaryEntities())
			{
				int posId = (from PartsOfSpeech pos in db.PartsOfSpeeches
							where pos.Abbreviation.ToLower() == text.ToLower()
							select pos.Id).Single();

				return posId;
			}
		}

		private int FindLexemeId(string text, DictionaryEntities db, bool excludeNonMainEntries)
		{
			int id = 0;
			bool mainEntryRequired = false;

			string lexeme = text;

			//in the case of homonyms, the lexeme text may end in a number - strip them out separately.
			string number = Regex.Match(lexeme, @"\d+").Value;

			int homonymNumber = 0;
			if (!String.IsNullOrEmpty(number))
			{
				//extract the homonym number and remove it from the lexeme
				homonymNumber = Int32.Parse(number);
				lexeme = text.Replace(number, "");
			}
			else
			{
				//look for the special text [m], which indicates we are supposed to jump to a main entry, not a minor one
				int pos = text.IndexOf("[m]");
				if(pos != -1)
				{
					mainEntryRequired = true;
					lexeme = text.Left(pos);
				}
			}

			if(homonymNumber > 0)
			{
				try
				{
					//the orderby is a special solution for 'ũ (there) which has a multi-POS entry (headword and subentry)
					var dummy = (from le in db.LexemeEntries
								 where le.Citation == lexeme
								 && (excludeNonMainEntries ?
								   le.MainEntryId == null //make sure the record we retrieve isn't just a link to a main entry
								   : true)
								 && le.Lexeme1.HomonymNumber == homonymNumber
								 orderby le.Lexeme1.Id, le.Order
								 select le).ToList();

					id = (from le in db.LexemeEntries
						  where le.Citation == lexeme
						  && (excludeNonMainEntries ?
							le.MainEntryId == null //make sure the record we retrieve isn't just a link to a main entry
							: true)
						  && le.Lexeme1.HomonymNumber == homonymNumber
						  orderby le.Lexeme1.Id, le.Order
						  select le.Lexeme1.Id).FirstOrDefault();
				}
				catch (InvalidOperationException ex)
				{
					string message = ex.Message;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
				}

				if (id == 0)
				{
					try
					{
						var dummy = (from l in db.Lexemes
								 where l.Lexeme1 == lexeme
								 && l.HomonymNumber == homonymNumber
								 && (excludeNonMainEntries ?
									   l.LexemeEntries1.Where(le => le.MainEntryId != null).Count() == 0 //make sure the record we retrieve isn't just a link to a main entry
									   : true)
								 select l).ToList();

						if (dummy.Count() == 0 || dummy.Count() > 1)
						{
							Debug.Assert(false);
						}

						id = (from l in db.Lexemes
							  where l.Lexeme1 == lexeme
							  && l.HomonymNumber == homonymNumber
							  && (excludeNonMainEntries ?
									l.LexemeEntries1.Where(le => le.MainEntryId != null).Count() == 0 //make sure the record we retrieve isn't just a link to a main entry
									: true)
							  select l.Id).Single();
					}
					catch (InvalidOperationException ex)
					{
						string message = ex.Message;
					}
					catch (Exception ex)
					{
						string message = ex.Message;
					}
				}
			}
			else
			{
				try
				{
					//debug help
					var dummy = (from le in db.LexemeEntries
									  where le.Citation == lexeme
									  && ((excludeNonMainEntries  || mainEntryRequired) ?
										le.MainEntryId == null //make sure the record we retrieve isn't just a link to a main entry
										: true)
									  select le).ToList();

					//check citation forms first
					id = (from le in db.LexemeEntries
						  where le.Citation == lexeme
						  && ((excludeNonMainEntries || mainEntryRequired) ?
							le.MainEntryId == null //make sure the record we retrieve isn't just a link to a main entry
							: true)
						  select le.Lexeme1.Id).SingleOrDefault();
				}
				catch(InvalidOperationException ex)
				{
					string message = ex.Message;
				}
				catch(Exception ex)
				{
					string message = ex.Message;
				}
				

				if(id == 0)
				{
					try
					{
						//debug help
						var dummy = (from l in db.Lexemes
						where l.Lexeme1 == lexeme
						&& (excludeNonMainEntries ?
							  l.LexemeEntries1.Where(le => le.MainEntryId != null).Count() == 0 //make sure the record we retrieve isn't just a link to a main entry
							  : true)
						select l).ToList();

						//more debug help
						var dummy1 = (from l in db.Lexemes
									 where l.Lexeme1 == lexeme
									 select l).ToList();

						var dummy2 = (from l in db.Lexemes
									 where l.Lexeme1 == lexeme
									 && l.LexemeEntries1.Where(le => le.MainEntryId != null).Count() == 0 //make sure the record we retrieve isn't just a link to a main entry 
									 select l).ToList();

						if (dummy.Count() == 0 || dummy.Count() > 1)
						{
							Debug.Assert(false);
						}

						//if not found, then check lexeme
						id = (from l in db.Lexemes
							  where l.Lexeme1 == lexeme
							  && (excludeNonMainEntries ?
									l.LexemeEntries1.Where(le => le.MainEntryId != null).Count() == 0 //make sure the record we retrieve isn't just a link to a main entry
									: true)
							  select l.Id).Single();
					}
					catch (InvalidOperationException ex)
					{
						string message = ex.Message;
					}
					catch (Exception ex)
					{
						string message = ex.Message;
					}
				}
			}

			Debug.Assert(id != 0);

			return id;
		}

		private int FindLexemeEntryId(string text, DictionaryEntities db)
		{
			int id = 0;
			bool mainEntryRequired = false;

			string lexeme = text;

			//in the case of homonyms, the lexeme text may end in a number - strip them out separately.
			string number = Regex.Match(lexeme, @"\d+").Value;

			int homonymNumber = 0;
			if (!String.IsNullOrEmpty(number))
			{
				//extract the homonym number and remove it from the lexeme
				homonymNumber = Int32.Parse(number);
				lexeme = text.Replace(number, "");
			}
			else
			{
				//look for the special text [m], which indicates we are supposed to jump to a main entry, not a minor one
				int pos = text.IndexOf("[m]");
				if (pos != -1)
				{
					mainEntryRequired = true;
					lexeme = text.Left(pos);
				}
			}

			if (homonymNumber > 0)
			{
				try
				{
					//the orderby is a special solution for 'ũ (there) which has a multi-POS entry (headword and subentry)
					var dummy = (from le in db.LexemeEntries
								 where le.Citation == lexeme
								 && le.Lexeme1.HomonymNumber == homonymNumber
								 orderby le.Id, le.Order
								 select le).ToList();

					id = (from le in db.LexemeEntries
						  where le.Citation == lexeme
						  && le.Lexeme1.HomonymNumber == homonymNumber
						  orderby le.Id, le.Order
						  select le.Id).FirstOrDefault();
				}
				catch (InvalidOperationException ex)
				{
					string message = ex.Message;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
				}

				if (id == 0)
				{
					try
					{
						var dummy = (from l in db.Lexemes
									 where l.Lexeme1 == lexeme
									 && l.HomonymNumber == homonymNumber
									 select l).ToList();

						if (dummy.Count() == 0 || dummy.Count() > 1)
						{
							Debug.Assert(false);
						}

						id = (from l in db.Lexemes
							  where l.Lexeme1 == lexeme
							  && l.HomonymNumber == homonymNumber
							  select l.LexemeEntries1.Where(le => le.TypeId == 1).FirstOrDefault().Id).Single();
					}
					catch (InvalidOperationException ex)
					{
						string message = ex.Message;
					}
					catch (Exception ex)
					{
						string message = ex.Message;
					}
				}
			}
			else
			{
				try
				{
					//debug help
					var dummy = (from le in db.LexemeEntries
								 where le.Citation == lexeme
								 && (mainEntryRequired ?
								   le.MainEntryId == null //make sure the record we retrieve isn't just a link to a main entry
								   : true)
								 select le).ToList();

					//check citation forms first
					id = (from le in db.LexemeEntries
						  where le.Citation == lexeme
						  && (mainEntryRequired ?
							le.MainEntryId == null //make sure the record we retrieve isn't just a link to a main entry
							: true)
						  select le.Id).SingleOrDefault();
				}
				catch (InvalidOperationException ex)
				{
					string message = ex.Message;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
				}


				if (id == 0)
				{
					try
					{
						//debug help
						var dummy = (from l in db.Lexemes
									 where l.Lexeme1 == lexeme
									 select l).ToList();

						if (dummy.Count() == 0 || dummy.Count() > 1)
						{
							Debug.Assert(false);
						}

						//if not found, then check lexeme
						id = (from l in db.Lexemes
							  where l.Lexeme1 == lexeme
							  select l.LexemeEntries1.Where(le => le.TypeId == 1).FirstOrDefault().Id).Single();
					}
					catch (InvalidOperationException ex)
					{
						string message = ex.Message;
					}
					catch (Exception ex)
					{
						string message = ex.Message;
					}
				}
			}

			Debug.Assert(id != 0);

			return id;
		}

		private Sens CreateSense(XElement parent, int order)
		{
			Sens sense = new Sens();
			sense.Order = order;

			//debug helpe
			XElement dummy1 = parent.Parent.Parent;

			XElement elem = parent.Element("ge");
			sense.EnglishGloss = elem.Value.Replace("_", " "); //remove underscores
			sense.EnglishGloss = sense.EnglishGloss.Replace(" ; ", ", "); //change semi-colon to column

			elem = parent.Element("gn");
			if (elem != null)
			{
				sense.NationalGloss = elem.Value.Replace("_", " "); //remove underscores
				sense.NationalGloss = sense.NationalGloss.Replace(" ; ", ", "); //change semi-colon to column
			}

			//set definition if there is one, if not use the gloss
			elem = parent.Element("de");
			if (elem != null)
			{
				sense.EnglishDefinition = elem.Value;
			}
			else
			{
				sense.EnglishDefinition = sense.EnglishGloss;
			}
			
			elem = parent.Element("dn");
			if (elem != null)
			{
				sense.NationalDefinition = elem.Value;
			}
			else
			{
				sense.NationalDefinition = sense.NationalGloss;
			}

			//set reversals if they exist
			elem = parent.Element("re");
			if (elem != null)
			{
				if(elem.Value != "*")
				{
					sense.EnglishReversal = elem.Value;
				}
			}
			else
			{
				sense.EnglishReversal = sense.EnglishGloss;
			}

			elem = parent.Element("rn");
			if (elem != null)
			{
				if (elem.Value != "*")
				{
					sense.NationalReversal = elem.Value;
				}
			}
			else
			{
				sense.NationalReversal = sense.NationalGloss;
			}

			elem = parent.Element("ee");
			if (elem != null)
			{
				sense.EncyclopaedicInfo = elem.Value;
			}

			elem = parent.Element("sc");
			if (elem != null)
			{
				sense.ScientificName = elem.Value;
			}

			elem = parent.Element("ue");
			if (elem != null)
			{
				sense.UsageComment = elem.Value;
			}

			//sense images
			var pictures = from pic in parent.Descendants("pc")
						   select pic;

			foreach (XElement picture in pictures)
			{
				Image image = new Image();
				image.Filename = picture.Value.ToLower().Replace(@"i:\", @"");

				SenseImage sImage = new SenseImage();
				sImage.Image = image;
				
				sense.SenseImages.Add(sImage);
			}

			//sense sources
			var sources = from source in parent.Descendants("so")
						   select source;

			foreach (XElement source in sources)
			{
				SenseSource sSource = new SenseSource();
				sSource.Code = ExtractSourceCode(source.Value);
					
				sense.SenseSources.Add(sSource);
			}

			//sense cross-refereneces - this is trickier because they may not exist yet
			var cfGroups = from cfGroup in parent.Descendants("cfGroup")
						  select cfGroup;
			
			//don't add the XReference element yet - but keep track at the sense level
			//we will find the id of the cross-reference later (once we've added everything to the DB)
			foreach (XElement cfGroup in cfGroups)
			{
				elem = cfGroup.Element("cf");
				sense.TempXReferences.Add(elem.Value);
			}

			//sense references
			var rfGroups = from rfGroup in parent.Descendants("rfGroup")
						   select rfGroup;

			int refOrder = 1;

			foreach (XElement rfGroup in rfGroups)
			{
				SenseReference sReference = new SenseReference();
				sReference.Order = refOrder;

				//add the rf code
				elem = rfGroup.Element("rf");
				sReference.TextReference = elem.Value;

				//try and work out the contributor from the reference
				int id = GetContributorIdFromReference(rfGroup.Element("rfcon"), ExtractSourceCode(sReference.TextReference));
				if(id != 0)
				{
					sReference.ContributorId = id;
				}
				
				//now add the vernacular example
				var xvGroups = from xvGroup in rfGroup.Descendants("xvGroup")
							   select xvGroup;

				//it's OK to have no xv - this just means a \rf which hasn't yet been processed. In that case
				//we don't add the example
				if (xvGroups.Count() > 0)
				{
					foreach (XElement xvGroup in xvGroups)
					{
						SenseExample example = new SenseExample();
						example.LanguageId = GetLanguageFromMetadata(ExtractSourceCode(sReference.TextReference));

						//set the text
						elem = xvGroup.Element("xv");
						example.Text = elem.Value;

						//debug help
						XElement dummy = xvGroup.Parent.Parent.Parent.Parent;

						//and the translation
						elem = xvGroup.Element("xe");
						sReference.EnglishTranslation = elem.Value;

						//and the sound file
						elem = xvGroup.Element("sv");
						if (elem != null)
						{
							string fileName = "";
							decimal start = 0.000m;
							decimal end = 0.000m;

							ParseSoundFileText(elem.Value, ref fileName, ref start, ref end);

							example.SoundFile = fileName;

							example.SoundFileStart = start;
							example.SoundFileEnd = end;
						}

						sReference.SenseExamples.Add(example);
					}

					//and the Hausa translation
					var xnGroups = from xnGroup in rfGroup.Descendants("xnGroup")
								   select xnGroup;

					foreach (XElement xnGroup in xnGroups)
					{
						SenseExample example = new SenseExample();
						example.LanguageId = 8; //Hausa

						//get the Hausa translation
						elem = xnGroup.Element("xn");
						example.Text = elem.Value;

						//and the sound file
						elem = xnGroup.Element("sh");
						if (elem != null)
						{
							string fileName = "";
							decimal start = 0.000m;
							decimal end = 0.000m;

							ParseSoundFileText(elem.Value, ref fileName, ref start, ref end);

							example.SoundFile = fileName;

							example.SoundFileStart = start;
							example.SoundFileEnd = end;
						}

						sReference.SenseExamples.Add(example);
					}

					//now add the object
					sense.SenseReferences.Add(sReference);

					refOrder++;
				}
			}

			return sense;
		}

		private int GetContributorIdFromReference(XElement rfconElem, string textReference)
		{
			int id = 0;

			Debug.Assert(!String.IsNullOrEmpty(textReference));

			//check for special rfcon field
			string rfcon = "";
			if(rfconElem != null)
			{
				rfcon = rfconElem.Value;
			}

			if (IsSourceDate(textReference))
			{
				//OK, just don't have a contributor ID
			}
			else if(textReference != "misc")
			{
				using (DictionaryEntities db = new DictionaryEntities())
				{
					if(String.IsNullOrEmpty(rfcon))
					{
						//get the contributor id from the metadata
						List<SpeechEventContributor> cons = (from SpeechEvent s in db.SpeechEvents
									where s.ToolboxCode == textReference
									select s.SpeechEventContributors).Single().ToList();
						
						if(cons.Count() > 0)
						{
							id = cons[0].ContributorId;
						}
					}
					else
					{
						//get the contributor id directly
						id = (from Contributor c in db.Contributors
									where c.ToolboxInitials == rfcon
								select c.Id).Single();
					}
				}
			}

			return id;
		}

		private void ParseSoundFileText(string text, ref string fileName, ref decimal start, ref decimal end)
		{
			//find .wav
			const string WavExtension = ".wav";
			const string Mp3Extension = ".mp3";

			int pos = text.IndexOf(WavExtension);
			if(pos != -1)
			{
				//move past the .wav
				pos = pos + 1 + WavExtension.Length;

				//file name is everything to the left
				fileName = text.Left(pos);

				//trim and replace .wav with .mp3
				fileName.Trim();
				fileName = fileName.Replace(WavExtension, Mp3Extension);

				string remainder = text.Right(text.Length - pos);
				remainder.Trim();

				if(remainder.Length > 0)
				{
					string[] tokens = remainder.Split(' ');

					Debug.Assert(tokens.Length == 2);

					start = Decimal.Parse(tokens[0]);
					end = Decimal.Parse(tokens[1]);
				}
			}
		}

		private bool IsSourceDate(string source)
		{
			bool isDate = false;

			DateTime result = new DateTime();
			if (DateTime.TryParse(source, out result))
			{
				isDate = true;
			}

			return isDate;
		}

		private int GetLanguageFromMetadata(string source)
		{
			int languageId = 0;

			//the source may be a fieldnote date, in which case we assume Cicipu
			if (IsSourceDate(source))
			{
				//it's a field note
				languageId = 1; //Cicipu
			}
			else
			{
				using (DictionaryEntities db = new DictionaryEntities())
				{
					languageId = (from e in db.SpeechEvents
								  where e.ToolboxCode == source
								  select e.LanguageId).Single();
				}
			}
			
			return languageId;
		}

		private string ExtractSourceCode(string source)
		{
			string code = "";

			//find the portion to the left of the first . (if there is one)
			int index = source.IndexOf('.');

			if(index == -1)
			{
				code = source;
			}
			else
			{
				code = source.Left(index);
			}

			return code;
		}

	}
}
