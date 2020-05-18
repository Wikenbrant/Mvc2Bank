using System;
using System.Text;

namespace Domain.SearchModels
{
    partial class CustomerSearch
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!String.IsNullOrEmpty(CustomerId))
            {
                sb.Append($"{nameof(CustomerId)}: {CustomerId}\n");
            }

            if (!String.IsNullOrEmpty(Gender))
            {
                sb.Append($"{nameof(Gender)}: {Gender}\n");
            }

            if (!String.IsNullOrEmpty(Givenname))
            {
                sb.Append($"{nameof(Givenname)}: {Givenname}\n");
            }

            if (!String.IsNullOrEmpty(Surname))
            {
                sb.Append($"{nameof(Surname)}: {Surname}\n");
            }

            if (!String.IsNullOrEmpty(Streetaddress))
            {
                sb.Append($"{nameof(Streetaddress)}: {Streetaddress}\n");
            }

            if (!String.IsNullOrEmpty(City))
            {
                sb.Append($"{nameof(City)}: {City}\n");
            }

            if (!String.IsNullOrEmpty(Zipcode))
            {
                sb.Append($"{nameof(Zipcode)}: {Zipcode}\n");
            }

            if (!String.IsNullOrEmpty(Country))
            {
                sb.Append($"{nameof(Country)}: {Country}\n");
            }

            if (!String.IsNullOrEmpty(CountryCode))
            {
                sb.Append($"{nameof(CountryCode)}: {CountryCode}\n");
            }

            if (!String.IsNullOrEmpty(NationalId))
            {
                sb.Append($"{nameof(NationalId)}: {NationalId}\n");
            }

            if (!String.IsNullOrEmpty(Telephonecountrycode))
            {
                sb.Append($"{nameof(Telephonecountrycode)}: {Telephonecountrycode}\n");
            }

            if (!String.IsNullOrEmpty(Telephonenumber))
            {
                sb.Append($"{nameof(Telephonenumber)}: {Telephonenumber}\n");
            }

            //if (Dispositions != null && Dispositions.Count > 0)
            //{
            //    sb.Append($"{nameof(Dispositions)}: [{String.Join(", ", Dispositions)}]\n");
            //}

            if (!String.IsNullOrEmpty(Gender))
            {
                sb.Append($"{nameof(Gender)}: {Gender}\n");
            }

            return sb.ToString();
        }

    }
}
