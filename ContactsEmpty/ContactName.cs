using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsEmpty{

    public class ContactName {

        string firstName { get; set; }
        string lastName { get; set; }
        string middleInitial { get; set; }
        int contactId { get; set; }

        public ContactName(string lastName, string firstName, string middleInitial, int contactId) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleInitial = middleInitial;
            this.contactId = contactId;
        }

    }
}