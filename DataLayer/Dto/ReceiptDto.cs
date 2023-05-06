﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dto
{
    public class ReceiptDto
    {
        public int ReceiptId { get; set; }
        public string DistributerName { get; set; }
        public string DistributerFirm { get; set; }
        public string DateOfReceipt { get; set; }
        public string StringItems { get; set; }
    }
}
