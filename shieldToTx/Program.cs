/**
    Copyright Header - A utility to generate csv files for several CPS
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
        public enum TxType : int { HD1 = 0, D868, D878, GD77, MD380 };

        public List<string> options = new List<string>();

        char separator = ',';

        bool capitalize()
        {
            for(int i=0; i < options.Count; i++)
            {
                if (options[i] == "m")
                    return true;
            }

            return false;
        }
        public void parseContent(String content, TxType type)
        {
            String[] lines = content.Split("\r\n");

            switch (type) {
                case TxType.HD1:
                    Console.WriteLine("Radio ID,Callsign,Name,City,State,Country,Remarks");
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

                case TxType.MD380:
                    Console.WriteLine("Radio ID, Callsign, Name, NickName, City, State, Country,");
                    break;

                default:
                    break;
            }

            bool capitalizeNames = capitalize();

            for (int i = 0; i < lines.Length; i++)
            {
                String[] user = lines[i].Split(separator);

                String shieldId = user[0];
                String name = user[1];
                String dmrId = "";

                if (user.Length == 3) {
                    dmrId = user[2];
                }
                else
                {
                    name = user[1] + "-" + user[2];
                    dmrId = user[3];
                }

                name = name.Trim();

                if (capitalizeNames)
                    name = name.ToUpper();

                String city = "";
                String state = "";
                String country;
                String remarks = "";

                if (dmrId.Length > 7)
                    dmrId = dmrId.Substring(0, 7);     // remove any comment

                switch (shieldId.Substring(0, 2))
                {
                    case "FS":
                        country = "France";
                        state = Departements.getDepartement(shieldId.Substring(2, 2));
                        break;

                    case "ES":
                        country = "Spain";
                        break;

                    case "US":
                        country = "USA";
                        break;

                    case "BS":
                        country = "Belgium";
                        break;

                    case "CS":
                        country = "Canada";
                        break;

                    case "HS":
                        country = "Swiss";
                        break;

                    default:
                        country = "";
                        break;
                }

                switch (type)
                {
                    case TxType.HD1: // ailucne HD1
                        Console.WriteLine("Private Call," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + dmrId);
                        break;

                    case TxType.D868: // AnyTone 868
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + remarks);
                        break;

                    case TxType.D878: // AnyTone 878
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + remarks + ",Private Call,None");
                        break;

                    case TxType.GD77: // GD77
                        Console.WriteLine(i + 1 + "," + shieldId + " " + name + "," + dmrId + "," + "Private Call" + "," + "On" + "," + "None" + "," + "None");
                        break;

                    case TxType.MD380:
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + "," + city + "," + state + "," + country + ",");
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
            Console.WriteLine(" shieldToTx v1.4 - Converti la base de données en ligne TheShield (tm) dans plusieurs formats de CPS");
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
            Console.WriteLine("\tshieldToTx [0..4] > nomdefichier.csv");
            Console.WriteLine("");
            Console.WriteLine("  - pour concaténer à un fichier existant");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx [0.." + sizeof(TxType) + "] >> nomdefichier.csv\t(note: ne pas oublier, s'il y a lieu, de supprimer la ligne d'entête de l'ajout)");
            Console.WriteLine("");
            Console.WriteLine("  - pour mettre le nom en majuscules ajouter l'option -m");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx -m [0.." + sizeof(TxType) + "] > nomdefichier.csv");
            Console.WriteLine("");
            Console.WriteLine(" avec\t0 pour Ailunce HD1");
            Console.WriteLine("\t1 pour AnyTone D868");
            Console.WriteLine("\t2 pour AnyTone D878");
            Console.WriteLine("\t3 pour OpenGD77");
            Console.WriteLine("\t4 pour MD380, MD390, MD2017, RT82");
#else
            Console.WriteLine("");
            Console.WriteLine(" shieldToTx v1.4 - Convert TheShield (tm) OnLine Database to several CPS formats");
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
            Console.WriteLine("\tshieldToTx [0..4] > filename.csv");
            Console.WriteLine("");
            Console.WriteLine("  - to append to an existing file");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx [0.."+ sizeof(TxType) + "] >> filename.csv\t(note: don't forget to remove the appended header line)");
            Console.WriteLine("");
            Console.WriteLine("  - to capitalize the name add option -m");
            Console.WriteLine("");
            Console.WriteLine("\tshieldToTx -m [0.." + sizeof(TxType) + "] > filename.csv");
            Console.WriteLine("");
            Console.WriteLine(" with\t0 for Ailunce HD1");
            Console.WriteLine("\t1 for AnyTone D868");
            Console.WriteLine("\t2 for AnyTone D878");
            Console.WriteLine("\t3 for OpenGD77");
            Console.WriteLine("\t4 for MD380, MD390, MD2017, RT82");
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
                    option = option.Substring(1);
                    if (option.Length == 1)
                    {
                        switch (option)
                        {
                            case "m":   // Capitalize string
                            case "c":   // Capitalize first char
                                program.options.Add(option);
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
                    if (!int.TryParse(option, out num) || (num > sizeof(Program.TxType)))
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

            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            program.parseContent(content, (Program.TxType)num);

            System.Environment.Exit(0);
        }
    }
}
