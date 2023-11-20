namespace ARVTech.Shared.Verifiers
{
    /// <summary>
    /// Class responsible for CPF string type verification methods.
    /// </summary>
    public static class CpfVerifier
    {
        /// <summary>
        /// Checks whether a passed string is a CPF string type.
        /// </summary>
        /// <param name="valueInput">The string to be checked.</param>
        /// <returns><c>True</c> if the string is CPF string type, otherwise <c>False</c>.</returns>
        public static bool IsValid(string valueInput)
        {
            try
            {
                valueInput = valueInput.Trim();
                valueInput = valueInput.Replace(".", string.Empty);
                valueInput = valueInput.Replace("-", string.Empty);

                int[] num = new int[11];
                int soma;
                int i;
                int resultado1;
                int resultado2;

                long lngCPF = 0;
                try
                {
                    long.TryParse(valueInput, out lngCPF);
                }
                catch { }

                if (lngCPF > 0 && valueInput.Length == 11)
                {
                    for (i = 0; i <= num.Length - 1; i++)
                    {
                        num[i] = int.Parse(valueInput.Substring(i, 1));
                    }

                    soma = (num[0] * 10) +
                        (num[1] * 9) +
                        (num[2] * 8) +
                        (num[3] * 7) +
                        (num[4] * 6) +
                        (num[5] * 5) +
                        (num[6] * 4) +
                        (num[7] * 3) +
                        (num[8] * 2);
                    soma = soma - (11 * ((int)(soma / 11)));
                    if (soma == 0 || soma == 1)
                    {
                        resultado1 = 0;
                    }
                    else
                    {
                        resultado1 = 11 - soma;
                    }

                    if (resultado1 == num[9])
                    {
                        soma = (num[0] * 11) +
                            (num[1] * 10) +
                            (num[2] * 9) +
                            (num[3] * 8) +
                            (num[4] * 7) +
                            (num[5] * 6) +
                            (num[6] * 5) +
                            (num[7] * 4) +
                            (num[8] * 3) +
                            (num[9] * 2);
                        soma = soma - (11 * ((int)(soma / 11)));
                        if (soma == 0 || soma == 1)
                        {
                            resultado2 = 0;
                        }
                        else
                        {
                            resultado2 = 11 - soma;
                        }

                        if (resultado2 == num[10])
                        {
                            if (valueInput == "11111111111" ||
                                valueInput == "22222222222" ||
                                valueInput == "33333333333" ||
                                valueInput == "44444444444" ||
                                valueInput == "55555555555" ||
                                valueInput == "66666666666" ||
                                valueInput == "77777777777" ||
                                valueInput == "88888888888" ||
                                valueInput == "99999999999" ||
                                valueInput == "00000000000")
                                return false;

                            return true;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}