using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class Ticket
    {
        public string Number { get; private set; }
        public Contact Owner { get; private set; }

        public Ticket(string number, Contact owner)
        {
            this.Number = number;
            this.Owner = owner;
        }
    }

    public class Contact
    {
        public string Name { get; private set; }
        public Company Company { get; private set; }

        public Contact(string name, Company company)
        {
            this.Name = name;
            this.Company = company;
        }
    }

    public class Company
    {
        public string Name { get; private set; }
        public double Revenue { get; private set; }

        public Company(string name, double revenue)
        {
            this.Name = name;
            this.Revenue = revenue;
        }
    }
}
