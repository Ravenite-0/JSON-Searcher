using static Utils.Constants;
using static Repo.ItemRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using static Utils.SysUtils;

namespace Repo {
    public abstract class ItemSearch {
        public static void SearchItems(string[] input) {
            switch(input[1].ToUpper()) {
                case TBL_ORGANIZATION:
                    break;
                case TBL_TICKET:
                    break;
                case TBL_USER:
                    break;
                default:
                    ThrowError($"Invalid table {input[1]}. Please type HELP for more information regarding tables.");
                    break;
            }
        }


       

        

        
    }
}