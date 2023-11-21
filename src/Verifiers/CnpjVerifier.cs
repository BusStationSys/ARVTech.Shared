namespace ARVTech.Shared.Verifiers
{
    /// <summary>
    /// Class responsible for CNPJ string type verification methods.
    /// </summary>
    public static class CnpjVerifier
    {
        /// <summary>
        /// Checks whether a passed string is a CNPJ string type.
        /// </summary>
        /// <param name="valueInput">The string to be checked.</param>
        /// <returns><c>True</c> if the string is CNPJ string type, otherwise <c>False</c>.</returns>
        public static bool IsValid(string valueInput)
        {
            try
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                valueInput = valueInput.Trim();
                valueInput = valueInput.Replace(".", string.Empty);
                valueInput = valueInput.Replace("-", string.Empty);
                valueInput = valueInput.Replace("/", string.Empty);

                if (valueInput.Length != 14)
                    return false;

                string tempCnpj = valueInput.Substring(0, 12);

                int soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                int resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                string digito = resto.ToString();

                tempCnpj += digito;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito += resto.ToString();

                return valueInput.EndsWith(digito);
            }
            catch
            {
                return false;
            }
        }
    }
}