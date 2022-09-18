using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.Excel;


namespace LETTER_DAL.Models
{
    public class Clients
    {
        [Column(1)]
        public string ClientName { get; set; } = string.Empty;
        [Column(2)]
        public string Id { get; set; } = string.Empty;
        [Column(3)]
        public string GeneralContract { get;set; } = string.Empty;
        [Column(4)]
        public string ContractNumber { get; set; } = string.Empty;
        [Column(8)]
        public string Currency { get; set; } = string.Empty;
        [Column(9)]
        public decimal AmountToPay { get; set; }
        [Column(10)]
        public string CharReward { get; set; } = string.Empty;
        [Column(11)]
        public string Rate { get; set; } = string.Empty;
        [FormulaResult]
        [Column(12)]
        public string CurrReward { get; set; } = string.Empty;
        
        [FormulaResult]
        [Column(Letter = "M")]
        [DataFormat(0xf)]
        public string DateStart { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;

        [FormulaResult]
        [Column(Letter = "N")]
        [DataFormat(0xf)]
        public string DateEnd { get; set; } = string.Empty;

        [FormulaResult]
        [Column(Letter = "O")]
        public decimal ToPayment { get; set; } 

        public string TotalNumberOfDaysInMonth(int month)
        {
            return DateTime.DaysInMonth(DateTime.Now.Year, month).ToString();
        }
    }
}
