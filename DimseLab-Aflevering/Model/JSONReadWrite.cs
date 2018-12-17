using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using DimseLab_Aflevering.Model;
using Newtonsoft.Json;

namespace DimseLab_Aflevering
{
    class JsonReadWrite
    {

        private static string ProjectFileName = "ProjectModel.json";
        private static string UserFileName = "UserModel.json";
        private static string DoohickeyFileName = "DoohickeyModel.json";



        /// <summary>
        /// Gemmer projektets data
        /// </summary>
        /// <param name="notes"></param>
        public static async void SaveProjectsAsJsonAsync(ObservableCollection<Project> notes)
        {
            string notesJsonString = JsonConvert.SerializeObject(notes);
            SerializeNotesFileAsync(notesJsonString, ProjectFileName);
        }

        public static async void SaveUsersAsJsonAsync(ObservableCollection<User> notes)
        {
            string notesJsonString = JsonConvert.SerializeObject(notes);
            SerializeNotesFileAsync(notesJsonString, UserFileName);
        }

        public static async void SaveDoohickeyAsJsonAsync(ObservableCollection<Doohickey> notes)
        {
            string notesJsonString = JsonConvert.SerializeObject(notes);
            SerializeNotesFileAsync(notesJsonString, DoohickeyFileName);
        }

        public static async Task<bool> IsFilePresent(string fileName)
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            return item != null;
        }


        /// <summary>
        /// Loader projektets data
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<Project>> LoadProjectsFromJsonAsync()
        {
            Debug.WriteLine("LoadNotesFromJsonAsync");

            if (await IsFilePresent(ProjectFileName))
            {
                try
                {
                    string notesJsonString = await DeserializeNotesFileAsync(ProjectFileName);
                    return (ObservableCollection<Project>)JsonConvert.DeserializeObject(notesJsonString, typeof(ObservableCollection<Project>));


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return new ObservableCollection<Project>();
            }
            else
            {
                return HardcodedProjects();
            }

        }

        public static async Task<ObservableCollection<User>> LoadUsersFromJsonAsync()
        {
            Debug.WriteLine("LoadNotesFromJsonAsync");

            if (await IsFilePresent(UserFileName))
            {
                try
                {
                    string notesJsonString = await DeserializeNotesFileAsync(UserFileName);
                    return (ObservableCollection<User>) JsonConvert.DeserializeObject(notesJsonString,
                        typeof(ObservableCollection<User>));


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new ObservableCollection<User>();
            }
            else
            {
               return HardcodedUsers();
            }

            return null;

        }

        public static async Task<ObservableCollection<Doohickey>> LoadDoohickeysFromJsonAsync()
        {
            Debug.WriteLine("LoadNotesFromJsonAsync");

            if (await IsFilePresent(DoohickeyFileName))
            {
                try
                {
                    string notesJsonString = await DeserializeNotesFileAsync(DoohickeyFileName);
                    return (ObservableCollection<Doohickey>) JsonConvert.DeserializeObject(notesJsonString,
                        typeof(ObservableCollection<Doohickey>));


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new ObservableCollection<Doohickey>();
            }
            else
            {
               return HardcodedDoohickeys();
            }

            return null;
        }

        // motode der kaldes i SaveNotesAsJsonAsync()
        private static async void SerializeNotesFileAsync(string notesJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, notesJsonString);
        }

        // metode der kaldes i LoadNotesFromJsonAsync()
        private static async Task<string> DeserializeNotesFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException e)
            {
                MessageDialogHelper.Show("Try save some data.", "Nothing loaded.");
                return null;
            }
        }

        /// <summary>
        /// message helper der giver besked første gang du loader programmet
        /// </summary>
        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }

        // Laver brugere hardcoded
        private static ObservableCollection<User> HardcodedUsers()
        {
            ObservableCollection<User> uList = new ObservableCollection<User>();
            uList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk", "1234"));
            uList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk", "1234"));
            uList.Add(new User("Michael", "Kjergaard", 46375817, "Michael@easj.dk", "1234"));
            uList.Add(new User("Lasse", "Grønbech", 46375817, "Lasse@easj.dk", "1234"));
            uList.Add(new User("André", "Horsten", 46375817, "Andre@easj.dk", "1234"));
            uList.Add(new User("Some", "Teacher", 20123456, "someTeacher@easj.dk", "dimseLab"));

            return uList;
        }

        // Laver projekter hardcoded
        private static ObservableCollection<Project> HardcodedProjects()
        {
            ObservableCollection<Project> pList = new ObservableCollection<Project>();
            Project project1 = new Project("Robotic Arm",
                "Vi vil udvikle en robotarm, der selv kan videreudvikle dette program", new DateTime(2018, 12, 31), 0);
            project1.ProjectMembers.Add(new User("Michael", "Kjergaard", 46375817, "Michael@easj.dk", "1234"));
            project1.ProjectBeginDate = new DateTime(2018, 12, 01);
            pList.Add(project1);

            Project project2 = new Project("Ny Computer", "Se min nye computer, hvor er den smart. Min er bare ikke stor og klodset som andres.", new DateTime(2018, 12, 9), 1);
            project2.ProjectMembers.Add(new User("Some", "Teacher", 20123456, "someTeacher@easj.dk", "dimseLab"));
            project2.ProjectBeginDate = new DateTime(2018, 11, 21);
            pList.Add(project2);

            Project project3 = new Project("Klap on, Klap off (Afsluttet)", "Med en raspberry pi vil vi kunne taende og slukke lyset med klappelyde.", new DateTime(2016, 01, 01), 2);
            project3.ProjectMembers.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk", "1234"));
            project3.IsFinished = true;
            project3.ProjectBeginDate = new DateTime(2015, 12, 01);
            pList.Add(project3);

            Project project4 = new Project("Sproejt mig i ansigtet", "Hold mig vaagen med Hvid Monster, hvis jeg falder i soevn", new DateTime(2018, 12, 19), 3);
            project4.ProjectMembers.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk", "1234"));
            project4.ProjectMembers.Add(new User("Michael", "Kjergaard", 46375817, "Michael@easj.dk", "1234"));
            project4.ProjectBeginDate = new DateTime(2018, 12, 01);
            pList.Add(project4);

            return pList;
        }

        // Hardcoded dimser
        private static ObservableCollection<Doohickey> HardcodedDoohickeys()
        {
            ObservableCollection<Doohickey> dList = new ObservableCollection<Doohickey>();

            dList.Add(new Doohickey("Raspberry Pi", 1));
            dList.Add(new Doohickey("Dildo", 2));
            dList.Add(new Doohickey("Webcam", 3));
            dList.Add(new Doohickey("Desert Eagle", 4));
            dList.Add(new Doohickey("9mm Laser", 5));
            dList.Add(new Doohickey("Billede af Lars", 6));
            dList.Add(new Doohickey("Manuel regulator", 7));
            dList.Add(new Doohickey("Harboe SportsBrus", 8));
            dList.Add(new Doohickey("Fiber Cable", 9));
            dList.Add(new Doohickey("Robot Arm", 10));
            dList.Add(new Doohickey("Drejebænk", 11));

            return dList;
        }

    }
}
