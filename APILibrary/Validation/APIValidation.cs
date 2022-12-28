using APILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APILibrary.Validation
{
    public static class APIValidation
    {
        public static bool IsJsonAPIResponseMoveModel(string jsonString)
        {
            //We need to split the string into the respective key value pairs and check they're the correct name and of the correct type
            List<string> listOfElements = GetListOfElements(jsonString);

            //List of elements should have 6 elements
            if(listOfElements.Count != 6)
            {
                return false;
            }

            List<string> listOfKeys = new List<string>();
            listOfKeys.Add("IsAllowed");
            listOfKeys.Add("HasWon");
            listOfKeys.Add("HasDrawn");

            for(int i = 0; i < listOfElements.Count; i++)
            {
                //Even elements are keys
                if(i % 2 == 0)
                {
                    if(!listOfKeys.Contains(listOfElements[i]))
                    {
                        return false;
                    }
                }

                //Odd elements are values
                //Take the lowercase value, is it true or false?
                if(!(i % 2 == 0))
                {
                    string lowercaseValue = listOfElements[i].ToLower();
                    if(lowercaseValue != "false" && lowercaseValue != "true")
                    {
                        return false;
                    }
                }
            }

            return true;



        }

        public static bool IsJsonAPIErrorModel(string jsonString)
        {
            List<string> listOfElements = GetListOfElements(jsonString);

            if(listOfElements.Count != 4)
            {
                return false;
            }

            List<string> listOfKeys = new List<string>();
            listOfKeys.Add("ErrorName");
            listOfKeys.Add("ErrorMessage");

            for(int i = 0; i < listOfElements.Count; i++)
            {
                if(i % 2 == 0)
                {
                    if (!listOfKeys.Contains(listOfElements[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsJsonAPIResponseInitModel(string jsonString)
        {
            //We need to split the string into the respective key value pairs and check they're the correct name and of the correct type
            List<string> listOfElements = GetListOfElements(jsonString);

            //List of elements should have 6 elements
            if (listOfElements.Count != 2)
            {
                return false;
            }

            List<string> listOfKeys = new List<string>();
            listOfKeys.Add("IsSuccessful");

            for (int i = 0; i < listOfElements.Count; i++)
            {
                //Even elements are keys
                if (i % 2 == 0)
                {
                    if (!listOfKeys.Contains(listOfElements[i]))
                    {
                        return false;
                    }
                }

                //Odd elements are values
                //Take the lowercase value, is it true or false?
                if (!(i % 2 == 0))
                {
                    string lowercaseValue = listOfElements[i].ToLower();
                    if (lowercaseValue != "false" && lowercaseValue != "true")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static List<string> GetListOfElements(string jsonString)
        {
            List<string> listOfElements = new List<string>();
            string temp = "";
            for (int i = 0; i < jsonString.Length; i++)
            {
                if (jsonString[i] == ',' || jsonString[i] == ':')
                {
                    //Get rid of whitespace and speech marks and brackets
                    temp = temp.Replace(" ", "");
                    temp = temp.Replace(",", "");
                    temp = temp.Replace("{", "");
                    temp = temp.Replace("}", "");
                    temp = temp.Replace("\"", "");
                    listOfElements.Add(temp);
                    temp = "";
                }
                else
                {


                    temp += jsonString[i];
                }
            }

            temp = temp.Replace(" ", "");
            temp = temp.Replace(",", "");
            temp = temp.Replace("{", "");
            temp = temp.Replace("}", "");
            temp = temp.Replace("\"", "");
            listOfElements.Add(temp);

            return listOfElements;
        }
    }
}
