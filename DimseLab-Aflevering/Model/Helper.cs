using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;

namespace DimseLab_Aflevering.Model
{
    class Helper
    {

        List<Project> _projectList = new List<Project>();

        List<Doohickey> _doohickeyList = new List<Doohickey>();

        List<User> _userList = new List<User>();

        public User CurrentUser { get; set; }

        public Helper()
        {
            CurrentUser = new User("Lars", "Truelsen", 4612456, "lars@easj.dk");


        }

        #region readingNwriting


        public async void SaveEverything()
        {
            await WriteProjectData();
            await WriteDoohickeyData();
            await WriteUserData();
        }

        /*
        public async void LoadEverything()
        {
            await ReadProjectData();
            await ReadDoohickeyData();
            await ReadUserData();
        }
        */


        //Denne kode er stjålet direkte fra Ebbe Vang. Fuck tha police!
        public static async Task SaveObjectToXml<T>(List<T> inputList, string filename)
        {
            // stores an object in XML format in file called 'filename'
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(@"C:/Dimselab");
            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();

            foreach (T ITEM in inputList)
            {
                using (stream)
                {
                    serializer.Serialize(stream, ITEM);
                }
            }

        }



        public async Task WriteProjectData()
        {
            await SaveObjectToXml(ProjectList, "ProjectData.xml");
        }


        // Her læser vi hele vores "database / Dummydata" af projekter fra, som bliver stoppet i en ny liste
        // som bliver brugt i både "Browse" og i "MyProjects". Men "MyProjects" bliver filtreret og sat i en ny liste
        public List<Project> ReadProjectData()
        {
            List<Project> projects = new List<Project>();



            // Project 1
            Project project1 = new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),1);

            project1.ProjectMembers.Add(new User("Lars", "Truelsen", 32324567, "Lars@easj.dk".ToLower()));

            projects.Add(project1);




            // Project 2
            var project2 = new Project("Bumse presseren", "We are developing a intelligent robotic arm", DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),2);

            project2.ProjectMembers.Add(new User("Lars", "Truelsen", 32324567, "Lars@easj.dk".ToLower()));

            projects.Add(project2);




            // Project 1
            var project3 = new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),3);

            project3.ProjectMembers.Add(new User("Karsten", "Karlsen", 32324567, "Karsten@easj.dk".ToLower()));

            projects.Add(project3);



            // Returns the new filtered project to the one that calls it.
            return projects;
        }

        public async Task WriteDoohickeyData()
        {
            await SaveObjectToXml(DoohickeyList, "DoohickeyData.xml");
        }

        public void ReadDoohickeyData()
        {

        }

        public async Task WriteUserData()
        {
            await SaveObjectToXml(UserList, "UserData.xml");
        }

        public void ReadUserData()
        {

        }

        #endregion




        public List<User> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }

        public List<Doohickey> DoohickeyList
        {
            get { return _doohickeyList; }
            set { _doohickeyList = value; }
        }

        public List<Project> ProjectList
        {
            get { return _projectList; }
            set { _projectList = value; }
        }
    }
}
