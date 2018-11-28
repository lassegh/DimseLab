using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DimseLab_Aflevering
{
    public class RegexSearch
    {

        /*
        ██▓    ▄▄▄       ██▀███    ██████     ██▒   █▓ ▄▄▄       ██▀███      ██░ ██ ▓█████  ██▀███  
        ▓██▒   ▒████▄    ▓██ ▒ ██▒▒██    ▒    ▓██░   █▒▒████▄    ▓██ ▒ ██▒   ▓██░ ██▒▓█   ▀ ▓██ ▒ ██▒
        ▒██░   ▒██  ▀█▄  ▓██ ░▄█ ▒░ ▓██▄       ▓██  █▒░▒██  ▀█▄  ▓██ ░▄█ ▒   ▒██▀▀██░▒███   ▓██ ░▄█ ▒
        ▒██░   ░██▄▄▄▄██ ▒██▀▀█▄    ▒   ██▒     ▒██ █░░░██▄▄▄▄██ ▒██▀▀█▄     ░▓█ ░██ ▒▓█  ▄ ▒██▀▀█▄  
        ░██████▒▓█   ▓██▒░██▓ ▒██▒▒██████▒▒      ▒▀█░   ▓█   ▓██▒░██▓ ▒██▒   ░▓█▒░██▓░▒████▒░██▓ ▒██▒
        ░ ▒░▓  ░▒▒   ▓▒█░░ ▒▓ ░▒▓░▒ ▒▓▒ ▒ ░      ░ ▐░   ▒▒   ▓▒█░░ ▒▓ ░▒▓░    ▒ ░░▒░▒░░ ▒░ ░░ ▒▓ ░▒▓░
        ░ ░ ▒  ░ ▒   ▒▒ ░  ░▒ ░ ▒░░ ░▒  ░ ░      ░ ░░    ▒   ▒▒ ░  ░▒ ░ ▒░    ▒ ░▒░ ░ ░ ░  ░  ░▒ ░ ▒░
        ░ ░    ░   ▒     ░░   ░ ░  ░  ░          ░░    ░   ▒     ░░   ░     ░  ░░ ░   ░     ░░   ░ 
        ░  ░     ░  ░   ░           ░           ░        ░  ░   ░         ░  ░  ░   ░  ░   ░     
        ░                                                 

            Hvasså kællinger???????

            Her er der lige en gratis gave fra Lars til jer numsehuller der koder! Jeg ved at vi skal søge på noget shit senere, så jeg har brudt sprintens etik og låner jeg hermed noget kode jeg alligevel havde liggende fra tidligere
            Den er gratis og kommenteret, motherfuckers



            "Know what I'm saying?" J-Roc


        */






            //Erstat HEJHEJHEJ med hvad end object du har en liste af
        public List<HEJHEJHEJ> RXsearch(string SearchTerm, List<HEJHEJHEJ> inputList)
        {
            List<HEJHEJHEJ> FilteredList = new List<HEJHEJHEJ>();


            foreach (HEJHEJHEJ ITEM in inputList)
            {

                string pattern = SearchTerm; //det pattern vi søger efter
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled; //en option der gør at vi ignorer forskellen på store og små bogstaver og noget andet gøjl
                string text = ITEM; //den tekst vi søger i
                Regex optionRegex = new Regex(pattern, options); //tager pattern og options og laver et Regex object der gør det faktiske arbejde

                //her sker magien
                if (optionRegex.IsMatch(text))
                {
                    FilteredList.Add(ITEM); 
                }
                

            }

            //returerner en liste med søgeresultater
            return FilteredList;
        }
    }
}