﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Page { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}