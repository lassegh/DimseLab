using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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


        //her var lars
        //alt andet lige, så undskylder han for den følgende WriteProjectData
        public static async Task WriteProjectData<T>(T objectToSave)
        {
            // stores an object in XML format in file called 'filename'
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(@"C:\Dimselab");
            StorageFile file = await folder.CreateFileAsync("ProjectData.xml", CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();

            using (stream)
            {
                serializer.Serialize(stream, objectToSave);
            }
        }





        // Her læser vi hele vores "database / Dummydata" af projekter fra, som bliver stoppet i en ny liste
        // som bliver brugt i både "Browse" og i "MyProjects". Men "MyProjects" bliver filtreret og sat i en ny liste
        public List<Project> ReadProjectData()
        {
            var projects = new List<Project>();



            // Project 1
            var project1 = new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture));

            project1.ProjectMembers.Add(new User("Lars", "Truelsen", 32324567, "Lars@easj.dk".ToLower()));

            projects.Add(project1);




            // Project 2
            var project2 = new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture));

            project2.ProjectMembers.Add(new User("Ole", "Olsen", 32324567, "Ole@easj.dk".ToLower()));

            projects.Add(project2);




            // Project 1
            var project3 = new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture));

            project3.ProjectMembers.Add(new User("Karsten", "Karlsen", 32324567, "Karsten@easj.dk".ToLower()));

            projects.Add(project3);



            // Returns the new filtered project to the one that calls it.
            return projects;
        }

        public void WriteDoohickeyData()
        {

        }

        public void ReadDoohickeyData()
        {

        }

        public void WriteUserData()
        {

        }

        public void ReadUserData()
        {

        }

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
