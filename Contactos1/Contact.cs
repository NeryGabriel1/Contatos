﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos1
{

    class Contact
{
    private string name;
    private string phone;

    public Contact()
    {
        name = "";
        phone = "";
    }

    public Contact(string nombre, string telefono)
    {
        name = nombre;
        phone = telefono;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Contact c = (Contact)obj;
        return Name == c.Name && Phone == c.Phone;
    }
    public override int GetHashCode()
    {
        int hashName = Name == null ? 0 : Name.GetHashCode();
        int hashPhone = Phone == null ? 0 : Phone.GetHashCode();
        return hashName ^ hashPhone;
    }

}
}


