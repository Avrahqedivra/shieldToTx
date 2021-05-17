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

using System;
using System.IO;
using System.Net;

namespace shieldToTx
{
    class Program
    {
        void parseContent(String content, int type)
        {
            String[] lines = content.Split("\r\n");

            switch(type) { 
                case 1:
                    Console.WriteLine("Radio ID,Callsign,Name,City,State,Country,Remarks");
                    break;

                case 2:
                    Console.WriteLine("Radio ID,Callsign,Name,City,State,Country,Remarks,Call Type,Call Alert");
                    break;

                default:
                    break;
            }

            for (int i=0; i < lines.Length; i++)
            {
                String[] user = lines[i].Split(',');

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

                switch(type)
                {
                    case 0: // ailucne HD1
                        Console.WriteLine("Private Call," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + dmrId);
                        break;

                    case 1: // AnyTone 868
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + remarks);
                        break;

                    case 2: // AnyTone 878
                        Console.WriteLine(dmrId + "," + shieldId + "," + name + "," + city + "," + state + "," + country + "," + remarks + ",Private Call,None");
                        break;

                    default:
                        break;
                }                    
            }
        }

        void usage()
        {
            Console.WriteLine("");
            Console.WriteLine("shieldToTx v1.1 - Convert TheShield (tm) OnLine Database to several CPS formats");
            Console.WriteLine("Copyright (c) 2021 Jean-Michel Cohen");
            Console.WriteLine("");
            Console.WriteLine("Usage: shieldToTx [0, 1, 2] > filename.csv");
            Console.WriteLine("");
            Console.WriteLine("with\t0 for Ailunce HD1");
            Console.WriteLine("\t1 for AnyTone D868");
            Console.WriteLine("\t2 for AnyTone D878");
        }

        static void Main(string[] args)
        {
            Program program = new Program();

            if (args.Length < 1)
            {
                program.usage();
                System.Environment.Exit(1);
            }

            switch(args[0])
            {
                case "0":
                case "1":
                case "2":
                    break;

                default:
                    program.usage();
                    System.Environment.Exit(1);
                    break;
            }

            String url = @"http://theshield.site/Liste_Shield.txt";

            WebClient client = new WebClient();
            
            Stream stream = client.OpenRead(url);
            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                program.parseContent(content, Int16.Parse(args[0]));
            }

            System.Environment.Exit(0);
        }
    }
}
