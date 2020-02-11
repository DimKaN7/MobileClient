using System;
using System.Collections.Generic;
using System.Text;

namespace MobileClient.Classes
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }

        public Person() { }

        public Person(int pId, string pName, string pAge)
        {
            Id = pId;
            Name = pName;
            Age = pAge;
        }

        public virtual string PropertiesFirst
        {
            get
            {
                return string.Format("Человек, имя: {0}", Name);
            }
        }

        public virtual string PropertiesSecond
        {
            get
            {
                return string.Format("Дата родения: {0}", Age);
            }
        }
    }
}
