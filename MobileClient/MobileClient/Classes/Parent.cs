using System;
using System.Collections.Generic;
using System.Text;

namespace MobileClient.Classes
{
    public class Parent: Person
    {
        public int ChildCount { get; set; }

        public Parent() { }

        public Parent(int pId, string pName, string pAge, int pChildCount) : base(pId, pName, pAge)
        {
            ChildCount = pChildCount;
        }
        
        public override string PropertiesFirst
        {
            get
            {
                return string.Format("Родитель, имя: {0}", Name);
            }
        }

        public override string PropertiesSecond
        {
            get
            {
                return string.Format("Дата родения: {0}, количество детей: {1}", Age, ChildCount);
            }
        }
    }
}
