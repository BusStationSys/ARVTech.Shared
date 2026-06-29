namespace ARVTech.Shared.Verifiers
{
    using System;

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
                int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                valueInput = valueInput.Trim().ToUpper();

                valueInput = valueInput.Replace(".", string.Empty)
                                       .Replace("-", string.Empty)
                                       .Replace("/", string.Empty);

                if (valueInput.Length != 14)
                    return false;

                string tempCnpj = valueInput.Substring(0, 12);

                int soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += GetNumericValue(tempCnpj[i]) * multiplicador1[i];

                int resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;

                string digito = resto.ToString();
                tempCnpj += digito;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += GetNumericValue(tempCnpj[i]) * multiplicador2[i];

                resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;

                digito += resto.ToString();

                return valueInput.EndsWith(digito);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the numeric value of a character.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static int GetNumericValue(char c)
        {
            if (char.IsDigit(c))
                return c - '0';

            if (char.IsLetter(c))
                return c - 'A' + 10;

            throw new ArgumentException("Caractere inválido no CNPJ.");
        }
    }
}