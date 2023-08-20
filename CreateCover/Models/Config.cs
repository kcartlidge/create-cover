namespace CreateCover.Models
{
    public class Config
    {
        public Dictionary<string, Tuple<string, bool, bool>> Allowed = new();

        public SortedList<string, string> ProvidedStrings = new();
        public SortedList<string, int> ProvidedInts = new();

        public bool HasError => Errors.Any();
        public Dictionary<string, string> Errors = new();

        private readonly Dictionary<string, Action<Config, string, object>> CustomValidators = new();

        /// <summary>Defines all expected parameters.</summary>
        /// <param name="examplesOfAllowed">
        /// Each parameter must follow the format:
        ///   `file="cover.svg" *`
        ///
        /// The argument is left of the equals sign.
        /// To the right is an example value.
        /// If the example is quoted a String is expected, otherwise an Int.
        /// If the entry ends with `*` then it's compulsory.
        /// </param>
        /// <example>
        /// new Config("file=\"cover.svg\" *", "titlefontsize=180");
        /// </example>
        public Config(params string[] examplesOfAllowed)
        {
            foreach (var example in examplesOfAllowed)
            {
                string[] bits = SplitParam(example);
                if (Allowed.ContainsKey(bits[0])) throw new Exception($"Duplicate example: {example}");
                var rqd = false;
                if (bits[1].EndsWith('*'))
                {
                    bits[1] = bits[1].TrimEnd('*').TrimEnd();
                    rqd = true;
                }
                Allowed.Add(
                    bits[0],
                    Tuple.Create(bits[1], bits[1].StartsWith('"'), rqd)
                );
            }
        }

        public void AddCustomValidator(
            string key,
            Action<Config, string, object> validator)
        {
            CustomValidators.Add(key, validator);
        }

        public void ShowInfo()
        {
            Console.WriteLine("ARGUMENTS:");
            var width = Allowed.Max(x => x.Key.Length);
            foreach (var item in Allowed)
            {
                var k = item.Key.PadRight(width);
                var v = item.Value.Item1;
                var t = item.Value.Item2 ? "string" : "integer";
                var r = item.Value.Item3 ? "*" : " ";
                Console.WriteLine($"  -{k}  {r} {t,-7}  eg {v}");
            }
            if (Allowed.Any(x => x.Value.Item3))
            {
                Console.WriteLine();
                Console.WriteLine("  * = required");
            }
        }

        public void SetFrom(string[] args)
        {
            // Normalise.
            var lowerArgs = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                string[] bits = SplitParam(arg);
                if (bits.Length != 2) continue;

                var lbit = bits[0].ToLowerInvariant();
                if (lowerArgs.ContainsKey(lbit))
                {
                    AddError(lbit, "appears more than once");
                    continue;
                }
                lowerArgs.Add(lbit, bits[1]);

                // Unknown argument?
                if (Allowed.ContainsKey(lbit)) continue;
                AddError(lbit, "is not recognised");
            }

            // Allowed strings.
            foreach (var k in Allowed.Where(x => x.Value.Item2))
            {
                if (lowerArgs.ContainsKey(k.Key))
                    ProvidedStrings.Add(k.Key, lowerArgs[k.Key]);
            }

            // Allowed ints.
            foreach (var k in Allowed.Where(x => x.Value.Item2 == false))
            {
                if (lowerArgs.ContainsKey(k.Key))
                {
                    int v;
                    if (int.TryParse(lowerArgs[k.Key], out v))
                        ProvidedInts.Add(k.Key, v);
                    else
                        AddError(k.Key, "value is not an integer");
                }
            }

            // Missing requirements.
            foreach (var k in Allowed.Where(x => x.Value.Item3))
            {
                if (k.Value.Item2 && ProvidedStrings.ContainsKey(k.Key)) continue;
                if ((k.Value.Item2 == false) && ProvidedInts.ContainsKey(k.Key)) continue;
                AddError(k.Key, "is missing");
            }

            // Custom validators.
            foreach (var validator in CustomValidators)
            {
                if (ProvidedStrings.ContainsKey(validator.Key))
                    validator.Value(this, validator.Key, ProvidedStrings[validator.Key]);
                if (ProvidedInts.ContainsKey(validator.Key))
                    validator.Value(this, validator.Key, ProvidedInts[validator.Key]);
            }
        }

        public void ShowAnyErrors()
        {
            if (HasError == false) return;
            var width = Errors.Max(x => x.Key.Length) + 1;

            Console.WriteLine();
            Console.WriteLine("ERRORS:");
            foreach (var item in Allowed)
                if (Errors.ContainsKey(item.Key))
                    Console.WriteLine($"  '{item.Key}'{Spaces(item.Key, width)} {Errors[item.Key]}");
            foreach (var item in Errors.Where(x => Allowed.ContainsKey(x.Key) == false))
                Console.WriteLine($"  '{item.Key}'{Spaces(item.Key, width)} {Errors[item.Key]}");
        }

        public void ShowProvided()
        {
            var strWidth = ProvidedStrings.Max(x => x.Key.Length) + 1;
            var intWidth = ProvidedInts.Max(x => x.Key.Length) + 1;
            var width = Math.Max(strWidth, intWidth);

            Console.WriteLine();
            Console.WriteLine("PROVIDED:");
            foreach (var item in Allowed)
            {
                if (ProvidedStrings.Any(x => x.Key == item.Key))
                {
                    var k = item.Key.PadRight(width);
                    var v = ProvidedStrings[item.Key];
                    Console.WriteLine($"  -{k}  \"{v}\"");
                }
                if (ProvidedInts.Any(x => x.Key == item.Key))
                {
                    var k = item.Key.PadRight(width);
                    var v = ProvidedInts[item.Key];
                    Console.WriteLine($"  -{k}  {v}");
                }
            }
        }

        public void AddError(string key, string message)
        {
            if (Errors.ContainsKey(key)) Errors[key] += ", and " + message;
            else Errors[key] = message;
        }

        private string Spaces(string alreadyPresent, int width)
        {
            return "".PadLeft(width - alreadyPresent.Length);
        }

        private string[] SplitParam(string parameter)
        {
            var bits = parameter.Split('=', 2, StringSplitOptions.TrimEntries);
            if (bits.Length != 2)
            {
                AddError(bits[0], $"isn't key=value");
                return new string[0];
            }
            bits[0] = bits[0].ToLowerInvariant().Replace("-", "");
            return bits;
        }
    }
}
