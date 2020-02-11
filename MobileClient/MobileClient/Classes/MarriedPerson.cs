using System;
using System.Collections.Generic;
using System.Text;

namespace MobileClient.Classes
{
    public class MarriedPerson: Person
    {
        public string PartnerName { get; set; }
        public string PartnerAge { get; set; }

        public MarriedPerson() { }

        public MarriedPerson(int pId, string pName, string pAge, string ppName, string ppAge) : base(pId, pName, pAge)
        {
            PartnerName = ppName;
            PartnerAge = ppAge;
        }

        public override string PropertiesFirst
        {
            get
            {
                return string.Format("Женатый/замужний человек, имя: {0}", Name);
            }
        }

        public override string PropertiesSecond
        {
            get
            {
                return string.Format("Дата родения: {0}, имя мужа/жены: {1}, дата рождения мужа/жены: {2}", Age, PartnerName, PartnerAge);
            }
        }
    }
}
