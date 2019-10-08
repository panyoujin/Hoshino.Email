using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshino.Email.Entity
{
    public class FailEmailAccountEntity : EmailAccountEntity
    {
        public string FailMessage { get; set; }
    }
}
