/**
    Copyright Header - A utility to generate csv contacts file for several CPS
    Copyright (C) 2021 Jean-Michel Cohen <jmc_96@hotmail.com>
    
    This file is part of Copyright Header.
    
    Copyright Header is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    
    Copyright Header is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    
    You should have received a copy of the GNU General Public License
    along with Copyright Header.  If not, see <http://www.gnu.org/licenses/>.    
*/

#define FRENCH

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace shieldToTx
{
    class Program
    {
        enum TxType : int { HD1 = 0, D868, D878, GD77, MD380, RT73, DM1701, D578, LASTITEM };

        public List<string> options = new List<string>();

        char separator = ',';

        string noname()
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].Substring(0, 1) == "n")
                    return options[i].Substring(1);
            }

            return "";
        }
        bool firstletter()
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i] == "c")
                    return true;
            }

            return false;
        }

        bool capitalize()
        {
            for(int i=0; i < options.Count; i++)
            {
                if (options[i] == "m")
                    return true;
            }

            return false;
        }
        void parseContent(String content, TxType type)
        {
            String[] lines = content.Split("\r\n");

            switch (type) {
                case TxType.HD1:
                    // Console.WriteLine("Call Type,Contacts Alias,City,Province,Country,Call ID,");
                    break;

                case TxType.D868:
                    Console.WriteLine("Radio ID,Callsign,Name,City,State,Country,Remarks");
                    break;

                case TxType.D878:
                    Console.WriteLine("Radio ID,Callsign,Name,City,State,Country,Remarks,Call Type,Call Alert");
                    break;

                case TxType.GD77:
                    Console.WriteLine("Number,Name,Call ID,Type,Ring Style,Call Receive Tone,Repeater Slot override");
                    break;

                case TxType.MD380:  // and MD390, MD2017, RT82
                    Console.WriteLine("Radio ID, Callsign, Name, NickName, City, State, Country,");
                    break;

                case TxType.RT73: // and RT3S
                    Console.WriteLine("Radio ID, Callsign, Name, NickName, City, State/Prov, Country");
                    break;

                case TxType.DM1701:
                    Console.WriteLine("Radio ID,Callsign,Name,Nickname,City,State,Country,Remarks");
                    break;

                case TxType.D578:
                    Console.WriteLine("No,Radio ID, Callsign, Name, City, State, Country, Remarks, Call Type,Call Alert");
                    break;

                default:
                    break;
            }

            bool    capitalizeNames = capitalize();
            bool    firstLetter = firstletter();
            String  noName = noname();

            for (int i = 0; i < lines.Length; i++)
            {
                String[] user = lines[i].Split(separator);

                String shieldId = user[0];
                String name = user[1];
                String dmrId = "";

                if (name.Length == 0)
                    name = noName;
                if (firstLetter && name.Length > 0)
                    name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

                if (user.Length == 3) {
                    dmrId = user[2];
                }
                else
                {
                    String user1 = user[1];
                    String user2 = user[2];

                    if (firstLetter && user1.Length > 0)
                        user1 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user1.ToLower());
                    if (firstLetter && user2.Length > 0)
                        user2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user2.ToLower());

                    name = user1 + "-" + user2;
                    dmrId = user[3];
                }

                name = name.Trim();

                if (capitalizeNames)
                    name = name.ToUpper();

                String city = "";
                String state = "";
                String country;
                String remarks = "";
                String nickName = "";

                if (dmrId.Length > 7)
                    dmrId = dmrId.Substring(0, 7);     // remove any comment

                switch (shieldId.Substring(0, 2))
                {
                    case "FS":
                        country = "France";
                        state = Departements.getDepartement(shieldId.Substring(2, 2));
                        break;

                    case "ES":
                        country = "Espagne";
                        break;

                    case "US":
                        country = "USA";
                        break;

                    case "BS":
                        country = "Belgique";
                        break;

                    case "CS":
                        country = "Canada";
                        break;

                    case "HS":
                        country = "Suisse";
                        break;

                    default:
                        country = "";
                        break;
                }

                if (nickName.Length == 0)
                    nickName = noName;
                if (firstLetter && nickName.Length > 0)
                    nickName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nickName.ToLower());

                if (city.Length == 0)
                    city = noName;
                if (firstLetter && city.Length > 0)
                    city = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city.ToLower());

                if (state.Length == 0)
                    state = noName;
                if (firstLetter && state.Length > 0)
                    state = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(state.ToLower());

                if (country.Length == 0)
                    country = noName;
                if (firstLetter && country.Length > 0)
                    country = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(country.ToLower());

                if (capitalizeNames)
                {
                    nickName = nickName.ToUpper();
                    city = city.ToUpper();
                    state = state.ToUpper();
                    country = country.ToUpper();
                }

                switch (type)
                {
                    case TxType.HD1: // ailucne HD1
                        Console.WriteLine("Private Call," + shieldId + " " + name + "," + city + "," + state + "," + country + "," + dmrId);
                        break;

                    case TxType.D868: // AnyTone 868
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + city + "-" + state + "," + country + "," + remarks);
                        break;

                    case TxType.D878: // AnyTone 878
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + remarks + ",Private Call,None");
                        break;

                    case TxType.GD77: // GD77
                        Console.WriteLine(i + 1 + "," + shieldId + " " + name + "," + dmrId + "," + "Private Call" + "," + "On" + "," + "None" + "," + "None");
                        break;

                    case TxType.MD380:
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + nickName + "," + city + "," + state + "," + country + ",");
                        break;

                    case TxType.RT73:
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + nickName + "," + city + "," + state + "," + country);
                        break;

                    case TxType.DM1701:
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + nickName + "," + city + "," + state + "," + country + "," + remarks);
                        break;

                    case TxType.D578:
                        Console.WriteLine("," + dmrId + "," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + remarks + ",Private Call,None");
                        break;

                    default:
                        break;
                }
            }
        }

        public void usage()
        {
#if FRENCH
            Console.WriteLine("");
            Console.WriteLine(" shieldToTx v1.6 - Converti la base de données en ligne TheShield (tm) dans plusieurs formats de CPS");
            Console.WriteLine(" Copyright (c) 2021 Jean-Michel Cohen");
            Console.WriteLine("");
            Console.WriteLine(" Usage : ");
            Console.WriteLine("");
            Console.WriteLine("  - pour obtenir cette aide");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx");
            Console.WriteLine("");
            Console.WriteLine("  - pour créer un fichier");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx [0..{0}] {1}",  (int)Program.TxType.LASTITEM-1, " > nomdefichier.csv");
            Console.WriteLine("");
            Console.WriteLine("  - pour concaténer à un fichier existant");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx [0..{0}] {1}", (int)Program.TxType.LASTITEM - 1, " >> nomdefichier.csv\t(note: ne pas oublier, s'il y a lieu, de supprimer la ligne d'entête de l'ajout)");
            Console.WriteLine("");
            Console.WriteLine("  - pour mettre le nom en majuscules ajouter l'option -m");
            Console.WriteLine("");
            Console.WriteLine("  - pour mettre la première lettre du nom, ville, pays en majuscule ajouter l'option -c");
            Console.WriteLine("");
            Console.WriteLine("  - pour remplacer les champs vides par XXXX ajouter l'option -nXXXX");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx -m -nXXXX -c [0..{0}] {1}", (int)Program.TxType.LASTITEM - 1, " > nomdefichier.csv");
            Console.WriteLine("");
            Console.WriteLine(" avec\t0 pour Ailunce HD1");
            Console.WriteLine("\t1 pour AnyTone D868");
            Console.WriteLine("\t2 pour AnyTone D878");
            Console.WriteLine("\t3 pour Radioddity OpenGD77");
            Console.WriteLine("\t4 pour Tytera MD380, MD390, MD2017, Retevis RT82");
            Console.WriteLine("\t5 pour Retevis RT73, RT3S");
            Console.WriteLine("\t6 BaoFeng pour DM1701");
            Console.WriteLine("\t7 pour AnyTone D578");
#else
            Console.WriteLine("");
            Console.WriteLine(" shieldToTx v1.6 - Convert TheShield (tm) OnLine Database to several CPS formats");
            Console.WriteLine(" Copyright (c) 2021 Jean-Michel Cohen");
            Console.WriteLine("");
            Console.WriteLine(" Usage : ");
            Console.WriteLine("");
            Console.WriteLine("  - to get this help");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx");
            Console.WriteLine("");
            Console.WriteLine("  - to create file");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx [0..{0}] {1}",  (int)Program.TxType.LASTITEM-1, " > filename.csv");
            Console.WriteLine("");
            Console.WriteLine("  - to append to an existing file");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx [0..{0}] {1}",  (int)Program.TxType.LASTITEM-1, " >> filename.csv\t(note: don't forget to remove the appended header line)");
            Console.WriteLine("");
            Console.WriteLine("  - to capitalize the name add option -m");
            Console.WriteLine("");
            Console.WriteLine("  - to uppercase the first letter of name, city, country add option -c");
            Console.WriteLine("");
            Console.WriteLine("  - to replace empty fields by XXXX add option -nXXXX");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx -m [0..{0}] {1}",  (int)Program.TxType.LASTITEM-1, " > filename.csv");
            Console.WriteLine("");
            Console.WriteLine(" with\t0 for Ailunce HD1");
            Console.WriteLine("\t1 for AnyTone D868");
            Console.WriteLine("\t2 for AnyTone D878");
            Console.WriteLine("\t3 for Radioddity OpenGD77");
            Console.WriteLine("\t4 for Tytera MD380, MD390, MD2017, Retevis RT82");
            Console.WriteLine("\t5 for Retevis RT73, RT3S");
            Console.WriteLine("\t6 for BaoFeng DM1701");
            Console.WriteLine("\t7 for AnyTone D578");
#endif
        }

        static void Main(string[] args)
        {
            int num = 0;
            Program program = new Program();

            // check if enough parameters (i.e TX type)
            if (args.Length < 1)
            {
                program.usage();
                System.Environment.Exit(1);
            }

            for (int i = 0; i < args.Length; i++)
            {
                string option = args[i];

                // check for options
                if (option.StartsWith("-"))
                {
                    if (option.Length >= 1)
                    {
                        switch (option.Substring(1, 1))
                        {
                            case "m":   // Capitalize string
                            case "c":   // Capitalize first char
                                program.options.Add(option.Substring(1));
                                break;

                            case "n":   // noname XXX
                                if (option.Substring(1).Length > 1)
                                    program.options.Add(option.Substring(1));
                                else
                                {
                                    program.usage();
                                    System.Environment.Exit(1);
                                }
                                break;

                            default:
                                program.usage();
                                System.Environment.Exit(1);
                                break;
                        }
                    }
                    else
                    {
                        program.usage();
                        System.Environment.Exit(1);
                    }

                }
                else
                {
                    if (!int.TryParse(option, out num) || (num > (int)Program.TxType.LASTITEM-1))
                    {
                        program.usage();
                        System.Environment.Exit(1);
                    }
                }
            }

            String url = @"http://theshield.site/Liste_Shield.txt";

            WebClient client = new WebClient();

            Stream stream = client.OpenRead(url);
            if (stream == null)
            {
                Console.WriteLine("");
#if FRENCH
                Console.WriteLine(" Cannot load contact file from TheShield");
                Console.WriteLine(" Check your internet connection!");
#else
            Console.WriteLine(" Impossible de télécharger la base de contacts du Shield");
            Console.WriteLine(" Vérifier votre connexion internet !");
#endif
                Console.WriteLine("");
                System.Environment.Exit(1);
            }

            Console.WriteLine("");

            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            program.parseContent(content, (Program.TxType)num);

            System.Environment.Exit(0);
        }
    }
}
