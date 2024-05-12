string test = "12  34  56  78 8 9 0   ";
string[] strings = test.Split(',',2);
strings.ToList().ForEach((string s) => Console.WriteLine(s));