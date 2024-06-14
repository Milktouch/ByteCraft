using ByteCraft.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace ByteCraft
{
    internal class DataSection
    {
        public readonly List<Variable> variables = new();
        public DataSection(List<string> data, int firstLine)
        {
            string variableRegex = "^([A-Za-z][A-Za-z0-9._]*[A-Za-z0-9])\\s+([a-zA-Z][a-zA-Z0-9_]*[A-Za-z0-9])\\s*,\\s*(?:(?=\")(\"[ -~]*\")|([{A-Za-z][A-Za-z0-9_,]*[A-Za-z0-9}]))\\s*$";
            Regex regex = new Regex(variableRegex, RegexOptions.Multiline);
            regex.Options.HasFlag(RegexOptions.Compiled);
            foreach (string s in data)
            {
                if (s.Trim().Equals(""))
                {
                    continue;

                }

                if (!regex.IsMatch(s))
                {
                    throw new SyntaxError("Invalid data section", firstLine + data.IndexOf(s), s);
                }

                string[] parts = regex.Split(s);
                string type = parts[0];
                string name = parts[1];
                string value = parts[2];


            }
        }



    }

}

