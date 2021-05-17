﻿using System;

namespace shieldToTx
{
    public class Departements
    {
        // code_departement,nom_departement,code_region,nom_region
        static String[] list = new String[] {
            "01,Ain,84,Auvergne-Rhône-Alpes",
            "02,Aisne,32,Hauts-de-France",
            "03,Allier,84,Auvergne-Rhône-Alpes",
            "04,Alpes-de-Haute-Provence,93,Provence-Alpes-Côte d'Azur",
            "05,Hautes-Alpes,93,Provence-Alpes-Côte d'Azur",
            "06,Alpes-Maritimes,93,Provence-Alpes-Côte d'Azur",
            "07,Ardèche,84,Auvergne-Rhône-Alpes",
            "08,Ardennes,44,Grand Est",
            "09,Ariège,76,Occitanie",
            "10,Aube,44,Grand Est",
            "11,Aude,76,Occitanie",
            "12,Aveyron,76,Occitanie",
            "13,Bouches-du-Rhône,93,Provence-Alpes-Côte d'Azur",
            "14,Calvados,28,Normandie",
            "15,Cantal,84,Auvergne-Rhône-Alpes",
            "16,Charente,75,Nouvelle-Aquitaine",
            "17,Charente-Maritime,75,Nouvelle-Aquitaine",
            "18,Cher,24,Centre-Val de Loire",
            "19,Corrèze,75,Nouvelle-Aquitaine",
            "21,Côte-d'Or,27,Bourgogne-Franche-Comté",
            "22,Côtes-d'Armor,53,Bretagne",
            "23,Creuse,75,Nouvelle-Aquitaine",
            "24,Dordogne,75,Nouvelle-Aquitaine",
            "25,Doubs,27,Bourgogne-Franche-Comté",
            "26,Drôme,84,Auvergne-Rhône-Alpes",
            "27,Eure,28,Normandie",
            "28,Eure-et-Loir,24,Centre-Val de Loire",
            "29,Finistère,53,Bretagne",
            "2A,Corse-du-Sud,94,Corse",
            "2B,Haute-Corse,94,Corse",
            "30,Gard,76,Occitanie",
            "31,Haute-Garonne,76,Occitanie",
            "32,Gers,76,Occitanie",
            "33,Gironde,75,Nouvelle-Aquitaine",
            "34,Hérault,76,Occitanie",
            "35,Ille-et-Vilaine,53,Bretagne",
            "36,Indre,24,Centre-Val de Loire",
            "37,Indre-et-Loire,24,Centre-Val de Loire",
            "38,Isère,84,Auvergne-Rhône-Alpes",
            "39,Jura,27,Bourgogne-Franche-Comté",
            "40,Landes,75,Nouvelle-Aquitaine",
            "41,Loir-et-Cher,24,Centre-Val de Loire",
            "42,Loire,84,Auvergne-Rhône-Alpes",
            "43,Haute-Loire,84,Auvergne-Rhône-Alpes",
            "44,Loire-Atlantique,52,Pays de la Loire",
            "45,Loiret,24,Centre-Val de Loire",
            "46,Lot,76,Occitanie",
            "47,Lot-et-Garonne,75,Nouvelle-Aquitaine",
            "48,Lozère,76,Occitanie",
            "49,Maine-et-Loire,52,Pays de la Loire",
            "50,Manche,28,Normandie",
            "51,Marne,44,Grand Est",
            "52,Haute-Marne,44,Grand Est",
            "53,Mayenne,52,Pays de la Loire",
            "54,Meurthe-et-Moselle,44,Grand Est",
            "55,Meuse,44,Grand Est",
            "56,Morbihan,53,Bretagne",
            "57,Moselle,44,Grand Est",
            "58,Nièvre,27,Bourgogne-Franche-Comté",
            "59,Nord,32,Hauts-de-France",
            "60,Oise,32,Hauts-de-France",
            "61,Orne,28,Normandie",
            "62,Pas-de-Calais,32,Hauts-de-France",
            "63,Puy-de-Dôme,84,Auvergne-Rhône-Alpes",
            "64,Pyrénées-Atlantiques,75,Nouvelle-Aquitaine",
            "65,Hautes-Pyrénées,76,Occitanie",
            "66,Pyrénées-Orientales,76,Occitanie",
            "67,Bas-Rhin,44,Grand Est",
            "68,Haut-Rhin,44,Grand Est",
            "69,Rhône,84,Auvergne-Rhône-Alpes",
            "70,Haute-Saône,27,Bourgogne-Franche-Comté",
            "71,Saône-et-Loire,27,Bourgogne-Franche-Comté",
            "72,Sarthe,52,Pays de la Loire",
            "73,Savoie,84,Auvergne-Rhône-Alpes",
            "74,Haute-Savoie,84,Auvergne-Rhône-Alpes",
            "75,Paris,11,Île-de-France",
            "76,Seine-Maritime,28,Normandie",
            "77,Seine-et-Marne,11,Île-de-France",
            "78,Yvelines,11,Île-de-France",
            "79,Deux-Sèvres,75,Nouvelle-Aquitaine",
            "80,Somme,32,Hauts-de-France",
            "81,Tarn,76,Occitanie",
            "82,Tarn-et-Garonne,76,Occitanie",
            "83,Var,93,Provence-Alpes-Côte d'Azur",
            "84,Vaucluse,93,Provence-Alpes-Côte d'Azur",
            "85,Vendée,52,Pays de la Loire",
            "86,Vienne,75,Nouvelle-Aquitaine",
            "87,Haute-Vienne,75,Nouvelle-Aquitaine",
            "88,Vosges,44,Grand Est",
            "89,Yonne,27,Bourgogne-Franche-Comté",
            "90,Territoire de Belfort,27,Bourgogne-Franche-Comté",
            "91,Essonne,11,Île-de-France",
            "92,Hauts-de-Seine,11,Île-de-France",
            "93,Seine-Saint-Denis,11,Île-de-France",
            "94,Val-de-Marne,11,Île-de-France",
            "95,Val-d'Oise,11,Île-de-France",
            "971,Guadeloupe,01,Guadeloupe",
            "972,Martinique,02,Martinique",
            "973,Guyane,03,Guyane",
            "974,La Réunion,04,La Réunion",
            "976,Mayotte,06,Mayotte"
        };

        public static String getDepartement(String depId)
        {
            if (depId.StartsWith("97"))
                return "DOM-TOM";

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].StartsWith(depId))
                {
                    String[] d = list[i].Split(',');
                    return StringExtensions.FoldToASCII(d[1]);
                }
            }

            return "";
        }
    }
}
