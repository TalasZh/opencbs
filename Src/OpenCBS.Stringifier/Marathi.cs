// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;

namespace OpenCBS.Stringifier
{
    public class Marathi : IStringifiable
    {
        private string[] _0To99 = new[]
        {
            "शून्य"
            , "एक"
            , "दो"
            , "तीन"
            , "चार"
            , "पांच"
            , "छह"
            , "सात"
            , "आठ"
            , "नौ"
            , "दस"
            , "ग्यारह"
            , "बारह"
            , "तेरह"
            , "चौदह"
            , "पंद्रह"
            , "सोलह"
            , "सत्रह"
            , "अठारह"
            , "उन्नीस"
            , "बीस"
            , "इीस"
            , "बाईस"
            , "तेइस"
            , "चौबीस"
            , "पच्चीस"
            , "छब्बीस"
            , "सताइस"
            , "अट्ठाइस"
            , "उनतीस"
            , "तीस"
            , "इकतीस"
            , "बतीस"
            , "तैंतीस"
            , "चौंतीस"
            , "पैंतीस"
            , "छतीस"
            , "सैंतीस"
            , "अड़तीस"
            , "उनतालीस"
            , "चालीस"
            , "इकतालीस"
            , "बयालीस"
            , "तैतालीस"
            , "चवालीस"
            , "पैंतालीस"
            , "छियालीस"
            , "सैंतालीस"
            , "अड़तालीस"
            , "उनचास"
            , "पचास"
            , "इक्यावन"
            , "बावन"
            , "तिरपन"
            , "चौवन"
            , "पचपन"
            , "छप्पन"
            , "सतावन"
            , "अठावन"
            , "उनसठ"
            , "साठ"
            , "इकसठ"
            , "बासठ"
            , "तिरसठ"
            , "चौंसठ"
            , "पैंसठ"
            , "छियासठ"
            , "सड़सठ"
            , "अड़सठ"
            , "उनहतर"
            , "सत्तर"
            , "इकहतर"
            , "बहतर"
            , "तिहतर"
            , "चौहतर"
            , "पचहतर"
            , "छिहतर"
            , "सतहतर"
            , "अठहतर"
            , "उन्नासी"
            , "अस्सी"
            , "इक्यासी"
            , "बयासी"
            , "तिरासी"
            , "चौरासी"
            , "पचासी"
            , "छियासी"
            , "सतासी"
            , "अट्ठासी"
            , "नवासी"
            , "नब्बे"
            , "इक्यानवे"
            , "बानवे"
            , "तिरानवे"
            , "चौरानवे"
            , "पचानवे"
            , "छियानवे"
            , "सतानवे"
            , "अट्ठानवे"
            , "निन्यानवे"
            , "सौ"
        };

        public string Stringify0to999(int amount)
        {
            if (amount < 99)
            {
                return _0To99[amount];
            }
            
            if (amount < 999)
            {
                int hundreds = amount/100;
                int remainder = amount%100;
                string retval = _0To99[hundreds] + " सौ";
                retval += " " + _0To99[remainder];
                return retval;
            }
            return "Should be less than 999";
        }

        public string Stringify(decimal amount)
        {
            int value = Convert.ToInt32(amount);
            if (value < 1000)
            {
                return Stringify0to999(value);
            }
            if (value < 1000000)
            {
                int thousands = value/1000;
                int remainder = value%1000;
                string retval = Stringify0to999(thousands) + " हजार ";
                retval += Stringify0to999(remainder);
                return retval;
            }
            if (value < 1000000000)
            {
                int millions = value/1000000;
                int remainder = value%1000000;
                int thousands = remainder/1000;
                remainder = remainder%1000;
                string retval = Stringify0to999(millions) + " मिलियन ";
                retval += Stringify0to999(thousands) + " हजार ";
                retval += Stringify0to999(remainder);
                return retval;
            }
            return "Cannot convert";
        }

        public string StringifyWithPercent(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
